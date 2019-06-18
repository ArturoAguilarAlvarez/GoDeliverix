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
	public partial class OrdenDescripcionPorElaborar : ContentPage
	{
        VMOrden ObjItem;
        ListView MyListviewOrdenesPorRealizar;

        public OrdenDescripcionPorElaborar (VMOrden ObjItem,ListView MyListviewOrdenesPorRealizar)
		{
			InitializeComponent ();
            this.ObjItem = ObjItem;
            App.MVOrden.ObtenerProductosDeOrden(ObjItem.Uidorden.ToString());
            MyListviewOrdenesEnPreparacion.ItemsSource = App.MVOrden.ListaDeProductos;
            this.MyListviewOrdenesPorRealizar = MyListviewOrdenesPorRealizar;
        }

        private async void ImageButtonFinalizar_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("Finalizar?", "¿A concluido el proceso de elaboracion de la orden "+ ObjItem.LNGFolio+"?", "Si", "No");
            if (action)
            {
                try
                {
                    App.MVOrden.AgregaEstatusALaOrden(new Guid("c412d367-7d05-45d8-aeca-b8fabbf129d9"), UidOrden: ObjItem.Uidorden, UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), StrParametro: "S");
                    //App.MVTarifario.AgregarCodigoAOrdenTarifario(UidCodigo: Guid.NewGuid(), UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), uidorden: ObjItem.Uidorden);
                    App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendiente para elaborar", TipoDeSucursal: "S");
                    MyListviewOrdenesPorRealizar.ItemsSource = null;
                    MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
                    await Navigation.PopToRootAsync();
                }
                catch (Exception)
                {
                    await DisplayAlert("Sorry", "¿Compruebe su conexion a internet?", "Si", "No");
                }
            }
        }


        private async void ButtonCancelar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AppPuestoTacos.Popup.ConfirmarCancelacionOrden(ObjItem, MyListviewOrdenesPorRealizar));
        }
    }
}