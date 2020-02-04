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

namespace AppCliente.Pagos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagoEfectivo : ContentPage
    {
        decimal cantidad = 0;
        decimal subtotal = 0;
        decimal TotalEnvio = 0;
        decimal TotalPagar = 0;
        decimal TotalPropina = 0;
        public PagoEfectivo()
        {
            InitializeComponent();
            CargaDireccionAEntregar();
            for (int i = 0; i < App.MVProducto.ListaDelCarrito.Count; i++)
            {
                cantidad = cantidad + App.MVProducto.ListaDelCarrito[i].Cantidad;
                decimal a = decimal.Parse(App.MVProducto.ListaDelCarrito[i].StrCosto);
            }
            for (int i = 0; i < App.MVProducto.ListaDelInformacionSucursales.Count; i++)
            {
                TotalEnvio = TotalEnvio + App.MVProducto.ListaDelInformacionSucursales[i].CostoEnvio;
                TotalPagar = TotalPagar + App.MVProducto.ListaDelInformacionSucursales[i].Total;
                subtotal = subtotal + App.MVProducto.ListaDelInformacionSucursales[i].Subtotal;
                TotalPropina += App.MVProducto.ListaDelInformacionSucursales[i].DPropina;
            }

            txtTotalEnvio.Text = "$" + TotalEnvio.ToString();
            txtCantidad.Text = cantidad.ToString();
            txtsubtotal.Text = "$" + subtotal.ToString();
            txtCantidadSucursales.Text = App.MVProducto.ListaDelInformacionSucursales.Count.ToString();
            txtPropina.Text = "$" + TotalPropina;

            btnConfirmarPago.Text = "Confirmar pago por $" + TotalPagar;
        }

        private async void CargaDireccionAEntregar()
        {
            using (var _webApi = new HttpClient())
            {
                string url = "" + Helpers.Settings.sitio + "/api/Direccion/GetDireccionCompleta?UidDireccion=" + App.DireccionABuscar + "";
                var content = await _webApi.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content.ToString()).Data.ToString();
                var MDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
                string referencia = string.Empty;
                if (MDireccion.REFERENCIA != "No hay informacion")
                {
                    referencia = MDireccion.REFERENCIA;
                }
                lblDireccionAEntregar.Text = MDireccion.PAIS + "," + MDireccion.ESTADO + "," + MDireccion.MUNICIPIO + "," + MDireccion.CIUDAD + "," + MDireccion.COLONIA + "," + MDireccion.CALLE0 + " " + MDireccion.MANZANA + " " + MDireccion.LOTE + ", CP " + MDireccion.CodigoPostal + ". " + "Referencia: " + referencia;
            }
        }
    }
}