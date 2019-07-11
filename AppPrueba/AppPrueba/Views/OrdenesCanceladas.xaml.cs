using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdenesCanceladas : ContentPage
    {
        public OrdenesCanceladas()
        {
            InitializeComponent();
            //App.MVOrden.BuscarOrdenesAppSucursal("Sucursal", UidLicencia: new Guid((AppPrueba.Helpers.Settings.Licencia)), EstatusSucursal: "Canceladas", TipoDeSucursal: "S");
            //MyListviewOrdenesRecuperarOrden.ItemsSource = App.MVOrden.ListaDeOrdenesCanceladas;
            //Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            //{
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        MetodoConsulta();
            //    });
            //    return true;
            //});
        }


        public void MetodoConsulta()//object sender, EventArgs e
        {
        }

        private async void ButtonRecuperarOrden_Clicked(object sender, EventArgs e)
        {
            try
            {
                var item = sender as Button;
                var ObjItem = item.BindingContext as VMOrden;
                var action = await DisplayAlert("", "¿Desea recuperar la orden " + ObjItem.LNGFolio + "?", "Si", "No");
                if (action)
                {
                    App.MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("B40D954D-D408-4769-B110-608436C490F1"), "S", AppPrueba.Helpers.Settings.Licencia, LngFolio: ObjItem.LNGFolio);
                    App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid((AppPrueba.Helpers.Settings.Licencia)), EstatusSucursal: "Canceladas", TipoDeSucursal: "S");
                    MyListviewOrdenesRecuperarOrden.ItemsSource = null;
                    MyListviewOrdenesRecuperarOrden.ItemsSource = App.MVOrden.ListaDeOrdenesCanceladas;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}