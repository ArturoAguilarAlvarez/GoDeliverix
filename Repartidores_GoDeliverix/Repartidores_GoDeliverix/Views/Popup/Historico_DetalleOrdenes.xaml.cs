
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Repartidores_GoDeliverix.Views.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Historico_DetalleOrdenes : ContentPage
	{
		public Historico_DetalleOrdenes ()
		{
			InitializeComponent ();
		}

        private async void Btnver_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Historico_DetalleOrden());
        }
    }
}