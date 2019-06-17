using Repartidores_GoDeliverix.Views.Popup;
using Rg.Plugins.Popup.Services;
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
		}

        private async void BtnHistorico_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopoLoading());
            await Navigation.PushAsync(new Historico_Bitacora());
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}