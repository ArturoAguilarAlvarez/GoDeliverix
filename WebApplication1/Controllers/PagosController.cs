using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Vista;
using VistaDelModelo;
using System.Xml;
using WebApplication1.App_Start;
using System.Web.Http.Results;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace WebApplication1.Controllers
{
    public class PagosController : ApiController
    {
        VMPagos MVPago;
        ResponseHelper respuesta;

        // GET: api/Pagos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public ResponseHelper GetObtenerPagoTarjeta(string UidOrdenFormaDeCobro)
        {
            respuesta = new ResponseHelper();
            MVPago = new VMPagos();
            MVPago.UidOrdenFormaDeCobro = new Guid(UidOrdenFormaDeCobro);
            MVPago.ObtenerEstatusPagoConTarjeta();
            respuesta.Data = MVPago;
            return respuesta;
        }

        // GET: api/Pagos/GetInsertarPago
        public ResponseHelper GetInsertarPago(Guid UidPago, Guid UidFormaDeCobro, Guid UidOrden, decimal MMonto, Guid UidEstatusDeCobro)
        {
            respuesta = new ResponseHelper();
            MVPago = new VMPagos();
            MVPago.UidPago = UidPago;
            MVPago.UidOrdenFormaDeCobro = UidFormaDeCobro;
            MVPago.UidOrden = UidOrden;
            MVPago.MMonto = MMonto;
            MVPago.UidEstatusDeCobro = UidEstatusDeCobro;
            respuesta.Data = MVPago.IntegrarPago();
            return respuesta;
        }
        /// <summary>
        /// valida si existe la transaccion del pago con tarjeta de la orden
        /// </summary>
        /// <param name="UidOrdenFormaDeCobro"></param>
        /// <returns></returns>
        public ResponseHelper GetValidarPagoOrdenTarjeta(string UidOrdenFormaDeCobro)
        {
            respuesta = new ResponseHelper();
            MVPago = new VMPagos();
            respuesta.Data = MVPago.ValidaPagoTarjeta(UidOrdenFormaDeCobro);
            return respuesta;
        }
        /// <summary>
        /// Obtiene el ultimo estatus del cobro de la orden
        /// </summary>
        /// <param name="UidOrden"></param>
        /// <returns></returns>
        public ResponseHelper GetObtenerEstatusDeCobro(string UidOrden)
        {
            respuesta = new ResponseHelper();
            MVPago = new VMPagos();
            MVPago.UidOrden = new Guid(UidOrden);
            respuesta.Data = MVPago.ObtenerEstatusDeCobro();
            return respuesta;
        }
        /// <summary>
        /// Actualiza el estatus de la orden cuando esta ha sido pagada en efectivo
        /// </summary>
        /// <param name="UidOrden"></param>
        /// <param name="Estatus"></param>
        /// <returns></returns>
        public ResponseHelper GetCambiarEstatusPago(string UidOrden, string Estatus)
        {
            respuesta = new ResponseHelper();
            MVPago = new VMPagos();
            respuesta.Data = MVPago.BitacoraEstatusCobro(UidOrden, Estatus);
            return respuesta;
        }
        // POST: api/Pagos/PostPagosTarjeta
        
        //public ResponseHelper GetPagosTarjeta([FromBody]RespuestaPago StrResponse = null)
        [HttpPost]
        public ResponseHelper PostPagosTarjeta([FromBody] RespuestaPago strResponse)
        {
            strResponse.StrResponse = HttpUtility.HtmlEncode(strResponse.StrResponse);
            respuesta = new ResponseHelper();
            string finalString = strResponse.StrResponse.Replace("%25", "%").Replace("%20", " ").Replace("%2B", "+").Replace("%3D", "=").Replace("%2F", "/").Replace("%0D%0A", "\r\n");
            // key con produccion
            string cadena = finalString;

            // Credenciales sanbox string key = "5DCC67393750523CD165F17E1EFADD21";
            string key = "7AACFE849FABD796F6DCB947FD4D5268";
            AESCrypto o = new AESCrypto();
            string decryptedString = o.decrypt(key, cadena);
            if (!string.IsNullOrEmpty(decryptedString))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(decryptedString);

                XmlNodeList RespuestaWebPayPlus = doc.DocumentElement.SelectNodes("//CENTEROFPAYMENTS");
                string reference = string.Empty;
                string response = string.Empty;
                string foliocpagos = string.Empty;
                string auth = string.Empty;
                string cc_type = string.Empty;
                string tp_operation = string.Empty;
                string cc_number = string.Empty;
                string amount = string.Empty;
                string fecha = string.Empty;
                string Hora = string.Empty;

                for (int i = 0; i < RespuestaWebPayPlus[0].ChildNodes.Count; i++)
                {
                    switch (RespuestaWebPayPlus[0].ChildNodes[i].Name)
                    {
                        case "reference":
                            reference = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
                            break;
                        case "response":
                            response = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
                            break;
                        case "foliocpagos":
                            foliocpagos = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
                            break;
                        case "auth":
                            auth = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
                            break;
                        case "date":
                            fecha = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
                            break;
                        case "time":
                            Hora = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
                            break;
                        case "cc_type":
                            cc_type = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
                            break;
                        case "tp_operation":
                            tp_operation = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
                            break;
                        case "cc_number":
                            cc_number = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
                            break;
                        case "amount":
                            amount = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
                            break;
                        default:
                            break;
                    }
                }
                VMPagos MVPagos = new VMPagos();
                string fecha1 = fecha + " " + Hora;
                DateTime fechaRegistro = DateTime.MinValue;
                switch (response)
                {
                    case "denied":
                        fechaRegistro = DateTime.Now;

                        cc_type = "denied";
                        auth = "denied";
                        tp_operation = "denied";
                        amount = "0.0";
                        respuesta.Data = MVPagos.AgregarInformacionTarjeta(auth, reference, fechaRegistro, response, cc_type, tp_operation);
                        break;
                    case "approved":
                        fechaRegistro = DateTime.Parse(fecha1);
                        respuesta.Data = MVPagos.AgregarInformacionTarjeta(auth, reference, fechaRegistro, response, cc_type, tp_operation, amount);
                        break;
                    case "error":
                        fechaRegistro = DateTime.Now;
                        cc_type = "error";
                        auth = "error";
                        tp_operation = "error";
                        amount = "0.0";
                        respuesta.Data = MVPagos.AgregarInformacionTarjeta(auth, reference, fechaRegistro, response, cc_type, tp_operation);
                        break;
                }

            }
            else
            {
                respuesta.Data = "la cadena no se puede desifrar " + cadena;
            }



            return respuesta;
        }
        
        //[HttpPost]        
        //public ResponseHelper PostPagosTarjeta([FromBody] RespuestaPago strResponse)
        //{
        //    respuesta = new ResponseHelper();

        //    // key con produccion
        //    string key = "5DCC67393750523CD165F17E1EFADD21";
        //    //string key = "7AACFE849FABD796F6DCB947FD4D5268";
        //    AESCrypto o = new AESCrypto();
        //    string r = strResponse.StrResponse;
        //    string decryptedString = o.decrypt(key, r);

        //    if (!string.IsNullOrEmpty(decryptedString))
        //    {
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(decryptedString);

        //        XmlNodeList RespuestaWebPayPlus = doc.DocumentElement.SelectNodes("//CENTEROFPAYMENTS");
        //        string reference = string.Empty;
        //        string response = string.Empty;
        //        string foliocpagos = string.Empty;
        //        string auth = string.Empty;
        //        string cc_type = string.Empty;
        //        string tp_operation = string.Empty;
        //        string cc_number = string.Empty;
        //        string amount = string.Empty;
        //        string fecha = string.Empty;
        //        string Hora = string.Empty;

        //        for (int i = 0; i < RespuestaWebPayPlus[0].ChildNodes.Count; i++)
        //        {
        //            switch (RespuestaWebPayPlus[0].ChildNodes[i].Name)
        //            {
        //                case "reference":
        //                    reference = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
        //                    break;
        //                case "response":
        //                    response = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
        //                    break;
        //                case "foliocpagos":
        //                    foliocpagos = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
        //                    break;
        //                case "auth":
        //                    auth = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
        //                    break;
        //                case "date":
        //                    fecha = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
        //                    break;
        //                case "time":
        //                    Hora = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
        //                    break;
        //                case "cc_type":
        //                    cc_type = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
        //                    break;
        //                case "tp_operation":
        //                    tp_operation = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
        //                    break;
        //                case "cc_number":
        //                    cc_number = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
        //                    break;
        //                case "amount":
        //                    amount = RespuestaWebPayPlus[0].ChildNodes[i].InnerText;
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        VMPagos MVPagos = new VMPagos();
        //        string fecha1 = fecha + " " + Hora;
        //        DateTime fechaRegistro = DateTime.MinValue;
        //        switch (response)
        //        {
        //            case "denied":
        //                fechaRegistro = DateTime.Now;

        //                cc_type = "denied";
        //                auth = "denied";
        //                tp_operation = "denied";
        //                amount = "0.0";
        //                //respuesta.Data = MVPagos.AgregarInformacionTarjeta(auth, reference, fechaRegistro, response, cc_type, tp_operation, amount);
        //                break;
        //            case "approved":
        //                fechaRegistro = DateTime.Parse(fecha1);
        //                //respuesta.Data = MVPagos.AgregarInformacionTarjeta(auth, reference, fechaRegistro, response, cc_type, tp_operation, amount);
        //                break;
        //            case "error":
        //                fechaRegistro = DateTime.Now;
        //                cc_type = "error";
        //                auth = "error";
        //                tp_operation = "error";
        //                amount = "0.0";
        //                break;
        //        }
        //        respuesta.Data = MVPagos.AgregarInformacionTarjeta(auth, reference, fechaRegistro, response, cc_type, tp_operation, amount);

        //    }




        //    return respuesta;
        //}

        // PUT: api/Pagos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pagos/5
        public void Delete(int id)
        {
        }
    }

    public class RespuestaPago 
    {
        private string _strResponse;

        public string StrResponse
        {
            get { return _strResponse; }
            set { _strResponse = value; }
        }

    }
}
