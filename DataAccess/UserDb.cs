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
    public class UserDb : BaseDapper
    {
        public UserDb() { }

        public User GetUser(Guid uid)
        {
            string query = @"
SELECT 
    U.UidUsuario AS [Uid],
    U.UidPerfil AS ProfileUid,
    U.Nombre AS [Name],    
    U.ApellidoPaterno AS FirstLastName,
    U.ApellidoMaterno AS SecondLastName,
    CE.Correo As Email,
    U.FechaDeNacimiento AS Birthday
FROM Usuarios AS U
    INNER JOIN CorreoUsuario AS CU ON CU.UidUsuario = U.UidUsuario
    INNER JOIN CorreoElectronico AS CE ON CE.IdCorreo = CU.UidCorreo
WHERE U.UidUsuario = @UidUsuario";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UidUsuario", uid);

            return this.QuerySingleOrDefault<User>(query,parameters);
        }
    }
}
