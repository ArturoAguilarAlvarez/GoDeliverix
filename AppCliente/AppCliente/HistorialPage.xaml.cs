using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using VistaDelModelo;
using System.Data;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistorialPage : ContentPage
    {
        public HistorialPage()
        {
            InitializeComponent();
            Cargar();
        }
        public void Cargar()
        {
            App.MVOrden.ObtenerOrdenesCliente(App.Global1.ToString(), "Usuario");
            for (int i = 0; i < App.MVOrden.ListaDeOrdenes.Count; i++)
            {
                int letras = App.MVOrden.ListaDeOrdenes[i].FechaDeOrden.Length;
                if (letras == 23)
                {
                    App.MVOrden.ListaDeOrdenes[i].StrNota = App.MVOrden.ListaDeOrdenes[i].FechaDeOrden.Substring(10, 13);
                    App.MVOrden.ListaDeOrdenes[i].FechaDeOrden = App.MVOrden.ListaDeOrdenes[i].FechaDeOrden.Substring(0, 9);
                }
                else
                {
                    App.MVOrden.ListaDeOrdenes[i].StrNota = App.MVOrden.ListaDeOrdenes[i].FechaDeOrden.Substring(11, 13);
                    App.MVOrden.ListaDeOrdenes[i].FechaDeOrden = App.MVOrden.ListaDeOrdenes[i].FechaDeOrden.Substring(0, 10);
                }
            }
            MyListViewHistorial.ItemsSource = App.MVOrden.ListaDeOrdenes;
        }

        private void MyListViewHistorial_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = ((ItemTappedEventArgs)e);
            VMOrden ObjItem = (VMOrden)item.Item;
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
                    MTotalSucursal = itemm["MTotalSucursal"].ToString(),
                    UidSucursal = new Guid(itemm["uidSucursal"].ToString()),
                    CostoEnvio = itemm["CostoEnvio"].ToString(),
                    LngCodigoDeEntrega = long.Parse(itemm["BintCodigoEntrega"].ToString()),
                    StrNota = Status,
                    MPropina = decimal.Parse(itemm["MPropina"].ToString()),
                    Imagen = "http://godeliverix.net/Vista/" + itemm["NVchRuta"].ToString(),
                });
            }

            Navigation.PushAsync(new HistorialDetalleEmpresa(ObjItem));
            //App.MVOrden.ObtenerProductosDeOrden(ObjItem.UidRelacionOrdenSucursal.ToString());
        }
    }
}