using AppPuestoTacos.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPuestoTacos.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrdenDescripcionPorElaborar : ContentPage
	{
        VMOrden ObjItem;
        ListView MyListviewOrdenesPorRealizar;
        HttpClient _client = new HttpClient();
        string url = "";

        public OrdenDescripcionPorElaborar (VMOrden ObjItem,ListView MyListviewOrdenesPorRealizar)
		{
			InitializeComponent ();
            this.ObjItem = ObjItem;
            Cargar();
            
            this.MyListviewOrdenesPorRealizar = MyListviewOrdenesPorRealizar;
        }

        public async void Cargar()
        {
            string _URL = (RestService.Servidor + "api/Orden/GetObtenerProductosDeOrden?UidOrden=" + ObjItem.Uidorden.ToString());
            string DatosObtenidos = await _client.GetStringAsync(_URL);
            var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();

            JArray blogPostArray = JArray.Parse(DatosGiros.ToString());

            App.MVOrden.ListaDeProductos = blogPostArray.Select(p => new VMOrden
            {
                UidProducto = (Guid)p["UidProducto"],
                StrNombreSucursal = (string)p["Identificador"],
                StrNombreProducto = (string)p["StrNombreProducto"],
                Imagen = (string)p["Imagen"],
                intCantidad = (int)p["intCantidad"],
                UidSucursal = (Guid)p["UidSucursal"],
                MTotal = (int)p["MTotal"],
                MCostoTarifario = (int)p["MCostoTarifario"],
                VisibilidadNota = (string)p["StrNota"]
            }).ToList();

            //App.MVOrden.ObtenerProductosDeOrden(ObjItem.Uidorden.ToString());
            MyListviewOrdenesEnPreparacion.ItemsSource = App.MVOrden.ListaDeProductos;
        }
        private async void ImageButtonFinalizar_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("Finalizar?", "¿A concluido el proceso de elaboracion de la orden "+ ObjItem.LNGFolio+"?", "Si", "No");
            if (action)
            {
                try
                {
                    App.MVOrden.AgregaEstatusALaOrden(new Guid("c412d367-7d05-45d8-aeca-b8fabbf129d9"), UidOrden: ObjItem.Uidorden, UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), StrParametro: "S");
                    //App.MVTarifario.AgregarCodigoAOrdenTarifario(UidCodigo: Guid.NewGuid(), UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), uidorden: ObjItem.Uidorden);
                    App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendiente para elaborar", TipoDeSucursal: "S");
                    MyListviewOrdenesPorRealizar.ItemsSource = null;
                    MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
                    await Navigation.PopToRootAsync();
                }
                catch (Exception)
                {
                    await DisplayAlert("Sorry", "¿Compruebe su conexion a internet?", "Si", "No");
                }
            }
        }


        private async void ButtonCancelar_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AppPuestoTacos.Popup.ConfirmarCancelacionOrden(ObjItem, MyListviewOrdenesPorRealizar));
        }
    }
}