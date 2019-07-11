using AppCliente.WebApi;
using Newtonsoft.Json;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

using Xamarin.Forms.Xaml;

namespace AppCliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DireccionModificar : ContentPage
	{

        public List<VMDireccion> DireccionesListaEstados = new List<VMDireccion>();
        public List<VMDireccion> DireccionesListaMunicipios = new List<VMDireccion>();
        public List<VMDireccion> DireccionesListaCiudad = new List<VMDireccion>();
        public List<VMDireccion> DireccionesListaColonia = new List<VMDireccion>();
        string Longitud = "0";  
        string Latitud = "0";
        double Longitud1 = 0;
        double Latitud1 = 0;
        Button Button;
        Label IDDireccionBusqueda;
        Xamarin.Forms.GoogleMaps.Position MyPosicion;
        HttpClient _client = new HttpClient();


        public DireccionModificar (Xamarin.Forms.GoogleMaps.Position MyPosicion)
		{
			InitializeComponent ();
            this.MyPosicion = MyPosicion;
            MIDireccion();
            Latitud = MyPosicion.Latitude.ToString();
            Longitud = MyPosicion.Longitude.ToString();

        }

        public DireccionModificar(Xamarin.Forms.GoogleMaps.Position MyPosicion, VMDireccion objDireccion)
        {
            InitializeComponent();
            this.MyPosicion = MyPosicion;
            MIDireccion();
            txtID.Text = objDireccion.ID.ToString();
            Latitud = MyPosicion.Latitude.ToString();
            Longitud = MyPosicion.Longitude.ToString();

        }



        private void MypickerPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            DireccionesListaMunicipios.Clear();
            DireccionesListaCiudad.Clear();
            DireccionesListaColonia.Clear();
            DireccionesListaEstados.Clear();
            MypickerMunicipio.Items.Clear();
            MypickerEstado.Items.Clear();
            MypickerCiudad.Items.Clear();
            MypickerColonia.Items.Clear();
            Guid Pais = new Guid("afd6c3b7-f5be-40c9-8385-936d275a8d6b");
            DataTable dt = new DataTable();

            dt = AppCliente.App.MVDireccion.Estados(Pais);

            DireccionesListaEstados.Clear();
            MypickerEstado.ItemsSource = null;
            MypickerMunicipio.ItemsSource = null;
            foreach (DataRow item in dt.Rows)
            {
                DireccionesListaEstados.Add(
                  new VMDireccion()
                  {
                      ID = new Guid(item["IdEstado"].ToString()),
                      ESTADO = item["Nombre"].ToString()

                  });
                MypickerEstado.Items.Add(item["Nombre"].ToString());
            }
        }

        private void MypickerEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DireccionesListaEstados.Count > 0)
            {
                VMDireccion x = DireccionesListaEstados.Find(t => t.ESTADO == MypickerEstado.SelectedItem.ToString());
                Guid estado = x.ID; ;
                DataTable dt = new DataTable();

                dt = AppCliente.App.MVDireccion.Municipios(estado);

                DireccionesListaMunicipios.Clear();
                DireccionesListaCiudad.Clear();
                DireccionesListaColonia.Clear();

                MypickerMunicipio.Items.Clear();

                MypickerCiudad.Items.Clear();
                MypickerColonia.Items.Clear();

                foreach (DataRow item in dt.Rows)
                {

                    DireccionesListaMunicipios.Add(
                      new VMDireccion()
                      {
                          ID = new Guid(item["IdMunicipio"].ToString()),
                          ESTADO = item["Nombre"].ToString()

                      });
                    MypickerMunicipio.Items.Add(item["Nombre"].ToString());
                }
            }
        }

        private void MypickerMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DireccionesListaMunicipios.Count > 0)
            {
                DireccionesListaCiudad.Clear();
                DireccionesListaColonia.Clear();

                MypickerCiudad.Items.Clear();
                MypickerColonia.Items.Clear();
                VMDireccion x = DireccionesListaMunicipios.Find(t => t.ESTADO == MypickerMunicipio.SelectedItem.ToString());
                Guid estado = x.ID; ;
                DataTable dt = new DataTable();

                dt = AppCliente.App.MVDireccion.Ciudades(estado);

                DireccionesListaCiudad.Clear();

                MypickerCiudad.ItemsSource = null;
                MypickerColonia.ItemsSource = null;
                foreach (DataRow item in dt.Rows)
                {

                    DireccionesListaCiudad.Add(
                      new VMDireccion()
                      {
                          ID = new Guid(item["IdCiudad"].ToString()),
                          ESTADO = item["Nombre"].ToString()

                      });
                    MypickerCiudad.Items.Add(item["Nombre"].ToString());
                }
            }
        }

        private void MypickerCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DireccionesListaCiudad.Count > 0)
            {
                DireccionesListaColonia.Clear();
                MypickerColonia.Items.Clear();
                VMDireccion x = DireccionesListaCiudad.Find(t => t.ESTADO == MypickerCiudad.SelectedItem.ToString());
                Guid estado = x.ID; ;
                DataTable dt = new DataTable();
                dt = AppCliente.App.MVDireccion.Colonias(estado);
                DireccionesListaColonia.Clear();
                MypickerColonia.ItemsSource = null;
                foreach (DataRow item in dt.Rows)
                {
                    DireccionesListaColonia.Add(
                      new VMDireccion()
                      {
                          ID = new Guid(item["IdColonia"].ToString()),
                          ESTADO = item["Nombre"].ToString()

                      });
                    MypickerColonia.Items.Add(item["Nombre"].ToString());
                }
            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        private void BtnGuardarEditar_Clicked(object sender, EventArgs e)
        {
            GuardarDireccion();
        }



        public async void MIDireccion()
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(MyPosicion.Latitude, MyPosicion.Longitude);
            var placemark = placemarks?.FirstOrDefault();
            if (placemarks != null)
            {
                var geocodeAddress =
                    $"AdminArea:       {placemark.AdminArea}\n" +
                    $"CountryCode:     {placemark.CountryCode}\n" +
                    $"CountryName:     {placemark.CountryName}\n" +
                    $"FeatureName:     {placemark.FeatureName}\n" +
                    $"Locality:        {placemark.Locality}\n" +
                    $"PostalCode:      {placemark.PostalCode}\n" +
                    $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                    $"SubLocality:     {placemark.SubLocality}\n" +
                    $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                    $"XD: {placemark.Location}\n"+
                    $"Thoroughfare:    {placemark.Thoroughfare}\n";
                MypickerPais.SelectedIndex = 1;
                try
                {
                    
                    MypickerEstado.SelectedItem = placemark.AdminArea.ToString().ToUpper();
                    //pickerMunicipio.SelectedItem = placemark.SubAdminArea.ToString().ToUpper();
                    //int index = DireccionesListaEstados.FindIndex(t => t.ESTADO == placemark.AdminArea.ToString().ToUpper());                   
                    //MypickerEstado.SelectedIndex = index;
                    MypickerMunicipio.SelectedItem = placemark.SubAdminArea.ToString().ToUpper();
                    MypickerCiudad.SelectedItem = placemark.Locality.ToString().ToUpper();
                    MypickerColonia.SelectedItem = placemark.SubLocality.ToString().ToUpper();
                }
                catch (Exception)
                {
                }
                try
                {
                    if (!string.IsNullOrEmpty(placemark.Thoroughfare.ToString()))
                    {
                        txtCalle.Text = placemark.Thoroughfare.ToString();
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (!string.IsNullOrEmpty(placemark.PostalCode.ToString()))
                    {
                        txtCodigoPostal.Text = placemark.PostalCode.ToString();
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                }
                catch (Exception)
                {
                }
                try
                {
                    if (!string.IsNullOrEmpty(placemark.SubThoroughfare.ToString()))
                    {
                        txtManzana.Text = placemark.SubThoroughfare.ToString();
                    }
                }
                catch (Exception)
                {
                }
                //txtCountryCode.Text = placemark.CountryCode.ToString();
                //txtCountryName.Text = placemark.CountryName.ToString();
                //txtAdminArea.Text = placemark.AdminArea.ToString();
                //txtSubAdminArea.Text = placemark.SubAdminArea.ToString();
                //txtLocality.Text = placemark.Locality.ToString();
                //txtFeatureName.Text = placemark.FeatureName.ToString()
                //txtSubLocality.Text = placemark.SubLocality.ToString();
                //txtSubThoroughfare.Text = placemark.SubThoroughfare.ToString();
                //txtThoroughfare.Text = placemark.Thoroughfare.ToString();
            }
        }



        public async void GuardarDireccion()
        {          try
            {
                Guid UidPais = new Guid("afd6c3b7-f5be-40c9-8385-936d275a8d6b");

                VMDireccion x = DireccionesListaEstados.Find(t => t.ESTADO == MypickerEstado.SelectedItem.ToString());
                Guid UidEstado = x.ID;

                VMDireccion a = DireccionesListaMunicipios.Find(t => t.ESTADO == MypickerMunicipio.SelectedItem.ToString());
                Guid UidMunicipio = a.ID;

                VMDireccion b = DireccionesListaCiudad.Find(t => t.ESTADO == MypickerCiudad.SelectedItem.ToString());
                Guid UidCiudad = b.ID;

                VMDireccion c = DireccionesListaColonia.Find(t => t.ESTADO == MypickerColonia.SelectedItem.ToString());
                Guid UidColonia = c.ID;

                string NOMBRECOLONIA = AppCliente.App.MVDireccion.ObtenerNombreDeLaColonia(UidColonia.ToString());



                //string NOMBRECIUDAD = AppCliente.App.MVDireccion.ObtenerNombreDeLaCiudad(UidCiudad.ToString());

                //if (txtID.Text != string.Empty && txtID.Text != null)
                //{
                //    AppCliente.App.MVDireccion.ActualizaListaDireccion(txtID.Text, UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, txtCalle.Text, txtEntreCalle.Text, txtYCalle.Text, txtManzana.Text, txtLote.Text, txtCodigoPostal.Text, txtReferencia.Text, txtIdentificador.Text, NOMBRECIUDAD, NOMBRECOLONIA, Latitud.ToString(), Longitud.ToString());
                //}
                //else
                //{
                //    AppCliente.App.MVDireccion.AgregaDireccionALista(UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, txtCalle.Text, txtEntreCalle.Text, txtYCalle.Text, txtManzana.Text, txtLote.Text, txtCodigoPostal.Text, txtReferencia.Text, NOMBRECIUDAD, NOMBRECOLONIA, txtIdentificador.Text, Latitud.ToString(), Longitud.ToString());
                //}

                //AppCliente.App.MVDireccion.GuardaListaDeDirecciones(AppCliente.App.MVDireccion.ListaDIRECCIONES, new Guid(AppCliente.App.Global1), "asp_AgregaDireccionUsuario", "Usuario");


                //for (int i = 0; i < AppCliente.App.MVDireccion.ListaDIRECCIONES.Count; i++)
                //{
                //    Guid guid = Guid.NewGuid();
                //    if (AppCliente.App.MVDireccion.ListaDIRECCIONES[i].Latitud != null)
                //    {
                //        Latitud = AppCliente.App.MVDireccion.ListaDIRECCIONES[i].Latitud;
                //    }
                //    else
                //    {
                //        Latitud = "0";
                //    }

                //    if (AppCliente.App.MVDireccion.ListaDIRECCIONES[i].Longitud != null)
                //    {
                //        Longitud = AppCliente.App.MVDireccion.ListaDIRECCIONES[i].Longitud;
                //    }
                //    else
                //    {
                //        Latitud = "0";
                //    }

                //    AppCliente.App.MVUbicacion.GuardaUbicacionDireccion(AppCliente.App.MVDireccion.ListaDIRECCIONES[i].ID, guid, Latitud, Longitud);
                //}



                string _Url = $"http://godeliverix.net/api/Direccion/GetGuardarDireccion?UidUsuario={App.Global1}&UidPais={UidPais}&UidEstado={UidEstado}&UidMunicipio={UidMunicipio}&UidCiudad={UidCiudad}&UidColonia={UidColonia}&CallePrincipal={txtCalle.Text}&CalleAux1={txtEntreCalle.Text}&CalleAux2={txtYCalle.Text}&Manzana={txtManzana.Text}&Lote={txtLote.Text}&CodigoPostal={txtCodigoPostal.Text}&Referencia={txtReferencia.Text}&NOMBRECIUDAD=s&NOMBRECOLONIA=s&Identificador={txtIdentificador.Text}&Latitud={Latitud}&Longitud={Longitud}";
                var content = await _client.GetAsync(_Url);

                _Url = ("http://www.godeliverix.net/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + AppCliente.App.Global1);
                string strDirecciones = await _client.GetStringAsync(_Url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
                //AppCliente.App.MVDireccion.ObtenerDireccionesUsuario(AppCliente.App.Global1);

                //await Navigation.PopToRootAsync();
                //MyListViewDirecciones.ItemsSource = null;

                //MyListViewDirecciones.ItemsSource = AppCliente.App.MVDireccion.ListaDIRECCIONES;

                //txtID.Text = null;

                //limpiarFormulario();
            }
            catch (Exception)
            {
                await DisplayAlert("sorry","Agrege todos los datos","ok");
    }


}
    }
}