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

        const string CorreoElectronicoPrincipal = "info@godeliverix.com";
        const string ContrasenaDeCorreo = "8)=#[0xy})gJ";
        const string HostCorreoElectronico = "mail.godeliverix.com";
        #endregion

        public Guid Ingresar(string USUARIO, string PASSWORD, string correoElectronico = "")
        {
            int Result = 0;
            Guid id = Guid.Empty;
            DataTable Resultado = CN.IngresoAlSitio(USUARIO, PASSWORD, correoElectronico);

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
            MailMessage mail = new MailMessage(CorreoElectronicoPrincipal, correo);
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
            smtp.Host = HostCorreoElectronico;
            smtp.EnableSsl = false;

            //Activacion de la cuenta de la que se enviaran los correos electronicos
            NetworkCredential credenciales = new NetworkCredential(CorreoElectronicoPrincipal, ContrasenaDeCorreo);
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

        public void EnviarCodigoDeActivacion(string nombre, string apellidoP, string correo, string codigo, string idioma)
        {
            MailMessage mail = new MailMessage(CorreoElectronicoPrincipal, correo);
            //Se crea el contenido del correo eletronico
            //Variables para cuerpo del correo
            var Label0 = "";
            var Label1 = "";
            var Label2 = "";
            var Label3 = "";
            var Label4 = "";
            switch (idioma)
            {
                case "es":
                    Label0 = "Activacion de cuenta";
                    Label1 = "Bienvenido!";
                    Label2 = "Hola";
                    Label3 = "Para terminar el proceso de registro introduzca el código que se muestra a continuación";
                    Label4 = "Todos los derechos reservados";
                    break;
                default:
                    Label0 = "Account activation";
                    Label1 = "Welcome!";
                    Label2 = "Hello";
                    Label3 = "To finish the registration process enter the code shown below";
                    Label4 = "All rights reserved";
                    break;
            }

            mail.Subject = Label0;


            string body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional //EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:v='urn:schemas-microsoft-com:vml'><head> <!--[if gte mso 9]><xml><o:OfficeDocumentSettings><o:AllowPNG/><o:PixelsPerInch>96</o:PixelsPerInch></o:OfficeDocumentSettings></xml><![endif]--> <meta content='text/html; charset=utf-8' http-equiv='Content-Type' /> <meta content='width=device-width' name='viewport' /> <!--[if !mso]><!--> <meta content='IE=edge' http-equiv='X-UA-Compatible' /> <!--<![endif]--> <title></title> <!--[if !mso]><!--> <link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' /> <!--<![endif]--> <style type='text/css'> body { margin: 0; padding: 0; } table, td, tr { vertical-align: top; border-collapse: collapse; } * { line-height: inherit; } a[x-apple-data-detectors=true] { color: inherit !important; text-decoration: none !important; } </style> <style id='media-query' type='text/css'> @media (max-width: 660px) { .block-grid, .col { min-width: 320px !important; max-width: 100% !important; display: block !important; } .block-grid { width: 100% !important; } .col { width: 100% !important; } .col>div { margin: 0 auto; } img.fullwidth, img.fullwidthOnMobile { max-width: 100% !important; } .no-stack .col { min-width: 0 !important; display: table-cell !important; } .no-stack.two-up .col { width: 50% !important; } .no-stack .col.num4 { width: 33% !important; } .no-stack .col.num8 { width: 66% !important; } .no-stack .col.num4 { width: 33% !important; } .no-stack .col.num3 { width: 25% !important; } .no-stack .col.num6 { width: 50% !important; } .no-stack .col.num9 { width: 75% !important; } .video-block { max-width: none !important; } .mobile_hide { min-height: 0px; max-height: 0px; max-width: 0px; display: none; overflow: hidden; font-size: 0px; } .desktop_hide { display: block !important; max-height: none !important; } } </style></head><body class='clean-body' style='margin: 0; padding: 0; -webkit-text-size-adjust: 100%; background-color: #eceff4;'> <!--[if IE]><div class='ie-browser'><![endif]--> <table bgcolor='#eceff4' cellpadding='0' cellspacing='0' class='nl-container' role='presentation' style='table-layout: fixed; vertical-align: top; min-width: 320px; Margin: 0 auto; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #eceff4; width: 100%; margin-top: 24px;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td style='word-break: break-word; vertical-align: top;' valign='top'> <!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td align='center' style='background-color:#eceff4'><![endif]--> <div style='background-color:transparent;'> <div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 540px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: transparent;'> <div style='border-collapse: collapse;display: table;width: 100%;background-color:transparent;'> <!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:transparent;'><tr><td align='center'><table cellpadding='0' cellspacing='0' border='0' style='width:540px'><tr class='layout-full-width' style='background-color:transparent'><![endif]--> <!--[if (mso)|(IE)]><td align='center' width='540' style='background-color:#ffffff;width:540px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 0px; padding-left: 0px; padding-top:5px; padding-bottom:5px;'><![endif]--> <div class='col num12' style='min-width: 320px; max-width: 540px; display: table-cell; vertical-align: top; width: 540px; background-color:#ffffff;'> <div style='width:100% !important;'> <!--[if (!mso)&(!IE)]><!--> <div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:5px; padding-bottom:5px; padding-right: 0px; padding-left: 0px;'> <!--<![endif]--> <div align='center' class='img-container center autowidth' style='padding-right: 10px;padding-left: 10px;'> <!--[if mso]><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr style='line-height:0px'><td style='padding-right: 10px;padding-left: 10px;' align='center'><![endif]--> <div style='font-size:1px;line-height:10px'> </div> <img align='center' alt='Alternate text' border='0' class='center autowidth' src='https://compuandsoft.com/img/godeliverix/goDeliverix_logo.png' style='text-decoration: none; -ms-interpolation-mode: bicubic; height: auto; border: 0; width: 100%; max-width: 135px; display: block;' title='goDeliverix' width='135' /> <div style='font-size:1px;line-height:15px'> </div> <!--[if mso]></td></tr></table><![endif]--> </div> <!--[if (!mso)&(!IE)]><!--> </div> <!--<![endif]--> </div> </div> <!--[if (mso)|(IE)]></td></tr></table><![endif]--> <!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]--> </div> </div> </div> <div style='background-color:transparent;'> <div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 540px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: #ffffff;'> <div style='border-collapse: collapse;display: table;width: 100%;background-color:#ffffff;'> <!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:transparent;'><tr><td align='center'><table cellpadding='0' cellspacing='0' border='0' style='width:540px'><tr class='layout-full-width' style='background-color:#ffffff'><![endif]--> <!--[if (mso)|(IE)]><td align='center' width='540' style='background-color:#ffffff;width:540px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 0px; padding-left: 0px; padding-top:0px; padding-bottom:0px;'><![endif]--> <div class='col num12' style='min-width: 320px; max-width: 540px; display: table-cell; vertical-align: top; width: 540px;'> <div style='width:100% !important;'> <!--[if (!mso)&(!IE)]><!--> <div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:0px; padding-bottom:0px; padding-right: 0px; padding-left: 0px;'> <!--<![endif]--> <table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px;' valign='top'> <table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' height='40' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 0px solid transparent; height: 40px; width: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td height='40' style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'><span></span></td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <!--[if mso]><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 25px; padding-left: 25px; padding-top: 0px; padding-bottom: 0px; font-family: Arial, sans-serif'><![endif]--> <div style='color:#555555;font-family:'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;line-height:1.2;padding-top:0px;padding-right:25px;padding-bottom:0px;padding-left:25px;'> <div style='line-height: 1.2; font-size: 12px; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #555555; mso-line-height-alt: 14px;'> <p style='font-size: 30px; line-height: 1.2; word-break: break-word; text-align: center; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif; mso-line-height-alt: 36px; margin: 0;'> <span style='font-size: 30px;'> <strong> " + Label1 + "! </strong> </span> </p> </div> </div> <!--[if mso]></td></tr></table><![endif]--> <table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px;' valign='top'> <table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' height='20' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 0px solid transparent; height: 20px; width: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td height='20' style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'><span></span></td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <!--[if mso]><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 25px; padding-left: 25px; padding-top: 5px; padding-bottom: 5px; font-family: Arial, sans-serif'><![endif]--> <div style='color:#555555;font-family:'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;line-height:1.2;padding-top:5px;padding-right:25px;padding-bottom:5px;padding-left:25px;'> <div style='line-height: 1.2; font-size: 12px; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #555555; mso-line-height-alt: 14px;'> <p style='font-size: 22px; line-height: 1.2; word-break: break-word; text-align: center; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif; mso-line-height-alt: 26px; margin: 0;'> <span style='font-size: 22px;'>" + Label2 + ", " + nombre + " " + apellidoP + ".</span></p> </div> </div> <!--[if mso]></td></tr></table><![endif]--> <!--[if mso]><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 25px; padding-left: 25px; padding-top: 5px; padding-bottom: 0px; font-family: Arial, sans-serif'><![endif]--> <div style='color:#555555;font-family:'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;line-height:1.2;padding-top:5px;padding-right:25px;padding-bottom:0px;padding-left:25px;'> <div style='line-height: 1.2; font-size: 12px; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #555555; mso-line-height-alt: 14px;'> <p style='font-size: 15px; line-height: 1.2; word-break: break-word; text-align: center; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif; mso-line-height-alt: 18px; margin: 0;'> <span style='font-size: 15px;'> " + Label3 + ": </span> </p> </div> </div> <!--[if mso]></td></tr></table><![endif]--> <table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px;' valign='top'> <table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' height='35' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 0px solid transparent; height: 35px; width: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td height='35' style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'><span></span></td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <!--[if (!mso)&(!IE)]><!--> </div> <!--<![endif]--> </div> </div> <!--[if (mso)|(IE)]></td></tr></table><![endif]--> <!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]--> </div> </div> </div> <div style='background-color:transparent;'> <div class='block-grid three-up' style='Margin: 0 auto; min-width: 320px; max-width: 540px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: #ffffff;'> <div style='border-collapse: collapse;display: table;width: 100%;background-color:#ffffff;'> <!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:transparent;'><tr><td align='center'><table cellpadding='0' cellspacing='0' border='0' style='width:540px'><tr class='layout-full-width' style='background-color:#ffffff'><![endif]--> <!--[if (mso)|(IE)]><td align='center' width='110' style='background-color:#ffffff;width:110px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 0px; padding-left: 0px; padding-top:5px; padding-bottom:5px;'><![endif]--> <div class='col num3' style='display: table-cell; vertical-align: top; max-width: 320px; min-width: 110px; width: 110px;'> <div style='width:100% !important;'> <!--[if (!mso)&(!IE)]><!--> <div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:5px; padding-bottom:5px; padding-right: 0px; padding-left: 0px;'> <!--<![endif]--> <table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;' valign='top'> <table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' height='0' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 0px solid transparent; height: 0px; width: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td height='0' style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'> <span></span> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <!--[if (!mso)&(!IE)]><!--> </div> <!--<![endif]--> </div> </div> <!--[if (mso)|(IE)]></td></tr></table><![endif]--> <!--[if (mso)|(IE)]></td><td align='center' width='320' style='background-color:#ffffff;width:320px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 0px; padding-left: 0px; padding-top:5px; padding-bottom:5px;background-color:#f1f2f6;'><![endif]--> <div class='col num6' style='display: table-cell; vertical-align: top; max-width: 320px; min-width: 318px; background-color: #f1f2f6; width: 320px;'> <div style='width:100% !important;'> <!--[if (!mso)&(!IE)]><!--> <div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:5px; padding-bottom:5px; padding-right: 0px; padding-left: 0px;'> <!--<![endif]--> <table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px;' valign='top'> <table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' height='25' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 0px solid transparent; height: 15px; width: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td height='25' style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'><span></span></td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <!--[if mso]><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 0px; padding-left: 0px; padding-top: 0px; padding-bottom: 0px; font-family: Arial, sans-serif'><![endif]--> <div style='color:#555555;font-family:'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;padding-top:0px;padding-right:0px;padding-bottom:0px;padding-left:0px;'> <div style='font-size: 12px; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #555555; mso-line-height-alt: 18px;'> <p style='font-size: 24px; word-break: break-word; text-align: center; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif; mso-line-height-alt: 23px; margin: 0;'> <strong> <span style='font-size: 24px;'>" + codigo + "</span> </strong> </p> </div> </div> <!--[if mso]></td></tr></table><![endif]--> <table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px;' valign='top'> <table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' height='25' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 0px solid transparent; height: 25px; width: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td height='25' style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'> <span></span> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <!--[if (!mso)&(!IE)]><!--> </div> <!--<![endif]--> </div> </div> <!--[if (mso)|(IE)]></td></tr></table><![endif]--> <!--[if (mso)|(IE)]></td><td align='center' width='110' style='background-color:#ffffff;width:110px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 0px; padding-left: 0px; padding-top:5px; padding-bottom:5px;'><![endif]--> <div class='col num3' style='display: table-cell; vertical-align: top; max-width: 320px; min-width: 110px; width: 110px;'> <div style='width:100% !important;'> <!--[if (!mso)&(!IE)]><!--> <div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:5px; padding-bottom:5px; padding-right: 0px; padding-left: 0px;'> <!--<![endif]--> <table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px;' valign='top'> <table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' height='0' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 0px solid transparent; height: 0px; width: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td height='0' style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'> <span></span> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <!--[if (!mso)&(!IE)]><!--> </div> <!--<![endif]--> </div> </div> <!--[if (mso)|(IE)]></td></tr></table><![endif]--> <!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]--> </div> </div> </div> <div style='background-color:transparent;'> <div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 540px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: #ffffff;'> <div style='border-collapse: collapse;display: table;width: 100%;background-color:#ffffff;'> <!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color:transparent;'><tr><td align='center'><table cellpadding='0' cellspacing='0' border='0' style='width:540px'><tr class='layout-full-width' style='background-color:#ffffff'><![endif]--> <!--[if (mso)|(IE)]><td align='center' width='540' style='background-color:#ffffff;width:540px; border-top: 0px solid transparent; border-left: 0px solid transparent; border-bottom: 0px solid transparent; border-right: 0px solid transparent;' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 0px; padding-left: 0px; padding-top:5px; padding-bottom:5px;'><![endif]--> <div class='col num12' style='min-width: 320px; max-width: 540px; display: table-cell; vertical-align: top; width: 540px;'> <div style='width:100% !important;'> <!--[if (!mso)&(!IE)]><!--> <div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:5px; padding-bottom:5px; padding-right: 0px; padding-left: 0px;'> <!--<![endif]--> <table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 35px; padding-right: 35px; padding-bottom: 35px; padding-left: 35px;' valign='top'> <table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 1px solid #BBBBBB; width: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'> <span></span> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <!--[if mso]><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='padding-right: 25px; padding-left: 25px; padding-top: 5px; padding-bottom: 5px; font-family: Arial, sans-serif'><![endif]--> <div style='color:#555555;font-family:'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;line-height:1.2;padding-top:5px;padding-right:25px;padding-bottom:5px;padding-left:25px;'> <div style='line-height: 1.2; font-size: 12px; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #555555; mso-line-height-alt: 14px;'> <p style='font-size: 14px; line-height: 1.2; word-break: break-word; text-align: center; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif; mso-line-height-alt: 17px; margin: 0;'> <strong> GoDeliverix </strong> </p> <p style='font-size: 14px; line-height: 1.2; word-break: break-word; text-align: center; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif; mso-line-height-alt: 17px; margin: 0;'> © " + Label4 + " 2020 </p> </div> </div> <!--[if mso]></td></tr></table><![endif]--> <table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px;' valign='top'> <table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' height='40' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-top: 0px solid transparent; height: 40px; width: 100%;' valign='top' width='100%'> <tbody> <tr style='vertical-align: top;' valign='top'> <td height='40' style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top'> <span></span> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <!--[if (!mso)&(!IE)]><!--> </div> <!--<![endif]--> </div> </div> <!--[if (mso)|(IE)]></td></tr></table><![endif]--> <!--[if (mso)|(IE)]></td></tr></table></td></tr></table><![endif]--> </div> </div> </div> <!--[if (mso)|(IE)]></td></tr></table><![endif]--> </td> </tr> </tbody> </table> <!--[if (IE)]></div><![endif]--></body></html>";
            // mail.Body = contenido;
            mail.Body = body;
            mail.IsBodyHtml = true;
            //Se activa una variable del protocolo SMTP para poder enviar el correo electronico
            SmtpClient smtp = new SmtpClient();
            smtp.Host = HostCorreoElectronico;
            smtp.EnableSsl = false;

            //Activacion de la cuenta de la que se enviaran los correos electronicos
            NetworkCredential credenciales = new NetworkCredential(CorreoElectronicoPrincipal, ContrasenaDeCorreo);
            // asignacion de las credenciales al protocolo smtp
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = credenciales;
            //Puerto de salida de correo electronico
            smtp.Port = 587;
            //envio del correo electronico
            smtp.Send(mail);
        }

        public void CorreoDeInformacionDeCambioDeTarifario(string EmpresaDistribuidora, string EmpresaSuministradora, string NombreSucursal, List<VMTarifario> ListaDeCambiosDeColonias, string correo)
        {
            //Obtener los nombres de las empresas,
            MailMessage mail = new MailMessage(CorreoElectronicoPrincipal, correo);
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
            smtp.Host = HostCorreoElectronico;
            smtp.EnableSsl = false;

            //Activacion de la cuenta de la que se enviaran los correos electronicos
            NetworkCredential credenciales = new NetworkCredential(CorreoElectronicoPrincipal, ContrasenaDeCorreo);
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
            MailMessage mail = new MailMessage(CorreoElectronicoPrincipal, correo);
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
            smtp.Host = HostCorreoElectronico;
            smtp.EnableSsl = false;

            //Activacion de la cuenta de la que se enviaran los correos electronicos
            NetworkCredential credenciales = new NetworkCredential(CorreoElectronicoPrincipal, ContrasenaDeCorreo);
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
