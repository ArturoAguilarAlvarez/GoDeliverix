using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupProductoComentario : PopupPage
	{
		public PopupProductoComentario ()
		{
			InitializeComponent ();
		}


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}