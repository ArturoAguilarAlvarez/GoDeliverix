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
	public partial class Ajustes_CorreoElectronico : ContentPage
    {
		public Ajustes_CorreoElectronico ()
		{
			InitializeComponent ();
		}
        public async void CloseWindowsPopup(object sender, EventArgs e)
        {
            await App.Navigator.PopToRootAsync();
        }
    }
}