using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppPuestoTacos;
using VistaDelModelo;

namespace AppPuestoTacos.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmarCancelacionOrden : PopupPage
    {
        VMOrden ObjItem;
        ListView MyListviewOrdenesRecibidas;
        public ConfirmarCancelacionOrden(VMOrden ObjItem, ListView MyListviewOrdenesRecibidas)
		{         
            InitializeComponent ();
            this.ObjItem = ObjItem;
            App.MVMensaje.Buscar(strLicencia: AppPuestoTacos.Helpers.Settings.Licencia);
            PickeMensaje.ItemsSource = App.MVMensaje.ListaDeMensajes;
            this.MyListviewOrdenesRecibidas = MyListviewOrdenesRecibidas;
        }

        private async void ButtonAceptarAccion_Clicked(object sender, EventArgs e)
        {
            CancelarOrden();
            await PopupNavigation.Instance.PopAllAsync();
            await Navigation.PopToRootAsync();
        }

        private void ButtonCancelarAccion_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        public void CancelarOrden()
        {
            VMMensaje ObjSeccion = PickeMensaje.SelectedItem as VMMensaje;
            if (ObjSeccion==null)
            {
                
                DisplayAlert("", "Debe seleccionar un mensaje para poder cancelar una orden", "ok");
            }
            else
            {
                try
                {
                    App.MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EAE7A7E6-3F19-405E-87A9-3162D36CE21B"), "S", AppPuestoTacos.Helpers.Settings.Licencia, LngFolio: ObjItem.LNGFolio, UidMensaje: ObjSeccion.Uid);
                    App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S");
                    App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendiente para elaborar", TipoDeSucursal: "S");
                    MyListviewOrdenesRecibidas.ItemsSource = null;
                    MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar;
                }
                catch (Exception)
                {
                    DisplayAlert("Error", "Sin acceso a internet", "ok");

                }

              
            }
        }
    }
}