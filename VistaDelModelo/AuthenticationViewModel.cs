using DBControl;
using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class AuthenticationViewModel
    {
        private AuthenticationDataAccess DbAuthentication { get; }

        public AuthenticationViewModel()
        {
            this.DbAuthentication = new AuthenticationDataAccess();
        }

        /// <summary>
        /// Iniciar session del repartidor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="profileUid"></param>
        /// <returns></returns>
        public DeliveryManLoginResult AuthenticateDeliveryMan(string username, string password, Guid profileUid)
        {
            DeliveryManLoginResult result = null;
            DataTable data = this.DbAuthentication.AuthenticateDeliveryMan(username, password, profileUid);

            if (data.Rows.Count > 0 || data.Rows.Count == 1)
            {
                foreach (DataRow row in data.Rows)
                {
                    result = new DeliveryManLoginResult()
                    {
                        Uid = (Guid)row["UidUsuario"],
                        UidPerfil = (Guid)row["UidPerfil"],
                        Usuario = (string)row["Usuario"],
                        CorreoElectronico = (string)row["Correo"],
                        FechaNacimiento = (DateTime)row["FechaDeNacimiento"],
                        Nombre = row.IsNull("Nombre") ? "" : (string)row["Nombre"],
                        ApellidoPaterno = row.IsNull("ApellidoPaterno") ? "" : (string)row["ApellidoPaterno"],
                        ApellidoMaterno = row.IsNull("ApellidoMaterno") ? "" : (string)row["ApellidoMaterno"]
                    };
                }
            }
            return result;
        }
    }
}
