using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;


namespace WebApplication1.Vista.Cliente
{
    public partial class MasterCliente : System.Web.UI.MasterPage
    {
        #region Propiedades
        VMProducto MVProducto;
        VMAcceso MVAcceso;
        VMOrden MVOrden = new VMOrden();
        VMTarifario MVTarifario = new VMTarifario();
        VMImagen MVImagen = new VMImagen();
        VMSucursales MVSucursales = new VMSucursales();
        VMGiro MVGiro = new VMGiro();
        VMCategoria MVCategoria = new VMCategoria();
        VMSubCategoria MVSubcategoria = new VMSubCategoria();
        VMDireccion MVDireccion;
        VMEmpresas MVEMpresa = new VMEmpresas();
        VMOferta MVOferta = new VMOferta();
        VMSeccion MVSeccion = new VMSeccion();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                if (!IsPostBack)
                {
                    //Obtiene el nombe de la empresa asociada
                    MVAcceso = new VMAcceso();
                    lblNombreUsuario.Text = MVAcceso.NombreDeUsuario(new Guid(Session["IdUsuario"].ToString()));
                    Session["MVOrden"] = MVOrden;
                    Session["MVTarifario"] = MVTarifario;
                    Session["MVImagen"] = MVImagen;

                    string pageName = this.Page.TemplateControl.AppRelativeVirtualPath;

                    if (pageName == "~/Vista/Cliente/Default.aspx")
                    {
                        HFUidProducto.Value = string.Empty;
                    }
                    if (pageName == "~/Vista/Cliente/Empresa.aspx")
                    {
                        lblUidProductoSeleccionado.Text = string.Empty;
                    }



                    if (Session["MVSucursales"] == null)
                    {
                        Session["MVSucursales"] = MVSucursales;
                    }
                    else
                    {
                        MVSucursales = (VMSucursales)Session["MVSucursales"];
                    }

                    if (Session["MVSeccion"] == null)
                    {
                        Session["MVSeccion"] = MVSucursales;
                    }
                    else
                    {
                        MVSeccion = (VMSeccion)Session["MVSeccion"];
                    }
                    //Valida que exista la sesion, esto para mostrar siempre el carrito de compras con el producto ingresado
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
                    Session["MVCategoria"] = MVCategoria;
                    Session["MVSubcategoria"] = MVSubcategoria;
                    Session["MVEMpresa"] = MVEMpresa;
                    MVEMpresa.LISTADEEMPRESAS = new System.Collections.Generic.List<VMEmpresas>();

                    MuestraDetallesDeLaOrdenGeneral();

                    MVDireccion.ObtenerDireccionesUsuario(Session["IdUsuario"].ToString());

                    DDlUbicacion.DataSource = MVDireccion.ListaDIRECCIONES;
                    DDlUbicacion.DataValueField = "ID";
                    DDlUbicacion.DataTextField = "Identificador";
                    DDlUbicacion.DataBind();

                    PanelDetallesProducto.Visible = false;
                }
                else
                {
                    if (Session["MVProducto"] != null)
                    {
                        MVProducto = (VMProducto)Session["MVProducto"];
                    }

                    MVImagen = (VMImagen)Session["MVImagen"];
                    MVOrden = (VMOrden)Session["MVOrden"];
                    MVSucursales = (VMSucursales)Session["MVSucursales"];
                    MVTarifario = (VMTarifario)Session["MVTarifario"];
                    MVGiro = (VMGiro)Session["MVGiro"];
                    MVCategoria = (VMCategoria)Session["MVCategoria"];
                    MVSubcategoria = (VMSubCategoria)Session["MVSubcategoria"];
                    MVDireccion = (VMDireccion)Session["MVDireccion"];
                    MVEMpresa = (VMEmpresas)Session["MVEMpresa"];
                    MVOferta = (VMOferta)Session["MVOferta"];
                    MVSeccion = (VMSeccion)Session["MVSeccion"];
                    MuestraDetallesDeLaOrdenGeneral();
                }
            }
            else
            {
                Response.Redirect("../Default/");
            }
        }

        protected void btnPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("Configuracion.aspx", false);
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnHistorial_Click(object sender, EventArgs e)
        {
            Response.Redirect("Historico.aspx");
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Remove("IdUsuario");
            Response.Redirect("../Default/Default.aspx");
        }


        /// <summary>
        /// Controla los datos de detalle general de las compras en el carrito
        /// </summary>
        protected void MuestraDetallesDeLaOrdenGeneral()
        {
            int cantidad = 0;
            foreach (var pro in MVProducto.ListaDelCarrito)
            {
                cantidad = cantidad + pro.Cantidad;
            }

            if (cantidad == 0)
            {
                lblProductosEnCarrito.Text = string.Empty;
            }
            else
            {
                lblProductosEnCarrito.Text = cantidad.ToString();
            }
        }


        #region Busqueda
        protected void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            Response.Redirect("Configuracion.aspx");
        }

        #endregion

        protected void DDlUbicacion_Load(object sender, EventArgs e)
        {
            if (DDlUbicacion.SelectedItem != null)
            {
                lblUidDireccion.Text = DDlUbicacion.SelectedItem.Value;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            //string giro = DDlGiro.SelectedItem.Value;
            //string categoria = string.Empty;
            //string subcategoria = string.Empty;
            //Guid Colonia = MVDireccion.ObtenerUidColonia(DDlUbicacion.SelectedItem.Value);
            //string Hora = string.Empty;
            //if (DateTime.Now.Hour < 10)
            //{
            //    Hora = "0" + DateTime.Now.Hour.ToString();
            //}
            //else
            //{
            //    Hora = DateTime.Now.Hour.ToString();
            //}

            //string hora = Hora + ":" + DateTime.Now.Minute.ToString();
            //CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            //string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            //string NombreABuscar = string.Empty;//txtNombreDeBusqueda.Text;
            //if (DDlCategoria.SelectedItem != null && new Guid(DDlCategoria.SelectedItem.Value) != Guid.Empty)
            //{
            //    categoria = DDlCategoria.SelectedItem.Value;
            //}
            //if (DDlSubcategoria.SelectedItem != null && new Guid(DDlSubcategoria.SelectedItem.Value) != Guid.Empty)
            //{
            //    subcategoria = DDlSubcategoria.SelectedItem.Value;
            //}

            ////Busqueda de producto

            ////Busqueda por giro
            //if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            //{
            //    //giro
            //    MVEMpresa.BuscarEmpresaBusquedaCliente("Giro", hora, Dia, Colonia, new Guid(giro), NombreABuscar);
            //    MVProducto.buscarProductosEmpresaDesdeCliente("Giro", hora, Dia, Colonia, new Guid(giro), NombreABuscar);
            //}
            //else
            //// Busqueda por categoria
            //if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            //{
            //    MVEMpresa.BuscarEmpresaBusquedaCliente("Categoria", hora, Dia, Colonia, new Guid(categoria), NombreABuscar);
            //    MVProducto.buscarProductosEmpresaDesdeCliente("Categoria", hora, Dia, Colonia, new Guid(categoria), NombreABuscar);
            //}
            //else
            ////Busqueda por subcategoria
            //if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            //{
            //    MVEMpresa.BuscarEmpresaBusquedaCliente("Subcategoria", hora, Dia, Colonia, new Guid(subcategoria), NombreABuscar);
            //    MVProducto.buscarProductosEmpresaDesdeCliente("Subcategoria", hora, Dia, Colonia, new Guid(subcategoria), NombreABuscar);
            //}

            //if (Page.AppRelativeVirtualPath != "~/Vista/Cliente/Default.aspx")
            //{
            //    Response.Redirect("Default.aspx");
            //}
            //else
            //{
            //    Panel panelEmpresa = Body.FindControl("PanelEmpresas") as Panel;
            //    Panel panelProductos = Body.FindControl("PanelProductos") as Panel;
            //    DataList DLProductos = Body.FindControl("DLProductos") as DataList;
            //    DataList DLEmpresa = Body.FindControl("DLEmpresa") as DataList;
            //    if (panelEmpresa.Visible)
            //    {
            //        DLEmpresa.DataSource = MVEMpresa.LISTADEEMPRESAS;
            //        DLEmpresa.DataBind();
            //    }
            //    if (panelProductos.Visible)
            //    {
            //        DLProductos.DataSource = MVProducto.ListaDeProductos;
            //        DLProductos.DataBind();
            //    }
            //}
        }

        protected void btnCategorias_Click(object sender, EventArgs e)
        {
            //if (PanelCategorias.Visible)
            //{
            //    PanelCategorias.Visible = false;
            //}
            //else if (!PanelCategorias.Visible)
            //{
            //    PanelCategorias.Visible = true;
            //}
        }



        protected void LvSucursales_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HiddenField uid = e.Item.FindControl("lblUidSeccion") as HiddenField;
            HiddenField UidSucursal = e.Item.FindControl("lblUidSucursal") as HiddenField;
            Label Precio = e.Item.FindControl("lblPrecio") as Label;
            Label tiempo = e.Item.FindControl("lblTiempoDeElaboracion") as Label;
            Label Sucursal = e.Item.FindControl("lblIdentificador") as Label;
            Label lblColonia = e.Item.FindControl("lblColonia") as Label;
            LinkButton btnSeleccionado = e.Item.FindControl("btnSeleccionado") as LinkButton;
            

            VMProducto registro = MVProducto.ListaDePreciosSucursales[e.Item.DataItemIndex];
            uid.Value = registro.UID.ToString();

            int minutos = 0;
            if (registro.DtmVariableParaTiempo.Hour > 1)
            {
                for (int i = 1; i < registro.DtmVariableParaTiempo.Hour; i++)
                {
                    minutos = minutos + 60;
                }
            }
            minutos = minutos + registro.DtmVariableParaTiempo.Minute;
            Precio.Text = "Precio " + "$" + registro.StrCosto;
            tiempo.Text = "Tiempo " + minutos + " minutos. ";
            Sucursal.Text = registro.StrIdentificador;
            UidSucursal.Value = registro.UidSucursal.ToString();

            MVDireccion.ObtenerDireccionSucursal(registro.UidSucursal.ToString());
            lblColonia.Text = MVDireccion.ObtenerNombreDeLaColonia(MVDireccion.COLONIA) + MVDireccion.CALLE0;
            if (MVProducto.ListaDeSubcategorias.Count == 1)
            {
                btnSeleccionado.CommandName = string.Empty;
            }
            

        }

        protected void LvSucursales_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {

        }

        protected void LvSucursales_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            ListView dl = sender as ListView;
            if (e == null || e.Item == null)
            {
                Trace.Write("dl_ItemCommand", "EventArgs.Item is null");
                throw new Exception("dl_ItemCommand: EventArgs.Item is null");
            }

            int selIdx = dl.SelectedIndex;

            Trace.Write("dl_ItemCommand", String.Format("{0}: {1}",
             e.CommandName.ToLower(), e.Item.DataItemIndex));

            switch (e.CommandName.ToLower())
            {
                case "select":
                    HiddenField uidSeccion = e.Item.FindControl("lblUidSeccion") as HiddenField;
                    selIdx = e.Item.DataItemIndex;
                    dl.SelectedIndex = selIdx;
                    HiddenField uid = dl.Items[dl.SelectedIndex].FindControl("lblUidSeccion") as HiddenField;
                    Label Precio = dl.Items[dl.SelectedIndex].FindControl("lblPrecio") as Label;
                    Label tiempo = dl.Items[dl.SelectedIndex].FindControl("lblTiempoDeElaboracion") as Label;
                    Label Sucursal = dl.Items[dl.SelectedIndex].FindControl("lblIdentificador") as Label;


                    VMProducto registro = MVProducto.ListaDePreciosSucursales.Find(p => p.UID == new Guid(uidSeccion.Value));
                    uid.Value = registro.UID.ToString();
                    Precio.Text = "Precio: $" + registro.StrCosto;
                    tiempo.Text = "Tiempo de elaboracion:" + registro.STRTiemporElaboracion;
                    Sucursal.Text = registro.StrIdentificador;

                    int Cantidad = int.Parse(lblCantidadProducto.Text);
                    double precio = double.Parse(registro.StrCosto);
                    lblPrecioFinal.Text = "$" + (Cantidad * precio).ToString();
                    break;
                case "unselect":
                    selIdx = -1;
                    lblPrecioFinal.Text = "$" + (int.Parse(lblCantidadProducto.Text) * 0).ToString();
                    break;
                default:
                    break;
            }
            if (selIdx != dl.SelectedIndex)
                dl.SelectedIndex = selIdx;
            dl.DataSource = MVProducto.ListaDePreciosSucursales;
            dl.DataBind();
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            int resultado;
            if (int.TryParse(lblCantidadProducto.Text, out resultado) && int.Parse(lblCantidadProducto.Text) >= 0)
            {
                int cantidad = int.Parse(lblCantidadProducto.Text);
                lblCantidadProducto.Text = (cantidad + 1).ToString();

            }
            else
            {
                pnlMensajeProducto.Visible = true;
                lblMensaje.Text = "Solo se admiten numeros mayores a 0";
            }
            if (LvSucursales.SelectedIndex != -1)
            {

                Guid UidSucursal = new Guid(LvSucursales.DataKeys[LvSucursales.SelectedIndex].Value.ToString());
                var registro = MVProducto.ListaDePreciosSucursales.Find(obj => obj.UidSucursal == UidSucursal);

                int Cantidad = int.Parse(lblCantidadProducto.Text);
                string stPrecio = registro.StrCosto;
                decimal Precio = decimal.Parse(stPrecio);
                lblPrecioFinal.Text = "$" + (Cantidad * Precio).ToString();
            }
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            int resultado;
            if (lblCantidadProducto.Text != "0")
            {
                if (int.TryParse(lblCantidadProducto.Text, out resultado) && int.Parse(lblCantidadProducto.Text) >= 0)
                {
                    int cantidad = int.Parse(lblCantidadProducto.Text);
                    lblCantidadProducto.Text = (cantidad - 1).ToString();

                }
                else
                {
                    lblMensaje.Text = "Solo se admiten numeros mayores a 0";
                }
                if (LvSucursales.SelectedIndex != -1)
                {
                    Guid UidSucursal = new Guid(LvSucursales.DataKeys[LvSucursales.SelectedIndex].Value.ToString());
                    var registro = MVProducto.ListaDePreciosSucursales.Find(obj => obj.UidSucursal == UidSucursal);

                    int Cantidad = int.Parse(lblCantidadProducto.Text);
                    string stPrecio = registro.StrCosto;
                    decimal Precio = decimal.Parse(stPrecio);
                    lblPrecioFinal.Text = "$" + (Cantidad * Precio).ToString();
                }
                else
                {
                    lblPrecioFinal.Text = string.Empty;
                }
            }
        }

        protected void btnAgregaCarrito_Click(object sender, EventArgs e)
        {

            if (LvSucursales.SelectedIndex != -1)
            {
                if (lblCantidadProducto.Text != "0")
                {

                    //Cantidad de articulos
                    Guid UidProducto = new Guid(HFUidProducto.Value);
                    Guid sucursal = new Guid(), seccion = new Guid();

                    HiddenField InformacionSeccion = LvSucursales.Items[LvSucursales.SelectedIndex].FindControl("lblUidSeccion") as HiddenField;
                    HiddenField UidSucursal = LvSucursales.Items[LvSucursales.SelectedIndex].FindControl("lblUidSucursal") as HiddenField;
                    seccion = new Guid(InformacionSeccion.Value);
                    sucursal = new Guid(UidSucursal.Value);
                    MVTarifario.BuscarTarifario("Cliente", ZonaEntrega: DDlUbicacion.SelectedValue, uidSucursal: UidSucursal.Value.ToString());
                    AgregarAlcarrito(UidProducto, sucursal, seccion, lblCantidadProducto.Text, txtNotasProducto.Text);

                }
                else
                {
                    pnlMensajeProducto.Visible = true;
                    lblMensaje.Text = "No se pueden agregar cantidades en 0";
                }
            }
            else
            {
                pnlMensajeProducto.Visible = true;
                lblMensaje.Text = "Se debe seleccionar una sucursal para por agregar a carrito";

            }

        }
        protected void txtCantidadProducto_TextChanged(object sender, EventArgs e)
        {
            int resultado;
            if (!int.TryParse(lblCantidadProducto.Text, out resultado))
            {
                lblMensaje.Text = "Solo se admiten numeros mayores a 0";
            }
        }


        #region Modulo Cliente/Empresa.aspx
        protected void lvSucursalesEmpresa_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            //Obtiene los controles de la pagina aniadada (Empresa.aspx)

            Label Sucursal = e.Item.FindControl("lblIdentificador") as Label;
            Label lblColonia = e.Item.FindControl("lblColonia") as Label;
            LinkButton btnSeleccion = e.Item.FindControl("btnSeleccion") as LinkButton;

            VMSucursales registro = MVSucursales.LISTADESUCURSALES[e.Item.DataItemIndex];
            
            

            Sucursal.Text = registro.IDENTIFICADOR;
            MVDireccion.ObtenerDireccionSucursal(registro.ID.ToString());
            lblColonia.Text = "Ubicación: " + MVDireccion.ObtenerNombreDeLaColonia(MVDireccion.COLONIA) + ", " + MVDireccion.CALLE0;

        }

        protected void lvSucursalesEmpresa_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            ListView dl = sender as ListView;
            if (e == null || e.Item == null)
            {
                Trace.Write("dl_ItemCommand", "EventArgs.Item is null");
                throw new Exception("dl_ItemCommand: EventArgs.Item is null");
            }

            int selIdx = dl.SelectedIndex;

            Trace.Write("dl_ItemCommand", String.Format("{0}: {1}",
             e.CommandName.ToLower(), e.Item.DataItemIndex));

            switch (e.CommandName.ToLower())
            {
                case "select":
                    HiddenField uidSeccion = e.Item.FindControl("lblUidSeccion") as HiddenField;
                    LinkButton btnSeleccion = e.Item.FindControl("btnSeleccion") as LinkButton;
                    //selIdx = e.Item.DataItemIndex;
                    //dl.SelectedIndex = selIdx;

                    DropDownList ddlOfertas = PanelCentral.Controls[0].Controls[27].FindControl("ddlOfertas") as DropDownList;
                    Label lblNombreSucursal = PanelCentral.Controls[0].Controls[27].FindControl("lblNombreSucursal") as Label;
                    Label lblHorarioDeServicio = PanelCentral.Controls[0].Controls[27].FindControl("lblHorarioDeServicio") as Label;
                    Label lblDireccionSucursalSeleccionada = PanelCentral.Controls[0].Controls[27].FindControl("lblDireccionSucursalSeleccionada") as Label;
                    DataList DLProductosSucursal = PanelCentral.Controls[0].Controls[27].FindControl("DLProductosSucursal") as DataList;
                    Menu MnSecciones = PanelCentral.Controls[0].Controls[27].FindControl("MnSecciones") as Menu;
                    HiddenField hfSucursalSeleccionada = PanelCentral.Controls[0].Controls[27].FindControl("hfSucursalSeleccionada") as HiddenField;
                    var registro = MVSucursales.LISTADESUCURSALES[e.Item.DataItemIndex];

                    lblNombreSucursal.Text = registro.IDENTIFICADOR;
                    lblHorarioDeServicio.Text = "De " + registro.HORAAPARTURA + " A " + registro.HORACIERRE;

                    MVDireccion.ObtenerDireccionSucursal(registro.ID.ToString());
                    lblDireccionSucursalSeleccionada.Text = "Ubicación: " + MVDireccion.ObtenerNombreDeLaColonia(MVDireccion.COLONIA) + ", " + MVDireccion.CALLE0;

                    MVOferta.Buscar(UIDSUCURSAL: registro.ID);
                    ddlOfertas.DataSource = MVOferta.ListaDeOfertas;
                    ddlOfertas.DataTextField = "STRNOMBRE";
                    ddlOfertas.DataValueField = "UID";
                    ddlOfertas.DataBind();

                    hfSucursalSeleccionada.Value = registro.ID.ToString();
                    MVSeccion.Buscar(UIDOFERTA: MVOferta.ListaDeOfertas[0].UID);
                    MnSecciones.Items.Clear();
                    MenuItem elemento = new MenuItem();
                    foreach (var item in MVSeccion.ListaDeSeccion)
                    {
                        elemento = new MenuItem();
                        elemento.Text = item.StrNombre;
                        elemento.Value = item.UID.ToString();
                        MnSecciones.Items.Add(elemento);
                    }
                    MnSecciones.Items[0].Selected = true;

                    MVProducto.BuscarProductosSeccion(new Guid(MnSecciones.Items[0].Value));

                    DLProductosSucursal.DataSource = MVProducto.ListaDeProductos;
                    DLProductosSucursal.DataBind();
                    break;
                default:
                    break;
            }
            if (selIdx != dl.SelectedIndex)
                dl.SelectedIndex = selIdx;
            dl.DataSource = MVSucursales.LISTADESUCURSALES;
            dl.DataBind();
        }

        protected void lvSucursalesEmpresa_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {

        }

        protected void btnQuita_Click(object sender, EventArgs e)
        {
            if (int.Parse(lblCantidadProductoSeleccionado.Text) >= 1)
            {
                int cantidad = int.Parse(lblCantidadProductoSeleccionado.Text);
                lblCantidadProductoSeleccionado.Text = (cantidad - 1).ToString();
                var registro = MVProducto.ListaDeProductos.Find(obj => obj.UID.ToString() == lblUidProductoSeleccionado.Text);
                decimal precio = (decimal.Parse(registro.StrCosto) * decimal.Parse(lblCantidadProductoSeleccionado.Text));
                lblPrecioProductoSeleccionado.Text = (precio).ToString();
            }
        }

        protected void btnAgrega_Click(object sender, EventArgs e)
        {
            if (int.Parse(lblCantidadProductoSeleccionado.Text) >= 0)
            {
                int cantidad = int.Parse(lblCantidadProductoSeleccionado.Text);
                lblCantidadProductoSeleccionado.Text = (cantidad + 1).ToString();

                var registro = MVProducto.ListaDeProductos.Find(obj => obj.UID.ToString() == lblUidProductoSeleccionado.Text);
                decimal precio = (decimal.Parse(registro.StrCosto) * decimal.Parse(lblCantidadProductoSeleccionado.Text));
                lblPrecioProductoSeleccionado.Text = (precio).ToString();

            }
        }

        protected void btnAgregarAlCarritoProductoSeleccionado_Click(object sender, EventArgs e)
        {
            if (lblCantidadProductoSeleccionado.Text != "0")
            {
                Guid UidProducto, UidSucursal, UidSeccion;
                UidProducto = new Guid(lblUidProductoSeleccionado.Text);

                UidSucursal = new Guid(lblUidSucursalSeleccionada.Text);
                UidSeccion = new Guid(lblUidSeccionSeleccionada.Text);
                MVTarifario.BuscarTarifario("Cliente", ZonaEntrega: DDlUbicacion.SelectedValue, uidSucursal: UidSucursal.ToString());
                AgregarAlcarrito(UidProducto, UidSucursal, UidSeccion, lblCantidadProductoSeleccionado.Text, txtNota.Text);
                DataList DLProductosSucursal = PanelCentral.Controls[0].Controls[27].FindControl("DLProductosSucursal") as DataList;
                DLProductosSucursal.DataSource = MVProducto.ListaDeProductos;
                DLProductosSucursal.DataBind();
            }
            else
            {
                pnlMensajeProductoSeleccionado.Visible = true;
                lblMensajeProductoSeleccionado.Text = "No se pueden agregar cantidades en 0";
            }

        }
        #endregion



        #region Modulo Cliente/Carrito.aspx
        protected void LVSeleccionDistribuidora_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                LinkButton Empresa = e.Item.FindControl("btnEmpresaSeleccionada") as LinkButton;

                Guid UidTarifario = new Guid(LVSeleccionDistribuidora.DataKeys[e.Item.DisplayIndex].Value.ToString());
                if (Empresa != null)
                {
                    ScriptManager.GetCurrent(Page).RegisterAsyncPostBackControl(Empresa);
                }
                Guid UidSucursal = new Guid(HFSucursalSeleccionada.Value);
                Guid Uidtarifario = new Guid(LVSeleccionDistribuidora.DataKeys[e.Item.DisplayIndex].Value.ToString());

                var registro = MVTarifario.ListaDeTarifarios.Find(t => t.UidTarifario == Uidtarifario);
                MVProducto.AgregaTarifarioOrden(UidSucursal, registro.UidTarifario, registro.DPrecio);


                LVSeleccionDistribuidora.DataSource = MVTarifario.ListaDeTarifarios;
                LVSeleccionDistribuidora.DataBind();

                ListView LVCarrito = PanelCentral.Controls[0].Controls[27].FindControl("LVCarrito") as ListView;
                Label lblCostoDeEnvio = PanelCentral.Controls[0].Controls[27].FindControl("lblCostoDeEnvio") as Label;
                Label lblCantidadProductos = PanelCentral.Controls[0].Controls[27].FindControl("lblCantidadProductos") as Label;
                Label lblTotalDeProductos = PanelCentral.Controls[0].Controls[27].FindControl("lblTotalDeProductos") as Label;
                Label lblTotal = PanelCentral.Controls[0].Controls[27].FindControl("lblTotal") as Label;
                LVCarrito.DataSource = MVProducto.ListaDelInformacionSucursales;
                LVCarrito.DataBind();

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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#ModalDistribuidores').modal('hide');", true);
            }
        }

        

        protected void LVSeleccionDistribuidora_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                LinkButton Empresa = e.Item.FindControl("btnEmpresaSeleccionada") as LinkButton;

                if (MVProducto.ListaDelInformacionSucursales.Exists(s => s.UidTarifario.ToString() == LVSeleccionDistribuidora.DataKeys[e.Item.DisplayIndex].Value.ToString()))
                {
                    Empresa.Attributes.Add("class", "btn btn-sm btn-danger");
                }
                else
                {
                    Empresa.Attributes.Add("class", "btn btn-sm btn-success");
                }
            }
        }
        #endregion

        protected void AgregarAlcarrito(Guid UidProducto, Guid UidSucursal, Guid UidSeccion, string StrCantidad, string StrNotas = "")
        {
            var Producto = MVProducto.ListaDelCarrito.FindAll(Objeto => Objeto.UID == UidProducto && Objeto.UidNota == Guid.Empty && Objeto.UidSucursal == UidSucursal);

            if (Producto.Count <= 1 && !string.IsNullOrEmpty(StrNotas) || (Producto.Count == 0 && string.IsNullOrEmpty(StrNotas)))
            {
                //Si solo existe un tarifario en la lista, se muestra al usuario en los datos de la sucursal
                if (MVTarifario.ListaDeTarifarios.Count > 0)
                {
                    Guid UidTarifario = new Guid();
                    decimal DmPrecio = 0.0m;
                    for (int i = 0; i < 1; i++)
                    {
                        UidTarifario = MVTarifario.ListaDeTarifarios[i].UidTarifario;
                        DmPrecio = MVTarifario.ListaDeTarifarios[i].DPrecio;
                    }
                    MVProducto.AgregaAlCarrito(UidProducto, UidSucursal, UidSeccion, StrCantidad, DmPrecio, UidTarifario, strNota: StrNotas);

                } // los datos de la informacion del tarifario se muestran vacios en caso de existir varios registros para esta orden.
                else
                {
                    MVProducto.AgregaAlCarrito(UidProducto, UidSucursal, UidSeccion, StrCantidad, 0.0m, Guid.Empty, strNota: StrNotas);
                }

                decimal Subtotal = 0.0m;
                decimal Envio = 0.0m;
                int cantidad = 0;
                foreach (var producto in MVProducto.ListaDelCarrito)
                {
                    cantidad = cantidad + producto.Cantidad;
                    Subtotal = Subtotal + decimal.Parse(producto.StrCosto);
                }
                foreach (var obj in MVProducto.ListaDelInformacionSucursales)
                {
                    Envio = Envio + obj.CostoEnvio;
                }
                lblProductosEnCarrito.Text = cantidad.ToString();

                //Modal de la pagina Empresas.aspx
                if (!string.IsNullOrEmpty(lblUidProductoSeleccionado.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "a", "$('#ModalProducto').modal('hide');", true);
                    DataList DLProductos = PanelCentral.Controls[0].Controls[27].FindControl("DLProductos") as DataList;
                    DLProductos.DataSource = MVProducto.ListaDeProductos;
                    DLProductos.DataBind();
                }
                //Modal de la pagina Default.aspx
                if (!string.IsNullOrEmpty(HFUidProducto.Value))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "b", "$('#myModal').modal('hide');", true);
                    DataList DLProductos = PanelCentral.Controls[0].Controls[27].FindControl("DLProductos") as DataList;
                    DLProductos.DataSource = MVProducto.ListaDeProductos;
                    DLProductos.DataBind();
                }
                MuestraDetallesDeLaOrdenGeneral();
            }
            else
            {
                //Modal de la pagina Default.aspx
                if (!string.IsNullOrEmpty(lblUidProductoSeleccionado.Text))
                {
                    pnlMensajeProductoSeleccionado.Visible = true;
                    lblMensajeProductoSeleccionado.Text = "Para agregar mas veces este producto de la sucursal seleccionada, gestione los que ya estan dentro del carrito";
                }
                //Modal de la pagina Empresas.aspx
                if (!string.IsNullOrEmpty(HFUidProducto.Value))
                {
                    pnlMensajeProducto.Visible = true;
                    lblMensaje.Text = "Para agregar mas veces este producto  de la sucursal seleccionada, gestione los que ya estan dentro del carrito";
                }
            }
        }

        protected void btnCarrito_Click(object sender, EventArgs e)
        {
            Response.Redirect("Carrito.aspx");
        }

        protected void btnAceptarEliminarProducto_Click(object sender, EventArgs e)
        {

            DataList DLCarrito = PanelCentral.Controls[0].Controls[27].FindControl("DLCarrito") as DataList;
            Guid UidProducto = new Guid(HFUidProductoAEliminar.Value);
            Guid UidSucursal = new Guid(HFUidSucursalProductoAEliminar.Value);
            MVProducto.EliminaProductoDelCarrito(UidProducto);

            DLCarrito.DataSource = MVProducto.ListaDelInformacionSucursales;
            DLCarrito.DataBind();
            DataList detalles = new DataList();
            foreach (DataListItem item in DLCarrito.Items)
            {
                if (new Guid(DLCarrito.DataKeys[item.ItemIndex].ToString()) == UidSucursal)
                {
                    detalles = item.FindControl("DLDetallesCompra") as DataList;
                }
            }
            var listaDetalle = MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == UidSucursal).ToList();
            (PanelCentral.Page as Carrito).MuestraDetallesDeLaOrdenGeneral();
            detalles.DataSource = listaDetalle;
            detalles.DataBind();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ModalEliminarProductoCarrito').modal('hide');</script>", false);
        }
    }
}