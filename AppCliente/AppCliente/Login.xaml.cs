using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using VistaDelModelo;
using Rg.Plugins.Popup.Services;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {

        #region Propiedades

        #endregion

        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //string currentuserID = (Application.Current.Properties["Userr"].ToString());
            AppCliente.Helpers.Settings.ClearAllData();
            LimpiarPerfil();
        }

        private async  void Button_Login(object sender, EventArgs e)
        {
            btnLogin.IsEnabled = false;
            var current = Connectivity.NetworkAccess;

            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
 
            if (current == NetworkAccess.Internet)
            {
            IsBusy = true;
            //popupLoadingView.IsVisible = true;
            //activityIndicator.IsRunning = true;

            string usuario = txtUsuario.Text;
            string password = txtIDContraseña.Text;
                if (Ingresar(usuario, password))
                {
                    App.MVDireccion.ObtenerDireccionesUsuario(App.Global1);
                    if (GuardarContraseña.IsToggled)
                    {

                    AppCliente.Helpers.Settings.UserName = txtUsuario.Text;
                    AppCliente.Helpers.Settings.Password = txtIDContraseña.Text;

                    //App.MVTelefono.TipoDeTelefonos();
                    //App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");
                    //App.MVCorreoElectronico.BuscarCorreos(UidPropietario: new Guid(AppCliente.App.Global1), strParametroDebusqueda: "Usuario");
                    //App.MVUsuarios.obtenerUsuario(AppCliente.App.Global1);
                    //App.MVDireccion.ObtenerDireccionesUsuario(AppCliente.App.Global1);

                    //for (int i = 0; i < MVDireccion.ListaDIRECCIONES.Count; i++)
                    //{
                    //    MVUbicacion.RecuperaUbicacionDireccion(MVDireccion.ListaDIRECCIONES[i].ID.ToString());
                    //}



                    Application.Current.MainPage = new MasterMenu();

                        //switch (Device.RuntimePlatform)
                        //    {
                        //        case Device.iOS:
                        //            Application.Current.MainPage = new NavigationPage(new TabsMain());
                        //            //Application.Current.MainPage = new HomePage();
                        //        break;
                        //        case Device.Android:
                        //        // Navigation.PushAsync(new RegistroPaso1());
                        //        Application.Current.MainPage = new MasterDetailPage1();

                        //        //Application.Current.MainPage = new HomePage();
                        //        break;
                        //        case Device.UWP:
                        //        case Device.macOS:
                        //        default:
                        //            // This is just an example. You wouldn't actually need to do this, since Padding is already 0 by default.
                        //            this.Padding = new Thickness(0);
                        //            break;
                        //    }

                        await PopupNavigation.Instance.PopAsync();
                    }
                    else
                    {

                        //App.MVTelefono.TipoDeTelefonos();
                        //App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");
                        //App.MVUsuarios.obtenerUsuario(AppCliente.App.Global1);
                        //App.MVDireccion.ObtenerDireccionesUsuario(AppCliente.App.Global1);
                        //App.MVCorreoElectronico.BuscarCorreos(UidPropietario: new Guid(AppCliente.App.Global1), strParametroDebusqueda: "Usuario");


                        Application.Current.MainPage = new MasterMenu();
                        //switch (Device.RuntimePlatform)
                        //{
                        //    case Device.iOS:
                        //        Application.Current.MainPage = new MasterDetailPage1();
                        //        break;
                        //    case Device.Android:
                        //        Application.Current.MainPage = new MasterDetailPage1();
                        //        break;
                        //    case Device.UWP:
                        //    case Device.macOS:
                        //    default:
                        //        // This is just an example. You wouldn't actually need to do this, since Padding is already 0 by default.
                        //        this.Padding = new Thickness(0);
                        //        break;
                        //}
                        await PopupNavigation.Instance.PopAsync();
                    }

                }
                else
                {
                    await PopupNavigation.Instance.PopAsync();

                    await DisplayAlert("Error", "Contraseña o usuario incorrecto", "ok");
                }

            }                     
            else
            {
                 await  PopupNavigation.Instance.PopAsync(true);
                 await DisplayAlert("Sorry", "Revisa tu conexión a internet e intenta otra vez", "ok");
            }

            btnLogin.IsEnabled = true;
        }

        private void Button_Siguiente(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistroPaso1());
        }

        private void Button_RecuperarContrasena(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecuperarPassword());
        }

        private async void DoSomething()
        {

            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
        }

        protected bool Ingresar(string Usuario,string Contrasena)
        {
            bool acceso = false;
            try
            {
                if (!string.IsNullOrEmpty(Usuario) && !string.IsNullOrEmpty(Contrasena))
                {
                    Guid id = Guid.Empty;
                    id = App.MVAcceso.Ingresar(Usuario, Contrasena);
                    if (id != Guid.Empty)
                    {
                        App.Global1 = id.ToString();
                        acceso = true;
                    }
                }
            }
            catch (Exception)
            {

                DisplayAlert("sorry", "No hay internet", "ok");
            }

            return acceso;
        }

        public void LimpiarPerfil()
        {
            App.Global1 = "";
            App.Usuario = "";
            App.Contrasena = "";

            App.buscarPor = "";
            App.giro = "";
            App.categoria = "";
            App.subcategoria = "";
            App.ListaCarrito = new List<VMProducto>();

            App.DireccionABuscar = "";

            App.MVAcceso = new VMAcceso();
            App.MVSucursales = new VMSucursales();
            App.MVUsuarios = new VMUsuarios();
            App.MVTelefono = new VMTelefono();
            App.MVUbicacion = new VMUbicacion();
            App.MVGiro = new VMGiro();
            App.MVCategoria = new VMCategoria();
            App.MVSubCategoria = new VMSubCategoria();
            App.MVImagen = new VMImagen();
            App.MVOferta = new VMOferta();
            App.MVSeccion = new VMSeccion();
            App.MVTarifario = new VMTarifario();
            App.MVProducto = new VMProducto();
            App.MVEmpresa = new VMEmpresas();

            App.MVOrden = new VMOrden();


            App.MVCorreoElectronico = new VMCorreoElectronico();
            App.MVDireccion = new VMDireccion();
        }
    }
}