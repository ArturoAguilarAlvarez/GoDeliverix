using Dapper;
using DataAccess.Common;
using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AuthDb : BaseDapper
    {
        public AuthDb()
        {

        }

        public StoreLoginResult LoginStore(string password, string username = "", string email = "")
        {
            DynamicParameters parameters = new DynamicParameters();
            string where = "";
            if (!string.IsNullOrEmpty(username.Trim()))
            {
                where = " AND U.[Usuario] = @Username ";
                parameters.Add("@Username", username);
            }

            if (!string.IsNullOrEmpty(email.Trim()))
            {
                where = " AND CE.[Correo] = @Email ";
                parameters.Add("@Email", email);
            }

            parameters.Add("@Password", password);

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

            return this.QuerySingleOrDefault<StoreLoginResult>(query, parameters);
        }
    }
}
