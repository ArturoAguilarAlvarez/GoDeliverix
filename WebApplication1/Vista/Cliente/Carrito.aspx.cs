using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;

namespace WebApplication1.Vista.Cliente
{
    public partial class Carrito : System.Web.UI.Page
    {
        VMProducto MVProducto;
        VMOrden MVOrden;
        VMTarifario MVTarifario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MVOrden"] == null)
                {
                    MVOrden = new VMOrden();
                    Session["MVOrden"] = MVOrden;
                }
                else
                {
                    MVOrden = (VMOrden)Session["MVOrden"];
                }
                if (Session["MVTarifario"] == null)
                {
                    MVTarifario = new VMTarifario();
                    Session["MVTarifario"] = MVTarifario;
                }
                else
                {
                    MVTarifario = (VMTarifario)Session["MVTarifario"];
                }
                if (Session["MVProducto"] == null)
                {
                    MVProducto = new VMProducto();
                    Session["MVProducto"] = MVProducto;
                }
                else
                {
                    MVProducto = (VMProducto)Session["MVProducto"];
                    LimpiaSeleccionDeListaDeSucursales();
                    DLCarrito.DataSource = MVProducto.ListaDelInformacionSucursales;
                    DLCarrito.DataBind();
                    MuestraDetallesDeLaOrdenGeneral();
                }
            }
            else
            {
                if (Session["MVProducto"] != null)
                {
                    MVProducto = (VMProducto)Session["MVProducto"];
                    DLCarrito.DataSource = MVProducto.ListaDelInformacionSucursales;
                    DLCarrito.DataBind();
                    MuestraDetallesDeLaOrdenGeneral();
                }
                if (Session["MVOrden"] != null)
                {
                    MVOrden = (VMOrden)Session["MVOrden"];
                }
                if (Session["MVTarifario"] != null)
                {
                    MVTarifario = (VMTarifario)Session["MVTarifario"];
                }
            }
        }
        //Ejecuta el metodo para cobrar, envia las ordenes a las sucursales correspondientes(Suministradora y distribuidora)
        protected void btnPagar_Click(object sender, EventArgs e)
        {
            if (MVProducto.ListaDelCarrito.Count > 0)
            {
                //Controles de la master page
                Label lblUidDireccion = Master.FindControl("lblUidDireccion") as Label;
                Label lblTotalDeOrden = Master.FindControl("lblTotalDeOrden") as Label;
                GridView GVInFormacionDePedido = Master.FindControl("GVInFormacionDePedido") as GridView;
                Label lblProductosEnCarrito = Master.FindControl("lblProductosEnCarrito") as Label;
                //Variables
                Guid UidOrden = Guid.NewGuid();
                decimal total = decimal.Parse(lblTotalDeProductos.Text);
                Guid UidUsuario = new Guid(Session["IdUsuario"].ToString());
                Guid UidDireccion = new Guid(lblUidDireccion.Text);


                if (!MVProducto.ListaDelInformacionSucursales.Exists(t => t.UidTarifario == Guid.Empty))
                {
                    //Guarda la orden con la sucursal
                    for (int i = 0; i < MVProducto.ListaDelCarrito.Count; i++)
                    {
                        VMProducto objeto = MVProducto.ListaDelInformacionSucursales.Find(Suc => Suc.UidSucursal == MVProducto.ListaDelCarrito[i].UidSucursal);
                        var objetos = MVProducto.ListaDelCarrito.FindAll(Suc => Suc.UidSucursal == MVProducto.ListaDelCarrito[i].UidSucursal);
                        decimal totalSucursal = 0.0m;
                        Guid UidOrdenSucursal = Guid.NewGuid();
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
                            MVOrden.GuardaProducto(UidOrdenSucursal, item.UidSeccionPoducto, item.Cantidad, item.StrCosto, item.UidSucursal, item.UidRegistroProductoEnCarrito, Uidnota, mensaje);
                        }
                        //Envia la orden a la sucursal suministradora
                        //Crea el codigo de entrega
                        Random Codigo = new Random();
                        long CodigoDeEnrega = Codigo.Next(00001, 99999);
                        MVOrden.GuardaOrden(UidOrden, total, UidUsuario, UidDireccion, objeto.UidSucursal, totalSucursal, UidOrdenSucursal, CodigoDeEnrega);
                        // Envia la orden a la sucursal distribuidora
                        MVTarifario.AgregarTarifarioOrden(UidOrden: UidOrdenSucursal, UidTarifario: objeto.UidTarifario);
                        //Una vez que se haya guardado ella base de datos se le cambia el estatus a la orden
                        MVOrden.AgregaEstatusALaOrden(new Guid("DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC"), UidOrden: UidOrdenSucursal, StrParametro: "U", UidSucursal: objeto.UidSucursal);
                        MVProducto.ListaDelCarrito.RemoveAll(sucursal => sucursal.UidSucursal == objeto.UidSucursal);
                        i = i - 1;
                    }

                    MVProducto.ListaDelCarrito.Clear();
                    MVProducto.ListaDelInformacionSucursales.Clear();
                    lblCantidadProductos.Text = string.Empty;
                    lblTotalDeProductos.Text = string.Empty;
                    lblProductosEnCarrito.Text = string.Empty;
                    DLCarrito.DataSource = MVProducto.ListaDelCarrito;
                    DLCarrito.DataBind();

                    MVOrden.ObtenerInformacionDeLaUltimaOrden(UidUsuario);
                    GVInFormacionDePedido.DataSource = MVOrden.ListaDeInformacionDeOrden;
                    GVInFormacionDePedido.DataBind();

                    double totalOrden = 0.0d;
                    foreach (VMOrden item in MVOrden.ListaDeInformacionDeOrden)
                    {
                        totalOrden = totalOrden + item.MCostoTarifario + item.MSubtotalSucursal;
                    }
                    lblTotalDeOrden.Text = totalOrden.ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ModalInformacionDeOrden').modal('show');</script>", false);

                    //DLDetallesCompra.DataSource = null;
                    //DLDetallesCompra.DataBind();

                    MuestraDetallesDeLaOrdenGeneral();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se ha elegido una empresa distribuidora dentro de la orden');", true);
                }

            }
        }

        /// <summary>
        /// Controla los datos de detalle general de las compras en el carrito
        /// </summary>
        public void MuestraDetallesDeLaOrdenGeneral()
        {
            decimal Subtotal = 0.0m;
            int cantidad = 0;
            decimal Envio = 0.0m;
            decimal Total = 0.0m;
            foreach (var pro in MVProducto.ListaDelCarrito)
            {
                cantidad = cantidad + pro.Cantidad;
                Subtotal = Subtotal + pro.Subtotal;
            }
            foreach (var envio in MVProducto.ListaDelInformacionSucursales)
            {
                Envio = Envio + envio.CostoEnvio;
            }
            lblCostoDeEnvio.Text = Envio.ToString();
            Total = Subtotal + Envio;
            Label lblProductosEnCarrito = Master.FindControl("lblProductosEnCarrito") as Label;
            if (cantidad == 0)
            {
                lblProductosEnCarrito.Text = string.Empty;
            }
            else
            {
                lblProductosEnCarrito.Text = cantidad.ToString();
            }

            lblCantidadProductos.Text = cantidad.ToString();
            lblTotalDeProductos.Text = Subtotal.ToString();
            lblTotal.Text = Total.ToString();
        }

        protected void DLCarrito_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "PanelDetalles")
            {
                LinkButton Detalles = e.Item.FindControl("btnInformacion") as LinkButton;
                Panel PanelDetalles = e.Item.FindControl("PanelDetalles") as Panel;
                var registro = MVProducto.ListaDelInformacionSucursales.Find(S => S.UidSucursal.ToString() == DLCarrito.DataKeys[e.Item.ItemIndex].ToString());
                LimpiaSeleccionDeListaDeSucursales();

                if (PanelDetalles.Visible && Detalles.CssClass.Contains("glyphicon glyphicon-minus"))
                {
                    registro.IsSelected = false;
                }
                else
                {
                    registro.IsSelected = true;
                }
                DLCarrito.SelectedIndex = e.Item.ItemIndex;
                DLCarrito.DataSource = MVProducto.ListaDelInformacionSucursales;
                DLCarrito.DataBind();
                Guid idSucursal = new Guid(DLCarrito.DataKeys[e.Item.ItemIndex].ToString());
                MVProducto.ListaDeDetallesDeOrden = MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == idSucursal).ToList();
                //DLDetallesCompra.DataSource = MVProducto.ListaDeDetallesDeOrden;
                //DLDetallesCompra.DataBind();
            }
            if (e.CommandName == "SeleccionDistribuidora")
            {
                //Obtiene el boton del listview y lo agrega al scriptManager para poder ejecutar su click
                LinkButton SeleccionDistribuidora = e.Item.FindControl("btnSeleccionarDistribuidora") as LinkButton;
                if (SeleccionDistribuidora != null)
                {
                    ScriptManager.GetCurrent(Page).RegisterAsyncPostBackControl(SeleccionDistribuidora);
                }
                DLCarrito.SelectedIndex = e.Item.ItemIndex;
                Guid idSucursal = new Guid(DLCarrito.DataKeys[e.Item.ItemIndex].ToString());

                DropDownList DDlUbicacion = Master.FindControl("DDlUbicacion") as DropDownList;
                MVTarifario.BuscarTarifario("Cliente", ZonaEntrega: DDlUbicacion.SelectedValue, uidSucursal: idSucursal.ToString());


                ListView LVSeleccionDistribuidora = Master.FindControl("LVSeleccionDistribuidora") as ListView;
                HiddenField HFSucursalSeleccionada = Master.FindControl("HFSucursalSeleccionada") as HiddenField;
                HFSucursalSeleccionada.Value = idSucursal.ToString();
                LVSeleccionDistribuidora.DataSource = MVTarifario.ListaDeTarifarios;
                LVSeleccionDistribuidora.DataBind();
                //Abre la ventana modal de seleccion de distribuidora
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#ModalDistribuidores').modal('show');", true);
            }

        }

        protected void DLDetallesCompra_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DataList dl = ((DataList)source);
            if (e.CommandName == "Agrega")
            {
                //Agrega control al update panel
                LinkButton Agrega = e.Item.FindControl("btnAgrega") as LinkButton;
                if (Agrega != null)
                {
                    ScriptManager.GetCurrent(Page).RegisterAsyncPostBackControl(Agrega);
                }

                Guid uidProduto = new Guid(dl.DataKeys[e.Item.ItemIndex].ToString());
                Guid sucursal = new Guid(), seccion = new Guid();
                //Busqueda de producto en subcategoria
                if (MVProducto.ListaDeSubcategorias.Count > 0)
                {
                    var objeto = MVProducto.ListaDeSubcategorias.Find(producto => producto.UID == uidProduto);
                    sucursal = objeto.UidSucursal;
                    seccion = objeto.UidSeccion;
                }
                else //Busqueda de producto en categoria
                if (MVProducto.ListaDeCategorias.Count > 0)
                {
                    var objeto = MVProducto.ListaDeCategorias.Find(producto => producto.UID == uidProduto);
                    sucursal = objeto.UidSucursal;
                    seccion = objeto.UidSeccion;
                }
                else //Busqueda de producto en giro
                if (MVProducto.ListaDeGiro.Count > 0)
                {
                    var objeto = MVProducto.ListaDeGiro.Find(producto => producto.UID == uidProduto);
                    sucursal = objeto.UidSucursal;
                    seccion = objeto.UidSeccion;
                }

                MVProducto.AgregaAlCarrito(uidProduto, sucursal, seccion, "1", RegistroProductoEnCarrito: uidProduto);

                DLCarrito.DataSource = MVProducto.ListaDelInformacionSucursales;
                DLCarrito.DataBind();
                MuestraDetallesDeLaOrdenGeneral();

                var product = MVProducto.ListaDelCarrito.Find(p => p.UidRegistroProductoEnCarrito == uidProduto);

                int fila = -1;
                foreach (DataListItem item in DLCarrito.Items)
                {
                    if (new Guid(DLCarrito.DataKeys[item.ItemIndex].ToString()) == product.UidSucursal)
                    {
                        fila = item.ItemIndex;
                    }
                }
                DLCarrito.SelectedIndex = fila;
                var listaDetalle = MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == new Guid(DLCarrito.DataKeys[fila].ToString())).ToList();

                dl.DataSource = listaDetalle;
                dl.DataBind();
            }
            if (e.CommandName == "Quita")
            {
                Guid uidProduto = new Guid(dl.DataKeys[e.Item.ItemIndex].ToString());

                //Agrega control al update panel
                LinkButton Quita = e.Item.FindControl("btnQuita") as LinkButton;
                if (Quita != null)
                {
                    ScriptManager.GetCurrent(Page).RegisterAsyncPostBackControl(Quita);
                }

                var producto = MVProducto.ListaDelCarrito.Find(y => y.UidRegistroProductoEnCarrito == uidProduto);
                MVProducto.QuitarDelCarrito(uidProduto);


                DLCarrito.DataSource = MVProducto.ListaDelInformacionSucursales;
                DLCarrito.DataBind();

                if (MVProducto.ListaDelInformacionSucursales.Exists(s => s.UidSucursal == producto.UidSucursal))
                {
                    var listaDetalle = MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == producto.UidSucursal).ToList();
                    dl.DataSource = listaDetalle;
                    dl.DataBind();
                }
                else
                {
                    PanelCarrito.Visible = true;
                    dl.DataSource = null;
                    dl.DataBind();
                }
                MuestraDetallesDeLaOrdenGeneral();
            }
            if (e.CommandName == "EliminaProducto")
            {
                Guid uidProduto = new Guid(dl.DataKeys[e.Item.ItemIndex].ToString());
                var producto = MVProducto.ListaDelCarrito.Find(p => p.UidRegistroProductoEnCarrito == uidProduto);
               
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ModalEliminarProductoCarrito').modal('show');</script>", false);
                HiddenField HFUidProductoAEliminar = Master.FindControl("HFUidProductoAEliminar") as HiddenField;
                HiddenField HFUidSucursalProductoAEliminar = Master.FindControl("HFUidSucursalProductoAEliminar") as HiddenField;
                Label lblCantidadDeProductosAEliminar = Master.FindControl("lblCantidadDeProductosAEliminar") as Label;
                int fila = -1;
                
                foreach (DataListItem item in DLCarrito.Items)
                {
                    if (new Guid(DLCarrito.DataKeys[item.ItemIndex].ToString()) == producto.UidSucursal)
                    {
                        fila = item.ItemIndex;
                    }
                }
                if (producto.Cantidad == 1)
                {
                    lblCantidadDeProductosAEliminar.Text = "Desea eliminar " + producto.Cantidad.ToString() + " producto del carrito";
                }
                else
                {
                    lblCantidadDeProductosAEliminar.Text = "Desea eliminar " + producto.Cantidad.ToString() + " productos del carrito";
                }
                
                lblCantidadDeProductosAEliminar.DataBind();
                HFUidProductoAEliminar.Value = uidProduto.ToString();
                HFUidProductoAEliminar.DataBind();
                HFUidSucursalProductoAEliminar.Value = DLCarrito.DataKeys[fila].ToString();
                HFUidSucursalProductoAEliminar.DataBind();
            }
        }

        protected void DLDetallesCompra_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            TextBox txtNotas = e.Item.FindControl("txtNotas") as TextBox;
            DataList dl = ((DataList)sender);
            if (MVProducto.ListaDelCarrito.Exists(p => p.UidRegistroProductoEnCarrito == new Guid(dl.DataKeys[e.Item.ItemIndex].ToString()) && p.UidNota != Guid.Empty))
            {
                VMProducto registro = MVProducto.ListaDelCarrito.Find(p => p.UidRegistroProductoEnCarrito == new Guid(dl.DataKeys[e.Item.ItemIndex].ToString()));
                txtNotas.Text = registro.StrNota;
                txtNotas.AutoPostBack = true;
                txtNotas.TextChanged += new EventHandler(txtNotas_TextChanged);
            }
            else
            {
                txtNotas.Visible = false;
            }
        }

        protected void DLCarrito_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Panel PanelDetalles = e.Item.FindControl("PanelDetalles") as Panel;
            DataList DLDetallesCompra = PanelDetalles.FindControl("DLDetallesCompra") as DataList;
            LinkButton btnInformacion = e.Item.FindControl("btnInformacion") as LinkButton;
            var registro = MVProducto.ListaDelInformacionSucursales.Find(S => S.UidSucursal.ToString() == DLCarrito.DataKeys[e.Item.ItemIndex].ToString());

            if (registro.IsSelected)
            {
                PanelDetalles.Visible = true;
                btnInformacion.CssClass = "btn btn-sm btn-danger glyphicon glyphicon-minus";
                btnInformacion.ToolTip = "Ocultar detalles";
            }
            else
            {
                PanelDetalles.Visible = false;
                btnInformacion.CssClass = "btn btn-sm btn-success glyphicon glyphicon-plus";
                btnInformacion.ToolTip = "Mostrar detalles";
            }

            Guid idSucursal = new Guid(DLCarrito.DataKeys[e.Item.ItemIndex].ToString());
            MVProducto.ListaDeDetallesDeOrden = MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == idSucursal).ToList();
            DLDetallesCompra.DataSource = MVProducto.ListaDeDetallesDeOrden;
            DLDetallesCompra.DataBind();
        }

        protected void LimpiaSeleccionDeListaDeSucursales()
        {
            foreach (var item in MVProducto.ListaDelInformacionSucursales)
            {
                item.IsSelected = false;
            }

        }

        protected void txtNotas_TextChanged(object sender, EventArgs e)
        {
            TextBox txtnotas = sender as TextBox;

            if (txtnotas != null && txtnotas.Text != string.Empty)
            {
                DataListItem itemDetalles = (DataListItem)txtnotas.NamingContainer;
                
                DataList DLDetalles = (DataList)itemDetalles.NamingContainer;

                DataListItem itemSucursal = (DataListItem)DLDetalles.NamingContainer;

                var registro = MVProducto.ListaDelCarrito.Find(p => p.UidRegistroProductoEnCarrito.ToString() == DLDetalles.DataKeys[itemDetalles.ItemIndex].ToString());
                registro.StrNota = txtnotas.Text;

                Guid idSucursal = new Guid(DLCarrito.DataKeys[itemSucursal.ItemIndex].ToString());
                MVProducto.ListaDeDetallesDeOrden = MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == idSucursal).ToList();
                DLDetalles.DataSource = MVProducto.ListaDeDetallesDeOrden;
                DLDetalles.DataBind();
            }
        }
    }
}