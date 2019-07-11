using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdenDescripcionEscaneado : ContentPage
    {
        Guid Uidorden;
        ListView MyListviewOrdenesPorEnviar;
        public OrdenDescripcionEscaneado(ListView MyListviewOrdenesPorEnviar)
        {
            InitializeComponent();
            App.MVOrden.ObtenerProductosDeOrden(App.MVOrden.Uidorden.ToString());
            MyListviewOrdenes.ItemsSource = App.MVOrden.ListaDeProductos;
            txtNumeroOrdenScaner.Text = App.MVOrden.LNGFolio.ToString();
            txtNombreEmpresaScaner.Text = App.MVOrden.StrNombreSucursal;
            Uidorden = App.MVOrden.Uidorden;
            App.MVOrden.Uidorden = new Guid();
            this.MyListviewOrdenesPorEnviar = MyListviewOrdenesPorEnviar;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("", "¿Entregar la orden " + App.MVOrden.LNGFolio.ToString() + "?", "Si", "No");
            if (action)
            {

                App.MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("0E08DE81-DED2-41BE-93A5-A3742C3C411F"), "S", AppPrueba.Helpers.Settings.Licencia, UidOrden: Uidorden);
                App.MVOrden.AgregaEstatusALaOrden(new Guid("B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7"), UidOrden: Uidorden, UidLicencia: new Guid(AppPrueba.Helpers.Settings.Licencia), StrParametro: "S");
                App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPrueba.Helpers.Settings.Licencia), EstatusSucursal: "Lista a enviar", TipoDeSucursal: "S");
                MyListviewOrdenesPorEnviar.ItemsSource = null;
                MyListviewOrdenesPorEnviar.ItemsSource = App.MVOrden.ListaDeOrdenesPorEnviar;
                await Navigation.PopAsync();
            }

        }
    }
}