using Repartidores_GoDeliverix.Views.Popup;
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
	public partial class Historial : ContentPage
	{
		public Historial ()
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
        }

        private async void BtnHistorico_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Historico_Bitacora());
        }
    }
}