using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBControl;
using Modelo;

namespace VistaDelModelo
{
    public class VMTurno
    {
        #region Propiedades
        Turno oTurno;
        private Guid _UidLiquidacion;

        public Guid UidLiquidacion
        {
            get { return _UidLiquidacion; }
            set { _UidLiquidacion = value; }
        }
        private string _strNombreEmpresa;

        public string StrNombreEmpresa
        {
            get { return _strNombreEmpresa; }
            set { _strNombreEmpresa = value; }
        }

        private long _LngFolio;
        public long LngFolio
        {
            get { return _LngFolio; }
            set { _LngFolio = value; }
        }

        private long _LngFolioGeneral;

        public long LngFolioGeneral
        {
            get { return _LngFolioGeneral; }
            set { _LngFolioGeneral = value; }
        }

        private decimal _mFondo;

        public decimal DFondoRepartidor
        {
            get { return _mFondo; }
            set { _mFondo = value; }
        }

        private DateTime _HoraIncio;
        public DateTime DtmHoraInicio
        {
            get { return _HoraIncio; }
            set { _HoraIncio = value; }
        }

        private DateTime _HoraFin;
        public DateTime DtmHoraFin
        {
            get { return _HoraFin; }
            set { _HoraFin = value; }
        }

        private Guid _UidTurno;
        public Guid UidTurno
        {
            get { return _UidTurno; }
            set { _UidTurno = value; }
        }

        private Guid _UidUsuario;

        public Guid UidUsuario
        {
            get { return _UidUsuario; }
            set { _UidUsuario = value; }
        }


        //Propiedades para el control del turno
        private decimal _DTotal;

        public decimal DTotal
        {
            get { return _DTotal; }
            set { _DTotal = value; }
        }

        private int _intTotalOrdenes;

        public int intTotalOrdenes
        {
            get { return _intTotalOrdenes; }
            set { _intTotalOrdenes = value; }
        }


        private decimal _DTotalSucursal;

        public decimal DTotalSucursal
        {
            get { return _DTotalSucursal; }
            set { _DTotalSucursal = value; }
        }

        private decimal _DPropina;

        public decimal DPropina
        {
            get { return _DPropina; }
            set { _DPropina = value; }
        }
        private decimal _EfectivoActual;

        public decimal DEfectivoActual
        {
            get { return _EfectivoActual; }
            set { _EfectivoActual = value; }
        }

        private decimal _DeGanancias;

        public decimal DGanancias
        {
            get { return _DeGanancias; }
            set { _DeGanancias = value; }
        }
        private decimal _DeLiquidacion;

        public decimal DLiquidacion
        {
            get { return _DeLiquidacion; }
            set { _DeLiquidacion = value; }
        }
        private decimal _DRecarga;

        public decimal DRecarga
        {
            get { return _DRecarga; }
            set { _DRecarga = value; }
        }

        private decimal _DPagosASucursal;

        public decimal DPagosASucursal
        {
            get { return _DPagosASucursal; }
            set { _DPagosASucursal = value; }
        }

        private int _IntCantidadDePagos;

        public int IntCantidadDePagos
        {
            get { return _IntCantidadDePagos; }
            set { _IntCantidadDePagos = value; }
        }

        private decimal _DTotalEnvio;

        public decimal DTotalEnvio
        {
            get { return _DTotalEnvio; }
            set { _DTotalEnvio = value; }
        }
        private long _lngCantidad;

        public long CantiadDeOrdenes
        {
            get { return _lngCantidad; }
            set { _lngCantidad = value; }
        }
        private List<VMTurno> _ListaDeTurnos;

        public List<VMTurno> ListaDeTurnos
        {
            get { return _ListaDeTurnos; }
            set { _ListaDeTurnos = value; }
        }

        private List<VMTurno> _ListaDeordenes;

        public List<VMTurno> ListaDeInformacionDeTurno
        {
            get { return _ListaDeordenes; }
            set { _ListaDeordenes = value; }
        }

        private List<VMTurno> _ListaDeLiquidaciones;

        public List<VMTurno> ListaDeLiquidaciones
        {
            get { return _ListaDeLiquidaciones; }
            set { _ListaDeLiquidaciones = value; }
        }


        private List<VMTurno> _ListaDeRepartidores;

