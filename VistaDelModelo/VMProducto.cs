using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace VistaDelModelo
{
    public class VMProducto
    {
        #region Propiedades
        Conexion CN = new Conexion();
        DBProducto DatosProductos = new DBProducto();
        Producto CLASSProducto = new Producto();
        private Guid _Uidproducto;

        public Guid UID
        {
            get { return _Uidproducto; }
            set { _Uidproducto = value; }
        }

        private string _strNombre;

        public string STRNOMBRE
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }

        private string _strDescripcion;

        public string STRDESCRIPCION
        {
            get { return _strDescripcion; }
            set { _strDescripcion = value; }
        }

        private int _estatus;

        public int ESTATUS
        {
            get { return _estatus; }
            set { _estatus = value; }
        }

        private Guid _UidEmpresa;

        public Guid UIDEMPRESA
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }
        private string _strRuta;

        public string STRRUTA
        {
            get { return _strRuta; }
            set { _strRuta = value; }
        }

        private Guid _UidGiro;
        public Guid UIDGIRO
        {
            get { return _UidGiro; }
            set { _UidGiro = value; }
        }
        public Guid UIDCATEGORIA { get; private set; }
        public Guid UIDSUBCATEGORIA { get; private set; }

        private string _TiempoElaboracion;

        public string STRTiemporElaboracion
        {
            get { return _TiempoElaboracion; }
            set { _TiempoElaboracion = value; }
        }
        public DateTime DtmVariableParaTiempo { get; set; }
        private string _mCosto;

        public string StrCosto
        {
            get { return _mCosto; }
            set { _mCosto = value; }
        }

        public bool IsVisible { get; set; }
        public bool IsSelected { get; set; }
        public string Empresa { get; set; }
        public Guid UidSucursal { get; set; }
        public Guid UidSeccion { get; set; }
        public Guid UidSeccionPoducto { get; set; }
        public Guid UidNota { get; set; }
        private string _StrNota;

        public string StrNota
        {
            get { return _StrNota; }
            set { _StrNota = value; }
        }

        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public decimal CostoEnvio { get; set; }
        private decimal _DPropina;
        public decimal DPropina
        {
            get { return _DPropina; }
            set { _DPropina = value; }
        }

        private Guid _UidTarifario;

        public Guid UidTarifario
        {
            get { return _UidTarifario; }
            set { _UidTarifario = value; }
        }

        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private string _StrIdentificador;

        public string StrIdentificador
        {
            get { return _StrIdentificador; }
            set { _StrIdentificador = value; }
        }

        private Guid _UidRegistroProductoEnCarrito;

        public Guid UidRegistroProductoEnCarrito
        {
            get { return _UidRegistroProductoEnCarrito; }
            set { _UidRegistroProductoEnCarrito = value; }
        }
        private List<VMProducto> _ListaDeProductos;

        public List<VMProducto> ListaDeProductos
        {
            get { return _ListaDeProductos; }
            set { _ListaDeProductos = value; }
        }

        public List<VMProducto> ListaDeDetallesDeOrden = new List<VMProducto>();
        public List<VMProducto> ListaDeProductosSeleccionados = new List<VMProducto>();
        public List<VMProducto> ListaDeGiro = new List<VMProducto>();
        public List<VMProducto> ListaDeCategorias = new List<VMProducto>();
        public List<VMProducto> ListaDeSubcategorias = new List<VMProducto>();
        public List<VMProducto> ListaDePreciosSucursales = new List<VMProducto>();



        public List<VMProducto> ListaDelCarrito = new List<VMProducto>();
        public List<VMProducto> ListaDelProductosSeleccionados = new List<VMProducto>();
        public List<VMProducto> ListaDelInformacionSucursales = new List<VMProducto>();

        private string _EstatusProducto;

        public string ColorEstatusProducto
        {
            get { return _EstatusProducto; }
            set { _EstatusProducto = value; }
        }

        #endregion
        #region Metodos
        public bool Guardar(string Nombre, string Descripcion, Guid UidEmpresa, Guid UidProducto, string Estatus)
        {
            return CLASSProducto.Guardar(new Producto() { UID = UidProducto, UIDEMPRESA = UidEmpresa, STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, oEstatus = new Estatus() { ID = Int32.Parse(Estatus) } });
        }
        public bool Actualizar(string Nombre, string Descripcion, Guid UidProducto, string Estatus)
        {
            return CLASSProducto.Actualizar(new Producto() { UID = UidProducto, STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, oEstatus = new Estatus() { ID = Int32.Parse(Estatus) } });
        }
        public void Buscar(string Nombre = "", string Descripcion = "", string estatus = "", Guid UidEmpresa = new Guid(), Guid UidProducto = new Guid(), string Giro = "", string Categoria = "", string Subcategoria = "")
        {
            SqlCommand Comando = new SqlCommand();
            ListaDeProductos.Clear();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BuscarProductos";

                if (UidProducto == Guid.Empty)
                {

                    Comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidEmpresa"].Value = UidEmpresa;

                    if (Nombre != string.Empty && Nombre != "")
                    {
                        Comando.Parameters.Add("@VchNombre", SqlDbType.VarChar, 50);
                        Comando.Parameters["@VchNombre"].Value = Nombre;
                    }
                    if (Descripcion != string.Empty && Descripcion != "")
                    {
                        Comando.Parameters.Add("@VchDescripcion", SqlDbType.VarChar, 300);
                        Comando.Parameters["@VchDescripcion"].Value = Descripcion;
                    }
                    if (estatus != "-1" && estatus != "")
                    {
                        Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                        Comando.Parameters["@IntEstatus"].Value = int.Parse(estatus);
                    }
                    if (!string.IsNullOrEmpty(Giro))
                    {
                        Comando.Parameters.Add("@VchGiro", SqlDbType.VarChar, 10000);
                        Comando.Parameters["@VchGiro"].Value = Giro;
                    }
                    if (!string.IsNullOrEmpty(Categoria))
                    {
                        Comando.Parameters.Add("@VchCategorias", SqlDbType.VarChar, 10000);
                        Comando.Parameters["@VchCategorias"].Value = Categoria;
                    }
                    if (!string.IsNullOrEmpty(Subcategoria))
                    {
                        Comando.Parameters.Add("@VchSubcategoria", SqlDbType.VarChar, 10000);
                        Comando.Parameters["@VchSubcategoria"].Value = Subcategoria;
                    }

                    foreach (DataRow item in CN.Busquedas(Comando).Rows)
                    {
                        Guid uidproducto = new Guid(item["UidProducto"].ToString());
                        string nombre = item["VchNombre"].ToString().ToUpper();
                        string descripcion = item["VchDescripcion"].ToString();
                        string Estatus = item["intEstatus"].ToString();
                        ListaDeProductos.Add(new VMProducto() { UID = uidproducto, STRNOMBRE = nombre, ESTATUS = Int32.Parse(Estatus) });
                    }
                }
                else
                {
                    Comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidEmpresa"].Value = UidEmpresa;

                    Comando.Parameters.Add("@UidProducto", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidProducto"].Value = UidProducto;

                    foreach (DataRow item in CN.Busquedas(Comando).Rows)
                    {
                        UID = new Guid(item["UidProducto"].ToString());
                        STRNOMBRE = item["VchNombre"].ToString();
                        STRDESCRIPCION = item["VchDescripcion"].ToString();
                        ESTATUS = Int32.Parse(item["intEstatus"].ToString());
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        public void ListaConImagen(string UidEmpresa)
        {
            foreach (DataRow item in DatosProductos.ProductoConimagen(UidEmpresa).Rows)
            {
                Guid uidproducto = new Guid(item["UidProducto"].ToString());
                string nombre = item["VchNombre"].ToString().ToUpper();
                string descripcion = item["VchDescripcion"].ToString();
                string ruta = item["NVchRuta"].ToString();
                ListaDeProductos.Add(new VMProducto() { UID = uidproducto, STRNOMBRE = nombre, STRRUTA = ruta });
            }
        }

        public void SeleccionDeProducto(string UidProducto)
        {
            VMProducto Objeto = new VMProducto();
            foreach (DataRow item in DatosProductos.ProductoConImagen(UidProducto).Rows)
            {
                Guid uidproducto = new Guid(item["UidProducto"].ToString());
                string nombre = item["VchNombre"].ToString().ToUpper();
                string ruta = item["NVchRuta"].ToString();

                Objeto = new VMProducto() { UID = uidproducto, STRNOMBRE = nombre, STRRUTA = ruta };


                if (ListaDeProductosSeleccionados == null)
                {
                    if (!ListaDeProductosSeleccionados.Exists(Producto => Producto.UID.ToString() == UidProducto))
                    {
                        ListaDeProductosSeleccionados.Add(Objeto);
                    }

                }
                if (ListaDeProductosSeleccionados != null)
                {
                    if (!ListaDeProductosSeleccionados.Exists(Producto => Producto.UID.ToString() == UidProducto))
                    {
                        ListaDeProductosSeleccionados.Add(Objeto);
                    }
                }
            }
        }

        public void DesSeleccionDeProducto(string v)
        {
            var Producto = ListaDeProductosSeleccionados.Find(Produc => Produc.UID.ToString() == v);
            ListaDeProductosSeleccionados.Remove(Producto);
        }
        /// <summary>
        /// Recibe el uid de la seccion a buscar y llena una lista de productos
        /// </summary>
        /// <param name="UidSeccion"></param>
        public void BuscarProductosSeccion(Guid UidSeccion)
        {
            ListaDeProductos = new List<VMProducto>();
            foreach (DataRow item in DatosProductos.ObtenProductoSeccion(UidSeccion).Rows)
            {
                Guid uidproducto = new Guid(item["UidProducto"].ToString());
                //string rutaimagen = "../" + item["NVchRuta"].ToString();
                string rutaimagen = "http://godeliverix.net/Vista/" + item["NVchRuta"].ToString();
                string Nombre = item["VchNombre"].ToString();
                string Descripcion = item["VchDescripcion"].ToString();
                DateTime Tiempo = DateTime.Parse(item["VchTiempoElaboracion"].ToString());
                string tiempo = Tiempo.Hour.ToString() + "HH" + Tiempo.Minute.ToString() + "MM";
                string costo = ((decimal)item["Mcosto"]).ToString("N2");
                Guid seccionProducto = UidSeccion;
                ListaDeProductos.Add(new VMProducto() { UID = uidproducto, UidSeccionPoducto = seccionProducto, STRRUTA = rutaimagen, STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, StrCosto = costo, STRTiemporElaboracion = tiempo });

            }
        }

        public bool RelacionGiro(string UidGiro, Guid UidSucursal)
        {
            bool resultado = false;
            try
            {
                CLASSProducto = new Producto();
                Guid Giro = new Guid(UidGiro);
                resultado = CLASSProducto.RELACIONGIRO(Giro, UidSucursal);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public bool RelacionCategoria(string UidCategoria, Guid UidSucursal)
        {
            bool resultado = false;
            try
            {
                CLASSProducto = new Producto();
                Guid Categoria = new Guid(UidCategoria);
                resultado = CLASSProducto.RELACIONCATEGORIA(Categoria, UidSucursal);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public bool RelacionSubategoria(string Uidsubategoria, Guid UidSucursal)
        {
            bool resultado = false;
            try
            {
                CLASSProducto = new Producto();
                Guid Subcategoria = new Guid(Uidsubategoria);
                resultado = CLASSProducto.RELACIONSUBCATEGORIA(Subcategoria, UidSucursal);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public void RecuperaGiro(string UidSucursal)
        {
            ListaDeGiro.Clear();
            foreach (DataRow item in DatosProductos.ObtenerGiro(UidSucursal).Rows)
            {
                Guid Giro = new Guid(item["UidGiro"].ToString());
                ListaDeGiro.Add(new VMProducto() { UIDGIRO = Giro });
            }
        }
        /// <summary>
        /// Quita producto del carrito enviando el uid del registro del articulo en el carrito
        /// </summary>
        /// <param name="uidProduto"></param>
        public void QuitarDelCarrito(Guid UidRegistro)
        {
            //var Productos = ListaDelCarrito.Where(x=>x.UID == uidProduto).ToList();
            var producto = ListaDelCarrito.Find(x => x.UidRegistroProductoEnCarrito == UidRegistro);
            if (producto.Cantidad > 1)
            {

                decimal costo = producto.Subtotal / producto.Cantidad;
                producto.Cantidad = producto.Cantidad - 1;
                producto.Subtotal = (producto.Subtotal - costo);


                var sucursal = ListaDelInformacionSucursales.Find(y => y.UidSucursal == producto.UidSucursal);
                sucursal.Cantidad = sucursal.Cantidad - 1;
                decimal preciototal = 0.0m;

                var product = ListaDelCarrito.Where(x => x.UidSucursal == sucursal.UidSucursal).ToList();
                foreach (var item in product)
                {
                    preciototal = preciototal + item.Subtotal;
                }
                sucursal.Subtotal = preciototal;
                sucursal.Total = preciototal + sucursal.CostoEnvio;
                //ListaDelInformacionSucursales.Remove(sucursal);
                //ListaDelInformacionSucursales.Add(sucursal);
            }
            else
            {
                //ListaDelCarrito.Remove(producto);
                var productos = ListaDelCarrito.Where(x => x.UidSucursal == producto.UidSucursal).ToList();
                if (productos.Count > 1)
                {
                    var sucursal = ListaDelInformacionSucursales.Find(y => y.UidSucursal == producto.UidSucursal);
                    sucursal.Cantidad = sucursal.Cantidad - 1;
                    decimal costo = producto.Subtotal / producto.Cantidad;
                    producto.Subtotal = (producto.Subtotal - costo);
                    producto.Cantidad = producto.Cantidad - 1;

                    decimal preciototal = 0.0m;

                    var product = ListaDelCarrito.Where(x => x.UidSucursal == sucursal.UidSucursal).ToList();
                    foreach (var item in product)
                    {
                        preciototal = preciototal + item.Subtotal;
                    }
                    sucursal.Subtotal = preciototal;
                    sucursal.Total = sucursal.Subtotal + sucursal.CostoEnvio;
                    if (producto.Cantidad == 0)
                    {
                        ListaDelCarrito.Remove(producto);
                    }
                    // ListaDelInformacionSucursales.Remove(sucursal);
                    //ListaDelInformacionSucursales.Add(sucursal);
                }
                else
                {
                    var sucursal = ListaDelInformacionSucursales.Find(y => y.UidSucursal == producto.UidSucursal);
                    ListaDelInformacionSucursales.Remove(sucursal);
                    ListaDelCarrito.Remove(producto);
                }
            }
        }

        public void EliminaProductoDelCarrito(Guid uidProduto)
        {
            var objeto = ListaDelCarrito.Find(p => p.UidRegistroProductoEnCarrito == uidProduto);
            var sucursal = ListaDelInformacionSucursales.Find(s => s.UidSucursal == objeto.UidSucursal);

            sucursal.Cantidad = sucursal.Cantidad - objeto.Cantidad;
            sucursal.Subtotal = sucursal.Subtotal - objeto.Subtotal;
            sucursal.Total = sucursal.Subtotal + sucursal.CostoEnvio;


            ListaDelCarrito.Remove(objeto);
            var productos = ListaDelCarrito.FindAll(p => p.UidSucursal == sucursal.UidSucursal);

            if (productos.Count == 0)
            {
                ListaDelInformacionSucursales.Remove(sucursal);
            }

        }

        public void AgregaAlCarrito(Guid uidProducto, Guid UidSucursal, Guid UidSeccion, string cantidad, decimal CostoDeEnvio = 0.0m, Guid UidTarifario = new Guid(), string strNota = "", Guid RegistroProductoEnCarrito = new Guid(), string URLEmpresa = "", string dPropina = "")
        {
            //Agrega un registro sin nota
            //Agrega un registro con una nota
            string nota = "";
            Guid uidNota = Guid.Empty;
            decimal dpropina = new decimal();
            if (!string.IsNullOrEmpty(dPropina))
            {
                dpropina = decimal.Parse(dPropina, System.Globalization.NumberStyles.Float);
            }
            else
            {
                dpropina = 0.0m;
            }
            if (RegistroProductoEnCarrito == Guid.Empty)
            {

                if (!string.IsNullOrEmpty(strNota))
                {
                    uidNota = Guid.NewGuid();
                    nota = strNota;
                }

                foreach (DataRow item in DatosProductos.ProductoCarrito(uidProducto, UidSucursal.ToString(), UidSeccion.ToString()).Rows)
                {
                    Guid uidproducto = new Guid(item["UidProducto"].ToString());
                    Guid uidsucursal = new Guid(item["UidSucursal"].ToString());
                    Guid UidSeccionpoducto = new Guid(item["UidSeccionProducto"].ToString());
                    string nombre = item["VchNombre"].ToString();
                    string costo = item["Mcosto"].ToString();
                    string sucursal = item["Identificador"].ToString();
                    //string imagen = "../" + item["NVchRuta"].ToString();
                    string imagen = "http://www.godeliverix.net/vista/" + item["NVchRuta"].ToString();
                    decimal SubTotal = decimal.Parse(costo) * int.Parse(cantidad);
                    decimal Total = SubTotal + CostoDeEnvio;
                    ListaDelCarrito.Add(new VMProducto()
                    {
                        UidRegistroProductoEnCarrito = Guid.NewGuid(),
                        UidSeccionPoducto = UidSeccionpoducto,
                        Total = Total,
                        Subtotal = SubTotal,
                        CostoEnvio = CostoDeEnvio,
                        UID = uidproducto,
                        UidSucursal = uidsucursal,
                        STRNOMBRE = nombre,
                        StrCosto = SubTotal.ToString(),
                        Empresa = sucursal,
                        STRRUTA = imagen,
                        Cantidad = int.Parse(cantidad),
                        UidNota = uidNota,
                        StrNota = nota,
                        ColorEstatusProducto = ""
                    });

                    if (!ListaDelInformacionSucursales.Exists(suc => suc.UidSucursal == uidsucursal))
                    {
                        ListaDelInformacionSucursales.Add(new VMProducto()
                        {
                            STRRUTA = URLEmpresa,
                            UidTarifario = UidTarifario,
                            UidSucursal = uidsucursal,
                            Empresa = sucursal,
                            Total = Total,
                            CostoEnvio = CostoDeEnvio,
                            Subtotal = SubTotal,
                            Cantidad = int.Parse(cantidad),
                            DPropina = dpropina
                        });
                    }
                    else
                    {
                        if (ListaDelInformacionSucursales.Exists(suc => suc.UidSucursal == UidSucursal))
                        {
                            var Sucursal = ListaDelInformacionSucursales.Find(suc => suc.UidSucursal == UidSucursal);
                            Sucursal.Subtotal = (Sucursal.Subtotal + SubTotal);
                            Sucursal.Cantidad = Sucursal.Cantidad + int.Parse(cantidad);
                            Sucursal.Total = (Sucursal.Subtotal + Sucursal.CostoEnvio);
                            Sucursal.DPropina = dpropina;
                        }
                    }

                }

            }
            else
            //Si existe el producto en la lista y no tiene un mensaje
            if (ListaDelCarrito.Exists(Objeto => Objeto.UidRegistroProductoEnCarrito == RegistroProductoEnCarrito))
            {
                var Producto = new VMProducto();
                Producto = ListaDelCarrito.Find(Objeto => Objeto.UidRegistroProductoEnCarrito == RegistroProductoEnCarrito);


                decimal precio = Producto.Subtotal / Producto.Cantidad;
                Producto.Cantidad = Producto.Cantidad + int.Parse(cantidad);

                Producto.Subtotal = (precio * Producto.Cantidad);
                Producto.Total = Producto.Subtotal + Producto.CostoEnvio;

                if (ListaDelInformacionSucursales.Exists(suc => suc.UidSucursal == Producto.UidSucursal))
                {
                    var sucursal = ListaDelInformacionSucursales.Find(suc => suc.UidSucursal == Producto.UidSucursal);
                    decimal preciototal = 0.0m;

                    var productos = ListaDelCarrito.Where(x => x.UidSucursal == sucursal.UidSucursal).ToList();
                    foreach (var item in productos)
                    {
                        preciototal = preciototal + item.Subtotal;
                    }
                    sucursal.Subtotal = preciototal;
                    sucursal.Cantidad = sucursal.Cantidad + int.Parse(cantidad);
                    if (CostoDeEnvio != 0)
                    {
                        sucursal.CostoEnvio = CostoDeEnvio;
                    }
                    sucursal.Total = sucursal.Subtotal + sucursal.CostoEnvio;
                    if (UidTarifario != Guid.Empty)
                    {
                        sucursal.UidTarifario = UidTarifario;
                    }
                }
            }
        }
        public void AgregaTarifarioOrden(Guid UidSucursal, Guid Uidtarifario, decimal dCostoDeEnvio)
        {
            var registro = ListaDelInformacionSucursales.Find(s => s.UidSucursal == UidSucursal);
            if (registro.UidTarifario == Guid.Empty)
            {
                registro.UidTarifario = Uidtarifario;
                registro.CostoEnvio = dCostoDeEnvio;
                registro.Total = registro.Subtotal + registro.CostoEnvio;
            }
            else
            {
                //Resta los valores si el tarifario ya fue seleccionado y se quiere cambiar
                registro.Total = registro.Subtotal - registro.CostoEnvio;
                registro.UidTarifario = Uidtarifario;
                registro.CostoEnvio = dCostoDeEnvio;
                registro.Total = registro.Subtotal + registro.CostoEnvio;
            }

        }
        public void SeleccionaProducto(Guid uidProducto, Guid UidSucursal, Guid UidSeccion)
        {
            if (!ListaDelProductosSeleccionados.Exists(p => p.UID == uidProducto))
            {
                foreach (DataRow item in DatosProductos.ProductoCarrito(uidProducto, UidSucursal.ToString(), UidSeccion.ToString()).Rows)
                {
                    Guid uidproducto = new Guid(item["UidProducto"].ToString());
                    Guid uidsucursal = new Guid(item["UidSucursal"].ToString());
                    Guid uiseccion = new Guid(item["UidSeccion"].ToString());
                    string nombre = item["VchNombre"].ToString();
                    string tiempo = item["VchTiempoElaboracion"].ToString();
                    string costo = item["Mcosto"].ToString();
                    string sucursal = item["Identificador"].ToString();
                    string imagen = "../" + item["NVchRuta"].ToString();
                    ListaDelProductosSeleccionados.Add(new VMProducto() { STRTiemporElaboracion = tiempo, UidSeccion = uiseccion, UID = uidproducto, UidSucursal = uidsucursal, StrCosto = costo, STRNOMBRE = nombre, Empresa = sucursal, STRRUTA = imagen });
                }
            }
        }



        public void AgregaProductoSeleccionadoALista()
        {
            foreach (var Producto in ListaDelProductosSeleccionados)
            {
                foreach (DataRow item in DatosProductos.ProductoCarrito(Producto.UID, Producto.UidSucursal.ToString(), Producto.UidSeccion.ToString()).Rows)
                {
                    Guid uidproducto = new Guid(item["UidProducto"].ToString());
                    Guid uidsucursal = new Guid(item["UidSucursal"].ToString());
                    Guid uiseccion = new Guid(item["UidSeccion"].ToString());
                    string nombre = item["VchNombre"].ToString();
                    DateTime Tiempo = DateTime.Parse(item["VchTiempoElaboracion"].ToString());
                    string tiempo = Tiempo.Hour.ToString() + "HH" + Tiempo.Minute.ToString() + "MM";
                    string costo = item["Mcosto"].ToString();
                    string sucursal = item["Identificador"].ToString();
                    string imagen = "../" + item["NVchRuta"].ToString();
                    var registro = new VMProducto() { STRTiemporElaboracion = tiempo, UidSeccion = uiseccion, UID = uidproducto, UidSucursal = uidsucursal, StrCosto = costo, STRNOMBRE = nombre, Empresa = sucursal, STRRUTA = imagen };

                    if (ListaDeGiro.Count > 0)
                    {
                        if (!ListaDeGiro.Exists(p => p.UID == uidproducto))
                        {
                            ListaDeGiro.Add(registro);
                        }
                    }
                    if (ListaDeCategorias.Count > 0)
                    {
                        if (!ListaDeCategorias.Exists(p => p.UID == uidproducto))
                        {
                            ListaDeCategorias.Add(registro);
                        }
                    }
                    if (ListaDeSubcategorias.Count > 0)
                    {
                        if (!ListaDeSubcategorias.Exists(p => p.UID == uidproducto))
                        {
                            ListaDeSubcategorias.Add(registro);
                        }
                    }
                }
            }
        }
        public void DesSeleccionaProducto(Guid uidProducto)
        {
            if (ListaDelProductosSeleccionados.Exists(pro => pro.UID == uidProducto))
            {
                var objeto = ListaDelProductosSeleccionados.Find(pro => pro.UID == uidProducto);
                ListaDelProductosSeleccionados.Remove(objeto);
            }
        }
        public void EliminaSubcategoria(string UidProducto)
        {
            DatosProductos.EliminaSubcategoria(UidProducto);
        }

        public void EliminaCategoria(string UidProducto)
        {
            DatosProductos.EliminaCategoria(UidProducto);
        }

        public void EliminaGiro(string UidProducto)
        {
            DatosProductos.EliminaGiro(UidProducto);
        }

        public void RecuperaCategoria(string UidSucursal)
        {
            ListaDeCategorias.Clear();
            foreach (DataRow item in DatosProductos.ObtenerCategoria(UidSucursal).Rows)
            {
                Guid Categoria = new Guid(item["UidCategoria"].ToString());
                ListaDeCategorias.Add(new VMProducto() { UIDCATEGORIA = Categoria });
            }
        }
        public void RecuperaSubcategoria(string UidSucursal)
        {
            ListaDeSubcategorias.Clear();
            foreach (DataRow item in DatosProductos.ObtenerSubcategoria(UidSucursal).Rows)
            {
                Guid Subcategoria = new Guid(item["UidSubcategoria"].ToString());
                ListaDeSubcategorias.Add(new VMProducto() { UIDSUBCATEGORIA = Subcategoria });
            }
        }

        public void ObtenerProductos(Guid valor)
        {
            ListaDeProductos.Clear();
            foreach (DataRow item in DatosProductos.ProductosDeSucursal(valor).Rows)
            {
                Guid uidproducto = new Guid(item["UidProducto"].ToString());
                string nombre = item["VchNombre"].ToString();
                string rutaimagen = item["NVchRuta"].ToString();
                ListaDeProductos.Add(new VMProducto() { UID = uidproducto, STRNOMBRE = nombre, STRRUTA = rutaimagen });
            }
        }

        public void BuscarProductos(Guid UIDSUCURSAL, string Nombre = "")
        {
            DataTable Dt = new DataTable();
            ListaDeProductos = new List<VMProducto>();
            try
            {
                SqlCommand CMD = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "asp_BuscarProductosSucursal"
                };

                CMD.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@UidSucursal"].Value = UIDSUCURSAL;


                if (Nombre != string.Empty)
                {
                    CMD.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
                    CMD.Parameters["@VchNombre"].Value = Nombre;
                }
                foreach (DataRow item in CN.Busquedas(CMD).Rows)
                {
                    Guid id = new Guid(item["UidProducto"].ToString());
                    string nombre = item["VchNombre"].ToString().ToUpper();
                    string Ruta = item["NVchRuta"].ToString().ToUpper();

                    ListaDeProductos.Add(new VMProducto() { UID = id, STRNOMBRE = nombre, STRRUTA = Ruta });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public void InformacionProducto(string UidProducto, string UidSeccion)
        {
            foreach (DataRow item in DatosProductos.Informacion(UidProducto, UidSeccion).Rows)
            {
                UID = new Guid(item["UidProducto"].ToString());
                STRTiemporElaboracion = item["VchTiempoElaboracion"].ToString();
                string numero = item["Mcosto"].ToString();
                if (string.IsNullOrEmpty(numero))
                {
                    numero = "0.000";
                }
                StrCosto = Math.Round(Convert.ToDouble(numero), 2).ToString();
            }
        }

        public void ActualizarProducto(string UIDPRODUCTO, string TIEMPODEELEABORACION, string COSTO, string UIDSECCION)
        {
            CLASSProducto = new Producto
            {
                UID = new Guid(UIDPRODUCTO),
                STRTiemporElaboracion = TIEMPODEELEABORACION,
                StrCosto = COSTO.ToString()
            };
            CLASSProducto.ActualizaInformacionProductoSucursal(UIDSECCION);
        }

        #region Busqueda de productos desde cliente
        public void buscarProductosEmpresaDesdeCliente(string StrParametroBusqueda, string StrDia, Guid UidEstado, Guid UidColonia, Guid UidBusquedaCategorias, string StrNombreEmpresa = "")
        {
            try
            {
                SqlCommand CMD = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "asp_BuscarProductosClientes"
                };

                CMD.Parameters.Add("@strParametroBusqueda", SqlDbType.VarChar, 100);
                CMD.Parameters["@strParametroBusqueda"].Value = StrParametroBusqueda;

                if (!string.IsNullOrEmpty(StrNombreEmpresa))
                {
                    CMD.Parameters.Add("@StrNombreProducto", SqlDbType.VarChar, 200);
                    CMD.Parameters["@StrNombreProducto"].Value = StrNombreEmpresa;
                }

                CMD.Parameters.Add("@StrDia", SqlDbType.VarChar, 20);
                CMD.Parameters["@StrDia"].Value = StrDia;

                CMD.Parameters.Add("@Estado", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@Estado"].Value = UidEstado;

                CMD.Parameters.Add("@UidColonia", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@UidColonia"].Value = UidColonia;

                CMD.Parameters.Add("@UidBusquedaCategorias", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@UidBusquedaCategorias"].Value = UidBusquedaCategorias;

                ListaDeProductos = new List<VMProducto>();
                foreach (DataRow item in CN.Busquedas(CMD).Rows)
                {
                    Guid uidproducto = new Guid(item["UidProducto"].ToString());
                    string nombre = item["VchNombre"].ToString().ToUpper();
                    string descripcion = item["VchDescripcion"].ToString();
                    string ruta = "https://www.godeliverix.net/vista/" + item["NVchRuta"].ToString();
                    //string ruta = "../" + item["NVchRuta"].ToString();
                    Guid UidEmpresa = new Guid(item["UidEmpresa"].ToString());

                    if (!ListaDeProductos.Exists(p => p.UID == uidproducto))
                    {
                        ListaDeProductos.Add(new VMProducto()
                        {
                            UID = uidproducto,
                            Empresa = item["NombreComercial"].ToString(),
                            UIDEMPRESA = UidEmpresa,
                            STRDESCRIPCION = descripcion,
                            STRNOMBRE = nombre,
                            STRRUTA = ruta
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BuscarProductoPorSucursal(string StrParametroBusqueda, string StrDia, Guid UidColonia, Guid UidEstado, Guid UidBusquedaCategorias, Guid UidProducto)
        {
            try
            {
                SqlCommand CMD = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "asp_BuscarSucursalesCliente"
                };

                CMD.Parameters.Add("@strParametroBusqueda", SqlDbType.VarChar, 100);
                CMD.Parameters["@strParametroBusqueda"].Value = StrParametroBusqueda;


                CMD.Parameters.Add("@StrDia", SqlDbType.VarChar, 20);
                CMD.Parameters["@StrDia"].Value = StrDia;

                CMD.Parameters.Add("@UidColonia", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@UidColonia"].Value = UidColonia;


                CMD.Parameters.Add("@UidEstado", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@UidEstado"].Value = UidEstado;

                CMD.Parameters.Add("@UidBusquedaCategorias", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@UidBusquedaCategorias"].Value = UidBusquedaCategorias;

                CMD.Parameters.Add("@UidProduto", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@UidProduto"].Value = UidProducto;

                ListaDePreciosSucursales.Clear();

                foreach (DataRow item in CN.Busquedas(CMD).Rows)
                {
                    Guid uidseccion = new Guid(item["UidSeccion"].ToString());
                    string stridentificador = item["Identificador"].ToString().ToUpper();
                    string strTiempoDeElaboracion = item["VchTiempoElaboracion"].ToString();
                    string dbCosto = item["Mcosto"].ToString();
                    Guid UidSucursal = new Guid(item["UidSucursal"].ToString());
                    Guid uidempresa = new Guid(item["UidEmpresa"].ToString());
                    if (!ListaDePreciosSucursales.Exists(p => p.UID == uidseccion))
                    {
                        ListaDePreciosSucursales.Add(new VMProducto()
                        {
                            UID = uidseccion,
                            StrCosto = dbCosto,
                            DtmVariableParaTiempo = DateTime.Parse(strTiempoDeElaboracion),
                            StrIdentificador = stridentificador,
                            UidSucursal = UidSucursal,
                            UIDEMPRESA = uidempresa
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion
        #endregion
    }
}
