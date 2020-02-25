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
        //double Longitud1 = 0;
        //double Latitud1 = 0;
        //Button Button;
        //Label IDDireccionBusqueda;
        ListView LVDirecciones;
        Xamarin.Forms.GoogleMaps.Position MyPosicion;
        HttpClient _client = new HttpClient();

        //Crea registro
        public DireccionModificar(Xamarin.Forms.GoogleMaps.Position MyPosicion, ListView LvDireccion)
        {
            InitializeComponent();
            this.MyPosicion = MyPosicion;
            MIDireccion();
            Latitud = MyPosicion.Latitude.ToString();
            Longitud = MyPosicion.Longitude.ToString();
            LVDirecciones = LvDireccion;
        }
        //actualiza registro
        public DireccionModificar(Xamarin.Forms.GoogleMaps.Position MyPosicion, VMDireccion objDireccion, ListView ListDirecciones)
        {
            InitializeComponent();
            this.MyPosicion = MyPosicion;
            MIDireccion();
            txtID.Text = objDireccion.ID.ToString();
            Latitud = MyPosicion.Latitude.ToString();
            Longitud = MyPosicion.Longitude.ToString();
            LVDirecciones = ListDirecciones;

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
        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(App.Global1) && string.IsNullOrEmpty(Helpers.Settings.UidDireccion))
            {
                App.MVDireccion.ListaDIRECCIONES.Clear();
            }
            else
            {
                string _Url = ("" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + App.Global1);
                string strDirecciones = await _client.GetStringAsync(_Url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
            }
            await Navigation.PopToRootAsync();
        }

        private void BtnGuardarEditar_Clicked(object sender, EventArgs e)
        {
            GuardarDireccion();
        }

        public async void MIDireccion()
        {
            try
            {
                if (string.IsNullOrEmpty(App.Global1))
                {
                    if (App.MVDireccion.ListaDIRECCIONES.Count == 1)
                    {
                        if (string.IsNullOrEmpty(Helpers.Settings.UidDireccion))
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
                            txtCalle.Text = App.MVDireccion.ListaDIRECCIONES[0].CALLE0;
                            txtEntreCalle.Text = App.MVDireccion.ListaDIRECCIONES[0].CALLE1;
                            txtYCalle.Text = App.MVDireccion.ListaDIRECCIONES[0].CALLE2;
                            txtReferencia.Text = App.MVDireccion.ListaDIRECCIONES[0].REFERENCIA;
                            txtIdentificador.Text = App.MVDireccion.ListaDIRECCIONES[0].IDENTIFICADOR;
                            txtManzana.Text = App.MVDireccion.ListaDIRECCIONES[0].MANZANA;
                            txtLote.Text = App.MVDireccion.ListaDIRECCIONES[0].LOTE;
                            txtCodigoPostal.Text = App.MVDireccion.ListaDIRECCIONES[0].CodigoPostal;
                        }
                        else
                        {
                            txtCalle.Text = Helpers.Settings.StrCALLE0;
                            txtEntreCalle.Text = Helpers.Settings.StrCALLE1;
                            txtYCalle.Text = Helpers.Settings.StrCALLE2;
                            txtReferencia.Text = Helpers.Settings.StrREFERENCIA;
                            txtIdentificador.Text = Helpers.Settings.StrIDENTIFICADOR;
                            txtManzana.Text = Helpers.Settings.StrMANZANA;
                            txtLote.Text = Helpers.Settings.StrLOTE;
                            txtCodigoPostal.Text = Helpers.Settings.StrCodigoPostal;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Aviso", "No se pudo cargar tu direccion desde tu posicion", "Aceptar");
                    }
                }
                else
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
                    txtCalle.Text = App.MVDireccion.ListaDIRECCIONES[0].CALLE0;
                    txtEntreCalle.Text = App.MVDireccion.ListaDIRECCIONES[0].CALLE1;
                    txtYCalle.Text = App.MVDireccion.ListaDIRECCIONES[0].CALLE2;
                    txtReferencia.Text = App.MVDireccion.ListaDIRECCIONES[0].REFERENCIA;
                    txtIdentificador.Text = App.MVDireccion.ListaDIRECCIONES[0].IDENTIFICADOR;
                    txtManzana.Text = App.MVDireccion.ListaDIRECCIONES[0].MANZANA;
                    txtLote.Text = App.MVDireccion.ListaDIRECCIONES[0].LOTE;
                    txtCodigoPostal.Text = App.MVDireccion.ListaDIRECCIONES[0].CodigoPostal;
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Aviso", e.Message, "Aceptar");
            }
        }

        public async void GuardarDireccion()
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
                    if (App.Global1 == string.Empty)
                    {
                        Helpers.Settings.UidDireccion = Guid.NewGuid().ToString();
                        Helpers.Settings.StrPAIS = UidPais.ToString();
                        Helpers.Settings.StrESTADO = UidEstado.ToString();
                        Helpers.Settings.StrMUNICIPIO = UidMunicipio.ToString();
                        Helpers.Settings.StrCIUDAD = UidCiudad.ToString();
                        Helpers.Settings.StrCOLONIA = UidColonia.ToString();
                        Helpers.Settings.StrCALLE0 = txtCalle.Text;
                        Helpers.Settings.StrCALLE1 = txtEntreCalle.Text;
                        Helpers.Settings.StrCALLE2 = txtYCalle.Text;
                        Helpers.Settings.StrCodigoPostal = txtCodigoPostal.Text;
                        Helpers.Settings.StrMANZANA = txtManzana.Text;
                        Helpers.Settings.StrLOTE = txtLote.Text;
                        Helpers.Settings.StrREFERENCIA = txtReferencia.Text;
                        Helpers.Settings.StrIDENTIFICADOR = txtIdentificador.Text;
                        Helpers.Settings.StrLongitud = Longitud;
                        Helpers.Settings.StrLatitud = Latitud;
                        Helpers.Settings.StrNombreCiudad = b.CIUDAD;
                        Helpers.Settings.StrNombreColonia = c.COLONIA;

                        App.MVDireccion.ListaDIRECCIONES = new List<VMDireccion>();
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
                    else
                    {
                        string _Url = "" + Helpers.Settings.sitio + "/api/Direccion/GetGuardarDireccion?UidUsuario=" + App.Global1 + "&UidPais=" + UidPais.ToString() + "&UidEstado=" + UidEstado.ToString() + "&UidMunicipio=" + UidMunicipio.ToString() + "&UidCiudad=" + UidCiudad.ToString() + "&UidColonia=" + UidColonia.ToString() + "&CallePrincipal=" + txtCalle.Text + "&CalleAux1=" + txtEntreCalle.Text + "&CalleAux2=" + txtYCalle.Text + "&Manzana=" + txtManzana.Text + "&Lote=" + txtLote.Text + "&CodigoPostal=" + txtCodigoPostal.Text + "&Referencia=" + txtReferencia.Text + "&NOMBRECIUDAD=s&NOMBRECOLONIA=s&Identificador=" + txtIdentificador.Text + "&Latitud=" + Latitud + "&Longitud=" + Longitud + "";
                        var content = await _client.GetAsync(_Url);
                        _Url = ("" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + App.Global1);
                        string strDirecciones = await _client.GetStringAsync(_Url);
                        var obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                        App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
                    }
                    LVDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
                    await Navigation.PopToRootAsync();
                    txtID.Text = null;
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