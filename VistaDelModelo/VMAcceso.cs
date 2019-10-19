using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DBControl;
using Modelo;
using Modelo.Usuario;

namespace VistaDelModelo
{
    public class VMAcceso
    {

        #region Propiedades
        DBAcceso CN = new DBAcceso();
        Cuenta oCuenta = new Cuenta();
        #endregion

        public Guid Ingresar(string USUARIO, string PASSWORD)
        {
            int Result = 0;
            Guid id = Guid.Empty;
            DataTable Resultado = CN.IngresoAlSitio(USUARIO, PASSWORD);

            if (Resultado.Rows.Count != 0)
            {
                Result = Resultado.Rows.Count;
                foreach (DataRow item in Resultado.Rows)
                {
                    id = new Guid(item["UidUsuario"].ToString());
                }

            }
            else
            {
                Result = 0;
            }

            return id;
        }

        public bool VerificarEstatus(string Uidusuario)
        {
            bool Resultado = false;
            if (CN.EstatusUsuario(Uidusuario.ToString()).Rows.Count == 1)
            {
                Resultado = true;
            }
            return Resultado;
        }
        public string NombreDeUsuario(Guid id)
        {
            string nombre = string.Empty;
            DataTable dt = CN.NombreDeUsuario(id);

            foreach (DataRow item in dt.Rows)
            {
                nombre = item["Usuario"].ToString();
            }
            return nombre;
        }
        public string PerfilDeUsuario(string Id)
        {
            string Perfil = string.Empty;
            DataTable Datos = CN.PerfilDeUsuario(new Guid(Id));
            foreach (DataRow item in Datos.Rows)
            {
                Perfil = item["UidPerfil"].ToString();
            }
            return Perfil;
        }

        public void CorreoDeConfirmacion(Guid UidUsuario, string correo, string usuario, string password, string Nombre, string Apellidos)
        {
            Guid CodigoActivacion = Guid.NewGuid();
            MailMessage mail = new MailMessage("website@compuandsoft.com", correo);
            mail.Subject = "Activacion de cuenta";
            //Se crea el contenido del correo eletronico
            string contenido = "Hola usuario " + Nombre + " " + Apellidos + ",";
            contenido += "<br/>Gracias por registrarte en GODELIVERIX!! Aqui esta la información de la cuenta:";
            contenido += "<br/>Usuario: " + usuario + "";
            contenido += "<br/>Contraseña: " + password + "";
            contenido += "<br/><br/><br/> Por favor dale click al siguiente link para poder verificar tu cuenta";
            contenido += "<br/><br/><a href='http://www.godeliverix.net/Vista/Activaciondecuenta.aspx?CodigoActivacionCuentaDeliverixxdxdxdxd=" + CodigoActivacion.ToString() + "&UidUsuarioxdxdxdxdDDxD=" + UidUsuario.ToString() + "'>Verificar mi cuenta ahora!</a>";
            contenido += "<br/><br/> Muchas gracias.";

            mail.Body = contenido;
            mail.IsBodyHtml = true;
            //Se activa una variable del protocolo SMTP para poder enviar el correo electronico
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.compuandsoft.com";
            smtp.EnableSsl = false;

            //Activacion de la cuenta de la que se enviaran los correos electronicos
            NetworkCredential credenciales = new NetworkCredential("website@compuandsoft.com", "W3bs1t35@");
            // asignacion de las credenciales al protocolo smtp
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = credenciales;
            //Puerto de salida de correo electronico
            smtp.Port = 587;
            //envio del correo electronico
            smtp.Send(mail);
            //Guarda el codigo en la base de datos con la cuenta desactivada.
            GuardarCodigo(CodigoActivacion, UidUsuario);
        }

        public void CorreoDeInformacionDeCambioDeTarifario(string EmpresaDistribuidora, string EmpresaSuministradora, string NombreSucursal, List<VMTarifario> ListaDeCambiosDeColonias, string correo)
        {
            //Obtener los nombres de las empresas,
            MailMessage mail = new MailMessage("website@compuandsoft.com", correo);
            mail.Subject = "Informacion de cambios en el tarifario";
            //Se crea el contenido del correo eletronico
            string contenido = "Hola empresa " + EmpresaDistribuidora + ",";
            contenido += "<br/>Se ha modificado su contrato con la empresa <strong>" + EmpresaSuministradora + "</strong>";
            contenido += "<br/>Las siguientes colonias dejaran de estar en su zona de servicio con la sucursal <strong>" + NombreSucursal + "</strong>:";

            foreach (var item in ListaDeCambiosDeColonias)
            {
                contenido += "<br/> " + item.StrNombreColoniaZE + "";
            }
            contenido += "<br/><br/><br/> Cualquier duda o aclaracion, contacte a la empresa.";
            contenido += "<br/><br/> Muchas gracias.";
            mail.Body = contenido;
            mail.IsBodyHtml = true;
            //Se activa una variable del protocolo SMTP para poder enviar el correo electronico
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.compuandsoft.com";
            smtp.EnableSsl = false;

            //Activacion de la cuenta de la que se enviaran los correos electronicos
            NetworkCredential credenciales = new NetworkCredential("website@compuandsoft.com", "W3bs1t35@");
            // asignacion de las credenciales al protocolo smtp
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = credenciales;
            //Puerto de salida de correo electronico
            smtp.Port = 587;
            //envio del correo electronico
            smtp.Send(mail);
        }

        public bool GuardarCodigo(Guid UidCodigo, Guid UidUsuario)
        {
            oCuenta = new Cuenta() { usuario = new Usuarios() { ID = UidUsuario }, CodigoActivacion = UidCodigo, estatus = new Estatus() { ID = 2 } };

            return oCuenta.Guardar();
        }
        public bool ActualizarEstatus(Guid UidCodigo, Guid UidUsuario)
        {
            oCuenta = new Cuenta() { usuario = new Usuarios() { ID = UidUsuario }, CodigoActivacion = UidCodigo, estatus = new Estatus() { ID = 1 } };
            return oCuenta.Actualizar();
        }

