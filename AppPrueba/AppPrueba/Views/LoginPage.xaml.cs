using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using AppPrueba.WebApi;
using System.Net.Http;

namespace AppPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            AILoading.IsVisible = false;

            if (string.IsNullOrEmpty(AppPrueba.Helpers.Settings.NombreSucursal))
            {
                lblSucursal.Text = "Sin sucursal asignada";
            }
            else
            {
                lblSucursal.Text = "Sucursal " + AppPrueba.Helpers.Settings.NombreSucursal;
            }
        }


        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {

            HttpClient _client = new HttpClient();
            string url;
            btnLogin.IsEnabled = false;
            AILoading.IsVisible = true;
            AILoading.IsRunning = true;
            //await PopupNavigation.Instance.PushAsync(new AppPrueba.Popup.Loanding());

            string perfil;
            try
            {
                string usuario = txtUsuario.Text;
                string password = txtIDContraseña.Text;
                if (!string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(password))
                {
                    string Uid;
                    using (HttpClient _WebApiGoDeliverix = new HttpClient())
                    {
                        url = RestService.Servidor + "api/Profile/GET?Usuario=" + usuario + "&Contrasena=" + password + "";
                        string content = await _WebApiGoDeliverix.GetStringAsync(url);
                        Uid = content = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    }
                    Guid Uidusuario = new Guid(Uid);
                    App.UIdUsuario = Uidusuario;
                    if (Uidusuario != Guid.Empty)
                    {
                        url = RestService.Servidor + "api/Profile/GetProfileType?UidUsuario=" + Uidusuario.ToString();
                        string strDirecciones = await _client.GetStringAsync(url);
                        perfil = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                        //Supervisior
                        if (perfil.ToUpper() == "81232596-4C6B-4568-9005-8D4A0A382FDA")
                        {
                            if (!string.IsNullOrEmpty(AppPrueba.Helpers.Settings.Licencia))
                            {
                                string sucursal = App.MVSucursal.ObtenSucursalDeLicencia(AppPrueba.Helpers.Settings.Licencia);
                                if (App.MVSucursal.VerificaExistenciaDeSupervisor(Uidusuario.ToString(), sucursal))
                                {
                                    AppPrueba.Helpers.Settings.Perfil = perfil;
                                    AppPrueba.Helpers.Settings.Usuario = usuario;
                                    AppPrueba.Helpers.Settings.Uidusuario = Uidusuario.ToString();
                                    AppPrueba.Helpers.Settings.Contrasena = password;
                                    Guid UidTurno = Guid.NewGuid();
                                    AppPrueba.Helpers.Settings.UidTurno = UidTurno.ToString();
                                    //Inicio deturno movil
                                    url = RestService.Servidor + "api/Turno/GetTurnoSuministradora?UidUsuario=" + Uidusuario.ToString() + "&UidTurno=" + UidTurno.ToString();
                                    await _client.GetStringAsync(url);

                                    App.MVEmpresas.ObtenerNombreComercial(App.UIdUsuario.ToString());
                                    App.NombreEmpresa = App.MVEmpresas.NOMBRECOMERCIAL;
                                    App.NOmbreUsuario = usuario;
                                    Application.Current.Properties["IsLogged"] = true;
                                    App.Current.MainPage = new Views.MasterMenu();
                                    AILoading.IsVisible = false;
                                    AILoading.IsRunning = false;
                                }
                                else
                                {
                                    AILoading.IsVisible = false;
                                    AILoading.IsRunning = false;
                                    await DisplayAlert("Error", "este usuario no es de esta sucursal", "ok");
                                }
                            }
                            else
                            {
                                AILoading.IsVisible = false;
                                AILoading.IsRunning = false;
                                await DisplayAlert("Error", "Debe ingresar como Administrador por primera vez", "ok");
                            }
                            App.NOmbreUsuario = txtUsuario.Text;
                        }
                        //Administrador
                        else if (perfil.ToUpper() == "76A96FF6-E720-4092-A217-A77A58A9BF0D")
                        {

                            App.NOmbreUsuario = txtUsuario.Text;
                            string Licencia = Helpers.Settings.Licencia;
                            url = RestService.Servidor + "api/Empresa/GetNombreEmpresa?UIdUsuario=" + App.UIdUsuario.ToString();
                            string NombreEmpresa = await _client.GetStringAsync(url);
                            App.NombreEmpresa = JsonConvert.DeserializeObject<ResponseHelper>(NombreEmpresa).Data.ToString();
                            App.NOmbreUsuario = usuario;

                            if (string.IsNullOrEmpty(Licencia))
                            {
                                Helpers.Settings.Perfil = perfil;
                                AppPrueba.Helpers.Settings.Usuario = usuario;
                                AppPrueba.Helpers.Settings.Contrasena = password;
                                await Navigation.PushAsync(new Views.SeleccionarSucursalLicencia(perfil));
                            }
                            else
                            {
                                App.Perfil = perfil;
                                AppPrueba.Helpers.Settings.Perfil = perfil;
                                AppPrueba.Helpers.Settings.Usuario = usuario;
                                AppPrueba.Helpers.Settings.Contrasena = password;


                                url = RestService.Servidor + "api/Empresa/GetNombreEmpresa?UIdUsuario=" + App.UIdUsuario.ToString();
                                NombreEmpresa = await _client.GetStringAsync(url);
                                App.NombreEmpresa = JsonConvert.DeserializeObject<ResponseHelper>(NombreEmpresa).Data.ToString();
                                AILoading.IsVisible = false;
                                AILoading.IsRunning = false;
                                Application.Current.Properties["IsLogged"] = true;
                                App.Current.MainPage = new Views.MasterMenu();//Perfil
                            }
                        }
                        else
                        {
                            AILoading.IsVisible = false;
                            AILoading.IsRunning = false;
                            await DisplayAlert("", "Usuario invalido", "OK");
                        }
                    }
                    else
                    {
                        AILoading.IsVisible = false;
                        AILoading.IsRunning = false;
                        await DisplayAlert("", "Usuario o contraseña incorrecta", "OK");
                    }
                }
                else
                {
                    await NewMethod();
                }
            }
            catch (Exception f)
            {
                btnLogin.IsEnabled = true;
                AILoading.IsVisible = false;
                AILoading.IsRunning = false;
                await DisplayAlert("", "No tiene Internet " + f.Message + "", "ok");
            }
            AILoading.IsVisible = false;
            AILoading.IsRunning = false;
            btnLogin.IsEnabled = true;
        }

        private async Task NewMethod()
        {
            await DisplayAlert("", "Acceso incorrecto", "ok");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}