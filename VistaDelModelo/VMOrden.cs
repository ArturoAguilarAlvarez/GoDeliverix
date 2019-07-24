using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VistaDelModelo
{
    public class VMOrden
    {
        #region Propiedades
        Orden oOrden;
        Conexion Datos;

        public string CodigoOrdenTarifario { get; set; }
        DBOrden oDBOrden;
        private Guid _UidOrden;

        public Guid Uidorden
        {
            get { return _UidOrden; }
            set { _UidOrden = value; }
        }

        private Guid _UidOrdenTarifario;

        public Guid UidOrdenTarifario
        {
            get { return _UidOrdenTarifario; }
            set { _UidOrdenTarifario = value; }
        }

        private Guid _UidordenRepartidor;

        public Guid UidordenRepartidor
        {
            get { return _UidordenRepartidor; }
            set { _UidordenRepartidor = value; }
        }

        private Guid _UidOrdenSucursal;

        public Guid UidOrdenSucursal
        {
            get { return _UidOrdenSucursal; }
            set { _UidOrdenSucursal = value; }
        }

        private Guid _uidProducto;
        private string _strFecha;

        public string FechaDeOrden
        {
            get { return _strFecha; }
            set { _strFecha = value; }
        }

        public Guid UidProducto
        {
            get { return _uidProducto; }
            set { _uidProducto = value; }
        }
        private decimal _dbTotal;

        public decimal MTotal
        {
            get { return _dbTotal; }
            set { _dbTotal = value; }
        }
        private long _LngCodigoDeEntrega;

        public long LngCodigoDeEntrega
        {
            get { return _LngCodigoDeEntrega; }
            set { _LngCodigoDeEntrega = value; }
        }



        private long _lngFolio;

        public long LNGFolio
        {
            get { return _lngFolio; }
            set { _lngFolio = value; }
        }
        private Guid _UidCodigo;

        public Guid UidCodigo
        {
            get { return _UidCodigo; }
            set { _UidCodigo = value; }
        }
        private string _StrNombreSucursal;

        public string StrNombreSucursal
        {
            get { return _StrNombreSucursal; }
            set { _StrNombreSucursal = value; }
        }
        private DateTime _dtmFecha;

        public DateTime DtmFecha
        {
            get { return _dtmFecha; }
            set { _dtmFecha = value; }
        }


        //Campos para wpf
        public string Visible { get; set; }
        public string Imagen { get; set; }
        public string VisibilidadNota { get; set; }
        public bool Seleccion { get; set; }

        public string UidRelacionOrdenSucursal { get; set; }
        public string Identificador { get; set; }
        public string MTotalSucursal { get; set; }
        public string CostoEnvio { get; set; }

        public string StrNota { get; set; }
        public double MCostoTarifario { get; set; }
        public double MSubtotalSucursal { get; set; }
        private string _StrEstatusOrdenSucursal;

        public string StrEstatusOrdenSucursal
        {
            get { return _StrEstatusOrdenSucursal; }
            set { _StrEstatusOrdenSucursal = value; }
        }

        public string StrEstatusOrdenTarifario { get; private set; }


        private string _StrEstatusOrdenRepartidor;

        public string StrEstatusOrdenRepartidor
        {
            get { return _StrEstatusOrdenRepartidor; }
            set { _StrEstatusOrdenRepartidor = value; }
        }

        public string StrEstatusOrdenGeneral { get; set; }
        public string StrNombreProducto { get; set; }
        public int intCantidad { get; set; }
        public Guid UidEstatus { get; set; }
        public Guid UidSucursal { get; set; }
        public Guid UidProductoEnOrden { get; set; }
        public Guid UidDireccionSucursal { get; set; }
        public Guid UidDireccionCliente { get; set; }
        private List<VMOrden> _ListaDeProductos;

        public List<VMOrden> ListaDeProductos
        {
            get { return _ListaDeProductos; }
            set { _ListaDeProductos = value; }
        }

        private List<VMOrden> _ListaDeOrdenes;

        public List<VMOrden> ListaDeOrdenes
        {
            get { return _ListaDeOrdenes; }
            set { _ListaDeOrdenes = value; }
        }

        private List<VMOrden> _ListaDeOrdenesCanceladas;

        public List<VMOrden> ListaDeOrdenesCanceladas
        {
            get { return _ListaDeOrdenesCanceladas; }
            set { _ListaDeOrdenesCanceladas = value; }
        }


        public List<VMOrden> ListaDeBitacoraDeOrdenes = new List<VMOrden>();
        public List<VMOrden> ListaDeInformacionDeOrden = new List<VMOrden>();

        public List<VMOrden> ListaDeOrdenesPorConfirmar = new List<VMOrden>();
        public List<VMOrden> ListaDeOrdenesPorElaborar = new List<VMOrden>();
        public List<VMOrden> ListaDeOrdenesPorEnviar = new List<VMOrden>();
        public List<VMOrden> ListaDeOrdenesEnviadas = new List<VMOrden>();
        public List<VMOrden> ListaDeOrdenesEmpresa = new List<VMOrden>();
        public List<VMOrden> ListaDeOrdenesCanceladasPermanentes = new List<VMOrden>();
        #endregion


        #region Metodos
        public bool GuardaOrden(Guid UIDORDEN, decimal Total, Guid Uidusuario, Guid UidDireccion, Guid Uidsucursal, decimal totalSucursal, Guid UidRelacionOrdenSucursal, long LngCodigoDeEntrega)
        {
            bool resultado = false;
            oOrden = new Orden();
            try
            {
                oOrden = new Orden
                {
                    UidDireccion = UidDireccion,
                    UidUsuario = Uidusuario,
                    Uidorden = UIDORDEN,
                    MTotal = Total,
                    UidSucursal = Uidsucursal,
                    TotalSucursal = totalSucursal,
                    LngCodigoDeEntrega = LngCodigoDeEntrega,
                    UidRelacionOrdenSucursal = UidRelacionOrdenSucursal
                };
                resultado = oOrden.GuardarOrden();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public void ObtenerCodigoOrdenTarifario(Guid uidOrdenTarifario)
        {
            try
            {
                oDBOrden = new DBOrden();

                foreach (DataRow item in oDBOrden.RecuperarCodigoOrdenTarifario(uidOrdenTarifario).Rows)
                {
                    CodigoOrdenTarifario = item["UidCodigo"].ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarCodigoUsuario(string strCodigo, string UidRepartidor)
        {
            bool resultado = false;
            try
            {
                oDBOrden = new DBOrden();
                if (oDBOrden.VerificaCodigoDeEntrega(strCodigo, UidRepartidor).Rows.Count == 1)
                {
                    resultado = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool GuardaProducto(Guid UIDORDEN, Guid UIDSECCIONPRODUCTO, int INTCANTIDAD, string STRCOSTO, Guid UidSucursal, Guid UidRegistroEncarrito, Guid UidNota, String StrMensaje)
        {
            bool resultado = false;
            try
            {
                oOrden = new Orden();
                resultado = oOrden.GuardarProductos(UIDORDEN, UIDSECCIONPRODUCTO, INTCANTIDAD, STRCOSTO, UidSucursal, UidRegistroEncarrito, UidNota, StrMensaje);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public DataTable ObtenerSucursaleDeOrden(Guid UidOrden)
        {
            Datos = new Conexion();
            DataTable DatoQuery = new DataTable();
            try
            {
                string Query = $"select distinct OS.UidRelacionOrdenSucursal,os.BintCodigoEntrega, i.NVchRuta, S.Identificador, (cast(os.MTotalSucursal as decimal(10, 2)) + cast(t.MCosto as decimal(10, 2))) as MTotal, os.IntFolio as LNGFolio, cast(os.MTotalSucursal as decimal(10, 2)) as MTotalSucursal,s.uidSucursal,cast(t.MCosto as decimal(10, 2)) as CostoEnvio from Ordenes o inner   join OrdenSucursal OS on o.UidOrden = OS.UidOrden                                                                                                                   inner  join Sucursales S on s.UidSucursal = OS.UidSucursal inner  join OrdenTarifario ot on ot.UidOrden = os.UidRelacionOrdenSucursal                                                                                                                   inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario                                                                                                                   inner join OrdenProducto op on op.UidOrden = os.UidRelacionOrdenSucursal inner join SeccionProducto sp on sp.UidSeccionProducto = op.UidSeccionProducto inner join ImagenEmpresa IE on IE.UidEmpresa = S.UidEmpresa inner join Imagenes i on i.UIdImagen = IE.UidImagen where o.UidOrden = '{ UidOrden.ToString()}' and i.NVchRuta like '%FotoPerfil%' ";
                DatoQuery = Datos.Consultas(Query);
            }
            catch (Exception)
            {

                throw;
            }
            return DatoQuery;
        }

        public void ObtenerInformacionDeLaUltimaOrden(Guid uidUsuario)
        {
            Datos = new Conexion();
            try
            {

                string Query = "select s.Identificador,os.MTotalSucursal,os.IntFolio, t.MCosto,o.MTotal,os.BIntCodigoEntrega from OrdenSucursal os inner join Ordenes o on o.UidOrden = os.UidOrden inner join Sucursales s on s.UidSucursal = os.UidSucursal inner join OrdenTarifario ot on ot.UidOrden = os.UidRelacionOrdenSucursal inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario inner join OrdenUsuario ou on ou.UidOrden = o.UidOrden where ou.UidUsuario = '" + uidUsuario.ToString() + "'";
                ListaDeInformacionDeOrden = new List<VMOrden>();
                foreach (DataRow item in Datos.Consultas(Query).Rows)
                {
                    ListaDeInformacionDeOrden.Add(new VMOrden()
                    {
                        StrNombreSucursal = item["Identificador"].ToString(),
                        MCostoTarifario = double.Parse(item["MCosto"].ToString()),
                        MSubtotalSucursal = double.Parse(item["MTotalSucursal"].ToString()),
                        LNGFolio = long.Parse(item["IntFolio"].ToString()),
                        LngCodigoDeEntrega = long.Parse(item["BIntCodigoEntrega"].ToString())
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Obtiene la lista de productos que tiene una orden de una sucursal, esta retorna una ListaDeProductos
        /// </summary>
        /// <param name="uidRelacionordenSucursal"></param>
        public void ObtenerProductosDeOrden(string uidRelacionordenSucursal)
        {
            Datos = new Conexion();
            try
            {
                string Query = "select distinct s.UidSucursal,op.UidListaDeProductosEnOrden,i.NVchRuta,p.uidproducto, s.Identificador,p.VchNombre,op.IntCantidad,op.MTotal,t.MCosto as tarifario from Ordenes o inner join OrdenSucursal os on os.UidOrden = o.UidOrden inner join Sucursales s on s.UidSucursal = os.UidSucursal inner join OrdenProducto op on os.UidRelacionOrdenSucursal = op.UidOrden inner join SeccionProducto sp on sp.UidSeccionProducto = op.UidSeccionProducto inner join Productos p on p.UidProducto = sp.UidProducto inner join OrdenTarifario ot on ot.UidOrden = os.UidRelacionOrdenSucursal inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario inner join ImagenProducto iproduc on iproduc.UidProducto = p.UidProducto  inner join Imagenes i on i.UIdImagen = iproduc.UidImagen where os.UidRelacionOrdenSucursal = '" + uidRelacionordenSucursal + "'";

                ListaDeProductos = new List<VMOrden>();
                foreach (DataRow item in Datos.Consultas(Query).Rows)
                {
                    string TieneNota = "Hidden";
                    if (VerificaExistenciaDeNotaEnOrden(item["UidListaDeProductosEnOrden"].ToString()))
                    {
                        TieneNota = "Visible";
                    }
                    //Muestra el total con 2 decimales a la derecha
                    decimal mt = decimal.Parse(item["MTotal"].ToString());
                    string Total = mt.ToString("N2");
                    ListaDeProductos.Add(new VMOrden()
                    {
                        UidProducto = new Guid(item["uidproducto"].ToString()),
                        UidProductoEnOrden = new Guid(item["UidListaDeProductosEnOrden"].ToString()),
                        StrNombreSucursal = item["Identificador"].ToString(),
                        StrNombreProducto = item["VchNombre"].ToString(),
                        //Imagen = item["NVchRuta"].ToString(),
                        Imagen = "http://godeliverix.net/Vista/" + item["NVchRuta"].ToString(),
                        intCantidad = int.Parse(item["IntCantidad"].ToString()),
                        UidSucursal = new Guid(item["UidSucursal"].ToString()),
                        MTotal = decimal.Parse(Total),
                        MCostoTarifario = double.Parse(item["tarifario"].ToString()),
                        VisibilidadNota = TieneNota
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable ObtenerProducto(string UIDPRODUCTOENLISTA)
        {
            Datos = new Conexion();
            DataTable DatoQuery = new DataTable();
            try
            {
                string Query = "select p.VchNombre,op.intCantidad, op.MTotal, op.UidListaDeProductosEnOrden, i.NVchRuta from ordenes o inner join OrdenProducto op on o.UidOrden = op.UidOrden inner join SeccionProducto sp on sp.UidSeccionProducto = op.UidSeccionProducto inner join Productos p on p.UidProducto = sp.UidProducto inner join ImagenProducto IP on IP.UidProducto = p.UidProducto inner join imagenes i on i.uidImagen = IP.UidImagen where op.UidListaDeProductosEnOrden = '" + UIDPRODUCTOENLISTA + "'";
                DatoQuery = Datos.Consultas(Query);
            }
            catch (Exception)
            {

                throw;
            }
            return DatoQuery;
        }

        public string ObtenerUsuarioPorUidOrdenSucursal(Guid uidOrden)
        {
            oDBOrden = new DBOrden();
            string UidUsuario = "";
            foreach (DataRow item in oDBOrden.ObtenerUidUsuarioPorUidOrdenSucursal(uidOrden).Rows)
            {
                UidUsuario = item["Uidusuario"].ToString();
            }
            return UidUsuario;
        }

        public void ObtenerOrdenesCliente(string UidUsuario, string parametro)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "asp_BuscarOrdenes";

                comando.Parameters.Add("@Uidusuario", SqlDbType.UniqueIdentifier);
                comando.Parameters["@Uidusuario"].Value = new Guid(UidUsuario);

                comando.Parameters.Add("@Parametro", SqlDbType.VarChar, 20);
                comando.Parameters["@Parametro"].Value = parametro;

                Datos = new Conexion();

                ListaDeOrdenes = new List<VMOrden>();


                foreach (DataRow item in Datos.Busquedas(comando).Rows)
                {
                    long CodigoEntrega = 00000;
                    if (item.Table.Columns.Contains("BIntCodigoEntrega"))
                    {
                        CodigoEntrega = long.Parse(item["BIntCodigoEntrega"].ToString());
                    }
                    VMOrden orden = new VMOrden() { Uidorden = new Guid(item["UidOrden"].ToString()), LngCodigoDeEntrega = CodigoEntrega, FechaDeOrden = item["DtmFechaDeCreacion"].ToString(), MTotal = decimal.Parse(item["MTotal"].ToString()), LNGFolio = int.Parse(item["intFolio"].ToString()) };
                    ListaDeOrdenes.Add(orden);
                }
            }
            catch (Exception)
            {

            }
        }

        public void BuscarOrdenes(string Parametro, Guid Uidusuario = new Guid(), string FechaInicial = "", string FechaFinal = "", string NumeroOrden = "", Guid UidLicencia = new Guid(), string EstatusSucursal = "", string TipoDeSucursal = "", Guid UidOrdenSucursal = new Guid())
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "asp_BuscarOrdenes";

                comando.Parameters.Add("@Parametro", SqlDbType.VarChar, 20);
                comando.Parameters["@Parametro"].Value = Parametro;

                if (Uidusuario != Guid.Empty)
                {
                    comando.Parameters.Add("@Uidusuario", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@Uidusuario"].Value = Uidusuario;
                }

                if (Uidorden != Guid.Empty)
                {
                    comando.Parameters.Add("@UidOrdenSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidOrdenSucursal"].Value = Uidorden;
                }
                if (UidLicencia != Guid.Empty)
                {
                    comando.Parameters.Add("@Licencia", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@Licencia"].Value = UidLicencia;
                }
                if (!string.IsNullOrEmpty(TipoDeSucursal))
                {
                    comando.Parameters.Add("@Tipo", SqlDbType.Char, 1);
                    comando.Parameters["@Tipo"].Value = char.Parse(TipoDeSucursal);
                }
                if (!string.IsNullOrWhiteSpace(FechaInicial))
                {
                    comando.Parameters.Add("@FechaInicial", SqlDbType.VarChar, 10);
                    comando.Parameters["@FechaInicial"].Value = FechaInicial;
                }

                if (!string.IsNullOrWhiteSpace(FechaFinal))
                {
                    comando.Parameters.Add("@FechaFinal", SqlDbType.VarChar, 10);
                    comando.Parameters["@FechaFinal"].Value = FechaFinal;
                }
                if (!string.IsNullOrWhiteSpace(NumeroOrden))
                {
                    comando.Parameters.Add("@NumeroDeOrden", SqlDbType.BigInt);
                    comando.Parameters["@NumeroDeOrden"].Value = long.Parse(NumeroOrden);
                }
                Datos = new Conexion();

                ListaDeOrdenes = new List<VMOrden>();
                switch (Parametro)
                {
                    case "Usuario":
                        foreach (DataRow item in Datos.Busquedas(comando).Rows)
                        {
                            VMOrden orden = new VMOrden() { Uidorden = new Guid(item["UidOrden"].ToString()), FechaDeOrden = item["DtmFechaDeCreacion"].ToString(), MTotal = decimal.Parse(item["MTotal"].ToString()), LNGFolio = int.Parse(item["IntFolio"].ToString()) };
                            ListaDeOrdenes.Add(orden);
                        }
                        break;
                    case "Sucursal":
                        #region Estatus de suministradora
                        if (EstatusSucursal == "Pendientes a confirmar")
                        {
                            foreach (DataRow item in Datos.Busquedas(comando).Rows)
                            {
                                string imagen = string.Empty;

                                //Estatus pendiente 
                                if (item["estatus"].ToString() == "DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "B40D954D-D408-4769-B110-608436C490F1")
                                {
                                    imagen = "Package";
                                    //Convierte el total en decimales con 2 numeros a la derecha
                                    decimal MT = decimal.Parse(item["MTotalSucursal"].ToString());
                                    string Total = MT.ToString("N2");
                                    if (!ListaDeOrdenes.Exists(o => o.Uidorden == new Guid(item["UidRelacionOrdenSucursal"].ToString())))
                                    {
                                        ListaDeOrdenes.Add(new VMOrden()
                                        {
                                            Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()),
                                            LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                            MTotal = decimal.Parse(Total),
                                            FechaDeOrden = item["DtmFechaDeCreacion"].ToString(),
                                            intCantidad = int.Parse(item["intCantidad"].ToString()),
                                            Imagen = imagen
                                        });
                                    }

                                }
                            }
                        }
                        if (EstatusSucursal == "Pendiente para elaborar")
                        {
                            foreach (DataRow item in Datos.Busquedas(comando).Rows)
                            {
                                string imagen = string.Empty;
                                //Estatus pendiente a crear pero  confirmado
                                if (item["estatus"].ToString() == "DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {
                                    imagen = "Imagenes/Pendiente.png";
                                }
                                else
                                //Estatus creando y confirmado
                                if (item["estatus"].ToString() == "2d2f38b8-7757-45fb-9ca6-6ecfe20356ed".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {
                                    imagen = "Imagenes/Creado.png";
                                }
                                if ((item["estatus"].ToString() == "DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC".ToLower() || item["estatus"].ToString() == "2d2f38b8-7757-45fb-9ca6-6ecfe20356ed") && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {

                                    if (!ListaDeOrdenes.Exists(o => o.Uidorden == new Guid(item["UidRelacionOrdenSucursal"].ToString())))
                                    {
                                        ListaDeOrdenes.Add(new VMOrden()
                                        {
                                            Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()),
                                            LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                            MTotal = decimal.Parse(item["MTotalSucursal"].ToString()),
                                            FechaDeOrden = item["DtmFechaDeCreacion"].ToString(),
                                            Imagen = imagen,
                                            UidEstatus = new Guid(item["estatus"].ToString().ToLower())
                                        });
                                    }

                                }
                            }
                        }
                        if (EstatusSucursal == "Lista a enviar")
                        {
                            foreach (DataRow item in Datos.Busquedas(comando).Rows)
                            {
                                string imagen = string.Empty;
                                //Estatus elaborado
                                if (item["estatus"].ToString() == "C412D367-7D05-45D8-AECA-B8FABBF129D9".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {
                                    imagen = "Imagenes/Elaborada.png";
                                }
                                else //Estatus pendiente a elaborar
                                if (item["estatus"].ToString() == "DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {
                                    imagen = "Imagenes/Pendiente.png";
                                }
                                else
                                //Estatus creando y confirmado
                                if (item["estatus"].ToString() == "2d2f38b8-7757-45fb-9ca6-6ecfe20356ed".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {
                                    imagen = "Imagenes/Creado.png";
                                }
                                //Estatus enviado

                                if ((item["estatus"].ToString() == "C412D367-7D05-45D8-AECA-B8FABBF129D9".ToLower() || item["estatus"].ToString() == "2d2f38b8-7757-45fb-9ca6-6ecfe20356ed".ToLower()) && item["EstatusOrdenSucursal"].ToString().ToUpper() != "EAE7A7E6-3F19-405E-87A9-3162D36CE21B")

                                {
                                    if (!ListaDeOrdenes.Exists(o => o.Uidorden == new Guid(item["UidRelacionOrdenSucursal"].ToString())))
                                    {
                                        ListaDeOrdenes.Add(new VMOrden()
                                        {
                                            Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()),
                                            LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                            MTotal = decimal.Parse(item["MTotalSucursal"].ToString()),
                                            FechaDeOrden = item["DtmFechaDeCreacion"].ToString(),
                                            Imagen = imagen
                                        });
                                    }

                                }
                            }
                        }
                        if (EstatusSucursal == "Canceladas")
                        {
                            ListaDeOrdenesCanceladas = new List<VMOrden>(); ;
                            foreach (DataRow item in Datos.Busquedas(comando).Rows)
                            {
                                string imagen = string.Empty;
                                //Estatus Cancelado 
                                if (item["EstatusOrdenSucursal"].ToString().ToUpper() == "EAE7A7E6-3F19-405E-87A9-3162D36CE21B")
                                {
                                    imagen = "Cancel";
                                    TimeSpan TiempoDeVida = new TimeSpan();
                                    TimeSpan TiempoRestante = new TimeSpan();
                                    DateTime horaActual = DateTime.Now;
                                    DateTime FechaRegistro = DateTime.Parse(item["FechaDeEstatusOrdenSucursal"].ToString());
                                    TimeSpan Diferencia = new TimeSpan();
                                    Diferencia = (horaActual - FechaRegistro);

                                    TiempoDeVida = new TimeSpan(0, 2, 0);
                                    TiempoRestante = TiempoDeVida - Diferencia;
                                    //Verifica que la orden cancelada sea menor a 2 minutos
                                    if (Diferencia.Minutes < 2)
                                    {

                                        //Agrega la ordenes y marca cuanto tiempo le queda de vida a la orden.
                                        ListaDeOrdenes.Add(new VMOrden()
                                        {
                                            Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()),
                                            LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                            MTotal = decimal.Parse(item["MTotalSucursal"].ToString()),
                                            FechaDeOrden = TiempoRestante.Minutes.ToString() + ":" + TiempoRestante.Seconds.ToString(),
                                            Imagen = imagen
                                        });
                                    }
                                    else
                                    {
                                        //Cambia el estatus de la orden al cliente y le mada mensaje del por que se cancelo,
                                        AgregaEstatusALaOrden(new Guid("EAE7A7E6-3F19-405E-87A9-3162D36CE21B"), "S", Mensaje: new Guid(item["UidMensaje"].ToString()), UidOrden: new Guid(item["UidRelacionOrdenSucursal"].ToString()), UidLicencia: UidLicencia);
                                        //Agrega a la lista las ordenes canceladas definitivamente
                                        ListaDeOrdenesCanceladas.Add(new VMOrden()
                                        {
                                            Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()),
                                            LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                            MTotal = decimal.Parse(item["MTotalSucursal"].ToString()),
                                            FechaDeOrden = TiempoRestante.Minutes.ToString() + ":" + TiempoRestante.Seconds.ToString(),
                                            Imagen = imagen
                                        });
                                    }
                                }
                            }
                        }
                        //Para la lista de la bitacora
                        if (EstatusSucursal == "Enviadas")
                        {
                            foreach (DataRow item in Datos.Busquedas(comando).Rows)
                            {
                                string imagen = string.Empty;
                                //Verifica si la orden no ha sido enviada
                                if (item["EstatusOrdenSucursal"].ToString() == "0D711229-02A4-4952-BEB5-8CB144040A5A".ToLower())
                                {
                                    ListaDeOrdenes.Add(new VMOrden()
                                    {
                                        Uidorden = new Guid(item["UidOrden"].ToString()),
                                        LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                        MTotal = decimal.Parse(item["MTotalSucursal"].ToString()),
                                        FechaDeOrden = item["DtmFechaDeCreacion"].ToString(),
                                        Imagen = imagen
                                    });
                                }
                            }
                        }
                        #endregion
                        break;
                    default:
                        break;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void BuscarOrdenesAppSucursal(string Parametro, Guid Uidusuario = new Guid(), string FechaInicial = "", string FechaFinal = "", string NumeroOrden = "", Guid UidLicencia = new Guid(), string EstatusSucursal = "", string TipoDeSucursal = "", Guid UidOrdenSucursal = new Guid())
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "asp_BuscarOrdenes";

                comando.Parameters.Add("@Parametro", SqlDbType.VarChar, 20);
                comando.Parameters["@Parametro"].Value = Parametro;

                if (Uidusuario != Guid.Empty)
                {
                    comando.Parameters.Add("@Uidusuario", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@Uidusuario"].Value = Uidusuario;
                }

                if (Uidorden != Guid.Empty)
                {
                    comando.Parameters.Add("@UidOrdenSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidOrdenSucursal"].Value = Uidorden;
                }
                if (UidLicencia != Guid.Empty)
                {
                    comando.Parameters.Add("@Licencia", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@Licencia"].Value = UidLicencia;
                }
                if (!string.IsNullOrEmpty(TipoDeSucursal))
                {
                    comando.Parameters.Add("@Tipo", SqlDbType.Char, 1);
                    comando.Parameters["@Tipo"].Value = char.Parse(TipoDeSucursal);
                }
                if (!string.IsNullOrWhiteSpace(FechaInicial))
                {
                    comando.Parameters.Add("@FechaInicial", SqlDbType.VarChar, 10);
                    comando.Parameters["@FechaInicial"].Value = FechaInicial;
                }

                if (!string.IsNullOrWhiteSpace(FechaFinal))
                {
                    comando.Parameters.Add("@FechaFinal", SqlDbType.VarChar, 10);
                    comando.Parameters["@FechaFinal"].Value = FechaFinal;
                }
                if (!string.IsNullOrWhiteSpace(NumeroOrden))
                {
                    comando.Parameters.Add("@NumeroDeOrden", SqlDbType.BigInt);
                    comando.Parameters["@NumeroDeOrden"].Value = long.Parse(NumeroOrden);
                }
                Datos = new Conexion();

                ListaDeOrdenes = new List<VMOrden>();
                switch (Parametro)
                {
                    case "Usuario":
                        foreach (DataRow item in Datos.Busquedas(comando).Rows)
                        {
                            VMOrden orden = new VMOrden() { Uidorden = new Guid(item["UidOrden"].ToString()), FechaDeOrden = item["DtmFechaDeCreacion"].ToString(), MTotal = decimal.Parse(item["MTotal"].ToString()), LNGFolio = int.Parse(item["IntFolio"].ToString()) };
                            ListaDeOrdenes.Add(orden);
                        }
                        break;
                    case "Sucursal":
                        #region Estatus de suministradora
                        if (EstatusSucursal == "Pendientes a confirmar")
                        {
                            ListaDeOrdenesPorConfirmar.Clear();
                            foreach (DataRow item in Datos.Busquedas(comando).Rows)
                            {
                                string imagen = string.Empty;

                                //Estatus pendiente 
                                if (item["estatus"].ToString() == "DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "B40D954D-D408-4769-B110-608436C490F1")
                                {
                                    imagen = "Package";
                                    //Convierte el total en decimales con 2 numeros a la derecha
                                    decimal MT = decimal.Parse(item["MTotalSucursal"].ToString());
                                    string Total = MT.ToString("N2");
                                    if (!ListaDeOrdenesPorConfirmar.Exists(o => o.Uidorden == new Guid(item["UidRelacionOrdenSucursal"].ToString())))
                                    {
                                        ListaDeOrdenesPorConfirmar.Add(new VMOrden()
                                        {
                                            Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()),
                                            LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                            MTotal = decimal.Parse(Total),
                                            FechaDeOrden = item["DtmFechaDeCreacion"].ToString(),
                                            intCantidad = int.Parse(item["intCantidad"].ToString()),


                                            Imagen = imagen
                                        });
                                    }

                                }
                            }
                        }
                        if (EstatusSucursal == "Pendiente para elaborar")
                        {
                            ListaDeOrdenesPorElaborar.Clear();
                            foreach (DataRow item in Datos.Busquedas(comando).Rows)
                            {
                                string imagen = string.Empty;
                                //Estatus pendiente a crear pero  confirmado
                                if (item["estatus"].ToString() == "DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {
                                    imagen = "Imagenes/Pendiente.png";
                                }
                                else
                                //Estatus creando y confirmado
                                if (item["estatus"].ToString() == "2d2f38b8-7757-45fb-9ca6-6ecfe20356ed".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {
                                    imagen = "Imagenes/Creado.png";
                                }
                                if ((item["estatus"].ToString() == "DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC".ToLower() || item["estatus"].ToString() == "2d2f38b8-7757-45fb-9ca6-6ecfe20356ed") && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {

                                    if (!ListaDeOrdenesPorElaborar.Exists(o => o.Uidorden == new Guid(item["UidRelacionOrdenSucursal"].ToString())))
                                    {
                                        ListaDeOrdenesPorElaborar.Add(new VMOrden()
                                        {
                                            Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()),
                                            LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                            MTotal = decimal.Parse(item["MTotalSucursal"].ToString()),
                                            FechaDeOrden = item["DtmFechaDeCreacion"].ToString(),
                                            Imagen = imagen,
                                            UidEstatus = new Guid(item["estatus"].ToString().ToLower())
                                        });
                                    }

                                }
                            }
                        }
                        if (EstatusSucursal == "Lista a enviar")
                        {
                            ListaDeOrdenesPorEnviar.Clear();
                            foreach (DataRow item in Datos.Busquedas(comando).Rows)
                            {
                                string imagen = string.Empty;
                                //Estatus elaborado
                                if (item["estatus"].ToString() == "C412D367-7D05-45D8-AECA-B8FABBF129D9".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {
                                    //imagen = "Imagenes/Elaborada.png";
                                    imagen = "Elaborada";
                                }
                                else //Estatus pendiente a elaborar
                                if (item["estatus"].ToString() == "DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {
                                    //imagen = "Imagenes/Pendiente.png";
                                    imagen = "Pendiente";
                                }
                                else
                                //Estatus creando y confirmado
                                if (item["estatus"].ToString() == "2d2f38b8-7757-45fb-9ca6-6ecfe20356ed".ToLower() && item["EstatusOrdenSucursal"].ToString().ToUpper() == "EC09BCDE-ADAC-441D-8CC1-798BC211E46E")
                                {
                                    //imagen = "Imagenes/Creado.png";
                                    imagen = "En elaboracion";
                                }
                                //Estatus enviado


                                string estatisss = item["estatus"].ToString();
                                if (item["estatus"].ToString() == "C412D367-7D05-45D8-AECA-B8FABBF129D9".ToLower() || item["estatus"].ToString() == "2d2f38b8-7757-45fb-9ca6-6ecfe20356ed".ToLower())
                                {
                                    if (!ListaDeOrdenesPorEnviar.Exists(o => o.Uidorden == new Guid(item["UidRelacionOrdenSucursal"].ToString())))
                                    {
                                        if (imagen == "Elaborada")
                                        {
                                            ListaDeOrdenesPorEnviar.Add(new VMOrden()
                                            {
                                                Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()),
                                                LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                                MTotal = decimal.Parse(item["MTotalSucursal"].ToString()),
                                                FechaDeOrden = item["DtmFechaDeCreacion"].ToString(),
                                                Imagen = imagen
                                            });
                                        }
                                    }

                                }
                            }
                        }
                        if (EstatusSucursal == "Canceladas")
                        {
                            ListaDeOrdenesCanceladas.Clear();
                            ListaDeOrdenesCanceladasPermanentes.Clear();
                            foreach (DataRow item in Datos.Busquedas(comando).Rows)
                            {
                                string imagen = string.Empty;
                                //Estatus Cancelado 
                                if (item["EstatusOrdenSucursal"].ToString().ToUpper() == "EAE7A7E6-3F19-405E-87A9-3162D36CE21B")
                                {
                                    imagen = "Cancel";
                                    TimeSpan TiempoDeVida = new TimeSpan();
                                    TimeSpan TiempoRestante = new TimeSpan();
                                    DateTime horaActual = DateTime.Now;
                                    DateTime FechaRegistro = DateTime.Parse(item["FechaDeEstatusOrdenSucursal"].ToString());
                                    TimeSpan Diferencia = new TimeSpan();
                                    Diferencia = (horaActual - FechaRegistro);

                                    TiempoDeVida = new TimeSpan(0, 2, 0);
                                    TiempoRestante = TiempoDeVida - Diferencia;
                                    //Verifica que la orden cancelada sea menor a 2 minutos
                                    if (Diferencia.Minutes < 2)
                                    {
                                        //Agrega la ordenes y marca cuanto tiempo le queda de vida a la orden.
                                        ListaDeOrdenesCanceladas.Add(new VMOrden()
                                        {
                                            Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()),
                                            LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                            MTotal = decimal.Parse(item["MTotalSucursal"].ToString()),
                                            FechaDeOrden = TiempoRestante.Minutes.ToString() + ":" + TiempoRestante.Seconds.ToString(),
                                            Imagen = imagen
                                        });
                                    }
                                    else
                                    {
                                        //Cambia el estatus de la orden al cliente y le mada mensaje del por que se cancelo,
                                        AgregaEstatusALaOrden(new Guid("EAE7A7E6-3F19-405E-87A9-3162D36CE21B"), "S", Mensaje: new Guid(item["UidMensaje"].ToString()), UidOrden: new Guid(item["UidRelacionOrdenSucursal"].ToString()), UidLicencia: UidLicencia);
                                        //Agrega a la lista las ordenes canceladas definitivamente
                                        ListaDeOrdenesCanceladasPermanentes.Add(new VMOrden()
                                        {
                                            Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()),
                                            LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                            MTotal = decimal.Parse(item["MTotalSucursal"].ToString()),
                                            FechaDeOrden = TiempoRestante.Minutes.ToString() + ":" + TiempoRestante.Seconds.ToString(),
                                            Imagen = imagen
                                        });
                                    }
                                }
                            }
                        }
                        //Para la lista de la bitacora
                        if (EstatusSucursal == "Enviadas")
                        {
                            foreach (DataRow item in Datos.Busquedas(comando).Rows)
                            {
                                string imagen = string.Empty;
                                //Verifica si la orden no ha sido enviada
                                if (item["EstatusOrdenSucursal"].ToString() == "0D711229-02A4-4952-BEB5-8CB144040A5A".ToLower())
                                {
                                    ListaDeOrdenes.Add(new VMOrden()
                                    {
                                        Uidorden = new Guid(item["UidOrden"].ToString()),
                                        LNGFolio = long.Parse(item["IntFolio"].ToString()),
                                        MTotal = decimal.Parse(item["MTotalSucursal"].ToString()),
                                        FechaDeOrden = item["DtmFechaDeCreacion"].ToString(),
                                        Imagen = imagen
                                    });
                                }
                            }
                        }
                        #endregion
                        break;
                    default:
                        break;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Obtiene la nota del producto en la orden mediante su Uid
        /// </summary>
        /// <param name="uidProductoEnOrden"></param>
        public void ObtenerNotaDeProductoEnOrden(Guid uidProductoEnOrden)
        {
            oDBOrden = new DBOrden();
            try
            {
                StrNota = string.Empty;
                foreach (DataRow item in oDBOrden.ObtenerNotaDeProducto(uidProductoEnOrden).Rows)
                {
                    StrNota = item["VchMensaje"].ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EliminarOrdenBitacoraListaDeBitacora(Guid uidorden, Guid UidLicencia)
        {
            if (ListaDeBitacoraDeOrdenes.Exists(Orden => Orden.Uidorden == uidorden))
            {
                VMOrden objeto = ListaDeBitacoraDeOrdenes.Find(Orden => Orden.Uidorden == uidorden);
                ListaDeBitacoraDeOrdenes.Remove(objeto);
                //Cambia el estatus a sin asignar
                AgregarEstatusOrdenEnSucursal(UidEstatus: new Guid("0D711229-02A4-4952-BEB5-8CB144040A5A"), cTipoDeSucursal: "S", UidOrden: objeto.Uidorden, UidLicencia: UidLicencia.ToString());
            }
        }

        public bool AgregaEstatusALaOrden(Guid UidEstatus, string StrParametro, Guid Mensaje = new Guid(), Guid UidOrden = new Guid(), long LngFolio = 0, Guid UidLicencia = new Guid(), Guid UidSucursal = new Guid())
        {
            bool resultado = false;
            oOrden = new Orden();
            try
            {
                oOrden = new Orden();
                if (Mensaje == Guid.Empty)
                {
                    oOrden.oMensaje = new Mensaje() { Uid = Guid.Empty };
                }
                else
                {
                    oOrden.oMensaje = new Mensaje() { Uid = Mensaje };
                }


                oOrden.Uidorden = UidOrden;

                oOrden.Estatus = UidEstatus;
                oOrden.LngFolio = LngFolio;
                resultado = oOrden.GuardaEstatus(UidLicencia, StrParametro, UidSucursal: UidSucursal);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public DataTable ObtenerEstatusOrden(string UidOrden)
        {
            Datos = new Conexion();
            string query = "select BO.DtmFecha,EO.VchNombre from BitacoraOrdenEstatus BO inner join EstatusDeOrden EO on BO.UidEstatusDeOrden = EO.UidEstatus inner join OrdenSucursal OS on OS.UidRelacionOrdenSucursal = BO.UidOrden where OS.UidRelacionOrdenSucursal = '" + UidOrden.ToString() + "' order by BO.DtmFecha asc";
            return Datos.Consultas(query);
        }


        #region Metodos de repartidores
        public void BuscarOrdenAsiganadaRepartidor(Guid UidUsuario)
        {
            oDBOrden = new DBOrden();
            Uidorden = Guid.Empty;
            foreach (DataRow item in oDBOrden.ObtenerOrdenRepartidor(UidUsuario).Rows)
            {
                Uidorden = new Guid(item["UidOrden"].ToString());
                UidOrdenTarifario = new Guid(item["UidRelacionOrdenTarifario"].ToString());
                UidOrdenSucursal = new Guid(item["UidRelacionOrdenSucursal"].ToString());
                UidordenRepartidor = new Guid(item["UidRelacionOrdenRepartidor"].ToString());
                UidSucursal = new Guid(item["UidSucursal"].ToString());
                UidDireccionCliente = new Guid(item["DireccionCliente"].ToString());
                LNGFolio = long.Parse(item["IntFolio"].ToString());
                StrNombreSucursal = item["Identificador"].ToString();
                StrEstatusOrdenTarifario = item["EstatusOrdenTarifario"].ToString();
                StrEstatusOrdenRepartidor = item["EstatusOrdenRepartidor"].ToString();
                StrEstatusOrdenGeneral = item["EstatusOrdenGeneral"].ToString();
            }
        }
        public void EliminarOrdenDeRepartidor(string UidRelacionOrdenRepartidor)
        {
            oDBOrden = new DBOrden();
            oDBOrden.eliminarRegistroOrdenRepartidor(UidRelacionOrdenRepartidor);
        }
        public void BuscarOrdenRepartidor(string strcodigo, string licencia)
        {
            oDBOrden = new DBOrden();
            foreach (DataRow item in oDBOrden.BuscarOrden(strcodigo, licencia).Rows)
            {
                Uidorden = new Guid(item["UidOrden"].ToString());
                LNGFolio = long.Parse(item["IntFolio"].ToString());
                StrNombreSucursal = item["Identificador"].ToString();
                StrEstatusOrdenSucursal = item["estatus"].ToString();
            }
        }
        #endregion

        #region WPF

        public bool AgregarEstatusOrdenEnSucursal(Guid UidEstatus, string cTipoDeSucursal, string UidLicencia = "", Guid UidOrden = new Guid(), long LngFolio = 0, Guid UidMensaje = new Guid())
        {
            bool resultado = false;
            oOrden = new Orden();
            try
            {
                oOrden = new Orden();
                oOrden.Uidorden = UidOrden;
                oOrden.Estatus = UidEstatus;
                oOrden.oMensaje = new Mensaje() { Uid = UidMensaje };
                oOrden.LngFolio = LngFolio;
                oOrden.cTipoDeSucursal = cTipoDeSucursal;
                if (!string.IsNullOrEmpty(UidLicencia))
                {
                    oOrden.uidLicencia = new Guid(UidLicencia);
                }
                resultado = oOrden.AgregaEstatusEnOrdenSucursal();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        /// <summary>
        /// Agrega la relacion de la orden con la sucrsal distribuidora
        /// </summary>
        /// <param name="uidorden"></param>
        /// <param name="UidSucursal"></param>
        /// <param name="Codigo"></param>
        /// /// <param name="LngFolio"></param>
        public void AsociarOrdenConSucursalDistribuidora(Guid uidorden = new Guid(), Guid UidSucursal = new Guid(), Guid Codigo = new Guid(), Guid UidLicencia = new Guid(), long LngFolio = 0)
        {
            try
            {
                oOrden = new Orden();
                oOrden.AsociarOrdenConDistribuidora(uidorden, UidSucursal, Codigo, UidLicencia, LngFolio);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UidLicencia"></param>
        public void ObtenerOrdenSucursalDistribuidora(string UidLicencia)
        {
            Datos = new Conexion();
            ListaDeBitacoraDeOrdenes.Clear();
            string query = "select  o.UidRelacionOrdenSucursal,OSD.UidRelacionOrdenSucursalDistribuidora, o.IntFolio,S.Identificador,OSD.UidCodigo,  convert(varchar,OSD.DtmFecha,7) as DtmFecha  from OrdenSucursalDistribuidora OSD inner join Sucursales S on S.UidSucursal = OSD.UidSucursalDistribuidora inner join OrdenSucursal o on o.UidRelacionOrdenSucursal = OSD.Uidorden and o.UidSucursal = (select Uidsucursal from SucursalLicencia where UidLicencia = '" + UidLicencia + "') where UidSucursalDistribuidora in (select UidSucursalDistribuidora from ContratoDeServicio where UidSucursalSuministradora in (select Uidsucursal from SucursalLicencia where UidLicencia = '" + UidLicencia + "'))";
            foreach (DataRow item in Datos.Consultas(query).Rows)
            {
                //Crea la variable donde se almacena el codigo 
                Guid uidcodigo = new Guid();
                //Valida que el codigo no sea null o este vacio cuando aun no se le ha asignado uno
                if (new Guid(item["UidCodigo"].ToString()) == Guid.Empty || new Guid(item["UidCodigo"].ToString()) == null)
                {
                    uidcodigo = Guid.Empty;
                }
                else
                {
                    uidcodigo = new Guid(item["UidCodigo"].ToString());
                }
                // se cambio el Guid de la sucursal a alimentar la lista
                ListaDeBitacoraDeOrdenes.Add(new VMOrden() { Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()), LNGFolio = long.Parse(item["IntFolio"].ToString()), StrNombreSucursal = item["Identificador"].ToString(), UidCodigo = uidcodigo, FechaDeOrden = item["DtmFecha"].ToString() });
            }
        }
        public void ObtenerOrdenesAsignadas(string UidLicencia)
        {
            Datos = new Conexion();
            ListaDeBitacoraDeOrdenes.Clear();

            string Query = " select distinct ot.UidRelacionOrdenTarifario, t.MCosto, s.Identificador,S.UidSucursal, os.MTotalSucursal, os.MTotalSucursal,t.MCosto,os.intfolio,Ot.DtmFecha,dbo.EstatusActualDeOrden(os.UidRelacionOrdenSucursal) as estatus, dbo.asp_ObtenerUltimoEstatusOrdenTarifario(ot.UidRelacionOrdenTarifario) as EstatusTarifario,dbo.asp_ObtenerUltimoEstatusOrdenRepartidor(ot.UidRelacionOrdenTarifario) as EstatusRepartidor  from OrdenTarifario ot " +
                " inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario " +
                " inner join ZonaDeRecoleccion ZDR on ZDR.UidZonaDeRecolecta = t.UidRelacionZonaRecolecta " +
                "  inner join OrdenSucursal os on os.UidRelacionOrdenSucursal = ot.UidOrden " +
                " inner join Sucursales s on s.UidSucursal = os.UidSucursal  inner join ContratoDeServicio CDS on CDS.UidSucursalSuministradora " +
                "= s.UidSucursal and ZDR.UidSucursal = CDS.UidSucursalDistribuidora  where CDS.UidSucursalDistribuidora = (select Uidsucursal from SucursalLicencia where UidLicencia = '" + UidLicencia + "')";
            foreach (DataRow item in Datos.Consultas(Query).Rows)
            {
                //Detecta pedidos confirmados
                if (item["estatus"].ToString().ToUpper() != "DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC" && item["estatus"].ToString().ToUpper() != "A2D33D7C-2E2E-4DC6-97E3-73F382F30D93" && item["estatus"].ToString().ToUpper() != "2FDEE8E7-0D54-4616-B4C1-037F5A37409D")
                {
                    //if (item["EstatusRepartidor"].ToString().ToUpper() == "12748F8A-E746-427D-8836-B54432A38C07" || item["EstatusRepartidor"] == null)
                    //{
                    ListaDeBitacoraDeOrdenes.Add(new VMOrden()
                    {
                        UidSucursal = new Guid(item["UidSucursal"].ToString()),
                        MTotal = decimal.Parse(item["MTotalSucursal"].ToString()),
                        MCostoTarifario = double.Parse(item["MCosto"].ToString()),
                        StrNombreSucursal = item["Identificador"].ToString(),
                        LNGFolio = long.Parse(item["intfolio"].ToString()),
                        FechaDeOrden = item["DtmFecha"].ToString(),
                        Uidorden = new Guid(item["UidRelacionOrdenTarifario"].ToString())
                    });
                    //}

                }
            }
        }
        public void SeleccionaOrden(Guid uidorden)
        {
            var objeto = new VMOrden();
            if (ListaDeOrdenes.Exists(u => u.Seleccion == true))
            {
                objeto = ListaDeOrdenes.Find(u => u.Seleccion == true);
                objeto.Seleccion = false;
                objeto = ListaDeOrdenes.Find(U => U.Uidorden == uidorden);
                objeto.Seleccion = false;
            }
        }
        #endregion

        #region Metodos privados de la clase
        /// <summary>
        /// Verifica si la orden tiene una nota mediante el id del registro dentro de la orden
        /// </summary>
        /// <param name="UidRegistroDeProdutoEnOrden"></param>
        /// <returns></returns>
        private bool VerificaExistenciaDeNotaEnOrden(string UidRegistroDeProdutoEnOrden)
        {
            bool resultado = false;
            try
            {
                oDBOrden = new DBOrden();
                if (oDBOrden.VerificarExistenciaDeNota(UidRegistroDeProdutoEnOrden).Rows.Count == 1)
                {
                    resultado = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        #endregion


        #endregion
    }
}
