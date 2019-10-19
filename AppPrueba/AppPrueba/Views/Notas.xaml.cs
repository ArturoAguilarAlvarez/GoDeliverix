
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

namespace AppPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notas : ContentPage
    {
        public Notas(Guid UidProductoEnOrden)
        {
            InitializeComponent();
            CargNota(UidProductoEnOrden);
        }

        private async void CargNota(Guid uidProductoEnOrden)
        {
            try
            {
                HttpClient _client = new HttpClient();
                string _URL = (RestService.Servidor + "api/Orden/GetObtenerNotaDeProducto?uidProductoEnOrden=" + uidProductoEnOrden + "");
                var DatosObtenidos = await _client.GetAsync(_URL);
                string res = await DatosObtenidos.Content.ReadAsStringAsync();
                var asd = JsonConvert.DeserializeObject<ResponseHelper>(res).Data.ToString();
                VistaDelModelo.VMOrden MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(asd);
                lblNota.Text = MVOrden.StrNota;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}