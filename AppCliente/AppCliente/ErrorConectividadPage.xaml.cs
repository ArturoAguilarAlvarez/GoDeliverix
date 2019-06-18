using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCliente;
using Xamarin.Essentials;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErrorConectividadPage : ContentPage
    {
        public ErrorConectividadPage()
        {
            InitializeComponent();
        }

        private void Button_ReintentarConexion(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                if (Ingresar())
                {

                    App.MVTelefono.TipoDeTelefonos();
                    App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(App.Global1), ParadetroDeBusqueda: "Usuario");
                    App.MVCorreoElectronico.BuscarCorreos(UidPropietario: new Guid(App.Global1), strParametroDebusqueda: "Usuario");
                    App.MVDireccion.ObtenerDireccionesUsuario(App.Global1);
                    //for (int i = 0; i < MVDireccion.ListaDIRECCIONES.Count; i++)
                    //{
                    //    MVUbicacion.RecuperaUbicacionDireccion(MVDireccion.ListaDIRECCIONES[i].ID.ToString());
                    //}
                    // App.MVUsuarios.obtenerUsuario(App.Global1);
                    //Application.Current.MainPage = new MasterDetailPage1();
                    //switch (Device.RuntimePlatform)
                    //{
                    //    case Device.iOS:
                    //        Application.Current.MainPage = new TabsMain();
                    //        //Application.Current.MainPage = new NavigationPage(new TabsMain());
                    //        break;
                    //    case Device.Android:
                    //        Application.Current.MainPage = new MasterDetailPage1();
                    //        break;
                    //    case Device.UWP:
                    //    case Device.macOS:
                    //    default:
                    //        // This is just an example. You wouldn't actually need to do this, since Padding is already 0 by default.
                    //        break;
                    //}
                    //MainPage = new MasterDetailPage1();

                }
                else
                {
                    Application.Current.MainPage = new Login();
                }
            }
            else
            {
                DisplayAlert("Sorry", "Revisa tu conexión a internet e intenta otra vez", "ok");
            }
        }



        protected bool Ingresar()
        {
            bool acceso = false;
            if (!string.IsNullOrEmpty(App.Usuario) && !string.IsNullOrEmpty(App.Contrasena))
            {
                Guid id = Guid.Empty;
                id = App.MVAcceso.Ingresar(App.Usuario, App.Contrasena);
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