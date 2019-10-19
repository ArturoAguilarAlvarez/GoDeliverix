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
using System.Net.Http;
using Newtonsoft.Json;
using AppCliente.WebApi;
using Newtonsoft.Json.Linq;
using Foundation;
using Com.OneSignal;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {

        #region Propiedades
        Guid _id = Guid.Empty;
        bool _acceso;
        #endregion

        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //string currentuserID = (Application.Current.Properties["Userr"].ToString());
            AppCliente.Helpers.Settings.ClearAllData();
            LimpiarPerfil();
        }

        private async void Button_Login(object sender, EventArgs e)
        {
            btnLogin.IsEnabled = false;
            var current = Connectivity.NetworkAccess;

            //await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());

            if (current == NetworkAccess.Internet)
            {
                IsBusy = true;
                //popupLoadingView.IsVisible = true;
                //activityIndicator.IsRunning = true;

                string usuario = txtUsuario.Text;
                string password = txtIDContraseña.Text;
                Ingresar(usuario, password);

            }
            else
            {
                //await  PopupNavigation.Instance.PopAsync(true);
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

       

        protected async void Ingresar(string Usuario, string Contrasena)
        {
            try
            {
                acLogin.IsRunning = true;
                acLogin.IsVisible = true;
                string url = "https://www.godeliverix.net/api/Profile/GET?Usuario=" + Usuario + "&Contrasena=" + Contrasena;
                string content = "";
                using (HttpClient _client = new HttpClient())
                {
                    string contenido = await _client.GetStringAsync(url);
                    content = JsonConvert.DeserializeObject<ResponseHelper>(contenido).Data.ToString();
                };

                _id = new Guid(content);

                if (_id != Guid.Empty)
                {
                    App.Global1 = _id.ToString();
                    _acceso = true;
                }
                if (_acceso)
                {
                    string strDirecciones = string.Empty;
                    using (HttpClient _client = new HttpClient())
                    {
                        var tex = "https://www.godeliverix.net/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + _id;
                        strDirecciones = await _client.GetStringAsync(tex);
                    }

                    var obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                    App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
                    OneSignal.Current.SetExternalUserId(App.Global1);
                    if (GuardarContraseña.IsToggled && string.IsNullOrEmpty(AppCliente.Helpers.Settings.UserName) && string.IsNullOrEmpty(AppCliente.Helpers.Settings.Password))
                    {
                        AppCliente.Helpers.Settings.UserName = txtUsuario.Text;
                        AppCliente.Helpers.Settings.Password = txtIDContraseña.Text;
                        Application.Current.MainPage = new MasterMenu();
                        //await PopupNavigation.Instance.PopAsync();
                    }
                    else
                    {
                        Application.Current.MainPage = new MasterMenu();
                        //await PopupNavigation.Instance.PopAsync();
                    }
                    Application.Current.Properties["IsLogged"] = true;

                }
                else
                {
                    // await PopupNavigation.Instance.PopAsync();

                    await DisplayAlert("Error", "Contraseña o usuario incorrecto", "ok");
                }
                acLogin.IsRunning = false;
                acLogin.IsVisible = false;
                //}
            }
            catch (Exception)
            {
                acLogin.IsRunning = false;
                acLogin.IsVisible = false;
                //await PopupNavigation.Instance.PopAsync();
                await DisplayAlert("Aviso del sistema", "No hay internet", "OK");
            }
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