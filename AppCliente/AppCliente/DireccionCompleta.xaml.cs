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
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DireccionCompleta : ContentPage
    {
        private VMDireccion _oDireccion;
        public VMDireccion oDireccion
        {
            get { return _oDireccion; }
            set { _oDireccion = value; }
        }
        private VMDireccion _oDireccionModificada;
        public VMDireccion oDireccionModificada
        {
            get { return _oDireccionModificada; }
            set { _oDireccionModificada = value; }
        }
        public List<VMDireccion> DireccionesListaEstados = new List<VMDireccion>();
        public List<VMDireccion> DireccionesListaMunicipios = new List<VMDireccion>();
        public List<VMDireccion> DireccionesListaCiudad = new List<VMDireccion>();
        public List<VMDireccion> DireccionesListaColonia = new List<VMDireccion>();
        double Longitud = 0.0;
        double Latitud = 0.0;
        ListView LVDirecciones = new ListView();

        public DireccionCompleta(VMDireccion odireccion, ListView lista)
        {
            InitializeComponent();
            LVDirecciones = lista;
            oDireccion = odireccion;
            if (oDireccion != null)
            {
                pagina.Title = oDireccion.IDENTIFICADOR;
                CargaDireccion();
            }
            else
            {
                pagina.Title = "Nueva ubicación";
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var location = await Geolocation.GetLocationAsync();
                    Latitud = location.Latitude;
                    Longitud = location.Longitude;

                    if (location != null)
                    {
                        var geo = new Geocoder();
                        var placemarks = await Geocoding.GetPlacemarksAsync(Latitud, Longitud);
                        //var addresses = await geo.GetAddressesForPositionAsync(new Position(Latitud, Longitud));
                        foreach (var item in placemarks)
                        {
                            ApiService ApiService = new ApiService("/api/Direccion");
                            Dictionary<string, string> parameters = new Dictionary<string, string>();
                            parameters.Add("StrNombreCiudad", item.Locality);
                            parameters.Add("CodigoEstado", item.AdminArea);
                            parameters.Add("CodigoPais", item.CountryCode);
                            parameters.Add("Latitud", item.Location.Latitude.ToString());
                            parameters.Add("Longitud", item.Location.Longitude.ToString());
                            var result = await ApiService.GET<VMDireccion>(action: "GetObtenerDireccionConDatosDeGoogle", responseType: ApiService.ResponseType.Object, arguments: parameters);
                            var oReponse = result as ResponseHelper;
                            if (result != null && oReponse.Status != false)
                            {
                                App.MVDireccion = oReponse.Data as VMDireccion;
                                if (App.MVDireccion.ListaDIRECCIONES.Count == 1)
                                {
                                    break;
                                }
                            }
                        }
                        CargaDireccionNueva();
                    }
                    Pin AquiEstoy = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "Nueva ubicación",
                        Position = new Position(Latitud, Longitud)
                    };
                    map.Pins.Clear();
                    map.Pins.Add(AquiEstoy);
                    var pos = new Position(Latitud, Longitud);
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(300)), true);
                });

            }
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

            dt = App.MVDireccion.Estados(Pais);

            DireccionesListaEstados.Clear();
            MypickerEstado.ItemsSource = null;
            MypickerMunicipio.ItemsSource = null;
            foreach (DataRow item in dt.Rows)
            {
                DireccionesListaEstados.Add(new VMDireccion()
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
                          MUNICIPIO = item["Nombre"].ToString()

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
                VMDireccion x = DireccionesListaMunicipios.Find(t => t.MUNICIPIO == MypickerMunicipio.SelectedItem.ToString());
                Guid Municipio = x.ID; ;
                DataTable dt = new DataTable();

                dt = App.MVDireccion.Ciudades(Municipio);

                DireccionesListaCiudad.Clear();

                MypickerCiudad.ItemsSource = null;
                MypickerColonia.ItemsSource = null;
                foreach (DataRow item in dt.Rows)
                {
                    DireccionesListaCiudad.Add(
                      new VMDireccion()
                      {
                          ID = new Guid(item["IdCiudad"].ToString()),
                          CIUDAD = item["Nombre"].ToString()

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
                VMDireccion x = DireccionesListaCiudad.Find(t => t.CIUDAD == MypickerCiudad.SelectedItem.ToString());
                Guid estado = x.ID;
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
                          COLONIA = item["Nombre"].ToString()

                      });
                    MypickerColonia.Items.Add(item["Nombre"].ToString());
                }
            }
        }
        private void MypickerColonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DireccionesListaColonia.Count > 0)
            {
                VMDireccion Colonia = DireccionesListaColonia.Find(t => t.COLONIA == MypickerColonia.SelectedItem.ToString());
                VMDireccion MVDireccion = new VMDireccion();
                txtCodigoPostal.Text = MVDireccion.ObtenerCodigoPostal(Colonia.ID);
            }

        }

        protected void CargaDireccion()
        {
            try
            {
                MypickerPais.SelectedIndex = 1;
                int Estado = new int();
                for (int i = 0; i < DireccionesListaEstados.Count; i++)
                {
                    if (DireccionesListaEstados[i].ID == new Guid(oDireccion.ESTADO.ToUpper()))
                    {
                        Estado = i;
                    }
                }
                MypickerEstado.SelectedIndex = Estado;
                int Municipio = new int();
                for (int i = 0; i < DireccionesListaMunicipios.Count; i++)
                {
                    if (DireccionesListaMunicipios[i].ID == new Guid(oDireccion.MUNICIPIO.ToUpper()))
                    {
                        Municipio = i;
                    }
                }
                MypickerMunicipio.SelectedIndex = Municipio;
                int Ciudad = new int();
                for (int i = 0; i < DireccionesListaCiudad.Count; i++)
                {
                    if (DireccionesListaCiudad[i].ID == new Guid(oDireccion.CIUDAD.ToUpper()))
                    {
                        Ciudad = i;
                    }
                }
                MypickerCiudad.SelectedIndex = Ciudad;
                int Colonia = new int();
                for (int i = 0; i < DireccionesListaColonia.Count; i++)
                {
                    if (DireccionesListaColonia[i].ID == new Guid(oDireccion.COLONIA.ToUpper()))
                    {
                        Colonia = i;
                    }
                }
                MypickerColonia.SelectedIndex = Colonia;
                txtCalle.Text = oDireccion.CALLE0;
                txtEntreCalle.Text = oDireccion.CALLE1;
                txtYCalle.Text = oDireccion.CALLE2;
                txtReferencia.Text = oDireccion.REFERENCIA;
                txtIdentificador.Text = oDireccion.IDENTIFICADOR;
                txtManzana.Text = oDireccion.MANZANA;
                txtLote.Text = oDireccion.LOTE;
                txtCodigoPostal.Text = oDireccion.CodigoPostal;
                Latitud = double.Parse(oDireccion.Latitud);
                Longitud = double.Parse(oDireccion.Longitud);
                Pin AquiEstoy = new Pin()
                {
                    Type = PinType.Place,
                    Label = oDireccion.IDENTIFICADOR,
                    Position = new Position(Latitud, Longitud)
                };
                map.Pins.Clear();
                map.Pins.Add(AquiEstoy);
                var pos = new Position(Latitud, Longitud);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(300)), true);
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void CargaDireccionNueva()
        {
            try
            {
                MypickerPais.SelectedIndex = 1;
                int Estado = new int();
                for (int i = 0; i < DireccionesListaEstados.Count; i++)
                {
                    if (DireccionesListaEstados[i].ID == new Guid(App.MVDireccion.ListaDIRECCIONES[0].ESTADO.ToUpper()))
                    {
                        Estado = i;
                    }
                }
                MypickerEstado.SelectedIndex = Estado;
                int Municipio = new int();
                for (int i = 0; i < DireccionesListaMunicipios.Count; i++)
                {
                    if (DireccionesListaMunicipios[i].ID == new Guid(App.MVDireccion.ListaDIRECCIONES[0].MUNICIPIO.ToUpper()))
                    {
                        Municipio = i;
                    }
                }
                MypickerMunicipio.SelectedIndex = Municipio;
                int Ciudad = new int();
                for (int i = 0; i < DireccionesListaCiudad.Count; i++)
                {
                    if (DireccionesListaCiudad[i].ID == new Guid(App.MVDireccion.ListaDIRECCIONES[0].CIUDAD.ToUpper()))
                    {
                        Ciudad = i;
                    }
                }
                MypickerCiudad.SelectedIndex = Ciudad;

                txtCalle.Text = string.Empty;
                txtEntreCalle.Text = string.Empty;
                txtYCalle.Text = string.Empty;
                txtReferencia.Text = string.Empty;
                txtIdentificador.Text = string.Empty;
                txtManzana.Text = string.Empty;
                txtLote.Text = string.Empty;
                txtCodigoPostal.Text = string.Empty;
                Pin AquiEstoy = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Nueva ubicación",
                    Position = new Position(Latitud, Longitud)
                };
                map.Pins.Clear();
                map.Pins.Add(AquiEstoy);
                var pos = new Position(Latitud, Longitud);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(300)), true);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async void map_MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            try
            {
                var Latitud = e.Point.Latitude;
                var Longitud = e.Point.Longitude;
                Pin AquiEstoy = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Nueva ubicación",
                    Position = new Position(Latitud, Longitud)
                };
                map.Pins.Clear();
                map.Pins.Add(AquiEstoy);
                var pos = new Position(Latitud, Longitud);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(500)));
                var geo = new Geocoder();
                var location = new Location();
                string content = string.Empty;
                IEnumerable<Placemark> placemarks = await Geocoding.GetPlacemarksAsync(Latitud, Longitud);
                IEnumerable<string> addresses = await geo.GetAddressesForPositionAsync(new Position(Latitud, Longitud));

                if (location != null)
                {
                    foreach (var item in placemarks)
                    {
                        using (HttpClient _webApi = new HttpClient())
                        {
                            string ciudad = string.Empty;
                            if (!string.IsNullOrEmpty(item.Locality))
                            {
                                ciudad = item.Locality;
                                string _URL = "" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionConDatosDeGoogle?StrNombreCiudad=" + ciudad + "&Latitud=" + item.Location.Latitude + "&Longitud=" + item.Location.Longitude + "";
                                await Task.Run(async () => { content = await _webApi.GetStringAsync(_URL); });
                                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                oDireccionModificada = JsonConvert.DeserializeObject<VMDireccion>(obj);
                                if (oDireccionModificada.ListaDIRECCIONES.Count == 1)
                                {
                                    oDireccionModificada.ListaDIRECCIONES[0].Longitud = item.Location.Longitude.ToString();
                                    oDireccionModificada.ListaDIRECCIONES[0].Latitud = item.Location.Latitude.ToString();
                                    MypickerPais.SelectedIndex = 1;
                                    int Estado = new int();
                                    for (int i = 0; i < DireccionesListaEstados.Count; i++)
                                    {
                                        if (DireccionesListaEstados[i].ID == new Guid(oDireccionModificada.ListaDIRECCIONES[0].ESTADO.ToUpper()))
                                        {
                                            Estado = i;
                                        }
                                    }
                                    MypickerEstado.SelectedIndex = Estado;
                                    int Municipio = new int();
                                    for (int i = 0; i < DireccionesListaMunicipios.Count; i++)
                                    {
                                        if (DireccionesListaMunicipios[i].ID == new Guid(oDireccionModificada.ListaDIRECCIONES[0].MUNICIPIO.ToUpper()))
                                        {
                                            Municipio = i;
                                        }
                                    }
                                    MypickerMunicipio.SelectedIndex = Municipio;
                                    int Ciudad = new int();
                                    for (int i = 0; i < DireccionesListaCiudad.Count; i++)
                                    {
                                        if (DireccionesListaCiudad[i].ID == new Guid(oDireccionModificada.ListaDIRECCIONES[0].CIUDAD.ToUpper()))
                                        {
                                            Ciudad = i;
                                        }
                                    }
                                    MypickerCiudad.SelectedIndex = Ciudad;
                                    VMDireccion Colonias = new VMDireccion();
                                    DataTable dt = Colonias.Colonias(new Guid(oDireccionModificada.ListaDIRECCIONES[0].CIUDAD));
                                    DireccionesListaColonia.Clear();
                                    MypickerColonia.Items.Clear();
                                    foreach (DataRow items in dt.Rows)
                                    {
                                        DireccionesListaColonia.Add(
                                          new VMDireccion()
                                          {
                                              ID = new Guid(items["IdColonia"].ToString()),
                                              COLONIA = items["Nombre"].ToString()

                                          });
                                        MypickerColonia.Items.Add(items["Nombre"].ToString());
                                    }
                                    txtCalle.Text = oDireccionModificada.ListaDIRECCIONES[0].CALLE0;
                                    txtEntreCalle.Text = oDireccionModificada.ListaDIRECCIONES[0].CALLE1;
                                    txtYCalle.Text = oDireccionModificada.ListaDIRECCIONES[0].CALLE2;
                                    txtReferencia.Text = oDireccionModificada.ListaDIRECCIONES[0].REFERENCIA;
                                    txtIdentificador.Text = oDireccionModificada.ListaDIRECCIONES[0].IDENTIFICADOR;
                                    txtManzana.Text = oDireccionModificada.ListaDIRECCIONES[0].MANZANA;
                                    txtLote.Text = oDireccionModificada.ListaDIRECCIONES[0].LOTE;
                                    txtCodigoPostal.Text = oDireccionModificada.ListaDIRECCIONES[0].CodigoPostal;
                                }
                            }
                            else
                            {
                                await DisplayAlert("Zona no disponible", "No existe servicio en el punto marcado", "Aceptar");
                            }
                        }
                    }
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
                await DisplayAlert("Aviso del sistema", "Los servicios de ubicacion no soportados por el dispositivo", "Aceptar");
            }
            catch (FeatureNotEnabledException)
            {
                await DisplayAlert("Ubicacion no activa", "Activa el GPS para obtener tu ubicacion", "Aceptar");
            }
            catch (PermissionException)
            {
                // Handle permission exception
                await DisplayAlert("Aviso", "Activa los permisos de ubicacion para continuar", "Aceptar");
            }
            catch (Exception)
            {
                // Unable to get location
                await DisplayAlert("Aviso", "No se puede obtener la ubicacion", "Aceptar");
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void btnGuardarEditar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdentificador.Text) && !string.IsNullOrEmpty(txtCalle.Text) && !string.IsNullOrEmpty(txtManzana.Text) && !string.IsNullOrEmpty(txtCodigoPostal.Text) && MypickerEstado.SelectedIndex != 0 && MypickerMunicipio.SelectedIndex != 0 && MypickerCiudad.SelectedIndex != 0 && MypickerColonia.SelectedIndex != 0)
            {
                try
                {
                    Guid UidPais = new Guid("afd6c3b7-f5be-40c9-8385-936d275a8d6b");

                    VMDireccion x = DireccionesListaEstados.Find(t => t.ESTADO == MypickerEstado.SelectedItem.ToString());
                    Guid UidEstado = x.ID;

                    VMDireccion a = DireccionesListaMunicipios.Find(t => t.MUNICIPIO == MypickerMunicipio.SelectedItem.ToString());
                    Guid UidMunicipio = a.ID;

                    VMDireccion b = DireccionesListaCiudad.Find(t => t.CIUDAD == MypickerCiudad.SelectedItem.ToString());
                    Guid UidCiudad = b.ID;

                    VMDireccion c = DireccionesListaColonia.Find(t => t.COLONIA == MypickerColonia.SelectedItem.ToString());
                    Guid UidColonia = c.ID;

                    string _Url;
                    if (oDireccion != null)
                    {
                        _Url = "" + Helpers.Settings.sitio + "/api/Direccion/GetActualizarDireccion?UidDireccion=" + oDireccion.ID.ToString() + "&UidPais=" + UidPais.ToString() + "&UidEstado=" + UidEstado.ToString() + "&UidMunicipio=" + UidMunicipio.ToString() + "&UidCiudad=" + UidCiudad.ToString() + "&UidColonia=" + UidColonia.ToString() + "&CallePrincipal=" + txtCalle.Text + "&CalleAux1=" + txtEntreCalle.Text + "&CalleAux2=" + txtYCalle.Text + "&Manzana=" + txtManzana.Text + "&Lote=" + txtLote.Text + "&CodigoPostal=" + txtCodigoPostal.Text + "&Referencia=" + txtReferencia.Text + "&NOMBRECIUDAD=s&NOMBRECOLONIA=s&Identificador=" + txtIdentificador.Text + "&Latitud=" + oDireccion.ListaDIRECCIONES[0].Latitud + "&Longitud=" + oDireccion.ListaDIRECCIONES[0].Longitud + "";

                        await DisplayAlert("Ubicación actualizada", "Actualización exitosa", "ok");
                    }
                    else
                    {
                        if (oDireccionModificada != null)
                        {
                            _Url = "" + Helpers.Settings.sitio + "/api/Direccion/GetGuardarDireccion?UidUsuario=" + App.Global1 + "&UidPais=" + UidPais.ToString() + "&UidEstado=" + UidEstado.ToString() + "&UidMunicipio=" + UidMunicipio.ToString() + "&UidCiudad=" + UidCiudad.ToString() + "&UidColonia=" + UidColonia.ToString() + "&CallePrincipal=" + txtCalle.Text + "&CalleAux1=" + txtEntreCalle.Text + "&CalleAux2=" + txtYCalle.Text + "&Manzana=" + txtManzana.Text + "&Lote=" + txtLote.Text + "&CodigoPostal=" + txtCodigoPostal.Text + "&Referencia=" + txtReferencia.Text + "&NOMBRECIUDAD=s&NOMBRECOLONIA=s&Identificador=" + txtIdentificador.Text + "&Latitud=" + oDireccionModificada.ListaDIRECCIONES[0].Latitud + "&Longitud=" + oDireccionModificada.ListaDIRECCIONES[0].Longitud + "";
                        }
                        else
                        {
                            _Url = "" + Helpers.Settings.sitio + "/api/Direccion/GetGuardarDireccion?UidUsuario=" + App.Global1 + "&UidPais=" + UidPais.ToString() + "&UidEstado=" + UidEstado.ToString() + "&UidMunicipio=" + UidMunicipio.ToString() + "&UidCiudad=" + UidCiudad.ToString() + "&UidColonia=" + UidColonia.ToString() + "&CallePrincipal=" + txtCalle.Text + "&CalleAux1=" + txtEntreCalle.Text + "&CalleAux2=" + txtYCalle.Text + "&Manzana=" + txtManzana.Text + "&Lote=" + txtLote.Text + "&CodigoPostal=" + txtCodigoPostal.Text + "&Referencia=" + txtReferencia.Text + "&NOMBRECIUDAD=s&NOMBRECOLONIA=s&Identificador=" + txtIdentificador.Text + "&Latitud=" + Latitud + "&Longitud=" + Longitud + "";
                        }
                        await DisplayAlert("Ubicación agregada", "Se ha agregado la ubicación " + txtIdentificador.Text + "", "ok");
                    }
                    using (HttpClient _client = new HttpClient())
                    {
                        var content = await _client.GetAsync(_Url);
                    }
                    _Url = ("" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + App.Global1);
                    using (HttpClient _client = new HttpClient())
                    {
                        string strDirecciones = await _client.GetStringAsync(_Url);
                        var obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                        App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
                    }

                    LVDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;

                    await Navigation.PopAsync();

                }
                catch (Exception)
                {
                    await DisplayAlert("Mensaje del sistema", "Agrege todos los datos", "ok");
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Agrege todos los datos requeridos", "Aceptar");
            }
        }
    }
}