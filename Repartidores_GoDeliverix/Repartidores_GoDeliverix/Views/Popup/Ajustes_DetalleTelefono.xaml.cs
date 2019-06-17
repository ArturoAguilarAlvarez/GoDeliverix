using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace Repartidores_GoDeliverix.Views.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Ajustes_DetalleTelefono : PopupPage
    {
		public Ajustes_DetalleTelefono ()
		{
			InitializeComponent ();
		}
        public async void CloseWindowsPopup(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}