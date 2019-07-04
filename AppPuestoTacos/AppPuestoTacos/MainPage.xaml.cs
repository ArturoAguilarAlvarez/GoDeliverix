using AppPuestoTacos.WebApi;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppPuestoTacos
{
    public partial class MainPage : ContentPage
    {

        HttpClient _client = new HttpClient();
        string url = "";

        public MainPage()
        {
            InitializeComponent();
            txtSucursal.Text = AppPuestoTacos.Helpers.Settings.NombreSucursal;
        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            btnLogin.IsEnabled = false;
            // await PopupNavigation.Instance.PushAsync(new AppPuestoTacos.Popup.Loanding());

            string perfil;
            try
            {
                string usuario = txtUsuario.Text;
                string password = txtIDContraseña.Text;
                if (!string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(password))
                {
                    url = RestService.Servidor + "api/Profile/GET?Usuario=" + usuario + "&Contrasena=" + password;
                    string content = await _client.GetStringAsync(url);
                    List<string> listaID = JsonConvert.DeserializeObject<List<string>>(content);
                    Guid Uidusuario = new Guid(listaID[0].ToString());
                    App.UIdUsuario = Uidusuario;
                    if (Uidusuario != Guid.Empty)
                    {
                        url = RestService.Servidor+"api/Profile/GetProfileType?UidUsuario="+Uidusuario.ToString();
                        string strDirecciones = await _client.GetStringAsync(url);
                        perfil = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();                       
                        //Supervisior
                        if (perfil.ToUpper() == "81232596-4C6B-4568-9005-8D4A0A382FDA")
                        {
                            if (!string.IsNullOrEmpty(AppPuestoTacos.Helpers.Settings.Licencia))
                            {
                                string sucursal = App.MVSucursal.ObtenSucursalDeLicencia(AppPuestoTacos.Helpers.Settings.Licencia);
                                if (App.MVSucursal.VerificaExistenciaDeSupervisor(Uidusuario.ToString(), sucursal))
                                {
                                    AppPuestoTacos.Helpers.Settings.Perfil = perfil;
                                    AppPuestoTacos.Helpers.Settings.Usuario = usuario;
                                    AppPuestoTacos.Helpers.Settings.Contrasena = password;
                                    App.MVEmpresas.ObtenerNombreComercial(App.UIdUsuario.ToString());
                                    App.NombreEmpresa = App.MVEmpresas.NOMBRECOMERCIAL;
                                    App.NOmbreUsuario = usuario;
                                    App.Current.MainPage = new View.MasterMenu();//Xam.Plugins.Settings
                                    
                                }
                                else
                                {
                                   // await PopupNavigation.Instance.PopAsync();
                                    await DisplayAlert("Error", "este usuario no es de esta sucursal", "ok");
                                }
                            }
                            else
                            {
                                // await PopupNavigation.Instance.PopAllAsync();
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
                                //Guid UidEmpresa = App.MVUsuarios.ObtenerIdEmpresa(Uidusuario.ToString());

                                //App.MVSucursal.DatosGridViewBusquedaNormal(UidEmpresa.ToString());


                                Helpers.Settings.Perfil = perfil;
                                AppPuestoTacos.Helpers.Settings.Usuario = usuario;
                                AppPuestoTacos.Helpers.Settings.Contrasena = password;
                                await Navigation.PushAsync(new View.SeleccionarSucursalLicencia(perfil));
                            }
                            else
                            {
                                App.Perfil = perfil;
                                AppPuestoTacos.Helpers.Settings.Perfil = perfil;
                                AppPuestoTacos.Helpers.Settings.Usuario = usuario;
                                AppPuestoTacos.Helpers.Settings.Contrasena = password;
                                App.MVEmpresas.ObtenerNombreComercial(App.UIdUsuario.ToString());
                                App.NombreEmpresa = App.MVEmpresas.NOMBRECOMERCIAL;
                                App.Current.MainPage =new View.MasterMenu();//Perfil
                            }
                        }
                        else
                        {
                          //  await PopupNavigation.Instance.PopAllAsync();
                            await DisplayAlert("", "Usuario invalido", "OK");
                        }

                    }
                    else
                    {
                       // await PopupNavigation.Instance.PopAllAsync();
                        await DisplayAlert("", "Usuario o contraseña incorrecta", "OK");
                    }
                }
                else
                {

                    // await PopupNavigation.Instance.PopAllAsync();
                    await NewMethod();
                }
            }
            catch (Exception)
            {
                btnLogin.IsEnabled = true;
                // await PopupNavigation.Instance.PopAllAsync();
                await DisplayAlert("", "No tiene Internet", "ok");
            }

            btnLogin.IsEnabled = true;
        }

        private async Task NewMethod()
        {
            await DisplayAlert("", "Acceso incorrecto", "ok");
        }

        private  void Button_Clicked_1(object sender, EventArgs e)
        {
            
        }
    }
}
