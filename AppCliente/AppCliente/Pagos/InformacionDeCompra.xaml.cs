﻿using AppCliente.ViewModel;
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
    public partial class InformacionDeCompra : ContentPage
    {
        VMMonedero MVMonedero;
        decimal cantidad = 0;
        decimal subtotal = 0;
        decimal TotalEnvio = 0;
        decimal TotalPagar = 0;
        decimal TotalPropina = 0;
        string TipoDeFormaDePago = "";
        public InformacionDeCompra(string FormaDePago)
        {
            InitializeComponent();
            TipoDeFormaDePago = FormaDePago;
            CargaDireccionAEntregar();
            for (int i = 0; i < App.MVProducto.ListaDelCarrito.Count; i++)
            {
                cantidad = cantidad + App.MVProducto.ListaDelCarrito[i].Cantidad;
                decimal a = decimal.Parse(App.MVProducto.ListaDelCarrito[i].StrCosto);
            }
            for (int i = 0; i < App.MVProducto.ListaDelInformacionSucursales.Count; i++)
            {
                TotalEnvio = TotalEnvio + App.MVProducto.ListaDelInformacionSucursales[i].CostoEnvio;
                TotalPropina += App.MVProducto.ListaDelInformacionSucursales[i].DPropina;
                TotalPagar = TotalPagar + App.MVProducto.ListaDelInformacionSucursales[i].Total + TotalPropina;
                subtotal = subtotal + App.MVProducto.ListaDelInformacionSucursales[i].Subtotal;
            }

            txtTotalEnvio.Text = "$" + TotalEnvio.ToString("N2");
            txtCantidad.Text = cantidad.ToString();
            txtsubtotal.Text = "$" + subtotal.ToString("N2");
            txtTotalPropina.Text = "$" + TotalPropina.ToString("N2");
            txtCantidadSucursales.Text = App.MVProducto.ListaDelInformacionSucursales.Count.ToString();
            txtPropina.Text = "$" + TotalPropina.ToString("N2");
            txtTotal.Text = "$" + TotalPagar.ToString("N2");

            InformacionMonedero.IsVisible = false;
            switch (TipoDeFormaDePago)
            {
                //Efectivo
                case "6518C044-CE40-41F4-9344-92F0C200A8C2":
                    btnConfirmarPago.Text = "Confirmar pago por $" + TotalPagar.ToString("N2");
                    break;
                //Tarjeta
                case "30545834-7FFE-4D1A-AA94-D6E569371C60":
                    btnConfirmarPago.Text = "Continuar el pago por $" + TotalPagar.ToString("N2");
                    break;
                //Monedero
                case "13DC10FE-FE47-48D6-A427-DD2F6DE0C564":
                    InformacionMonedero.IsVisible = true;
                    cargaMonedero();
                    btnConfirmarPago.Text = "Confirmar pago por $" + TotalPagar.ToString("N2");
                    break;
                default:
                    break;
            }
        }
        private async void cargaMonedero()
        {
            string _Url = $"" + Helpers.Settings.sitio + "/api/Monedero/Get?" +
                                $"id={App.Global1}";
            var content = "";
            using (HttpClient _client = new HttpClient())
            {
                content = await _client.GetStringAsync(_Url);
            }
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            MVMonedero = JsonConvert.DeserializeObject<VMMonedero>(obj);
            lblCantidadEnMonedero.Text = "Saldo en monedero $ " + MVMonedero.MMonto.ToString("N2") + "";
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

        private async void btnConfirmarPago_Clicked(object sender, EventArgs e)
        {
            string _Url = string.Empty;
            var content = "";
            switch (TipoDeFormaDePago)
            {
                //Efectivo
                case "6518C044-CE40-41F4-9344-92F0C200A8C2":
                    await DisplayAlert("Mensaje", "Su orden ha sido enviada", "Aceptar");

                    MVPago.EnviarOrdenASucursales(TotalPagar, App.Global1, App.DireccionABuscar, TipoDeFormaDePago, Guid.NewGuid(), Guid.NewGuid());
                    await Navigation.PopToRootAsync();
                    break;
                //Tarjeta
                case "30545834-7FFE-4D1A-AA94-D6E569371C60":

                    _Url = $"" + Helpers.Settings.sitio + "/api/Usuario/GetObtenerFolioCliente?" +
                                 $"UidUsuario={App.Global1}" + "";
                    var FolioCliente = "";
                    using (HttpClient _client = new HttpClient())
                    {
                        content = await _client.GetStringAsync(_Url);
                        FolioCliente = JsonConvert.DeserializeObject<ResponseHelper>(content.ToString()).Data.ToString();
                    }
                    //_Url = string.Empty;
                    //content = string.Empty;
                    //_Url = $"http://godeliverix.net/api/Pagos/GetObtenerFolioPagoConTarjeta";
                    //VMPagos opago = new VMPagos();
                    //using (HttpClient _client = new HttpClient())
                    //{
                    //    content = await _client.GetStringAsync(_Url);
                    //    var obj = JsonConvert.DeserializeObject<ResponseHelper>(content.ToString()).Data.ToString();
                    //    opago = JsonConvert.DeserializeObject<VMPagos>(obj);
                    //}

                    //await Navigation.PushAsync(new PagoTarjeta(TipoDeFormaDePago, FolioCliente,opago.StrFolioPagoTarjeta));
                    await Navigation.PushAsync(new PagoTarjeta(TipoDeFormaDePago, FolioCliente));
                    //await Navigation.PopAsync();
                    break;
                //Monedero
                case "13DC10FE-FE47-48D6-A427-DD2F6DE0C564":

                    if (TotalPagar > MVMonedero.MMonto)
                    {
                        await DisplayAlert("Mensaje", "No cuentas con dinero suficiente para realizar el pago", "Aceptar");
                    }
                    else
                    {
                        MVPago.EnviarOrdenASucursales(TotalPagar, App.Global1, App.DireccionABuscar, TipoDeFormaDePago, Guid.NewGuid(), Guid.NewGuid());

                        _Url = $"" + Helpers.Settings.sitio + "/api/Monedero/GetMovimientoMonedero?" +
                               $"UidUsuario={App.Global1}" + $"&TipoDeMovimiento=6C7F4C2E-0D27-4200-9485-7BE331066D33" +
                               $"&Concepto=DCA75F23-5DDC-4EA5-B088-6D5B187F76F4" +
                               $"&DireccionMovimiento=" + App.DireccionABuscar + "" +
                               $"&Monto=" + TotalPagar + "";
                        content = "";
                        using (HttpClient _client = new HttpClient())
                        {
                            content = await _client.GetStringAsync(_Url);
                        }
                    }
                    await Navigation.PopToRootAsync();
                    break;
                default:
                    break;
            }
        }
    }
}