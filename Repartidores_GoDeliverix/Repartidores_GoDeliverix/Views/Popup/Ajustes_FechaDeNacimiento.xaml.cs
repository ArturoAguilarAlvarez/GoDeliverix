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
	public partial class Ajustes_FechaDeNacimiento : ContentPage
    {
		public Ajustes_FechaDeNacimiento ()
		{
			InitializeComponent ();
		}
        public async void CloseWindowsPopup(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}