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
        #endregion
    }
}
