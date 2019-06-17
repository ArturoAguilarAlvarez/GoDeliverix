using Rg.Plugins.Popup.Services;
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
	public partial class Historico_DetalleDia : ContentPage
	{
		public Historico_DetalleDia ()
		{
			InitializeComponent ();
		}

        private async void BtnHistorico_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopoLoading());
            await Navigation.PushAsync(new Historico_DetalleOrdenes());
            await PopupNavigation.Instance.PopAllAsync();
        }
        
    }
}