﻿using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repartidores_GoDeliverix.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using VistaDelModelo;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMAjustes : ControlsController
    {

        string url = "";

        /// <summary>
        /// Atributos para el popup de los ajustes generales
        /// </summary>
        private string _strNombre;
        public string StrNombre
        {
            get { return _strNombre; }
            set { SetValue(ref _strNombre, value); }
        }
        private string _StrApellidoPaterno;

        public string StrApellidoPaterno
        {
            get { return _StrApellidoPaterno; }
            set { SetValue(ref _StrApellidoPaterno, value); }
        }
        private string _StrApellidoMaterno;

        public string StrApellidoMaterno
        {
            get { return _StrApellidoMaterno; }
            set { SetValue(ref _StrApellidoMaterno, value); }
        }
        private string _strFechaDeNacimiento;

        public string StrFechaDeNacimiento
        {
            get { return _strFechaDeNacimiento; }
            set { SetValue(ref _strFechaDeNacimiento, value); }
        }

        private string _strCorreoElectronico;

        //Datos de correo electronico
        public string StrCorreoElectronico
        {
            get { return _strCorreoElectronico; }
            set { SetValue(ref _strCorreoElectronico, value); }
        }

        private ObservableCollection<VMAjustesItem> _lsAjustes;

        public ObservableCollection<VMAjustesItem> LsAjustes
        {
            get { return _lsAjustes; }
            set { SetValue(ref _lsAjustes, value); }
        }

        private ObservableCollection<VMAjustesTelefono> _lsTelefono;

        public ObservableCollection<VMAjustesTelefono> LsTelefono
        {
            get { return _lsTelefono; }
            set { SetValue(ref _lsTelefono, value); }
        }
        private ObservableCollection<VMAjustesDireccion> _ListaDireccion;

        public ObservableCollection<VMAjustesDireccion> ListaDireccion
        {
            get { return _ListaDireccion; }
            set { SetValue(ref _ListaDireccion, value); }
        }

        private bool _IsLoading;
        public string ModuloACambiar { get; set; }
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { SetValue(ref _IsLoading, value); }
        }

        #region Propiedades de controles
        //Colores
        private Color _CGeneral;
        public Color CGeneral
        {
            get { return _CGeneral; }
            set { SetValue(ref _CGeneral, value); }
        }
        private Color _CTelefono;
        public Color CTelefono
        {
            get { return _CTelefono; }
            set { SetValue(ref _CTelefono, value); }
        }
        private Color _CDireccion;
        public Color CDireccion
        {
            get { return _CDireccion; }
            set { SetValue(ref _CDireccion, value); }
        }
        //Visibilidad de paneles
        private bool _BGeneral;
        public bool BGeneral
        {
            get { return _BGeneral; }
            set { SetValue(ref _BGeneral, value); }
        }
        private bool _BTelefono;
        public bool BTelefono
        {
            get { return _BTelefono; }
            set { SetValue(ref _BTelefono, value); }
        }
        private bool _BDireccion;
        public bool BDireccion
        {
            get { return _BDireccion; }
            set { SetValue(ref _BDireccion, value); }
        }
        public ICommand oPanelGeneral { get { return new RelayCommand(Panelgeneral); } }

        public ICommand oPanelTelefono { get { return new RelayCommand(Paneltelefono); } }
        public ICommand oPanelDireccion { get { return new RelayCommand(Paneldireccion); } }
        private void Paneltelefono()
        {
            muestraPanel("Telefono");
        }
        private void Paneldireccion()
        {
            muestraPanel("Direccion");
        }
        private void Panelgeneral()
        {
            muestraPanel("General");
        }

        protected void muestraPanel(string Panel)
        {
            CTelefono = Color.Gray;
            CDireccion = Color.Gray;
            CGeneral = Color.Gray;

            BGeneral = false;
            BTelefono = false;
            BDireccion = false;
            switch (Panel)
            {
                case "General":
                    CGeneral = Color.Blue;
                    BGeneral = true;
                    break;
                case "Telefono":
                    CTelefono = Color.Blue;
                    BTelefono = true;
                    break;
                case "Direccion":
                    CDireccion = Color.Blue;
                    BDireccion = true;
                    break;
                default:
                    break;
            }
        }
        #endregion

        public VMAjustes()
        {
            muestraPanel("General");
            Obtendatos();
        }
        public ICommand IsReloading { get { return new RelayCommand(Recargar); } }
        public ICommand IsSavingName { get { return new RelayCommand(GuardarUsuario); } }
        public ICommand IsSavingMail { get { return new RelayCommand(GuardarUsuario); } }
        public ICommand IsSavingBirthday { get { return new RelayCommand(GuardarUsuario); } }
        public ICommand IsSavingPhone { get { return new RelayCommand(GuardarUsuario); } }
        public ICommand AddNumber { get { return new RelayCommand(AgregarTelefono); } }
        public ICommand AddAddres { get { return new RelayCommand(AgregarDireccion); } }

        private void AgregarDireccion()
        {
            var AppInstance = MainViewModel.GetInstance();
            AppInstance.vmAjustesDireccion = new VMAjustesDireccion();
            AppInstance.vmAjustesDireccion.AgregaDireccion();
        }

        private void AgregarTelefono()
        {
            var AppInstance = MainViewModel.GetInstance();
            AppInstance.MVAjustesTelefono = new VMAjustesTelefono();
            AppInstance.MVAjustesTelefono.AgregarTelefono();
        }

        private void GuardarUsuario()
        {
            switch (ModuloACambiar)
            {
                case "Nombre":
                    ActualizaUsuario(Nombre: StrNombre, ApellidoMaterno: StrApellidoMaterno, ApellidoPaterno: StrApellidoPaterno);
                    break;
                case "Fecha de nacimiento":
                    ActualizaUsuario(fnacimiento: StrFechaDeNacimiento);
                    break;
                case "Correo electronico":
                    ActualizaCorreo();
                    break;
                case "Telefonos":
                    ActualizaTelefonos();
                    break;
            }
            Recargar();
        }

        public async void ActualizaUsuario(string Nombre = "", string ApellidoPaterno = "", string ApellidoMaterno = "", string fnacimiento = "")
        {
            var AppInstance = MainViewModel.GetInstance();
            string UidUsuario = AppInstance.Session_.UidUsuario.ToString();
            string PerfilDeUsuario = "DFC29662-0259-4F6F-90EA-B24E39BE4346";
            using (var _webApi = new HttpClient())
            {
                string url = "";
                try
                {
                    if (!string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(ApellidoPaterno) && !string.IsNullOrEmpty(ApellidoMaterno))
                    {
                        url = "" + settings.Sitio + "api/Usuario/GetActualizarUsuario?UidUsuario=" + UidUsuario + "&Nombre=" + Nombre + "&ApellidoPaterno=" + ApellidoPaterno + "&ApellidoMaterno=" + ApellidoMaterno + "&perfil=" + PerfilDeUsuario + "";
                    }
                    if (!string.IsNullOrEmpty(fnacimiento))
                    {
                        url = "" + settings.Sitio + "api/Usuario/GetActualizarUsuario?UidUsuario=" + UidUsuario + "&fnacimiento=" + fnacimiento + "&perfil=" + PerfilDeUsuario + "";
                    }
                    await _webApi.GetAsync(url);
                }
                catch (Exception e)
                {
                    GenerateMessage("Aviso del sistema", "GuardaUsuario " + e.Message, "ok");
                }
            }
        }
        public void ActualizaCorreo()
        {
            var AppInstance = MainViewModel.GetInstance();
            string UidUsuario = AppInstance.Session_.UidUsuario.ToString();
            using (var _webApi = new HttpClient())
            {
                try
                {
                    url = "" + settings.Sitio + "api/CorreoElectronico/GetActualizarCorreo?UidPropietario=" + UidUsuario + "&strParametroDeInsercion=Usuario&strCorreoElectronico=" + StrCorreoElectronico + "&UidCorreoElectronico=" + Guid.NewGuid() + "";
                    _webApi.GetAsync(url);
                }
                catch (Exception e)
                {
                    GenerateMessage("Aviso del sistema", "Actualiza correo " + e.Message, "ok");
                }
            }
        }
        private void ActualizaTelefonos()
        {
            using (var _webApi = new HttpClient())
            {
                try
                {
                    var AppInstance = MainViewModel.GetInstance();
                    string UidUsuario = AppInstance.Session_.UidUsuario.ToString();
                    url = "" + settings.Sitio + "api/Telefono/GetActualizaTelefonoApi?UidPropietario=" + UidUsuario + "&strParametroDeInsercion=Usuario&strCorreoElectronico=" + StrCorreoElectronico + "&UidCorreoElectronico=" + Guid.NewGuid() + "";
                    _webApi.GetAsync(url);
                }
                catch (Exception e)
                {
                    GenerateMessage("Aviso del sistema", "Actualiza telefono " + e.Message, "ok");
                }

            }
        }
        public void Recargar()
        {
            Obtendatos();
        }

        protected async void Obtendatos()
        {
            try
            {
                IsLoading = true;
                var AppInstance = MainViewModel.GetInstance();
                Guid uidUsuario = AppInstance.Session_.UidUsuario;
                //Declaracion de las vistas del modelo 
                VMUsuarios MVUsuario = new VMUsuarios();
                //Obtiene los datos
                using (var _webapi = new HttpClient())
                {
                    try
                    {
                        string uril = "" + settings.Sitio + "api/Usuario/GetBuscarUsuarios?UidUsuario=" + uidUsuario + "&UIDPERFIL=DFC29662-0259-4F6F-90EA-B24E39BE4346";
                        string content = await _webapi.GetStringAsync(uril);
                        var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        MVUsuario = JsonConvert.DeserializeObject<VMUsuarios>(obj);
                    }
                    catch (Exception e)
                    {
                        GenerateMessage("Aviso del sistema", "Usuario" + e.Message, "Ok");
                    }

                }

                //Datos del usuario
                AppInstance.Nombre = MVUsuario.StrNombre;
                StrNombre = MVUsuario.StrNombre;
                StrApellidoMaterno = MVUsuario.StrApellidoPaterno;
                StrApellidoPaterno = MVUsuario.StrApellidoMaterno;
                this.StrFechaDeNacimiento = DateTime.Parse(MVUsuario.DtmFechaDeNacimiento).ToString();
                //Obtiene el correo electronico
                using (var _webApi = new HttpClient())
                {
                    try
                    {
                        string uril = "" + settings.Sitio + "api/CorreoElectronico/GetBuscarCorreo?UidPropietario=" + uidUsuario + "&strParametroDebusqueda=Usuario";
                        string content = await _webApi.GetStringAsync(uril);
                        string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        VMCorreoElectronico MVCorreoElectronico = JsonConvert.DeserializeObject<VMCorreoElectronico>(obj);
                        StrCorreoElectronico = MVCorreoElectronico.CORREO;
                    }
                    catch (Exception e)
                    {
                        GenerateMessage("Aviso del sistema", "Correo " + e.Message, "ok");
                    }
                }
                //Obtiene los telefonos 
                using (var _WebApi = new HttpClient())
                {
                    try
                    {
                        url = "" + settings.Sitio + "api/Telefono/GetBuscarTelefonos?UidPropietario=" + uidUsuario + "&ParadetroDeBusqueda=Usuario";
                        string content = await _WebApi.GetStringAsync(url);
                        var inf = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        VMTelefono MVTelefono = JsonConvert.DeserializeObject<VMTelefono>(inf);
                        LsTelefono = new ObservableCollection<VMAjustesTelefono>();
                        foreach (VMTelefono item in MVTelefono.ListaDeTelefonos)
                        {
                            LsTelefono.Add(new VMAjustesTelefono()
                            {
                                UidTelefono = item.ID,
                                intNumeroTelefono = item.NUMERO,
                                StrTipoDeTelefono = item.StrNombreTipoDeTelefono,
                                UidTipoDeTelefono = item.UidTipo
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        GenerateMessage("Aviso del sistema", "telefono " + e.Message, "ok");
                    }
                }

                using (var _Api = new HttpClient())
                {
                    try
                    {
                        //Obtiene las direcciones
                        url = "" + settings.Sitio + "api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + uidUsuario + "";
                        string content = await _Api.GetStringAsync(url);
                        var Informacion = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        VMDireccion MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(Informacion);
                        //Asignacion de variables locales
                        ListaDireccion = new ObservableCollection<VMAjustesDireccion>();
                        foreach (VMDireccion item in MVDireccion.ListaDIRECCIONES)
                        {
                            ListaDireccion.Add(new VMAjustesDireccion()
                            {
                                UidDireccion = item.ID,
                                UidPais = new Guid(item.PAIS),
                                UidEstado = new Guid(item.ESTADO),
                                UidMunicipio = new Guid(item.MUNICIPIO),
                                UidCiudad = new Guid(item.CIUDAD),
                                UidColonia = new Guid(item.COLONIA),
                                CallePrincipal = item.CALLE0,
                                CalleAux1 = item.CALLE1,
                                CalleAux2 = item.CALLE2,
                                Manzana = item.MANZANA,
                                Lote = item.LOTE,
                                CodigoPostal = item.CodigoPostal,
                                Referencia = item.REFERENCIA,
                                Identificador = item.IDENTIFICADOR,
                                NombreColonia = item.NOMBRECOLONIA
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        GenerateMessage("Aviso del sistema", "direccion " + e.Message, "ok");
                    }

                }
                LsAjustes = new ObservableCollection<VMAjustesItem>
                {
                    new VMAjustesItem() { StrRuta = "Nombre.png", Titulo = "Nombre", Detalles = StrNombre + " " + StrApellidoMaterno + " " + StrApellidoPaterno },
                    new VMAjustesItem() { StrRuta = "Usuario.png", Titulo = "Usuario", Detalles = MVUsuario.StrUsuario },
                    new VMAjustesItem() { StrRuta = "FechaDeNacimiento.png", Titulo = "Fecha de nacimiento", Detalles = StrFechaDeNacimiento },
                    new VMAjustesItem() { StrRuta = "Mail.png", Titulo = "Correo electronico", Detalles = StrCorreoElectronico },
                    //LsAjustes.Add(new VMAjustesItem() { StrRuta = "Telefono.png", Titulo = "Telefonos", Detalles = LsTelefono.Count.ToString() });
                    //LsAjustes.Add(new VMAjustesItem() { StrRuta = "Direccion.png", Titulo = "Direcciones", Detalles = ListaDireccion.Count.ToString() });
                    new VMAjustesItem() { StrRuta = "Salida.png", Titulo = "Cerrar sesion", Detalles = "" }
                };
                IsLoading = false;
            }
            catch (Exception e)
            {
                GenerateMessage("Aviso del sistema", "Mensaje " + e.Message, "Ok");
            }
        }

        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }

    }
}
