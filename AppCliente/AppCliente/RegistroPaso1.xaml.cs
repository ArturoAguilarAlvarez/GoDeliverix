using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistroPaso1 : ContentPage
	{
		public RegistroPaso1 ()
		{
			InitializeComponent ();
		}

        private void Button_Siguiente(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistroPaso2());
        }
    }
}