using AppPrueba.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            url = (RestService.Servidor + "api/Orden/GetOrdenesSucursal?Licencia=" + AppPrueba.Helpers.Settings.Licencia.ToString() + "&Estatus=Lista%20a%20enviar&tipoSucursal=s");
            string DatosObtenidos = await _client.GetStringAsync(url);
            var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
            App.MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(DatosGiros);
            MyListviewOrdenesPorEnviar.ItemsSource = App.MVOrden.ListaDeOrdenesPorEnviar;
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
                    await Navigation.PopAsync();
                    escaneado = result.Text;
                    BuscarOrdenCodigo();
                });
            };
            //await PopupNavigation.Instance.PushAsync(new AppPuestoTacos.Popup.Escaner());
        }

        private async void BuscarOrdenCodigo()
        {
            try
            {
                App.MVOrden.BuscarOrdenRepartidor(escaneado, AppPrueba.Helpers.Settings.Licencia);
                if (escaneado.Length == 36)
                {
                    if (App.MVOrden.UidEstatus != null)
                    {
                        if (App.MVOrden.UidEstatus.ToString() == "C412D367-7D05-45D8-AECA-B8FABBF129D9".ToLower())
                        {
                            await DisplayAlert("", "Orden lista", "ok");
                            await Navigation.PushAsync(new OrdenDescripcionEscaneado(MyListviewOrdenesPorEnviar));
                            //await PopupNavigation.Instance.PushAsync(new App.Popup.Escaner());
                            //txtNumeroOrdenScaner.Text = App.MVOrden.Uidorden.ToString();
                            //txtNumeroOrdenScaner.Text = "Orden: " + App.MVOrden.LNGFolio;
                            //txtNombreEmpresaScaner.Text = App.MVOrden.StrNombreSucursal;
                            //btnAsignarOrdenRepartidor.IsEnabled = true;
                        }
                        else if (App.MVOrden.UidEstatus.ToString() == "B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7".ToLower())
                        {
                            App.MVOrden.Uidorden = new Guid();
                            await DisplayAlert("", "La orden ya ha sido enviada", "ok");
                            //lblMensajeOrden.Content = "La orden ya ha sido enviada";
                        }
                        else
                        {
                            App.MVOrden.Uidorden = new Guid();
                            await DisplayAlert("", "La orden no esta lista para ser entregada al repartidor", "ok");
                            //lblMensajeOrden.Content = "La orden no esta lista para ser entregada al repartidor";
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
            catch (Exception)
            {
                App.MVOrden.Uidorden = new Guid();
                await DisplayAlert("", "Codigo invalido", "ok");
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