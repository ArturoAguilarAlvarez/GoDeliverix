using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    public class SeccionDataAccess
    {
        /// <summary>
        /// Conexion a la base de datos
        /// </summary>
        protected readonly Conexion dbConexion;

        public SeccionDataAccess()
        {
            this.dbConexion = new Conexion();
        }

        public DataTable ObtenerSeccionesOfertas(Guid uidEstado, Guid uidOferta)
        {
            string query = @"
-- Zona horaria del usuario acorde al estado
DECLARE @TimeZone VARCHAR(50);
-- Fecha y Hora local del usuario
DECLARE @UserDateTime DATETIME;
-- Hora actual del usuario
DECLARE @UserTime VARCHAR(20);

-- DECLARE @UidEstado uniqueidentifier = '1fce366d-c225-47fd-b4bb-5ee4549fe913'
-- DECLARE @UidOferta UNIQUEIDENTIFIER = '6660EEE1-6E1E-477A-8DDE-3EF5BB7AA908'

-- Obtener zona horaria del estado
SELECT
    @TimeZone = Z.IdZonaHoraria
FROM [ZonaHoraria] AS Z
    INNER JOIN [ZonaHorariaPais] AS P ON P.[IdZonaHoraria] = Z.[IdZonaHoraria]
    INNER JOIN [ZonaHorariaEstado] AS E ON E.[UidRelacionZonaPaisEstado] = P.[UidZonaHorariaPais]
WHERE E.UidEstado = @UidEstado

-- Obtener DateTime del la zona horaria
SELECT @UserDateTime = SYSDATETIMEOFFSET() AT TIME ZONE @TimeZone 

-- Obtener Time del DateTime
SELECT @UserTime = CONVERT(VARCHAR, @UserDateTime, 8);

SELECT 
    S.[UidSeccion] AS [Uid],
    S.[VchNombre] AS [Name],
    S.VchHoraInicio,
    S.VchHoraFin,
    CASE WHEN @UserTime BETWEEN S.VchHoraInicio AND S.VchHoraFin THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS [Available]
FROM [Seccion] AS S
WHERE S.IntEstatus = 1 
    AND S.UidOferta = @UidOferta;";

            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@UidOferta", uidOferta);
            command.Parameters.AddWithValue("@UidEstado", uidEstado);

            return this.dbConexion.Busquedas(command);
        }
    }
}
