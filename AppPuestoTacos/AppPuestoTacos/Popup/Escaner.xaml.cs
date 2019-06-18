using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ZXing.Net.Mobile.Forms;

namespace AppPuestoTacos.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Escaner : PopupPage
    {
		public Escaner ()
		{
			InitializeComponent ();
            //txtNumeroOrdenScaner.Text = "Orden: " + App.MVOrden.Uidorden.ToString();
            txtNumeroOrdenScaner.Text = "Orden: " + App.MVOrden.LNGFolio;
            txtNombreEmpresaScaner.Text = "Empresa distribuidora: " + App.MVOrden.StrNombreSucursal;
        }

        private void BtnAsignarOrdenRepartidor_Clicked(object sender, EventArgs e)
        {
            App.MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("0E08DE81-DED2-41BE-93A5-A3742C3C411F"), "S",AppPuestoTacos.Helpers.Settings.Licencia, UidOrden: App.MVOrden.Uidorden);
            App.MVOrden.AgregaEstatusALaOrden(new Guid("B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7"), UidOrden: App.MVOrden.Uidorden, UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), StrParametro: "S");
            App.MVOrden.Uidorden = Guid.Empty;
            //App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Lista a enviar", TipoDeSucursal: "S");
            //App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Lista a enviar", TipoDeSucursal: "S");
            PopupNavigation.Instance.PopAllAsync();
        }

        private void BtnDetalleCompra_Clicked(object sender, EventArgs e)
        {

        }
    }
}