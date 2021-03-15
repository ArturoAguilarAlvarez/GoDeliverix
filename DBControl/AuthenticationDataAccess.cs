using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    /// <summary>
    /// Realizar consultas y manipulacion de datos 
    /// </summary>
    public class AuthenticationDataAccess
    {
        /// <summary>
        /// Conexion a la base de datos
        /// </summary>
        protected readonly Conexion dbConexion;

        public AuthenticationDataAccess()
        {
            this.dbConexion = new Conexion();
        }

        /// <summary>
        /// Validar acceso del repartidor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="profileUid"></param>
        /// <returns></returns>
        public DataTable AuthenticateDeliveryMan(string username, string password, Guid profileUid)
        {
            string query = $@"
                SELECT 
                    U.[UidUsuario],
                    U.[Usuario],
                    U.[Nombre],
                    U.[ApellidoPaterno],
                    U.[ApellidoMaterno],
                    U.[FechaDeNacimiento],
                    U.[UidPerfil],
                    R.[UidRepartidor],
                    C.[Correo]
                FROM [Usuarios] AS U
                    INNER JOIN [Repartidor] AS R ON R.[UidUsuario] = U.[UidUsuario]
                    LEFT JOIN [CorreoUsuario] AS CU ON CU.[UidUsuario] = U.[UidUsuario]
                    LEFT JOIN [CorreoElectronico] AS C ON C.[IdCorreo] = CU.[UidCorreo]
                WHERE [Usuario] = '{username}' 
                    AND [Contrasena] COLLATE Latin1_General_CS_AS =  '{password}'
                    AND [UidPerfil] = '{profileUid.ToString()}'";

            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }

        public DataTable LoginStore(string password, string username = "", string email = "")
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;

            string where = "";
            if (!string.IsNullOrEmpty(username.Trim()))
            {
                where = " AND U.[Usuario] = @Username ";
                command.Parameters.AddWithValue("@Username", username);
            }

            if (!string.IsNullOrEmpty(email.Trim()))
            {
                where = " AND CE.[Correo] = @Email ";
                command.Parameters.AddWithValue("@Email", email);
            }

            string query = $@"
SELECT
    U.[UidUsuario] AS [Uid],
    U.[Nombre] AS [Name],
    U.[ApellidoMaterno] AS [FirstLastName],
    U.[ApellidoPaterno] AS [SecondLastName],
    U.[Usuario],
    CE.[Correo] AS [Email],
    P.[UidPerfil],
    P.[Nombre] AS [ProfileName]
FROM [Usuarios] AS U
    INNER JOIN [CorreoUsuario] AS CU ON CU.[UidUsuario] = U.[UidUsuario]
    INNER JOIN [CorreoElectronico] AS CE ON CE.[IdCorreo] = CU.[UidCorreo]
    INNER JOIN [Perfiles] AS P ON P.[UidPerfil] = U.[UidPerfil]
WHERE U.[Contrasena] COLLATE Latin1_General_CS_AS = @Password {where}";
            command.CommandText = query;

            command.Parameters.AddWithValue("@Password", password);

            DataTable data = this.dbConexion.Busquedas(command);
            return data;
        }
    }
}
