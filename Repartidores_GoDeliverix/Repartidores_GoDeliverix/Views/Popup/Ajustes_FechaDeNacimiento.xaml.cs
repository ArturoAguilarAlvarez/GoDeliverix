using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Repartidores_GoDeliverix.Views.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Ajustes_FechaDeNacimiento : PopupPage
    {
		public Ajustes_FechaDeNacimiento ()
		{
			InitializeComponent ();
		}
        public async void CloseWindowsPopup(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}