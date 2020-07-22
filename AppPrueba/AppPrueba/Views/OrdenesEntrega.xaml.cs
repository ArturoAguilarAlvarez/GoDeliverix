using AppPrueba.WebApi;
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
using ZXing.Net.Mobile.Forms;

namespace AppPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdenesEntrega : ContentPage
    {
        bool Ordenamiento = false;
        string escaneado;
        HttpClient _client = new HttpClient();
        string url = "";
        public OrdenesEntrega()
        {
            try
            {
                InitializeComponent();
                Cargar();
            }
            catch (Exception)
            {

                DisplayAlert("Sorry", "Sin acceso a internet", "Ok");
            }
        }

        public async void Cargar()
        {
            url = (RestService.Servidor + "api/Orden/GetOrdenesSucursal?Licencia=" + AppPrueba.Helpers.Settings.Licencia.ToString() + "&Estatus=Listaaenviar&tipoSucursal=s");
            string DatosObtenidos = await _client.GetStringAsync(url);
            var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
            App.MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(DatosGiros);
            foreach (var item in App.MVOrden.ListaDeOrdenes)
            {
                item.Imagen = item.Imagen.Replace("Imagenes/", string.Empty).Replace(".png", string.Empty);
            }
            MyListviewOrdenesPorEnviar.ItemsSource = App.MVOrden.ListaDeOrdenes;
        }

        private void ImageButtonEscanear_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread
                (async () =>
                {
                    escaneado = result.Text;
                    BuscarOrdenCodigo();
                    await Navigation.PopAsync();
                });
            };
        }
        private async void BuscarOrdenCodigo()
        {
            try
            {
                url = RestService.Servidor + "api/Orden/GetBuscarOrdenRepartidor?UidCodigo=" + escaneado + "&UidLicencia=" + AppPrueba.Helpers.Settings.Licencia + "";
                string content = await _client.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVOrden = new VMOrden();
                App.MVOrden = JsonConvert.DeserializeObject<VMOrden>(obj);

                if (escaneado.Length == 36)
                {
                    if (App.MVOrden.StrEstatusOrdenSucursal != null)
                    {
                        if (App.MVOrden.StrEstatusOrdenSucursal.ToString() == "C412D367-7D05-45D8-AECA-B8FABBF129D9".ToLower())
                        {
                            await DisplayAlert("", "Orden lista", "ok");
                            await Navigation.PushAsync(new OrdenDescripcionEscaneado(MyListviewOrdenesPorEnviar));
                        }
                        else if (App.MVOrden.UidEstatus.ToString() == "B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7".ToLower())
                        {
                            App.MVOrden.Uidorden = new Guid();
                            await DisplayAlert("", "La orden ya ha sido enviada", "ok");
                        }
                        else
                        {
                            App.MVOrden.Uidorden = new Guid();
                            await DisplayAlert("", "La orden no esta lista para ser entregada al repartidor", "ok");
                        }
                    }
                    else
                    {
                        App.MVOrden.Uidorden = new Guid();
                        await DisplayAlert("", "No hay coincidencia con el codigo", "ok");
                    }
                }
                else
                {
                    App.MVOrden.Uidorden = new Guid();
                    await DisplayAlert("", "Codigo invalido", "ok");
                }
            }
            catch (Exception e)
            {
                App.MVOrden.Uidorden = new Guid();
                await DisplayAlert("", "Codigo invalido: " + e.Message.ToString(), "ok");
            }
        }

        private void ButtonOrdenarFecha_Clicked(object sender, EventArgs e)
        {
            if (Ordenamiento)
            {
                App.MVOrden.ListaDeOrdenesPorEnviar = App.MVOrden.ListaDeOrdenesPorEnviar.OrderBy(x => x.FechaDeOrden).ToList();
                MyListviewOrdenesPorEnviar.ItemsSource = null;
                MyListviewOrdenesPorEnviar.ItemsSource = App.MVOrden.ListaDeOrdenesPorEnviar;
                Ordenamiento = false;
            }
            else
            {
                App.MVOrden.ListaDeOrdenesPorEnviar = App.MVOrden.ListaDeOrdenesPorEnviar.OrderByDescending(x => x.FechaDeOrden).ToList();
                MyListviewOrdenesPorEnviar.ItemsSource = null;
                MyListviewOrdenesPorEnviar.ItemsSource = App.MVOrden.ListaDeOrdenesPorEnviar;
                Ordenamiento = true;
            }
        }

        private void ButtonOrdenarPorOrden_Clicked(object sender, EventArgs e)
        {
            if (Ordenamiento)
            {
                App.MVOrden.ListaDeOrdenesPorEnviar = App.MVOrden.ListaDeOrdenesPorEnviar.OrderBy(x => x.LNGFolio).ToList();
                MyListviewOrdenesPorEnviar.ItemsSource = null;
                MyListviewOrdenesPorEnviar.ItemsSource = App.MVOrden.ListaDeOrdenesPorEnviar;
                Ordenamiento = false;
            }
            else
            {
                App.MVOrden.ListaDeOrdenesPorEnviar = App.MVOrden.ListaDeOrdenesPorEnviar.OrderByDescending(x => x.LNGFolio).ToList();
                MyListviewOrdenesPorEnviar.ItemsSource = null;
                MyListviewOrdenesPorEnviar.ItemsSource = App.MVOrden.ListaDeOrdenesPorEnviar;
                Ordenamiento = true;
            }
        }
    }
}