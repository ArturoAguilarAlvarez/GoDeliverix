using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repartidores_GoDeliverix.Helpers;
using Repartidores_GoDeliverix.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using VistaDelModelo;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMHomeOrden : ControlsController
    {


        #region Variables de la Api
        string url = "";
        #endregion

        #region Informacion de la sucursal de la orden

        private string _StrEmpresa;

        public string StrEmpresaNombreComercial
        {
            get { return _StrEmpresa; }
            set { SetValue(ref _StrEmpresa, value); }
        }
        #endregion
        public string StrUidOrden { get; set; }
        private Guid _UidOrdenTarifario;

        public Guid UidOrdenTarifario
        {
            get { return _UidOrdenTarifario; }
            set { SetValue(ref _UidOrdenTarifario, value); }
        }

        private Guid _UidordenRepartidor;

        public Guid UidordenRepartidor
        {
            get { return _UidordenRepartidor; }
            set { SetValue(ref _UidordenRepartidor, value); }
        }

        #region Propiedades
        //Vistas del modelo
        protected VMOrden MVOrden;
        protected VMDireccion MVDireccion;
        protected VMUsuarios MVUsuario;
        protected VMTelefono MVTelefono;
        protected VMSucursales MVSucursal;
        protected VMEmpresas MVEmpresa;
        protected VMAcceso mVAcceso;
        protected VMUbicacion MVUbicacion;

        private Guid _UidOrden;

        public Guid UidOrden
        {
            get { return _UidOrden; }
            set { SetValue(ref _UidOrden, value); }
        }

        private Guid _UidEstatus;

        public Guid UidEstatus
        {
            get { return _UidEstatus; }
            set { SetValue(ref _UidEstatus, value); }
        }

        private string _StrIdentificador;

        public string StrIdentificadorSucursal
        {
            get { return _StrIdentificador; }
            set { SetValue(ref _StrIdentificador, value); }
        }
        private long _LngFolio;

        public long LngFolio
        {
            get { return _LngFolio; }
            set { SetValue(ref _LngFolio, value); }
        }


        #region Propiedades del producto
        private Guid _UidProducto;

        public Guid UidProducto
        {
            get { return _UidProducto; }
            set { SetValue(ref _UidProducto, value); }
        }
        private Guid _UidProductoEnOrden;

        public Guid UidProductoEnOrden
        {
            get { return _UidProductoEnOrden; }
            set { SetValue(ref _UidProductoEnOrden, value); }
        }
        private Guid _UidSucursalSuministradora;

        public Guid UidSucursalSuministradora
        {
            get { return _UidSucursalSuministradora; }
            set { SetValue(ref _UidSucursalSuministradora, value); }
        }
        private string _StrNombreProducto;

        public string StrNombreProducto
        {
            get { return _StrNombreProducto; }
            set { SetValue(ref _StrNombreProducto, value); }
        }
        private int _intCantidad;

        public int IntCantidad
        {
            get { return _intCantidad; }
            set { SetValue(ref _intCantidad, value); }
        }
        private decimal _MTotal;

        public decimal MTotal
        {
            get { return _MTotal; }
            set { SetValue(ref _MTotal, value); }
        }
        private decimal _MSTotal;

        public decimal MSTotal
        {
            get { return _MSTotal; }
            set { SetValue(ref _MSTotal, value); }
        }

        private decimal _MPropina;

        public decimal MPropina
        {
            get { return _MPropina; }
            set { SetValue(ref _MPropina, value); }
        }
        private decimal _MSubTotal;

        public decimal MSubTotal
        {
            get { return _MSubTotal; }
            set { SetValue(ref _MSubTotal, value); }
        }
        private decimal _MTotalPropina;

        public decimal MTotalConPropina
        {
            get { return _MTotalPropina; }
            set { SetValue(ref _MTotalPropina, value); }
        }
        private decimal _MTotalTarifario;

        public decimal MTotalTarifario
        {
            get { return _MTotalTarifario; }
            set { SetValue(ref _MTotalTarifario, value); }
        }

        private List<Productos> _ListaProductos;

        public List<Productos> ListaProductos
        {
            get { return _ListaProductos; }
            set { SetValue(ref _ListaProductos, value); }
        }


        private string _StrCodigo;

        public string StrCodigo
        {
            get { return _StrCodigo; }
            set { SetValue(ref _StrCodigo, value); }
        }
        private string _StrNota;

        public string StrNota
        {
            get { return _StrNota; }
            set { SetValue(ref _StrNota, value); }
        }
        #endregion

        #region Propiedades de los puntos de direccion
        private Guid _UidSucursal;

        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { SetValue(ref _UidSucursal, value); }
        }

        private string _UidDireccionDelCliente;

        public string UidDireccionDelCliente
        {
            get { return _UidDireccionDelCliente; }
            set { SetValue(ref _UidDireccionDelCliente, value); }
        }

        private string _StrColoniaSucursal;

        public string StrColoniaSucursal
        {
            get { return _StrColoniaSucursal; }
            set { SetValue(ref _StrColoniaSucursal, value); }
        }
        private string _StrColoniaCliente;

        public string StrColoniaCliente
        {
            get { return _StrColoniaCliente; }
            set { SetValue(ref _StrColoniaCliente, value); }
        }
        private string _StrLatitudSucursal;


        public string StrLatitudSucursal
        {
            get { return _StrLatitudSucursal; }
            set { SetValue(ref _StrLatitudSucursal, value); }
        }
        private string _StrLatitudCliente;

        public string StrLatitudCliente
        {
            get { return _StrLatitudCliente; }
            set { SetValue(ref _StrLatitudCliente, value); }
        }

        private string _UbicacionSucursal;

        public string StrUbicacionSucursal
        {
            get { return _UbicacionSucursal; }
            set { SetValue(ref _UbicacionSucursal, value); }
        }

        private string _UbicacionCliente;

        public string StrUbicacionCliente
        {
            get { return _UbicacionCliente; }
            set { SetValue(ref _UbicacionCliente, value); }
        }

        private string _StrNumeroCliente;

        public string StrNumeroCliente
        {
            get { return _StrNumeroCliente; }
            set { SetValue(ref _StrNumeroCliente, value); }
        }

        private bool _BlVisibilidadPagoAlRecoger;

        public bool BlVisibilidadPagoAlRecoger
        {
            get { return _BlVisibilidadPagoAlRecoger; }
            set { SetValue(ref _BlVisibilidadPagoAlRecoger, value); }
        }

        #endregion


        #region Propiedades para la informacion del usuario
        private string _StrNombreUsuario;

        public string StrNombreUsuario
        {
            get { return _StrNombreUsuario; }
            set { SetValue(ref _StrNombreUsuario, value); }
        }
        #endregion

        #region Propiedades de pagos
        private string _StrEstatusCobro;

        public string StrEstatusCobro
        {
            get { return _StrEstatusCobro; }
            set { SetValue(ref _StrEstatusCobro, value); }
        }

        private string _strPagoAlRecolectar;

        public string StrPagoAlRecolectar
        {
            get { return _strPagoAlRecolectar; }
            set { SetValue(ref _strPagoAlRecolectar, value); }
        }
        private string _strNotaDeProducto;

        public string StrNotaDeProducto
        {
            get { return _strNotaDeProducto; }
            set { SetValue(ref _strNotaDeProducto, value); }
        }

        #endregion
        #endregion


        #region Commands
        public ICommand ShowInfoOrder { get { return new RelayCommand(CargaDirecciones); } }
        public ICommand ConfirmOrder { get { return new RelayCommand(ConfirmarOrder); } }
        public ICommand CancelOrder { get { return new RelayCommand(CanelarOrden); } }
        public ICommand GetCode { get { return new RelayCommand(ObtenerCodigo); } }
        public ICommand RefreshAll { get { return new RelayCommand(RefrescaOrden); } }

        private void RefrescaOrden()
        {
            Device.InvokeOnMainThreadAsync(async () => { await CargaOrden(); });
        }
        private async void CanelarOrden()
        {
            using (var _WebApiGoDeliverix = new HttpClient())
            {
                var AppInstance = MainViewModel.GetInstance();
                //Cambia el estatus del repartidor
                _WebApiGoDeliverix.BaseAddress = new Uri("" + Helpers.settings.Sitio + "api/");
                url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=O&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=12748F8A-E746-427D-8836-B54432A38C07&UidOrdenRepartidor=" + UidordenRepartidor + "";
                await _WebApiGoDeliverix.GetAsync(url);
            }




            //mVAcceso = new VMAcceso();
            //mVAcceso.BitacoraRegistroRepartidores(char.Parse("O"), AppInstance.Session_.UidUsuario, new Guid("12748F8A-E746-427D-8836-B54432A38C07"), UidOrdenRepartidor: UidordenRepartidor);
            CargaOrden();
            GenerateMessage("Cancelacion exitosa", "Orden Cancelada", "Aceptar");

        }

        private async void ObtenerCodigo()
        {
            bool Respuesta = false;
            var AppInstance = MainViewModel.GetInstance();
            if (!string.IsNullOrEmpty(StrCodigo))
            {
                long codigo = 0;
                if (long.TryParse(StrCodigo, out codigo))
                {
                    using (var _WebApiGoDeliverix = new HttpClient())
                    {
                        url = "" + settings.Sitio + "api/Orden/GetBuscarOrdenPorCodigoQR?strCodigo=" + StrCodigo + "&UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor.ToString() + "";
                        var content = await _WebApiGoDeliverix.GetStringAsync(url);
                        var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        Respuesta = bool.Parse(obj.ToString());
                    }

                    if (Respuesta)
                    {
                        using (var _WebApiGoDeliverix = new HttpClient())
                        {
                            url = "" + settings.Sitio + "api/Profile/GetBitacoraRegistroRepartidores?StrParametro=O&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=7DA3A42F-2271-47B4-B9B8-EDD311F56864&UidOrdenRepartidor=" + AppInstance.MVHome.UidordenRepartidor + "";
                            await _WebApiGoDeliverix.GetAsync(url);
                        }

                        using (var _WebApiGoDeliverix = new HttpClient())
                        {
                            //Peticion de la api para el cambio del estatus de la orden
                            url = "" + settings.Sitio + "api/Orden/GetAgregaEstatusALaOrden?UidEstatus=2FDEE8E7-0D54-4616-B4C1-037F5A37409D&StrParametro=S&UidOrden=" + AppInstance.MVHome.UidOrdenSucursal + "";
                            await _WebApiGoDeliverix.GetAsync(url);
                        }

                        using (var _WebApiGoDeliverix = new HttpClient())
                        {
                            //Peticion de la api para el cambio del estatus de la orden
                            url = "" + settings.Sitio + "api/Pagos/GetCambiarEstatusPago?Estatus=afe0ffaa-3ab8-4ecf-962d-cba4f79d9e34&UidOrden=" + AppInstance.MVHome.UidOrdenSucursal + "";
                            await _WebApiGoDeliverix.GetAsync(url);
                        }


                        AppInstance.MVTurno.CargarTurno();
                        AppInstance.MVHome.Verifica();
                        await AppInstance.MVTurno.VerificaEstatusTurno();
                        AppInstance.MVTurno.VerificaEstatusARecargando();
                        await Application.Current.MainPage.Navigation.PopAsync();
                        GenerateMessage("Orden entregada", "Felicidades, entregaste la orden!!!", "Aceptar");
                    }
                    else
                    {
                        GenerateMessage("Mensaje del sistema", "El codigo no coincide con la orden", "Aceptar");
                    }
                }
                else
                {
                    GenerateMessage("Mensaje del sistema", "El codigo no debe de contener letras o simbolos especiales", "Aceptar");
                }

            }
            else
            {
                GenerateMessage("Mensaje del sistema", "Debes de introducir un codigo", "Aceptar");
            }

        }

        #endregion
        /// <summary>
        /// Confirmacion de la orden por el repartidor, cambia el estatus de la misma para el control interno.
        /// </summary>
        private async void ConfirmarOrder()
        {
            using (var _WebApiGoDeliverix = new HttpClient())
            {
                mVAcceso = new VMAcceso();
                var AppInstance = MainViewModel.GetInstance();
                _WebApiGoDeliverix.BaseAddress = new Uri("" + Helpers.settings.Sitio + "api/");
                url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=O&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=A42B2588-D650-4DD9-829D-5978C927E2ED&UidOrdenRepartidor=" + UidordenRepartidor + "";
                await _WebApiGoDeliverix.GetAsync(url);
            }
        }

        private async void CargaDirecciones()
        {
            MVDireccion = new VMDireccion();
            MVUbicacion = new VMUbicacion();
            string NombreColonia;
            using (var _WebApiGoDeliverix = new HttpClient())
            {
                _WebApiGoDeliverix.BaseAddress = new Uri("" + settings.Sitio + "api/");
                //Obtiene la ubicacion de la sucursal
                url = "Ubicacion/GetRecuperaUbicacionSucursal?UidSucursal=" + UidSucursal + "";
                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);
            }


            using (var _WebApiGoDeliverix = new HttpClient())
            {
                _WebApiGoDeliverix.BaseAddress = new Uri("" + settings.Sitio + "api/");
                //Obtiene la direccion de la sucursal
                url = "Direccion/GetObtenerDireccionCompletaDeSucursal?UidSucursal=" + UidSucursal + "";
                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                MVDireccion = JsonConvert.DeserializeObject<VistaDelModelo.VMDireccion>(obj);
            }

            using (var _WebApiGoDeliverix = new HttpClient())
            {
                _WebApiGoDeliverix.BaseAddress = new Uri("" + settings.Sitio + "api/");
                url = "Direccion/GetObtenerNombreDeLaColonia?UidColonia=" + MVDireccion.COLONIA + "";
                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                NombreColonia = obj.ToString();
            }


            StrUbicacionSucursal = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;

            StrColoniaSucursal = NombreColonia;

            using (var _WebApiGoDeliverix = new HttpClient())
            {
                _WebApiGoDeliverix.BaseAddress = new Uri("" + settings.Sitio + "api/");
                url = "Ubicacion/GetRecuperaUbicacionDireccion?UidDireccion=" + UidDireccionDelCliente + "";
                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);
            }

            using (var _WebApiGoDeliverix = new HttpClient())
            {
                _WebApiGoDeliverix.BaseAddress = new Uri("" + settings.Sitio + "api/");
                //Obtiene la direccion del usuario
                url = "Direccion/GetBuscarDireccion?UidDireccion=" + UidDireccionDelCliente + "";
                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                MVDireccion = JsonConvert.DeserializeObject<VistaDelModelo.VMDireccion>(obj);
            }

            using (var _WebApiGoDeliverix = new HttpClient())
            {
                _WebApiGoDeliverix.BaseAddress = new Uri("" + settings.Sitio + "api/");
                url = "Direccion/GetObtenerNombreDeLaColonia?UidColonia=" + MVDireccion.COLONIA + "";
                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                NombreColonia = obj.ToString();
            }



            // MVUbicacion.RecuperaUbicacionDireccion(UidDireccionDelCliente);
            StrUbicacionCliente = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;
            StrColoniaCliente = NombreColonia;
        }

        public VMHomeOrden()
        {

        }
        public async Task CargaOrden()
        {
            try
            {
                MVOrden = new VMOrden();
                using (var _WebApiGoDeliverix = new HttpClient())
                {
                    url = "" + Helpers.settings.Sitio + "api/Orden/GetObtenerProductosDeOrden?UidOrden=" + StrUidOrden + "";
                    string DatosObtenidos = await _WebApiGoDeliverix.GetStringAsync(url);
                    var DatosProductos = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
                    MVOrden = JsonConvert.DeserializeObject<VMOrden>(DatosProductos);
                }
                string estatus = string.Empty;
                using (var _WebApiGoDeliverix = new HttpClient())
                {
                    url = "" + Helpers.settings.Sitio + "api/Pagos/GetObtenerEstatusDeCobro?UidOrden=" + StrUidOrden + "";
                    string DatosObtenidos = await _WebApiGoDeliverix.GetStringAsync(url);
                    estatus = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();

                }
                if (estatus == "Pendiente")
                {
                    estatus = "Pago en destino";
                }
                StrEstatusCobro = estatus;
                using (var _WebApiGoDeliverix = new HttpClient())
                {
                    url = "" + Helpers.settings.Sitio + "api/Sucursales/GetBuscarSucursales?UidSucursal=" + UidSucursal + "";
                    var content = await _WebApiGoDeliverix.GetStringAsync(url);
                    var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    MVSucursal = JsonConvert.DeserializeObject<VistaDelModelo.VMSucursales>(obj);
                }
                bool respuesta = false;
                using (var _WebApiGoDeliverix = new HttpClient())
                {
                    url = "" + Helpers.settings.Sitio + "api/Contrato/GetPagaAlRecolectar?UidOrdenSucursal=" + StrUidOrden + "";
                    var content = await _WebApiGoDeliverix.GetStringAsync(url);
                    respuesta = bool.Parse(JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString());
                }
                if (respuesta)
                {
                    StrPagoAlRecolectar = "Pago al recolectar orden";
                    BlVisibilidadPagoAlRecoger = true;
                }
                else
                {
                    StrPagoAlRecolectar = "Pagado";
                    BlVisibilidadPagoAlRecoger = false;
                }
                StrIdentificadorSucursal = MVSucursal.IDENTIFICADOR;

                using (var _WebApiGoDeliverix = new HttpClient())
                {
                    url = "" + Helpers.settings.Sitio + "api/Empresa/GetBuscarEmpresas?UidEmpresa=" + MVSucursal.UidEmpresa + "";
                    string content = await _WebApiGoDeliverix.GetStringAsync(url);
                    string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    MVEmpresa = JsonConvert.DeserializeObject<VistaDelModelo.VMEmpresas>(obj);
                }

                StrEmpresaNombreComercial = MVEmpresa.NOMBRECOMERCIAL;

                if (!string.IsNullOrEmpty(UidDireccionDelCliente))
                {
                    //Obtiene el guid del cliente
                    string content;
                    string obj;
                    using (var _WebApiGoDeliverix = new HttpClient())
                    {
                        url = "" + Helpers.settings.Sitio + "api/Direccion/GetObtenerUidUsuarioDeUidDireccion?UidDireccion=" + UidDireccionDelCliente + "";
                        content = await _WebApiGoDeliverix.GetStringAsync(url);
                        obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    }

                    string UidUsuario = obj;
                    using (var _WebApiGoDeliverix = new HttpClient())
                    {
                        url = "" + Helpers.settings.Sitio + "api/Usuario/GetBuscarUsuarios?UidUsuario=" + UidUsuario + "&UIDPERFIL=4F1E1C4B-3253-4225-9E46-DD7D1940DA19";
                        content = await _WebApiGoDeliverix.GetStringAsync(url);
                        obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        MVUsuario = JsonConvert.DeserializeObject<VistaDelModelo.VMUsuarios>(obj);
                    }

                    using (var _WebApiGoDeliverix = new HttpClient())
                    {
                        url = "" + Helpers.settings.Sitio + "api/Telefono/GetObtenerNumeroCliente?UidCliente=" + UidUsuario + "";
                        content = await _WebApiGoDeliverix.GetStringAsync(url);
                        obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        MVTelefono = JsonConvert.DeserializeObject<VistaDelModelo.VMTelefono>(obj);
                    }
                    StrNumeroCliente = MVTelefono.NUMERO;
                    StrNombreUsuario = MVUsuario.StrNombre + " " + MVUsuario.StrApellidoPaterno;
                }
                MTotal = 0.0m;
                MSTotal = 0.0m;
                foreach (VMOrden item in MVOrden.ListaDeProductos)
                {
                    MTotalTarifario = 0.0m;
                    ListaProductos.Add(new Productos()
                    {
                        UidProducto = item.UidProductoEnOrden.ToString(),
                        StrNombreProducto = item.StrNombreProducto,
                        IntCantidad = item.intCantidad,
                        MSubTotal = decimal.Parse(item.MTotalSucursal),
                        MTotal = item.MTotal,
                        BTieneNota = string.IsNullOrEmpty(item.VisibilidadNota) ? false : true
                    });
                    MPropina = item.MPropina;
                    MTotalTarifario = decimal.Parse(item.MCostoTarifario.ToString());
                    MTotal += item.MTotal;
                    MSubTotal += decimal.Parse(item.MTotalSucursal);
                }
                MTotalConPropina = MTotal + MPropina + MTotalTarifario;
                MSubTotal = MSubTotal;
            }
            catch (Exception e)
            {
                GenerateMessage("Error", e.Message, "OK");
            }
            //MVOrden.ObtenerProductosDeOrden(StrUidOrden);
        }
        public async Task MuestraNota(string uidProducto)
        {
            using (var _WebApiGoDeliverix = new HttpClient())
            {
                var ordennota = new VMOrden();
                url = "" + Helpers.settings.Sitio + "api/Orden/GetObtenerNotaDeProducto?uidProductoEnOrden=" + uidProducto + "";
                string DatosObtenidos = await _WebApiGoDeliverix.GetStringAsync(url);
                var DatosProductos = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
                ordennota = JsonConvert.DeserializeObject<VMOrden>(DatosProductos);
                StrNota = ordennota.StrNota;
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
