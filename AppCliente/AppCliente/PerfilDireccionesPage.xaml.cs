using AppCliente.WebApi;
using Newtonsoft.Json;
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
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class
        PerfilDireccionesPage : ContentPage
    {
        public List<VMDireccion> DireccionesListaEstados = new List<VMDireccion>();
        public List<VMDireccion> DireccionesListaMunicipios = new List<VMDireccion>();
        public List<VMDireccion> DireccionesListaCiudad = new List<VMDireccion>();
        public List<VMDireccion> DireccionesListaColonia = new List<VMDireccion>();
        double Longitud = 0;
        double Latitud = 0;
        double Longitud1 = 0;
        double Latitud1 = 0;
        Button Button;
        Label IDDireccionBusqueda;
        HttpClient _WebApiGoDeliverix = new HttpClient();
        public PerfilDireccionesPage()
        {
            InitializeComponent();
            if (App.Global1 != string.Empty)
            {
                //App.MVDireccion.ObtenerDireccionesUsuario(App.Global1);
                for (int i = 0; i < App.MVDireccion.ListaDIRECCIONES.Count; i++)
                {
                    App.MVDireccion.ListaDIRECCIONES[i].Clicked = false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Helpers.Settings.UidDireccion))
                {
                    App.MVDireccion.ListaDIRECCIONES.Clear();
                    App.MVDireccion.ListaDIRECCIONES.Add(new VMDireccion()
                    {
                        ID = new Guid(Helpers.Settings.UidDireccion),
                        PAIS = Helpers.Settings.StrPAIS,
                        ESTADO = Helpers.Settings.StrESTADO,
                        MUNICIPIO = Helpers.Settings.StrMUNICIPIO,
                        CIUDAD = Helpers.Settings.StrCIUDAD,
                        COLONIA = Helpers.Settings.StrCOLONIA,
                        CALLE0 = Helpers.Settings.StrCALLE0,
                        CALLE1 = Helpers.Settings.StrCALLE1,
                        CALLE2 = Helpers.Settings.StrCALLE2,
                        MANZANA = Helpers.Settings.StrMANZANA,
                        LOTE = Helpers.Settings.StrLOTE,
                        CodigoPostal = Helpers.Settings.StrCodigoPostal,
                        REFERENCIA = Helpers.Settings.StrREFERENCIA,
                        IDENTIFICADOR = Helpers.Settings.StrIDENTIFICADOR,
                        NOMBRECIUDAD = Helpers.Settings.StrNombreCiudad,
                        NOMBRECOLONIA = Helpers.Settings.StrNombreColonia
                    });
                }

            }
            //MiUbicacion();
            MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
            MyListViewDirecciones.TabIndex = 0;
        }


        //Para seleccionar 
        public PerfilDireccionesPage(Button button, Label IDDireccionBusqueda)
        {
            InitializeComponent();

            for (int i = 0; i < App.MVDireccion.ListaDIRECCIONES.Count; i++)
            {
                App.MVDireccion.ListaDIRECCIONES[i].Clicked = false;
            }

            MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
            //MiUbicacion();
            MyListViewDirecciones.TabIndex = 0;
            this.Button = button;
            this.IDDireccionBusqueda = IDDireccionBusqueda;
            Title = "seleccionar Direccion";
        }

        [Obsolete]
        private void MenuItem_Editar(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            VMDireccion ObjItem = (VMDireccion)item.CommandParameter;
            MypickerPais.SelectedIndex = 0;

            //string tipo = "Gestion";
            Guid Pais = new Guid("afd6c3b7-f5be-40c9-8385-936d275a8d6b");
            DataTable dt = new DataTable();

            dt = App.MVDireccion.Estados(Pais);

            DireccionesListaEstados.Clear();
            MypickerEstado.ItemsSource = null;
            MypickerCiudad.ItemsSource = null;
            MypickerColonia.ItemsSource = null;

            foreach (DataRow item2 in dt.Rows)
            {
                DireccionesListaEstados.Add(
                  new VMDireccion()
                  {
                      ID = new Guid(item2["IdEstado"].ToString()),
                      ESTADO = item2["Nombre"].ToString()
                  });
                MypickerEstado.Items.Add(item2["Nombre"].ToString());
            }
            string Estado = DireccionesListaEstados.Find(t => t.ID.ToString() == ObjItem.ESTADO).ESTADO;
            MypickerEstado.SelectedItem = Estado;

            Guid IDEstado = DireccionesListaEstados.Find(t => t.ID.ToString() == ObjItem.ESTADO).ID;
            dt = App.MVDireccion.Municipios(IDEstado);
            DireccionesListaMunicipios.Clear();
            MypickerMunicipio.ItemsSource = null;
            foreach (DataRow item2 in dt.Rows)
            {
                DireccionesListaMunicipios.Add(
                  new VMDireccion()
                  {
                      ID = new Guid(item2["IdMunicipio"].ToString()),

                      ESTADO = item2["Nombre"].ToString()
                  });
                MypickerMunicipio.Items.Add(item2["Nombre"].ToString());
            }
            string Municipio = DireccionesListaMunicipios.Find(t => t.ID.ToString() == ObjItem.MUNICIPIO).ESTADO;
            MypickerMunicipio.SelectedItem = Municipio;

            Guid IDMunicipio = DireccionesListaMunicipios.Find(t => t.ID.ToString() == ObjItem.MUNICIPIO).ID;
            dt = App.MVDireccion.Ciudades(IDMunicipio);
            DireccionesListaCiudad.Clear();
            MypickerCiudad.ItemsSource = null;
            foreach (DataRow item2 in dt.Rows)
            {
                DireccionesListaCiudad.Add(
                  new VMDireccion()
                  {
                      ID = new Guid(item2["IdCiudad"].ToString()),
                      ESTADO = item2["Nombre"].ToString()
                  });
                MypickerCiudad.Items.Add(item2["Nombre"].ToString());
            }
            string Ciudad = DireccionesListaCiudad.Find(t => t.ID.ToString() == ObjItem.CIUDAD).ESTADO;
            MypickerCiudad.SelectedItem = Ciudad;

            Guid IDCiudad = DireccionesListaCiudad.Find(t => t.ID.ToString() == ObjItem.CIUDAD).ID;
            dt = App.MVDireccion.Colonias(IDCiudad);
            DireccionesListaColonia.Clear();
            MypickerColonia.ItemsSource = null;
            foreach (DataRow item2 in dt.Rows)
            {
                DireccionesListaColonia.Add(
                  new VMDireccion()
                  {
                      ID = new Guid(item2["IdColonia"].ToString()),
                      ESTADO = item2["Nombre"].ToString()
                  });
                MypickerColonia.Items.Add(item2["Nombre"].ToString());
            }
            string Colonia = DireccionesListaColonia.Find(t => t.ID.ToString() == ObjItem.COLONIA).ESTADO;
            MypickerColonia.SelectedItem = Colonia;

            txtIdentificador.Text = ObjItem.IDENTIFICADOR;
            txtCalle.Text = ObjItem.CALLE0;
            txtEntreCalle.Text = ObjItem.CALLE1;
            txtYCalle.Text = ObjItem.CALLE2;
            txtManzana.Text = ObjItem.MANZANA;
            txtLote.Text = ObjItem.LOTE;
            txtCodigoPostal.Text = ObjItem.CodigoPostal;
            txtReferencia.Text = ObjItem.REFERENCIA;
            txtID.Text = ObjItem.ID.ToString();
            PanelDatos.IsVisible = true;
            btnNuevo.IsVisible = false;
            PanelListView.IsVisible = false;
            try
            {
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(double.Parse(ObjItem.Latitud), double.Parse(ObjItem.Longitud)),
                    Label = "hoasd",
                    Address = "dasd",
                    Id = "MI ubicación actual"
                };
                MyMap.Pins.Add(pin);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(ObjItem.Latitud), double.Parse(ObjItem.Longitud)), Distance.FromMiles(1.0)));
                Latitud1 = double.Parse(ObjItem.Latitud);
                Longitud1 = double.Parse(ObjItem.Longitud);
            }
            catch (Exception)
            {
            }
        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            VMDireccion ObjItem = (VMDireccion)item.CommandParameter;
            Guid Gui = ObjItem.ID;
            int index = App.MVDireccion.ListaDIRECCIONES.FindIndex(x => x.ID == Gui);
            App.MVDireccion.QuitaDireeccionDeLista(ObjItem.ID.ToString());
            App.MVDireccion.EliminaDireccionUsuario(ObjItem.ID.ToString());

            App.MVDireccion.ObtenerDireccionesUsuario(App.Global1);
            MyListViewDirecciones.ItemsSource = null;
            MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
        }

        private void Button_Clicked_NuevaDireccion(object sender, EventArgs e)
        {
            limpiarFormulario();
            MiUbicacion();
            PanelDatos.IsVisible = true;
            btnNuevo.IsVisible = false;
            PanelListView.IsVisible = false;
        }

        private async void BtnGuardarEditar_Clicked(object sender, EventArgs e)
        {
            PanelDatos.IsVisible = false;
            btnNuevo.IsVisible = true;
            PanelListView.IsVisible = true;
            Guid UidPais = new Guid("afd6c3b7-f5be-40c9-8385-936d275a8d6b");

            VMDireccion x = DireccionesListaEstados.Find(t => t.ESTADO == MypickerEstado.SelectedItem.ToString());
            Guid UidEstado = x.ID;

            VMDireccion a = DireccionesListaMunicipios.Find(t => t.ESTADO == MypickerMunicipio.SelectedItem.ToString());
            Guid UidMunicipio = a.ID;

            VMDireccion b = DireccionesListaCiudad.Find(t => t.ESTADO == MypickerCiudad.SelectedItem.ToString());
            Guid UidCiudad = b.ID;

            VMDireccion c = DireccionesListaColonia.Find(t => t.ESTADO == MypickerColonia.SelectedItem.ToString());
            Guid UidColonia = c.ID;

            string NOMBRECOLONIA = App.MVDireccion.ObtenerNombreDeLaColonia(UidColonia.ToString());

            string url = string.Empty;

            string NOMBRECIUDAD = App.MVDireccion.ObtenerNombreDeLaCiudad(UidCiudad.ToString());

            if (txtID.Text != string.Empty && txtID.Text != null)
            {
                url = "" + Helpers.Settings.sitio + "/api/Direccion/GetActualizarDireccion?UidPais=" + UidPais + "&UidEstado=" + UidEstado + "&UidMunicipio=" + UidMunicipio + "&UidCiudad=" + UidCiudad + "&UidColonia=" + UidColonia + "&CallePrincipal=" + txtCalle.Text + "&CalleAux1=" + txtEntreCalle.Text + "&CalleAux2=" + txtYCalle.Text + "&Manzana=" + txtManzana.Text + "&Lote=" + txtLote.Text + "&CodigoPostal=" + txtCodigoPostal.Text + "&Referencia=" + txtReferencia.Text + "&NOMBRECIUDAD=S&NOMBRECOLONIA=S&Identificador=" + txtIdentificador.Text + "&Latitud=0&Longitud=0&UidDireccion=" + txtID.Text + "";
            }
            else
            {
                url = "" + Helpers.Settings.sitio + "/api/Direccion/GetGuardarDireccion?UidUsuario=" + App.Global1 + "&UidPais=" + UidPais + "&UidEstado=" + UidEstado + "&UidMunicipio=" + UidMunicipio + "&UidCiudad=" + UidCiudad + "&UidColonia=" + UidColonia + "&CallePrincipal=" + txtCalle.Text + "&CalleAux1=" + txtEntreCalle.Text + "&CalleAux2=" + txtYCalle.Text + "&Manzana=" + txtManzana.Text + "&Lote=" + txtLote.Text + "&CodigoPostal=" + txtCodigoPostal.Text + "&Referencia=" + txtReferencia.Text + "&NOMBRECIUDAD=S&NOMBRECOLONIA=S&Identificador=" + txtIdentificador.Text + "&Latitud=0&Longitud=0";
            }

            await _WebApiGoDeliverix.GetAsync(url);

            url = ("" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + App.Global1);
            var strDirecciones = await _WebApiGoDeliverix.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
            App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);

            MyListViewDirecciones.ItemsSource = null;

            MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;

            txtID.Text = null;

            limpiarFormulario();
        }


        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            PanelDatos.IsVisible = false;
            btnNuevo.IsVisible = true;
            PanelListView.IsVisible = true;
            txtID.Text = null;
            limpiarFormulario();
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

            //string tipo = "Gestion";
            Guid Pais = new Guid("afd6c3b7-f5be-40c9-8385-936d275a8d6b");
            DataTable dt = new DataTable();

            dt = App.MVDireccion.Estados(Pais);

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

                dt = App.MVDireccion.Municipios(estado);

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

                dt = App.MVDireccion.Ciudades(estado);

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
                dt = App.MVDireccion.Colonias(estado);
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


        public async void MIDireccion()
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(Latitud, Longitud);

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
                    $"Thoroughfare:    {placemark.Thoroughfare}\n";


                MypickerPais.SelectedIndex = 1;
                try
                {
                    MypickerMunicipio.SelectedItem = placemark.SubAdminArea.ToString().ToUpper();
                    MypickerEstado.SelectedItem = placemark.AdminArea.ToString().ToUpper();
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
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(Latitud, Longitud),
                    Label = "hoasd",
                    Address = "dasd",
                    Id = "MI ubicación actual"
                };
                MyMap.Pins.Add(pin);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Latitud, Longitud), Distance.FromMiles(1.0)));
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

        public async void MiUbicacion()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                Latitud = location.Latitude;
                Longitud = location.Longitude;
            }
            MIDireccion();
        }

        public void limpiarFormulario()
        {
            Longitud = 0;
            Latitud = 0;
            Longitud1 = 0;
            Latitud1 = 0;
            DireccionesListaEstados.Clear();
            MypickerEstado.ItemsSource = null;
            MypickerCiudad.ItemsSource = null;
            MypickerColonia.ItemsSource = null;
            txtID.Text = null;
            txtCalle.Text = null;
            txtEntreCalle.Text = null;
            txtYCalle.Text = null;
            txtLote.Text = null;
            txtManzana.Text = null;
            txtReferencia.Text = null;
            txtIdentificador.Text = null;
            txtCodigoPostal.Text = null;
        }

        private void MyListViewDirecciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e;
            VMDireccion ObjItem = (VMDireccion)item.Item;
            if (ObjItem.Clicked)
            {
                ObjItem.Clicked = false;
                txtIDDireccionn.Text = "0";
            }
            else
            {
                for (int i = 0; i < App.MVDireccion.ListaDIRECCIONES.Count; i++)
                {
                    App.MVDireccion.ListaDIRECCIONES[i].Clicked = false;
                }
                ObjItem.Clicked = true;
                txtIDDireccionn.Text = ObjItem.ID.ToString();
            }
            MyListViewDirecciones.ItemsSource = null;
            MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
        }

        public async void EliminarDireccion()
        {
            if (txtIDDireccionn.Text != "0" && !string.IsNullOrEmpty(txtIDDireccionn.Text))
            {
                var action = await DisplayAlert("Eliminar?", "Estas seguro de eliminar esta direccion?", "Si", "No");
                if (action)
                {
                    Guid Gui = new Guid(txtIDDireccionn.Text);

                    int index = App.MVDireccion.ListaDIRECCIONES.FindIndex(x => x.ID == Gui);

                    var tex = ("" + Helpers.Settings.sitio + "/api/Direccion/DeleteDireccionUsuario?UidDireccion=" + txtIDDireccionn.Text);
                    string strDirecciones = await _WebApiGoDeliverix.GetStringAsync(tex);

                    tex = ("" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + App.Global1);
                    strDirecciones = await _WebApiGoDeliverix.GetStringAsync(tex);
                    var obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                    App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);

                    MyListViewDirecciones.ItemsSource = null;
                    MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
                    txtIDDireccionn.Text = "0";
                }
            }

        }


        private void Button_Clicked_Eliminar(object sender, EventArgs e)
        {
            EliminarDireccion();
        }

        private void Button_Clicked_Editar(object sender, EventArgs e)
        {
            Guid Gui = new Guid(txtIDDireccionn.Text);

            int index = AppCliente.App.MVDireccion.ListaDIRECCIONES.FindIndex(x => x.ID == Gui);

            MypickerPais.SelectedIndex = 0;


            //string tipo = "Gestion";
            Guid Pais = new Guid("afd6c3b7-f5be-40c9-8385-936d275a8d6b");
            DataTable dt = new DataTable();

            dt = App.MVDireccion.Estados(Pais);

            DireccionesListaEstados.Clear();
            MypickerEstado.ItemsSource = null;
            MypickerCiudad.ItemsSource = null;
            MypickerColonia.ItemsSource = null;

            foreach (DataRow item2 in dt.Rows)
            {
                DireccionesListaEstados.Add(
                  new VMDireccion()
                  {
                      ID = new Guid(item2["IdEstado"].ToString()),
                      ESTADO = item2["Nombre"].ToString()

                  });
                MypickerEstado.Items.Add(item2["Nombre"].ToString());
            }
            string Estado = DireccionesListaEstados.Find(t => t.ID.ToString() == App.MVDireccion.ListaDIRECCIONES[index].ESTADO).ESTADO;
            MypickerEstado.SelectedItem = Estado;


            Guid IDEstado = DireccionesListaEstados.Find(t => t.ID.ToString() == App.MVDireccion.ListaDIRECCIONES[index].ESTADO).ID;
            dt = App.MVDireccion.Municipios(IDEstado);
            DireccionesListaMunicipios.Clear();
            MypickerMunicipio.ItemsSource = null;
            foreach (DataRow item2 in dt.Rows)
            {
                DireccionesListaMunicipios.Add(
                  new VMDireccion()
                  {
                      ID = new Guid(item2["IdMunicipio"].ToString()),

                      ESTADO = item2["Nombre"].ToString()
                  });
                MypickerMunicipio.Items.Add(item2["Nombre"].ToString());
            }
            string Municipio = DireccionesListaMunicipios.Find(t => t.ID.ToString() == App.MVDireccion.ListaDIRECCIONES[index].MUNICIPIO).ESTADO;
            MypickerMunicipio.SelectedItem = Municipio;


            Guid IDMunicipio = DireccionesListaMunicipios.Find(t => t.ID.ToString() == App.MVDireccion.ListaDIRECCIONES[index].MUNICIPIO).ID;
            dt = App.MVDireccion.Ciudades(IDMunicipio);
            DireccionesListaCiudad.Clear();
            MypickerCiudad.ItemsSource = null;
            foreach (DataRow item2 in dt.Rows)
            {
                DireccionesListaCiudad.Add(
                  new VMDireccion()
                  {
                      ID = new Guid(item2["IdCiudad"].ToString()),
                      ESTADO = item2["Nombre"].ToString()
                  });
                MypickerCiudad.Items.Add(item2["Nombre"].ToString());
            }
            string Ciudad = DireccionesListaCiudad.Find(t => t.ID.ToString() == App.MVDireccion.ListaDIRECCIONES[index].CIUDAD).ESTADO;
            MypickerCiudad.SelectedItem = Ciudad;

            Guid IDCiudad = DireccionesListaCiudad.Find(t => t.ID.ToString() == App.MVDireccion.ListaDIRECCIONES[index].CIUDAD).ID;
            dt = App.MVDireccion.Colonias(IDCiudad);
            DireccionesListaColonia.Clear();
            MypickerColonia.ItemsSource = null;
            foreach (DataRow item2 in dt.Rows)
            {
                DireccionesListaColonia.Add(
                  new VMDireccion()
                  {
                      ID = new Guid(item2["IdColonia"].ToString()),
                      ESTADO = item2["Nombre"].ToString()
                  });
                MypickerColonia.Items.Add(item2["Nombre"].ToString());
            }
            string Colonia = DireccionesListaColonia.Find(t => t.ID.ToString() == App.MVDireccion.ListaDIRECCIONES[index].COLONIA).ESTADO;
            MypickerColonia.SelectedItem = Colonia;

            txtIdentificador.Text = App.MVDireccion.ListaDIRECCIONES[index].IDENTIFICADOR;
            txtCalle.Text = App.MVDireccion.ListaDIRECCIONES[index].CALLE0;
            txtEntreCalle.Text = App.MVDireccion.ListaDIRECCIONES[index].CALLE1;
            txtYCalle.Text = App.MVDireccion.ListaDIRECCIONES[index].CALLE2;
            txtManzana.Text = App.MVDireccion.ListaDIRECCIONES[index].MANZANA;
            txtLote.Text = App.MVDireccion.ListaDIRECCIONES[index].LOTE;
            txtCodigoPostal.Text = App.MVDireccion.ListaDIRECCIONES[index].CodigoPostal;
            txtReferencia.Text = App.MVDireccion.ListaDIRECCIONES[index].REFERENCIA;
            txtID.Text = App.MVDireccion.ListaDIRECCIONES[index].ID.ToString();
            PanelDatos.IsVisible = true;
            btnNuevo.IsVisible = false;
            PanelListView.IsVisible = false;
            try
            {
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(double.Parse(App.MVDireccion.ListaDIRECCIONES[index].Latitud), double.Parse(App.MVDireccion.ListaDIRECCIONES[index].Longitud)),
                    Label = "hoasd",
                    Address = "dasd",
                    Id = "MI ubicación actual"
                };
                MyMap.Pins.Add(pin);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(App.MVDireccion.ListaDIRECCIONES[index].Latitud), double.Parse(App.MVDireccion.ListaDIRECCIONES[index].Longitud)), Distance.FromMiles(1.0)));
                Latitud1 = double.Parse(App.MVDireccion.ListaDIRECCIONES[index].Latitud);
                Longitud1 = double.Parse(App.MVDireccion.ListaDIRECCIONES[index].Longitud);
            }
            catch (Exception)
            {
            }
        }

        private void Button_Clicked_seleccionar(object sender, EventArgs e)
        {
            App.DireccionABuscar = txtIDDireccionn.Text;
            App.Current.MainPage.Navigation.PopAsync(false);
            try
            {
                Button.Text = "ENTREGAR EN " + App.MVDireccion.ListaDIRECCIONES.Find(x => x.ID == new Guid(txtIDDireccionn.Text)).IDENTIFICADOR + " >";
                IDDireccionBusqueda.Text = App.MVDireccion.ListaDIRECCIONES.Find(x => x.ID == new Guid(txtIDDireccionn.Text)).ID.ToString();
            }
            catch (Exception)
            {
            }
        }
    }

}