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
	public partial class Historico_Bitacora : ContentPage
	{
		public Historico_Bitacora ()
		{
			InitializeComponent ();
		}

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        //private async void Btnver_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Historico_DetalleDia());
        //}
    }
}