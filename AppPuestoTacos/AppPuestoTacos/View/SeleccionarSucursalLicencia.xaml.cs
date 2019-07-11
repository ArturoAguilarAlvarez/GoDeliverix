using AppPuestoTacos.WebApi;
using Newtonsoft.Json;
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
	public partial class SeleccionarSucursalLicencia : ContentPage
	{
        string perfil;
        string url;
        HttpClient _client = new HttpClient();
        public SeleccionarSucursalLicencia (string perfil)
		{
			InitializeComponent ();
            Cargar(perfil);



        }

        private async void PickerSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                VMSucursales ovjSeccion = PickerSucursales.SelectedItem as VMSucursales;
                //App.MVLicencia.ObtenerLicenciaSucursal(ovjSeccion.ID.ToString());           
                url = RestService.Servidor + "api/Empresa/GetLicenciasEmpresa/"+ ovjSeccion.ID.ToString();
                string _strMVLicencia = await _client.GetStringAsync(url);
                string strLicencia = JsonConvert.DeserializeObject<ResponseHelper>(_strMVLicencia).Data.ToString();
                App.MVLicencia = JsonConvert.DeserializeObject<VistaDelModelo.VMLicencia>(strLicencia);

                PickerLicencia.ItemsSource = null;
                int a = App.MVLicencia.ListaDeLicencias.Count;
                for (int i = 0; i < a; i++)
                {
                    if (App.MVLicencia.ListaDeLicencias[i].UidEstatus == 0)
                    {
                        App.MVLicencia.ListaDeLicencias.Remove(App.MVLicencia.ListaDeLicencias[i]);
                        a = a - 1;
                        i = i - 1;
                    }
                    if (App.MVLicencia.ListaDeLicencias[i].BLUso == true)
                    {
                        App.MVLicencia.ListaDeLicencias.Remove(App.MVLicencia.ListaDeLicencias[i]);
                        a = a - 1;
                        i = i - 1;
                    }
                }
                App.MVLicencia.ListaDeLicencias.RemoveAt(App.MVLicencia.ListaDeLicencias.Count - 1);
                PickerLicencia.ItemsSource = App.MVLicencia.ListaDeLicencias;
            }
            catch (Exception)
            {
            }
        }

        private async void ButtonGuardarLicencia_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(AppPuestoTacos.Helpers.Settings.Licencia))
                {
                    App.MVLicencia.ActualizarLicenciaSucursal(UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), bdisponibilidad: false);
                    AppPuestoTacos.Helpers.Settings.ClearAllData();
                }
                VMSucursales objSucursal = PickerSucursales.SelectedItem as VMSucursales;
                VMLicencia objLicencia = PickerLicencia.SelectedItem as VMLicencia;
                //App.MVLicencia.CambiaDisponibilidadDeLicencia(objLicencia.UidLicencia.ToString());
                url = RestService.Servidor + "api/Licencia/GetCambiaDisponibilidadDeLicencia?Licencia=" + objLicencia.UidLicencia.ToString();
                string GuidEmpresa = await _client.GetStringAsync(url);
                AppPuestoTacos.Helpers.Settings.NombreSucursal = objSucursal.IDENTIFICADOR;
                AppPuestoTacos.Helpers.Settings.Licencia = objLicencia.UidLicencia.ToString();
                AppPuestoTacos.Helpers.Settings.UidSucursal = objSucursal.ID.ToString();
                AppPuestoTacos.Helpers.Settings.NombreSucursal = App.MVSucursal.LISTADESUCURSALES[0].IDENTIFICADOR;
                AppPuestoTacos.Helpers.Settings.Perfil = perfil;
                App.Current.MainPage = new MasterMenu();
                //App.MVEmpresas.ObtenerNombreComercial(App.UIdUsuario.ToString());
                //App.NombreEmpresa = App.MVEmpresas.NOMBRECOMERCIAL;
            }
            catch (Exception)
            {


            }

        }

        public async void Cargar(string perfil)
        {
            url = RestService.Servidor + "api/Empresa/GetIdEmpresa?Uidusuario=" + App.UIdUsuario.ToString();
            string GuidEmpresa = await _client.GetStringAsync(url);
            string UidEmpresa = JsonConvert.DeserializeObject<ResponseHelper>(GuidEmpresa).Data.ToString();

            url = RestService.Servidor + "api/Empresa/GetListaSucursalesDeEmpresa?UidEmpresa=" + UidEmpresa;
            string _strMVSucursal = await _client.GetStringAsync(url);
            string MVSucursal = JsonConvert.DeserializeObject<ResponseHelper>(_strMVSucursal).Data.ToString();
            App.MVSucursal = JsonConvert.DeserializeObject<VistaDelModelo.VMSucursales>(MVSucursal);

            PickerSucursales.ItemsSource = App.MVSucursal.LISTADESUCURSALES;
            var SelectSucursal = App.MVSucursal.LISTADESUCURSALES.Find(t => t.IDENTIFICADOR == AppPuestoTacos.Helpers.Settings.NombreSucursal);
            this.perfil = perfil;
            PickerSucursales.SelectedItem = SelectSucursal;
        }
    }
}