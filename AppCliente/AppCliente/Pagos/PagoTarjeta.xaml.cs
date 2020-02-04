using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using AppCliente.WebApi;
using AppCliente.ViewModel;
using VistaDelModelo;
namespace AppCliente.Pagos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagoTarjeta : ContentPage
    {
        decimal cantidad = 0;
        decimal subtotal = 0;
        decimal TotalEnvio = 0;
        decimal TotalPagar = 0;
        decimal TotalPropina = 0;
        bool OrdenVerificada = false;
        Guid UidOrden;
        Guid UidOrdenPago;
        string TipoDeFormaDePago = string.Empty;
        Timer tiempo = new Timer();
        //public PagoTarjeta(string formadepago,string FolioCliente, string FolioPagoTarjeta)
        public PagoTarjeta(string formadepago, string FolioCliente)
        {
            InitializeComponent();
            TipoDeFormaDePago = formadepago;
            for (int i = 0; i < App.MVProducto.ListaDelCarrito.Count; i++)
            {
                cantidad += App.MVProducto.ListaDelCarrito[i].Cantidad;
                decimal a = decimal.Parse(App.MVProducto.ListaDelCarrito[i].StrCosto);
            }
            for (int i = 0; i < App.MVProducto.ListaDelInformacionSucursales.Count; i++)
            {
                TotalEnvio += App.MVProducto.ListaDelInformacionSucursales[i].CostoEnvio;
                TotalPagar += App.MVProducto.ListaDelInformacionSucursales[i].Total;
                subtotal += App.MVProducto.ListaDelInformacionSucursales[i].Subtotal;
                TotalPropina += App.MVProducto.ListaDelInformacionSucursales[i].DPropina;
            }

            UidOrden = Guid.NewGuid();
            UidOrdenPago = Guid.NewGuid();

            App.MVCorreoElectronico.BuscarCorreos(UidPropietario: new Guid(App.Global1), strParametroDebusqueda: "Usuario");
            string ArchivoXml = "" +
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                "<P>\r\n  <business>\r\n" +
                "    <id_company>Z937</id_company>\r\n" +
                "    <id_branch>1050</id_branch>\r\n" +
                "    <user>Z937SDUS1</user>\r\n" +
                "    <pwd>09K1HT91B3</pwd>\r\n" +
                "  </business>\r\n" +
                "  <url>\r\n" +
                "    <reference>" + UidOrdenPago.ToString() + "</reference>\r\n" +
                "    <amount>" + TotalPagar + "</amount>\r\n" +
                "    <moneda>MXN</moneda>\r\n" +
                "    <canal>W</canal>\r\n" +
                "    <omitir_notif_default>1</omitir_notif_default>\r\n" +
                "    <st_correo>1</st_correo>\r\n" +
                "    <mail_cliente>" + App.MVCorreoElectronico.CORREO + "</mail_cliente>\r\n" +
                "    <datos_adicionales>\r\n" +
                "      <data id=\"1\" display=\"false\">\r\n" +
                "        <label>PRINCIPAL</label>\r\n" +
                "        <value>" + FolioCliente + "</value>\r\n" +
                "      </data>\r\n" +
                "      <data id=\"2\" display=\"true\">\r\n" +
                "        <label>Concepto:</label>\r\n" +
                "        <value>Orden en GoDeliverix.</value>\r\n" +
                "      </data>\r\n" +
                "      <data id=\"3\" display=\"false\">\r\n" +
                "        <label>Color</label>\r\n" +
                "        <value>Azul</value>\r\n" +
                "      </data>\r\n" +
                "    </datos_adicionales>\r\n" +
                "  </url>\r\n" +
                "</P>\r\n";
            string originalString = ArchivoXml;
            string key = "7AACFE849FABD796F6DCB947FD4D5268";
            AESCrypto o = new AESCrypto();
            string encryptedString = o.encrypt(originalString, key);
            string finalString = encryptedString.Replace("%", "%25").Replace(" ", "%20").Replace("+", "%2B").Replace("=", "%3D").Replace("/", "%2F");

            string encodedString = HttpUtility.UrlEncode("<pgs><data0>9265655113</data0><data>" + encryptedString + "</data></pgs>");
            string postParam = "xml=" + encodedString;

            var client = new RestClient("https://bc.mitec.com.mx/p/gen");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", postParam, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            //lblRespuesta.Text = o.decrypt(key, content);
            string decryptedString = o.decrypt(key, content);
            string str1 = decryptedString.Replace("<P_RESPONSE><cd_response>success</cd_response><nb_response></nb_response><nb_url>", "");
            string url = str1.Replace("</nb_url></P_RESPONSE>", "");

            WVWebPay.Source = new UrlWebViewSource { Url = url };

            tiempo.Interval = 2000;
            tiempo.Elapsed += new ElapsedEventHandler(VerificaPago);
            tiempo.Start();

        }


        protected void VerificaPago(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool respuesta = false;

                //if (!OrdenVerificada)
                //{
                using (var _webApi = new HttpClient())
                {
                    string url = "" + Helpers.Settings.sitio + "/api/Pagos/GetValidarPagoOrdenTarjeta?UidOrdenFormaDeCobro=" + UidOrdenPago.ToString() + "";
                    var datos = await _webApi.GetStringAsync(url);
                    string obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                    respuesta = bool.Parse(obj);
                }
                if (respuesta)
                {

                    using (var _webApi = new HttpClient())
                    {
                        string url = "" + Helpers.Settings.sitio + "/api/Pagos/GetObtenerPagoTarjeta?UidOrdenFormaDeCobro=" + UidOrdenPago.ToString() + "";
                        var datos = await _webApi.GetStringAsync(url);
                        var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                        App.oPago = JsonConvert.DeserializeObject<VMPagos>(obj);

                    }
                    if (App.oPago.StrEstatusPagosTarjeta == "denied")
                    {
                        UidOrdenPago = Guid.NewGuid();
                        //GenerateMessage("Alerta del sistema", "El pago ha sido denegado, intente de nuevo o consulte con su banco", "Aceptar");
                        //await Navigation.PopAsync();
                    }
                    if (App.oPago.StrEstatusPagosTarjeta == "approved")
                    {
                        OrdenVerificada = true;
                        MVPago.EnviarOrdenASucursales(TotalPagar, App.Global1, App.DireccionABuscar, TipoDeFormaDePago, UidOrden, UidOrdenPago);
                        await Navigation.PopToRootAsync();
                    }
                    if (App.oPago.StrEstatusPagosTarjeta == "error")
                    {
                        UidOrdenPago = Guid.NewGuid();
                        //await DisplayAlert("Alerta del sistema", "Ha ocurrio un error al efectuar el pago,intente de nuevo o consulte con su banco", "Aceptar");
                        //await Navigation.PopAsync();
                        //await Navigation.PopToRootAsync();
                    }
                }
                //}
            });

        }
        protected async static void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await App.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }
        protected override void OnDisappearing()
        {
            tiempo.Stop();
            base.OnDisappearing();
        }

        private void WVWebPay_Navigating(object sender, WebNavigatingEventArgs e)
        {
            labelLoading.IsVisible = true;
        }

        private void WVWebPay_Navigated(object sender, WebNavigatedEventArgs e)
        {
            labelLoading.IsVisible = false;
        }
    }
}