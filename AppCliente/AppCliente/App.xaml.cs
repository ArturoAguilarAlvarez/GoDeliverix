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
using System.Net.Http;
using Newtonsoft.Json;
using AppCliente.WebApi;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppCliente
{
    public partial class App : Application
    {
        #region Propiedades globales
        private HttpClient _client = new HttpClient();
        public static string Global1 = "";
        public static string FolioUsuario = "";
        public static string Usuario = "";
        public static string Contrasena = "";

        public static string buscarPor = "";
        public static string giro = "";
        public static string categoria = "";
        public static string subcategoria = "";
        public static List<VMProducto> ListaCarrito = new List<VMProducto>();

        public static string UidColoniaABuscar = "";
        public static string UidEstadoABuscar = "";
        public static string DireccionABuscar = "";

        public static VMAcceso MVAcceso = new VMAcceso();
        public static VMSucursales MVSucursales = new VMSucursales();
        public static VMUsuarios MVUsuarios = new VMUsuarios();
        public static VMTelefono MVTelefono = new VMTelefono();
        public static VMUbicacion MVUbicacion = new VMUbicacion();
        public static VMGiro MVGiro = new VMGiro();
        public static VMPagos oPago = new VMPagos();
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

        public static string Navegacion;

        public static string MessageFromNotification = "";


        public App()
        {
            InitializeComponent();
            SetCultureToUSEnglish();
            Usuario = AppCliente.Helpers.Settings.UserName;
            Contrasena = AppCliente.Helpers.Settings.Password;
            var current = Connectivity.NetworkAccess;
            if (string.IsNullOrEmpty(Usuario) || string.IsNullOrEmpty(Contrasena))
            {
                // MainPage = new NavigationPage(new Login());
                MainPage = new MasterMenu();

            }
            else
            {
                if (current == NetworkAccess.Internet)
                {

                    if (Ingresar().Result)
                    {
                        MVDireccion.ObtenerDireccionesUsuario(Global1);

                        MainPage = new MasterMenu();

                    }
                    else
                    {
                        MainPage = new NavigationPage(new MasterMenu());
                        //MainPage = new NavigationPage(new Login());
                    }
                }
                else
                {
                    MainPage = new NavigationPage(new ErrorConectividadPage());
                }
            }

        }

        private void OnHandleNotificationOpened(Com.OneSignal.Abstractions.OSNotificationOpenedResult result)
        {
            if (result.notification.payload.additionalData.ContainsKey("additional_message"))
            {
                // Si el payload posee la key additional_message, ejecutar esta seccion de codigo
                MessageFromNotification = result.notification.payload.additionalData["additional_message"].ToString();
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
            base.OnSleep();
        }


        protected override void OnResume()
        {
            MasterMenuMenuItem objeto = new MasterMenuMenuItem { Id = 0, Title = "", TargetType = typeof(HomePage) };

            switch (Navegacion)
            {
                case "Login":
                    objeto = new MasterMenuMenuItem { Id = 3, Title = "Inciar sesión", TargetType = typeof(Login) };
                    break;
                case "UsuarioDirecciones":
                    objeto = new MasterMenuMenuItem { Id = 3, Title = "Direcciones", TargetType = typeof(UsuarioDirecciones) };
                    break;
                case "HomePage":
                    objeto = new MasterMenuMenuItem { Id = 3, Title = "Busqueda", TargetType = typeof(HomePage) };
                    break;
                case "Monedero":
                    objeto = new MasterMenuMenuItem { Id = 3, Title = "Monedero", TargetType = typeof(Monedero) };
                    break;
                case "HistorialPage":
                    objeto = new MasterMenuMenuItem { Id = 3, Title = "Historial", TargetType = typeof(HistorialPage) };
                    break;
                case "PerfilTelefonoPage":
                    objeto = new MasterMenuMenuItem { Id = 3, Title = "Telefonos", TargetType = typeof(PerfilTelefonoPage) };
                    break;
                case "PerfilGeneralPage":
                    objeto = new MasterMenuMenuItem { Id = 3, Title = "Perfil", TargetType = typeof(PerfilGeneralPage) };
                    break;
            }
            var Page = (Page)Activator.CreateInstance(objeto.TargetType);
            App app = Application.Current as App;
            MasterDetailPage md = (MasterDetailPage)app.MainPage;
            md.Detail = new NavigationPage(Page);
        }

        protected async Task<bool> Ingresar()
        {
            bool acceso = false;
            if (!string.IsNullOrEmpty(Usuario) && !string.IsNullOrEmpty(Contrasena))
            {
                Guid id = Guid.Empty;
                id = MVAcceso.Ingresar(Usuario, Contrasena);
                try
                {
                    using (HttpClient _WebApiGoDeliverix = new HttpClient())
                    {
                        string url = "" + Helpers.Settings.sitio + "/api/Profile/GET?Usuario=" + Usuario + "&Contrasena=" + Contrasena + "";

                        string content = await _WebApiGoDeliverix.GetStringAsync(url);
                        id = new Guid(content = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString());
                    }

                    Guid UidUsuario = id;
                    if (UidUsuario != null && UidUsuario != Guid.Empty)
                    {
                        string perfil;
                        using (HttpClient _WebApiGoDeliverix = new HttpClient())
                        {
                            string url = "" + Helpers.Settings.sitio + "/api/Profile/GetProfileType?UidUsuario=" + UidUsuario + "";
                            string content = await _WebApiGoDeliverix.GetStringAsync(url);
                            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                            perfil = obj.ToString();
                        }

                        //Entrada solo para perfil de cliente
                        if (perfil.ToUpper() == "4F1E1C4B-3253-4225-9E46-DD7D1940DA19")
                        {
                            VMUsuarios MVUsuario = new VMUsuarios();
                            using (var _webAppi = new HttpClient())
                            {
                                string url = "" + Helpers.Settings.sitio + "/ api/Usuario/GetBuscarUsuarios?UidUsuario=" + UidUsuario + "&UIDPERFIL=4F1E1C4B-3253-4225-9E46-DD7D1940DA19";
                                string content = await _webAppi.GetStringAsync(url);
                                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                MVUsuario = JsonConvert.DeserializeObject<VistaDelModelo.VMUsuarios>(obj);
                            }
                            if (UidUsuario != Guid.Empty)
                            {
                                App.Global1 = UidUsuario.ToString();
                                acceso = true;
                            }
                        }
                        else
                        {
                            GenerateMessage("Datos invalidos", "El usuario no es cliente", "Aceptar");
                        }
                    }
                    else
                    {
                        GenerateMessage("Datos invalidos", "El usuario no existe", "Aceptar");
                    }
                }
                catch (Exception)
                {
                    GenerateMessage("Sin coneccion a internet", "No se pudo conectar con los servicios", "OK");
                    throw;
                }


            }
            return acceso;
        }
        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }

    }
}
