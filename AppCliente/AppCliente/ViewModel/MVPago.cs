using AppCliente.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using VistaDelModelo;
using Xamarin.Forms;

namespace AppCliente.ViewModel
{
    public class MVPago
    {
        #region Propiedades
        private string _NombreTipoDePago;

        public string NombreTipoDePago
        {
            get { return _NombreTipoDePago; }
            set { _NombreTipoDePago = value; }
        }
        private string _IconoTipoDePago;

        public string IconoTipoDePago
        {
            get { return _IconoTipoDePago; }
            set { _IconoTipoDePago = value; }
        }

        private string _DescripcionPago;

        public string DescripcionPago
        {
            get { return _DescripcionPago; }
            set { _DescripcionPago = value; }
        }

        #endregion

        public MVPago()
        {

        }

        #region Metodos
        public async static void EnviarOrdenASucursales(decimal totalAPagar, string uidUsuario, string uidDireccion, string UidFormaDeCobro, Guid UidOrden, Guid UidPago)
        {

            Guid UidUsuario = new Guid(uidUsuario);
            decimal total = totalAPagar;
            Guid UidDireccion = new Guid(uidDireccion);


            if (App.MVProducto.ListaDelCarrito.Count > 0)
            {
                if (!App.MVProducto.ListaDelInformacionSucursales.Exists(t => t.UidTarifario == Guid.Empty))
                {                    //Guarda la orden con la sucursal
                    for (int i = 0; i < App.MVProducto.ListaDelCarrito.Count; i++)
                    {
                        VMProducto objeto = App.MVProducto.ListaDelInformacionSucursales.Find(Suc => Suc.UidSucursal == App.MVProducto.ListaDelCarrito[i].UidSucursal);
                        var objetos = App.MVProducto.ListaDelCarrito.FindAll(Suc => Suc.UidSucursal == App.MVProducto.ListaDelCarrito[i].UidSucursal);
                        decimal totalSucursal = 0.0m;
                        Guid UidOrdenSucursal = Guid.NewGuid();
                        //Envia la orden a la sucursal suministradora
                        Random Codigo = new Random();
                        long CodigoDeEnrega = Codigo.Next(00001, 99999);

                        foreach (var item in objetos)
                        {
                            totalSucursal = totalSucursal + item.Subtotal;
                            //Guarda la relacion con los productos
                            Guid Uidnota = new Guid();
                            string mensaje = "";
                            if (item.UidNota == null || item.UidNota == Guid.Empty)
                            {
                                Uidnota = Guid.Empty;
                            }
                            else
                            {
                                Uidnota = item.UidNota;
                            }
                            if (!string.IsNullOrEmpty(item.StrNota) && item.StrNota != null)
                            {
                                mensaje = item.StrNota;
                            }
                            else
                            {
                                mensaje = "sin nota";
                            }
                            string _Url = $"" + Helpers.Settings.sitio + "/api/Orden/GetGuardarProductos?" +
                                $"UIDORDEN={UidOrdenSucursal}" +
                                $"&UIDSECCIONPRODUCTO={item.UidSeccionPoducto}" +
                                $"&INTCANTIDAD={item.Cantidad}" +
                                $"&STRCOSTO={item.StrCosto}" +
                                $"&UidSucursal={item.UidSucursal}" +
                                $"&UidRegistroEncarrito={item.UidRegistroProductoEnCarrito}" +
                                $"&UidNota={Uidnota}" +
                                $"&StrMensaje={mensaje}" +
                                $"&UidTarifario={objeto.UidTarifario}";
                            using (HttpClient _client = new HttpClient())
                            {
                                await _client.GetAsync(_Url);
                            }
                        }
                        string _Url1 = $"" + Helpers.Settings.sitio + "/api/Orden/GetGuardarOrden?" +
                            $"UIDORDEN={UidOrden}" +
                            $"&Total={total}" +
                            $"&Uidusuario={UidUsuario}" +
                            $"&UidDireccion={UidDireccion}" +
                            $"&Uidsucursal={objeto.UidSucursal}" +
                            $"&totalSucursal={totalSucursal}" +
                            $"&UidRelacionOrdenSucursal={UidOrdenSucursal}" +
                            $"&LngCodigoDeEntrega={CodigoDeEnrega}" +
                            $"&UidTarifario={objeto.UidTarifario}";

                        using (HttpClient _client = new HttpClient())
                        {
                            await _client.GetAsync(_Url1);
                        }

                        // Envia la orden a la sucursal distribuidora
                        string _Url2 = $@"" + Helpers.Settings.sitio + "/api/Tarifario/GetGuardarTarifario?" +
                            $"UidOrdenSucursal={UidOrdenSucursal}" +
                            $"&DPropina={objeto.DPropina}" +
                            $"&UidTarifario={objeto.UidTarifario}";
                        using (HttpClient _client = new HttpClient())
                        {
                            await _client.GetAsync(_Url2);
                        }

                        //Una vez que se haya guardado ella basededatos se le cambia el estatus a la orden
                        string _Url3 = $"" + Helpers.Settings.sitio + "/api/Orden/GetAgregaEstatusALaOrden?" +
                            $"UidEstatus=DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC" +
                            $"&StrParametro=U" +
                            $"&UidOrden={UidOrdenSucursal}" +
                            $"&UidSucursal={objeto.UidSucursal}";
                        using (HttpClient _client = new HttpClient())
                        {
                            await _client.GetAsync(_Url3);
                        }
                        App.MVProducto.ListaDelCarrito.RemoveAll(sucursal => sucursal.UidSucursal == objeto.UidSucursal);
                        i = i - 1;
                    }


                    //Inserta el registro del modo de pago
                    string _UrlPago = $"" + Helpers.Settings.sitio + "/api/Pagos/GetInsertarPago?" +
                        $"UIDORDEN={UidOrden}" +
                        $"&UidPago={UidPago}" +
                        $"&UidFormaDeCobro={UidFormaDeCobro}" +
                        $"&MMonto={total}" +
                        $"&UidEstatusDeCobro=E728622B-97D7-431F-B01C-7E0B5F8F3D31";

                    using (HttpClient _client = new HttpClient())
                    {
                        var content1 = await _client.GetAsync(_UrlPago);
                    }

                    LimpiarCarrito();
                    App.MVOrden.ObtenerInformacionDeLaUltimaOrden(UidUsuario);
                    switch (UidFormaDeCobro.ToUpper())
                    {
                        //Pago en efectivo
                        case "6518C044-CE40-41F4-9344-92F0C200A8C2":
                            GenerateMessage("Felicidades!", "Su orden se ha pagado en efectivo", "OK");
                            break;
                        //Pago en Tarjeta
                        case "30545834-7FFE-4D1A-AA94-D6E569371C60":
                            GenerateMessage("Felicidades!", "Su orden se ha pagado con Tarjeta", "OK");
                            break;
                        //Pago en Monedero
                        case "13DC10FE-FE47-48D6-A427-DD2F6DE0C564":
                            string _Url = $"" + Helpers.Settings.sitio + "/api/Monedero/Get?" +
                               $"id={App.Global1}";
                            var content = "";
                            using (HttpClient _client = new HttpClient())
                            {
                                content = await _client.GetStringAsync(_Url);
                            }
                            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                            VMMonedero MVMonedero;
                            MVMonedero = JsonConvert.DeserializeObject<VMMonedero>(obj);
                            GenerateMessage("Felicidades!", "Su orden se ha pagado con Monedero.\n Saldo disponible $" + MVMonedero.MMonto.ToString("N2") + " ", "OK");
                            break;
                    }

                }
                else
                {
                    GenerateMessage("Sucursal no seleccionada", "No se ha elegido una empresa distribuidora dentro de algun pedido", "ok");
                }
            }
        }

        protected static void LimpiarCarrito()
        {
            App.MVProducto.ListaDelCarrito.Clear();
            App.MVProducto.ListaDelInformacionSucursales.Clear();
        }

        protected async static void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }
        #endregion
    }
}
