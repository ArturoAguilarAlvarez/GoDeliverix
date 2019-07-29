using DBControl;
using Modelo;
using Modelo.Usuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VistaDelModelo
{
    public class VMSucursales
    {
        #region Propiedades
        DbSucursal Datos = new DbSucursal();
        VMTelefono MVTelefono = new VMTelefono();
        VMDireccion MVDireccion = new VMDireccion();
        DbSucursal odbSucursal;
        private Guid _uidSucursal;

        #region pripiedades de modelo
        public Guid UidRelacionRegistro { get; set; }
        public Guid ID
        {
            get { return _uidSucursal; }
            set { _uidSucursal = value; }
        }

        private string _identificador;

        public string IDENTIFICADOR
        {
            get { return _identificador; }
            set { _identificador = value; }
        }
        private string _HoraApertura;

        public string HORAAPARTURA
        {
            get { return _HoraApertura; }
            set { _HoraApertura = value; }
        }
        private string _HoraCierre;

        public string HORACIERRE
        {
            get { return _HoraCierre; }
            set { _HoraCierre = value; }
        }

        private int _UidEstatus;

        public int Estatus
        {
            get { return _UidEstatus; }
            set { _UidEstatus = value; }
        }
        private Guid _UidEmpresa;



        public Guid UidEmpresa
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }

        #endregion

        public string StrRutaImagen { get; set; }
        public string StrEstatusRepartidor { get; set; }
        private double _dbTotal;
        public double MTotal
        {
            get { return _dbTotal; }
            set { _dbTotal = value; }
        }
        private Guid _UidGiro;

        public Guid UIDGIRO
        {
            get { return _UidGiro; }
            set { _UidGiro = value; }
        }

        private Telefono _t;

        public Telefono T
        {
            get { return _t; }
            set { _t = value; }
        }

        private string _StrNombre;

        public string StrNombreRepartidor
        {
            get { return _StrNombre; }
            set { _StrNombre = value; }
        }
        public string StrNombreColonia { get; set; }
        private string _strUsuario;

        public string StrUsuario
        {
            get { return _strUsuario; }
            set { _strUsuario = value; }
        }

        private string _strVehiculo;

        public string StrVehiculo
        {
            get { return _strVehiculo; }
            set { _strVehiculo = value; }
        }


        private Sucursal _sucursal = new Sucursal();

        public Sucursal SUCURSAL
        {
            get { return _sucursal; }
            set { _sucursal = value; }
        }

        public void AgregarALista(Guid UidSucursal)
        {

            if (!ListaDeSucursalesSeleccionadas.Count.Equals(1))
            {
                if (!ListaDeSucursalesSeleccionadas.Exists(Sucursal => Sucursal.ID == UidSucursal))
                {
                    var objeto = LISTADESUCURSALES.Find(Sucursal => Sucursal.ID == UidSucursal);
                    ListaDeSucursalesSeleccionadas.Add(objeto);
                    objeto = LISTADESUCURSALES.Find(Sucursal => Sucursal.ID == UidSucursal);
                    objeto.Seleccionar = true;
                }
            }
            else
            {
                ListaDeSucursalesSeleccionadas.Clear();

                if (!ListaDeSucursalesSeleccionadas.Exists(Sucursal => Sucursal.ID == UidSucursal))
                {
                    var objeto = LISTADESUCURSALES.Find(Sucursal => Sucursal.ID == UidSucursal);
                    ListaDeSucursalesSeleccionadas.Add(objeto);
                    objeto = LISTADESUCURSALES.Find(Sucursal => Sucursal.ID == UidSucursal);
                    objeto.Seleccionar = true;
                }
            }

        }

        private DataTable datatable;
        public DataTable DT
        {
            get { return datatable; }
            set { datatable = value; }
        }
        //Borrar este metodo
        public void EliminarDeLista(Guid UidSucursal)
        {
            if (ListaDeSucursalesSeleccionadas.Exists(Sucursal => Sucursal.ID == UidSucursal))
            {
                var objeto = ListaDeSucursalesSeleccionadas.Find(Sucursal => Sucursal.ID == UidSucursal);
                ListaDeSucursalesSeleccionadas.Remove(objeto);
                objeto = LISTADESUCURSALES.Find(Sucursal => Sucursal.ID == UidSucursal);
                objeto.Seleccionar = false;
            }
        }

        private Guid _UidestatusContrato;

        public Guid UidEstatusContrato
        {
            get { return _UidestatusContrato; }
            set { _UidestatusContrato = value; }
        }

        public Guid UIDCATEGORIA { get; private set; }
        public Guid UIDSUBCATEGORIA { get; private set; }
        public Guid UidProducto { get; set; }
        public Guid UidColonia { get; set; }

        //Listas
        public List<Estatus> ESTATUS;

        public List<VMSucursales> LISTADESUCURSALES = new List<VMSucursales>();

        public List<TipoDeTelefono> TIPOTELEFONO;
        public List<Telefono> ListaTELEFONO = new List<Telefono>();
        public List<Direccion> ListaDIRECCIONES = new List<Direccion>();
        public List<VMSucursales> ListaDeGiro = new List<VMSucursales>();
        public List<VMSucursales> ListaDeCategorias = new List<VMSucursales>();
        public List<VMSucursales> ListaDeSubcategorias = new List<VMSucursales>();
        public List<VMSucursales> ListaDeproductos = new List<VMSucursales>();
        public List<VMSucursales> ListaDeColoniasEntrega = new List<VMSucursales>();
        public List<VMSucursales> ListaDeColoniasRecolecta = new List<VMSucursales>();
        public List<VMSucursales> ListaDeSucursalesConGiro = new List<VMSucursales>();
        public List<VMSucursales> ListaDeSucursalesConCategoria = new List<VMSucursales>();
        public List<VMSucursales> ListaDeSucursalesConSubcategoria = new List<VMSucursales>();
        public List<VMSucursales> ListaDeSucursalesDeContrato = new List<VMSucursales>();
        public List<VMSucursales> ListaDeSucursalesSeleccionadas = new List<VMSucursales>();
        public List<VMSucursales> ListaDeOrdenesAsignadas = new List<VMSucursales>();
        #region Propiedades de la direccion (Solo para almacenar el nombre de la direccion)
        private string _manzana;

        public string StrManzana
        {
            get { return _manzana; }
            set { _manzana = value; }
        }
        public string StrLote { get; set; }
        public string strCodigoPostal { get; set; }
        public string strCiudad { get; set; }
        public string strColonia { get; set; }
        public string StrCalle { get; set; }
        #endregion
        //Variable par aalmacenar el nombre de la empresa de la sucursal
        public string strEmpresa { get; set; }
        public bool Seleccionar { get; set; }
        //Variables para el control de los repartiores de la sucursal
        public List<VMSucursales> ListaDeRepartidoresyVehiculosEnSucursal = new List<VMSucursales>();
        public Guid UidUsuario { get; set; }
        public Guid UidVehiculo { get; set; }
        //Variables para el control de las ordenes asignadas
        public long lngFolio { get; set; }
        public Guid UidOrden { get; set; }
        public string FechaAsignacion { get; set; }
        public string usuario { get; set; }
        public bool Seleccion { get; set; }
        #endregion



        #region Metodos

        #region Catalogos



        #region Catalogo Telefono
        public void TipoDeTelefonos()
        {
            TIPOTELEFONO = new List<TipoDeTelefono>();
            foreach (DataRow item in Datos.ObtenerTipoDeTelefono().Rows)
            {
                Guid id = new Guid(item["UidTipoDeTelefono"].ToString());
                string nombre = item["Nombre"].ToString();
                TIPOTELEFONO.Add(new TipoDeTelefono(id, nombre));
            }
        }
        #endregion



        #endregion

        #region GridView busqueda normal

        public void DatosGridViewBusquedaNormal(string UidEmpresa)
        {
            LISTADESUCURSALES = new List<VMSucursales>();
            DataTable Dt = Datos.ObtenerSucursalBusquedaNormal(UidEmpresa);
            foreach (DataRow item in Dt.Rows)
            {
                Guid id = new Guid(item["UidSucursal"].ToString().ToUpper());
                string identificador = item["Identificador"].ToString().ToUpper();
                string HA = item["HorarioApertura"].ToString().ToUpper();
                string HC = item["HorarioCierre"].ToString().ToUpper();


                LISTADESUCURSALES.Add(new VMSucursales() { ID = id, IDENTIFICADOR = identificador, HORAAPARTURA = HA, HORACIERRE = HC });
            }
        }

        #endregion

        #region GridView busqueda ampliada

        public void DatosGridViewBusquedaAmpliada(string UidEmpresa)
        {
            LISTADESUCURSALES = new List<VMSucursales>();
            DataTable Dt = Datos.ObtenerSucursalBusquedaAmpliada(UidEmpresa);
            foreach (DataRow item in Dt.Rows)
            {
                Guid id = new Guid(item["UidSucursal"].ToString().ToUpper());
                string identificador = item["Identificador"].ToString().ToUpper();
                string HA = item["HorarioApertura"].ToString().ToUpper();
                string HC = item["HorarioCierre"].ToString().ToUpper();


                LISTADESUCURSALES.Add(new VMSucursales() { ID = id, IDENTIFICADOR = identificador, HORAAPARTURA = HA, HORACIERRE = HC });
            }
        }

        #endregion

        #region Busquedas
        public void BuscarSucursales(string identificador = "", string horaApertura = "", string horaCierre = "", Guid IdColonia = new Guid(), string Uidempresa = "", string UidSucursal = "")
        {
            DataTable Dt = new DataTable();
            LISTADESUCURSALES = new List<VMSucursales>();
            try
            {
                SqlCommand CMD = new SqlCommand();
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.CommandText = "asp_BuscarSucursal";

                if (!string.IsNullOrWhiteSpace(Uidempresa))
                {
                    CMD.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidEmpresa"].Value = new Guid(Uidempresa);
                }
                if (IdColonia != Guid.Empty)
                {
                    CMD.Parameters.Add("@Colonia", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@Colonia"].Value = IdColonia;
                }
                if (((UidSucursal == Guid.Empty.ToString() || (string.IsNullOrWhiteSpace(UidSucursal)))))
                {
                    if (identificador != string.Empty)
                    {
                        CMD.Parameters.Add("@Identificador", SqlDbType.NVarChar, 50);
                        CMD.Parameters["@Identificador"].Value = identificador;
                    }
                    if (horaApertura != string.Empty)
                    {
                        CMD.Parameters.Add("@HoraApertura", SqlDbType.NVarChar, 20);
                        CMD.Parameters["@HoraApertura"].Value = horaApertura;
                    }
                    if (horaCierre != string.Empty)
                    {
                        CMD.Parameters.Add("@HoraCierre", SqlDbType.NVarChar, 20);
                        CMD.Parameters["@HoraCierre"].Value = horaCierre;
                    }
                    if (Estatus != 0)
                    {
                        CMD.Parameters.Add("@IntEstatus", SqlDbType.Int);
                        CMD.Parameters["@IntEstatus"].Value = Estatus;
                    }
                    DT = Datos.Busquedas(CMD);
                    foreach (DataRow item in DT.Rows)
                    {
                        Guid id = new Guid(item["UidSucursal"].ToString());
                        string IDENTIFICADOR = item["Identificador"].ToString().ToUpper();
                        string HA = item["HorarioApertura"].ToString().ToUpper();
                        string HC = item["HorarioCierre"].ToString().ToUpper();
                        int estatus = 0;
                        Guid uidEmpresa = new Guid();
                        if (item["IntEstatus"] != null)
                        {
                            estatus = int.Parse(item["IntEstatus"].ToString());
                        }
                        if (item["UidEmpresa"] != null)
                        {
                            uidEmpresa = new Guid(item["UidEmpresa"].ToString());
                        }
                        LISTADESUCURSALES.Add(new VMSucursales() { ID = id, IDENTIFICADOR = IDENTIFICADOR, HORAAPARTURA = HA, HORACIERRE = HC, Estatus = estatus, UidEmpresa = uidEmpresa });
                    }
                }
                else
                {
                    if (UidSucursal != Guid.Empty.ToString() && !string.IsNullOrWhiteSpace(UidSucursal))
                    {
                        CMD.Parameters.Add("@uidSucursal", SqlDbType.UniqueIdentifier);
                        CMD.Parameters["@uidSucursal"].Value = new Guid(UidSucursal);
                    }
                    DT = Datos.Busquedas(CMD);
                    foreach (DataRow item in DT.Rows)
                    {
                        Guid id = new Guid(item["UidSucursal"].ToString());
                        string Identificador = item["Identificador"].ToString().ToUpper();
                        string HA = item["HorarioApertura"].ToString().ToUpper();
                        string HC = item["HorarioCierre"].ToString().ToUpper();

                        ID = id;
                        IDENTIFICADOR = Identificador;
                        HORAAPARTURA = HA;
                        HORACIERRE = HC;
                        Estatus = int.Parse(item["IntEstatus"].ToString());
                        UidEmpresa = new Guid(item["UidEmpresa"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void BuscarSucursalesCliente(Guid uidEmpresa, string day, Guid UidDireccion)
        {
            DataTable Dt = new DataTable();
            LISTADESUCURSALES = new List<VMSucursales>();
            try
            {
                SqlCommand CMD = new SqlCommand();
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.CommandText = "asp_BuscarSucursalesClientes";


                CMD.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@UidEmpresa"].Value = uidEmpresa;

                CMD.Parameters.Add("@UidDireccion", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@UidDireccion"].Value = UidDireccion;



                CMD.Parameters.Add("@StrDia", SqlDbType.NVarChar, 20);
                CMD.Parameters["@StrDia"].Value = day;
                DT = Datos.Busquedas(CMD);
                foreach (DataRow item in DT.Rows)
                {
                    Guid id = new Guid(item["UidSucursal"].ToString());
                    string IDENTIFICADOR = item["Identificador"].ToString().ToUpper();
                    string HA = item["HorarioApertura"].ToString().ToUpper();
                    string HC = item["HorarioCierre"].ToString().ToUpper();
                    LISTADESUCURSALES.Add(new VMSucursales() { ID = id, IDENTIFICADOR = IDENTIFICADOR, HORAAPARTURA = HA, HORACIERRE = HC, UidEmpresa = uidEmpresa });

                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string ObtenerUidSucursalVehiculo(Guid valor)
        {
            Guid UidSucursal = Guid.Empty;
            foreach (DataRow item in Datos.ObtenerUidSucursalVehiculo(valor).Rows)
            {
                UidSucursal = new Guid(item["UidSucursal"].ToString());
            }
            return UidSucursal.ToString();
        }

        /// <summary>
        /// Busca los contratos y los almacena en una lista, si solo se envia el UidSucursal el contrato no aparece en la lista normal, lo busca aun si este no es visible y lo agrega a la lista
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="horaApertura"></param>
        /// <param name="horaCierre"></param>
        /// <param name="IdColonia"></param>
        /// <param name="UidEstatusContrato"></param>
        /// <param name="StrCodigoLocalizador"></param>
        /// <param name="StrTipoDeBusqueda"></param>
        /// <param name="tipoDeEmpresa"></param>
        /// <param name="UidSucursal"></param>
        public void BuscarSucursalesContrato(string identificador = "", string horaApertura = "", string horaCierre = "", Guid IdColonia = new Guid(), Guid UidEstatusContrato = new Guid(), string StrCodigoLocalizador = "", string StrTipoDeBusqueda = "", int tipoDeEmpresa = 0, Guid UidSucursal = new Guid(), int Visibilidad = 1)
        {
            DataTable Dt = new DataTable();
            LISTADESUCURSALES = new List<VMSucursales>();
            try
            {
                SqlCommand CMD = new SqlCommand();
                CMD.CommandType = CommandType.StoredProcedure;
                CMD.CommandText = "asp_BuscarSucursal";

                if (UidSucursal != Guid.Empty)
                {
                    CMD.Parameters.Add("@uidSucursal", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@uidSucursal"].Value = UidSucursal;
                }
                if (StrTipoDeBusqueda != string.Empty)
                {
                    CMD.Parameters.Add("@TipoDeBusqueda", SqlDbType.VarChar, 10);
                    CMD.Parameters["@TipoDeBusqueda"].Value = StrTipoDeBusqueda;
                }
                if (IdColonia != Guid.Empty)
                {
                    CMD.Parameters.Add("@Colonia", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@Colonia"].Value = IdColonia;
                }
                if (tipoDeEmpresa != 0)
                {
                    CMD.Parameters.Add("@IntTipoDeEmpresa", SqlDbType.Int);
                    CMD.Parameters["@IntTipoDeEmpresa"].Value = tipoDeEmpresa;
                }
                if (Visibilidad != -1)
                {
                    CMD.Parameters.Add("@BVisibilidad", SqlDbType.Int);
                    CMD.Parameters["@BVisibilidad"].Value = Visibilidad;
                }

                if (UidSucursal != Guid.Empty && Visibilidad != 1)
                {
                    DT = Datos.Busquedas(CMD);
                    foreach (DataRow item in DT.Rows)
                    {
                        Guid id = new Guid(item["UidSucursal"].ToString());
                        string IDENTIFICADOR = item["Identificador"].ToString().ToUpper();
                        string HA = item["HorarioApertura"].ToString().ToUpper();
                        string HC = item["HorarioCierre"].ToString().ToUpper();
                        int estatus = 0;
                        Guid uidEmpresa = new Guid();
                        if (item["IntEstatus"] != null)
                        {
                            estatus = int.Parse(item["IntEstatus"].ToString());
                        }
                        if (item["UidEmpresa"] != null)
                        {
                            uidEmpresa = new Guid(item["UidEmpresa"].ToString());
                        }
                        if (!ListaDeSucursalesDeContrato.Exists(suc => suc.ID == id))
                        {
                            ListaDeSucursalesDeContrato.Add(new VMSucursales() { ID = id, IDENTIFICADOR = IDENTIFICADOR, HORAAPARTURA = HA, HORACIERRE = HC, Estatus = estatus, UidEmpresa = uidEmpresa });
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(StrCodigoLocalizador))
                    {
                        if (identificador != string.Empty)
                        {
                            CMD.Parameters.Add("@Identificador", SqlDbType.NVarChar, 50);
                            CMD.Parameters["@Identificador"].Value = identificador;
                        }
                        if (horaApertura != string.Empty)
                        {
                            CMD.Parameters.Add("@HoraApertura", SqlDbType.NVarChar, 20);
                            CMD.Parameters["@HoraApertura"].Value = horaApertura;
                        }
                        if (horaCierre != string.Empty)
                        {
                            CMD.Parameters.Add("@HoraCierre", SqlDbType.NVarChar, 20);
                            CMD.Parameters["@HoraCierre"].Value = horaCierre;
                        }
                        if (UidEstatusContrato != Guid.Empty)
                        {
                            CMD.Parameters.Add("@IntEstatus", SqlDbType.UniqueIdentifier);
                            CMD.Parameters["@IntEstatus"].Value = UidEstatusContrato;
                        }

                        DT = Datos.Busquedas(CMD);
                        ListaDeSucursalesDeContrato.Clear();
                        foreach (DataRow item in DT.Rows)
                        {
                            Guid id = new Guid(item["UidSucursal"].ToString());
                            string IDENTIFICADOR = item["Identificador"].ToString().ToUpper();
                            string HA = item["HorarioApertura"].ToString().ToUpper();
                            string HC = item["HorarioCierre"].ToString().ToUpper();
                            int estatus = 0;
                            Guid uidEmpresa = new Guid();
                            if (item["IntEstatus"] != null)
                            {
                                estatus = int.Parse(item["IntEstatus"].ToString());
                            }
                            if (item["UidEmpresa"] != null)
                            {
                                uidEmpresa = new Guid(item["UidEmpresa"].ToString());
                            }
                            if (!ListaDeSucursalesDeContrato.Exists(suc => suc.ID == id))
                            {
                                ListaDeSucursalesDeContrato.Add(new VMSucursales() { ID = id, IDENTIFICADOR = IDENTIFICADOR, HORAAPARTURA = HA, HORACIERRE = HC, Estatus = estatus, UidEmpresa = uidEmpresa });
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(StrCodigoLocalizador))
                        {
                            CMD.Parameters.Add("@VchCodigo", SqlDbType.VarChar, 6);
                            CMD.Parameters["@VchCodigo"].Value = StrCodigoLocalizador;
                        }
                        DT = Datos.Busquedas(CMD);
                        ListaDeSucursalesDeContrato.Clear();
                        foreach (DataRow item in DT.Rows)
                        {
                            Guid id = new Guid(item["UidSucursal"].ToString());
                            string Identificador = item["Identificador"].ToString().ToUpper();
                            string HA = item["HorarioApertura"].ToString().ToUpper();
                            string HC = item["HorarioCierre"].ToString().ToUpper();
                            if (!string.IsNullOrWhiteSpace(StrTipoDeBusqueda))
                            {
                                ListaDeSucursalesDeContrato.Add(new VMSucursales() { ID = id, IDENTIFICADOR = Identificador, HORAAPARTURA = HA, HORACIERRE = HC });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void ObtenerSucursal(string id)
        {
            DataTable DtEmpresa = Datos.ObtenerSucursal(id);
            SUCURSAL = new Sucursal();
            foreach (DataRow item in DtEmpresa.Rows)
            {
                //Datos de la sucursal
                Guid ID = new Guid(item["UidSucursal"].ToString());
                string IDENTIFICADOR = item["Identificador"].ToString().ToUpper();
                string HA = item["HorarioApertura"].ToString().ToUpper();
                string HC = item["HorarioCierre"].ToString().ToUpper();
                bool visibilidad = bool.Parse(item["VisibilidadDeInformacion"].ToString());
                string codigo = item["CodigoLocalizador"].ToString();
                SUCURSAL = new Sucursal() { ID = ID, IDENTIFICADOR = IDENTIFICADOR, HORAAPARTURA = HA, HORACIERRE = HC, StrCodigo = codigo, BVisibilidad = visibilidad };

                this.ID = ID;
                this.IDENTIFICADOR = IDENTIFICADOR;
                HORAAPARTURA = HA;
                HORACIERRE = HC;
                Estatus = int.Parse(item["IntEstatus"].ToString());
            }
        }

        public string obtenerUidSucursal(string Uidusuario)
        {
            Guid UidSucursal = Guid.Empty;
            foreach (DataRow item in Datos.ObtenerUidSucursal(Uidusuario).Rows)
            {
                UidSucursal = new Guid(item["UidSucursal"].ToString());
            }
            return UidSucursal.ToString();
        }
        public string obtenerUidSucursalRepartidor(string Uidusuario)
        {
            Guid UidSucursal = Guid.Empty;
            foreach (DataRow item in Datos.ObtenerUidSucursalRepartidor(Uidusuario).Rows)
            {
                UidSucursal = new Guid(item["UidSucursal"].ToString());
            }
            return UidSucursal.ToString();
        }

        public void ObtenerSucursalesDistribuidorasContratadas(string UidLicencia)
        {
            LISTADESUCURSALES.Clear();
            foreach (DataRow item in Datos.ObtenerDistribuidorasEnContrato(UidLicencia).Rows)
            {
                Guid Id = new Guid(item["UidSucursal"].ToString());
                string Nombre = item["Identificador"].ToString();
                LISTADESUCURSALES.Add(new VMSucursales() { ID = Id, IDENTIFICADOR = Nombre, Seleccionar = false });
            }
        }

        #endregion


        #region Inserciones y actualizaciones
        public bool GuardarSucursal(Guid UidSucursal, Guid UIDDIRECCION, string IDENTIFICADOR, string Empresa, string HoraApertura, string HoraCierre, string ESTATUS, bool bVisibilidadInformacion, string codigo)
        {
            bool resultado = false;
            try
            {
                Guid UIDEMPRESA = new Guid(Empresa);
                resultado = _sucursal.GUARDARSUCURSAL(new Sucursal()
                {
                    ID = UidSucursal,
                    HORAAPARTURA = HoraApertura,
                    HORACIERRE = HoraCierre,
                    IDENTIFICADOR = IDENTIFICADOR,
                    DIRECCION = new Direccion() { ID = UIDDIRECCION },
                    EMPRESA = new Suministros() { UIDEMPRESA = UIDEMPRESA },
                    USUARIO = new Usuarios() { ID = new Guid(Empresa) },
                    Estatus = new Estatus() { ID = int.Parse(ESTATUS) },
                    BVisibilidad = bVisibilidadInformacion,
                    StrCodigo = codigo
                });
            }
            catch (Exception)
            {

                throw;
            }

            return resultado;

        }


        public Guid ObtenerUidEmpresa(string Usuario)
        {
            Guid UIDEMPRESA = new Guid();
            foreach (DataRow item in Datos.ObtenUidDeEmpresa(Usuario).Rows)
            {
                UIDEMPRESA = new Guid(item["UIDEMPRESA"].ToString());
            }
            return UIDEMPRESA;
        }
        public bool ActualizarDatos(string IdSucursal, string IDENTIFICADOR, string HoraApertura, string HoraCierre, string estatus, bool bVisibilidadInformacion, string codigo)
        {
            bool resultado = false;
            Guid ID = new Guid(IdSucursal);
            try
            {
                resultado = SUCURSAL.ACTUALIZASUCURSAL(new Sucursal() { ID = ID, HORAAPARTURA = HoraApertura, HORACIERRE = HoraCierre, Estatus = new Estatus() { ID = int.Parse(estatus) }, IDENTIFICADOR = IDENTIFICADOR, BVisibilidad = bVisibilidadInformacion, StrCodigo = codigo });
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        #endregion

        #region Validaciones

        //public bool ValidaRfc(string RFC)
        //{
        //    bool resultado = false;
        //    if (Datos.ValidarRFC(RFC).Rows.Count <= 0)
        //    {
        //        resultado = true;
        //    }
        //    return resultado;
        //}

        //public bool validaCorreoElectronico(string correo)
        //{
        //    bool resultado = false;
        //    if (Datos.VAlidarCorreoElectronico(correo).Rows.Count <= 0)
        //    {
        //        resultado = true;
        //    }
        //    return resultado;
        //}

        #endregion

        public void SeleccionaSucursal(Guid valor)
        {
            if (ListaDeSucursalesConSubcategoria.Count > 0)
            {
                for (int i = 0; i < ListaDeSucursalesConSubcategoria.Count; i++)
                {
                    if (ListaDeSucursalesConSubcategoria[i].ID != valor)
                    {
                        ListaDeSucursalesConSubcategoria.RemoveAt(i);
                        i--;
                    }
                }
            }
            else if (ListaDeSucursalesConCategoria.Count > 0)
            {
                for (int i = 0; i < ListaDeSucursalesConCategoria.Count; i++)
                {
                    if (ListaDeSucursalesConCategoria[i].ID != valor)
                    {
                        ListaDeSucursalesConCategoria.RemoveAt(i);
                        i--;
                    }
                }
            }
            else if (ListaDeSucursalesConGiro.Count > 0)
            {

                for (int i = 0; i < ListaDeSucursalesConGiro.Count; i++)
                {
                    if (ListaDeSucursalesConGiro[i].ID != valor)
                    {
                        ListaDeSucursalesConGiro.RemoveAt(i);
                        i--;
                    }
                }

            }
        }

        #region Sucursales con catalogos
        public void SucursalesConGiro(string uidGiro, string uidcolonia, string tiempoActual, string Nombre = "")
        {
            ListaDeSucursalesConGiro.Clear();
            foreach (DataRow item in Datos.obtenerSucursalesGiro(uidGiro, uidcolonia, tiempoActual, Nombre).Rows)
            {
                Guid uidsucursal = new Guid(item["uidSucursal"].ToString());
                string horaApertura = item["HorarioApertura"].ToString();
                string horaCierre = item["HorarioCierre"].ToString();
                string nombre = item["Identificador"].ToString();
                string Empresa = item["NombreComercial"].ToString();
                string imagen = "../" + item["NVchRuta"].ToString();
                string Manzana = item["Manzana"].ToString();
                string Lote = item["Lote"].ToString();
                string CodigoPostral = item["CodigoPostal"].ToString();
                string ciudad = item["ciudad"].ToString();
                string colonia = item["colonia"].ToString();
                string calle = item["Calle0"].ToString().ToUpper();
                if (string.IsNullOrEmpty(imagen))
                {
                    imagen = "../Img/Giro/Default.png";
                }
                ListaDeSucursalesConGiro.Add(new VMSucursales() { strEmpresa = Empresa, ID = uidsucursal, HORAAPARTURA = horaApertura, HORACIERRE = horaCierre, IDENTIFICADOR = nombre, StrRutaImagen = imagen, StrManzana = Manzana, StrLote = Lote, strCodigoPostal = CodigoPostral, strCiudad = ciudad, strColonia = colonia, StrCalle = calle });
            }
        }

        public void SucursalesConCategoria(string uidCategoria, string uidcolonia, string tiempoActual, string Nombre = "")
        {
            ListaDeSucursalesConCategoria.Clear();
            foreach (DataRow item in Datos.obtenerSucursalesCategoria(uidCategoria, uidcolonia, tiempoActual, Nombre).Rows)
            {
                Guid uidsucursal = new Guid(item["uidSucursal"].ToString());
                string identificador = item["Identificador"].ToString();
                string horaApertura = item["HorarioApertura"].ToString();
                string horaCierre = item["HorarioCierre"].ToString();
                string nombre = item["Identificador"].ToString();
                string Empresa = item["NombreComercial"].ToString();
                string imagen = "../" + item["NVchRuta"].ToString();
                string Manzana = item["Manzana"].ToString();
                string Lote = item["Lote"].ToString();
                string CodigoPostral = item["CodigoPostal"].ToString();
                string ciudad = item["ciudad"].ToString();
                string colonia = item["colonia"].ToString();
                string calle = item["Calle0"].ToString().ToUpper();
                if (string.IsNullOrEmpty(imagen))
                {
                    imagen = "../Img/Categoria/Default.jpg";
                }
                ListaDeSucursalesConCategoria.Add(new VMSucursales() { strEmpresa = Empresa, ID = uidsucursal, HORAAPARTURA = horaApertura, HORACIERRE = horaCierre, IDENTIFICADOR = nombre, StrRutaImagen = imagen, StrManzana = Manzana, StrLote = Lote, strCodigoPostal = CodigoPostral, strCiudad = ciudad, strColonia = colonia, StrCalle = calle });
            }
        }
        public void SucursalesConSubcategoria(string uidCategoria, string uidcolonia, string tiempoActual, string Nombre = "")
        {
            ListaDeSucursalesConSubcategoria.Clear();
            foreach (DataRow item in Datos.obtenerSucursalesSubcategoria(uidCategoria, uidcolonia, tiempoActual, Nombre).Rows)
            {
                Guid uidsucursal = new Guid(item["uidSucursal"].ToString());
                string horaApertura = item["HorarioApertura"].ToString();
                string horaCierre = item["HorarioCierre"].ToString();
                string nombre = item["Identificador"].ToString();
                string Empresa = item["NombreComercial"].ToString();
                string imagen = "../" + item["NVchRuta"].ToString();
                string Manzana = item["Manzana"].ToString();
                string Lote = item["Lote"].ToString();
                string CodigoPostral = item["CodigoPostal"].ToString();
                string ciudad = item["ciudad"].ToString();
                string colonia = item["colonia"].ToString();
                string calle = item["Calle0"].ToString().ToUpper();
                if (string.IsNullOrEmpty(imagen))
                {
                    imagen = "../Img/Subcategoria/Default.jpg";
                }
                ListaDeSucursalesConSubcategoria.Add(new VMSucursales() { strEmpresa = Empresa, ID = uidsucursal, HORAAPARTURA = horaApertura, HORACIERRE = horaCierre, IDENTIFICADOR = nombre, StrRutaImagen = imagen, StrManzana = Manzana, StrLote = Lote, strCodigoPostal = CodigoPostral, strCiudad = ciudad, strColonia = colonia, StrCalle = calle });
            }
        }
        #endregion
        public DataView Sort(string sortExpression, string valor, string Panel, string UidEmpresa)
        {
            DataSet ds = new DataSet();
            DataTable t = new DataTable();

            if (Panel == "Normal")
            {
                t = Datos.ObtenerSucursalBusquedaNormal(UidEmpresa);
            }
            else if (Panel == "Ampliada")
            {
                t = Datos.ObtenerSucursalBusquedaAmpliada(UidEmpresa);
            }

            ds.Tables.Add(t);


            DataView dt = null;
            dt = new DataView(ds.Tables[0]);
            dt.Sort = sortExpression + " " + valor;

            return dt;
        }

        public bool RelacionGiro(string UidGiro, Guid UidSucursal)
        {
            bool resultado = false;
            try
            {
                SUCURSAL = new Sucursal();
                Guid Giro = new Guid(UidGiro);
                resultado = SUCURSAL.RELACIONGIRO(Giro, UidSucursal);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public bool RelacionCategoria(string UidCategoria, Guid UidSucursal)
        {
            bool resultado = false;
            try
            {
                SUCURSAL = new Sucursal();
                Guid Categoria = new Guid(UidCategoria);
                resultado = SUCURSAL.RELACIONCATEGORIA(Categoria, UidSucursal);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public bool RelacionSubategoria(string Uidsubategoria, Guid UidSucursal)
        {
            bool resultado = false;
            try
            {
                SUCURSAL = new Sucursal();
                Guid Subcategoria = new Guid(Uidsubategoria);
                resultado = SUCURSAL.RELACIONSUBCATEGORIA(Subcategoria, UidSucursal);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public void RecuperaGiro(string UidSucursal)
        {
            ListaDeGiro.Clear();
            foreach (DataRow item in Datos.ObtenerGiro(UidSucursal).Rows)
            {
                Guid sucursal = new Guid(item["UidSucursal"].ToString());
                Guid Giro = new Guid(item["UidGiro"].ToString());
                ListaDeGiro.Add(new VMSucursales() { ID = sucursal, UIDGIRO = Giro });
            }
        }
        public void RecuperaCategoria(string UidSucursal)
        {
            ListaDeCategorias.Clear();
            foreach (DataRow item in Datos.ObtenerCategoria(UidSucursal).Rows)
            {
                Guid sucursal = new Guid(item["UidSucursal"].ToString());
                Guid Categoria = new Guid(item["UidCategoria"].ToString());
                ListaDeCategorias.Add(new VMSucursales() { ID = sucursal, UIDCATEGORIA = Categoria });
            }
        }
        public void RecuperaSubcategoria(string UidSucursal)
        {
            ListaDeSubcategorias.Clear();
            foreach (DataRow item in Datos.ObtenerSubcategoria(UidSucursal).Rows)
            {
                Guid sucursal = new Guid(item["UidSucursal"].ToString());
                Guid Subcategoria = new Guid(item["UidSubcategoria"].ToString());
                ListaDeSubcategorias.Add(new VMSucursales() { ID = sucursal, UIDSUBCATEGORIA = Subcategoria });
            }
        }

        public void EliminaGiro(string UidSucursal)
        {
            Datos.EliminaGiro(UidSucursal);
        }

        public void EliminaCategoria(string UidSucursal)
        {
            Datos.EliminaCategoria(UidSucursal);
        }
        public void EliminaSubcategoria(string UidSucursal)
        {
            Datos.EliminaSubcategoria(UidSucursal);
        }



        public void RecuperarProductos(Guid UIDSUCURSAL, string UIDSECCION)
        {
            ListaDeproductos.Clear();
            foreach (DataRow item in Datos.obtenerProductos(UIDSUCURSAL.ToString(), UIDSECCION).Rows)
            {
                Guid uidproducto = new Guid(item["UidProducto"].ToString());
                ListaDeproductos.Add(new VMSucursales() { UidProducto = uidproducto });
            }
        }


        #region Zona de servicio

        public bool GuardaZona(Guid UidRelacion, Guid uidsucursal, Guid uidcolonia, string strParametro)
        {
            SUCURSAL = new Sucursal();
            return SUCURSAL.AgregaZonaDeServicio(UidRelacion, uidsucursal, uidcolonia, strParametro);

        }

        public string ObtenerSucursalSupervisor(Guid uid)
        {
            string Sucursal = string.Empty;
            odbSucursal = new DbSucursal();
            foreach (DataRow item in odbSucursal.ObtenerSucursalSupervisor(uid).Rows)
            {
                Sucursal = item["Identificador"].ToString();
            }
            return Sucursal;
        }

        public string obtenerSucursalRepartidor(Guid uid)
        {
            string Sucursal = string.Empty;
            odbSucursal = new DbSucursal();
            foreach (DataRow item in odbSucursal.ObtenerSucursalRepartidor(uid).Rows)
            {
                Sucursal = item["Identificador"].ToString();
            }
            return Sucursal;
        }

        public void EliminaZona(Guid uidsucursal)
        {
            Datos.EliminaZonaDeServicio(uidsucursal);
        }
        public void RecuperaZonaEntrega(Guid uidsucursal)
        {
            ListaDeColoniasEntrega.Clear();
            foreach (DataRow item in Datos.RecuperaZonaEntrega(uidsucursal).Rows)
            {
                ListaDeColoniasEntrega.Add(new VMSucursales() { UidRelacionRegistro = new Guid(item["UidRelacionZonaServicio"].ToString()), UidColonia = new Guid(item["uidcolonia"].ToString()), ID = new Guid(item["uidciudad"].ToString()), StrNombreColonia = item["Nombre"].ToString() });
            }
        }
        public void RecuperaZonaRecoleccion(Guid uidsucursal)
        {
            ListaDeColoniasRecolecta.Clear();
            foreach (DataRow item in Datos.RecuperaZonaRecoleccion(uidsucursal).Rows)
            {
                ListaDeColoniasRecolecta.Add(new VMSucursales() { UidRelacionRegistro = new Guid(item["UidZonaDeRecolecta"].ToString()), UidColonia = new Guid(item["uidcolonia"].ToString()), ID = new Guid(item["uidciudad"].ToString()), StrNombreColonia = item["Nombre"].ToString() });
            }
        }

        #endregion

        #region Validacion de estatus
        public bool VerificaEstatusSucursal(string UidSucursal)
        {
            bool resultado = false;
            if (Datos.VerificaSucursalActiva(UidSucursal).Rows.Count == 1)
            {
                resultado = true;
            }
            return resultado;
        }
        public string ObtenSucursalDeLicencia(string licencia)
        {
            string sucursal = "";
            foreach (DataRow item in Datos.ObtenerSucursalAPartirDeLicencia(licencia).Rows)
            {
                sucursal = item["Uidsucursal"].ToString();
            }
            return sucursal;
        }
        #endregion

        #region Supervisores
        public bool VerificaExistenciaDeSupervisor(string UidUsuario, string UidSucursal)
        {
            bool resultado = false;
            if (Datos.verificaExistenciaUsuario(UidUsuario, UidSucursal).Rows.Count == 1)
            {
                resultado = true;
            }
            return resultado;
        }
        #endregion


        #region Windows Presentation Foundation

        public void ObtenerRepartidoresYVehiculos(string UidLicencia)
        {
            ListaDeRepartidoresyVehiculosEnSucursal.Clear();
            foreach (DataRow item in Datos.ObtenerRepartidoresYVehiculos(UidLicencia).Rows)
            {
                if (!ListaDeRepartidoresyVehiculosEnSucursal.Exists(Rep => Rep.ID == new Guid(item["UidRelacionVehiculoUsuario"].ToString())))
                {
                    ListaDeRepartidoresyVehiculosEnSucursal.Add(
                        new VMSucursales()
                        {
                            ID = new Guid(item["UidRelacionVehiculoUsuario"].ToString()),
                            UidUsuario = new Guid(item["UidUsuario"].ToString()),
                            UidVehiculo = new Guid(item["UidVehiculo"].ToString()),
                            StrNombreRepartidor = item["Nombre"].ToString(),
                            usuario = item["Usuario"].ToString(),
                            StrVehiculo = item["VchModelo"].ToString()
                        });
                }
            }
        }
        public void AgregaAlistaDeRepartidores(Guid UidUsuario, Guid UidVehiculo)
        {
            if (!ListaDeRepartidoresyVehiculosEnSucursal.Exists(Rep => Rep.UidUsuario == UidUsuario && Rep.UidVehiculo == UidVehiculo))
            {
                SUCURSAL = new Sucursal();
                Guid Uid = Guid.NewGuid();
                SUCURSAL.GuardarRepartidor(UidUsuario, UidVehiculo, Uid);
                ListaDeRepartidoresyVehiculosEnSucursal.Add(new VMSucursales() { ID = Uid, UidUsuario = UidUsuario, UidVehiculo = UidVehiculo });
            }
        }
        public void EliminaRegistroListaRepartidores(Guid idRegistro)
        {
            odbSucursal = new DbSucursal();
            if (ListaDeRepartidoresyVehiculosEnSucursal.Exists(Rep => Rep.ID == idRegistro))
            {
                var objeto = ListaDeRepartidoresyVehiculosEnSucursal.Find(Rep => Rep.ID == idRegistro);
                odbSucursal.EliminarRegistroRepartidoresVehiculo(idRegistro);
                ListaDeRepartidoresyVehiculosEnSucursal.Remove(objeto);
            }
        }
        /// <summary>
        /// Busqueda de el tipo de empresa por licencia,si devuelve TRUE la el tipo es suministradora
        /// </summary>
        /// <param name="UidLicencia"></param>
        /// <returns></returns>
        public bool ObtenerElTipoDeSucursal(string UidLicencia)
        {
            bool Resultado = false;
            if (Datos.ObtenerElTipoDeEmpresa(UidLicencia).Rows.Count == 1)
            {
                Resultado = true;
            }
            return Resultado;
        }
        public void AsignarOrdenRepartidor( Guid UidTurnoRepartidor, Guid UidSucursal , Guid UidOrdenRepartidor)
        {
            try
            {
                SUCURSAL = new Sucursal();
                SUCURSAL.AsingaOrdenRepartidor(UidTurnoRepartidor, UidSucursal, UidOrdenRepartidor);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void ObtenerOrdenesAsignadasARepartidor(string licencia)
        {
            ListaDeOrdenesAsignadas.Clear();
            odbSucursal = new DbSucursal();
            string EstatusRepartidor = string.Empty;
            foreach (DataRow item in odbSucursal.ObtenerOrdenesAsignadasARepartir(licencia).Rows)
            {
                EstatusRepartidor = string.Empty;
                if (item["EstatusRepartidor"].ToString().ToUpper() != "7DA3A42F-2271-47B4-B9B8-EDD311F56864" && item["EstatusRepartidor"].ToString().ToUpper() != "12748F8A-E746-427D-8836-B54432A38C07")
                {
                    switch (item["EstatusRepartidor"].ToString().ToUpper())
                    {
                        case "A42B2588-D650-4DD9-829D-5978C927E2ED":
                            EstatusRepartidor = "Confirmada";
                            break;
                        case "B6791F2C-FA16-40C6-B5F5-123232773612":
                            EstatusRepartidor = "Recolectada";
                            break;
                        case "12748F8A-E746-427D-8836-B54432A38C07":
                            EstatusRepartidor = "Rechazada";
                            break;
                        case "6294DACE-C9D1-4F9F-A942-FF12B6E7E957":
                            EstatusRepartidor = "Pendiente";
                            break;
                        default:
                            break;
                    }
                    var objeto = new VMSucursales()
                    {
                        ID = new Guid(item["UidRelacionOrdenRepartidor"].ToString()),
                        UidUsuario = new Guid(item["UidUsuario"].ToString()),
                        StrNombreRepartidor = item["Nombre"].ToString(),
                        IDENTIFICADOR = item["Identificador"].ToString(),
                        lngFolio = long.Parse(item["IntFolio"].ToString()),
                        UidOrden = new Guid(item["UidOrden"].ToString()),
                        FechaAsignacion = item["DtmFecha"].ToString(),
                        usuario = item["Usuario"].ToString(),
                        MTotal = double.Parse(item["MTotalSucursal"].ToString()),
                        StrEstatusRepartidor = EstatusRepartidor
                    };
                    ListaDeOrdenesAsignadas.Add(objeto);
                }
            }
        }

        public void EliminarRelacionRepartidorOrdenAsignada(Guid UidRegistro)
        {
            if (ListaDeOrdenesAsignadas.Exists(ord => ord.ID == UidRegistro))
            {
                var objeto = ListaDeOrdenesAsignadas.Find(ord => ord.ID == UidRegistro);
                ListaDeOrdenesAsignadas.Remove(objeto);
            }
        }

        public void ObtenerContratosDeSucursal(int tipodeEmpresa, Guid uidSucursal, Guid UidColonia = new Guid())
        {
            odbSucursal = new DbSucursal();
            foreach (DataRow item in odbSucursal.obtenerContratosSucursal(tipodeEmpresa, uidSucursal).Rows)
            {
                Guid Suministradora = new Guid();
                Guid Distribuidora = new Guid();
                if (tipodeEmpresa == 1)
                {
                    Suministradora = new Guid(item["UidSucursalSuministradora"].ToString());
                    Distribuidora = new Guid(item["UidSucursalDistribuidora"].ToString());
                    if (!ListaDeSucursalesDeContrato.Exists(Cont => Cont.ID == Distribuidora))
                    {
                        BuscarSucursalesContrato(UidSucursal: Distribuidora, Visibilidad: 0, tipoDeEmpresa: tipodeEmpresa, StrTipoDeBusqueda: "Contrato");
                    }
                }
                else if (tipodeEmpresa == 2)
                {
                    Suministradora = new Guid(item["UidSucursalSuministradora"].ToString());
                    Distribuidora = new Guid(item["UidSucursalDistribuidora"].ToString());
                    if (!ListaDeSucursalesDeContrato.Exists(Cont => Cont.ID == Suministradora))
                    {
                        BuscarSucursalesContrato(UidSucursal: Distribuidora, Visibilidad: 0, tipoDeEmpresa: tipodeEmpresa, IdColonia: UidColonia, StrTipoDeBusqueda: "Contrato");
                    }
                }

            }
        }
        #endregion
        #endregion
    }
}
