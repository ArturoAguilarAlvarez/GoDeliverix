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
	public partial class Home_Entregar : ContentPage
	{
       
        public Home_Entregar ()
		{
			InitializeComponent ();
		}

        private async void BtnFinalizar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}