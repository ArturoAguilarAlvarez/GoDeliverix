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

            string ho = "<!DOCTYPE html>\r\n" +
               "<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n" +
               "<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\r\n" +
               "<title></title>\r\n" +
               "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"/>\r\n" +
               "</head>\r\n" +
               "<body style=\"margin: 0; padding: 0;\">\r\n" +
               "\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\t\r\n" +
               "\t\t<tr>\r\n" +
               "\t\t\t<td style=\"padding: 10px 0 30px 0;\">\r\n" +
               "\t\t\t\t<table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"border: 1px solid #cccccc; border-collapse: collapse;\">\r\n" +
               "\t\t\t\t\t<tr>\r\n" +
               "\t\t\t\t\t\t<td align=\"center\" bgcolor=\"#03357d\" style=\"padding: 40px 0 30px 0; color: #153643; font-size: 28px; font-weight: bold; font-family: Arial, sans-serif;\">\r\n" +
               "\t\t\t\t\t\t\t<a href=\"https://godeliverix.net/\"><img src=\'http://www.godeliverix.net/Vista/img/GoDeliverix.jpg' alt =\"goParkix\" width=\"300\" height=\"230\" style=\"display: block;\" /></a>\r\n" +
               "\t\t\t\t\t\t</td>\r\n" +
               "\t\t\t\t\t</tr>\r\n" +
               "\t\t\t\t\t<tr>\r\n" +
               "\t\t\t\t\t\t<td bgcolor=\"#ffffff\" style=\"padding: 40px 30px 40px 30px;\">\r\n" +
               "\t\t\t\t\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n" +
               "\t\t\t\t\t\t\t\t<tr>\r\n" +
               "\t\t\t\t\t\t\t\t\t<td style=\"text-align: center;color: #153643; font-family: Arial, sans-serif; font-size: 24px;\">\r\n" +
               "\t\t\t\t\t\t\t\t\t\t<b> Hola, " + Nombre + " " + Apellidos + "</b>\r\n" +
               "\t\t\t\t\t\t\t\t\t\t<h3>¡Bienvenido!</h3>\r\n" +
               "\t\t\t\t\t\t\t\t\t</td>\r\n" +
               "\t\t\t\t\t\t\t\t</tr>\r\n" +
               "\t\t\t\t\t\t\t\t<tr>\r\n" +
               "\t\t\t\t\t\t\t\t\t<td style=\"text-align: center;color: #153643; font-family: Arial, sans-serif; font-size: 24px;\">\r\n" +
               "\t\t\t\t\t\t\t\t\t\t<b> A contunuacion le mostramos sus credenciales de acceso usuario: " + usuario + " y contraseña " + password + "</b>\r\n" +
               "\t\t\t\t\t\t\t\t\t</td>\r\n" +
               "\t\t\t\t\t\t\t\t</tr>\r\n" +
               "\t\t\t\t\t\t\t\t<tr>\r\n" +
               "\t\t\t\t\t\t\t\t\t<td style=\"padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;text-align: center;\">\r\n" +
               "\t\t\t\t\t\t\t\t\t\tSu cuenta ha sido registrada, pero necesita ser activarla.\r\n" +
               "\r\n\r\n\t\t\t\t\t\t<br/><br/><a href='http://www.godeliverix.net/Vista/Activaciondecuenta.aspx?CodigoActivacionCuentaDeliverixxdxdxdxd=" + CodigoActivacion.ToString() + "&UidUsuarioxdxdxdxdDDxD=" + UidUsuario.ToString() + "'>Para dar activarla dale click aqui!</a>" +
               "\r\n\r\n\t\t\t\t\t\t<br/><br/><a style =\"display:block;color:#fff;font-weight:400;text-align:center;width:230px;font-size:20px;text-decoration:none;background:#28a745;margin:0 auto;padding:15px 0\" href=\"https://play.google.com/store/apps/details?id=com.CompuAndSoft.GDCliente\"> Descargar app</a>" +
               "\t\t\t\t\t\t\t\t\t</td>\r\n" +
               "\t\t\t\t\t\t\t\t</tr>\r\n" +
               "\t\t\t\t\t\t\t</table>\r\n" +
               "\t\t\t\t\t\t</td>\r\n" +
               "\t\t\t\t\t</tr>\r\n" +
               "\t\t\t\t\t<tr>\r\n" +
               "\t\t\t\t\t\t<td bgcolor=\"#df5f16\" style=\"padding: 30px 30px 30px 30px;\">\r\n" +
               "\t\t\t\t\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n" +
               "\t\t\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t\t\t<td style=\"color: #ffffff; font-family: Arial, sans-serif; font-size: 14px;\" width=\"75%\">\r\n\t\t\t\t\t\t\t\t\tGoDeliverix &reg; Todos los derechos reservados, 2020<br/>\r\n" +
                "\t\t\t\t\t\t\t\t\t</td>\r\n" +
                "\t\t\t\t\t\t\t\t\t<td align=\"right\" width=\"25%\">\r\n" +
                //"\t\t\t\t\t\t\t\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n\t\t\t\t\t\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\">\r\n" +
                //"\t\t\t\t\t\t\t\t\t\t\t\t\t<a href=\"https://twitter.com/goparkix\" style=\"color: #ffffff;\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t<img src=\"https://help.twitter.com/content/dam/help-twitter/brand/logo.png\" alt=\"Twitter\" width=\"38\" height=\"38\" style=\"display: block;\" border=\"0\" />\r\n" +
                //"\t\t\t\t\t\t\t\t\t\t\t\t\t</a>\r\n" +
                //"\t\t\t\t\t\t\t\t\t\t\t\t</td>\r\n" +
                //"\t\t\t\t\t\t\t\t\t\t\t\t<td style=\"font-size: 0; line-height: 0;\" width=\"20\">&nbsp;</td>\r\n" +
                //"\t\t\t\t\t\t\t\t\t\t\t\t<td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\">\r\n" +
                ////"\t\t\t\t\t\t\t\t\t\t\t\t\t<a href=\"https://www.facebook.com/goparkix/ \" style=\"color: #ffffff;\">\r\n" +
                ////"\t\t\t\t\t\t\t\t\t\t\t\t\t\t<img src=\"https://images.vexels.com/media/users/3/137253/isolated/preview/90dd9f12fdd1eefb8c8976903944c026-icono-de-facebook-logo-by-vexels.png\" alt=\"Facebook\" width=\"38\" height=\"38\" style=\"display: block;\" border=\"0\" />\r\n" +
                ////"\t\t\t\t\t\t\t\t\t\t\t\t\t</a>\r\n" +
                //"\t\t\t\t\t\t\t\t\t\t\t\t</td>\r\n" +
                //"\t\t\t\t\t\t\t\t\t\t\t</tr>\r\n" +
                //"\t\t\t\t\t\t\t\t\t\t</table>\r\n" +
                "\t\t\t\t\t\t\t\t\t</td>\r\n" +
                "\t\t\t\t\t\t\t\t</tr>\r\n" +
                "\t\t\t\t\t\t\t</table>\r\n" +
                "\t\t\t\t\t\t</td>\r\n" +
                "\t\t\t\t\t</tr>\r\n" +
                "\t\t\t\t</table>\r\n" +
                "\t\t\t</td>\r\n" +
                "\t\t</tr>\r\n" +
                "\t</table>\r\n" +
                "</body>\r\n" +
                "</html>";



            // mail.Body = contenido;
            mail.Body = ho;
            mail.IsBodyHtml = true;
            //Se activa una variable del protocolo SMTP para poder enviar el correo electronico
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.compuandsoft.com";
            smtp.EnableSsl = false;

            //Activacion de la cuenta de la que se enviaran los correos electronicos
            NetworkCredential credenciales = new NetworkCredential("website@compuandsoft.com", "+K7;v0{?SgVX");
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
            NetworkCredential credenciales = new NetworkCredential("website@compuandsoft.com", "+K7;v0{?SgVX");
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
            NetworkCredential credenciales = new NetworkCredential("website@compuandsoft.com", "+K7;v0{?SgVX");
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
        public void BitacoraRegistroRepartidores(char StrParametro, Guid UidUsuario, Guid UidEstatus, Guid UidOrdenRepartidor = new Guid())
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

                cmd.Parameters.Add("@StrParametro", SqlDbType.Char, 1);
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
