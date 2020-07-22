using AppCliente.ViewModel;
using AppCliente.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeleccionarSucursalPrecioProducto : ContentPage
    {


        public static List<VMProducto> ListaPreciosProcto = new List<VMProducto>();
        Button btn;
        Label lbEmpresa;
        Label lbCosto;
        Label uidEmpresaSeleccionada;
        Label sucursal;
        List<MvmSucursales> ListaSucursales = new List<MvmSucursales>();
        double cantidad;

        public SeleccionarSucursalPrecioProducto(List<VMProducto> lista, Button btn, Label lbEmpresa, Label lbCosto, double cantidad, Label uidEmpresaSeleccionada, Label sucursal)
        {
            InitializeComponent();
            cargaVentana(lista,btn,lbEmpresa,lbCosto,cantidad,uidEmpresaSeleccionada,sucursal);
        }

        protected async void cargaVentana(List<VMProducto> lista, Button btn, Label lbEmpresa, Label lbCosto, double cantidad, Label uidEmpresaSeleccionada, Label sucursal) 
        {
            this.sucursal = sucursal;
            int index = App.MVProducto.ListaDePreciosSucursales.FindIndex(t => t.StrIdentificador == lbEmpresa.Text);
            ListaSucursales = new List<MvmSucursales>();
            foreach (var item in App.MVProducto.ListaDePreciosSucursales)
            {
                ListaSucursales.Add(new MvmSucursales() { StrIdentificador = item.StrIdentificador,StrCosto = item.StrCosto,StrDireccion = item.StrDireccion, CSeleccion = Color.Transparent,UidSeccion = item.UidSeccion, UID  = item.UID,UidSucursal = item.UidSucursal });
            }
            var objsucursal = ListaSucursales.Find(t => t.StrIdentificador == lbEmpresa.Text);
            objsucursal.CSeleccion = Color.Red;
            MyListViewBusquedaEmpresaDelProducto.ItemsSource = ListaSucursales;
            ListaPreciosProcto = lista;
            this.btn = btn;
            this.lbEmpresa = lbEmpresa;
            this.lbCosto = lbCosto;
            this.cantidad = cantidad;
            this.uidEmpresaSeleccionada = uidEmpresaSeleccionada;
            HttpClient _api = new HttpClient();
            string _link = "" + Helpers.Settings.sitio + "/api/Imagen/GetImagenDePerfilEmpresa?UidEmpresa=" + lista[0].UIDEMPRESA + "";
            var conten = await _api.GetStringAsync(_link);
            var ob = JsonConvert.DeserializeObject<ResponseHelper>(conten).Data.ToString();
            var oimagen = JsonConvert.DeserializeObject<VMImagen>(ob);
            string ruta = Helpers.Settings.sitio + "/vista/" + oimagen.STRRUTA;
            ImagenEmpresa.Source = ruta;
        }

        private void MyListViewBusquedaEmpresaDelProducto_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                MvmSucursales a = (MvmSucursales)MyListViewBusquedaEmpresaDelProducto.SelectedItem;
                lbEmpresa.Text = a.StrIdentificador;
                string i = a.UidSeccion.ToString();
                lbCosto.Text = "$" + a.StrCosto;
                string sad = a.UID.ToString();
                double costo = double.Parse(a.StrCosto);
                uidEmpresaSeleccionada.Text = a.UID.ToString();
                sucursal.Text = a.UidSucursal.ToString();
                costo = (cantidad) * (costo);
                btn.Text = "Agregar carrito $" + costo;
                Navigation.PopAsync();
            }
            catch (Exception)
            {
            }
        }

        private void BtnCabiarEmpresa_clicked(object sender, EventArgs e)
        {
            try
            {
                VMProducto a = (VMProducto)MyListViewBusquedaEmpresaDelProducto.SelectedItem;

                lbEmpresa.Text = a.StrIdentificador;

                lbCosto.Text = a.StrCosto;
                string sad = a.UID.ToString();
                double costo = double.Parse(a.StrCosto);
                uidEmpresaSeleccionada.Text = a.UID.ToString();
                sucursal.Text = a.UidSucursal.ToString();
                costo = (cantidad) * (costo);

                btn.Text = "Agregar carrito $" + costo; ;


                Navigation.PopAsync();
            }
            catch (Exception)
            {
                DisplayAlert("Alerta", "Seleciona una sucursal", "ok");
            }

        }
    }
}