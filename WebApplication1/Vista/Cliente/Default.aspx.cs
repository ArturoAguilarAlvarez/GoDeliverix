using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;

namespace WebApplication1.Vista.Cliente
{
    public partial class Default : System.Web.UI.Page
    {
        #region Propiedades
        VMGiro MVGiro = new VMGiro();
        VMCategoria MVCategoria = new VMCategoria();
        VMSubCategoria MVSubcategoria = new VMSubCategoria();
        VMDireccion MVDireccion;
        VMSucursales MVSucursales = new VMSucursales();
        VMProducto MVProducto;
        VMSeccion MVSeccion = new VMSeccion();
        VMOferta MVOferta = new VMOferta();
        VMTarifario MVTarifario = new VMTarifario();
        VMImagen MVImagen = new VMImagen();
        VMEmpresas MVEmpresa;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {

                if (!IsPostBack)
                {
                    if (Session["MVProducto"] == null)
                    {
                        MVProducto = new VMProducto();
                        Session["MVProducto"] = MVProducto;
                    }
                    else
                    {
                        MVProducto = (VMProducto)Session["MVProducto"];
                    }
                    if (Session["MVDireccion"] == null)
                    {
                        MVDireccion = new VMDireccion();
                        Session["MVDireccion"] = MVDireccion;
                    }
                    else
                    {
                        MVDireccion = (VMDireccion)Session["MVDireccion"];
                    }

                    Session["MVGiro"] = MVGiro;
                    Session["MVImagen"] = MVImagen;
                    Session["MVCategoria"] = MVCategoria;
                    Session["MVSubcategoria"] = MVSubcategoria;


                    Session["MVSeccion"] = MVSeccion;
                    Session["MVOferta"] = MVOferta;
                    Session["MVSucursales"] = MVSucursales;
                    Session["MVTarifario"] = MVTarifario;

                    MVGiro.ListaDeGiroConimagen();
                    DDlGiro.DataSource = MVGiro.LISTADEGIRO;
                    DDlGiro.DataValueField = "UIDVM";
                    DDlGiro.DataTextField = "STRNOMBRE";
                    DDlGiro.DataBind();

                    string uidgiro = DDlGiro.SelectedItem.Value;
                    PanelCategorias.Visible = false;
                    MVCategoria.BuscarCategorias(UidGiro: uidgiro, tipo: "seleccion");
                    DDlCategoria.DataSource = MVCategoria.LISTADECATEGORIAS;
                    DDlCategoria.DataValueField = "UIDCATEGORIA";
                    DDlCategoria.DataTextField = "STRNOMBRE";
                    DDlCategoria.DataBind();

                    MVSubcategoria.BuscarSubCategoria(UidCategoria: Guid.Empty.ToString(), Tipo: "Seleccionar");
                    DDlSubcategoria.DataSource = MVSubcategoria.LISTADESUBCATEGORIAS;
                    DDlSubcategoria.DataValueField = "UID";
                    DDlSubcategoria.DataTextField = "STRNOMBRE";
                    DDlSubcategoria.DataBind();

                    MuestraPanel("Productos");

                    lblCantidadDeResultados.Text = string.Empty;
                    txtNombreDeBusqueda.Attributes.Add("placeholder", "Buscar todo");
                    if (Session["MVEMpresa"] == null)
                    {
                        MVEmpresa = new VMEmpresas();
                        Session["MVEMpresa"] = MVEmpresa;
                    }
                    else
                    {
                        MVEmpresa = (VMEmpresas)Session["MVEMpresa"];
                    }

                    if (Session["MVDireccion"] == null)
                    {
                        MVDireccion = new VMDireccion();
                        Session["MVDireccion"] = MVDireccion;
                    }
                    else
                    {
                        MVDireccion = (VMDireccion)Session["MVDireccion"];
                    }

                    if (MVEmpresa.LISTADEEMPRESAS != null)
                    {
                        if (MVEmpresa.LISTADEEMPRESAS.Count > 0)
                        {
                            DLEmpresa.DataSource = MVEmpresa.LISTADEEMPRESAS;
                            DLEmpresa.DataBind();
                        }
                    }
                    PanelBusqueda.Visible = true;
                    PanelDeDetallesEmpresa.Visible = false;

                }
                else
                {
                    //Primero se cargan los datos para crear la tarjeta de las empresas
                    if (Session["MVDireccion"] == null)
                    {
                        MVDireccion = new VMDireccion();
                        Session["MVDireccion"] = MVDireccion;
                    }
                    else
                    {
                        MVDireccion = (VMDireccion)Session["MVDireccion"];
                    }

                    if (Session["MVEMpresa"] == null)
                    {
                        MVEmpresa = new VMEmpresas();
                        Session["MVEMpresa"] = MVEmpresa;
                    }
                    else
                    {
                        MVEmpresa = (VMEmpresas)Session["MVEMpresa"];

                        if (MVEmpresa.LISTADEEMPRESAS.Count > 0)
                        {
                            DLEmpresa.DataSource = MVEmpresa.LISTADEEMPRESAS;
                            DLEmpresa.DataBind();
                        }
                    }
                    if (Session["MVProducto"] == null)
                    {
                        MVProducto = new VMProducto();
                        Session["MVProducto"] = MVProducto;
                    }
                    else
                    {
                        MVProducto = (VMProducto)Session["MVProducto"];
                    }

                    //if (PanelEmpresas.Visible)
                    //{
                    //    MuestraPanel("Empresa");
                    //}
                    //if (PanelProductos.Visible)
                    //{
                    //    MuestraPanel("Productos");
                    //}
                    MVGiro = (VMGiro)Session["MVGiro"];
                    MVImagen = (VMImagen)Session["MVImagen"];
                    MVCategoria = (VMCategoria)Session["MVCategoria"];
                    MVSubcategoria = (VMSubCategoria)Session["MVSubcategoria"];
                    MVProducto = (VMProducto)Session["MVProducto"];
                    MVSeccion = (VMSeccion)Session["MVSeccion"];
                    MVOferta = (VMOferta)Session["MVOferta"];
                    MVSucursales = (VMSucursales)Session["MVSucursales"];
                    MVTarifario = (VMTarifario)Session["MVTarifario"];
                    MVEmpresa = (VMEmpresas)Session["MVEMpresa"];
                }
            }
            else
            {
                Response.Redirect("../Default/");
            }
        }