        public List<VMTurno> ListaDeRepartidores
        {
            get { return _ListaDeRepartidores; }
            set { _ListaDeRepartidores = value; }
        }

        private string _strAccionTurnoRepartidor;

        public string StrAccionTurnoRepartidor
        {
            get { return _strAccionTurnoRepartidor; }
            set { _strAccionTurnoRepartidor = value; }
        }


        #region Variables para reportes
        private int _IntComisionSistema;

        public int IntComisionSistema
        {
            get { return _IntComisionSistema; }
            set { _IntComisionSistema = value; }
        }
        private decimal _DGananciasSucursal;

        public decimal DGananciasSucursal
        {
            get { return _DGananciasSucursal; }
            set { _DGananciasSucursal = value; }
        }
        private decimal _DPrecioOrden;

        public decimal DPrecioOrden
        {
            get { return _DPrecioOrden; }
            set { _DPrecioOrden = value; }
        }
        private decimal _DPagoDeComision;

        public decimal DPagoDeComision
        {
            get { return _DPagoDeComision; }
            set { _DPagoDeComision = value; }
        }
        private string _StrTipoDePagoDeOrden;

        public string StrTipoDePagoDeOrden
        {
            get { return _StrTipoDePagoDeOrden; }
            set { _StrTipoDePagoDeOrden = value; }
        }
        private string _StrEmpresaDistribuidora;

        public string StrEmpresaDistribuidora
        {
            get { return _StrEmpresaDistribuidora; }
            set { _StrEmpresaDistribuidora = value; }
        }
        private string _strNombreSucursal;

        public string strNombreSucursal
        {
            get { return _strNombreSucursal; }
            set { _strNombreSucursal = value; }
        }

        private bool _BPagoAlRecolectar;

        public bool BPagoAlRecolectar
        {
            get { return _BPagoAlRecolectar; }
            set { _BPagoAlRecolectar = value; }
        }

        #endregion


        #region Informacion de usuario
        private string _strNombre;
        private Conexion oConexion;
        private string _strUsuario;

        public string strUsuario
        {
            get { return _strUsuario; }
            set { _strUsuario = value; }
        }


        public string StrNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }


        #endregion

        #endregion


        #region Metodos

