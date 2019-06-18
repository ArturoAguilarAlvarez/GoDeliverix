using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPuestoTacos.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrdenDescripcionConfirmar : ContentPage
	{
        VMOrden ObjItem;
        ListView MyListviewOrdenesRecibidas;
        public OrdenDescripcionConfirmar (VMOrden ObjItem, ListView MyListviewOrdenesRecibidas)
		{
			InitializeComponent ();

            MyListviewOrdenesConfirmar.ItemsSource = App.MVOrden.ListaDeProductos;
            this.ObjItem = ObjItem;
            this.MyListviewOrdenesRecibidas = MyListviewOrdenesRecibidas;

        }

        private async void ButtonCancelar_Clicked(object sender, EventArgs e)
        {
                await Navigation.PopToRootAsync();
                await PopupNavigation.Instance.PushAsync(new AppPuestoTacos.Popup.ConfirmarCancelacionOrden(ObjItem, MyListviewOrdenesRecibidas));
        }

        private async void ButtonConfirmar_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("", "¿Confirmar esta orden?", "Si", "No");
            if (action)
            {
                ConfirmarOrden();
            }
        }

        public void ConfirmarOrden()
        {
            App.MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EC09BCDE-ADAC-441D-8CC1-798BC211E46E"), "S", AppPuestoTacos.Helpers.Settings.Licencia, UidOrden: ObjItem.Uidorden);
            App.MVOrden.AgregaEstatusALaOrden(new Guid("2d2f38b8-7757-45fb-9ca6-6ecfe20356ed"), UidOrden: ObjItem.Uidorden, UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), StrParametro: "S");
            App.MVTarifario.AgregarCodigoAOrdenTarifario(UidCodigo: Guid.NewGuid(), UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), uidorden: ObjItem.Uidorden);

            App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S");
            MyListviewOrdenesRecibidas.ItemsSource = null;
            MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar;


            Navigation.PopToRootAsync();
        }
    }
}