        /// <summary>
        /// Genera las cantidades de valores a la descripcion total de la orden
        /// </summary>


        protected void ChkbxSeleccionarProducto_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    foreach (ListViewItem item in LVProductos.Items)
            //    {
            //        CheckBox productoSeleccionado = (CheckBox)item.FindControl("ChkbxSeleccionarProducto");
            //        if (productoSeleccionado.Checked)
            //        {
            //            Guid UidProducto = new Guid(LVProductos.DataKeys[item.DataItemIndex].Value.ToString());
            //            Guid sucursal = new Guid(), seccion = new Guid();
            //            //Busqueda de producto en subcategoria
            //            if (MVProducto.ListaDeSubcategorias.Count > 0)
            //            {
            //                var objeto = MVProducto.ListaDeSubcategorias.Find(producto => producto.UID == UidProducto);
            //                sucursal = objeto.UidSucursal;
            //                seccion = objeto.UidSeccion;
            //            }
            //            else //Busqueda de producto en categoria
            //            if (MVProducto.ListaDeCategorias.Count > 0)
            //            {
            //                var objeto = MVProducto.ListaDeCategorias.Find(producto => producto.UID == UidProducto);
            //                sucursal = objeto.UidSucursal;
            //                seccion = objeto.UidSeccion;
            //            }
            //            else //Busqueda de producto en giro
            //            if (MVProducto.ListaDeGiro.Count > 0)
            //            {
            //                var objeto = MVProducto.ListaDeGiro.Find(producto => producto.UID == UidProducto);
            //                sucursal = objeto.UidSucursal;
            //                seccion = objeto.UidSeccion;
            //            }
            //            MVProducto.SeleccionaProducto(UidProducto, sucursal, seccion);
            //        }
            //        else
            //        {
            //            MVProducto.DesSeleccionaProducto(new Guid(LVProductos.DataKeys[item.DataItemIndex].Value.ToString()));
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

        }

        protected void LVProductoSeleccionado_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label ProductoEnCarrito = (Label)e.Item.FindControl("LblProductoEnCarrito");
                VMProducto registro = e.Item.DataItem as VMProducto;

                bool Producto = false;
                if (MVProducto.ListaDelCarrito != null && MVProducto.ListaDelCarrito.Count > 0)
                {
                    Producto = MVProducto.ListaDelCarrito.Exists(p => p.UID == registro.UID);
                }
                if (Producto)
                {
                    ProductoEnCarrito.Visible = true;
                }
                else
                {
                    ProductoEnCarrito.Visible = false;
                }
            }
        }

        /// <summary>
        /// Este metodo sirve para el click del boton de los productos dentro del data list de las empresas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListProductos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "VentanaModalCarrito")
            {
                HiddenField uid = e.Item.FindControl("HFUidProducto") as HiddenField;
                DropDownList DDlGiro = Master.FindControl("DDlGiro") as DropDownList;
                DropDownList DDlUbicacion = Master.FindControl("DDlUbicacion") as DropDownList;
                DropDownList DDlCategoria = Master.FindControl("DDlCategoria") as DropDownList;
                DropDownList DDlSubcategoria = Master.FindControl("DDlSubcategoria") as DropDownList;
                TextBox txtNombreDeBusqueda = Master.FindControl("txtNombreDeBusqueda") as TextBox;
                Label lblTituloProducto = Master.FindControl("lblTituloProducto") as Label;
                Label lblMensaje = Master.FindControl("lblMensaje") as Label;
                ListView LvSucursales = Master.FindControl("LvSucursales") as ListView;
                Label lblCantidadProducto = Master.FindControl("lblCantidadProducto") as Label;
                Label LblDescripcionCarrito = Master.FindControl("LblDescripcionCarrito") as Label;
                Image ImgProductoCarrito = Master.FindControl("ImgProductoCarrito") as Image;
                var lista = sender as ListView;
                Guid UidProducto = new Guid(uid.Value.ToString());
                var registro = MVProducto.ListaDeProductos.Find(r => r.UID == UidProducto);

                lblTituloProducto.Text = registro.STRNOMBRE;
                ImgProductoCarrito.ImageUrl = registro.STRRUTA;

                LblDescripcionCarrito.Text = registro.STRDESCRIPCION;
                lblMensaje.Text = string.Empty;
                lblCantidadProducto.Text = "1";
                LvSucursales.SelectedIndex = -1;
                #region Busqueda de sucursales con el  mismo producto
                string giro = DDlGiro.SelectedItem.Value;
                string categoria = string.Empty;
                string subcategoria = string.Empty;
                Guid Colonia = new Guid(DDlUbicacion.SelectedItem.Value);

                
                CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
                string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                string NombreABuscar = txtNombreDeBusqueda.Text;
                if (DDlCategoria.SelectedItem != null && new Guid(DDlCategoria.SelectedItem.Value) != Guid.Empty)
                {
                    categoria = DDlCategoria.SelectedItem.Value;
                }
                if (DDlSubcategoria.SelectedItem != null && new Guid(DDlSubcategoria.SelectedItem.Value) != Guid.Empty)
                {
                    subcategoria = DDlSubcategoria.SelectedItem.Value;
                }

                if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                {
                    MVProducto.BuscarProductoPorSucursal("Giro",  Dia, Colonia, new Guid(giro), UidProducto);
                }
                if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                {
                    MVProducto.BuscarProductoPorSucursal("Categoria",  Dia, Colonia, new Guid(categoria), UidProducto);
                }
                if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
                {
                    MVProducto.BuscarProductoPorSucursal("Subcategoria",  Dia, Colonia, new Guid(subcategoria), UidProducto);
                }
                LvSucursales.DataSource = MVProducto.ListaDePreciosSucursales;
                LvSucursales.DataBind();

                #endregion
                //Abre la ventana modal
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ModalDistribuidores').modal('show');</script>", false);
            }
        }

        #region Paneles
        /// <summary>
        /// Muestra un panel en la vista
        /// </summary>
        /// <param name="Panel">Empresa|Productos|</param>
        protected void MuestraPanel(string Panel)
        {
            blBuqueda.Items.Clear();
            if (Panel == "Empresa")
            {
                PanelEmpresas.Visible = true;
                liPanelEmpresa.Attributes.Add("class", "active");
                DLEmpresa.DataSource = null;
                DLEmpresa.DataBind();
            }
            else
            {
                PanelEmpresas.Visible = false;
                liPanelEmpresa.Attributes.Add("class", "");
            }
            if (Panel == "Productos")
            {
                PanelProductos.Visible = true;
                liPanelProductos.Attributes.Add("class", "active");
                DLProductos.DataSource = null;
                DLProductos.DataBind();
            }
            else
            {
                PanelProductos.Visible = false;
                liPanelProductos.Attributes.Add("class", "");
            }
        }

        private void CargaGrid(string NombreControl)
        {
            switch (NombreControl)
            {
                case "Productos":
                    BuscarProductos();
                    DLProductos.DataSource = MVProducto.ListaDeProductos;
                    DLProductos.DataBind();
                    break;
                case "Empresas":
                    //DropDownList DDlGiro = Master.FindControl("DDlGiro") as DropDownList;
                    if (DDlGiro.SelectedItem != null)
                    {
                        BuscarEmpresas();
                    }

                    DLEmpresa.DataSource = MVEmpresa.LISTADEEMPRESAS;
                    DLEmpresa.DataBind();
                    break;
                default:
                    break;
            }
        }

        protected void btnPanelEmpresa_Click(object sender, EventArgs e)
        {
            lblCantidadDeResultados.Text = string.Empty;
            MuestraPanel("Empresa");
        }
        protected void btnPanelProductos_Click(object sender, EventArgs e)
        {
            lblCantidadDeResultados.Text = string.Empty;
            MuestraPanel("Productos");
        }


        protected void BuscarEmpresas()
        {
            lblCantidadDeResultados.Text = string.Empty;
            //DropDownList DDlGiro = Master.FindControl("DDlGiro") as DropDownList;
            DropDownList DDlUbicacion = Master.FindControl("DDlUbicacion") as DropDownList;
            //DropDownList DDlCategoria = Master.FindControl("DDlCategoria") as DropDownList;
            //DropDownList DDlSubcategoria = Master.FindControl("DDlSubcategoria") as DropDownList;
            //TextBox txtNombreDeBusqueda = Master.FindControl("txtNombreDeBusqueda") as TextBox;
            string giro = Guid.Empty.ToString();
            if (DDlGiro.SelectedItem != null)
            {
                giro = DDlGiro.SelectedItem.Value;
            }

            string categoria = string.Empty;
            string subcategoria = string.Empty;
            Guid UidDireccion =new Guid(DDlUbicacion.SelectedItem.Value);
            string Hora = string.Empty;
            if (DateTime.Now.Hour < 10)
            {
                Hora = "0" + DateTime.Now.Hour.ToString();
            }
            else
            {
                Hora = DateTime.Now.Hour.ToString();
            }

            string hora = Hora + ":" + DateTime.Now.Minute.ToString();
            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            string NombreABuscar = txtNombreDeBusqueda.Text;
            if (DDlCategoria.SelectedItem != null && new Guid(DDlCategoria.SelectedItem.Value) != Guid.Empty)
            {
                categoria = DDlCategoria.SelectedItem.Value;
            }
            if (DDlSubcategoria.SelectedItem != null && new Guid(DDlSubcategoria.SelectedItem.Value) != Guid.Empty)
            {
                subcategoria = DDlSubcategoria.SelectedItem.Value;
            }

            //Busqueda de producto

            //Busqueda por giro
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                //giro
                //MVProducto.AgregaProductoSeleccionadoALista();
                MVEmpresa.BuscarEmpresaBusquedaCliente("Giro",  Dia, UidDireccion, new Guid(giro));
                //DLEmpresa.DataSource = MVEMpresa.LISTADEEMPRESAS;
                //DLEmpresa.DataBind();
            }
            else
            // Busqueda por categoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                MVEmpresa.BuscarEmpresaBusquedaCliente("Categoria",  Dia, UidDireccion, new Guid(categoria));
                //DLEmpresa.DataSource = MVEMpresa.LISTADEEMPRESAS;
                //DLEmpresa.DataBind();
            }
            else
            //Busqueda por subcategoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            {
                MVEmpresa.BuscarEmpresaBusquedaCliente("Subcategoria",  Dia, UidDireccion, new Guid(subcategoria));
                //DLEmpresa.DataSource = MVEMpresa.LISTADEEMPRESAS;
                //DLEmpresa.DataBind();
            }

            lblCantidadDeResultados.Text = MVEmpresa.LISTADEEMPRESAS.Count + " resultados";
        }

        protected void BuscarProductos()
        {
            lblCantidadDeResultados.Text = string.Empty;
            //DropDownList DDlGiro = Master.FindControl("DDlGiro") as DropDownList;
            DropDownList DDlUbicacion = Master.FindControl("DDlUbicacion") as DropDownList;
            //DropDownList DDlCategoria = Master.FindControl("DDlCategoria") as DropDownList;
            //DropDownList DDlSubcategoria = Master.FindControl("DDlSubcategoria") as DropDownList;
            //TextBox txtNombreDeBusqueda = Master.FindControl("txtNombreDeBusqueda") as TextBox;

            string giro = DDlGiro.SelectedItem.Value;
            string categoria = string.Empty;
            string subcategoria = string.Empty;
            Guid UidDireccion = MVDireccion.ObtenerUidColonia(DDlUbicacion.SelectedItem.Value);
            string Hora = string.Empty;
            if (DateTime.Now.Hour < 10)
            {
                Hora = "0" + DateTime.Now.Hour.ToString();
            }
            else
            {
                Hora = DateTime.Now.Hour.ToString();
            }
            string hora = Hora + ":" + DateTime.Now.Minute.ToString();
            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            string NombreABuscar = txtNombreDeBusqueda.Text;
            if (DDlCategoria.SelectedItem != null && new Guid(DDlCategoria.SelectedItem.Value) != Guid.Empty)
            {
                categoria = DDlCategoria.SelectedItem.Value;
            }
            if (DDlSubcategoria.SelectedItem != null && new Guid(DDlSubcategoria.SelectedItem.Value) != Guid.Empty)
            {
                subcategoria = DDlSubcategoria.SelectedItem.Value;
            }
            //Busqueda por giro
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                MVProducto.buscarProductosEmpresaDesdeCliente("Giro",  Dia, UidDireccion, new Guid(giro));
            }
            else
            // Busqueda por categoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                MVProducto.buscarProductosEmpresaDesdeCliente("Categoria",  Dia, UidDireccion, new Guid(categoria));
            }
            else
            //Busqueda por subcategoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            {
                MVProducto.buscarProductosEmpresaDesdeCliente("Subcategoria", hora,  UidDireccion, new Guid(subcategoria));
            }

            lblCantidadDeResultados.Text = MVProducto.ListaDeProductos.Count + " resultados";
        }

        #endregion

        protected void DLProductos_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "VentanaModalCarrito")
            {
                HiddenField uid = e.Item.FindControl("HFUidProducto") as HiddenField;
                //DropDownList DDlGiro = Master.FindControl("DDlGiro") as DropDownList;
                DropDownList DDlUbicacion = Master.FindControl("DDlUbicacion") as DropDownList;
                TextBox txtNotasProducto = Master.FindControl("txtNotasProducto") as TextBox;
                //DropDownList DDlCategoria = Master.FindControl("DDlCategoria") as DropDownList;
                //DropDownList DDlSubcategoria = Master.FindControl("DDlSubcategoria") as DropDownList;
                //TextBox txtNombreDeBusqueda = Master.FindControl("txtNombreDeBusqueda") as TextBox;
                Label lblTituloProducto = Master.FindControl("lblTituloProducto") as Label;
                Label lblMensaje = Master.FindControl("lblMensaje") as Label;
                HiddenField HFUidProducto = Master.FindControl("HFUidProducto") as HiddenField;
                ListView LvSucursales = Master.FindControl("LvSucursales") as ListView;
                Label lblCantidadProducto = Master.FindControl("lblCantidadProducto") as Label;
                Label LblDescripcionCarrito = Master.FindControl("LblDescripcionCarrito") as Label;
                Label lblPrecioFinal = Master.FindControl("lblPrecioFinal") as Label;
                Image ImgProductoCarrito = Master.FindControl("ImgProductoCarrito") as Image;
                Image ImgEmpresaDefault = Master.FindControl("ImgEmpresaDefault") as Image;
                Panel PanelDetallesProducto = Master.FindControl("PanelDetallesProducto") as Panel;
                Panel pnlMensajeProducto = Master.FindControl("pnlMensajeProducto") as Panel;

                Guid UidProducto = new Guid(uid.Value.ToString());
                var registro = MVProducto.ListaDeProductos.Find(r => r.UID == UidProducto);
                HFUidProducto.Value = UidProducto.ToString();
                lblTituloProducto.Text = registro.STRNOMBRE;
                ImgProductoCarrito.ImageUrl = registro.STRRUTA;

                pnlMensajeProducto.Visible = false;
                LblDescripcionCarrito.Text = registro.STRDESCRIPCION;
                lblMensaje.Text = string.Empty;
                txtNotasProducto.Text = string.Empty;
                txtNotasProducto.Attributes.Add("placeholder", "Agregar nota para " + registro.STRNOMBRE + "");
                lblCantidadProducto.Text = "1";
                #region Busqueda de sucursales con el  mismo producto
                string giro = DDlGiro.SelectedItem.Value;
                string categoria = string.Empty;
                string subcategoria = string.Empty;
                Guid Colonia = new Guid(DDlUbicacion.SelectedItem.Value);

                
                CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
                string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                string NombreABuscar = txtNombreDeBusqueda.Text;
                if (DDlCategoria.SelectedItem != null && new Guid(DDlCategoria.SelectedItem.Value) != Guid.Empty)
                {
                    categoria = DDlCategoria.SelectedItem.Value;
                }
                if (DDlSubcategoria.SelectedItem != null && new Guid(DDlSubcategoria.SelectedItem.Value) != Guid.Empty)
                {
                    subcategoria = DDlSubcategoria.SelectedItem.Value;
                }

                if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                {
                    // GetBuscarProductosCliente
                    MVProducto.BuscarProductoPorSucursal("Giro",  Dia, Colonia, new Guid(giro), UidProducto);
                }
                if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                {
                    // GetBuscarProductosCliente
                    MVProducto.BuscarProductoPorSucursal("Categoria",  Dia, Colonia, new Guid(categoria), UidProducto);
                }
                if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
                {
                    // GetBuscarProductosCliente
                    MVProducto.BuscarProductoPorSucursal("Subcategoria", Dia, Colonia, new Guid(subcategoria), UidProducto);
                }

                LvSucursales.SelectedIndex = 0;
                lblPrecioFinal.Text = "$" + (decimal.Parse(lblCantidadProducto.Text) * decimal.Parse(MVProducto.ListaDePreciosSucursales[0].StrCosto)).ToString().Replace(",", ".");

                PanelDetallesProducto.Visible = true;
                LvSucursales.DataSource = MVProducto.ListaDePreciosSucursales;
                LvSucursales.DataBind();

                // GetImagenDePerfilEmpresa
                MVImagen.ObtenerImagenPerfilDeEmpresa(MVProducto.ListaDePreciosSucursales[0].UIDEMPRESA.ToString());
                ImgEmpresaDefault.ImageUrl = "../" + MVImagen.STRRUTA;
                ImgEmpresaDefault.DataBind();
                #endregion
                //Alimenta los listview
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#myModal').modal('show');", true);
            }
        }

        protected void DLProductos_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            HiddenField hf = e.Item.FindControl("HFUidProducto") as HiddenField;
            DropDownList DDlUbicacion = Master.FindControl("DDlUbicacion") as DropDownList;
            Label LblProductoEnCarrito = e.Item.FindControl("LblProductoEnCarrito") as Label;
            Label lblDescripcionDeProducto = e.Item.FindControl("lblDescripcionDeProducto") as Label;
            hf.Value = MVProducto.ListaDeProductos[e.Item.ItemIndex].UID.ToString();
            Label lblTituloProducto = Master.FindControl("lblTituloProducto") as Label;
            Label lblMensaje = Master.FindControl("lblMensaje") as Label;
            HiddenField HFUidProducto = Master.FindControl("HFUidProducto") as HiddenField;
            ListView LvSucursales = Master.FindControl("LvSucursales") as ListView;
            Label lblCantidadProducto = Master.FindControl("lblCantidadProducto") as Label;
            Label LblDescripcionCarrito = Master.FindControl("LblDescripcionCarrito") as Label;
            Image ImgProductoCarrito = Master.FindControl("ImgProductoCarrito") as Image;

            Guid UidProducto = MVProducto.ListaDeProductos[e.Item.ItemIndex].UID;
            Label lblPrecio = e.Item.FindControl("lblPrecio") as Label;
            var registro = MVProducto.ListaDeProductos.Find(r => r.UID == UidProducto);
            HFUidProducto.Value = UidProducto.ToString();

            string NombreProducto = string.Empty;
            if (registro.STRNOMBRE.Length >= 12)
            {
                NombreProducto = registro.STRNOMBRE.Substring(0, 8) + "...";
            }
            else
            {
                NombreProducto = registro.STRNOMBRE;
            }
            lblDescripcionDeProducto.Text = registro.STRDESCRIPCION;
            lblTituloProducto.Text = NombreProducto;
            lblTituloProducto.ToolTip = registro.STRNOMBRE;
            ImgProductoCarrito.ImageUrl = registro.STRRUTA;

            LblDescripcionCarrito.Text = registro.STRDESCRIPCION;
            lblMensaje.Text = string.Empty;
            lblCantidadProducto.Text = "1";
            LvSucursales.SelectedIndex = -1;

            string giro = DDlGiro.SelectedItem.Value;
            string categoria = string.Empty;
            string subcategoria = string.Empty;
            Guid Colonia = new Guid(DDlUbicacion.SelectedItem.Value);

            
            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            string NombreABuscar = txtNombreDeBusqueda.Text;
            if (DDlCategoria.SelectedItem != null && new Guid(DDlCategoria.SelectedItem.Value) != Guid.Empty)
            {
                categoria = DDlCategoria.SelectedItem.Value;
            }
            if (DDlSubcategoria.SelectedItem != null && new Guid(DDlSubcategoria.SelectedItem.Value) != Guid.Empty)
            {
                subcategoria = DDlSubcategoria.SelectedItem.Value;
            }

            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                MVProducto.BuscarProductoPorSucursal("Giro",  Dia, Colonia, new Guid(giro), UidProducto);
            }
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                MVProducto.BuscarProductoPorSucursal("Categoria",  Dia, Colonia, new Guid(categoria), UidProducto);
            }
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            {
                MVProducto.BuscarProductoPorSucursal("Subcategoria",  Dia, Colonia, new Guid(subcategoria), UidProducto);
            }
            lblPrecio.Text = "$" + MVProducto.ListaDePreciosSucursales[0].StrCosto.Replace(",", ".");
            bool Producto = false;
            if (MVProducto.ListaDelCarrito != null && MVProducto.ListaDelCarrito.Count > 0)
            {
                Producto = MVProducto.ListaDelCarrito.Exists(p => p.UID == registro.UID);
            }
            if (Producto)
            {
                LblProductoEnCarrito.ForeColor = System.Drawing.Color.Purple;
                var registros = MVProducto.ListaDelCarrito.FindAll(p => p.UID == registro.UID);
                int cantidad = 0;
                foreach (var item in registros)
                {
                    cantidad = cantidad + item.Cantidad;
                }
                LblProductoEnCarrito.Text = cantidad.ToString();
                LblProductoEnCarrito.ToolTip = cantidad.ToString() + " articulos en el carrito";
            }
            else
            {
                LblProductoEnCarrito.ForeColor = System.Drawing.Color.Gray;
            }
        }

        protected void DDlGiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string uidgiro = DDlGiro.SelectedItem.Value;
            MVCategoria.BuscarCategorias(UidGiro: uidgiro, tipo: "seleccion");
            DDlCategoria.DataSource = MVCategoria.LISTADECATEGORIAS;
            DDlCategoria.DataValueField = "UIDCATEGORIA";
            DDlCategoria.DataTextField = "STRNOMBRE";
            DDlCategoria.DataBind();


            MVSubcategoria.BuscarSubCategoria(UidCategoria: Guid.Empty.ToString(), Tipo: "Seleccionar");
            DDlSubcategoria.DataSource = MVSubcategoria.LISTADESUBCATEGORIAS;
            DDlSubcategoria.DataValueField = "UID";
            DDlSubcategoria.DataTextField = "STRNOMBRE";
            DDlSubcategoria.DataBind();
        }

        protected void DDlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string uidcategoria = DDlCategoria.SelectedItem.Value;
            MVSubcategoria.BuscarSubCategoria(UidCategoria: uidcategoria, Tipo: "Seleccionar");
            DDlSubcategoria.DataSource = MVSubcategoria.LISTADESUBCATEGORIAS;
            DDlSubcategoria.DataValueField = "UID";
            DDlSubcategoria.DataTextField = "STRNOMBRE";
            DDlSubcategoria.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            //Limpia la lista de la busqueda 
            blBuqueda.Items.Clear();
            DropDownList DDlUbicacion = Master.FindControl("DDlUbicacion") as DropDownList;
            string giro = DDlGiro.SelectedItem.Value;
            string categoria = string.Empty;
            string subcategoria = string.Empty;
            Guid UidDireccion = new Guid(DDlUbicacion.SelectedItem.Value);
            string Hora = string.Empty;

            ListItem ItemGiro = new ListItem();
            ListItem ItemCategoria = new ListItem();
            ListItem ItemSubcategoria = new ListItem();

            ItemGiro.Text = DDlGiro.SelectedItem.Text;
            blBuqueda.Items.Add(ItemGiro);
            if (DateTime.Now.Hour < 10)
            {
                Hora = "0" + DateTime.Now.Hour.ToString();
            }
            else
            {
                Hora = DateTime.Now.Hour.ToString();
            }

            string hora = Hora + ":" + DateTime.Now.Minute.ToString();
            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            string NombreABuscar = txtNombreDeBusqueda.Text;
            if (DDlCategoria.SelectedItem != null && new Guid(DDlCategoria.SelectedItem.Value) != Guid.Empty)
            {
                categoria = DDlCategoria.SelectedItem.Value;
                ItemCategoria.Text = DDlCategoria.SelectedItem.Text;
            }
            if (DDlSubcategoria.SelectedItem != null && new Guid(DDlSubcategoria.SelectedItem.Value) != Guid.Empty)
            {
                subcategoria = DDlSubcategoria.SelectedItem.Value;
                ItemSubcategoria.Text = DDlSubcategoria.SelectedItem.Text;
            }


            if (ItemCategoria != null)
            {
                blBuqueda.Items.Add(ItemCategoria);
            }
            if (ItemSubcategoria != null)
            {
                blBuqueda.Items.Add(ItemSubcategoria);
            }

            if (!string.IsNullOrEmpty(NombreABuscar))
            {
                ListItem nombre = new ListItem();
                nombre.Text = NombreABuscar;
                blBuqueda.Items.Add(nombre);
            }
            //Busqueda de producto

            //Busqueda por giro
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                //giro
                MVEmpresa.BuscarEmpresaBusquedaCliente("Giro",  Dia, UidDireccion, new Guid(giro), NombreABuscar);
                MVProducto.buscarProductosEmpresaDesdeCliente("Giro",  Dia, UidDireccion, new Guid(giro), NombreABuscar);
            }
            else
            // Busqueda por categoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                MVEmpresa.BuscarEmpresaBusquedaCliente("Categoria",  Dia, UidDireccion, new Guid(categoria), NombreABuscar);
                MVProducto.buscarProductosEmpresaDesdeCliente("Categoria",  Dia, UidDireccion, new Guid(categoria), NombreABuscar);
            }
            else
            //Busqueda por subcategoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            {
                MVEmpresa.BuscarEmpresaBusquedaCliente("Subcategoria",  Dia, UidDireccion, new Guid(subcategoria), NombreABuscar);
                MVProducto.buscarProductosEmpresaDesdeCliente("Subcategoria",  Dia, UidDireccion, new Guid(subcategoria), NombreABuscar);
            }
            PanelCategorias.Visible = false;
            if (Page.AppRelativeVirtualPath != "~/Vista/Cliente/Default.aspx")
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblCantidadDeResultados.Text = string.Empty;
                if (PanelEmpresas.Visible)
                {
                    DLEmpresa.DataSource = MVEmpresa.LISTADEEMPRESAS;
                    DLEmpresa.DataBind();
                    lblCantidadDeResultados.Text = MVEmpresa.LISTADEEMPRESAS.Count + " resultados";
                }
                if (PanelProductos.Visible)
                {
                    DLProductos.DataSource = MVProducto.ListaDeProductos;
                    DLProductos.DataBind();
                    lblCantidadDeResultados.Text = MVProducto.ListaDeProductos.Count + " resultados";
                }
            }
        }

        protected void btnFiltrosBusqueda_Click(object sender, EventArgs e)
        {

            MVEmpresa.LISTADEEMPRESAS.Clear();
            MVProducto.ListaDeProductos.Clear();
            if (PanelCategorias.Visible)
            {
                PanelCategorias.Visible = false;
            }
            else
            {
                PanelCategorias.Visible = true;
            }
        }
        
        protected void DLEmpresa_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "VentanaEmpresas")
            {
                Label lblUbicacion = Master.FindControl("lblUidDireccion") as Label;
                Guid UidEmpresa = new Guid(DLEmpresa.DataKeys[e.Item.ItemIndex].ToString());
                Session["UidEmpresa"] = UidEmpresa;
                PanelDeDetallesEmpresa.Visible = true;
                PanelBusqueda.Visible = false;


                MVImagen.ObtenerImagenPerfilDeEmpresa(UidEmpresa.ToString());
                imgEmpresa.ImageUrl = "../" + MVImagen.STRRUTA;
                imgEmpresa.AlternateText = MVImagen.STRDESCRIPCION;

                MVEmpresa.BuscarEmpresas(UidEmpresa: UidEmpresa);
                lblNombreEmpresa.Text = MVEmpresa.NOMBRECOMERCIAL;
                TimeZoneInfo localZone = TimeZoneInfo.Local;
                

               
                DropDownList DDlUbicacion = Master.FindControl("DDlUbicacion") as DropDownList;
                Guid UidDireccion = new Guid(DDlUbicacion.SelectedItem.Value);
                if (DDlUbicacion != null)
                {

                    
                    CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
                    string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                    if (DDlUbicacion.Text != null)
                    {
                        MVSucursales.BuscarSucursalesCliente(UidEmpresa,  Dia, UidDireccion);
                    }
                }


                var registro = MVSucursales.LISTADESUCURSALES[0];
                MVOferta.Buscar(UIDSUCURSAL: registro.ID, ESTATUS: "1");
                ddlOfertas.DataSource = MVOferta.ListaDeOfertas;
                ddlOfertas.DataTextField = "STRNOMBRE";
                ddlOfertas.DataValueField = "UID";
                ddlOfertas.DataBind();

                MuestraSeccion(MVOferta.ListaDeOfertas[0].UID, UidDireccion);

                hfSucursalSeleccionada.Value = registro.ID.ToString();
                lblNombreSucursal.Text = registro.IDENTIFICADOR;
                lblHorarioDeServicio.Text = "De " + registro.HORAAPARTURA + " A " + registro.HORACIERRE;

                MVDireccion.ObtenerDireccionSucursal(registro.ID.ToString());
                lblDireccionSucursalSeleccionada.Text = "Ubicación: " + MVDireccion.ObtenerNombreDeLaColonia(MVDireccion.COLONIA) + ", " + MVDireccion.CALLE0;
                if (MVSeccion.ListaDeSeccion.Count > 0)
                {
                    MVProducto.BuscarProductosSeccion(MVSeccion.ListaDeSeccion[0].UID);

                    DLProductosSucursal.DataSource = MVProducto.ListaDeProductos;
                    DLProductosSucursal.DataBind();
                }
                MVImagen.obtenerImagenDePortadaEmpresa(UidEmpresa.ToString());
                if (!string.IsNullOrWhiteSpace(MVImagen.STRRUTA))
                {
                    PortadaEmpresa.Attributes.Add("style", "background-image: url('../" + MVImagen.STRRUTA + "'); background-repeat: repeat; background-size: percentage;");
                }
                else
                {
                    PortadaEmpresa.Attributes.Add("style", "background-color: #f1f1f1; background-repeat: repeat; background-size: percentage;");
                }



            }
        }

        protected void DLEmpresa_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            
            Guid UidEmpresa = new Guid(DLEmpresa.DataKeys[e.Item.ItemIndex].ToString());
            Label lblUbicacion = Master.FindControl("lblUidDireccion") as Label;
            Guid UidDireccion = new Guid(lblUbicacion.Text);
            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            Label lblCantidadDeSucursales = e.Item.FindControl("lblCantidadDeSucursales") as Label;
            Label lblCantidadDeProductos = e.Item.FindControl("lblCantidadDeProductos") as Label;
            MVSucursales.BuscarSucursalesCliente(UidEmpresa,  Dia, UidDireccion);

            if (MVSucursales.LISTADESUCURSALES.Count > 1)
            {
                lblCantidadDeSucursales.Text = MVSucursales.LISTADESUCURSALES.Count.ToString() + " sucursales disponibles";
            }
            else
            {
                lblCantidadDeSucursales.Text = MVSucursales.LISTADESUCURSALES.Count.ToString() + " sucursal disponible";
            }

            if (MVProducto != null)
            {
                int articulos = 0;

                foreach (var item in MVSucursales.LISTADESUCURSALES)
                {
                    var productos = MVProducto.ListaDelCarrito.FindAll(p => p.UidSucursal == item.ID);

                    for (int i = 0; i < productos.Count; i++)
                    {
                        articulos = articulos + productos[i].Cantidad;
                    }
                }
                if (articulos>0)
                {
                    lblCantidadDeProductos.ForeColor = System.Drawing.Color.Purple;
                    lblCantidadDeProductos.Text = articulos.ToString();
                }
                else
                {
                    lblCantidadDeProductos.ForeColor = System.Drawing.Color.Gray;
                }
                
            }

        }

        protected void ddlOfertas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList DDlUbicacion = Master.FindControl("DDlUbicacion") as DropDownList;
            Guid UidDireccion = new Guid(DDlUbicacion.SelectedItem.Value);
            MuestraSeccion(new Guid(ddlOfertas.SelectedItem.Value), UidDireccion);
        }

        public void MuestraSeccion(Guid UidOferta,Guid UidDireccion)
        {
            MVSeccion.Buscar(UIDOFERTA: UidOferta, UidDirecccion: UidDireccion);
            MnSecciones.Items.Clear();
            MenuItem registro = new MenuItem();

            foreach (var item in MVSeccion.ListaDeSeccion)
            {
                registro = new MenuItem();
                registro.Text = item.StrNombre;
                registro.Value = item.UID.ToString();
                MnSecciones.Items.Add(registro);
            }
            if (MVSeccion.ListaDeSeccion.Count > 0)
            {
                MnSecciones.Items[0].Selected = true;
            }
            MnSecciones.DataBind();
        }

        protected void btnMostrarSucursales_Click(object sender, EventArgs e)
        {
            Label lblNombreDeEmpresa = Master.FindControl("lblNombreDeEmpresa") as Label;
            ListView lvSucursalesEmpresa = Master.FindControl("lvSucursalesEmpresa") as ListView;
            lvSucursalesEmpresa.DataSource = MVSucursales.LISTADESUCURSALES;
            lvSucursalesEmpresa.DataBind();
            foreach (var item in lvSucursalesEmpresa.Items)
            {
                if (lvSucursalesEmpresa.DataKeys[item.DataItemIndex].Value.ToString() == hfSucursalSeleccionada.Value.ToString())
                {
                    lvSucursalesEmpresa.SelectedIndex = item.DataItemIndex;
                }
            }
            
            lblNombreDeEmpresa.Text = lblNombreEmpresa.Text;
        }

        protected void MnSecciones_MenuItemClick(object sender, MenuEventArgs e)
        {
            e.Item.Selectable = true;
            e.Item.Selected = true;
                    
            Guid Seccion = new Guid(e.Item.Value);
            if (Seccion != Guid.Empty)
            {
                // GetObtenerInformacionDeProductoDeLaSucursal
                MVProducto.BuscarProductosSeccion(Seccion);

                DLProductosSucursal.DataSource = MVProducto.ListaDeProductos;
                DLProductosSucursal.DataBind();
            }
        }

        protected void DLProductosSucursal_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Image imgProductoSeleccionado = Master.FindControl("imgProductoSeleccionado") as Image;
            Label lblDescripcionProductoSeleccionado = Master.FindControl("lblDescripcionProductoSeleccionado") as Label;
            Label lblPrecioDeProductoSeleccionado = Master.FindControl("lblPrecioDeProductoSeleccionado") as Label;
            Label lblPrecioProductoSeleccionado = Master.FindControl("lblPrecioProductoSeleccionado") as Label;
            Label lblNombreDeProductoSeleccionado = Master.FindControl("lblNombreDeProductoSeleccionado") as Label;
            Label lblCantidadProductoSeleccionado = Master.FindControl("lblCantidadProductoSeleccionado") as Label;
            Label lblUidProductoSeleccionado = Master.FindControl("lblUidProductoSeleccionado") as Label;
            Label lblMensajeProductoSeleccionado = Master.FindControl("lblMensajeProductoSeleccionado") as Label;
            Panel pnlMensajeProductoSeleccionado = Master.FindControl("pnlMensajeProductoSeleccionado") as Panel;

            TextBox txtNota = Master.FindControl("txtNota") as TextBox;
            pnlMensajeProductoSeleccionado.Visible = false;
            lblMensajeProductoSeleccionado.Text = string.Empty;
            Guid UidProducto = MVProducto.ListaDeProductos[e.Item.ItemIndex].UID;
            var registro = MVProducto.ListaDeProductos.Find(r => r.UID == UidProducto);
            txtNota.Attributes.Add("placeholder", "Agregar nota para " + registro.STRNOMBRE + "");
            Label lblUidSucursalSeleccionada = Master.FindControl("lblUidSucursalSeleccionada") as Label;
            Label lblUidSeccionSeleccionada = Master.FindControl("lblUidSeccionSeleccionada") as Label;
            lblUidSucursalSeleccionada.Text = hfSucursalSeleccionada.Value;
            lblUidSeccionSeleccionada.Text = MnSecciones.SelectedValue;

            lblUidProductoSeleccionado.Text = registro.UID.ToString();
            lblCantidadProductoSeleccionado.Text = "1";
            lblNombreDeProductoSeleccionado.Text = registro.STRNOMBRE;
            imgProductoSeleccionado.ImageUrl = registro.STRRUTA;
            lblDescripcionProductoSeleccionado.Text = registro.STRDESCRIPCION;
            lblPrecioDeProductoSeleccionado.Text = "$" + registro.StrCosto;
            lblPrecioProductoSeleccionado.Text = registro.StrCosto;
            txtNota.Text = string.Empty;



        }

        protected void DLProductosSucursal_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            HiddenField hf = e.Item.FindControl("HFUidProducto") as HiddenField;
            DropDownList DDlUbicacion = Master.FindControl("DDlUbicacion") as DropDownList;
            Label LblProductoEnCarrito = e.Item.FindControl("LblProductoEnCarrito") as Label;
            Label lblDescripcionDeProducto = e.Item.FindControl("lblDescripcionDeProducto") as Label;
           
            Guid UidProducto = MVProducto.ListaDeProductos[e.Item.ItemIndex].UID;

            lblDescripcionDeProducto.Text = MVProducto.ListaDeProductos[e.Item.ItemIndex].STRDESCRIPCION;

            Label lblPrecio = e.Item.FindControl("lblPrecio") as Label;
            if (UidProducto != Guid.Empty)
            {
                var registro = MVProducto.ListaDeProductos.Find(p => p.UID == UidProducto);
                lblPrecio.Text = "$" + registro.StrCosto;
            }

            bool Producto = false;
            if (MVProducto.ListaDelCarrito != null && MVProducto.ListaDelCarrito.Count > 0)
            {
                Producto = MVProducto.ListaDelCarrito.Exists(p => p.UID == UidProducto);
            }
            if (Producto)
            {
                var registros = MVProducto.ListaDelCarrito.FindAll(p => p.UID == UidProducto);
                int cantidad = 0;
                foreach (var item in registros)
                {
                    cantidad = cantidad + item.Cantidad;
                }
                if (cantidad>0)
                {
                    LblProductoEnCarrito.ForeColor = System.Drawing.Color.Purple;
                    LblProductoEnCarrito.Text = cantidad.ToString();
                    LblProductoEnCarrito.ToolTip = cantidad.ToString() + " articulo en el carrito";
                }
            }
            else
            {
                LblProductoEnCarrito.ForeColor = System.Drawing.Color.Gray;
                LblProductoEnCarrito.ToolTip = "No existe en el carrito";
            }

        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            PanelDeDetallesEmpresa.Visible = false;
            PanelBusqueda.Visible = true;
        }
    }
}