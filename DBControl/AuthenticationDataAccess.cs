using System;
using System.Collections.Generic;
using System.Data;
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
                    C.[Correo]
                FROM [Usuarios] AS U
                    INNER JOIN [CorreoUsuario] AS CU ON CU.[UidUsuario] = U.[UidUsuario]
                    INNER JOIN [CorreoElectronico] AS C ON C.[IdCorreo] = CU.[UidCorreo]
                WHERE [Usuario] = '{username}' 
                    AND [Contrasena] COLLATE Latin1_General_CS_AS =  '{password}'
                    AND [UidPerfil] = '{profileUid.ToString()}'";

            DataTable data = this.dbConexion.Consultas(query);
            return data;
        }
    }
}
