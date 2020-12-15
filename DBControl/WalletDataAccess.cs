using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    /// <summary>
    /// Acceso de datos para la tabla Monedero
    /// </summary>
    public class WalletDataAccess
    {
        /// <summary>
        /// Conexion a la base de datos
        /// </summary>
        protected readonly Conexion dbConexion;

        public WalletDataAccess()
        {
            this.dbConexion = new Conexion();
        }

        public DataTable GetByUserId(Guid uidUser)
        {
            string query = $@"
                SELECT
                    M.[UidMonedero],
                    M.[UidUsuario],
                    M.[MMonto],
                    M.[DtmFechaDeCreacion]
                FROM [Monedero] AS M
                WHERE M.UidUsuario = '{uidUser.ToString()}'";

            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }
    }
}
