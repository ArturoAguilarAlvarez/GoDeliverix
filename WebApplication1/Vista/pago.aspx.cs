using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using RestSharp;
namespace WebApplication1.Vista
{
    public partial class pago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string xmlString = System.IO.File.ReadAllText(path: Server.MapPath("FichaPago.xsd"));

            //XmlDocument xml = new XmlDocument();

            //xml.LoadXml("<P>< business > < id_company > SNBX </ id_company > < id_branch > 01SNBXBRNCH </ id_branch >      < user > SNBXUSR01 </ user>   < pwd > SECRETO </ pwd >    </ business >    < url >      < reference > FACTURA666 </ reference >      < amount > 550.00 </ amount >   < moneda > MXN </ moneda >   < canal > W </ canal >  < omitir_notif_default > 1 </ omitir_notif_default >  < st_correo > 1 </ st_correo >     < fh_vigencia > 29 / 11 / 2019 </ fh_vigencia >      < mail_cliente > softwareapps@compuandsoft.com </ mail_cliente >    < st_cr > A </ st_cr >    < datos_adicionales >   < data id = '"+"1"+"' display = '"+"true"+ "' >          < label > Cliente </ label >          < value > Cliente117 </ value >        </ data >        < data id = '" + "2" + "' display = '" + "true" + "' >          < label > Cantidad  </ label > < value > 30 </ value >  </ data > < data id = '" + "3" + "' display = '" + "false" + "' >        < label > Ordenes en pedido </ label > < value > 4 </ value >        </ data >  </ datos_adicionales >  </ url >  </ P > ");
            //XmlElement root = xml.CreateElement("p");
            //XmlElement elemento = xml.CreateElement("business");
            //elemento.AppendChild(new XmlNode("id_company", "SNBX"));
            //elemento.SetAttribute("id_branch", "01SNBXBRNCH");
            //elemento.SetAttribute("user", "SNBXUSR01");
            //elemento.SetAttribute("pwd", "SECRETO");
            //root.AppendChild(elemento);
            //XmlElement elemento2 = xml.CreateElement("url");
            //elemento2.SetAttribute("reference", "FACTURA666");
            //elemento2.SetAttribute("amount", "550.00");
            //elemento2.SetAttribute("moneda", "MXN");
            //elemento2.SetAttribute("canal", "W");
            //elemento2.SetAttribute("omitir_notif_default", "1");
            //elemento2.SetAttribute("st_correo", "1");
            //elemento2.SetAttribute("fh_vigencia", "29/11/2019");
            //elemento2.SetAttribute("mail_cliente", "softwareapps@compuandsoft.com");
            //elemento2.SetAttribute("st_cr", "A");
            //XmlElement datosextras = xml.CreateElement("datos_adicionales");
            //elemento2.AppendChild(datosextras);
            //elemento2.SetAttribute("fh_vigencia", "29/11/2019");


            //root.AppendChild(elemento2);


            //xml.AppendChild(root);

            AESCrypto o = new AESCrypto();
            string originalString = xmlString;
            string key = "5DCC67393750523CD165F17E1EFADD21";
            string encryptedString = o.encrypt(originalString, key);
            string finalString = encryptedString.Replace("%", "%25").Replace(" ", "%20").Replace("+", "%2B").Replace("=", "%3D").Replace("/", "%2F");

            string encodedString = HttpUtility.UrlEncode("<pgs><data0>SNDBX123</data0><data>[" + encryptedString + "]</data></pgs>");
            string postParam = "xml=" + encodedString;
            var client = new RestClient("https://wppsandbox.mit.com.mx/gen?" + postParam);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //request.AddQueryParameter(postParam, ParameterType.RequestBody.ToString());

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            //lblRespuesta.Text = o.decrypt(key, content);
            string decryptedString = o.decrypt(key, content);
            string str1 = decryptedString.Replace("<P_RESPONSE><cd_response>success</cd_response><nb_response></nb_response><nb_url>", "");
            string url = str1.Replace("</nb_url></P_RESPONSE>", "");
            iframePrueba.Src = url;
        }
    }
}