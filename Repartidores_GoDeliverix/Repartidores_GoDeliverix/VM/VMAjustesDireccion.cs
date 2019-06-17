using GalaSoft.MvvmLight.Command;
using Repartidores_GoDeliverix.Modelo;
using Repartidores_GoDeliverix.Views.Popup;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Input;
using VistaDelModelo;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMAjustesDireccion : ControlsController
    {
        #region Propiedades de la clase
        #region Propiedades para los controles de la vista

        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { SetValue(ref _IsLoading, value); }
        }
        private int _IndexPaisSeleccionado;
        public int IndexPaisSeleccionado
        {
            get { return _IndexPaisSeleccionado; }
            set
            {
                SetValue(ref _IndexPaisSeleccionado, value);
                if (_IndexPaisSeleccionado > 0)
                {
                    ListaEstado = new List<Estado>();
                    VMDireccion MVDireccion = new VMDireccion();
                    UidPais = ListaPais[_IndexPaisSeleccionado].UidPais;
                    foreach (DataRow item in MVDireccion.Estados(UidPais).Rows)
                    {
                        ListaEstado.Add(new Estado()
                        {
                            UidEstado = new Guid(item["IdEstado"].ToString()),
                            NombreEstado = item["Nombre"].ToString()
                        });
                    }

                    ListaMunicipio.Add(new Municipio() { UidMunicipio = Guid.Empty, NombreMunicipio = "--Selecciona--" });
                    ListaCiudad.Add(new Ciudad() { UidCiudad = Guid.Empty, NombreCiudad = "--Selecciona--" });
                    ListaColonia.Add(new Colonia() { UidColonia = Guid.Empty, NombreColonia = "--Selecciona--" });
                }
                else
                {
                    ListaEstado.Add(new Estado() { UidEstado = Guid.Empty, NombreEstado = "--Selecciona--" });
                    ListaMunicipio.Add(new Municipio() { UidMunicipio = Guid.Empty, NombreMunicipio = "--Selecciona--" });
                    ListaCiudad.Add(new Ciudad() { UidCiudad = Guid.Empty, NombreCiudad = "--Selecciona--" });
                    ListaColonia.Add(new Colonia() { UidColonia = Guid.Empty, NombreColonia = "--Selecciona--" });
                }

            }
        }
        private int _IndexEstadoSeleccionado;
        public int IndexEstadoSeleccionado
        {
            get { return _IndexEstadoSeleccionado; }
            set
            {
                SetValue(ref _IndexEstadoSeleccionado, value);
                if (_IndexEstadoSeleccionado > 0)
                {
                    VMDireccion MVDireccion = new VMDireccion();
                    ListaCiudad = new List<Ciudad>();
                    ListaColonia = new List<Colonia>();
                    ListaMunicipio = new List<Municipio>();
                    UidEstado = ListaEstado[_IndexEstadoSeleccionado].UidEstado;
                    foreach (DataRow item in MVDireccion.Municipios(UidEstado).Rows)
                    {
                        ListaMunicipio.Add(new Municipio()
                        {
                            UidMunicipio = new Guid(item["IdMunicipio"].ToString()),
                            NombreMunicipio = item["Nombre"].ToString()
                        });
                    }

                    ListaCiudad.Add(new Ciudad() { UidCiudad = Guid.Empty, NombreCiudad = "--Selecciona--" });
                    ListaColonia.Add(new Colonia() { UidColonia = Guid.Empty, NombreColonia = "--Selecciona--" });
                }
            }
        }
        private int _IndexMunicipioSeleccionado;
        public int IndexMunicipioSeleccionado
        {
            get { return _IndexMunicipioSeleccionado; }
            set
            {
                SetValue(ref _IndexMunicipioSeleccionado, value);
                if (_IndexMunicipioSeleccionado > 0)
                {
                    VMDireccion MVDireccion = new VMDireccion();
                    ListaCiudad = new List<Ciudad>();
                    ListaColonia = new List<Colonia>();
                    UidMunicipio = ListaMunicipio[_IndexMunicipioSeleccionado].UidMunicipio;
                    foreach (DataRow item in MVDireccion.Ciudades(UidMunicipio).Rows)
                    {
                        ListaCiudad.Add(new Ciudad()
                        {
                            UidCiudad = new Guid(item["IdCiudad"].ToString()),
                            NombreCiudad = item["Nombre"].ToString()
                        });
                    }

                    ListaColonia.Add(new Colonia() { UidColonia = Guid.Empty, NombreColonia = "--Selecciona--" });
                }

            }
        }
        private int _IndexCiudadSeleccionado;
        public int IndexCiudadSeleccionado
        {
            get { return _IndexCiudadSeleccionado; }
            set
            {
                SetValue(ref _IndexCiudadSeleccionado, value);

                if (_IndexCiudadSeleccionado > 0)
                {
                    VMDireccion MVDireccion = new VMDireccion();
                    ListaColonia = new List<Colonia>();
                    UidCiudad = ListaCiudad[_IndexCiudadSeleccionado].UidCiudad;
                    foreach (DataRow item in MVDireccion.Colonias(UidCiudad).Rows)
                    {
                        ListaColonia.Add(new Colonia()
                        {
                            UidColonia = new Guid(item["IdColonia"].ToString()),
                            NombreColonia = item["Nombre"].ToString()
                        });
                    }

                }

            }
        }
        private int _IndexColoniaSeleccionado;
        public int IndexColoniaSeleccionado
        {
            get { return _IndexColoniaSeleccionado; }
            set
            {
                SetValue(ref _IndexColoniaSeleccionado, value);
                if (_IndexColoniaSeleccionado > 0)
                {
                    VMDireccion MVDireccion = new VMDireccion();
                    UidColonia = ListaColonia[_IndexColoniaSeleccionado].UidColonia;
                    CodigoPostal = MVDireccion.ObtenerCodigoPostal(UidColonia);

                }
            }
        }
        private string _NombreColonia;
        public string NombreColonia
        {
            get { return _NombreColonia; }
            set
            {
                SetValue(ref _NombreColonia, value);
            }
        }
        #endregion


        #region Propiedades de la direccion
        private Guid _UidPais;
        public Guid UidPais
        {
            get { return _UidPais; }
            set { SetValue(ref _UidPais, value); }
        }
        private Guid _UidEstado;
        public Guid UidEstado
        {
            get { return _UidEstado; }
            set { SetValue(ref _UidEstado, value); }
        }
        private Guid __UidMunicipio;
        public Guid UidMunicipio
        {
            get { return __UidMunicipio; }
            set { SetValue(ref __UidMunicipio, value); }
        }
        private Guid _UidCiudad;
        public Guid UidCiudad
        {
            get { return _UidCiudad; }
            set { SetValue(ref _UidCiudad, value); }
        }
        private Guid _UidColonia;
        public Guid UidColonia
        {
            get { return _UidColonia; }
            set { SetValue(ref _UidColonia, value); }
        }
        private Guid _UidDireccion;
        public Guid UidDireccion
        {
            get { return _UidDireccion; }
            set { SetValue(ref _UidDireccion, value); }
        }
        private string _CallePrincipal;
        public string CallePrincipal
        {
            get { return _CallePrincipal; }
            set { SetValue(ref _CallePrincipal, value); }
        }
        private string _CalleAux1;
        public string CalleAux1
        {
            get { return _CalleAux1; }
            set { SetValue(ref _CalleAux1, value); }
        }
        private string _CalleAux2;
        public string CalleAux2
        {
            get { return _CalleAux2; }
            set { SetValue(ref _CalleAux2, value); }
        }
        private string _Manzana;
        public string Manzana
        {
            get { return _Manzana; }
            set { SetValue(ref _Manzana, value); }
        }
        private string _Lote;
        public string Lote
        {
            get { return _Lote; }
            set { SetValue(ref _Lote, value); }
        }
        private string _CodigoPostal;
        public string CodigoPostal
        {
            get { return _CodigoPostal; }
            set { SetValue(ref _CodigoPostal, value); }
        }
        private string _Identificador;
        public string Identificador
        {
            get { return _Identificador; }
            set { SetValue(ref _Identificador, value); }
        }
        private string _Referencia;
        public string Referencia
        {
            get { return _Referencia; }
            set { SetValue(ref _Referencia, value); }
        }
        #endregion
        /// <summary>
        /// Cada propiedad en esta clase tiene su identifiador y su nombre par armar las listas
        /// </summary>
        #region Propiedades de Pais,Estado,Municipio,Ciudad,Colonia





        #endregion
        #region Listas 
        private List<Pais> _ListaPais;
        public List<Pais> ListaPais
        {
            get { return _ListaPais; }
            set { SetValue(ref _ListaPais, value); }
        }
        private List<Estado> _ListaEstado;
        public List<Estado> ListaEstado
        {
            get { return _ListaEstado; }
            set { SetValue(ref _ListaEstado, value); }
        }
        private List<Municipio> _ListaMunicipio;
        public List<Municipio> ListaMunicipio
        {
            get { return _ListaMunicipio; }
            set { SetValue(ref _ListaMunicipio, value); }
        }
        private List<Ciudad> _ListaCiudad;
        public List<Ciudad> ListaCiudad
        {
            get { return _ListaCiudad; }
            set { SetValue(ref _ListaCiudad, value); }
        }
        private List<Colonia> _ListaColonia;
        public List<Colonia> ListaColonia
        {
            get { return _ListaColonia; }
            set { SetValue(ref _ListaColonia, value); }
        }
        private List<VMAjustesDireccion> _ListaDirecciones;
        public List<VMAjustesDireccion> ListaDirecciones
        {
            get { return _ListaDirecciones; }
            set { SetValue(ref _ListaDirecciones, value); }
        }
        #endregion
        #endregion
        #region Comandos
        public ICommand DeleteAddress { get { return new RelayCommand(EliminaDireccion); } }
        public ICommand EditAddress { get { return new RelayCommand(EditaDireccion); } }
        public ICommand SaveAddress { get { return new RelayCommand(GuardaDireccion); } }
        public ICommand AddAddress { get { return new RelayCommand(AgregaDireccion); } }
        public ICommand IsReloading { get { return new RelayCommand(RecargarDirecciones); } }

        private void RecargarDirecciones()
        {
            throw new NotImplementedException();
        }
        private void GuardaDireccion()
        {
            var AppInstance = MainViewModel.GetInstance();
            VMDireccion MVDireccion = new VMDireccion();
            Guid UidUsuario = AppInstance.Session_.UidUsuario;
            MVDireccion.ObtenerDireccionesUsuario(UidUsuario.ToString());
            if (UidDireccion == Guid.Empty)
            {
                Guid UidDireccion = Guid.NewGuid();
                MVDireccion.AgregaDireccionALista(UidDireccion,UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, CallePrincipal, CalleAux1, CalleAux2, Manzana, Lote, CodigoPostal, Referencia, "", "", Identificador);

            }
            else
            {

                MVDireccion.ActualizaListaDireccion(UidDireccion.ToString(), UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, CallePrincipal, CalleAux1, CalleAux2, Manzana, Lote, CodigoPostal, Referencia, Identificador, "", "");
            }
            MVDireccion.GuardaListaDeDirecciones(MVDireccion.ListaDIRECCIONES, UidUsuario, "asp_AgregaDireccionUsuario", "Usuario");
            AppInstance.MVAjustes.Recargar();
        }
        public async void AgregaDireccion()
        {
            var AppInstance = MainViewModel.GetInstance();
            AppInstance.vmAjustesDireccion = new VMAjustesDireccion();
            AppInstance.vmAjustesDireccion.ListaPais = new List<Pais>();
            AppInstance.vmAjustesDireccion.ListaEstado = new List<Estado>();
            AppInstance.vmAjustesDireccion.ListaMunicipio = new List<Municipio>();
            AppInstance.vmAjustesDireccion.ListaCiudad = new List<Ciudad>();
            AppInstance.vmAjustesDireccion.ListaColonia = new List<Colonia>();
            //Datos generales
            AppInstance.vmAjustesDireccion.UidDireccion = Guid.Empty;
            AppInstance.vmAjustesDireccion.CallePrincipal = string.Empty;
            AppInstance.vmAjustesDireccion.CalleAux1 = string.Empty;
            AppInstance.vmAjustesDireccion.CalleAux2 = string.Empty;
            AppInstance.vmAjustesDireccion.Manzana = string.Empty;
            AppInstance.vmAjustesDireccion.Lote = string.Empty;
            AppInstance.vmAjustesDireccion.CodigoPostal = string.Empty;
            AppInstance.vmAjustesDireccion.Referencia = string.Empty;
            AppInstance.vmAjustesDireccion.Identificador = string.Empty;

            VMDireccion MVDireccion = new VMDireccion();
            //Alimenta lista de Pais
            foreach (DataRow item in MVDireccion.Paises().Rows)
            {
                if (item["UidPais"] != null && (new Guid(item["UidPais"].ToString()) == Guid.Empty || new Guid(item["UidPais"].ToString()) != Guid.Empty))
                {
                    AppInstance.vmAjustesDireccion.ListaPais.Add(new Pais()
                    {
                        UidPais = new Guid(item["UidPais"].ToString()),
                        NombrePais = item["Nombre"].ToString()
                    });
                }

            }
            AppInstance.vmAjustesDireccion.ListaEstado.Add(new Estado() { UidEstado = Guid.Empty, NombreEstado = "--Selecciona--" });
            AppInstance.vmAjustesDireccion.ListaMunicipio.Add(new Municipio() { UidMunicipio = Guid.Empty, NombreMunicipio = "--Selecciona--" });
            AppInstance.vmAjustesDireccion.ListaCiudad.Add(new Ciudad() { UidCiudad = Guid.Empty, NombreCiudad = "--Selecciona--" });
            AppInstance.vmAjustesDireccion.ListaColonia.Add(new Colonia() { UidColonia = Guid.Empty, NombreColonia = "--Selecciona--" });
            //Datos del control
            AppInstance.vmAjustesDireccion.IndexPaisSeleccionado = 0;
            AppInstance.vmAjustesDireccion.IndexEstadoSeleccionado = 0;
            AppInstance.vmAjustesDireccion.IndexMunicipioSeleccionado = 0;
            AppInstance.vmAjustesDireccion.IndexCiudadSeleccionado = 0;
            AppInstance.vmAjustesDireccion.IndexColoniaSeleccionado = 0;
            //Levanta a la ventana modal
            await PopupNavigation.Instance.PushAsync(new Ajustes_DetalleDireccion());
        }
        private void EliminaDireccion()
        {
            var AppInstance = MainViewModel.GetInstance();
            Guid UidUsuario = AppInstance.Session_.UidUsuario;
            VMDireccion MVDireccion = new VMDireccion();
            MVDireccion.ObtenerDireccionesUsuario(UidUsuario.ToString());
            MVDireccion.QuitaDireeccionDeLista(UidDireccion.ToString());
            MVDireccion.GuardaListaDeDirecciones(MVDireccion.ListaDIRECCIONES, UidUsuario, "asp_AgregaDireccionUsuario", "Usuario");
            AppInstance.MVAjustes.Recargar();
        }
        private async void EditaDireccion()
        {
            var AppInstance = MainViewModel.GetInstance();
            AppInstance.vmAjustesDireccion = new VMAjustesDireccion();
            AppInstance.vmAjustesDireccion.ListaPais = new List<Pais>();
            AppInstance.vmAjustesDireccion.ListaEstado = new List<Estado>();
            AppInstance.vmAjustesDireccion.ListaMunicipio = new List<Municipio>();
            AppInstance.vmAjustesDireccion.ListaCiudad = new List<Ciudad>();
            AppInstance.vmAjustesDireccion.ListaColonia = new List<Colonia>();
            //Datos generales
            AppInstance.vmAjustesDireccion.UidDireccion = UidDireccion;
            AppInstance.vmAjustesDireccion.CallePrincipal = CallePrincipal;
            AppInstance.vmAjustesDireccion.CalleAux1 = CalleAux1;
            AppInstance.vmAjustesDireccion.CalleAux2 = CalleAux2;
            AppInstance.vmAjustesDireccion.Manzana = Manzana;
            AppInstance.vmAjustesDireccion.Lote = Lote;
            AppInstance.vmAjustesDireccion.CodigoPostal = CodigoPostal;
            AppInstance.vmAjustesDireccion.Referencia = Referencia;
            AppInstance.vmAjustesDireccion.Identificador = Identificador;

            VMDireccion MVDireccion = new VMDireccion();
            //Alimenta lista de Pais
            foreach (DataRow item in MVDireccion.Paises().Rows)
            {
                if (item["UidPais"] != null && (new Guid(item["UidPais"].ToString()) == Guid.Empty || new Guid(item["UidPais"].ToString()) != Guid.Empty))
                {
                    AppInstance.vmAjustesDireccion.ListaPais.Add(new Pais()
                    {
                        UidPais = new Guid(item["UidPais"].ToString()),
                        NombrePais = item["Nombre"].ToString()
                    });
                }

            }
            //Datos del control
            AppInstance.vmAjustesDireccion.IndexPaisSeleccionado = AppInstance.vmAjustesDireccion.ListaPais.FindIndex(p => p.UidPais == UidPais); ;
            AppInstance.vmAjustesDireccion.IndexEstadoSeleccionado = AppInstance.vmAjustesDireccion.ListaEstado.FindIndex(E => E.UidEstado == UidEstado);
            AppInstance.vmAjustesDireccion.IndexMunicipioSeleccionado = AppInstance.vmAjustesDireccion.ListaMunicipio.FindIndex(M => M.UidMunicipio == UidMunicipio);
            AppInstance.vmAjustesDireccion.IndexCiudadSeleccionado = AppInstance.vmAjustesDireccion.ListaCiudad.FindIndex(C => C.UidCiudad == UidCiudad);
            AppInstance.vmAjustesDireccion.IndexColoniaSeleccionado = AppInstance.vmAjustesDireccion.ListaColonia.FindIndex(C => C.UidColonia == UidColonia);
            //Levanta a la ventana modal
            await PopupNavigation.Instance.PushAsync(new Ajustes_DetalleDireccion());
        }
        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }

        #endregion
        public VMAjustesDireccion()
        {
        }
    }
}