        public void InformacionTurnoCallCenter(Guid uidusuario)
        {
            try
            {
                oTurno = new Turno() { UidUsuario = UidUsuario };
                foreach (DataRow item in oTurno.vertificaUltimoTurnoCallCenter(uidusuario).Rows)
                {
                    UidTurno = new Guid(item["UidTunoCallCenter"].ToString());
                    LngFolio = long.Parse(item["LngFolio"].ToString());
                    this.UidUsuario = new Guid(item["UidUsuario"].ToString());
                    DtmHoraInicio = DateTime.Parse(item["DtmHoraInicio"].ToString());
                    if (!string.IsNullOrEmpty(item["DtmHoraFin"].ToString()))
                    {
                        DtmHoraFin = DateTime.Parse(item["DtmHoraFin"].ToString());
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool TurnoYaabiertoEnTurnoCallCenter(Guid uidSucursal, string UidUsuario)
        {
            oTurno = new Turno();
            bool resultado = false;
            if (oTurno.VerificaExistenciaTurnoSucursalConTurnoCallCenter(uidSucursal, UidUsuario).Rows.Count > 0)
            {
                resultado = true;
            }
            return resultado;
        }

        public void EstatusTurno(Guid UidUsuario, Guid UidTurno)
        {
            oTurno = new Turno() { UidTurno = UidTurno, UidUsuario = UidUsuario };
            oTurno.CreaOActualiza();
        }
        public void AgregaEstatusTurnoRepartidor(string UidTurnoRepartidor, string UidEstatusTurno)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_AgregaEstatusTurnoRepartidor";
                //Dato1
                cmd.Parameters.Add("@UidTurnoReparatidor", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidTurnoReparatidor"].Value = new Guid(UidTurnoRepartidor);

                cmd.Parameters.Add("@UidEstatusTurnoRepartidor", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidEstatusTurnoRepartidor"].Value = new Guid(UidEstatusTurno);

                oConexion = new Conexion();
                //Mandar comando a ejecución
                oConexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public void LiquidarARepartidor(string strTurnoRepartidor, string StrTurnoDistribuidora, string strMontoALiquidar, string StrAccionRepartidor)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_AgregarLiquidacionRepartidor";
                //Dato1
                cmd.Parameters.Add("@UidTurnoDistribuidora", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidTurnoDistribuidora"].Value = new Guid(StrTurnoDistribuidora);

                cmd.Parameters.Add("@UidTurnoRepartidor", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidTurnoRepartidor"].Value = new Guid(strTurnoRepartidor);

                cmd.Parameters.Add("@MMontoALiquidar", SqlDbType.Money);
                cmd.Parameters["@MMontoALiquidar"].Value = decimal.Parse(strMontoALiquidar);

                cmd.Parameters.Add("@StrAccionTurno", SqlDbType.VarChar, 15);
                cmd.Parameters["@StrAccionTurno"].Value = StrAccionRepartidor;

                oConexion = new Conexion();
                //Mandar comando a ejecución
                oConexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ObtenerEstatusUltimoTurno(object UidRepartidor)
        {
            oTurno = new Turno();
            string resultado = Guid.Empty.ToString();
            foreach (DataRow item in oTurno.ObtenerEstatusUltimoTurnoRepartidor(UidRepartidor).Rows)
            {
                resultado = item["EstatusTurno"].ToString();
            };
            return resultado;
        }

        public string ObtenerMontoAPortar(string uidRepartidor)
        {
            oTurno = new Turno();
            string registro = string.Empty;
            foreach (DataRow item in oTurno.ObtenerMontoAsignado(uidRepartidor).Rows)
            {
                UidLiquidacion = new Guid(item["UidRepartidor"].ToString());
                registro = decimal.Parse(item["MMontoMaximo"].ToString()).ToString("N2");
            }
            return registro;
        }

        /// <summary>
        /// Consulta el ultimo turno del repartidor.
        /// </summary>
        /// <returns></returns>
        public void ConsultaUltimoTurno(Guid UidUsuario)
        {
            try
            {
                oTurno = new Turno() { UidUsuario = UidUsuario };
                foreach (DataRow item in oTurno.VerificaEstatusDeTurno().Rows)
                {
                    UidTurno = new Guid(item["UidTurnoRepartidor"].ToString());
                    LngFolio = long.Parse(item["LngFolio"].ToString());
                    this.UidUsuario = new Guid(item["UidUsuario"].ToString());
                    DtmHoraInicio = DateTime.Parse(item["DtmHoraInicio"].ToString());

                    DFondoRepartidor = decimal.Parse(decimal.Parse(item["mfondo"].ToString()).ToString("N2"));
                    if (!string.IsNullOrEmpty(item["DtmHoraFin"].ToString()))
                    {
                        DtmHoraFin = DateTime.Parse(item["DtmHoraFin"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ConsultarUltimoTurnoDistribuidora(string UidLicencia)
        {
            try
            {
                oTurno = new Turno() { UidUsuario = UidUsuario };
                foreach (DataRow item in oTurno.VerificaUltimoTurnoDistribuidora(UidLicencia).Rows)
                {
                    UidTurno = new Guid(item["UidTurnodistribuidora"].ToString());
                    LngFolio = long.Parse(item["LngFolio"].ToString());
                    this.UidUsuario = new Guid(item["UidUsuario"].ToString());
                    DtmHoraInicio = DateTime.Parse(item["DtmHoraInicio"].ToString());
                    if (!string.IsNullOrEmpty(item["DtmHoraFin"].ToString()))
                    {
                        DtmHoraFin = DateTime.Parse(item["DtmHoraFin"].ToString());
                    }
                    if (string.IsNullOrEmpty(item["liquidaciones"].ToString()))
                    {
                        DLiquidacion = 0;
                    }
                    else
                    {
                        DLiquidacion = decimal.Parse(item["liquidaciones"].ToString());
                    }
                    if (string.IsNullOrEmpty(item["recargas"].ToString()))
                    {
                        DRecarga = 0;
                    }
                    else
                    {
                        DRecarga = decimal.Parse(item["recargas"].ToString());
                    }
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ConsultarHistorico(Guid UidUsuario)
        {
            try
            {
                oTurno = new Turno() { UidUsuario = UidUsuario };
                ListaDeTurnos = new List<VMTurno>();
                foreach (DataRow item in oTurno.HistoricoTurno(UidUsuario).Rows)
                {
                    ListaDeTurnos.Add(new VMTurno()
                    {
                        UidTurno = new Guid(item["UidTurnoRepartidor"].ToString()),
                        LngFolio = long.Parse(item["LngFolio"].ToString()),
                        UidUsuario = new Guid(item["UidUsuario"].ToString()),
                        DtmHoraInicio = DateTime.Parse(item["DtmHoraInicio"].ToString()),
                        DtmHoraFin = DateTime.Parse(item["DtmHoraFin"].ToString()),
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void InformacionDeOrdenesPorTuno(Guid UidTurno)
        {
            try
            {
                oTurno = new Turno() { UidUsuario = UidUsuario };
                foreach (DataRow item in oTurno.InformacionDeOrdenes(UidTurno).Rows)
                {
                    if (!string.IsNullOrEmpty(item["TotalOrdenes"].ToString()))
                    {
                        intTotalOrdenes = int.Parse(item["TotalOrdenes"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["totalEnvio"].ToString()))
                    {
                        DTotalEnvio = decimal.Parse(item["totalEnvio"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["totalSucursal"].ToString()))
                    {
                        DTotalSucursal = decimal.Parse(item["totalSucursal"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["propina"].ToString()))
                    {
                        DPropina = decimal.Parse(item["propina"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["Efectivo"].ToString()))
                    {
                        DEfectivoActual = decimal.Parse(item["Efectivo"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["liquidacion"].ToString()))
                    {
                        DLiquidacion = decimal.Parse(item["liquidacion"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["ganancias"].ToString()))
                    {
                        DGanancias = decimal.Parse(item["ganancias"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["PagosSucursales"].ToString()))
                    {
                        DPagosASucursal = decimal.Parse(item["PagosSucursales"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["recarga"].ToString()))
                    {
                        DRecarga = decimal.Parse(item["recarga"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["CantidadDePagos"].ToString()))
                    {
                        intTotalOrdenes = int.Parse(item["CantidadDePagos"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void InformacionHistoricoOrdenesTurno(Guid UidTurno)
        {
            try
            {
                oTurno = new Turno() { UidUsuario = UidUsuario };
                ListaDeTurnos = new List<VMTurno>();
                foreach (DataRow item in oTurno.HistoricoOrdenesTurno(UidTurno).Rows)
                {
                    ListaDeTurnos.Add(new VMTurno()
                    {
                        LngFolio = long.Parse(item["IntFolio"].ToString()),
                        DTotalEnvio = decimal.Parse(item["MCosto"].ToString()),
                        DTotalSucursal = decimal.Parse(item["MTotalSucursal"].ToString()),
                        DPropina = decimal.Parse(item["MPropina"].ToString())
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool VerificaTurnoCerrado(string UidTurnoRepartidor)
        {

            bool resut = false;
            try
            {
                oTurno = new Turno();
                if (oTurno.verificaTurnoCerradoRepartidor(UidTurnoRepartidor).Rows.Count > 0)
                {
                    resut = true;
                }
            }
            catch (Exception)
            {

                throw;
            }


            return resut;
        }

        public decimal VerFondoRepartidor(string UidUsuario)
        {
            decimal mfondo = 0;
            oTurno = new Turno();
            foreach (DataRow item in oTurno.ObtenerFondo(UidUsuario).Rows)
            {
                mfondo = decimal.Parse(decimal.Parse(item["MFondo"].ToString()).ToString("N2"));
            }
            return mfondo;
        }
        public void RepartidoresConFondoAEntregar(string licencia)
        {
            oTurno = new Turno();
            try
            {
                foreach (DataRow item in oTurno.ObtenerUltimoTurnoDeRepartidore(licencia).Rows)
                {
                    decimal total = 0;
                    if (VerificaTurnoCerrado(item["UidTurnoRepartidor"].ToString()))
                    {
                        total = VerFondoRepartidor(item["UidUsuario"].ToString());
                        if (total > 0)
                        {
                            VMTurno usuario = new VMTurno()
                            {
                                UidUsuario = new Guid(item["UidUsuario"].ToString()),
                                UidTurno = new Guid(item["UidTurnoRepartidor"].ToString()),
                                StrNombre = item["Nombre"].ToString(),
                                DTotalEnvio = total
                            };
                            if (!ListaDeRepartidores.Exists(u => u.UidUsuario == usuario.UidUsuario))
                            {
                                ListaDeRepartidores.Add(usuario);
                            }
                            else
                            {
                                var registro = ListaDeRepartidores.Find(u => u.UidUsuario == usuario.UidUsuario);

                                if (registro.DTotalEnvio == 0)
                                {
                                    registro.DTotalEnvio = registro.DTotalEnvio + total;
                                }
                                else
                                {
                                    registro.DTotalEnvio = registro.DTotalEnvio - total;
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ObtenerRepartidoresALiquidar(string UidLicencia)
        {
            ListaDeRepartidores = new List<VMTurno>();
            oTurno = new Turno();
            foreach (DataRow item in oTurno.ObtenerRepartidoresALiquidar(UidLicencia).Rows)
            {
                //Varifica que este activo el campo
                if (item["EstatusTurno"].ToString().ToUpper() == "AE28F243-AA0D-43BD-BF10-124256B75B00" || item["EstatusTurno"].ToString().ToUpper() == "B03E3407-F76D-4DFA-8BF9-7F059DC76141")
                {

                    string estatus = "";
                    switch (item["EstatusTurno"].ToString().ToUpper())
                    {
                        case "AE28F243-AA0D-43BD-BF10-124256B75B00":
                            estatus = "Liquidando";
                            break;
                        case "B03E3407-F76D-4DFA-8BF9-7F059DC76141":
                            estatus = "Recargando";
                            break;
                        default:
                            break;
                    }
                    decimal total = 0;
                    if (!string.IsNullOrEmpty(item["Ordenes"].ToString()))
                    {
                        var MVTurno = new VMTurno();
                        MVTurno.ConsultaUltimoTurno(new Guid(item["UidUsuario"].ToString()));
                        decimal MontoFondo = MVTurno.DFondoRepartidor;
                        total = decimal.Parse(decimal.Parse(item["Ordenes"].ToString()).ToString("N2"));

                        if (MontoFondo < total)
                        {
                            total = total - MontoFondo;
                        }
                        else
                        {
                            total += MontoFondo;
                        }
                    }

                    VMTurno usuario = new VMTurno()
                    {
                        UidUsuario = new Guid(item["UidUsuario"].ToString()),
                        UidTurno = new Guid(item["UidTurnoRepartidor"].ToString()),
                        StrNombre = item["Nombre"].ToString(),
                        DTotalEnvio = total,
                        StrAccionTurnoRepartidor = estatus
                    };
                    if (!ListaDeRepartidores.Exists(u => u.UidUsuario == UidUsuario))
                    {
                        ListaDeRepartidores.Add(usuario);
                    }
                }
            }
            foreach (DataRow rep in oTurno.ObtenerRepartidoresARecargar(UidLicencia).Rows)
            {
                //Varifica que este activo el campo
                if (rep["EstatusTurno"].ToString().ToUpper() == "AE28F243-AA0D-43BD-BF10-124256B75B00" || rep["EstatusTurno"].ToString().ToUpper() == "B03E3407-F76D-4DFA-8BF9-7F059DC76141")
                {

                    string estatus = "";
                    switch (rep["EstatusTurno"].ToString().ToUpper())
                    {
                        case "AE28F243-AA0D-43BD-BF10-124256B75B00":
                            estatus = "Liquidando";
                            break;
                        case "B03E3407-F76D-4DFA-8BF9-7F059DC76141":
                            estatus = "Recargando";
                            break;
                        default:
                            break;
                    }
                    decimal total = 0;
                    decimal TotalEfectivo = 0;
                    decimal TotalEnvio = 0;
                    decimal TotalPropina = 0;
                    if (!string.IsNullOrEmpty(rep["Ordenes"].ToString()))
                    {
                        TotalEfectivo = decimal.Parse(rep["Ordenes"].ToString());
                        TotalEnvio = (decimal.Parse(decimal.Parse(rep["envio"].ToString()).ToString("N2")));
                        TotalPropina = decimal.Parse(decimal.Parse(rep["propina"].ToString()).ToString("N2"));
                        var MVTurno = new VMTurno();
                        MVTurno.ConsultaUltimoTurno(new Guid(rep["UidUsuario"].ToString()));
                        decimal MontoFondo = MVTurno.DFondoRepartidor;
                        if (MontoFondo > TotalEfectivo)
                        {
                            total = TotalEfectivo;
                        }
                        else
                        {
                            total = TotalEfectivo - MontoFondo;
                        }
                    }

                    VMTurno usuario = new VMTurno()
                    {
                        UidUsuario = new Guid(rep["UidUsuario"].ToString()),
                        UidTurno = new Guid(rep["UidTurnoRepartidor"].ToString()),
                        StrNombre = rep["Nombre"].ToString(),
                        DTotalEnvio = decimal.Parse(total.ToString("N2")),
                        StrAccionTurnoRepartidor = estatus
                    };
                    if (!ListaDeRepartidores.Exists(u => u.UidUsuario == UidUsuario))
                    {
                        ListaDeRepartidores.Add(usuario);
                    }
                }
            }
        }
        public void ConsultarTurnoSuministradoraDesdeCallCenter(string UidLicencia, Guid UidTurnoCallCenter)
        {
            try
            {
                oTurno = new Turno() { UidUsuario = UidUsuario };
                foreach (DataRow item in oTurno.VerificaTurnoSuministradoraCallCenter(UidLicencia, UidTurnoCallCenter).Rows)
                {
                    UidTurno = new Guid(item["UidTurnoSuministradora"].ToString());
                    LngFolio = long.Parse(item["LngFolio"].ToString());
                    this.UidUsuario = new Guid(item["UidUsuario"].ToString());
                    DtmHoraInicio = DateTime.Parse(item["DtmHoraInicio"].ToString());
                    if (!string.IsNullOrEmpty(item["DtmHoraFin"].ToString()))
                    {
                        DtmHoraFin = DateTime.Parse(item["DtmHoraFin"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ConsultarUltimoTurnoSuministradora(string UidLicencia)
        {
            try
            {
                oTurno = new Turno() { UidUsuario = UidUsuario };
                foreach (DataRow item in oTurno.VerificaUltimoTurnoSuministradora(UidLicencia).Rows)
                {
                    UidTurno = new Guid(item["UidTurnoSuministradora"].ToString());
                    LngFolio = long.Parse(item["LngFolio"].ToString());
                    this.UidUsuario = new Guid(item["UidUsuario"].ToString());
                    DtmHoraInicio = DateTime.Parse(item["DtmHoraInicio"].ToString());
                    StrNombreEmpresa = item["NombreComercial"].ToString();
                    if (!string.IsNullOrEmpty(item["DtmHoraFin"].ToString()))
                    {
                        DtmHoraFin = DateTime.Parse(item["DtmHoraFin"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void TurnoDistribuidora(Guid uid, Guid uidTurnoDistribuidor)
        {
            oTurno = new Turno()
            {
                UidTurno = uidTurnoDistribuidor,
                UidUsuario = uid
            };
            oTurno.TurnoDistribuidora();
        }

        public void TurnoSuministradora(Guid uid, Guid uidTurnoDistribuidor = new Guid(), string licencia = "")
        {
            if (string.IsNullOrEmpty(licencia))
            {
                oTurno = new Turno()
                {
                    UidTurno = uidTurnoDistribuidor,
                    UidUsuario = uid
                };
            }
            else
            {
                oTurno = new Turno()
                {
                    UidLicencia = new Guid(licencia),
                    UidUsuario = uid
                };
            }
            oTurno.TurnoSuministradora();
        }
        public void TurnoCallCenter(Guid UidUsuario)
        {
            try
            {
                oTurno = new Turno();
                oTurno.UidUsuario = UidUsuario;
                oTurno.TurnoCallCenter();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RelacionTurnoSuministradoraCallcenter(Guid UidTurnoSuministradora, Guid UidTurnoCallcenter)
        {
            try
            {
                oTurno = new Turno();
                oTurno.RelacionTurnoSuministradoraCallCenter(UidTurnoSuministradora, UidTurnoCallcenter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ObtenerLiquidacionesDeTurnoDistribuidora(string UidTurnoDistribuidora)
        {
            oTurno = new Turno();
            return oTurno.ObtenerLiquidaciones(UidTurnoDistribuidora);
        }
        public string ObtenerUltimoEstatusTurno(string UidTurnoRepartidor)
        {
            oTurno = new Turno();
            string resultado = "";
            foreach (DataRow item in oTurno.ObtenerUltimoEstatusDeTurno(UidTurnoRepartidor).Rows)
            {
                resultado = item["EstatusTurno"].ToString();
            };
            return resultado;
        }

        public void AgregarInformacionRepartidor(Guid uidUsuario, string MMonto, int PorcentajeGanancias)
        {
            oTurno = new Turno();
            oTurno.AgregarInformacionRepartidor(uidUsuario, MMonto, PorcentajeGanancias);
        }

        public void ObtenBitacoraTurno(string UidLicencia)
        {
            oConexion = new Conexion();
            string query = "select  u.UidUsuario, UPPER(u.Nombre) + ' ' + UPPER(u.ApellidoPaterno) as Nombre, u.Usuario, sum(os.MTotalSucursal) as TotalOrdenes, sum(t.MCosto) as TotalEnvio, count(orep.UidOrden) as OrdenesEfectuadas, lr.MMontoLiquidado from LiquidacionRepartidor lr  inner join TurnoRepartidor tr on tr.UidTurnoRepartidor = lr.UidTurnoRepartidor inner join Usuarios u on tr.UidUsuario = u.UidUsuario inner join TurnoDistribuidora td on td.UidTurnoDistribuidora = lr.UidTurnoDistribuidora inner join OrdenRepartidor orep on orep.LngFolioLiquidacion = lr.LngFolio inner join OrdenTarifario ot on ot.UidRelacionOrdenTarifario = orep.UidOrden inner join OrdenSucursal os on os.UidRelacionOrdenSucursal = ot.UidOrden inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario where td.UidTurnoDistribuidora = '" + UidLicencia + "' group by u.UidUsuario, u.Nombre, u.ApellidoPaterno, u.Usuario, lr.MMontoLiquidado";

            ListaDeTurnos = new List<VMTurno>();
            foreach (DataRow item in oConexion.Consultas(query).Rows)
            {
                if (ListaDeTurnos.Exists(l => l.UidUsuario == new Guid(item["UidUsuario"].ToString())))
                {
                    var objeto = ListaDeTurnos.Find(x => x.UidUsuario == new Guid(item["UidUsuario"].ToString()));

                    objeto.CantiadDeOrdenes = objeto.CantiadDeOrdenes + long.Parse(item["OrdenesEfectuadas"].ToString());
                    objeto.DTotalEnvio = objeto.DTotalEnvio + decimal.Parse(item["TotalEnvio"].ToString());
                    objeto.DTotalSucursal = objeto.DTotalSucursal + decimal.Parse(item["TotalOrdenes"].ToString());
                    objeto.DTotal = objeto.DTotal + decimal.Parse(item["MMontoLiquidado"].ToString());
                }
                else
                {
                    ListaDeTurnos.Add(new VMTurno()
                    {
                        UidUsuario = new Guid(item["UidUsuario"].ToString()),
                        CantiadDeOrdenes = long.Parse(item["OrdenesEfectuadas"].ToString()),
                        DTotalEnvio = decimal.Parse(item["TotalEnvio"].ToString()),
                        DTotalSucursal = decimal.Parse(item["TotalOrdenes"].ToString()),
                        DTotal = decimal.Parse(item["MMontoLiquidado"].ToString()),
                        StrNombre = item["Nombre"].ToString(),
                        strUsuario = item["Usuario"].ToString(),

                    });
                }
            }
        }

        public void ObtenerInformacionLiquidacionesTuno(string UidTurnoRepartidor)
        {
            oConexion = new Conexion();
            string query = "select s.Identificador,lr.DtmFechaRegistro,lr.MMontoLiquidado, u.Nombre from LiquidacionRepartidor lr inner join TurnoDistribuidora td on td.UidTurnoDistribuidora = lr.UidTurnoDistribuidora inner join Sucursales s on s.UidSucursal = td.UidSucursal inner join Usuarios u on u.UidUsuario = td.UidUsuario where lr.UidTurnoRepartidor = '" + UidTurnoRepartidor + "'";
            ListaDeLiquidaciones = new List<VMTurno>();
            foreach (DataRow item in oConexion.Consultas(query).Rows)
            {
                ListaDeLiquidaciones.Add(new VMTurno()
                {
                    StrNombre = item["Identificador"].ToString(),
                    strUsuario = item["Nombre"].ToString(),
                    DTotal = decimal.Parse(item["MMontoLiquidado"].ToString()),
                    DtmHoraInicio = DateTime.Parse(item["DtmFechaRegistro"].ToString())
                });
            }
        }

        public bool TurnoAbierto(Guid UidSucursal)
        {
            oConexion = new Conexion();
            bool resultado = false;
            string query = "select top 1 * from TurnoSuministradora ts where ts.DtmHoraFin is  null and ts.UidSucursal = '" + UidSucursal.ToString() + "'";
            if (oConexion.Consultas(query).Rows.Count > 0)
            {
                resultado = true;
            }
            return resultado;
        }

        public void InformacionDeCierreDeTurnoSucursalSuministradora(string StrTipoDeSucursal, string UidTurnoSuministradora = "", string UidLicencia = "")
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_ConsultarInformacionDeTurno";
                //Dato1
                cmd.Parameters.Add("@VchTipoDeEmpresa", SqlDbType.VarChar, 15);
                cmd.Parameters["@VchTipoDeEmpresa"].Value = StrTipoDeSucursal;

                if (!string.IsNullOrEmpty(UidTurnoSuministradora))
                {
                    cmd.Parameters.Add("@UidTurnoSucursal", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@UidTurnoSucursal"].Value = new Guid(UidTurnoSuministradora);
                }
                if (!string.IsNullOrEmpty(UidLicencia))
                {
                    cmd.Parameters.Add("@UidLicenciaSucursal", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@UidLicenciaSucursal"].Value = new Guid(UidLicencia);
                }

                oConexion = new Conexion();
                ListaDeInformacionDeTurno = new List<VMTurno>();

                //Mandar comando a ejecución
                if (StrTipoDeSucursal == "Suministradora")
                {
                    foreach (DataRow item in oConexion.Busquedas(cmd).Rows)
                    {
                        ListaDeInformacionDeTurno.Add(new VMTurno()
                        {
                            LngFolio = long.Parse(item["Orden"].ToString()),
                            IntComisionSistema = int.Parse(item["Comision"].ToString()),
                            DPagosASucursal = decimal.Parse(item["Cobrado"].ToString()),
                            DGananciasSucursal = decimal.Parse(item["Ingreso"].ToString()),
                            DPrecioOrden = decimal.Parse(item["PagoDeComision"].ToString()) + decimal.Parse(item["Ingreso"].ToString()),
                            DPagoDeComision = decimal.Parse(item["PagoDeComision"].ToString()),
                            StrTipoDePagoDeOrden = item["TipoDePago"].ToString(),
                            StrEmpresaDistribuidora = item["EmpresaDistribuidora"].ToString(),
                            BPagoAlRecolectar = bool.Parse(item["PagoAlRecolectar"].ToString())
                        });
                    }
                }
                else if(StrTipoDeSucursal == "Distribuidora")
                {
                    foreach (DataRow item in oConexion.Busquedas(cmd).Rows)
                    {
                        ListaDeInformacionDeTurno.Add(new VMTurno()
                        {
                            LngFolio = long.Parse(item["Orden"].ToString()),
                            LngFolioGeneral = long.Parse(item["FolioOrdenDistribuidora"].ToString()),
                            DPagosASucursal = decimal.Parse(item["MontoSucursal"].ToString()),
                            DPagoDeComision = decimal.Parse(item["Pagado"].ToString()),                            
                            DGananciasSucursal = decimal.Parse(item["Ingresos"].ToString()),
                            DGanancias = decimal.Parse(item["GananciasRepartidor"].ToString()),
                            StrTipoDePagoDeOrden = item["TipoDePago"].ToString(),
                            StrEmpresaDistribuidora = item["Empresa"].ToString(),
                            strNombreSucursal = item["Sucursal"].ToString(),
                            strUsuario =item["Repartidor"].ToString(),
                            DPropina = decimal.Parse(item["Propina"].ToString()),
                            DTotalEnvio = decimal.Parse(item["Envio"].ToString())
                        });
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
