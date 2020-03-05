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
                Application.Current.MainPage = new MasterMenu();
            }
            else
            {
                DisplayAlert("Sin internet", "El dispositivo no esta conectado a internet, verifique su conexión.", "Aceptar");
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