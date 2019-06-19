using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppPuestoTacos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            txtSucursal.Text = AppPuestoTacos.Helpers.Settings.NombreSucursal;
        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            btnLogin.IsEnabled = false;
            await PopupNavigation.Instance.PushAsync(new AppPuestoTacos.Popup.Loanding());

            string perfil;
            try
            {
                string usuario = txtUsuario.Text;
                string password = txtIDContraseña.Text;
                if (!string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(password))
                {
                    Guid Uidusuario = App.MVAcceso.Ingresar(usuario, password);
                    App.UIdUsuario = Uidusuario;
                    if (Uidusuario != Guid.Empty)
                    {
                        perfil = App.MVAcceso.PerfilDeUsuario(Uidusuario.ToString());
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
                                    await PopupNavigation.Instance.PopAsync();
                                    await DisplayAlert("Error", "este usuario no es de esta sucursal", "ok");
                                }
                            }
                            else
                            {
                                await PopupNavigation.Instance.PopAsync();
                                await DisplayAlert("Error", "Debe ingresar como Administrador por primera vez", "ok");
                            }
                            App.NOmbreUsuario = txtUsuario.Text;
                        }
                        //Administrador
                        else if (perfil.ToUpper() == "76A96FF6-E720-4092-A217-A77A58A9BF0D")
                        {
                            App.NOmbreUsuario = txtUsuario.Text;
                            string Licencia = Helpers.Settings.Licencia;

                            App.MVEmpresas.ObtenerNombreComercial(App.UIdUsuario.ToString());
                            App.NombreEmpresa = App.MVEmpresas.NOMBRECOMERCIAL;
                            App.NOmbreUsuario = usuario;

                            if (string.IsNullOrEmpty(Licencia))
                            {
                                Guid UidEmpresa = App.MVUsuarios.ObtenerIdEmpresa(Uidusuario.ToString());
                                App.MVSucursal.DatosGridViewBusquedaNormal(UidEmpresa.ToString());
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
                            //App.MVUsuarios.BusquedaDeUsuario(UidUsuario: Id, UIDPERFIL: new Guid(perfil), UidEmpresa: UidEmpresa);
                            //DisplayAlert("","Administrador","ok");
                        }

                    }
                }
                else
                    await NewMethod();
                await PopupNavigation.Instance.PopAllAsync();
            }
            catch (Exception)
            {
                btnLogin.IsEnabled = true;
                await PopupNavigation.Instance.PopAllAsync();
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
