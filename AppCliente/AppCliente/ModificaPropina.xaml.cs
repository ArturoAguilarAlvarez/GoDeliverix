using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class ModificaPropina : ContentPage
    {
        VMOrden MVORden = new VMOrden();
        ListView ListHistorialEmpresa = new ListView();
        Guid UidOrden = new Guid();
        public ModificaPropina()
        {
            MetodoConsulta();
        }
        public ModificaPropina(VMOrden MVOrden, ListView MyListViewHistorial, Guid UidOrden)
        {
            InitializeComponent();
            MVORden = MVOrden;
            this.UidOrden = UidOrden;
            ListHistorialEmpresa = MyListViewHistorial;
            txtPropinaActual.Text = MVOrden.MPropina.ToString("F2");
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            decimal mpropina = 0.0m;
            if (decimal.TryParse(txtPropinaActual.Text, out mpropina))
            {
                using (HttpClient _WebApi = new HttpClient())
                {
                    string url = "https://www.godeliverix.net/api/Tarifario/GetModificarPropina?UidOrdenSucursal=" + MVORden.UidRelacionOrdenSucursal + "&MPropina=" + mpropina + "";
                    MetodoConsulta();
                    await _WebApi.GetStringAsync(url);
                    Navigation.InsertPageBefore(new CodigoDeEntrega(MVORden.LngCodigoDeEntrega), this);
                    await Navigation.PopAsync();
                }
            }
            else
            {
                GenerateMessage("Numero invalido", "El formato de la propina no es correcto", "Ok");
            }
        }
        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }
        public void MetodoConsulta()
        {
            App.MVOrden.ListaDeOrdenesEmpresa.Clear();
            DataTable DatoQuery = App.MVOrden.ObtenerSucursaleDeOrden(UidOrden);
            string Status;
            foreach (DataRow itemm in DatoQuery.Rows)
            {
                Status = "";
                //App.MVOrden.ObtenerEstatusOrden();
                DataTable datoStatus = App.MVOrden.ObtenerEstatusOrden(itemm["UidRelacionOrdenSucursal"].ToString());
                //datoStatus.Rows[0][""].ToString();
                foreach (DataRow item2 in datoStatus.Rows)
                {
                    Status = item2["VchNombre"].ToString();
                }
                App.MVOrden.ListaDeOrdenesEmpresa.Add(new VMOrden()
                {
                    UidRelacionOrdenSucursal = itemm["UidRelacionOrdenSucursal"].ToString(),
                    Identificador = itemm["Identificador"].ToString(),
                    MTotal = decimal.Parse(itemm["MTotal"].ToString()) + decimal.Parse(itemm["MPropina"].ToString()),
                    LNGFolio = long.Parse(itemm["LNGFolio"].ToString()),
                    UidSucursal = new Guid(itemm["uidSucursal"].ToString()),
                    CostoEnvio = itemm["CostoEnvio"].ToString(),
                    StrNota = Status,
                    MPropina = decimal.Parse(itemm["MPropina"].ToString()),
                    MTotalSucursal = itemm["MTotalSucursal"].ToString() + decimal.Parse(itemm["MPropina"].ToString()),
                    Imagen = "http://godeliverix.net/Vista/" + itemm["NVchRuta"].ToString(),
                });
            }
            ListHistorialEmpresa.ItemsSource = App.MVOrden.ListaDeOrdenesEmpresa;
        }
    }
}