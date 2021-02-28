using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    public class OfertaDataAccess
    {
        /// <summary>
        /// Conexion a la base de datos
        /// </summary>
        protected readonly Conexion dbConexion;

        public OfertaDataAccess()
        {
            this.dbConexion = new Conexion();
        }

        public DataTable ObtenerOfertasSucursal(string dia, Guid uidSucursal)
        {
            string query = @"
SELECT 
    DISTINCT O.UidOferta AS [Uid],
    O.VchNombre AS [Name],
    O.IntEstatus AS [Status],
    CASE WHEN D.UidDia IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS [Available]
FROM [Oferta] AS O
    INNER JOIN [DiaOferta] AS DO ON DO.UidOferta = O.UidOferta
    INNER JOIN [Dias] AS D ON D.UidDia = DO.UidDia
WHERE O.IntEstatus = 1 AND O.Uidsucursal = @UidSucursal AND D.VchNombre = @Dia;";

            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@Dia", dia);
            command.Parameters.AddWithValue("@UidSucursal", uidSucursal);

            return this.dbConexion.Busquedas(command);
        }
    }
}
