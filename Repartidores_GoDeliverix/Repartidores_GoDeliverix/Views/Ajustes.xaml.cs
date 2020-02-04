using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Repartidores_GoDeliverix.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Ajustes : ContentPage
	{
		public Ajustes ()
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.Android)
            {
                SLContenido.Padding = new Thickness(0, 0, 0, 0);
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                SLContenido.Padding = new Thickness(20, 20, 20, 0);
            }
            MuestraPanel("General");
        }

        private void BtnGeneral_Clicked(object sender, EventArgs e)
        {
            MuestraPanel("General");
        }

        private void BtnDireccion_Clicked(object sender, EventArgs e)
        {
            MuestraPanel("Direccion");
        }

        private void BtnTelefono_Clicked(object sender, EventArgs e)
        {
            MuestraPanel("Telefono");
        }

        protected void MuestraPanel(string Panel)
        {
            if (Panel == "General")
            {
                PanelGeneral.IsVisible = true;
            }
            else
            {
                PanelGeneral.IsVisible = false;
            }
            if (Panel == "Direccion")
            {
                PanelDireccion.IsVisible = true;
            }
            else
            {
                PanelDireccion.IsVisible = false;
            }
            if (Panel == "Telefono")
            {
                PanelTelefono.IsVisible = true;
            }
            else
            {
                PanelTelefono.IsVisible = false;
            }
        }
    }
}