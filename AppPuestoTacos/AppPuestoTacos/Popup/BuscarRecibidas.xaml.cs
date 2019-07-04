using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace AppPuestoTacos.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuscarRecibidas : PopupPage
    {
        ListView ListViewBusqueda;
        Label BuscarAutomatico;

        public BuscarRecibidas (ListView listViewBusqueda,Label BuscarAutomatico)
		{
			InitializeComponent ();
            ListViewBusqueda = listViewBusqueda;
            this.BuscarAutomatico = BuscarAutomatico;

        }

        private async void ButtonCancelar_Clicked(object sender, EventArgs e)
        {

            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void ButtonBuscar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroDeOrden.Text))
            {
                BuscarAutomatico.Text = "false";
            }
            else
            {
                BuscarAutomatico.Text = "true";
            }
            try
            {
                App.MVOrden.BuscarOrdenes("Sucursal", NumeroOrden: txtNumeroDeOrden.Text+"%", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S");
                //App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S", NumeroOrden: txtOACNumeroDeOrden.Text);
                ListViewBusqueda.ItemsSource = null;
                ListViewBusqueda.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar;
            }
            catch (Exception)
            {

                await DisplayAlert("","Ingrese un numero de orden valido","ok");
            }

            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}