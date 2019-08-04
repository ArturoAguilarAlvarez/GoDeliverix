﻿using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repartidores_GoDeliverix.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using VistaDelModelo;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMHomeOrden : ControlsController
    {


        #region Variables de la Api
        string url = "";
        HttpClient _WebApiGoDeliverix = new HttpClient();
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
        private decimal _MSubTotal;

        public decimal MSubTotal
        {
            get { return _MSubTotal; }
            set { SetValue(ref _MSubTotal, value); }
        }
        private decimal _MTotalTarifario;

        public decimal MTotalTarifario
        {
            get { return _MTotalTarifario; }
            set { SetValue(ref _MTotalTarifario, value); }
        }

        private List<VMHomeOrden> _ListaProductos;

        public List<VMHomeOrden> ListaProductos
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

        #endregion


        #region Propiedades para la informacion del usuario
        private string _StrNombreUsuario;

        public string StrNombreUsuario
        {
            get { return _StrNombreUsuario; }
            set { SetValue(ref _StrNombreUsuario, value); }
        }
        #endregion
        #endregion


        #region Commands
        public ICommand ShowInfoOrder { get { return new RelayCommand(CargaDirecciones); } }
        public ICommand ConfirmOrder { get { return new RelayCommand(ConfirmarOrder); } }
        public ICommand CancelOrder { get { return new RelayCommand(CanelarOrden); } }
        public ICommand GetCode { get { return new RelayCommand(ObtenerCodigo); } }
        private async void CanelarOrden()
        {
            var AppInstance = MainViewModel.GetInstance();
            //Cambia el estatus del repartidor
            _WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");

            url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=O&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=12748F8A-E746-427D-8836-B54432A38C07&UidOrdenRepartidor=" + UidordenRepartidor + "";
            await _WebApiGoDeliverix.GetAsync(url);



            //mVAcceso = new VMAcceso();
            //mVAcceso.BitacoraRegistroRepartidores(char.Parse("O"), AppInstance.Session_.UidUsuario, new Guid("12748F8A-E746-427D-8836-B54432A38C07"), UidOrdenRepartidor: UidordenRepartidor);
            cargaOrden();
            GenerateMessage("Cancelacion exitosa", "Orden Cancelada", "Aceptar");

        }

        private async void ObtenerCodigo()
        {
            MVOrden = new VMOrden();
            var AppInstance = MainViewModel.GetInstance();
            _WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");

            url = "Orden/GetBuscarOrdenPorCodigoQR?strCodigo=" + StrCodigo + "&UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor.ToString() + "";
            var content = await _WebApiGoDeliverix.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            bool Respuesta = bool.Parse(obj.ToString());

            if (Respuesta)
            {
                url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=O&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=7DA3A42F-2271-47B4-B9B8-EDD311F56864&UidOrdenRepartidor=" + AppInstance.MVHome.UidordenRepartidor + "";
                await _WebApiGoDeliverix.GetAsync(url);

                //Peticion de la api para el cambio del estatus de la orden
                url = "Orden/GetAgregaEstatusALaOrden?UidEstatus=2FDEE8E7-0D54-4616-B4C1-037F5A37409D&StrParametro=S&UidOrden=" + AppInstance.MVHome.UidOrdenSucursal + "";
                await _WebApiGoDeliverix.GetAsync(url);

                AppInstance.MVTurno.CargarTurno();
                // MVOrden.AgregaEstatusALaOrden(new Guid("2FDEE8E7-0D54-4616-B4C1-037F5A37409D"), UidOrden: UidOrdenSucursal, StrParametro: "S");
                AppInstance.MVHome.Verifica();
                GenerateMessage("Orden entregada", "Felicidades, entregaste la orden!!!", "Aceptar");
            }
            else
            {
                GenerateMessage("Mensaje del sistema", "El codigo no coincide con la orden", "Aceptar");
            }
        }

        #endregion
        /// <summary>
        /// Confirmacion de la orden por el repartidor, cambia el estatus de la misma para el control interno.
        /// </summary>
        private async void ConfirmarOrder()
        {
            mVAcceso = new VMAcceso();
            var AppInstance = MainViewModel.GetInstance();
            _WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");
            url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=O&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=A42B2588-D650-4DD9-829D-5978C927E2ED&UidOrdenRepartidor=" + UidordenRepartidor + "";
            await _WebApiGoDeliverix.GetAsync(url);

            //mVAcceso.BitacoraRegistroRepartidores(char.Parse("O"), AppInstance.Session_.UidUsuario, new Guid("A42B2588-D650-4DD9-829D-5978C927E2ED"), UidOrdenRepartidor: UidordenRepartidor);

        }

        private async void CargaDirecciones()
        {
            MVDireccion = new VMDireccion();
            MVUbicacion = new VMUbicacion();
            string NombreColonia = "";
            _WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");

            //Obtiene la ubicacion de la sucursal
            url = "Ubicacion/GetRecuperaUbicacionSucursal?UidSucursal=" + UidSucursal + "";
            string content = await _WebApiGoDeliverix.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);

            //Obtiene la direccion de la sucursal
            url = "Direccion/GetObtenerDireccionCompletaDeSucursal?UidSucursal=" + UidSucursal + "";
            content = await _WebApiGoDeliverix.GetStringAsync(url);
            obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            MVDireccion = JsonConvert.DeserializeObject<VistaDelModelo.VMDireccion>(obj);

            //MVDireccion.ObtenerDireccionSucursal(UidSucursal.ToString());
            //MVUbicacion.RecuperaUbicacionSucursal(UidSucursal.ToString());

            url = "Direccion/GetObtenerNombreDeLaColonia?UidColonia=" + MVDireccion.COLONIA + "";
            content = await _WebApiGoDeliverix.GetStringAsync(url);
            obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            NombreColonia = obj.ToString();

            StrUbicacionSucursal = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;


            StrColoniaSucursal = NombreColonia;

            //MVDireccion.BuscarDireccionPorUid(UidDireccionDelCliente);

            url = "Ubicacion/GetRecuperaUbicacionDireccion?UidDireccion=" + UidDireccionDelCliente + "";
            content = await _WebApiGoDeliverix.GetStringAsync(url);
            obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);

            //Obtiene la direccion del usuario
            url = "Direccion/GetBuscarDireccion?UidDireccion=" + UidDireccionDelCliente + "";
            content = await _WebApiGoDeliverix.GetStringAsync(url);
            obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            MVDireccion = JsonConvert.DeserializeObject<VistaDelModelo.VMDireccion>(obj);

            NombreColonia = string.Empty;
            url = "Direccion/GetObtenerNombreDeLaColonia?UidColonia=" + MVDireccion.COLONIA + "";
            content = await _WebApiGoDeliverix.GetStringAsync(url);
            obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            NombreColonia = obj.ToString();


            // MVUbicacion.RecuperaUbicacionDireccion(UidDireccionDelCliente);
            StrUbicacionCliente = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;
            StrColoniaCliente = NombreColonia;
        }

        public VMHomeOrden()
        {

        }
        public async void cargaOrden()
        {
            try
            {
                MVOrden = new VMOrden();
                url = "http://www.godeliverix.net/api/Orden/GetObtenerProductosDeOrden?UidOrden=" + StrUidOrden + "";

                string DatosObtenidos = await _WebApiGoDeliverix.GetStringAsync(url);
                var DatosProductos = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
                MVOrden = JsonConvert.DeserializeObject<VMOrden>(DatosProductos);


                url = string.Empty;
                url = "http://www.godeliverix.net/api/Sucursales/GetBuscarSucursales?UidSucursal=" + UidSucursal + "";
                var content = await _WebApiGoDeliverix.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                MVSucursal = JsonConvert.DeserializeObject<VistaDelModelo.VMSucursales>(obj);
                StrIdentificadorSucursal = MVSucursal.IDENTIFICADOR;

                url = string.Empty;
                url = "http://www.godeliverix.net/api/Empresa/GetBuscarEmpresas?UidEmpresa=" + MVSucursal.UidEmpresa + "";
                content = await _WebApiGoDeliverix.GetStringAsync(url);
                obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                MVEmpresa = JsonConvert.DeserializeObject<VistaDelModelo.VMEmpresas>(obj);
                StrEmpresaNombreComercial = MVEmpresa.NOMBRECOMERCIAL;

                if (!string.IsNullOrEmpty(UidDireccionDelCliente))
                {
                    //Obtiene el guid del cliente
                    url = string.Empty;
                    url = "http://www.godeliverix.net/api/Direccion/GetObtenerUidUsuarioDeUidDireccion?UidDireccion=" + UidDireccionDelCliente + "";
                    content = await _WebApiGoDeliverix.GetStringAsync(url);
                    obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    string UidUsuario = obj.ToString();

                    url = "http://www.godeliverix.net/api/Usuario/GetBuscarUsuarios?UidUsuario=" + UidUsuario + "&UIDPERFIL=4F1E1C4B-3253-4225-9E46-DD7D1940DA19";
                    content = await _WebApiGoDeliverix.GetStringAsync(url);
                    obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    MVUsuario = JsonConvert.DeserializeObject<VistaDelModelo.VMUsuarios>(obj);

                    StrNombreUsuario = MVUsuario.StrNombre + " "+ MVUsuario.StrApellidoPaterno;
                }


                MTotal = 0.0m;
                ListaProductos = new List<VMHomeOrden>();
                foreach (VMOrden item in MVOrden.ListaDeProductos)
                {
                    MTotalTarifario = 0.0m;
                    ListaProductos.Add(new VMHomeOrden()
                    {
                        StrNombreProducto = item.StrNombreProducto,
                        IntCantidad = item.intCantidad,
                        MTotal = item.MTotal
                    });
                    MTotalTarifario = decimal.Parse(item.MCostoTarifario.ToString());
                    MTotal = MTotal + item.MTotal;
                }
                MSubTotal = MTotal;
                MTotal = MTotal + MTotalTarifario;


                


            }
            catch (Exception)
            {

                GenerateMessage("Aviso", "Sin acceso a los servidores", "OK");
            }

            //MVOrden.ObtenerProductosDeOrden(StrUidOrden);

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
