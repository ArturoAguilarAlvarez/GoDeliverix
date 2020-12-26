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

        public DataTable ReadAllUserTransactions(Guid uidUser, Guid? uidConcept = null, Guid? uidType = null)
        {
            string where = string.Empty;

            if (uidConcept.HasValue)
            {
                where += $" AND M.[UidConcepto] = '{uidConcept.ToString()}' ";
            }

            if (uidType.HasValue)
            {
                where += $" AND M.[UidTipoDeMovimiento] = '{uidType.ToString()}' ";
            }

            string query = $@"
                SELECT
                    M.[UidMovimiento] AS [Uid],
                    M.[DtmFechaRegistro] AS [Date],
                    M.[LngFolio] AS [Folio],
                    M.[MMonto] AS [Amount],
                    T.[UidTipoDeMovimiento] AS [UidType],
                    T.[VchNombre] AS [Type],
                    C.[UidConcepto] AS [UidConcept],
                    C.[VchNombre] AS [Concept]
                FROM [Movimientos] AS M
                    INNER JOIN [Monedero] AS W ON W.[UidMonedero] = M.[UidMonedero]
                    INNER JOIN [TipoDeMovimiento] AS T ON T.[UidTipoDeMovimiento] = M.[UidTipoDeMovimiento]
                    INNER JOIN [Conceptos] AS C ON C.[UidConcepto] = M.[UidConcepto]
                WHERE W.[UidUsuario] = '{uidUser.ToString()}' {where}";

            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }
    }
}
