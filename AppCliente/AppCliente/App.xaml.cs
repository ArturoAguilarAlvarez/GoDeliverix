using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VistaDelModelo;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Rg.Plugins.Popup.Services;
using Com.OneSignal;
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppCliente
{
    public partial class App : Application
    {
        #region Propiedades globales
        public static string Global1 = "";
        public static string Usuario = "";
        public static string Contrasena = "";

        public static string buscarPor = "";
        public static string giro = "";
        public static string categoria = "";
        public static string subcategoria = "";
        public static List<VMProducto> ListaCarrito = new List<VMProducto>();

        public static string DireccionABuscar = "";

        public static VMAcceso MVAcceso = new VMAcceso();
        public static VMSucursales MVSucursales = new VMSucursales();
        public static VMUsuarios MVUsuarios = new VMUsuarios();
        public static VMTelefono MVTelefono = new VMTelefono();
        public static VMUbicacion MVUbicacion = new VMUbicacion();
        public static VMGiro MVGiro = new VMGiro();
        public static VMCategoria MVCategoria = new VMCategoria();
        public static VMSubCategoria MVSubCategoria = new VMSubCategoria();
        public static VMImagen MVImagen = new VMImagen();
        public static VMOferta MVOferta = new VMOferta();
        public static VMSeccion MVSeccion = new VMSeccion();
        public static VMTarifario MVTarifario = new VMTarifario();
        public static VMProducto MVProducto = new VMProducto();
        public static VMEmpresas MVEmpresa = new VMEmpresas();
        public static VMOrden MVOrden = new VMOrden();
        public static VMCorreoElectronico MVCorreoElectronico = new VMCorreoElectronico();
        public static VMDireccion MVDireccion = new VMDireccion();
        public static List<VMProducto> ListaDeProductos = new List<VMProducto>();
        public static List<VMEmpresas> LISTADEEMPRESAS = new List<VMEmpresas>();
        #endregion

        public App()
        {
            InitializeComponent();
            SetCultureToUSEnglish();
            Usuario = AppCliente.Helpers.Settings.UserName;
            Contrasena = AppCliente.Helpers.Settings.Password;
            var current = Connectivity.NetworkAccess;
            if (string.IsNullOrEmpty(Usuario) || string.IsNullOrEmpty(Contrasena))
            {
                MainPage = new NavigationPage(new Login());
            }
            else
            {
                if (current == NetworkAccess.Internet)
                {

                    if (Ingresar())
                    {
                        MVDireccion.ObtenerDireccionesUsuario(Global1);

                        MainPage = new MasterMenu();
                        OneSignal.Current.StartInit("YOUR_ONESIGNAL_APP_ID")
                  .EndInit();
                    }
                    else
                    {
                        MainPage = new NavigationPage(new Login());
                    }

                }
                else
                {
                    MainPage = new NavigationPage(new ErrorConectividadPage());
                }
            }

        }




        private void SetCultureToUSEnglish()
        {
            CultureInfo englishUSCulture = new CultureInfo("es-MX");
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            if (Application.Current.Properties.ContainsKey("IsLogged"))
            {
                MainPage = new MasterMenu();
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
        }

        protected bool Ingresar()
        {
            bool acceso = false;
            if (!string.IsNullOrEmpty(Usuario) && !string.IsNullOrEmpty(Contrasena))
            {
                Guid id = Guid.Empty;
                id = MVAcceso.Ingresar(Usuario, Contrasena);
                if (id != Guid.Empty)
                {
                    App.Global1 = id.ToString();
                    acceso = true;
                }
            }
            return acceso;
        }
    }
}