        public bool RecuperarCodigoDeConfirmacion(string Email)
        {
            bool resultado = false;
            DataTable tabla = CN.RecuperarUsuarioPorEmail(Email);
            if (tabla.Rows.Count == 1)
            {
                foreach (DataRow item in tabla.Rows)
                {
                    string Nombre = item["Nombre"].ToString();
                    string usuario = item["usuario"].ToString();
                    string Correo = item["Correo"].ToString();
                    string password = item["contrasena"].ToString();
                    string UidUsuario = item["UidUsuario"].ToString();
                    string APPaterno = item["ApellidoPaterno"].ToString();
                    string APMaterno = item["ApellidoMaterno"].ToString();
                    CorreoDeConfirmacion(new Guid(UidUsuario), Correo, usuario, password, Nombre, APPaterno + " " + APPaterno);
                    resultado = true;
                }
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        public string ObtenerUltimoEstatusBitacoraRepartidor(Guid uidUsuario)
        {
            string valor = "";
            CN = new DBAcceso();
            foreach (DataRow item in CN.ObtenerUltimoEstatusRepartidor(uidUsuario).Rows)
            {
                valor = item["uidestatusrepartidor"].ToString();
            }
            return valor;
        }

        public bool RecuperarCuenta(string Email)
        {
            bool resultado = false;
            DataTable tabla = CN.RecuperarUsuarioPorEmail(Email);
            if (tabla.Rows.Count == 1)
            {
                foreach (DataRow item in tabla.Rows)
                {
                    string Nombre = item["Nombre"].ToString();
                    string usuario = item["usuario"].ToString();
                    string Correo = item["Correo"].ToString();
                    string password = item["contrasena"].ToString();
                    string UidUsuario = item["UidUsuario"].ToString();
                    string APPaterno = item["ApellidoPaterno"].ToString();
                    string APMaterno = item["ApellidoMaterno"].ToString();
                    EnviarDatosDeCuentaPorCorreo(new Guid(UidUsuario), Correo, usuario, password, Nombre, APPaterno + " " + APPaterno);
                    resultado = true;
                }
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        private void EnviarDatosDeCuentaPorCorreo(Guid guid, string correo, string usuario, string password, string nombre, string v)
        {
            Guid CodigoActivacion = Guid.NewGuid();
            MailMessage mail = new MailMessage("website@compuandsoft.com", correo);
            mail.Subject = "Informacion de cuenta";
            //Se crea el contenido del correo eletronico
            string contenido = "Estos son los datos de tu cuenta " + nombre + ",";
            contenido += "<br/>Usuario: " + usuario + "";
            contenido += "<br/>Contraseña: " + password + "";
            contenido += "<br/><br/><br/> Ir al sitio";
            contenido += "<br/><br/><a href='http://www.godeliverix.net/'>Ir a GoDeliverix</a>";


            mail.Body = contenido;
            mail.IsBodyHtml = true;
            //Se activa una variable del protocolo SMTP para poder enviar el correo electronico
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.compuandsoft.com";
            smtp.EnableSsl = false;

            //Activacion de la cuenta de la que se enviaran los correos electronicos
            NetworkCredential credenciales = new NetworkCredential("website@compuandsoft.com", "W3bs1t35@");
            // asignacion de las credenciales al protocolo smtp
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = credenciales;
            //Puerto de salida de correo electronico
            smtp.Port = 587;
            //envio del correo electronico
            smtp.Send(mail);
        }
        /// <summary>
        /// Inserta a la bitacora la accion del repartidor, ya sea si cierra session o hace alguna accion sobre la orden que se le asigno
        /// </summary>
        /// <param name="StrParametro">Solo 2 opciones S (Accion en session) | O (Accion en orden)</param>
        /// <param name="UidUsuario"></param>
        /// <param name="estatus"></param>
        /// <param name="UidOrdenRepartidor"></param>
        public void BitacoraRegistroRepartidores(char StrParametro,Guid UidUsuario, Guid UidEstatus, Guid UidOrdenRepartidor = new Guid())
        {
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_BitacoraEstatusRepartidor";
                
                cmd.Parameters.Add("@uidUsuario", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@uidUsuario"].Value = UidUsuario;
                //Parametro opcional para el manejo de la accion sobre una orden asignada
                if (UidOrdenRepartidor != Guid.Empty)
                {
                    cmd.Parameters.Add("@UidOrdenRepartidor", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@UidOrdenRepartidor"].Value = UidOrdenRepartidor;
                }
                cmd.Parameters.Add("@UidEstatus", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidEstatus"].Value = UidEstatus;

                cmd.Parameters.Add("@StrParametro", SqlDbType.Char,1);
                cmd.Parameters["@StrParametro"].Value = StrParametro;

                var oconexion = new Conexion();
                //Mandar comando a ejecución
                resultado = oconexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Inserta a la bitacora la accion del supervisor, ya sea si cierra session o hace alguna accion sobre la orden que se le asigno
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <param name="estatus"></param>
        public void BitacoraRegistroSupervisores(Guid UidUsuario, Guid UidEstatus)
        {
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_BitacoraDeSupervisores";
                
                cmd.Parameters.Add("@uidUsuario", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@uidUsuario"].Value = UidUsuario;
                
                cmd.Parameters.Add("@UidEstatus", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidEstatus"].Value = UidEstatus;

                var oconexion = new Conexion();
                //Mandar comando a ejecución
                resultado = oconexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
