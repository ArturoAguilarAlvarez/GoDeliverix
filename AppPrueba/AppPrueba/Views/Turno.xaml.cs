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
    public partial class Turno : ContentPage
    {
        public Turno()
        {
            InitializeComponent();
            carga();
        }

        private async void carga()
        {
            string UidLicencia = AppPrueba.Helpers.Settings.Licencia;
            using (HttpClient _WebApi = new HttpClient())
            {
                string url = RestService.Servidor + "api/Turno/GetUltimoTurnoSuministradora?UidLicencia=" + UidLicencia + "";
                var datos = await _WebApi.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                var MVTurno = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                lblHoraInicio.Text = MVTurno.DtmHoraInicio.ToString();
                lblFolio.Text = MVTurno.LngFolio.ToString();
            }
        }
    }
}