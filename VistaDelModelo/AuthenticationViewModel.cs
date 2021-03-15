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
        private AddressDataAccess DbAddress { get; }

        public AuthenticationViewModel()
        {
            this.DbAuthentication = new AuthenticationDataAccess();
            this.DbAddress = new AddressDataAccess();
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
                        CorreoElectronico = row.IsNull("Correo") ? "" : (string)row["Correo"],
                        FechaNacimiento = (DateTime)row["FechaDeNacimiento"],
                        Nombre = row.IsNull("Nombre") ? "" : (string)row["Nombre"],
                        ApellidoPaterno = row.IsNull("ApellidoPaterno") ? "" : (string)row["ApellidoPaterno"],
                        ApellidoMaterno = row.IsNull("ApellidoMaterno") ? "" : (string)row["ApellidoMaterno"],
                        UidRepartidor = (Guid)row["UidRepartidor"],
                    };
                }
            }
            return result;
        }

        public StoreLoginResult LoginStore(string password, string username = "", string email = "")
        {
            StoreLoginResult login = new StoreLoginResult();

            DataTable data = this.DbAuthentication.LoginStore(password, username, email);

            if (data.Rows.Count == 0)
            {
                throw new Exception();
            }
            else
            {
                foreach (DataRow row in data.Rows)
                {
                    login = new StoreLoginResult()
                    {
                        Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                        UidPerfil = row.IsNull("UidPerfil") ? Guid.Empty : (Guid)row["UidPerfil"],
                        Email = row.IsNull("Email") ? string.Empty : (string)row["Email"],
                        FirstLastName = row.IsNull("FirstLastName") ? string.Empty : (string)row["FirstLastName"],
                        Name = row.IsNull("Name") ? string.Empty : (string)row["Name"],
                        ProfileName = row.IsNull("ProfileName") ? string.Empty : (string)row["ProfileName"],
                        SecondLastName = row.IsNull("SecondLastName") ? string.Empty : (string)row["SecondLastName"],
                        Usuario = row.IsNull("Usuario") ? string.Empty : (string)row["Usuario"]
                    };
                }
            }

            return login;
        }
    }
}
