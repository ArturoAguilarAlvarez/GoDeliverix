using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBControl;
namespace Modelo
{
    public class Turno
    {
        Conexion oConexion;
        DBTurno oDbTurno;

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

        private Guid _UidSucursal;

        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }


        #region Metodos
        public bool CreaOActualiza()
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ControlTurnoRepartidor";

                Comando.Parameters.Add("@UidTurnoRepartidor", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidTurnoRepartidor"].Value = UidTurno;

                Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidUsuario"].Value = UidUsuario;

                oConexion = new Conexion();
                resultado = oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        /// <summary>
        /// Verifica si el turno esta abierto(true) o cerrado(false)
        /// </summary>
        /// <returns></returns>
        public DataTable VerificaEstatusDeTurno()
        {
            oDbTurno = new DBTurno();
            return oDbTurno.VerificaUltimoTurno(UidUsuario);
        }
        /// <summary>
        /// Obtiene la informacion de las ordenes realizadas por turno
        /// </summary>
        /// <param name="uidTurno"></param>
        /// <returns></returns>
        public DataTable InformacionDeOrdenes(Guid uidTurno)
        {
            oDbTurno = new DBTurno();
            return oDbTurno.InformacionDeOrdenesPorTurno(uidTurno);
        }

        public DataTable HistoricoTurno(Guid uidUsuario)
        {
            oDbTurno = new DBTurno();
            return oDbTurno.HistoricoTurno(uidUsuario);
        }
        public DataTable HistoricoOrdenesTurno(Guid UidTurno)
        {
            oDbTurno = new DBTurno();
            return oDbTurno.HistoricoOrdenes(UidTurno);
        }
        public DataTable ObtenerRepartidoresALiquidar(string UidLicencia)
        {
            oDbTurno = new DBTurno();
            return oDbTurno.ObtenerRepartidoresParaLiquidar(UidLicencia);
        }
        /// <summary>
        /// Controla el turno en la empresa distribuidora
        /// </summary>
        public void TurnoDistribuidora()
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_ControlTurnoDistribuidora";
                //Dato1
                cmd.Parameters.Add("@UidTurnoDistribuidora", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidTurnoDistribuidora"].Value = UidTurno;

                cmd.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidUsuario"].Value = UidUsuario;


                oConexion = new Conexion();
                //Mandar comando a ejecución
                oConexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable ObtenerLiquidaciones(string uidTurnoDistribuidora)
        {
            oConexion = new Conexion();
            string query = "select td.UidTurnoDistribuidora, UPPER(u.Nombre) + ' ' + UPPER(u.ApellidoPaterno) as Nombre, u.Usuario, sum(os.MTotalSucursal) as TotalOrdenes,sum(t.MCosto) as TotalEnvio, count(orep.UidOrden) as OrdenesEfectuadas, lr.MMontoLiquidado  from LiquidacionRepartidor lr inner join TurnoRepartidor tr on tr.UidTurnoRepartidor = lr.UidTurnoRepartidor inner join Usuarios u on tr.UidUsuario = u.UidUsuario inner join TurnoDistribuidora td on td.UidTurnoDistribuidora = lr.UidTurnoDistribuidora inner join OrdenRepartidor orep on orep.LngFolioLiquidacion = lr.LngFolio inner join OrdenTarifario ot on ot.UidRelacionOrdenTarifario = orep.UidOrden inner join OrdenSucursal os on os.UidRelacionOrdenSucursal = ot.UidOrden inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario where  td.UidTurnoDistribuidora = '" + uidTurnoDistribuidora + "' group by td.UidTurnoDistribuidora, u.Nombre, u.ApellidoPaterno, u.Usuario, lr.MMontoLiquidado";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerUltimoEstatusDeTurno(string uidTurnoRepartidor)
        {
            oConexion = new Conexion();
            string query = "select dbo.GD_ObtenerUltimoEstatusTurnoRepartidor('"+ uidTurnoRepartidor + "') as EstatusTurno";
            return oConexion.Consultas(query);
        }
        /// <summary>
        /// Obtiene el estatus del ultimo turno del repartidor
        /// </summary>
        /// <param name="UidRepartidor"></param>
        /// <returns></returns>
        public DataTable ObtenerEstatusUltimoTurnoRepartidor(object UidRepartidor)
        {
            oConexion = new Conexion();
            string query = "select top 1 dbo.GD_ObtenerUltimoEstatusTurnoRepartidor(UidTurnoRepartidor) as EstatusTurno from TurnoRepartidor tr where tr.UidUsuario = '"+ UidRepartidor + "' order by LngFolio desc";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerMontoAsignado(string uidRepartidor)
        {
            oConexion = new Conexion();
            string query = "select * from Repartidor where UidUsuario = '" + uidRepartidor + "'";
            return oConexion.Consultas(query);
        }

        public void AgregarInformacionRepartidor(Guid uidUsuario, string mMonto)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_AgregarInformacionRepartidor";
                //Dato1
                cmd.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidUsuario"].Value = uidUsuario;

                cmd.Parameters.Add("@MMonto", SqlDbType.Money);
                cmd.Parameters["@MMonto"].Value = decimal.Parse(mMonto);

                oConexion = new Conexion();
                //Mandar comando a ejecución
                oConexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Verifica el si hay un turno abierto en la empresa distribuidora
        /// </summary>
        /// <param name="UidLicencia"></param>
        /// <returns></returns>
        public DataTable VerificaUltimoTurnoDistribuidora(string UidLicencia)
        {
            oConexion = new Conexion();
            string query = "select top 1 * from turnodistribuidora td " +
                "left join sucursallicencia sl on sl.UidSucursal = td.UidSucursal where sl.uidlicencia = '"+UidLicencia+"' order by td.DtmHoraInicio desc";
            return oConexion.Consultas(query);
        }

        public void TurnoSuministradora()
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_ControlTurnoSuministradora";
                //Dato1
                cmd.Parameters.Add("@UidTurnoSuministradora", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidTurnoSuministradora"].Value = UidTurno;

                cmd.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidUsuario"].Value = UidUsuario;

                oConexion = new Conexion();
                //Mandar comando a ejecución
                oConexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable VerificaUltimoTurnoSuministradora(object UidLicencia)
        {
            oConexion = new Conexion();
            string query = "select top 1 * from TurnoSuministradora td " +
                "left join sucursallicencia sl on sl.UidSucursal = td.UidSucursal where sl.uidlicencia = '" + UidLicencia + "' order by td.DtmHoraInicio desc";
            return oConexion.Consultas(query);
        }

        #endregion
    }
}
