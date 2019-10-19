using Plugin.Clipboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistorialDetalleEmpresa : ContentPage
    {
        VMOrden ObjItem;

        public HistorialDetalleEmpresa(VMOrden ObjItem)
        {
            InitializeComponent();
            MyListViewHistorial.ItemsSource = null;
            MyListViewHistorial.ItemsSource = App.MVOrden.ListaDeOrdenesEmpresa;
            this.ObjItem = ObjItem;
            Title = "Orden " + ObjItem.LNGFolio;
        }

        public void MetodoConsulta()
        {
            App.MVOrden.ListaDeOrdenesEmpresa.Clear();
            DataTable DatoQuery = App.MVOrden.ObtenerSucursaleDeOrden(ObjItem.Uidorden);
            string Status;
            foreach (DataRow itemm in DatoQuery.Rows)
            {
                Status = "";
                //App.MVOrden.ObtenerEstatusOrden();
                DataTable datoStatus = App.MVOrden.ObtenerEstatusOrden(itemm["UidRelacionOrdenSucursal"].ToString());
                //datoStatus.Rows[0][""].ToString();
                foreach (DataRow item2 in datoStatus.Rows)
                {
                    Status = item2["VchNombre"].ToString();
                }
                App.MVOrden.ListaDeOrdenesEmpresa.Add(new VMOrden()
                {
                    UidRelacionOrdenSucursal = itemm["UidRelacionOrdenSucursal"].ToString(),
                    Identificador = itemm["Identificador"].ToString(),
                    MTotal = decimal.Parse(itemm["MTotal"].ToString()) + decimal.Parse(itemm["MPropina"].ToString()),
                    LNGFolio = long.Parse(itemm["LNGFolio"].ToString()),
                    UidSucursal = new Guid(itemm["uidSucursal"].ToString()),
                    CostoEnvio = itemm["CostoEnvio"].ToString(),
                    StrNota = Status,
                    MPropina = decimal.Parse(itemm["MPropina"].ToString()),
                    MTotalSucursal = itemm["MTotalSucursal"].ToString(),
                    Imagen = "http://godeliverix.net/Vista/" + itemm["NVchRuta"].ToString(),
                });
            }

            MyListViewHistorial.ItemsSource = null;
            MyListViewHistorial.ItemsSource = App.MVOrden.ListaDeOrdenesEmpresa;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var ObjItem = item.BindingContext as VMOrden;

            App.MVOrden.ObtenerProductosDeOrden(ObjItem.UidRelacionOrdenSucursal.ToString());
            Navigation.PushAsync(new HistorialEmpresaProductos(ObjItem.LNGFolio));

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistorialOrdenEmpresaSeguimiento());
        }

        private void MyListViewHistorial_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = ((ItemTappedEventArgs)e);
            VMOrden ObjItem = (VMOrden)item.Item;
            App.MVOrden.ObtenerProductosDeOrden(ObjItem.UidRelacionOrdenSucursal.ToString());

            if (ObjItem.StrNota == "Enviando")
            {
                Navigation.PushAsync(new HistorialEmpresaProductosMapa());
            }
            else
            {
                Navigation.PushAsync(new HistorialEmpresaProductos(ObjItem.LNGFolio));
            }

        }

        private async void ButtonCopiarNumeroEnvio_Clicked_2(object sender, EventArgs e)
        {
            var item = sender as Button;
            var ObjItem = item.BindingContext as VMOrden;
            var action = await DisplayAlert("", "Confirmar " + ObjItem.MPropina.ToString("N2") + " propina?", "Si", "No");
            if (action)
            {
                await Navigation.PushAsync(new CodigoDeEntrega(ObjItem.LngCodigoDeEntrega));
            }
            else if (!action)
            {
                await Navigation.PushAsync(new ModificaPropina(ObjItem, MyListViewHistorial, this.ObjItem.Uidorden));
            }
        }

        private void MyListViewHistorial_Refreshing(object sender, EventArgs e)
        {
            MetodoConsulta();
            MyListViewHistorial.IsRefreshing = false;
        }
    }
}