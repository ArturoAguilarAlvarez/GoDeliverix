using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPuestoTacos.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrdenesCanceladasPermanente : ContentPage
	{
		public OrdenesCanceladasPermanente ()
		{
			InitializeComponent ();
            App.MVOrden.BuscarOrdenesAppSucursal("Sucursal", UidLicencia: new Guid((AppPuestoTacos.Helpers.Settings.Licencia)), EstatusSucursal: "Canceladas", TipoDeSucursal: "S");
            MyListviewOrdenesRecuperarOrden.ItemsSource = App.MVOrden.ListaDeOrdenesCanceladasPermanentes;
        }
	}
}