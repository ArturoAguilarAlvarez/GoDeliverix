using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppPuestoTacos;
using VistaDelModelo;
using System.Net.Http;
using AppPuestoTacos.WebApi;
using Newtonsoft.Json;

namespace AppPuestoTacos.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmarCancelacionOrden : PopupPage
    {
        VMOrden ObjItem;
        ListView MyListviewOrdenesRecibidas;

        HttpClient _client = new HttpClient();
        string _url = "";
        public ConfirmarCancelacionOrden(VMOrden ObjItem, ListView MyListviewOrdenesRecibidas)
        {
            InitializeComponent();
            this.ObjItem = ObjItem;
            //App.MVMensaje.Buscar(strLicencia: AppPuestoTacos.Helpers.Settings.Licencia);
            Cargar();
            this.MyListviewOrdenesRecibidas = MyListviewOrdenesRecibidas;
        }

        public async void Cargar()
        { 
            string _url = ("http://godeliverix.net/api/Empresa/GetMensajeSucursal?Licencia=" + AppPuestoTacos.Helpers.Settings.Licencia.ToString());
            var DatosObtenidos = await _client.GetAsync(_url);
            string res = await DatosObtenidos.Content.ReadAsStringAsync();
            var asd = JsonConvert.DeserializeObject<ResponseHelper>(res).Data.ToString();
            App.MVMensaje = JsonConvert.DeserializeObject<VistaDelModelo.VMMensaje>(asd);
            PickeMensaje.ItemsSource = App.MVMensaje.ListaDeMensajes;
        }
        private async void ButtonAceptarAccion_Clicked(object sender, EventArgs e)
        {
            CancelarOrden();
            await PopupNavigation.Instance.PopAllAsync();
            await Navigation.PopToRootAsync();
        }

        private void ButtonCancelarAccion_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        public async void CancelarOrden()
        {
            VMMensaje ObjSeccion = PickeMensaje.SelectedItem as VMMensaje;
            if (ObjSeccion==null)
            {
                
               await DisplayAlert("", "Debe seleccionar un mensaje para poder cancelar una orden", "ok");
            }
            else
            {
                try
                {
                    //App.MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EAE7A7E6-3F19-405E-87A9-3162D36CE21B"), "S", AppPuestoTacos.Helpers.Settings.Licencia, LngFolio: ObjItem.LNGFolio, UidMensaje: ObjSeccion.Uid);
                     _url = (RestService.Servidor + "api/Orden/GetCancelarOrden?Licencia=" + AppPuestoTacos.Helpers.Settings.Licencia +
                        "&LNGFolio="+ ObjItem.LNGFolio.ToString()
                        + "&IdMensaje="+ ObjSeccion.Uid);
                    var DatosObtenidos = await _client.GetAsync(_url);

                    // App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S");
                    // App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendiente para elaborar", TipoDeSucursal: "S");

                    string _URL = (RestService.Servidor + "api/Orden/GetOrdenesSucursal?Licencia=" + AppPuestoTacos.Helpers.Settings.Licencia +
                        "&Estatus=Pendientes%20a%20confirmar&tipoSucursal=s");
                    var DatosObtenidos2 = await _client.GetAsync(_URL);
                    string res = await DatosObtenidos2.Content.ReadAsStringAsync();
                    var asd = JsonConvert.DeserializeObject<ResponseHelper>(res).Data.ToString();
                    App.MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(asd);

                    MyListviewOrdenesRecibidas.ItemsSource = null;
                    MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar;
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "Sin acceso a internet", "ok");

                }

              
            }
        }
    }
}