using Subgurim.Controles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;

namespace WebApplication1.Vista
{
    public partial class Sucursales : System.Web.UI.Page
    {
        #region Propiedades
        VMSucursales MVSucursales = new VMSucursales();
        VMDireccion MVDireccion = new VMDireccion();
        VMGiro MVGiro = new VMGiro();
        VMCategoria MVCategoria = new VMCategoria();
        VMSubCategoria MVSubcategoria = new VMSubCategoria();
        VMUbicacion MVUbicacion = new VMUbicacion();
        VMLicencia MVLicencia = new VMLicencia();
        VMEstatus MVEstatus = new VMEstatus();
        VMEmpresas MVEmpresa = new VMEmpresas();
        VMContrato MVContrato = new VMContrato();
        VMTelefono MVTelefono = new VMTelefono();
        VMMensaje MVMensaje = new VMMensaje();
        VMTarifario MVTarifario = new VMTarifario();
        string AccionesDeLaPagina = "";

        //Variables globales del control sobe los mapas 
        GMapType.GTypes TipoMapa = GMapType.GTypes.Normal;
        GMarkerOptions MarketOPciones = new GMarkerOptions();
        GMarker Marcador = new GMarker();
        GInfoWindow ventana = new GInfoWindow();
        string PlantillaMensajeVentana = string.Empty;
        int Zoom = 0;
        double DbLatitud, DbLongitud = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            //Inicio de listas
            //MVSucursales.Estatus();
            MVSucursales.TipoDeTelefonos();
            //new GLatLng(20.634701674933097, -87.07286689780835)
            MapaPrueba.Language = "es";
            MapaPrueba.GZoom = 17;
            MapaPrueba.mapType = TipoMapa;
            MapaPrueba.enableRotation = true;
            MarketOPciones.draggable = true;
            Marcador.options = MarketOPciones;
            MapaPrueba.Add(ventana);
            MapaPrueba.Add(new GMapUI());
            MapaPrueba.resetInfoWindows();
            if (Session["UidEmpresaSistema"] == null)
            {
                Response.Redirect("Default/");
            }
            else
            if (Session["UidEmpresaSistema"] == null && Session["Accion"] == null)
            {
                Response.Redirect("Default/");
            }
            else
            {
                if (!IsPostBack)
                {
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + DGVTarifario.ClientID + "', 400, 950 , 40 ,true); </script>", false);
                    MVMensaje.ListaDeMensajes = new List<VMMensaje>();

                    Session.Remove("Accion");
                    AccionesDeLaPagina = string.Empty;
                    Session["MVSucursales"] = MVSucursales;
                    Session["MVDireccion"] = MVDireccion;
                    Session["MVGiro"] = MVGiro;
                    Session["MVCategoria"] = MVCategoria;
                    Session["MVSubcategoria"] = MVSubcategoria;
                    Session["MVUbicacion"] = MVUbicacion;
                    Session["MVLicencia"] = MVLicencia;
                    Session["MVEstatus"] = MVEstatus;
                    Session["MVEmpresa"] = MVEmpresa;
                    Session["MVContrato"] = MVContrato;
                    Session["MVTarifario"] = MVTarifario;
                    //Sesiones de la ubicacion
                    Session["TipoMapa"] = TipoMapa;
                    Session["MarketOPciones"] = MarketOPciones;
                    Session["Marcador"] = Marcador;
                    Session["PlantillaMensajeVentana"] = PlantillaMensajeVentana;
                    Session["Zoom"] = Zoom;
                    Session["DbLatitud"] = DbLatitud;
                    Session["DbLongitud"] = DbLongitud;
                    Session["MVTelefono"] = MVTelefono;
                    Session["MVMensaje"] = MVMensaje;

                    #region Panel derecho
                    #region Paneles

                    //Visualiza el panel 
                    MuestraPanel("General");
                    //Botones
                    btnEditar.Enabled = false;


                    btnGuardar.Visible = false;
                    btnCancelar.Visible = false;
                    btnGuardarTelefono.Visible = false;
                    btnCancelarTelefono.Visible = false;
                    btnEditarTelefono.Enabled = false;

                    //Placeholders
                    txtDManzana.Attributes.Add("placeholder", "Manzana");
                    txtCalle1.Attributes.Add("placeholder", "Calle");
                    txtCalle2.Attributes.Add("placeholder", "Calle");
                    txtIdetificador.Attributes.Add("placeholder", "Identificador");
                    txtDReferencia.Attributes.Add("placeholder", "Referencia");
                    txtDTelefono.Attributes.Add("palceholder", "Telefono");

                    //GridView telefonos
                    DGVTELEFONOS.DataSource = null;
                    DGVTELEFONOS.DataBind();

                    //Deshabilita los controles de los datos generales
                    EstatusControlesPanel(false);
                    //Deshabilita los controles de la zona de servicio
                    EstatusControlesZonaDeServicio(false, "Entrega");
                    EstatusControlesZonaDeServicio(false, "Recolecta");
                    #endregion
                    #region DropdownList


                    DDLDTipoDETelefono.DataSource = MVSucursales.TIPOTELEFONO;
                    DDLDTipoDETelefono.DataValueField = "ID";
                    DDLDTipoDETelefono.DataTextField = "NOMBRE";
                    DDLDTipoDETelefono.DataBind();

                    #endregion
                    #region Limites
                    txtIdetificador.MaxLength = 50;
                    txtCalle1.MaxLength = 100;
                    txtCalle2.MaxLength = 100;
                    txtDManzana.MaxLength = 4;
                    txtDLote.MaxLength = 8;
                    txtDReferencia.MaxLength = 500;
                    #endregion

                    #region Zona de servicio

                    //Obtiene el pais para el dropdownlist
                    DDLZonaPais.DataSource = MVDireccion.Paises();
                    DDLZonaPais.DataTextField = "Nombre";
                    DDLZonaPais.DataValueField = "UidPais";
                    DDLZonaPais.DataBind();
                    //Comienza en vacio el GridView de las ciudades
                    DGVZonaCiudades.DataSource = null;
                    DGVZonaCiudades.DataBind();
                    //Limpia la lista de checkbox
                    DeseleccionaCheckboxListColoniasEntrega();
                    DeseleccionaCheckboxListColoniasRecolecta();
                    #endregion

                    #region Zona de recolecta
                    DDLZRPais.DataSource = MVDireccion.Paises();
                    DDLZRPais.DataTextField = "Nombre";
                    DDLZRPais.DataValueField = "UidPais";
                    DDLZRPais.DataBind();
                    #endregion

                    #region Contrato

                    //DropdownList 
                    MVEstatus.ObtnenerEstatusDeContrato();
                    ddlCEstatus.DataSource = MVEstatus.ListaEstatus;
                    ddlCEstatus.DataValueField = "UidEstatus";
                    ddlCEstatus.DataTextField = "NOMBRE";
                    ddlCEstatus.DataBind();
                    PanelDeInformacion.Visible = false;
                    #endregion

                    #region Telefono
                    EstatusControlesTelefono(false);
                    #endregion

                    //Creacion de menus dinamicos
                    CreaMenuSegunEmpresa(Session["UidEmpresaSistema"].ToString());
                    #endregion

                    #region Panel izquierdo
                    #region Filtros
                    //Panel de filtros          
                    PnlFiltros.Visible = false;
                    lblVisibilidadfiltros.Text = " Mostrar";
                    //Placeholders del panel de filtros
                    txtFIdentificador.Attributes.Add("placeholder", "Identificador");
                    txtDLote.Attributes.Add("placeholder", "Lote");
                    txtCalle0.Attributes.Add("placeholder", "Calle");
                    txtCalle1.Attributes.Add("placeholder", "Calle");
                    txtCalle2.Attributes.Add("placeholder", "Calle");
                    txtDCodigoPostal.Attributes.Add("placeholder", "Codigo Postal");
                    txtDTelefono.Attributes.Add("placeholder", "Telefono");
                    txtDReferencia.Attributes.Add("placeholder", "Referencia");
                    //Botones
                    btnBuscar.Enabled = false;
                    btnBorrarFiltros.Enabled = false;
                    //Busqueda ampliada
                    PanelBusquedaAmpliada.Visible = false;
                    DGVBUSQUEDAAMPLIADA.DataSource = null;
                    DGVBUSQUEDAAMPLIADA.DataBind();
                    #endregion
                    #region DropDownList

                    //Alimenta dropdownlist del pais en busqueda avanzada.
                    DDLDBAPAIS.DataSource = MVDireccion.Paises();
                    DDLDBAPAIS.DataTextField = "Nombre";
                    DDLDBAPAIS.DataValueField = "UidPais";
                    DDLDBAPAIS.DataBind();

                    #endregion
                    #region GridView empresa simple
                    MVSucursales.DatosGridViewBusquedaNormal(Session["UidEmpresaSistema"].ToString());
                    cargaGrid("Normal");
                    #endregion
                    #endregion

                    #region Panel de direccion
                    txtIdentificadorDeDireccion.Attributes.Add("placeholder", "Identificador");
                    txtIdentificadorDeDireccion.Text = "Predeterminado";

                    #region DropdownList

                    //Obtiene el pais
                    DDLDPais.DataSource = MVDireccion.Paises();
                    DDLDPais.DataTextField = "Nombre";
                    DDLDPais.DataValueField = "UidPais";
                    DDLDPais.DataBind();

                    MuestraEstados("00000000-0000-0000-0000-000000000000", "Gestion");
                    MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Gestion");
                    MuestraCiudad("00000000-0000-0000-0000-000000000000", "Gestion");
                    MuestraColonia("00000000-0000-0000-0000-000000000000", "Gestion");

                    #endregion
                    #endregion

                    #region Busqueda ampliada

                    #region Filtros de busqueda ampliada
                    lblBAFiltrosVisibilidad.Text = " Mostrar";
                    PanelFiltrosBusquedaAmpliada.Visible = false;

                    #endregion

                    #region DropdownList

                    MuestraEstados("00000000-0000-0000-0000-000000000000", "Filtro");
                    MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Filtro");
                    MuestraCiudad("00000000-0000-0000-0000-000000000000", "Filtro");
                    MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");
                    #endregion

                    #region Placeholders
                    txtBAIdentificador.Attributes.Add("placeholder", "Identificador");
                    txtBACalle.Attributes.Add("placeholder", "Calle");
                    txtBACOdigoPostal.Attributes.Add("placeholder", "Codigo Postal");

                    #endregion

                    #region Gridview Busqueda ampliada

                    #endregion

                    #endregion

                    #region Licencia
                    ddlEstatusSucursal.DataSource = MVEstatus.ObtenerListaActiva();
                    ddlEstatusSucursal.DataTextField = "Nombre";
                    ddlEstatusSucursal.DataValueField = "IdEstatus";
                    ddlEstatusSucursal.DataBind();

                    #endregion

                    PanelMensaje.Visible = false;
                    MVGiro.ListaDeGiroConimagen();
                    DLGiro.DataSource = MVGiro.LISTADEGIRO;
                    DLGiro.DataBind();
                    //Muestra el tooltip de cada registro del giro
                    foreach (DataListItem item in DLGiro.Items)
                    {
                        Label ObjectoLabel = (Label)item.FindControl("lblDescripcion");
                        if (ObjectoLabel != null)
                        {
                            foreach (var Giro in MVGiro.LISTADEGIRO)
                            {
                                if (DLGiro.DataKeys[item.ItemIndex].ToString() == Giro.UIDVM.ToString())
                                {
                                    ObjectoLabel.ToolTip = Giro.STRDESCRIPCION;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MVEmpresa = (VMEmpresas)Session["MVEmpresa"];
                    MVSucursales = (VMSucursales)Session["MVSucursales"];
                    MVLicencia = (VMLicencia)Session["MVLicencia"];
                    MVDireccion = (VMDireccion)Session["MVDireccion"];
                    MVGiro = (VMGiro)Session["MVGiro"];
                    MVCategoria = (VMCategoria)Session["MVCategoria"];
                    MVSubcategoria = (VMSubCategoria)Session["MVSubcategoria"];
                    MVUbicacion = (VMUbicacion)Session["MVUbicacion"];
                    //Sesiones de la ubicacion
                    TipoMapa = (GMapType.GTypes)Session["TipoMapa"];
                    MarketOPciones = (GMarkerOptions)Session["MarketOPciones"];
                    Marcador = (GMarker)Session["Marcador"];
                    PlantillaMensajeVentana = (string)Session["PlantillaMensajeVentana"];
                    Zoom = (int)Session["Zoom"];
                    MVEstatus = (VMEstatus)Session["MVEstatus"];
                    MVContrato = (VMContrato)Session["MVContrato"];
                    MVTelefono = (VMTelefono)Session["MVTelefono"];
                    MVMensaje = (VMMensaje)Session["MVMensaje"];
                    MVTarifario = (VMTarifario)Session["MVTarifario"];

                    if (PanelTarifario.Visible == true)
                    {
                        CrearGridViewTarifario(MVTarifario.ListaDeTarifarios);
                    }
                }
            }
        }

        #region Panel izquierdo
        #region Filtros

        protected void MostrarYOcultarFiltrosBusquedaNormal(object sender, EventArgs e)
        {
            VisivilidadDeFiltros("Normal");
        }

        protected void MostrarYOcultarFiltrosBusquedaAvanzada(object sender, EventArgs e)
        {
            VisivilidadDeFiltros("Ampliada");
        }

        protected void VisivilidadDeFiltros(string Filtro)
        {
            switch (Filtro)
            {
                case "Normal":

                    if (lblVisibilidadfiltros.Text == " Mostrar")
                    {
                        lblVisibilidadfiltros.Text = " Ocultar";
                        btnBuscar.Enabled = true;
                        PnlFiltros.Visible = true;
                        btnBorrarFiltros.Enabled = true;
                        btnBuscar.CssClass = "btn btn-sm btn-default";
                        btnBorrarFiltros.CssClass = "btn btn-sm btn-default";

                    }
                    else if (lblVisibilidadfiltros.Text == " Ocultar")
                    {
                        lblVisibilidadfiltros.Text = " Mostrar";
                        btnBuscar.Enabled = false;
                        btnBuscar.CssClass = "btn btn-sm btn-default disabled";
                        btnBorrarFiltros.CssClass = "btn btn-sm btn-default disabled";
                        PnlFiltros.Visible = false;
                        btnBorrarFiltros.Enabled = false;
                    }

                    break;

                case "Ampliada":

                    if (lblBAFiltrosVisibilidad.Text == " Mostrar")
                    {
                        lblBAFiltrosVisibilidad.Text = " Ocultar";

                        PanelFiltrosBusquedaAmpliada.Visible = true;

                        BtnBABuscar.Enabled = true;
                        BtnBALimpiar.Enabled = true;

                        BtnBABuscar.CssClass = "btn btn-sm btn-default";
                        BtnBALimpiar.CssClass = "btn btn-sm btn-default";

                    }
                    else if (lblBAFiltrosVisibilidad.Text == " Ocultar")
                    {
                        //Muestra texto en el boton de ocultar en la busqueda ampliada.
                        lblBAFiltrosVisibilidad.Text = " Mostrar";

                        PanelFiltrosBusquedaAmpliada.Visible = false;

                        BtnBABuscar.Enabled = false;
                        BtnBALimpiar.Enabled = false;

                        BtnBABuscar.CssClass = "btn btn-sm btn-default disabled";
                        BtnBALimpiar.CssClass = "btn btn-sm btn-default disabled";

                    }

                    break;

                default:
                    break;
            }

        }

        protected void VaciarFiltros(object sender, EventArgs e)
        {
            LimpiaFiltros();
        }

        protected void LimpiaFiltros()
        {
            txtFIdentificador.Text = string.Empty;
            txtFHoraApertura.Text = string.Empty;
            txtFHoraCierre.Text = string.Empty;

        }

        #endregion
        #region GridView

        #region Busqueda Normal
        protected void GVWEmpresaNormal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                GridViewRow PagerRow = DGVEMPRESAS.TopPagerRow;
                Session["MVSucursales"] = MVSucursales;
                Label Registros = PagerRow.Cells[0].FindControl("lblTotalDeRegistros") as Label;
                ImageButton DobleDerecho = PagerRow.Cells[0].FindControl("btnUltimo") as ImageButton;
                ImageButton DobleIzquierdo = PagerRow.Cells[0].FindControl("btnPrimero") as ImageButton;
                ImageButton Izquierda = PagerRow.Cells[0].FindControl("btnAnterior") as ImageButton;
                ImageButton Derecha = PagerRow.Cells[0].FindControl("btnSiguiente") as ImageButton;
                DropDownList PaginasBusquedaNormal = PagerRow.Cells[0].FindControl("DDLDNUMERODEPAGINAS") as DropDownList;

                int PaginaActual = DGVEMPRESAS.PageIndex + 1;
                int Total = DGVEMPRESAS.PageCount;

                if (Registros != null)
                {
                    //Limpia la lista de elementos asociada al dropdownlist de la paginacion de la busqueda normal.
                    PaginasBusquedaNormal.Items.Clear();
                    for (int i = 0; i < Total; i++)
                    {
                        int Pagina = i + 1;
                        ListItem item = new ListItem(Pagina.ToString());
                        if (i == DGVEMPRESAS.PageIndex)
                        {
                            item.Selected = true;
                        }
                        PaginasBusquedaNormal.Items.Add(item);
                    }

                    if (PaginaActual == 1 && PaginaActual != Total)
                    {
                        DobleIzquierdo.Enabled = false;
                        Izquierda.Enabled = false;
                        DobleIzquierdo.Visible = true;
                        Izquierda.Visible = true;
                        DobleIzquierdo.CssClass = "disabled";
                        Izquierda.CssClass = "disabled";

                        DobleDerecho.Enabled = true;
                        Derecha.Enabled = true;
                        DobleDerecho.CssClass = " ";
                        Derecha.CssClass = " ";
                    }
                    else if (PaginaActual != 1 && PaginaActual == Total)
                    {
                        DobleIzquierdo.Enabled = true;
                        Izquierda.Enabled = true;
                        DobleIzquierdo.Visible = true;
                        Izquierda.Visible = true;
                        DobleIzquierdo.CssClass = " ";
                        Izquierda.CssClass = " ";

                        DobleDerecho.Enabled = false;
                        Derecha.Enabled = false;
                        DobleDerecho.CssClass = "disabled";
                        Derecha.CssClass = "disabled";
                        DobleDerecho.Visible = true;
                        Derecha.Visible = true;
                    }
                    else if (DGVEMPRESAS.PageSize >= MVSucursales.LISTADESUCURSALES.Count)
                    {
                        DobleIzquierdo.Enabled = false;
                        Izquierda.Enabled = false;
                        DobleIzquierdo.Visible = false;
                        Izquierda.Visible = false;
                        DobleIzquierdo.CssClass = "disabled";
                        Izquierda.CssClass = "disabled";

                        DobleDerecho.Enabled = false;
                        Derecha.Enabled = false;
                        DobleDerecho.Visible = false;
                        Derecha.Visible = false;
                        DobleDerecho.CssClass = "disabled";
                        Derecha.CssClass = "disabled";

                    }
                    else
                    {
                        DobleIzquierdo.Enabled = true;
                        Izquierda.Enabled = true;
                        DobleIzquierdo.Visible = true;
                        Izquierda.Visible = true;
                        DobleIzquierdo.CssClass = " ";
                        Izquierda.CssClass = " ";

                        DobleDerecho.Enabled = true;
                        Derecha.Enabled = true;
                        DobleDerecho.Visible = true;
                        Derecha.Visible = true;
                        DobleDerecho.CssClass = " ";
                        Derecha.CssClass = " ";
                    }

                    int RegistroFinal = (DGVEMPRESAS.PageIndex + 1) * (DGVEMPRESAS.Rows.Count + 1);
                    int RegistroInicial = (((DGVEMPRESAS.PageIndex + 1) * DGVEMPRESAS.PageSize) - DGVEMPRESAS.PageSize) + 1;
                    string CantidadDeRegistros = MVSucursales.LISTADESUCURSALES.Count.ToString();


                    Registros.Text = RegistroInicial + " - " + RegistroFinal + " de " + CantidadDeRegistros;
                }
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVEMPRESAS, "Select$" + e.Row.RowIndex);


                //var icono = e.Row.FindControl("lblTipo") as Label;
                //icono.Attributes.Add("data-placement", "bottom");
                //icono.Attributes.Add("data-toggle", "tooltip");
                //if (e.Row.Cells[6].Text == "Suministradora")
                //{
                //    icono.CssClass = "glyphicon glyphicon-cutlery";
                //    icono.ToolTip = "Suministradora";

                //}
                //if (e.Row.Cells[6].Text == "Distribuidora")
                //{
                //    icono.CssClass = "glyphicon glyphicon-send";
                //    icono.ToolTip = "Distribuidora";
                //}

                //Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                //ESTATUS.Attributes.Add("data-placement", "bottom");
                //ESTATUS.Attributes.Add("data-toggle", "tooltip");
                //if (e.Row.Cells[4].Text == "ACTIVO")
                //{
                //    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                //    ESTATUS.ToolTip = "ACTIVO";
                //}
                //if (e.Row.Cells[4].Text == "INACTIVO")
                //{
                //    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                //    ESTATUS.ToolTip = "INACTIVO";
                //}
            }

        }

        protected void GridViewBusquedaNormal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGVEMPRESAS.PageIndex = e.NewPageIndex;
            cargaGrid("Normal");
        }


        protected void GVWEmpresaBusquedaNormal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Paneles"] != null)
            {
                Session.Remove("Paneles");
            }

            AccionesDeLaPagina = string.Empty;
            Session.Remove("Accion");
            textboxActivados();

            string valor = DGVEMPRESAS.SelectedDataKey.Value.ToString();

            btnEditar.CssClass = "btn btn-sm btn-default";
            btnEditar.Enabled = true;

            PanelBusquedaAmpliada.Visible = false;
            PanelDerecho.Visible = true;
            PanelIzquierdo.Visible = true;

            MuestraSucursalEnGestion(valor);
        }

        protected void DGVEMPRESASNORMAL_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            Session["sortExpression"] = sortExpression;

            if (Session["SortDirection"] != null && Session["SortDirection"].ToString() == SortDirection.Descending.ToString())
            {
                Session["SortDirection"] = SortDirection.Ascending;
                Sort(sortExpression, "ASC");
            }
            else
            {
                Session["SortDirection"] = SortDirection.Descending;
                Sort(sortExpression, "DESC");
            }
        }

        private void Sort(string sortExpression, string Valor)
        {
            DGVEMPRESAS.DataSource = MVSucursales.Sort(sortExpression, Valor, "Normal", Session["UidEmpresaSistema"].ToString());
            DGVEMPRESAS.DataBind();
        }

        protected void DGVEMPRESAS_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            GridViewRow pagerRow = (GridViewRow)gv.TopPagerRow;

            if (pagerRow != null && pagerRow.Visible == false)
                pagerRow.Visible = true;
        }

        #endregion

        protected void GVWEmpresaAmpliada_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Session["MVSucursales"] = MVSucursales;
                Label NumeroDePagina = new Label();
                Label TotalDePaginas = new Label();


                GridViewRow PagerRow = DGVBUSQUEDAAMPLIADA.TopPagerRow;

                Label Registros = PagerRow.Cells[0].FindControl("lblTotalDeRegistros") as Label;
                ImageButton DobleDerecho = PagerRow.Cells[0].FindControl("btnUltimo") as ImageButton;
                ImageButton DobleIzquierdo = PagerRow.Cells[0].FindControl("btnPrimero") as ImageButton;
                ImageButton Izquierda = PagerRow.Cells[0].FindControl("btnAnterior") as ImageButton;
                ImageButton Derecha = PagerRow.Cells[0].FindControl("btnSiguiente") as ImageButton;
                DropDownList Tamanio = PagerRow.Cells[0].FindControl("DDLTAMANOGRIDAMPLIADA") as DropDownList;
                DropDownList PaginasBusquedaAmpliada = PagerRow.Cells[0].FindControl("DDLDBANUMERODEPAGINAS") as DropDownList;

                if ((NumeroDePagina != null) && (TotalDePaginas != null))
                {
                    int PaginaActual = DGVBUSQUEDAAMPLIADA.PageIndex + 1;
                    int Total = DGVBUSQUEDAAMPLIADA.PageCount;


                    //Limpia la lista de elementos asociada al dropdownlist de la paginacion de la busqueda normal.
                    PaginasBusquedaAmpliada.Items.Clear();
                    for (int i = 0; i < Total; i++)
                    {
                        int Pagina = i + 1;
                        ListItem item = new ListItem(Pagina.ToString());
                        if (i == DGVBUSQUEDAAMPLIADA.PageIndex)
                        {
                            item.Selected = true;
                        }
                        PaginasBusquedaAmpliada.Items.Add(item);
                    }

                    foreach (ListItem item in Tamanio.Items)
                    {
                        if (Convert.ToInt32(item.Value) == DGVBUSQUEDAAMPLIADA.PageSize)
                        {
                            item.Selected = true;
                        }
                    }

                    if (PaginaActual == 1 && PaginaActual != Total)
                    {
                        DobleIzquierdo.Enabled = false;
                        Izquierda.Enabled = false;
                        DobleIzquierdo.Visible = true;
                        Izquierda.Visible = true;
                        DobleIzquierdo.CssClass = "disabled";
                        Izquierda.CssClass = "disabled";

                        DobleDerecho.Enabled = true;
                        Derecha.Enabled = true;
                        DobleDerecho.CssClass = " ";
                        Derecha.CssClass = " ";
                    }
                    else if (PaginaActual != 1 && PaginaActual == Total)
                    {
                        DobleIzquierdo.Enabled = true;
                        Izquierda.Enabled = true;
                        DobleIzquierdo.Visible = true;
                        Izquierda.Visible = true;
                        DobleIzquierdo.CssClass = " ";
                        Izquierda.CssClass = " ";

                        DobleDerecho.Enabled = false;
                        Derecha.Enabled = false;
                        DobleDerecho.CssClass = "disabled";
                        Derecha.CssClass = "disabled";
                        DobleDerecho.Visible = true;
                        Derecha.Visible = true;
                    }
                    else if (DGVBUSQUEDAAMPLIADA.PageSize >= MVSucursales.LISTADESUCURSALES.Count)
                    {
                        DobleIzquierdo.Enabled = false;
                        Izquierda.Enabled = false;
                        DobleIzquierdo.Visible = false;
                        Izquierda.Visible = false;
                        DobleIzquierdo.CssClass = "disabled";
                        Izquierda.CssClass = "disabled";

                        DobleDerecho.Enabled = false;
                        Derecha.Enabled = false;
                        DobleDerecho.Visible = false;
                        Derecha.Visible = false;
                        DobleDerecho.CssClass = "disabled";
                        Derecha.CssClass = "disabled";

                    }
                    else
                    {
                        DobleIzquierdo.Enabled = true;
                        Izquierda.Enabled = true;
                        DobleIzquierdo.Visible = true;
                        Izquierda.Visible = true;
                        DobleIzquierdo.CssClass = " ";
                        Izquierda.CssClass = " ";

                        DobleDerecho.Enabled = true;
                        Derecha.Enabled = true;
                        DobleDerecho.Visible = true;
                        Derecha.Visible = true;
                        DobleDerecho.CssClass = " ";
                        Derecha.CssClass = " ";
                    }

                    Tamanio.SelectedIndex = Tamanio.Items.IndexOf(Tamanio.Items.FindByValue(Tamanio.SelectedItem.Value.ToString()));
                    int RegistroFinal = (DGVBUSQUEDAAMPLIADA.PageIndex + 1) * (DGVBUSQUEDAAMPLIADA.Rows.Count + 1);
                    int RegistroInicial = (((DGVBUSQUEDAAMPLIADA.PageIndex + 1) * DGVBUSQUEDAAMPLIADA.PageSize) - DGVBUSQUEDAAMPLIADA.PageSize) + 1;
                    string CantidadDeRegistros = MVSucursales.LISTADESUCURSALES.Count.ToString();


                    Registros.Text = RegistroInicial + " - " + RegistroFinal + " de " + CantidadDeRegistros;


                    NumeroDePagina.Text = PaginaActual.ToString();
                    TotalDePaginas.Text = Total.ToString();
                }

                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVBUSQUEDAAMPLIADA, "Select$" + e.Row.RowIndex);
                //var icono = e.Row.FindControl("lblTipo") as Label;
                //if (e.Row.Cells[6].Text == "Suministradora")
                //{
                //    icono.CssClass = "glyphicon glyphicon-cutlery";
                //    icono.ToolTip = "Suministradora";

                //}
                //if (e.Row.Cells[6].Text == "Distribuidora")
                //{
                //    icono.CssClass = "glyphicon glyphicon-send";
                //    icono.ToolTip = "Distribuidora";
                //}

                //Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                //if (e.Row.Cells[4].Text == "ACTIVO")
                //{
                //    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                //    ESTATUS.ToolTip = "ACTIVO";
                //}
                //if (e.Row.Cells[4].Text == "INACTIVO")
                //{
                //    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                //    ESTATUS.ToolTip = "INACTIVO";
                //}


            }

        }
        protected void TamanioGrid(object sender, EventArgs e)
        {
            int cantidad = int.Parse(((DropDownList)sender).SelectedValue);

            DGVBUSQUEDAAMPLIADA.PageSize = cantidad;
            DGVBUSQUEDAAMPLIADA.DataSource = MVSucursales.LISTADESUCURSALES;
            DGVBUSQUEDAAMPLIADA.DataBind();


        }
        protected void PaginaSeleccionadaBusquedaAmpliada(object sender, EventArgs e)
        {
            int valor = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            DGVBUSQUEDAAMPLIADA.PageIndex = valor - 1;
            cargaGrid("Ampliada");
        }
        protected void PaginaSeleccionadaBusquedaNormal(object sender, EventArgs e)
        {
            int valor = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            DGVEMPRESAS.PageIndex = valor - 1;
            cargaGrid("Normal");
        }
        protected void DGVBUSQUEDAAMPLIADA_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            GridViewRow pagerRow = (GridViewRow)gv.TopPagerRow;

            if (pagerRow != null && pagerRow.Visible == false)
                pagerRow.Visible = true;
        }
        protected void cargaGrid(string Busqueda)
        {
            Session["MVSucursales"] = MVSucursales;
            switch (Busqueda)
            {
                case "Normal":

                    DGVEMPRESAS.DataSource = MVSucursales.LISTADESUCURSALES;
                    DGVEMPRESAS.DataBind();
                    break;
                case "Ampliada":

                    DGVBUSQUEDAAMPLIADA.DataSource = MVSucursales.LISTADESUCURSALES;
                    DGVBUSQUEDAAMPLIADA.DataBind();
                    break;
                case "Telefono":
                    DGVTELEFONOS.DataSource = MVTelefono.ListaDeTelefonos;
                    DGVTELEFONOS.DataBind();
                    break;
                case "Licencia":
                    DgvLicencia.DataSource = MVLicencia.ListaDeLicencias;
                    DgvLicencia.DataBind();
                    break;
                case "Empresas":
                    dgvBusquedaDeEmpresa.DataSource = MVSucursales.ListaDeSucursalesDeContrato;
                    dgvBusquedaDeEmpresa.DataBind();
                    break;
                case "Mensajes":
                    DgvMensajes.DataSource = MVMensaje.ListaDeMensajes;
                    DgvMensajes.DataBind();
                    break;
                default:
                    break;
            }
        }
        protected void GridViewBusquedaAmpliada_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGVBUSQUEDAAMPLIADA.PageIndex = e.NewPageIndex;
            cargaGrid("Ampliada");
        }
        protected void GVWEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Paneles"] != null)
            {
                Session.Remove("Paneles");
            }
            AccionesDeLaPagina = string.Empty;
            Session.Remove("Accion");
            textboxActivados();

            string valor = DGVEMPRESAS.SelectedDataKey.Value.ToString();

            btnEditar.CssClass = "btn btn-sm btn-default";
            btnEditar.Enabled = true;

            MuestraSucursalEnGestion(valor);
        }
        protected void GVBusquedaAvanzadaEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Paneles"] != null)
            {
                Session.Remove("Paneles");
            }
            AccionesDeLaPagina = string.Empty;
            textboxActivados();
            Session.Remove("Accion");

            string valor = DGVBUSQUEDAAMPLIADA.SelectedDataKey.Value.ToString();

            btnEditar.CssClass = "btn btn-sm btn-default";
            btnEditar.Enabled = true;


            PanelBusquedaAmpliada.Visible = false;
            PanelDerecho.Visible = true;
            PanelIzquierdo.Visible = true;

            MuestraSucursalEnGestion(valor);
        }

        protected void MuestraUbicacionEnMapa(Double latitud, double longitud, string mensajeVentana, int zoom)
        {
            MapaPrueba.setCenter(new GLatLng(latitud, longitud), zoom, TipoMapa);
            Marcador = new GMarker(new GLatLng(latitud, longitud));
            Marcador.options = MarketOPciones;
            Session["Marcador"] = Marcador;
            ventana = new GInfoWindow(Marcador, mensajeVentana, true);
            MapaPrueba.Add(ventana);

        }
        protected void MuestraSucursalEnGestion(string valor)
        {
            PanelMensaje.Visible = false;
            //Vacia la lista de elementos seleccionados 
            MVGiro.LISTADEGIROSELECCIONADO.Clear();
            MVCategoria.LISTADECATEGORIASELECIONADA.Clear();
            MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Clear();
            MVContrato.ListaDeSucursalesEnContrato.Clear();
            MVSucursales.ListaDeSucursalesDeContrato.Clear();
            MVTarifario.ListaDeTarifarios.Clear();
            MVTarifario.ListaDeTarifariosSeleccionados.Clear();
            //Recupera la informacion de la sucursal
            MVSucursales.ObtenerSucursal(valor);
            //recupera la informacion de los telefonos asociados a la sucursal
            MVTelefono.ObtenerTelefonosSucursal(valor, "gestion");
            //Recupera los giros asociados a la sucursal
            MVSucursales.RecuperaGiro(valor);
            //Recupera las categorias asociadas a la sucursal
            MVSucursales.RecuperaCategoria(valor);
            //Recupera las subcategorias asociadas a la sucursal
            MVSucursales.RecuperaSubcategoria(valor);
            //Recupera la ubicacion de la sucursal
            MVUbicacion.RecuperaUbicacionSucursal(valor);
            //Recuperar la licencia de la sucursal
            MVLicencia.ObtenerLicenciaSucursal(valor);
            //Recupera la relacion con la empresa distribuidora
            MVContrato.ObtenRelacionContrato(valor);

            //Recupera los mensajes de la sucursal
            MVMensaje.Buscar(new Guid(valor));
            cargaGrid("Empresas");
            cargaGrid("Telefono");
            cargaGrid("Mensajes");
            //Manda la licencia al gridview
            DgvLicencia.DataSource = MVLicencia.ListaDeLicencias;
            DgvLicencia.DataBind();


            //dropdownlist del estatus de la sucursal
            ddlEstatusSucursal.SelectedIndex = ddlEstatusSucursal.Items.IndexOf(ddlEstatusSucursal.Items.FindByValue(MVSucursales.Estatus.ToString()));
            ddlEstatusSucursal.DataBind();

            MapaPrueba.resetInfoWindows();
            double Latitud, Longitud;
            Random numero = new Random();

            if (string.IsNullOrEmpty(MVUbicacion.VchLatitud) && string.IsNullOrEmpty(MVUbicacion.VchLongitud))
            {
                Latitud = numero.Next(-90, 90);
                Longitud = numero.Next(-90, 90);
                Zoom = 17;
                PlantillaMensajeVentana = "<label>No existe la ubicación de esta sucursal(" + MVSucursales.IDENTIFICADOR + ")</label>\n<p>Elige una ubicacion para que tus clientes puedan verte!!</p>";
            }
            else
            {
                Latitud = Double.Parse(MVUbicacion.VchLatitud);
                Longitud = double.Parse(MVUbicacion.VchLongitud);
                Zoom = 14;
                PlantillaMensajeVentana = "<center>" + MVSucursales.IDENTIFICADOR + "</center>\n ";
            }


            Session["PlantillaMensajeVentana"] = PlantillaMensajeVentana;
            Session["Zoom"] = Zoom;
            MuestraUbicacionEnMapa(Latitud, Longitud, PlantillaMensajeVentana, Zoom);




            MuestraDireccion(valor);
            Session["MVSucursales"] = MVSucursales;
            txtNombreDeSucursal.Text = MVSucursales.SUCURSAL.IDENTIFICADOR;
            txtUidSucursal.Text = MVSucursales.SUCURSAL.ID.ToString();
            txtIdetificador.Text = MVSucursales.SUCURSAL.IDENTIFICADOR;
            txtDHoraApertura.Text = MVSucursales.SUCURSAL.HORAAPARTURA;
            txtDHoraCierre.Text = MVSucursales.SUCURSAL.HORACIERRE;
            txtClaveDeBusqueda.Text = MVSucursales.SUCURSAL.StrCodigo;
            chkVisibilidadInformacion.Checked = MVSucursales.SUCURSAL.BVisibilidad;
            PanelMensaje.Visible = false;


            #region Tarifario
            //Si es suministradora lo obtiene por el contrato
            if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString()))
            {
                foreach (DataListItem item in DLGiro.Items)
                {
                    CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkGiro");
                    ObjectoCheckBox.Checked = false;
                }

                foreach (DataListItem item in DLGiro.Items)
                {
                    CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkGiro");
                    if (ObjectoCheckBox != null)
                    {

                        foreach (var Giro in MVSucursales.ListaDeGiro)
                        {
                            if (DLGiro.DataKeys[item.ItemIndex].ToString() == Giro.UIDGIRO.ToString())
                            {
                                ObjectoCheckBox.Checked = true;
                            }
                        }

                    }
                }
                //Muestra los giros seleccionados 
                if (MVSucursales.ListaDeGiro.Count != 0)
                {
                    DLGiroSeleccionado.SelectedIndex = -1;

                    MVGiro.LISTADEGIROSELECCIONADO.Clear();

                    foreach (var item in MVSucursales.ListaDeGiro)
                    {
                        MVGiro.SeleccionDeGiro(item.UIDGIRO.ToString());
                    }

                    DLGiroSeleccionado.DataSource = MVGiro.LISTADEGIROSELECCIONADO;
                    DLGiroSeleccionado.DataBind();
                }
                else
                {
                    DLGiroSeleccionado.DataSource = null;
                    DLGiroSeleccionado.DataBind();
                }
                //Muestra las categorias seleccionadas
                if (MVSucursales.ListaDeCategorias.Count != 0)
                {
                    DlCategoriaSeleccionada.SelectedIndex = -1;

                    MVCategoria.LISTADECATEGORIASELECIONADA.Clear();

                    foreach (var item in MVSucursales.ListaDeCategorias)
                    {
                        MVCategoria.SeleccionarCategoria(item.UIDCATEGORIA.ToString());
                    }

                    DlCategoriaSeleccionada.DataSource = MVCategoria.LISTADECATEGORIASELECIONADA;
                    DlCategoriaSeleccionada.DataBind();
                }
                else
                {
                    DlCategoriaSeleccionada.DataSource = null;
                    DlCategoriaSeleccionada.DataBind();
                }
                //Muestra las subcategorias seleccionadas
                if (MVSucursales.ListaDeSubcategorias.Count != 0)
                {
                    MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Clear();

                    foreach (var item in MVSucursales.ListaDeSubcategorias)
                    {
                        MVSubcategoria.SeleccionarSubcategoria(item.UIDSUBCATEGORIA.ToString());
                    }
                }
                DLCategoria.DataSource = null;
                DLCategoria.DataBind();

                dlSubcategoria.DataSource = null;
                dlSubcategoria.DataBind();

                MVTarifario.ListaDeTarifariosSeleccionados.Clear();
                foreach (var item in MVContrato.ListaDeSucursalesEnContrato)
                {
                    MVTarifario.BuscarTarifario(TipoDeBusqueda: "Contrato", contrato: item.Uid.ToString());
                }
            }
            else //Si es por distribuidora solo por el uid trae los tarifarios
            {
                MVTarifario.BuscarTarifario("Gestion", uidSucursal: valor);
                CrearGridViewTarifario(MVTarifario.ListaDeTarifarios);
            }
            #endregion
        }

        #endregion
        #region Busqueda
        protected void BuscarEmpresasBusquedaNormal(object sender, EventArgs e)
        {
            string Identificador = txtFIdentificador.Text;
            string HA = txtFHoraApertura.Text;
            string HC = txtFHoraCierre.Text;

            if (txtFIdentificador.Text == string.Empty && txtFHoraApertura.Text == string.Empty && txtFHoraCierre.Text == string.Empty)
            {
                MVSucursales.DatosGridViewBusquedaNormal(Session["UidEmpresaSistema"].ToString());
                cargaGrid("Normal");
                lblVisibilidadfiltros.Text = " Mostrar";
                PnlFiltros.Visible = false;
                btnBuscar.Enabled = false;
                btnBorrarFiltros.Enabled = false;
                btnBuscar.CssClass = "btn btn-sm btn-default disabled";
                btnBorrarFiltros.CssClass = "btn btn-sm btn-default disabled";
            }
            else
            {
                MVSucursales.BuscarSucursales(Identificador, HA, HC, Uidempresa: Session["UidEmpresaSistema"].ToString());
                cargaGrid("Ampliada");
                cargaGrid("Normal");

                lblVisibilidadfiltros.Text = " Mostrar";
                PnlFiltros.Visible = false;
                btnBuscar.Enabled = false;
                btnBorrarFiltros.Enabled = false;
                btnBuscar.CssClass = "btn btn-sm btn-default disabled";
                btnBorrarFiltros.CssClass = "btn btn-sm btn-default disabled";
            }
        }

        protected void BuscaEmpresasBusquedaAmpliada(object sender, EventArgs e)
        {

            string Identificador = txtBAIdentificador.Text;
            string HA = txtBaHorarioApertura.Text;
            string HC = txtBAHorarioCierre.Text;
            string Calle = txtBACalle.Text;

            Guid Pais = new Guid(DDLDBAPAIS.SelectedItem.Value.ToString());
            Guid Estado = new Guid(DDLDBAESTADO.SelectedItem.Value.ToString());
            Guid Municipio = new Guid(DDLDBAMUNICIPIO.SelectedItem.Value.ToString());
            Guid Ciudad = new Guid(DDLDBACIUDAD.SelectedItem.Value.ToString());
            Guid Colonia = new Guid(DDLDBACOLONIA.SelectedItem.Value.ToString());



            if (Identificador == string.Empty && HA == string.Empty && HC == string.Empty && Calle == string.Empty && Pais == new Guid("00000000-0000-0000-0000-000000000000") && Estado == new Guid("00000000-0000-0000-0000-000000000000") && Municipio == new Guid("00000000-0000-0000-0000-000000000000") && Ciudad == new Guid("00000000-0000-0000-0000-000000000000") && Colonia == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                MVSucursales.DatosGridViewBusquedaAmpliada(Session["UidEmpresaSistema"].ToString());
                cargaGrid("Ampliada");
                lblBAFiltrosVisibilidad.Text = " Mostrar";
                PanelFiltrosBusquedaAmpliada.Visible = false;
                BtnBABuscar.Enabled = false;
                BtnBALimpiar.Enabled = false;
                BtnBABuscar.CssClass = "btn btn-sm btn-default disabled";
                BtnBALimpiar.CssClass = "btn btn-sm btn-default disabled";

            }
            else
            {
                MVSucursales.BuscarSucursales(Identificador, HA, HC, Colonia, Uidempresa: Session["UidEmpresaSistema"].ToString());
                cargaGrid("Ampliada");
                cargaGrid("Normal");

                lblBAFiltrosVisibilidad.Text = " Mostrar";
                PanelFiltrosBusquedaAmpliada.Visible = false;
                BtnBABuscar.Enabled = false;
                BtnBALimpiar.Enabled = false;
                BtnBABuscar.CssClass = "btn btn-sm btn-default disabled";
                BtnBALimpiar.CssClass = "btn btn-sm btn-default disabled";
            }
        }
        #endregion
        #endregion

        #region Panel derecho

        #region Activar cajas de texto
        protected void ActivarCajasDeTexto(object sender, EventArgs e)
        {
            //Activar la sesion de panel y remover sesion de edicion
            Session["Paneles"] = "Activados";
            AccionesDeLaPagina = "NuevoRegistro";
            Session["Accion"] = AccionesDeLaPagina;
            txtUidSucursal.Text = string.Empty;
            txtUidSucursal.Text = Guid.NewGuid().ToString();
            MVLicencia.ListaDeLicencias.Clear();
            MVLicencia.AgregaLicenciaALista(new Guid(txtUidSucursal.Text), Guid.NewGuid(), "");
            //Cambia el texto del boton para guardar
            lblGuardarDatos.CssClass = "glyphicon glyphicon-ok";
            //Limpia las cajas de texto del panel de gestion de datos
            LimpiarCajasDeTexto();
            QuitarColorACamposObligatorios();
            //Activa las cajas de texto del panel de gestion de datos
            textboxActivados(ControlDeACcion: "Activado");

            //Deshabilita boton de edicion
            btnEditar.CssClass = "btn btn-sm btn-default disabled";
            btnEditar.Enabled = false;
            DGVEMPRESAS.SelectedIndex = -1;
            DgvLicencia.SelectedIndex = -1;
            MVSucursales = new VMSucursales();
            Session["AD"] = MVSucursales;

            //Limpia los campos seleccionados
            MVDireccion.ListaCiudadesSeleccionadasEntrega.Clear();
            MVDireccion.ListaColoniasSeleccionadasEntrega.Clear();
            //Limpia el gridview de las ciudades seleccionadas
            DGVZonaCiudades.DataSource = null;
            DGVZonaCiudades.DataBind();
            //Limpia la lista de colonias
            chklColonias.DataSource = null;
            chklColonias.DataBind();
            //Limpia los telefonos 
            MVTelefono.ListaDeTelefonos = new List<VMTelefono>();
            //Limpia catalogos
            MVCategoria.LISTADECATEGORIASELECIONADA.Clear();
            MVGiro.LISTADEGIROSELECCIONADO.Clear();
            MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Clear();

            cargaGrid("Direccion");
            cargaGrid("Telefono");
            cargaGrid("Licencia");
        }
        protected void CancelarAgregacion(object sender, EventArgs e)
        {
            QuitarColorACamposObligatorios();
            DgvLicencia.SelectedIndex = -1;
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }

            if (Session["Paneles"] != null)
            {
                Session.Remove("Paneles");
                AccionesDeLaPagina = string.Empty;
                textboxActivados();
                BorrarCamposDeGestion();

                MVSucursales = new VMSucursales();
                Session["MVSucursales"] = MVSucursales;
                cargaGrid("Direccion");
                cargaGrid("Telefono");
                cargaGrid("Licencia");

                MVLicencia.ListaDeLicencias.Clear();
            }
            if (AccionesDeLaPagina == "Edicion")
            {
                AccionesDeLaPagina = string.Empty;
                //btnGuardar.Visible = false;
                //btnCancelar.Visible = false;
                //btnEditar.CssClass = "btn btn-sm btn-default";
                //btnEditar.Enabled = true;
                //btnEditarTelefono.Enabled = false;
                //btnEditarTelefono.CssClass = "btn btn-sm btn-default disabled";
                EstatusControlesPanel(false);
                DesactivarCajasDeTextoPanelGestion();
                MuestraSucursalEnGestion(txtUidSucursal.Text);
                MuestraZonaDeServicio(new Guid(txtUidSucursal.Text));
                btnNuevo.CssClass = "btn btn-sm btn-default";
                btnNuevo.Enabled = true;

                cargaGrid("Telefono");
                cargaGrid("Direccion");
                cargaGrid("Licencia");

            }
            PanelDeBusqueda.Visible = true;

            LimpiarCajasDeTexto();
            LimpiarCamposDeDireccion();
            DesactivarCajasDeTextoPanelGestion();
            Session.Remove("Accion");
            DGVEMPRESAS.SelectedIndex = -1;
            DGVTELEFONOS.SelectedIndex = -1;
            DGVZonaCiudades.SelectedIndex = -1;

            //Limpia las colonias 
            LimpiaListaDeColoniasEntrega();
            LimpiaListaDeColoniasRecolecta();
        }
        protected void BorrarCamposDeGestion()
        {
            btnEditar.Attributes.Add("CssClass", "btn btn-sm btn-default disabled");
            //Datos generales
            txtIdetificador.Text = string.Empty;
            LimpiarCamposDeDireccion();
        }
        #endregion

        #region Barra de navegacion del panel derecho

        protected void CreaMenuSegunEmpresa(string UidEmpresa)
        {

            //Suministradora
            if (MVEmpresa.ObtenerTipoDeEmpresa(UidEmpresa))
            {
                liDatosZonaDeServicio.Visible = false;
            }
            else
            //Distribuidora
            if (!MVEmpresa.ObtenerTipoDeEmpresa(UidEmpresa))
            {
                liDatosGiro.Visible = false;

                liDatosCategoria.Visible = false;

                liDatosSubcategoria.Visible = false;

            }
        }

        protected void PanelGeneral(object sender, EventArgs e)
        {
            //Visualiza el panel 
            MuestraPanel("General");
            ColoniasSeleccionadas();

            //Obtiene las categorias seleccionadas
            if (DLCategoria.Items.Count != 0)
            {
                ObtenerCategoriasSeleccionadas();
            }
            //Obtiene las subcategorias seleccionadas
            if (dlSubcategoria.Items.Count != 0)
            {
                ObtenerSubcategoriasSeleccionadas();
            }

        }
        protected void PanelDireccion(object sender, EventArgs e)
        {
            ColoniasSeleccionadas();

            //Visualiza el panel 
            MuestraPanel("Direccion");

            //Obtiene las categorias seleccionadas
            if (DLCategoria.Items.Count != 0)
            {
                ObtenerCategoriasSeleccionadas();
            }
            //Obtiene las subcategorias seleccionadas
            if (dlSubcategoria.Items.Count != 0)
            {
                ObtenerSubcategoriasSeleccionadas();
            }

        }
        protected void PanelContacto(object sender, EventArgs e)
        {
            //Visualiza el panel 
            MuestraPanel("Contacto");
            ColoniasSeleccionadas();

            //Obtiene las categorias seleccionadas
            if (DLCategoria.Items.Count != 0)
            {
                ObtenerCategoriasSeleccionadas();
            }
            //Obtiene las subcategorias seleccionadas
            if (dlSubcategoria.Items.Count != 0)
            {
                ObtenerSubcategoriasSeleccionadas();
            }
        }
        protected void PanelGiro(object sender, EventArgs e)
        {
            //Visualiza el panel 
            MuestraPanel("Giro");

            //Obtiene las categorias seleccionadas
            if (DLCategoria.Items.Count != 0)
            {
                ObtenerCategoriasSeleccionadas();
            }
            //Obtiene las subcategorias seleccionadas
            if (dlSubcategoria.Items.Count != 0)
            {
                ObtenerSubcategoriasSeleccionadas();
            }


        }
        protected void btnContrato_Click(object sender, EventArgs e)
        {
            MuestraPanel("Contrato");
            if (DLCategoria.Items.Count != 0)
            {
                ObtenerCategoriasSeleccionadas();
            }
            //Obtiene las subcategorias seleccionadas
            if (dlSubcategoria.Items.Count != 0)
            {
                ObtenerSubcategoriasSeleccionadas();
            }
        }
        protected void btnDatosUbicacion_Click(object sender, EventArgs e)
        {
            PanelDatosDireccion.Visible = false;
            PanelUbicacion.Visible = true;

            liInformacionDireccion.Attributes.Add("class", "");
            liDatosUbicacion.Attributes.Add("class", "active");

        }
        protected void btnGridCategoria_Click(object sender, EventArgs e)
        {
            //Visualiza el panel 
            MuestraPanel("Categoria");


            MVGiro.LISTADEGIROSELECCIONADO = new List<VMGiro>();

            if (DLGiro.Items.Count != 0)
            {
                ObtenerGirosSeleccionados();
            }
            else
            {
                foreach (var item in MVSucursales.ListaDeGiro)
                {
                    MVGiro.SeleccionDeGiro(item.UIDGIRO.ToString());
                }
            }

            if (DLCategoria.Items.Count != 0)
            {
                ObtenerCategoriasSeleccionadas();
            }
            else
            {
                foreach (var item in MVSucursales.ListaDeCategorias)
                {
                    if (!MVCategoria.LISTADECATEGORIASELECIONADA.Exists(Cat => Cat.UIDCATEGORIA == item.UIDCATEGORIA))
                    {
                        MVCategoria.SeleccionarCategoria(item.UIDCATEGORIA.ToString());
                    }
                }
            }

            if (dlSubcategoria.Items.Count != 0)
            {
                ObtenerSubcategoriasSeleccionadas();
            }
            else
            {
                foreach (var item in MVSucursales.ListaDeSubcategorias)
                {
                    if (!MVSucursales.ListaDeSubcategorias.Exists(Subcat => Subcat.UIDSUBCATEGORIA == item.UIDSUBCATEGORIA))
                    {
                        MVSubcategoria.SeleccionarSubcategoria(item.UIDSUBCATEGORIA.ToString());
                    }
                }
            }

            DLGiroSeleccionado.SelectedIndex = -1;
            DLGiroSeleccionado.DataSource = MVGiro.LISTADEGIROSELECCIONADO;
            DLGiroSeleccionado.DataBind();


            quitaCategoriasSeleccionadasSinGiro();
            QuitaSubcategoriaSeleccionadaSinCategoria();

            DLCategoria.DataSource = null;
            DLCategoria.DataBind();
        }
        protected void btnGridSubcategoria_Click(object sender, EventArgs e)
        {
            //Visualiza el panel 
            MuestraPanel("Subcategoria");

            if (DLCategoria.Items.Count != 0)
            {
                ObtenerCategoriasSeleccionadas();
            }
            else
            {
                foreach (var item in MVSucursales.ListaDeCategorias)
                {
                    if (!MVCategoria.LISTADECATEGORIASELECIONADA.Exists(Cat => Cat.UIDCATEGORIA == item.UIDCATEGORIA))
                    {
                        MVCategoria.SeleccionarCategoria(item.UIDCATEGORIA.ToString());
                    }
                }
            }

            if (dlSubcategoria.Items.Count != 0)
            {
                ObtenerSubcategoriasSeleccionadas();
            }
            else
            {
                foreach (var item in MVSucursales.ListaDeSubcategorias)
                {
                    if (!MVSucursales.ListaDeSubcategorias.Exists(Subcat => Subcat.UIDSUBCATEGORIA == item.UIDSUBCATEGORIA))
                    {
                        MVSubcategoria.SeleccionarSubcategoria(item.UIDSUBCATEGORIA.ToString());
                    }
                }
            }

            quitaCategoriasSeleccionadasSinGiro();
            QuitaSubcategoriaSeleccionadaSinCategoria();


            DlCategoriaSeleccionada.SelectedIndex = -1;
            DlCategoriaSeleccionada.DataSource = MVCategoria.LISTADECATEGORIASELECIONADA;
            DlCategoriaSeleccionada.DataBind();

            dlSubcategoria.DataSource = null;
            dlSubcategoria.DataBind();
        }

        protected void BtnInformacionDireccion_Click(object sender, EventArgs e)
        {
            ColoniasSeleccionadas();

            PanelDatosDireccion.Visible = true;
            PanelUbicacion.Visible = false;

            liInformacionDireccion.Attributes.Add("class", "active");
            liDatosUbicacion.Attributes.Add("class", "");
        }
        protected void btnDatosZonaDeServicio_Click(object sender, EventArgs e)
        {

            if (DGVTarifario.Visible)
            {
                GuardaTarifario();
            }

            ColoniasSeleccionadas();

            //Visualiza el panel 
            liZonaDeRecolecta.Attributes.Add("class", "");
            liDatosZonaDeEntrega.Attributes.Add("class", "active");
            liDatosTarifario.Attributes.Add("class", "");

            PanelZonasServicio.Visible = true;
            PanelZonaDeRecolecta.Visible = false;
            PanelTarifario.Visible = false;

        }
        protected void btnZonaDeRecolecta_Click(object sender, EventArgs e)
        {
            if (DGVTarifario.Visible)
            {
                GuardaTarifario();
            }
            ColoniasSeleccionadas();

            //Visualiza el panel 
            liDatosZonaDeEntrega.Attributes.Add("class", "");
            liDatosTarifario.Attributes.Add("class", "");
            liZonaDeRecolecta.Attributes.Add("class", "active");

            PanelZonasServicio.Visible = false;
            PanelTarifario.Visible = false;
            PanelZonaDeRecolecta.Visible = true;
        }
        protected void BtnZonaDeServicio_Click(object sender, EventArgs e)
        {
            //Este es el metodo del boton del menu 
            MuestraPanel("Zona de servicio");
            if (DLCategoria.Items.Count != 0)
            {
                ObtenerCategoriasSeleccionadas();
            }
            //Obtiene las subcategorias seleccionadas
            if (dlSubcategoria.Items.Count != 0)
            {
                ObtenerSubcategoriasSeleccionadas();
            }
            ColoniasSeleccionadas();


            liDatosZonaDeEntrega.Attributes.Add("class", "");
            liDatosTarifario.Attributes.Add("class", "");
            liZonaDeRecolecta.Attributes.Add("class", "active");
            PanelZonasServicio.Visible = false;
            PanelTarifario.Visible = false;
            PanelZonaDeRecolecta.Visible = true;
        }

        protected void MuestraPanel(string panel)
        {
            if (DGVTarifario.Visible)
            {
                GuardaTarifario();
            }

            PanelDeInformacion.Visible = false;

            //Se visualiza el panel y se asigna su clase para que aparezca activo
            if (panel == "General")
            {
                pnlDatosGenerales.Visible = true;
                liDatosGenerales.Attributes.Add("class", "active");
            }
            else
            {
                pnlDatosGenerales.Visible = false;
                liDatosGenerales.Attributes.Add("class", "");
            }
            if (panel == "Direccion")
            {
                pnlDireccion.Visible = true;
                liDatosDireccion.Attributes.Add("class", "active");
                liInformacionDireccion.Attributes.Add("class", "active");
            }
            else
            {
                pnlDireccion.Visible = false;
                liDatosDireccion.Attributes.Add("class", "");
            }
            if (panel == "Contacto")
            {
                pnlContacto.Visible = true;
                liDatosContacto.Attributes.Add("class", "active");
            }
            else
            {
                pnlContacto.Visible = false;
                liDatosContacto.Attributes.Add("class", "");
            }
            if (panel == "Giro")
            {
                pnlGiro.Visible = true;
                liDatosGiro.Attributes.Add("class", "active");
            }
            else
            {
                pnlGiro.Visible = false;
                liDatosGiro.Attributes.Add("class", "");
            }
            if (panel == "Categoria")
            {
                pnlCategoria.Visible = true;
                liDatosCategoria.Attributes.Add("class", "active");
            }
            else
            {
                pnlCategoria.Visible = false;
                liDatosCategoria.Attributes.Add("class", "");
            }
            if (panel == "Subcategoria")
            {
                pnlSubcategoria.Visible = true;
                liDatosSubcategoria.Attributes.Add("class", "active");
            }
            else
            {
                pnlSubcategoria.Visible = false;
                liDatosSubcategoria.Attributes.Add("class", "");
            }

            if (panel == "Ubicacion")
            {
                PanelUbicacion.Visible = true;
                liDatosUbicacion.Attributes.Add("class", "active");
            }
            else
            {
                PanelUbicacion.Visible = false;
                liDatosUbicacion.Attributes.Add("class", "");
            }
            if (panel == "Contrato")
            {
                PanelDeContrato.Visible = true;
                liDatosContrato.Attributes.Add("class", "active");
            }
            else
            {
                PanelDeContrato.Visible = false;
                liDatosContrato.Attributes.Add("class", "");
            }

            if (panel == "Atencion a clientes")
            {
                PanelAtencionAClientes.Visible = true;
                liDatosAtencionACliente.Attributes.Add("class", "active");
            }
            else
            {
                PanelAtencionAClientes.Visible = false;
                liDatosAtencionACliente.Attributes.Add("class", "");
            }
            if (panel == "Zona de servicio")
            {
                PanelZonaDeServicio.Visible = true;
                liDatosZonaDeServicio.Attributes.Add("class", "active");
                liInformacionDireccion.Attributes.Add("class", "active");
            }
            else
            {
                PanelZonaDeServicio.Visible = false;
                liDatosZonaDeServicio.Attributes.Add("class", "");
            }
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            textboxActivados(ControlDeACcion: "Desactivado");


        }
        #endregion

        #region Controles del panel
        protected void EstatusControlesPanel(bool estado)
        {
            //Activiar cajas de texto
            txtIdetificador.Enabled = estado;
            txtDHoraApertura.Enabled = estado;
            txtDHoraCierre.Enabled = estado;

            //Visibilidad de boton guardar y cancelar
            btnGuardar.Visible = estado;
            btnCancelar.Visible = estado;


            btnNuevoTelefono.Enabled = estado;

            btnNuevoTelefono.Enabled = estado;

            //Habilitar el datalist para seleccionar giro,categoria, subcategoria
            DLGiro.Enabled = estado;
            DLGiroSeleccionado.Enabled = estado;
            DlCategoriaSeleccionada.Enabled = estado;

            //Control de mapa 
            btnBuscarUbicacion.Enabled = estado;
            txtBusquedaUbicacion.Enabled = estado;
            MapaPrueba.Enabled = estado;

            btnMiUbicacion.Enabled = estado;
            DgvLicencia.Enabled = estado;
            DGVTELEFONOS.Enabled = estado;

            ddlEstatusSucursal.Enabled = estado;

            //Controlesdelmodulo de contratos
            txtBIdentificador.Enabled = estado;
            txtBHoraApertura.Enabled = estado;
            txtBCodigo.Enabled = estado;
            txtBHoraDeCierre.Enabled = estado;
            btnBuscarEmpresa.Enabled = estado;
            ddlCEstatus.Enabled = estado;
            //Boton para generar codigo de busqueda
            btnGenerarCodigoDeBusqueda.Enabled = estado;
            txtClaveDeBusqueda.Enabled = estado;
            //Mensajes de clientes
            txtMensaje.Enabled = estado;
            DgvMensajes.Enabled = estado;

            chkVisibilidadInformacion.Enabled = estado;
            //Asignacion de clase a los paneles
            if (estado)
            {
                btnAgregarMensaje.CssClass = "btn btn-sm btn-success";
                btnNuevoTelefono.CssClass = "btn btn-sm btn-default";
                btnMiUbicacion.CssClass = "btn btn-sm btn-default";
                btnBuscarUbicacion.CssClass = "input-group-addon";
                btnGenerarCodigoDeBusqueda.CssClass = "btn btn-sm btn-default";
            }
            else
            {
                DgvLicencia.SelectedIndex = -1;
                DgvLicencia.EditIndex = -1;
                btnAgregarMensaje.CssClass = "btn btn-sm btn-success disabled";
                btnNuevoTelefono.CssClass = "btn btn-sm btn-default disabled";
                btnMiUbicacion.CssClass = "btn btn-sm btn-default disabled";
                btnBuscarUbicacion.CssClass = "input-group-addon disabled";
                btnGenerarCodigoDeBusqueda.CssClass = "btn btn-sm btn-default disabled";

            }
            EstatusControlesDireccion(estado);
            EstatusControlesZonaDeServicio(estado, "Entrega");
            EstatusControlesZonaDeServicio(estado, "Recolecta");

            //Modulo de direccion
            EstatusControlesDireccion(estado);

            //Tarifario 
            DGVTarifario.Enabled = estado;
            BtnCopiarTarifarioAbajo.Enabled = estado;
            BtnCopiarTarifarioArriba.Enabled = estado;
        }
        protected void ActivarCajasDeTextoGestor()
        {
            EstatusControlesPanel(true);
        }
        private void textboxActivados(string ControlDeACcion = "")
        {
            if (AccionesDeLaPagina == "Edicion" && ControlDeACcion == "Desactivado")
            {
                EstatusControlesPanel(true);
                cargaGrid("Telefono");
                //Carga el gridview de las licencias
                cargaGrid("Licencia");
                PanelMensaje.Visible = false;
                btnNuevo.Enabled = false;
                btnNuevo.CssClass = "btn btn-sm btn-default disabled";
                lblGuardarDatos.CssClass = "glyphicon glyphicon-refresh";

                if (Session["Marcador"] != null)
                {
                    Marcador = (GMarker)Session["Marcador"];
                    PlantillaMensajeVentana = (string)Session["PlantillaMensajeVentana"];
                    Zoom = (int)Session["Zoom"];
                    MuestraUbicacionEnMapa(Marcador.point.lat, Marcador.point.lng, PlantillaMensajeVentana, Zoom);
                }
            }
            else if (AccionesDeLaPagina == "NuevoRegistro" && ControlDeACcion == "Activado")
            {
                //Limpia los elementos seleccionados
                MVGiro.LISTADEGIROSELECCIONADO.Clear();
                MVCategoria.LISTADECATEGORIASELECIONADA.Clear();
                MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Clear();
                MVTarifario.ListaDeTarifariosSeleccionados.Clear();
                ActivarCajasDeTextoGestor();
                ActivarCamposDeDireccion();
                PanelMensaje.Visible = false;
                LimpiarCajasDeTexto();
                //Muestra una ubicacion default al iniciar el mapa
                MuestraUbicacionEnMapa(20.123, -30.0034, "<center><string>Indica mi posicion</strong></center>", 17);
                btnNuevo.Enabled = true;
                btnNuevo.CssClass = "btn btn-sm btn-default ";

            }
            else if (ControlDeACcion == "")
            {
                MapaPrueba.resetInfoWindows();
                DesactivarCajasDeTextoPanelGestion();
                DesactivarCamposDeDireccion();
                btnNuevo.Enabled = true;
                LimpiarCajasDeTexto();
                btnNuevo.CssClass = "btn btn-sm btn-default ";
            }

        }

        private void DesactivarCajasDeTextoPanelGestion()
        {
            EstatusControlesPanel(false);
        }

        private void LimpiarCajasDeTexto()
        {
            //Borrar datos de textbox
            txtIdetificador.Text = string.Empty;
            txtDTelefono.Text = string.Empty;
            txtDHoraApertura.Text = string.Empty;
            txtDHoraCierre.Text = string.Empty;
            DDLDTipoDETelefono.SelectedIndex = -1;
        }

        protected void ActivarEdicion(object sender, EventArgs e)
        {
            cargaGrid("Telefono");
            cargaGrid("Direccion");
            AccionesDeLaPagina = "Edicion";
            Session["Accion"] = AccionesDeLaPagina;
            textboxActivados(ControlDeACcion: "Desactivado");
            ActivarCajasDeTextoGestor();
            ActivarCamposDeDireccion();
        }

        #endregion

        #region Dropdownlist

        //Metodos par mostrar informacion en los dropdownlist
        protected void MuestraEstados(string id, string tipo)
        {
            Guid Pais = new Guid(id);

            switch (tipo)
            {
                case "Gestion":
                    DDLDEstado.DataSource = MVDireccion.Estados(Pais);
                    DDLDEstado.DataValueField = "IdEstado";
                    DDLDEstado.DataTextField = "Nombre";
                    DDLDEstado.DataBind();
                    break;
                case "Filtro":
                    DDLDBAESTADO.DataSource = MVDireccion.Estados(Pais);
                    DDLDBAESTADO.DataValueField = "IdEstado";
                    DDLDBAESTADO.DataTextField = "Nombre";
                    DDLDBAESTADO.DataBind();
                    break;
                case "Zona de servicio":
                    DDLZonaEstado.DataSource = MVDireccion.Estados(Pais);
                    DDLZonaEstado.DataValueField = "IdEstado";
                    DDLZonaEstado.DataTextField = "Nombre";
                    DDLZonaEstado.DataBind();
                    break;
                case "Recolecta":
                    DDLZREstado.DataSource = MVDireccion.Estados(Pais);
                    DDLZREstado.DataValueField = "IdEstado";
                    DDLZREstado.DataTextField = "Nombre";
                    DDLZREstado.DataBind();
                    break;
                default:
                    break;
            }
        }
        protected void MuestraMunicipio(string id, string tipo)
        {
            Guid estado = new Guid(id);
            switch (tipo)
            {
                case "Gestion":
                    DDLDMunicipio.DataSource = MVDireccion.Municipios(estado);
                    DDLDMunicipio.DataTextField = "NOMBRE";
                    DDLDMunicipio.DataValueField = "IDMUNICIPIO";
                    DDLDMunicipio.DataBind();
                    break;
                case "Filtro":
                    DDLDBAMUNICIPIO.DataSource = MVDireccion.Municipios(estado);
                    DDLDBAMUNICIPIO.DataTextField = "NOMBRE";
                    DDLDBAMUNICIPIO.DataValueField = "IDMUNICIPIO";
                    DDLDBAMUNICIPIO.DataBind();
                    break;
                case "Zona de servicio":
                    DDLZonaMunicipio.DataSource = MVDireccion.Municipios(estado);
                    DDLZonaMunicipio.DataTextField = "NOMBRE";
                    DDLZonaMunicipio.DataValueField = "IDMUNICIPIO";
                    DDLZonaMunicipio.DataBind();
                    break;
                case "Recolecta":
                    DDLZRMunicipio.DataSource = MVDireccion.Municipios(estado);
                    DDLZRMunicipio.DataTextField = "NOMBRE";
                    DDLZRMunicipio.DataValueField = "IDMUNICIPIO";
                    DDLZRMunicipio.DataBind();
                    break;
                default:
                    break;
            }
        }
        protected void MuestraCiudad(string id, string tipo)
        {
            Guid Municipio = new Guid(id);
            switch (tipo)
            {
                case "Gestion":
                    DDLDCiudad.DataSource = MVDireccion.Ciudades(Municipio);
                    DDLDCiudad.DataTextField = "Nombre";
                    DDLDCiudad.DataValueField = "IdCiudad";
                    DDLDCiudad.DataBind();
                    break;
                case "Filtro":
                    DDLDBACIUDAD.DataSource = MVDireccion.Ciudades(Municipio);
                    DDLDBACIUDAD.DataTextField = "Nombre";
                    DDLDBACIUDAD.DataValueField = "IdCiudad";
                    DDLDBACIUDAD.DataBind();
                    break;
                case "Zona de servicio":
                    DDLZonaCiudad.DataSource = MVDireccion.Ciudades(Municipio);
                    DDLZonaCiudad.DataTextField = "Nombre";
                    DDLZonaCiudad.DataValueField = "IdCiudad";
                    DDLZonaCiudad.DataBind();
                    break;
                case "Recolecta":
                    DDLZRCiudad.DataSource = MVDireccion.Ciudades(Municipio);
                    DDLZRCiudad.DataTextField = "Nombre";
                    DDLZRCiudad.DataValueField = "IdCiudad";
                    DDLZRCiudad.DataBind();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Visualiza los controles donde se cargara la colonia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipo">Gestion, Filtro, Zona de servicio</param>
        protected void MuestraColonia(string id, string tipo)
        {
            Guid Ciudad = new Guid(id);
            switch (tipo)
            {
                case "Gestion":
                    DDLDColonia.DataSource = MVDireccion.Colonias(Ciudad);
                    DDLDColonia.DataTextField = "Nombre";
                    DDLDColonia.DataValueField = "IdColonia";
                    DDLDColonia.DataBind();
                    break;
                case "Filtro":
                    DDLDBACOLONIA.DataSource = MVDireccion.Colonias(Ciudad);
                    DDLDBACOLONIA.DataTextField = "Nombre";
                    DDLDBACOLONIA.DataValueField = "IdColonia";
                    DDLDBACOLONIA.DataBind();
                    break;
                case "Zona de servicio":
                    break;
                default:
                    break;
            }

        }
        protected void ObtenerEstado(object sender, EventArgs e)
        {
            DropDownList objeto = sender as DropDownList;
            switch (objeto.ID)
            {
                case "DDLDPais":
                    if (DDLDPais.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraEstados(DDLDPais.SelectedItem.Value.ToString(), "Gestion");
                        MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Gestion");
                        MuestraCiudad("00000000-0000-0000-0000-000000000000", "Gestion");
                        MuestraColonia("00000000-0000-0000-0000-000000000000", "Gestion");
                    }
                    break;
                case "DDLDBAPAIS":
                    if (DDLDBAPAIS.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraEstados(DDLDBAPAIS.SelectedItem.Value.ToString(), "Filtro");
                        MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Filtro");
                        MuestraCiudad("00000000-0000-0000-0000-000000000000", "Filtro");
                        MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");
                    }
                    break;
                case "DDLZonaPais":
                    if (DDLZonaPais.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraEstados(DDLZonaPais.SelectedItem.Value.ToString(), "Zona de servicio");
                        MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                        MuestraCiudad("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                        MuestraColonia("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                    }
                    break;
                case "DDLZRPais":
                    if (DDLZRPais.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraEstados(DDLZRPais.SelectedItem.Value.ToString(), "Recolecta");
                        MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Recolecta");
                        MuestraCiudad("00000000-0000-0000-0000-000000000000", "Recolecta");
                        MuestraColonia("00000000-0000-0000-0000-000000000000", "Recolecta");
                    }
                    break;
            }
        }
        protected void ObtenerMunicipio(object sender, EventArgs e)
        {
            DropDownList objeto = sender as DropDownList;
            switch (objeto.ID)
            {
                case "DDLDEstado":
                    if (DDLDEstado.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraMunicipio(DDLDEstado.SelectedItem.Value.ToString(), "Gestion");
                        MuestraCiudad("00000000-0000-0000-0000-000000000000", "Gestion");
                        MuestraColonia("00000000-0000-0000-0000-000000000000", "Gestion");
                    }
                    break;
                case "DDLDBAESTADO":
                    if (DDLDBAESTADO.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraMunicipio(DDLDBAESTADO.SelectedItem.Value.ToString(), "Filtro");
                        MuestraCiudad("00000000-0000-0000-0000-000000000000", "Filtro");
                        MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");
                    }
                    break;
                case "DDLZonaEstado":
                    if (DDLZonaEstado.SelectedItem != null)
                    {
                        if (DDLZonaEstado.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                        {
                            MuestraMunicipio(DDLZonaEstado.SelectedItem.Value.ToString(), "Zona de servicio");
                            MuestraCiudad("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                            MuestraColonia("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                        }
                    }
                    break;
                case "DDLZREstado":
                    if (DDLZREstado.SelectedItem != null)
                    {
                        if (DDLZREstado.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                        {
                            MuestraMunicipio(DDLZREstado.SelectedItem.Value.ToString(), "Recolecta");
                            MuestraCiudad("00000000-0000-0000-0000-000000000000", "Recolecta");
                            MuestraColonia("00000000-0000-0000-0000-000000000000", "Recolecta");
                        }
                    }
                    break;
            }
        }
        protected void ObtenerCiudad(object sender, EventArgs e)
        {
            DropDownList objeto = sender as DropDownList;
            switch (objeto.ID)
            {
                case "DDLDMunicipio":
                    if (DDLDMunicipio.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraCiudad(DDLDMunicipio.SelectedItem.Value.ToString(), "Gestion");
                        MuestraColonia("00000000-0000-0000-0000-000000000000", "Gestion");
                    }
                    break;
                case "DDLDBAMUNICIPIO":
                    if (DDLDBAMUNICIPIO.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraCiudad(DDLDBAMUNICIPIO.SelectedItem.Value.ToString(), "Filtro");
                        MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");
                    }
                    break;
                case "DDLZonaMunicipio":
                    if (DDLZonaMunicipio.SelectedItem != null)
                    {
                        if (DDLZonaMunicipio.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                        {
                            MuestraCiudad(DDLZonaMunicipio.SelectedItem.Value.ToString(), "Zona de servicio");
                            MuestraColonia("00000000-0000-0000-0000-000000000000", "Zona de servicio");
                        }
                    }
                    break;
                case "DDLZRMunicipio":
                    if (DDLZRMunicipio.SelectedItem != null)
                    {
                        if (DDLZRMunicipio.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                        {
                            MuestraCiudad(DDLZRMunicipio.SelectedItem.Value.ToString(), "Recolecta");
                            MuestraColonia("00000000-0000-0000-0000-000000000000", "Recolecta");
                        }
                    }
                    break;
            }
        }
        protected void ObtenerColonia(object sender, EventArgs e)
        {
            DropDownList objeto = sender as DropDownList;
            switch (objeto.ID)
            {
                case "DDLDCiudad":
                    if (DDLDCiudad.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraColonia(DDLDCiudad.SelectedItem.Value.ToString(), "Gestion");
                    }
                    break;
                case "DDLDBACIUDAD":
                    if (DDLDBAMUNICIPIO.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraColonia(DDLDBACIUDAD.SelectedItem.Value.ToString(), "Filtro");
                    }
                    break;
                case "DDLZonaCiudad":
                    if (DDLZonaCiudad.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        MuestraColonia(DDLZonaCiudad.SelectedItem.Value.ToString(), "Filtro");
                    }
                    break;
                default:
                    break;
            }
        }
        protected void ObtenerCP(object sender, EventArgs e)
        {
            Guid Colonia = new Guid(DDLDColonia.SelectedItem.Value.ToString());
            txtDCodigoPostal.Text = MVDireccion.ObtenerCodigoPostal(Colonia);
        }

        #endregion Guardar datos

        protected void GuardarDatos(object sender, EventArgs e)
        {
            QuitarColorACamposObligatorios();
            ColoniasSeleccionadas();
            if (txtIdetificador.Text != string.Empty)
            {
                #region Variables
                //Datos generales
                string Identificador = txtIdetificador.Text;
                string UIDSUCURSAL = string.Empty;
                bool VisibilidaddeInformacion = chkVisibilidadInformacion.Checked;
                string codigoDeBusqueda = txtClaveDeBusqueda.Text;
                if (txtDUisSucursal.Text != string.Empty)
                {
                    UIDSUCURSAL = txtDUisSucursal.Text;
                }

                string HoraDeApertura = txtDHoraApertura.Text;
                string HoraDeCierre = txtDHoraCierre.Text;

                string estatus = ddlEstatusSucursal.SelectedItem.Value;
                AccionesDeLaPagina = Session["Accion"].ToString();

                //Variables de ubicacion
                Guid UidUbicacion = Guid.NewGuid();
                DbLatitud = (double)Session["DbLatitud"];
                DbLongitud = (double)Session["DbLongitud"];
                #endregion

                if (Session["Accion"].ToString() == "NuevoRegistro")
                {
                    #region Guardar datos

                    Guid UidSucursal = Guid.NewGuid();
                    Guid UidDireccion = Guid.NewGuid();

                    if (MVSucursales.GuardarSucursal(UidSucursal, UidDireccion, Identificador, Session["UidEmpresaSistema"].ToString(), HoraDeApertura, HoraDeCierre, ddlEstatusSucursal.SelectedItem.Value, VisibilidaddeInformacion, codigoDeBusqueda))
                    {
                        if (MVDireccion.GuardaDireccion(UidDireccion, new Guid(DDLDPais.SelectedItem.Value.ToString()), new Guid(DDLDEstado.SelectedItem.Value), new Guid(DDLDMunicipio.SelectedItem.Value), new Guid(DDLDCiudad.SelectedItem.Value.ToString()), new Guid(DDLDColonia.SelectedItem.Value.ToString()), txtCalle0.Text, txtCalle1.Text, txtCalle2.Text, txtDManzana.Text, txtDLote.Text, txtDCodigoPostal.Text, txtDReferencia.Text, txtIdentificadorDeDireccion.Text))
                        {
                            //Obtiene los giros seleccionados
                            ObtenerGirosSeleccionados();
                            ObtenerCategoriasSeleccionadas();
                            ObtenerSubcategoriasSeleccionadas();
                            #region Catalogos
                            if (MVGiro.LISTADEGIROSELECCIONADO != null && MVGiro.LISTADEGIROSELECCIONADO.Count > 0)
                            {
                                foreach (var item in MVGiro.LISTADEGIROSELECCIONADO)
                                {
                                    MVSucursales.RelacionGiro(item.UIDVM.ToString(), UidSucursal);
                                }
                            }
                            if (MVCategoria.LISTADECATEGORIASELECIONADA != null && MVCategoria.LISTADECATEGORIASELECIONADA.Count > 0)
                            {
                                foreach (var item in MVCategoria.LISTADECATEGORIASELECIONADA)
                                {
                                    MVSucursales.RelacionCategoria(item.UIDCATEGORIA.ToString(), UidSucursal);
                                }
                            }
                            if (MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS != null && MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Count > 0)
                            {
                                foreach (var item in MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS)
                                {
                                    MVSucursales.RelacionSubategoria(item.UID.ToString(), UidSucursal);
                                }
                            }
                            #endregion


                            if (MVTelefono.ListaDeTelefonos.Count > 0)
                            {
                                MVTelefono.GuardaTelefono(UidSucursal, "Sucursal");
                            }

                            #region Zona de servicio

                            //Zona de recolecta
                            if (MVDireccion.ListaColoniasSeleccionadasEntrega.Count > 0 && MVDireccion.ListaColoniasSeleccionadasEntrega != null)
                            {
                                foreach (var item in MVDireccion.ListaColoniasSeleccionadasEntrega)
                                {
                                    MVSucursales.GuardaZona(item.UidRegistro, UidSucursal, item.ID, "Entrega");
                                }
                                //Limpia colonias
                                LimpiaListaDeColoniasEntrega();
                            }
                            //Zona de entrega
                            if (MVDireccion.ListaColoniasSeleccionadasRecolecta.Count > 0 && MVDireccion.ListaColoniasSeleccionadasRecolecta != null)
                            {
                                foreach (var item in MVDireccion.ListaColoniasSeleccionadasRecolecta)
                                {
                                    MVSucursales.GuardaZona(item.UidRegistro, UidSucursal, item.ID, "Recolecta");
                                }
                                //Limpia las colonias 
                                LimpiaListaDeColoniasRecolecta();
                            }

                            #endregion

                            #region Licencia
                            if (MVLicencia.ListaDeLicencias.Count > 0 && MVLicencia.ListaDeLicencias != null)
                            {
                                foreach (var item in MVLicencia.ListaDeLicencias)
                                {
                                    if (!string.IsNullOrWhiteSpace(item.VchIdentificador))
                                    {
                                        MVLicencia.GuardaRelacionSucursal(item.UidLicencia, item.Propietario, item.UidEstatus, item.BLUso, item.VchIdentificador);
                                    }
                                }
                            }
                            #endregion

                            #region Atencion a clientes
                            if (MVMensaje.ListaDeMensajes.Count > 0 && MVMensaje.ListaDeMensajes != null)
                            {
                                //Guarda mensajes
                                foreach (var item in MVMensaje.ListaDeMensajes)
                                {
                                    MVMensaje.GuardarMensaje(item.Uid, item.StrMensaje);
                                    MVMensaje.AsociarMensajeSucursal(item.Uid, new Guid(txtUidSucursal.Text));
                                }
                            }
                            #endregion

                            MVUbicacion.GuardaUbicacionsucursal(UidSucursal, UidUbicacion, DbLatitud.ToString(), DbLongitud.ToString());

                            #region Contrato
                            MVContrato.GuardaRelacionDeContrato();
                            #endregion


                            #region Tarifario
                            if (MVTarifario.ListaDeTarifarios.Count > 0 && DGVTarifario.Visible)
                            {
                                GuardaTarifario();
                                MVTarifario.GuardaTarifario();
                            }

                            if (MVTarifario.ListaDeTarifariosSeleccionados.Count > 0)
                            {
                                MVTarifario.GuardaTarifarioConContrato();
                            }
                            #endregion

                            PanelMensaje.Visible = true;
                            LblMensaje.Text = "Registro agregado!";
                        }
                        else
                        {
                            PanelMensaje.Visible = true;
                            LblMensaje.Text = "Problema al asociar la direccion!";
                        }
                    }
                    else
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Registro no agregado!";
                    }
                    btnGuardar.Visible = false;
                    btnCancelar.Visible = false;
                    PanelIzquierdo.Visible = true;
                    pnlDireccion.Visible = false;
                    PanelDeBusqueda.Visible = true;

                    Session.Remove("Paneles");
                    Session.Remove("Accion");
                    MVSucursales = new VMSucursales();
                    MVSucursales.DatosGridViewBusquedaNormal(Session["UidEmpresaSistema"].ToString());
                    cargaGrid("Normal");
                    Session["MVSucursales"] = MVSucursales;
                    AccionesDeLaPagina = string.Empty;
                    textboxActivados();

                    #endregion
                }
                else
                if (Session["Accion"].ToString() == "Edicion")
                {
                    #region Actualizar datos
                    UIDSUCURSAL = txtUidSucursal.Text;
                    if (MVSucursales.ActualizarDatos(UIDSUCURSAL, Identificador, HoraDeApertura, HoraDeCierre, estatus, VisibilidaddeInformacion, codigoDeBusqueda))
                    {

                        MVDireccion.EliminaDireccion(UIDSUCURSAL);

                        ObtenerGirosSeleccionados();
                        ObtenerCategoriasSeleccionadas();
                        ObtenerSubcategoriasSeleccionadas();

                        if (MVDireccion.GuardaDireccion(new Guid(txtIdDireccion.Text), new Guid(DDLDPais.SelectedItem.Value.ToString()), new Guid(DDLDEstado.SelectedItem.Value), new Guid(DDLDMunicipio.SelectedItem.Value), new Guid(DDLDCiudad.SelectedItem.Value.ToString()), new Guid(DDLDColonia.SelectedItem.Value.ToString()), txtCalle0.Text, txtCalle1.Text, txtCalle2.Text, txtDManzana.Text, txtDLote.Text, txtDCodigoPostal.Text, txtDReferencia.Text, txtIdentificadorDeDireccion.Text))
                        {

                            #region Catalogos
                            //Elimina datos
                            MVSucursales.EliminaGiro(UIDSUCURSAL);
                            MVSucursales.EliminaCategoria(UIDSUCURSAL);
                            MVSucursales.EliminaSubcategoria(UIDSUCURSAL);
                            if (MVGiro.LISTADEGIROSELECCIONADO != null && MVGiro.LISTADEGIROSELECCIONADO.Count > 0)
                            {
                                foreach (var item in MVGiro.LISTADEGIROSELECCIONADO)
                                {
                                    MVSucursales.RelacionGiro(item.UIDVM.ToString(), new Guid(UIDSUCURSAL));
                                }
                            }
                            if (MVCategoria.LISTADECATEGORIASELECIONADA != null && MVCategoria.LISTADECATEGORIASELECIONADA.Count > 0)
                            {
                                foreach (var item in MVCategoria.LISTADECATEGORIASELECIONADA)
                                {
                                    MVSucursales.RelacionCategoria(item.UIDCATEGORIA.ToString(), new Guid(UIDSUCURSAL));
                                }
                            }
                            if (MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS != null && MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Count > 0)
                            {
                                foreach (var item in MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS)
                                {
                                    MVSucursales.RelacionSubategoria(item.UID.ToString(), new Guid(UIDSUCURSAL));
                                }
                            }
                            #endregion

                            #region Zona de servicio
                            //Elimina los registros de la base de datos
                            MVSucursales.EliminaZona(new Guid(UIDSUCURSAL));
                            //Zona de recolecta
                            if (MVDireccion.ListaColoniasSeleccionadasEntrega.Count > 0 && MVDireccion.ListaColoniasSeleccionadasEntrega != null)
                            {
                                foreach (var item in MVDireccion.ListaColoniasSeleccionadasEntrega)
                                {
                                    MVSucursales.GuardaZona(item.UidRegistro, new Guid(UIDSUCURSAL), item.ID, "Entrega");
                                }
                                //Limpia colonias
                                LimpiaListaDeColoniasEntrega();
                            }
                            //Zona de entrega
                            if (MVDireccion.ListaColoniasSeleccionadasRecolecta.Count > 0 && MVDireccion.ListaColoniasSeleccionadasRecolecta != null)
                            {
                                foreach (var item in MVDireccion.ListaColoniasSeleccionadasRecolecta)
                                {
                                    MVSucursales.GuardaZona(item.UidRegistro, new Guid(UIDSUCURSAL), item.ID, "Recolecta");
                                }
                                //Limpia las colonias 
                                LimpiaListaDeColoniasRecolecta();
                            }

                            #endregion

                            #region Licencia
                            //Elimina licencia 
                            MVLicencia.eliminaLicenciaSucursal(new Guid(txtUidSucursal.Text));
                            //Guarda licencia
                            foreach (var item in MVLicencia.ListaDeLicencias)
                            {
                                if (!string.IsNullOrWhiteSpace(item.VchIdentificador))
                                {
                                    MVLicencia.GuardaRelacionSucursal(item.UidLicencia, item.Propietario, item.UidEstatus, item.BLUso, item.VchIdentificador);
                                }
                            }
                            #endregion

                            #region Mensajes
                            //Elimina mensajes
                            MVMensaje.EliminaMensajesSucursal(UIDSUCURSAL);
                            //Guarda mensajes
                            foreach (var item in MVMensaje.ListaDeMensajes)
                            {
                                MVMensaje.GuardarMensaje(item.Uid, item.StrMensaje);
                                MVMensaje.AsociarMensajeSucursal(item.Uid, new Guid(txtUidSucursal.Text));
                            }
                            #endregion

                            #region Contacto
                            //Elimina contacto
                            MVTelefono.EliminaTelefonoSucursal(UIDSUCURSAL);
                            //Guarda contacto
                            if (MVTelefono.ListaDeTelefonos.Count > 0)
                            {
                                MVTelefono.GuardaTelefono(new Guid(UIDSUCURSAL), "Sucursal");
                            }
                            #endregion

                            #region Tarifario
                            if (MVTarifario.ListaDeTarifarios.Count > 0 && DGVTarifario.Visible)
                            {
                                GuardaTarifario();
                                MVTarifario.EliminaTarifarioDeBaseDeDatos(UIDSUCURSAL);
                                MVTarifario.GuardaTarifario();
                            }

                            #endregion

                            //Actualiza el datagrid de las ciudades de la zona de servicio
                            MuestraZonaDeServicio(new Guid(txtUidSucursal.Text));
                            EstatusControlesZonaDeServicio(false, "Entrega");
                            EstatusControlesZonaDeServicio(false, "Recolecta");
                            MVUbicacion.GuardaUbicacionsucursal(new Guid(UIDSUCURSAL), UidUbicacion, DbLatitud.ToString(), DbLongitud.ToString());

                            if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString()))
                            {
                                string UidSuministradora = txtUidSucursal.Text;

                                if (MVTarifario.ListaDeTarifariosSeleccionados.Count > 0)
                                {
                                    //crear el metodo y verificar que este borre los datos relacionados al contrato

                                    MVTarifario.EliminarTarifarioDeSucursal(new Guid(UidSuministradora));

                                    MVTarifario.GuardaTarifarioConContrato();
                                }
                                MVContrato.borrarSucursalSuministradora(UidSuministradora);
                                MVContrato.GuardaRelacionDeContrato();
                            }
                            else
                            {
                                string UidDistribuidora = txtUidSucursal.Text;
                                MVContrato.borrarSucursalDistribuidora(UidDistribuidora);
                                MVContrato.GuardaRelacionDeContrato();
                            }


                            PanelMensaje.Visible = true;
                            LblMensaje.Text = "Registro actualizado!";
                        }
                    }
                    else
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Registro actualizado!";
                    }
                    Session.Remove("Accion");
                    MVSucursales = new VMSucursales();
                    MVSucursales.DatosGridViewBusquedaNormal(Session["UidEmpresaSistema"].ToString());
                    cargaGrid("Normal");
                    DgvLicencia.Enabled = false;
                    cargaGrid("Licencia");
                    dgvBusquedaDeEmpresa.DataSource = null;
                    dgvBusquedaDeEmpresa.DataBind();
                    Session["MVSucursales"] = MVSucursales;
                    AccionesDeLaPagina = string.Empty;
                    textboxActivados();
                    DGVEMPRESAS.SelectedIndex = -1;
                    #endregion
                }
            }
            else
            {
                if (txtIdetificador.Text == string.Empty || txtIdetificador.Text == " ")
                {
                    txtIdetificador.BorderColor = System.Drawing.Color.Red;
                }

                if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString()))
                {
                    string UidSuministradora = txtUidSucursal.Text;
                    MVContrato.borrarSucursalSuministradora(UidSuministradora);
                }
                else
                {
                    string UidDistribuidora = txtUidSucursal.Text;
                    MVContrato.borrarSucursalDistribuidora(UidDistribuidora);
                }



            }

        }

        protected void QuitarColorACamposObligatorios()
        {
            txtIdetificador.BorderColor = System.Drawing.Color.White;

        }

        #endregion

        #region Panel de direccion

        protected void EstatusControlesDireccion(bool estatus)
        {
            //Datos de la direccion
            txtIdentificadorDeDireccion.Enabled = estatus;
            DDLDMunicipio.Enabled = estatus;
            DDLDColonia.Enabled = estatus;
            DDLDEstado.Enabled = estatus;
            txtDReferencia.Enabled = estatus;
            txtDManzana.Enabled = estatus;
            DDLDPais.Enabled = estatus;
            txtDLote.Enabled = estatus;
            txtCalle0.Enabled = estatus;
            txtCalle1.Enabled = estatus;
            txtCalle2.Enabled = estatus;
            txtDCodigoPostal.Enabled = estatus;
            DDLDCiudad.Enabled = estatus;
        }
        protected void ActivarCamposDeDireccion()
        {
            EstatusControlesDireccion(true);
        }
        protected void DesactivarCamposDeDireccion()
        {
            EstatusControlesDireccion(false);
        }

        protected void LimpiarCamposDeDireccion()
        {
            //Datos de direccion
            txtCalle0.Text = string.Empty;
            txtCalle1.Text = string.Empty;
            txtCalle2.Text = string.Empty;
            txtDCodigoPostal.Text = string.Empty;

            DDLDPais.SelectedIndex = 0;
            txtDReferencia.Text = string.Empty;
            txtDLote.Text = string.Empty;
            txtDManzana.Text = string.Empty;

        }

        protected void MuestraDireccion(string id)
        {
            MVDireccion.ObtenerDireccionSucursal(id);
            //Esconde el Id en un textbox
            txtIdDireccion.Text = MVDireccion.ID.ToString();
            DDLDPais.SelectedIndex = DDLDPais.Items.IndexOf(DDLDPais.Items.FindByValue(MVDireccion.PAIS));
            //Obtener lista de estado y seleccionar el del objeto
            MuestraEstados(MVDireccion.PAIS, "Gestion");
            DDLDEstado.SelectedIndex = DDLDEstado.Items.IndexOf(DDLDEstado.Items.FindByValue(MVDireccion.ESTADO));

            // Obtener lista de municipio y seleccionar el del objeto
            MuestraMunicipio(MVDireccion.ESTADO, "Gestion");
            DDLDMunicipio.SelectedIndex = DDLDMunicipio.Items.IndexOf(DDLDMunicipio.Items.FindByValue(MVDireccion.MUNICIPIO));

            // Obtener lista de ciudades y seleccionar el del objeto
            MuestraCiudad(MVDireccion.MUNICIPIO, "Gestion");
            DDLDCiudad.SelectedIndex = DDLDCiudad.Items.IndexOf(DDLDCiudad.Items.FindByValue(MVDireccion.CIUDAD));

            // Obtener lista de colonias y seleccionar el del objeto
            MuestraColonia(MVDireccion.CIUDAD, "Gestion");
            DDLDColonia.SelectedIndex = DDLDColonia.Items.IndexOf(DDLDColonia.Items.FindByValue(MVDireccion.COLONIA));

            txtCalle0.Text = MVDireccion.CALLE0;
            txtCalle1.Text = MVDireccion.CALLE1;
            txtCalle2.Text = MVDireccion.CALLE2;
            txtDManzana.Text = MVDireccion.MANZANA;
            txtDLote.Text = MVDireccion.LOTE;
            txtDCodigoPostal.Text = MVDireccion.CodigoPostal;
            txtDReferencia.Text = MVDireccion.REFERENCIA;




            DeseleccionaCheckboxListColoniasEntrega();
            DeseleccionaCheckboxListColoniasRecolecta();

            chklColonias.Items.Clear();
            chklColonias.DataBind();


            chklZR.Items.Clear();
            chklZR.DataBind();

            if (DGVZonaCiudades.SelectedValue != null)
            {
                LinkButton boton = DGVZonaCiudades.Rows[DGVZonaCiudades.SelectedIndex].FindControl("btnEliminaZona") as LinkButton;

                boton.Enabled = false;
                boton.CssClass = "btn btn-sm btn-default disabled";

                DGVZonaCiudades.SelectedIndex = -1;
            }
            //Recupera zona de servicio
            MuestraZonaDeServicio(new Guid(id));

        }

        #endregion

        #region Telefonos

        protected void NuevoTelefono(object sender, EventArgs e)
        {
            btnEditarTelefono.Enabled = false;
            btnEditarTelefono.CssClass = "btn btn-sm btn-default disabled";
            txtDTelefono.Text = string.Empty;
            DDLDTipoDETelefono.SelectedIndex = -1;
            DGVTELEFONOS.SelectedIndex = -1;
            IconActualizaTelefono.CssClass = "glyphicon glyphicon-ok";
            EstatusControlesTelefono(true);
        }

        protected void CancelarTelefono(object sender, EventArgs e)
        {
            EstatusControlesTelefono(false);
        }
        protected void AgregaTelefono(object sender, EventArgs e)
        {
            if (IconActualizaTelefono.CssClass == "glyphicon glyphicon-refresh")
            {
                ActualizaTelefono();
            }
            else
            {
                GuardaTelefono();
            }

        }

        protected void GuardaTelefono()
        {
            MVTelefono.AgregaTelefonoALista(DDLDTipoDETelefono.SelectedItem.Value.ToString(), txtDTelefono.Text, DDLDTipoDETelefono.SelectedItem.Text.ToString());

            DDLDTipoDETelefono.SelectedIndex = -1;
            txtDTelefono.Text = string.Empty;
            DDLDTipoDETelefono.Enabled = false;
            txtDTelefono.Enabled = false;
            btnGuardarTelefono.Visible = false;
            btnCancelarTelefono.Visible = false;
            cargaGrid("Telefono");
        }
        protected void ActualizaTelefono()
        {
            MVTelefono.ActualizaRegistroEnListaDeTelefonos(txtIdTelefono.Text, DDLDTipoDETelefono.SelectedItem.Value.ToString(), txtDTelefono.Text);

            DDLDTipoDETelefono.SelectedIndex = -1;
            txtDTelefono.Text = string.Empty;
            DDLDTipoDETelefono.Enabled = false;
            txtDTelefono.Enabled = false;
            btnGuardarTelefono.Visible = false;
            btnCancelarTelefono.Visible = false;
            cargaGrid("Telefono");
        }

        protected void EstatusControlesTelefono(bool Estatus)
        {
            btnGuardarTelefono.Visible = Estatus;
            btnCancelarTelefono.Visible = Estatus;
            DDLDTipoDETelefono.Enabled = Estatus;
            txtDTelefono.Enabled = Estatus;
        }

        protected void DGVTELEFONOS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVTELEFONOS, "Select$" + e.Row.RowIndex);

                LinkButton Eliminar = e.Row.FindControl("EliminaTelefono") as LinkButton;
                Label lblTipoDeTelefono = e.Row.FindControl("txtTipoDeTelefono") as Label;

                var icono = e.Row.FindControl("lblEliminarTelefono") as Label;

                icono.CssClass = "glyphicon glyphicon-trash";

                if (Session["Accion"] != null)
                {
                    AccionesDeLaPagina = Session["Accion"].ToString();
                }

                if (MVTelefono.ListaDeTelefonos.Count <= 1)
                {
                    Eliminar.Enabled = false;
                    Eliminar.CssClass = "btn btn-sm btn-default disabled";
                }
                else
                {
                    if (DGVTELEFONOS.Enabled)
                    {
                        Eliminar.Enabled = true;
                        Eliminar.CssClass = "btn btn-sm btn-default";
                    }
                    else
                    {
                        Eliminar.Enabled = false;
                        Eliminar.CssClass = "btn btn-sm btn-default disabled";
                    }
                }
                if (!string.IsNullOrEmpty(e.Row.Cells[1].Text))
                {
                    lblTipoDeTelefono.Text = DDLDTipoDETelefono.Items.FindByValue(e.Row.Cells[1].Text).Text;
                }
            }
        }

        protected void DGVTELEFONOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow Seleccionado = DGVTELEFONOS.Rows[index];
                GridView valor = (GridView)sender;
                string ID = valor.DataKeys[Seleccionado.RowIndex].Value.ToString();
                MVTelefono.QuitaTelefonoDeLista(ID);
                Session["AD"] = MVSucursales;
                cargaGrid("Telefono");

            }
        }



        protected void DGVTELEFONOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            if (AccionesDeLaPagina != string.Empty)
            {
                string valor = DGVTELEFONOS.SelectedDataKey.Value.ToString();
                Session["AD"] = MVSucursales;
                MVTelefono.ObtenTelefono(valor);
                btnEditarTelefono.Enabled = true;
                btnEditarTelefono.CssClass = "btn btn-sm btn-default";
                EstatusControlesTelefono(false);
                txtIdTelefono.Text = MVTelefono.ID.ToString();
                DDLDTipoDETelefono.SelectedIndex = DDLDTipoDETelefono.Items.IndexOf(DDLDTipoDETelefono.Items.FindByValue(MVTelefono.UidTipo.ToString()));
                txtDTelefono.Text = MVTelefono.NUMERO;

            }
        }
        protected void EditaTelefono(object sender, EventArgs e)
        {
            EstatusControlesTelefono(true);
            IconActualizaTelefono.CssClass = "glyphicon glyphicon-refresh";
        }
        #endregion

        #region Panel Busqueda ampliada

        protected void BusquedaAvanzada(object sender, EventArgs e)
        {
            PanelBusquedaAmpliada.Visible = true;
            PanelDerecho.Visible = false;
            PanelIzquierdo.Visible = false;
            PanelFiltrosBusquedaAmpliada.Visible = true;
            VisivilidadDeFiltros("Ampliada");

        }
        protected void BusquedaNormal(object sender, EventArgs e)
        {
            PanelBusquedaAmpliada.Visible = false;
            PanelDerecho.Visible = true;
            PanelIzquierdo.Visible = true;


        }
        protected void BorrarFiltrosBusquedaAvanzada(object sender, EventArgs e)
        {
            //Cajas de texto 
            txtBAIdentificador.Text = string.Empty;
            txtBaHorarioApertura.Text = string.Empty;
            txtBAHorarioCierre.Text = string.Empty;
            txtBACalle.Text = string.Empty;
            txtBACOdigoPostal.Text = string.Empty;

            //Dropdown list
            DDLDBAPAIS.SelectedIndex = -1;

            MuestraEstados("00000000-0000-0000-0000-000000000000", "Filtro");
            MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Filtro");
            MuestraCiudad("00000000-0000-0000-0000-000000000000", "Filtro");
            MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");

        }

        #endregion

        protected void DLGiro_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DataList dl = source as DataList;
            if (e == null || e.Item == null)
            {
                Trace.Write("dl_ItemCommand", "EventArgs.Item is null");
                throw new Exception("dl_ItemCommand: EventArgs.Item is null");
            }

            int selIdx = dl.SelectedIndex;

            Trace.Write("dl_ItemCommand", String.Format("{0}: {1}",
                e.CommandName.ToLower(), e.Item.ItemIndex));
            switch (e.CommandName.ToLower())
            {
                case "select":
                    selIdx = e.Item.ItemIndex;
                    break;
                case "unselect":
                    selIdx = -1;
                    break;
            }

            if (selIdx != dl.SelectedIndex)
                dl.SelectedIndex = selIdx;
            dl.DataSource = MVGiro.LISTADEGIRO;
            dl.DataBind();
        }

        protected void quitaCategoriasSeleccionadasSinGiro()
        {
            for (int i = 0; i < MVCategoria.LISTADECATEGORIASELECIONADA.Count; i++)
            {
                if (!MVGiro.LISTADEGIROSELECCIONADO.Exists(GIro => GIro.UIDVM.ToString() == MVCategoria.LISTADECATEGORIASELECIONADA[i].UIDGIRO))
                {
                    MVCategoria.LISTADECATEGORIASELECIONADA.RemoveAt(i);
                    i--;
                }
            }
        }

        protected void QuitaSubcategoriaSeleccionadaSinCategoria()
        {
            for (int i = 0; i < MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Count; i++)
            {
                if (!MVCategoria.LISTADECATEGORIASELECIONADA.Exists(Categoria => Categoria.UIDCATEGORIA == MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS[i].UIDCATEGORIA))
                {
                    MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.RemoveAt(i);
                    i--;
                }
            }
        }
        protected void ObtenerGirosSeleccionados()
        {
            foreach (DataListItem item in DLGiro.Items)
            {
                CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkGiro");
                if (ObjectoCheckBox != null)
                {
                    if (ObjectoCheckBox.Checked == true)
                    {
                        if (!MVGiro.LISTADEGIROSELECCIONADO.Exists(G => G.UIDVM.ToString() == DLGiro.DataKeys[item.ItemIndex].ToString()))
                        {
                            MVGiro.SeleccionDeGiro(DLGiro.DataKeys[item.ItemIndex].ToString());
                        }

                    }
                }
            }
        }
        protected void ObtenerCategoriasSeleccionadas()
        {
            //Obtiene las categorias seleccionadas del grid
            foreach (DataListItem item in DLCategoria.Items)
            {
                //Obtiene el control de checkbox del datalist
                CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkCategoria");
                if (ObjectoCheckBox != null)
                {
                    //Verifica si el Checkbox esta seleccionado y agrega a la lista
                    if (ObjectoCheckBox.Checked == true)
                    {
                        if (!MVCategoria.LISTADECATEGORIASELECIONADA.Exists(Cat => Cat.UIDCATEGORIA.ToString() == DLCategoria.DataKeys[item.ItemIndex].ToString()))
                        {
                            MVCategoria.SeleccionarCategoria(DLCategoria.DataKeys[item.ItemIndex].ToString());
                        }
                    }
                    //Verifica si el checkbox esta deselecionado y lo borra de la lista si existe
                    if (ObjectoCheckBox.Checked == false)
                    {
                        MVCategoria.DeselecionarCategoria(DLCategoria.DataKeys[item.ItemIndex].ToString());
                    }
                }
            }
        }
        protected void ObtenerSubcategoriasSeleccionadas()
        {
            //Obtiene las subcategorias seleccionadas del grid
            foreach (DataListItem item in dlSubcategoria.Items)
            {
                //Obtiene el control de checkbox del datalist
                CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkSubcategoria");
                if (ObjectoCheckBox != null)
                {
                    //Verifica si el Checkbox esta seleccionado y agrega a la lista
                    if (ObjectoCheckBox.Checked == true)
                    {
                        if (!MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Exists(Cat => Cat.UID.ToString() == dlSubcategoria.DataKeys[item.ItemIndex].ToString()))
                        {
                            MVSubcategoria.SeleccionarSubcategoria(dlSubcategoria.DataKeys[item.ItemIndex].ToString());
                        }
                    }
                    //Verifica si el checkbox esta deselecionado y lo borra de la lista si existe
                    if (ObjectoCheckBox.Checked == false)
                    {
                        MVSubcategoria.DeseleccionarSubcategoria(dlSubcategoria.DataKeys[item.ItemIndex].ToString());
                    }
                }
            }
        }
        protected void DLGiroSeleccionado_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DataList dl = source as DataList;
            if (e == null || e.Item == null)
            {
                Trace.Write("dl_ItemCommand", "EventArgs.Item is null");
                throw new Exception("dl_ItemCommand: EventArgs.Item is null");
            }

            int selIdx = dl.SelectedIndex;

            quitaCategoriasSeleccionadasSinGiro();


            if (DLCategoria.Items.Count >= 0)
            {
                ObtenerCategoriasSeleccionadas();
            }

            Trace.Write("dl_ItemCommand", String.Format("{0}: {1}",
             e.CommandName.ToLower(), e.Item.ItemIndex));
            switch (e.CommandName.ToLower())
            {
                case "select":
                    selIdx = e.Item.ItemIndex;
                    MVCategoria.CategoriaConImagen(DLGiroSeleccionado.DataKeys[e.Item.ItemIndex].ToString());
                    DLCategoria.DataSource = MVCategoria.LISTADECATEGORIAS;
                    DLCategoria.DataBind();
                    break;
                case "unselect":
                    selIdx = -1;
                    DLCategoria.DataSource = null;
                    DLCategoria.DataBind();
                    break;
            }

            if (selIdx != dl.SelectedIndex)
                dl.SelectedIndex = selIdx;
            dl.DataSource = MVGiro.LISTADEGIROSELECCIONADO;
            dl.DataBind();

            foreach (DataListItem item in DLCategoria.Items)
            {
                //Obtiene el control de checkbox del datalist
                CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkCategoria");
                if (ObjectoCheckBox != null)
                {
                    //Verifica que el checkbox no este seleccionado
                    if (ObjectoCheckBox.Checked == false)
                    {
                        foreach (var categoria in MVCategoria.LISTADECATEGORIASELECIONADA)
                        {
                            Guid IDCategoria = new Guid(DLCategoria.DataKeys[item.ItemIndex].ToString());
                            if (IDCategoria == categoria.UIDCATEGORIA)
                            {
                                ObjectoCheckBox.Checked = true;
                            }
                        }
                    }
                }
            }
            Session["MVCategoria"] = MVCategoria;
        }

        protected void DlCategoriaSeleccionada_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DataList dl = source as DataList;
            if (e == null || e.Item == null)
            {
                Trace.Write("dl_ItemCommand", "EventArgs.Item is null");
                throw new Exception("dl_ItemCommand: EventArgs.Item is null");
            }

            int selIdx = dl.SelectedIndex;

            QuitaSubcategoriaSeleccionadaSinCategoria();


            if (dlSubcategoria.Items.Count >= 0)
            {
                ObtenerSubcategoriasSeleccionadas();
            }

            Trace.Write("dl_ItemCommand", String.Format("{0}: {1}",
            e.CommandName.ToLower(), e.Item.ItemIndex));
            switch (e.CommandName.ToLower())
            {
                case "select":
                    selIdx = e.Item.ItemIndex;
                    MVSubcategoria.SubcategoriaConImagen(DlCategoriaSeleccionada.DataKeys[e.Item.ItemIndex].ToString());
                    dlSubcategoria.DataSource = MVSubcategoria.LISTADESUBCATEGORIAS;
                    dlSubcategoria.DataBind();
                    break;
                case "unselect":
                    selIdx = -1;
                    dlSubcategoria.DataSource = null;
                    dlSubcategoria.DataBind();
                    break;
            }

            if (selIdx != dl.SelectedIndex)
                dl.SelectedIndex = selIdx;
            dl.DataSource = MVCategoria.LISTADECATEGORIASELECIONADA;
            dl.DataBind();



            foreach (DataListItem item in dlSubcategoria.Items)
            {
                //Obtiene el control de checkbox del datalist
                CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkSubcategoria");
                if (ObjectoCheckBox != null)
                {
                    //Verifica que el checkbox no este seleccionado
                    if (ObjectoCheckBox.Checked == false)
                    {
                        foreach (var Subcategoria in MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS)
                        {
                            Guid IdSubcategoria = new Guid(dlSubcategoria.DataKeys[item.ItemIndex].ToString());
                            if (IdSubcategoria == Subcategoria.UID)
                            {
                                ObjectoCheckBox.Checked = true;
                            }
                        }
                    }
                }
            }
        }

        protected void btnBuscarUbicacion_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtBusquedaUbicacion.Text))
            {
                string key = ConfigurationManager.AppSettings.Get("googlemaps.subgurim.net");
                GeoCode Informacion = GMap.geoCodeRequest(txtBusquedaUbicacion.Text, key, new GLatLngBounds(new GLatLng(40, 10), new GLatLng(50, 20)));
                MapaPrueba.resetInfoWindows();
                Marcador = new GMarker(Informacion.Placemark.coordinates);
                PlantillaMensajeVentana = "<center>" + Informacion.Placemark.address + "</center>";
                MuestraUbicacionEnMapa(Marcador.point.lat, Marcador.point.lng, PlantillaMensajeVentana, 17);
            }
        }

        protected void btnMiUbicacion_Click(object sender, EventArgs e)
        {
            //string DataBase = Server.MapPath("~/Vista/GeoLiteCity.dat");
            //LookupService servicio = new LookupService(DataBase);
            //Location Ubicacion = servicio.getLocation(Request.UserHostAdºdress);

            //if (Ubicacion != null)
            //{
            //    PlantillaMensajeVentana = "<label>" + MVSucursales.ID + "</label>\n<p>Latitud: " + Ubicacion.latitude.ToString() + "\nLongitud: " + Ubicacion.longitude.ToString() + "</p>";
            //    MuestraUbicacionEnMapa(Ubicacion.latitude, Ubicacion.longitude, PlantillaMensajeVentana, 17);
            //}
        }

        protected void btnRadioMarcador_Click(object sender, EventArgs e)
        {
            string[] Alfabeto = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            for (int i = 0; i < txtRadio.Text.Length; i++)
            {
                for (int j = 0; j < Alfabeto.Length; j++)
                {
                    if (txtRadio.Text.Substring(i, 1) == Alfabeto[j])
                    {
                        txtRadio.BorderColor = Color.Red;
                        txtRadio.ToolTip = "El campo solo acepta numeros decimales";
                        break;
                    }
                }
            }


            double latitud = double.Parse(Session["DbLatitud"].ToString());
            double longitud = double.Parse(Session["DbLongitud"].ToString());
            var earthsradius = 6376;
            var d2r = Math.PI / 180;   // degrees to radians
            var r2d = 180 / Math.PI;   // radians to degrees
            double radio = double.Parse(txtRadio.Text);
            var rlatitud = (radio / earthsradius) * r2d;
            var rLongitud = rlatitud / Math.Cos(double.Parse(Session["DbLatitud"].ToString()) * d2r);
            List<GLatLng> puntos = new List<GLatLng>();
            for (int i = 0; i < radio; i++)
            {
                double theta = Math.PI * (i / (double)(radio / 2));
                double ex = rLongitud + (rLongitud * Math.Cos(theta));
                double ey = rlatitud + (rlatitud * Math.Sin(theta));
                puntos.Add(new GLatLng(ey, ex));
            }

            GPolygon circulo = new GPolygon(puntos, Color.Red, 3, 1, Color.Red, 1);
            circulo.createPolygon(new GLatLng(latitud, longitud), 20, radio);
            circulo.close();
            MapaPrueba.Add(circulo);
            // MuestraUbicacionEnMapa()


        }

        protected string MapaPrueba_MarkerClick(object s, GAjaxServerEventArgs e)
        {
            //Almacena la longitud y la latitud 
            DbLatitud = e.point.lat;
            DbLongitud = e.point.lng;
            //Almacena los valores para usar en el metodo de guardar
            Session["DbLatitud"] = DbLatitud;
            Session["DbLongitud"] = DbLongitud;
            return "";
        }

        #region Zona de servicio

        protected void LimpiaListaDeColoniasEntrega()
        {
            chklColonias.Items.Clear();
            chklColonias.DataBind();
        }
        protected void LimpiaListaDeColoniasRecolecta()
        {
            chklZR.Items.Clear();
            chklZR.DataBind();
        }
        protected void MuestraZonaDeServicio(Guid uidsucursal = new Guid())
        {
            //Limpia la lista de checkbox
            //DeseleccionaCheckboxListColonias();
            //Recupera la zona de servicio
            MVSucursales.RecuperaZonaEntrega(uidsucursal);
            MVSucursales.RecuperaZonaRecoleccion(uidsucursal);
            //limpia las ciudades seleccionadas
            MVDireccion.ListaCiudadesSeleccionadasEntrega.Clear();
            MVDireccion.ListaColoniasSeleccionadasEntrega.Clear();
            MVDireccion.ListaCiudadesSeleccionadasRecolecta.Clear();
            MVDireccion.ListaColoniasSeleccionadasRecolecta.Clear();

            foreach (var item in MVSucursales.ListaDeColoniasEntrega)
            {
                MVDireccion.SeleccionarCiudadEntrega(item.ID);
                MVDireccion.SeleccionarColoniaEntrega(item.UidRelacionRegistro, item.UidColonia, item.ID, item.StrNombreColonia);
            }

            foreach (var item in MVSucursales.ListaDeColoniasRecolecta)
            {
                MVDireccion.SeleccionarCiudadRecolecta(item.ID);
                MVDireccion.SeleccionarColoniaRecolecta(item.UidRelacionRegistro, item.UidColonia, item.ID, item.StrNombreColonia);
            }
            //Recarga el gridview de las ciudades asociadas
            DGVZonaCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasEntrega;
            DGVZonaCiudades.DataBind();
            DGVZRCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasRecolecta;
            DGVZRCiudades.DataBind();

        }


        protected void EstatusControlesZonaDeServicio(bool estatus, string panel)
        {
            if (panel == "Entrega")
            {
                //Zona de servicio
                btnAgregarCiudad.Enabled = estatus;
                DGVZonaCiudades.Enabled = estatus;
                btnBusquedaColonia.Enabled = estatus;
                if (estatus)
                {
                    btnBusquedaColonia.CssClass = "input-group-addon";
                    btnAgregarCiudad.CssClass = "btn btn-sm btn-success";
                }
                else
                {
                    btnBusquedaColonia.CssClass = "input-group-addon disabled";
                    btnAgregarCiudad.CssClass = "btn btn-sm btn-success disabled";
                }

                chklColonias.Enabled = estatus;
                chkSeleccionarTodos.Enabled = estatus;
            }
            if (panel == "Recolecta")
            {
                //Zona de servicio
                btnZRAgregaCiudad.Enabled = estatus;
                DGVZRCiudades.Enabled = estatus;
                btnZrBusquedaColonia.Enabled = estatus;
                if (estatus)
                {
                    btnZrBusquedaColonia.CssClass = "input-group-addon";
                    btnZRAgregaCiudad.CssClass = "btn btn-sm btn-success";
                }
                else
                {
                    btnZrBusquedaColonia.CssClass = "input-group-addon disabled";
                    btnZRAgregaCiudad.CssClass = "btn btn-sm btn-success disabled";
                }

                chklZRSeleccionaTodos.Enabled = estatus;
                chklZR.Enabled = estatus;
            }
        }
        /// <summary>
        /// Este metodo selecciona las colonias ya sea de entrega o recolecta, valida que se haya seleccionado la ciudad y la guarda junto con las colonias en una lista temporal.
        /// </summary>
        protected void ColoniasSeleccionadas()
        {
            if (DGVZonaCiudades.SelectedValue != null)
            {
                if (chklColonias.Items.Count != 0)
                {
                    foreach (ListItem item in chklColonias.Items)
                    {
                        if (item.Selected)
                        {

                            MVDireccion.SeleccionarColoniaEntrega(Guid.NewGuid(), new Guid(item.Value), new Guid(DGVZonaCiudades.SelectedValue.ToString()), StrNombreColonia: item.Text);
                        }
                        else
                        {
                            if (DGVZonaCiudades.SelectedValue != null)
                            {
                                MVDireccion.DeseleccionarColoniaEntrega(UidColonia: new Guid(item.Value), UidCiudad: new Guid(DGVZonaCiudades.SelectedValue.ToString()));
                            }
                        }
                    }
                }
            }
            if (DGVZRCiudades.SelectedValue != null)
            {
                if (chklZR.Items.Count != 0)
                {
                    foreach (ListItem item in chklZR.Items)
                    {
                        if (item.Selected)
                        {
                            MVDireccion.SeleccionarColoniaRecolecta(Guid.NewGuid(), new Guid(item.Value), new Guid(DGVZRCiudades.SelectedValue.ToString()), StrNombreColonia: item.Text);
                        }
                        else
                        {
                            if (DGVZRCiudades.SelectedValue != null)
                            {
                                MVDireccion.DeseleccionarColoniaRecolecta(UidColonia: new Guid(item.Value), UidCiudad: new Guid(DGVZRCiudades.SelectedValue.ToString()));
                            }
                        }
                    }
                }
            }
        }

        protected void SeleccionaColonias()
        {
            int contador = 0;
            if (chklColonias.Items.Count != 0)
            {
                foreach (ListItem item in chklColonias.Items)
                {
                    if (MVDireccion.ListaColoniasSeleccionadasEntrega.Exists(Col => Col.ID.ToString() == item.Value))
                    {
                        contador = contador + 1;
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }
            if (contador == chklColonias.Items.Count)
            {
                chkSeleccionarTodos.Checked = true;
            }
            else
            {
                chkSeleccionarTodos.Checked = false;
            }
        }

        protected void chkSeleccionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionarTodos.Checked)
            {
                SeleccionaTodasLasColoniasEntrega();
            }
            else
            {
                DeseleccionaCheckboxListColoniasEntrega();
            }
        }
        protected void chklZTSeleccionaTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chklZRSeleccionaTodos.Checked)
            {
                SeleccionaTodasLasColoniasRecolecta();
            }
            else
            {
                DeseleccionaCheckboxListColoniasRecolecta();
            }
        }
        protected void DeseleccionaCheckboxListColoniasEntrega()
        {
            //Recorre la lista y desselecciona los checkbox
            foreach (ListItem item in chklColonias.Items)
            {
                item.Selected = false;
            }
        }
        protected void DeseleccionaCheckboxListColoniasRecolecta()
        {
            //Recorre la lista y desselecciona los checkbox
            foreach (ListItem item in chklZR.Items)
            {
                item.Selected = false;
            }
        }
        protected void btnAgregarCiudad_Click(object sender, EventArgs e)
        {
            if (DDLZonaCiudad.SelectedItem != null)
            {
                Guid ciudad = new Guid(DDLZonaCiudad.SelectedItem.Value);
                MVDireccion.SeleccionarCiudadEntrega(ciudad);

                //Actualiza el datagrid
                DGVZonaCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasEntrega;
                DGVZonaCiudades.DataBind();

                if (DGVZonaCiudades.SelectedIndex != -1)
                {
                    LinkButton boton = DGVZonaCiudades.SelectedRow.FindControl("btnEliminaZona") as LinkButton;
                    boton.Enabled = false;
                    boton.CssClass = "btn btn-sm btn-default disabled";
                }

                DGVZonaCiudades.SelectedIndex = -1;

                //Verifica si existen colonias seleccionadas
                ColoniasSeleccionadas();

                DGVZonaCiudades.SelectedIndex = -1;
                LimpiaListaDeColoniasEntrega();
            }
            if (DDLZRCiudad.SelectedItem != null)
            {
                Guid ciudad = new Guid(DDLZRCiudad.SelectedItem.Value);
                MVDireccion.SeleccionarCiudadRecolecta(ciudad);

                //Actualiza el datagrid
                DGVZRCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasRecolecta;
                DGVZRCiudades.DataBind();

                if (DGVZRCiudades.SelectedIndex != -1)
                {
                    LinkButton boton = DGVZRCiudades.SelectedRow.FindControl("btnEliminaZona") as LinkButton;
                    boton.Enabled = false;
                    boton.CssClass = "btn btn-sm btn-default disabled";
                }

                DGVZRCiudades.SelectedIndex = -1;

                //Verifica si existen colonias seleccionadas
                ColoniasSeleccionadas();

                DGVZonaCiudades.SelectedIndex = -1;
                LimpiaListaDeColoniasEntrega();
            }

        }

        protected void DGVZonaCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Guarda las colonias seleccionadas
            ColoniasSeleccionadas();

            Guid ciudad = new Guid(DGVZonaCiudades.SelectedDataKey.Value.ToString());
            //Recupera la zona de servicio
            // MuestraZonaDeServicio();

            //Recarga la lista de colonias 
            chklColonias.DataSource = MVDireccion.Colonias(ciudad, "CheckboxList");
            chklColonias.DataTextField = "Nombre";
            chklColonias.DataValueField = "IdColonia";
            chklColonias.DataBind();

            SeleccionaCheckboxListColoniasEntrega();

            EstatusControlesZonaDeServicio(true, "Entrega");

            LinkButton boton = DGVZonaCiudades.SelectedRow.FindControl("btnEliminaZona") as LinkButton;
            boton.Enabled = true;
            boton.CssClass = "btn btn-sm btn-default";

            //Actualiza el datagrid
            DGVZonaCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasEntrega;
            DGVZonaCiudades.DataBind();



            //Si hay colonias seleccionadas las selecciona en el control
            SeleccionaColonias();
        }

        protected void DGVZRCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Guarda las colonias seleccionadas
            ColoniasSeleccionadas();

            Guid ciudad = new Guid(DGVZRCiudades.SelectedDataKey.Value.ToString());
            //Recupera la zona de servicio
            // MuestraZonaDeServicio();

            //Recarga la lista de colonias 
            chklZR.DataSource = MVDireccion.Colonias(ciudad, "CheckboxList");
            chklZR.DataTextField = "Nombre";
            chklZR.DataValueField = "IdColonia";
            chklZR.DataBind();

            SeleccionaCheckboxListColoniasRecolecta();

            EstatusControlesZonaDeServicio(true, "Recolecta");

            LinkButton boton = DGVZRCiudades.SelectedRow.FindControl("btnEliminaZona") as LinkButton;
            boton.Enabled = true;
            boton.CssClass = "btn btn-sm btn-default";

            //Actualiza el datagrid
            DGVZRCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasRecolecta;
            DGVZRCiudades.DataBind();



            //Si hay colonias seleccionadas las selecciona en el control
            SeleccionaColonias();
        }

        public void SeleccionaCheckboxListColoniasEntrega()
        {
            foreach (ListItem chk in chklColonias.Items)
            {
                if (MVDireccion.ListaColoniasSeleccionadasEntrega.Exists(objeto => objeto.ID.ToString() == chk.Value))
                {
                    chk.Selected = true;
                }
                else
                {
                    chk.Selected = false;
                }
            }
        }

        public void SeleccionaCheckboxListColoniasRecolecta()
        {
            foreach (ListItem chk in chklZR.Items)
            {
                if (MVDireccion.ListaColoniasSeleccionadasRecolecta.Exists(objeto => objeto.ID.ToString() == chk.Value))
                {
                    chk.Selected = true;
                }
                else
                {
                    chk.Selected = false;
                }
            }
        }

        protected void DGVZonaCiudades_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVZonaCiudades, "Select$" + e.Row.RowIndex);
                LinkButton boton = e.Row.FindControl("btnEliminaZona") as LinkButton;

                if (e.Row.RowIndex != DGVZonaCiudades.SelectedIndex)
                {
                    boton.Enabled = false;
                    boton.CssClass = "btn btn-sm btn-default disabled";
                }

            }
        }

        protected void DGVZonaCiudades_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                //Obtiene el datakey del gridzonadeservicio
                string uid = DGVZonaCiudades.DataKeys[index].Value.ToString();
                Guid uidCiudad = new Guid(uid);
                // MVDireccion.EliminaColoniasDeZonaDeServicio(uidCiudad, txtUidSucursal.Text);
                // MVDireccion.DeseleccionarCiudad(uidCiudad);


                //Limpia el checkboxlist
                chklColonias.DataSource = MVDireccion.Colonias(uidCiudad, "CheckboxList");
                chklColonias.DataTextField = "Nombre";
                chklColonias.DataValueField = "IdColonia";
                chklColonias.DataBind();

                foreach (ListItem item in chklColonias.Items)
                {
                    //Elimina las colonias asociadas a la ciudad
                    MVDireccion.DeseleccionarColoniaEntrega(UidCiudad: uidCiudad, UidColonia: new Guid(item.Value.ToString()));
                }

                //Limpia el checkboxlist
                LimpiaListaDeColoniasEntrega();

                //Elimina la ciudad de la lista
                MVDireccion.DeseleccionarCiudadEntrega(uidCiudad);

            }

        }
        protected void DGVZRCiudades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                //Obtiene el datakey del gridzonadeservicio
                string uid = DGVZRCiudades.DataKeys[index].Value.ToString();
                Guid uidCiudad = new Guid(uid);
                // MVDireccion.EliminaColoniasDeZonaDeServicio(uidCiudad, txtUidSucursal.Text);
                // MVDireccion.DeseleccionarCiudad(uidCiudad);


                //Limpia el checkboxlist
                chklZR.DataSource = MVDireccion.Colonias(uidCiudad, "CheckboxList");
                chklZR.DataTextField = "Nombre";
                chklZR.DataValueField = "IdColonia";
                chklZR.DataBind();

                foreach (ListItem item in chklZR.Items)
                {
                    //Elimina las colonias asociadas a la ciudad
                    MVDireccion.DeseleccionarColoniaEntrega(UidCiudad: uidCiudad, UidColonia: new Guid(item.Value.ToString()));
                }

                //Limpia el checkboxlist
                LimpiaListaDeColoniasRecolecta();

                //Elimina la ciudad de la lista
                MVDireccion.DeseleccionarCiudadEntrega(uidCiudad);

            }

        }
        protected void DGVZonaCiudades_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DGVZonaCiudades.SelectedIndex = -1;
            //Actualiza el datagrid
            DGVZonaCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasEntrega;
            DGVZonaCiudades.DataBind();
        }
        protected void DGVZRCiudades_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DGVZRCiudades.SelectedIndex = -1;
            //Actualiza el datagrid
            DGVZRCiudades.DataSource = MVDireccion.ListaCiudadesSeleccionadasRecolecta;
            DGVZRCiudades.DataBind();
        }
        protected void btnBusquedaColonia_Click(object sender, EventArgs e)
        {
            if (DGVZonaCiudades.SelectedIndex != -1)
            {
                //Guarda las colonias seleccionadas en la lista
                ColoniasSeleccionadas();


                if (string.IsNullOrWhiteSpace(txtBusquedaColonia.Text))
                {
                    //Retorna todas las colonias
                    chklColonias.DataSource = MVDireccion.Colonias(new Guid(DGVZonaCiudades.DataKeys[Convert.ToInt32(DGVZonaCiudades.SelectedIndex)].Value.ToString()), "CheckboxList");
                }
                else
                {
                    //Retorna colonias filtradas por nombre
                    chklColonias.DataSource = MVDireccion.Colonias(new Guid(DGVZonaCiudades.DataKeys[Convert.ToInt32(DGVZonaCiudades.SelectedIndex)].Value.ToString()), Nombre: txtBusquedaColonia.Text);
                }

                chklColonias.DataTextField = "Nombre";
                chklColonias.DataValueField = "IdColonia";
                chklColonias.DataBind();

                //Si hay colonias seleccionadas en la lista y selecciona en el control
                SeleccionaColonias();
            }
        }
        protected void btnZrBusquedaColonia_Click(object sender, EventArgs e)
        {
            if (DGVZRCiudades.SelectedIndex != -1)
            {
                //Guarda las colonias seleccionadas en la lista
                ColoniasSeleccionadas();

                if (string.IsNullOrWhiteSpace(txtZRBusquedaColonia.Text))
                {
                    //Retorna todas las colonias
                    chklZR.DataSource = MVDireccion.Colonias(new Guid(DGVZRCiudades.DataKeys[Convert.ToInt32(DGVZRCiudades.SelectedIndex)].Value.ToString()), "CheckboxList");
                }
                else
                {
                    //Retorna colonias filtradas por nombre
                    chklZR.DataSource = MVDireccion.Colonias(new Guid(DGVZRCiudades.DataKeys[Convert.ToInt32(DGVZRCiudades.SelectedIndex)].Value.ToString()), Nombre: txtZRBusquedaColonia.Text);
                }

                chklZR.DataTextField = "Nombre";
                chklZR.DataValueField = "IdColonia";
                chklZR.DataBind();

                //Si hay colonias seleccionadas en la lista y selecciona en el control
                SeleccionaColonias();
            }
        }
        protected void DDLTipoDeColonias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DGVZonaCiudades.SelectedIndex != -1)
            {
                string tipo = DDLTipoDeColonias.SelectedItem.Text;
                Guid ciudad = new Guid(DGVZonaCiudades.SelectedDataKey.Value.ToString());
                ColoniasSeleccionadas();
                if (chklColonias.Items.Count > 0)
                {
                    //Recarga la lista de colonias 
                    chklColonias.DataSource = MVDireccion.Colonias(ciudad, "CheckboxList");
                    chklColonias.DataTextField = "Nombre";
                    chklColonias.DataValueField = "IdColonia";
                    chklColonias.DataBind();
                    switch (tipo)
                    {
                        case "Todos":
                            SeleccionaCheckboxListColoniasEntrega();
                            break;
                        case "Seleccionados":
                            //Se crea la lista, 
                            List<ListItem> Seleccionados = new List<ListItem>();

                            //Recorre los elemtentos del control y si existe un valor, marca su checkbox
                            for (int i = 0; i < chklColonias.Items.Count; i++)
                            {
                                if (MVDireccion.ListaColoniasSeleccionadasEntrega.Exists(dir => dir.ID.ToString() == chklColonias.Items[i].Value))
                                { chklColonias.Items[i].Selected = true; }
                                else { Seleccionados.Add(chklColonias.Items[i]); }
                            }
                            //Si el elemto  existe en la lista, este lo elimina
                            for (int i = 0; i < Seleccionados.Count; i++)
                            {
                                chklColonias.Items.Remove(Seleccionados[i]);
                            }

                            break;
                        case "Deseleccionados":

                            List<ListItem> Deseleccionados = new List<ListItem>();

                            for (int i = 0; i < chklColonias.Items.Count; i++)
                            {
                                if (!MVDireccion.ListaColoniasSeleccionadasEntrega.Exists(dir => dir.ID.ToString() == chklColonias.Items[i].Value))
                                { chklColonias.Items[i].Selected = false; }
                                else { Deseleccionados.Add(chklColonias.Items[i]); }
                            }

                            for (int i = 0; i < Deseleccionados.Count; i++)
                            {
                                chklColonias.Items.Remove(Deseleccionados[i]);
                            }

                            break;
                        default:
                            break;
                    }
                }

            }
        }

        protected void SeleccionaTodasLasColoniasEntrega()
        {
            //Recorre la lista y desselecciona los checkbox
            foreach (ListItem item in chklColonias.Items)
            {
                item.Selected = true;
            }
        }
        protected void SeleccionaTodasLasColoniasRecolecta()
        {
            //Recorre la lista y desselecciona los checkbox
            foreach (ListItem item in chklZR.Items)
            {
                item.Selected = true;
            }
        }



        #endregion
        #region Licencia
        protected void DgvLicencia_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DgvLicencia, "Select$" + e.Row.RowIndex);
                //Iconos
                Label lblEstatus = e.Row.FindControl("lblEstatus") as Label;
                lblEstatus.Attributes.Add("data-placement", "bottom");
                lblEstatus.Attributes.Add("data-toggle", "tooltip");

                Label lblDisponibilidad = e.Row.FindControl("lblDisponibilidad") as Label;
                lblDisponibilidad.Attributes.Add("data-placement", "bottom");
                lblDisponibilidad.Attributes.Add("data-toggle", "tooltip");
                Label IconoEstatus = e.Row.FindControl("lblIconoEstatus") as Label;
                IconoEstatus.Attributes.Add("data-placement", "bottom");
                IconoEstatus.Attributes.Add("data-toggle", "tooltip");
                //Botones de acciones
                //LinkButton btnNuevo = e.Row.FindControl("btnNuevoLicencia") as LinkButton;
                //btnNuevo.Attributes.Add("data-placement", "bottom");
                //btnNuevo.Attributes.Add("data-toggle", "tooltip");
                LinkButton btnModificar = e.Row.FindControl("btnModificarLicencia") as LinkButton;
                btnModificar.Attributes.Add("data-placement", "bottom");
                btnModificar.Attributes.Add("data-toggle", "tooltip");
                //LinkButton btnEliminar = e.Row.FindControl("btnEliminarLicencia") as LinkButton;
                //btnEliminar.Attributes.Add("data-placement", "bottom");
                //btnEliminar.Attributes.Add("data-toggle", "tooltip");
                LinkButton btnRenovar = e.Row.FindControl("btnRenovarLicencia") as LinkButton;
                btnRenovar.Attributes.Add("data-placement", "bottom");
                btnRenovar.Attributes.Add("data-toggle", "tooltip");
                LinkButton btnestatus = e.Row.FindControl("btnEstatusLicencia") as LinkButton;
                btnestatus.Attributes.Add("data-placement", "bottom");
                btnestatus.Attributes.Add("data-toggle", "tooltip");
                TextBox txtIdentificador = e.Row.FindControl("lblIdentificador") as TextBox;
                //txtIdentificador.Attributes.Add("data-placement", "bottom");
                //txtIdentificador.Attributes.Add("data-toggle", "tooltip");




                //Estilos para el boton de estatus 
                if (e.Row.Cells[6].Text == "0")
                {
                    btnestatus.CssClass = "btn btn-sm btn-danger disabled";
                    btnestatus.ToolTip = "Desactivar";
                }
                if (e.Row.Cells[6].Text == "1")
                {
                    btnestatus.CssClass = "btn btn-sm btn-info disabled";
                    btnestatus.ToolTip = " Activar";
                }
                if (e.Row.Cells[7].Text == "True")
                {
                    lblDisponibilidad.CssClass = "glyphicon glyphicon-ban-circle";
                    lblDisponibilidad.ToolTip = "No disponible";
                }
                if (e.Row.Cells[7].Text == "False")
                {
                    lblDisponibilidad.CssClass = "glyphicon glyphicon-record";
                    lblDisponibilidad.ToolTip = "Disponible";
                }

                //Estilos para la modificacion del icono de estatus del boton de acciones y el icono en la columna
                if (e.Row.Cells[6].Text == "1")
                {
                    lblEstatus.CssClass = "glyphicon glyphicon-ok-sign";
                    lblEstatus.ToolTip = "Activo";
                    IconoEstatus.CssClass = "glyphicon glyphicon-remove";
                }
                if (e.Row.Cells[6].Text == "0")
                {
                    lblEstatus.CssClass = "glyphicon glyphicon-remove-sign";
                    lblEstatus.ToolTip = "Inactivo";
                    IconoEstatus.CssClass = "glyphicon glyphicon-ok";
                }
                //Da el estilo para las filas no seleccionadas
                if (DgvLicencia.SelectedIndex != e.Row.RowIndex)
                {
                    //Deshabilita los botones 
                    //btnNuevo.Enabled = false;
                    btnModificar.Enabled = false;
                    btnestatus.Enabled = false;
                    //btnEliminar.Enabled = false;
                    btnRenovar.Enabled = false;
                    txtIdentificador.Enabled = false;
                    //Estilos de css para los botones
                    //btnNuevo.CssClass = "btn btn-sm btn-success  disabled";
                    btnModificar.CssClass = "btn btn-sm disabled";
                    //btnEliminar.CssClass = "btn btn-sm btn-danger disabled";
                    btnRenovar.CssClass = "btn btn-sm btn-warning disabled";

                    //El boton estatus cambia de color dependiendo el estatus de la licencia
                    if (e.Row.Cells[6].Text == "1")
                    {
                        btnestatus.CssClass = "btn btn-sm btn-danger disabled";
                        btnestatus.ToolTip = "Activar";
                    }
                    if (e.Row.Cells[6].Text == "0")
                    {
                        btnestatus.CssClass = "btn btn-sm btn-info disabled";
                        btnestatus.ToolTip = "Desactivar";
                    }
                }
                else
                {
                    TextBox txtIdentificadorEdicion = e.Row.FindControl("txtIdentificador") as TextBox;
                    if (txtIdentificadorEdicion != null)
                    {
                        txtIdentificadorEdicion.Enabled = false;
                    }
                    //El boton estatus cambia de color dependiendo el estatus de la licencia
                    if (e.Row.Cells[6].Text == "1")
                    {
                        btnestatus.CssClass = "btn btn-sm btn-danger";
                        btnestatus.ToolTip = "Activar";
                    }
                    if (e.Row.Cells[6].Text == "0")
                    {
                        btnestatus.CssClass = "btn btn-sm btn-info";
                        btnestatus.ToolTip = "Desactivar";
                    }
                }
                //Muestra los botones dinamicos en el gridview con una fila y varias
                if ((MVLicencia.ListaDeLicencias.Count - 1) == e.Row.RowIndex)
                {
                    //btnNuevo.Visible = true;
                    btnModificar.Visible = true;
                    btnestatus.Visible = true;
                    //btnEliminar.Visible = false;
                    btnRenovar.Visible = true;
                }
                else
                {
                    //btnNuevo.Visible = false;
                    btnModificar.Visible = true;
                    btnestatus.Visible = true;
                    //btnEliminar.Visible = true;
                    btnRenovar.Visible = true;
                }

            }
        }

        protected void DgvLicencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            LinkButton boton = DgvLicencia.Rows[DgvLicencia.SelectedRow.RowIndex].FindControl("btnAceptarLicencia") as LinkButton;
            TextBox CajaDeTexto = DgvLicencia.Rows[DgvLicencia.SelectedRow.RowIndex].FindControl("txtIdentificador") as TextBox;

            if (!boton.Visible)
            {
                DgvLicencia.SelectedIndex = DgvLicencia.SelectedRow.RowIndex;
                DgvLicencia.EditIndex = -1;
                cargaGrid("Licencia");
            }
            else
            {
                if (CajaDeTexto != null)
                {
                    CajaDeTexto.Focus();
                }
            }

        }

        protected void btnEstatusLicencia_Click(object sender, EventArgs e)
        {
            int filaSeleccionada = DgvLicencia.SelectedRow.RowIndex;
            Guid UidLicencia = new Guid(DgvLicencia.DataKeys[filaSeleccionada].Value.ToString());
            MVLicencia.ActualizaEstatusLicenciaSucursal(UidLicencia);
            cargaGrid("Licencia");
        }

        protected void DgvLicencia_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = DgvLicencia.SelectedRow.RowIndex;

            LinkButton Edita = null;

            Edita = DgvLicencia.Rows[index].FindControl("btnModificarLicencia") as LinkButton;

            if (Edita != null)
            {
                if (Edita.Visible)
                {
                    if (Edita.CommandArgument == "Edicion")
                    {
                        //Indica cual fila fue seleccionada par aeditar
                        DgvLicencia.EditIndex = index;
                        //Actualiza el gridview
                        DgvLicencia.DataSource = MVLicencia.ListaDeLicencias;
                        DgvLicencia.DataBind();
                        //Obtiene los botones de la fila y los desaparece
                        //LinkButton Eliminar = DgvLicencia.Rows[index].FindControl("btnEliminarLicencia") as LinkButton;
                        LinkButton btnAceptar = DgvLicencia.Rows[index].FindControl("btnAceptarLicencia") as LinkButton;
                        LinkButton btnCancelar = DgvLicencia.Rows[index].FindControl("btnCancelarLicencia") as LinkButton;
                        LinkButton btnModificar = DgvLicencia.Rows[index].FindControl("btnModificarLicencia") as LinkButton;
                        LinkButton btnRenovar = DgvLicencia.Rows[index].FindControl("btnRenovarLicencia") as LinkButton;
                        LinkButton btnestatus = DgvLicencia.Rows[index].FindControl("btnEstatusLicencia") as LinkButton;
                        TextBox txtIdentificadorEdicion = DgvLicencia.Rows[index].FindControl("txtIdentificador") as TextBox;

                        txtIdentificadorEdicion.Enabled = true;
                        btnestatus.Visible = true;
                        btnAceptar.Visible = true;
                        btnCancelar.Visible = true;


                        //Eliminar.Enabled = false;
                        btnRenovar.Enabled = false;
                        btnestatus.Enabled = false;

                        btnestatus.CssClass = btnestatus.CssClass + " disabled";
                        //Eliminar.CssClass = Eliminar.CssClass + " disabled";
                        btnRenovar.CssClass = btnRenovar.CssClass + " disabled";

                    }
                }
            }
        }

        protected void DgvLicencia_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DgvLicencia.EditIndex = -1;
            DgvLicencia.SelectedIndex = -1;
            cargaGrid("Licencia");
        }

        protected void DgvLicencia_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Indica cual fila fue seleccionada par aeditar
            //DgvLicencia.EditIndex = e.RowIndex;
            //Actualiza el gridview
            DgvLicencia.DataSource = MVLicencia.ListaDeLicencias;
            DgvLicencia.DataBind();
            //Obtiene los botones de la fila y los desaparece
            int index = DgvLicencia.SelectedIndex;
            LinkButton Eliminar = DgvLicencia.Rows[index].FindControl("btnEliminarLicencia") as LinkButton;
            LinkButton Modificar = DgvLicencia.Rows[index].FindControl("btnModificarLicencia") as LinkButton;
            LinkButton btnestatus = DgvLicencia.Rows[index].FindControl("btnEstatusLicencia") as LinkButton;
            LinkButton btnAceptar = DgvLicencia.Rows[index].FindControl("btnAceptarLicencia") as LinkButton;
            LinkButton btnCancelar = DgvLicencia.Rows[index].FindControl("btnCancelarLicencia") as LinkButton;
            LinkButton btnRenovar = DgvLicencia.Rows[index].FindControl("btnRenovarLicencia") as LinkButton;

            btnAceptar.Visible = true;
            btnCancelar.Visible = true;
            Modificar.Enabled = false;
            Eliminar.Enabled = true;
            btnRenovar.Enabled = false;
            btnestatus.Enabled = false;

            btnestatus.CssClass = btnestatus.CssClass + " disabled";
            Modificar.CssClass = Modificar.CssClass + " disabled";
            btnRenovar.CssClass = btnRenovar.CssClass + " disabled";

        }

        protected void DgvLicencia_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Encuentra los botones
            //LinkButton Eliminar = DgvLicencia.Rows[e.RowIndex].FindControl("btnEliminarLicencia") as LinkButton;
            LinkButton Edita = DgvLicencia.Rows[e.RowIndex].FindControl("btnModificarLicencia") as LinkButton;
            //LinkButton Nuevo = DgvLicencia.Rows[e.RowIndex].FindControl("btnNuevoLicencia") as LinkButton;
            LinkButton btnAceptar = DgvLicencia.Rows[e.RowIndex].FindControl("btnAceptarLicencia") as LinkButton;
            LinkButton btnCancelar = DgvLicencia.Rows[e.RowIndex].FindControl("btnCancelarLicencia") as LinkButton;
            LinkButton Renova = DgvLicencia.Rows[e.RowIndex].FindControl("btnRenovarLicencia") as LinkButton;
            TextBox txtIdentificador = DgvLicencia.Rows[e.RowIndex].FindControl("txtIdentificador") as TextBox;
            Guid Sucursal = new Guid(txtUidSucursal.Text);
            //Agrega a lista
            //if (Nuevo.Visible)
            //{
            //    if (!string.IsNullOrWhiteSpace(txtIdentificador.Text))
            //    {
            //        Guid UidLicencia = new Guid(DgvLicencia.DataKeys[e.RowIndex].Value.ToString());
            //        if (UidLicencia == Guid.Empty)
            //        {
            //            UidLicencia = Guid.NewGuid();
            //        }
            //        MVLicencia.AgregaLicenciaALista(Sucursal, UidLicencia, txtIdentificador.Text);
            //        DgvLicencia.EditIndex = -1;
            //        DgvLicencia.SelectedIndex = -1;
            //        cargaGrid("Licencia");

            //    }
            //    else
            //    {
            //        txtIdentificador.BorderColor = Color.Red;
            //        txtIdentificador.ToolTip = "Campo Requerido";

            //    }
            //}
            //Modifica
            if (Edita.Enabled && Edita.Visible)
            {
                Guid UidLicencia = new Guid(DgvLicencia.DataKeys[e.RowIndex].Value.ToString());
                MVLicencia.ActualizaLicenciaEnLista(UidLicencia.ToString(), txtIdentificador.Text);
                DgvLicencia.EditIndex = -1;
                DgvLicencia.SelectedIndex = -1;
                cargaGrid("Licencia");
            }
            //Elimina
            //if (Eliminar.Enabled && Eliminar.Visible)
            //{
            //    Guid UidLicencia = new Guid(DgvLicencia.DataKeys[e.RowIndex].Value.ToString());
            //    MVLicencia.EliminaLicencia(UidLicencia);
            //    DgvLicencia.EditIndex = -1;
            //    DgvLicencia.SelectedIndex = -1;
            //    cargaGrid("Licencia");
            //}
            if (Renova.Enabled && Renova.Visible)
            {
                Guid UidLicencia = new Guid(DgvLicencia.DataKeys[e.RowIndex].Value.ToString());
                MVLicencia.ActualizaDisponibilidadLicenciaSucursal(UidLicencia);
                cargaGrid("Licencia");
            }
        }

        protected void btnNuevoLicencia_Click(object sender, EventArgs e)
        {
            int index = DgvLicencia.SelectedIndex;
            DgvLicencia.EditIndex = DgvLicencia.SelectedIndex;
            cargaGrid("Licencia");
            //Obtiene los botones de la fila y los desaparece
            LinkButton Eliminar = DgvLicencia.Rows[index].FindControl("btnEliminarLicencia") as LinkButton;
            LinkButton btnAceptar = DgvLicencia.Rows[index].FindControl("btnAceptarLicencia") as LinkButton;
            LinkButton btnCancelar = DgvLicencia.Rows[index].FindControl("btnCancelarLicencia") as LinkButton;
            LinkButton btnRenovar = DgvLicencia.Rows[index].FindControl("btnRenovarLicencia") as LinkButton;
            TextBox txtIdentificador = DgvLicencia.Rows[index].FindControl("txtIdentificador") as TextBox;

            txtIdentificador.Enabled = true;
            txtIdentificador.Focus();
            btnAceptar.Visible = true;
            btnCancelar.Visible = true;
            Eliminar.Visible = false;
            btnRenovar.Visible = false;
        }

        protected void btnRecargarSucursal_Click(object sender, EventArgs e)
        {
            AccionesDeLaPagina = string.Empty;
            Session.Remove("Accion");
            textboxActivados();
            MuestraSucursalEnGestion(txtUidSucursal.Text);
        }

        protected void chkbSeleccion_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.Parent.Parent;
            string dataKey = dgvBusquedaDeEmpresa.DataKeys[gr.RowIndex].Value.ToString();
            if (chk.Checked)
            {
                // MVContrato.SeleccionarEmpresa(new Guid(dataKey));
            }
            if (!chk.Checked)
            {
                MVContrato.DeseleccionarEmpresa(new Guid(dataKey));
            }
        }

        protected void btnGenerarCodigoDeBusqueda_Click(object sender, EventArgs e)
        {
            int[] Numeros = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] vocales = new char[5] { 'a', 'e', 'i', 'o', 'u' };
            Random rnd = new Random();
            string ClaveDeBusqueda = string.Empty;
            for (int i = 0; i <= 1; i++)
            {
                ClaveDeBusqueda = ClaveDeBusqueda + Numeros[rnd.Next(1, 10)].ToString() + alpha[rnd.Next(1, 26)].ToString() + vocales[rnd.Next(1, 5)].ToString();
            }
            txtClaveDeBusqueda.Text = Guid.NewGuid().ToString();
        }

        protected void DGVZRCiudades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVZRCiudades, "Select$" + e.Row.RowIndex);
                LinkButton boton = e.Row.FindControl("btnEliminaZona") as LinkButton;

                if (e.Row.RowIndex != DGVZRCiudades.SelectedIndex)
                {
                    boton.Enabled = false;
                    boton.CssClass = "btn btn-sm btn-default disabled";
                }
            }
        }

        protected void ddlZRTIpoSeleccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DGVZRCiudades.SelectedIndex != -1)
            {
                string tipo = ddlZRTIpoSeleccion.SelectedItem.Text;
                Guid ciudad = new Guid(DGVZRCiudades.SelectedDataKey.Value.ToString());
                ColoniasSeleccionadas();
                if (chklZR.Items.Count > 0)
                {
                    //Recarga la lista de colonias 
                    chklZR.DataSource = MVDireccion.Colonias(ciudad, "CheckboxList");
                    chklZR.DataTextField = "Nombre";
                    chklZR.DataValueField = "IdColonia";
                    chklZR.DataBind();
                    switch (tipo)
                    {
                        case "Todos":
                            SeleccionaCheckboxListColoniasRecolecta();
                            break;
                        case "Seleccionados":
                            List<ListItem> Seleccionados = new List<ListItem>();

                            for (int i = 0; i < chklZR.Items.Count; i++)
                            {
                                if (MVDireccion.ListaColoniasSeleccionadasRecolecta.Exists(dir => dir.ID.ToString() == chklZR.Items[i].Value))
                                { chklZR.Items[i].Selected = true; }
                                else { Seleccionados.Add(chklZR.Items[i]); }
                            }

                            for (int i = 0; i < Seleccionados.Count; i++)
                            {
                                chklZR.Items.Remove(Seleccionados[i]);
                            }

                            break;
                        case "Deseleccionados":

                            List<ListItem> Deseleccionados = new List<ListItem>();

                            for (int i = 0; i < chklZR.Items.Count; i++)
                            {
                                if (!MVDireccion.ListaColoniasSeleccionadasRecolecta.Exists(dir => dir.ID.ToString() == chklZR.Items[i].Value))
                                { chklZR.Items[i].Selected = false; }
                                else { Deseleccionados.Add(chklZR.Items[i]); }
                            }

                            for (int i = 0; i < Deseleccionados.Count; i++)
                            {
                                chklZR.Items.Remove(Deseleccionados[i]);
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
        }

        protected void btnRenovarLicencia_Click(object sender, EventArgs e)
        {
            int index = DgvLicencia.SelectedIndex;
            cargaGrid("Licencia");
            //LinkButton Eliminar = DgvLicencia.Rows[index].FindControl("btnEliminarLicencia") as LinkButton;
            LinkButton Renova = DgvLicencia.Rows[index].FindControl("btnRenovarLicencia") as LinkButton;
            LinkButton Edita = DgvLicencia.Rows[index].FindControl("btnModificarLicencia") as LinkButton;
            LinkButton Nuevo = DgvLicencia.Rows[index].FindControl("btnNuevoLicencia") as LinkButton;
            LinkButton btnAceptar = DgvLicencia.Rows[index].FindControl("btnAceptarLicencia") as LinkButton;
            LinkButton btnCancelar = DgvLicencia.Rows[index].FindControl("btnCancelarLicencia") as LinkButton;

            //Eliminar.Enabled = false;
            Edita.Enabled = false;
            Renova.Enabled = true;

            btnAceptar.Visible = true;
            btnCancelar.Visible = true;

            Edita.CssClass = Edita.CssClass + " disabled";

            //Eliminar.CssClass = Eliminar.CssClass + " disabled";


        }

        #endregion

        #region Contrato

        protected void dgvBusquedaDeEmpresa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvBusquedaDeEmpresa, "Select$" + e.Row.RowIndex);
                GridViewRow PagerRow = dgvBusquedaDeEmpresa.TopPagerRow;
                LinkButton btn = e.Row.FindControl("btnEstatusContrato") as LinkButton;
                btn.Attributes.Add("data-placement", "bottom");
                btn.Attributes.Add("data-toggle", "tooltip");
                System.Web.UI.WebControls.Image imgEstatus = e.Row.FindControl("lblIconoEstatusContrato") as System.Web.UI.WebControls.Image;
                imgEstatus.Attributes.Add("data-placement", "bottom");
                imgEstatus.Attributes.Add("data-toggle", "tooltip");
                System.Web.UI.WebControls.Image imgIconoEstatus = e.Row.FindControl("imgEstatus") as System.Web.UI.WebControls.Image;
                imgEstatus.Attributes.Add("data-placement", "bottom");
                imgEstatus.Attributes.Add("data-toggle", "tooltip");
                LinkButton btnAceptar = e.Row.FindControl("btnAceptar") as LinkButton;
                btnAceptar.Attributes.Add("data-placement", "bottom");
                btnAceptar.Attributes.Add("data-toggle", "tooltip");
                LinkButton btnCancelar = e.Row.FindControl("btnCancelar") as LinkButton;
                btnCancelar.Attributes.Add("data-placement", "bottom");
                btnCancelar.Attributes.Add("data-toggle", "tooltip");

                btn.Visible = true;
                btnAceptar.Visible = false;
                btnCancelar.Visible = false;

                Guid UidSucursalDistribuidora = new Guid();
                Guid UidSucursalSuministradora = new Guid();
                if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString()))
                {
                    UidSucursalSuministradora = new Guid(txtUidSucursal.Text);
                    UidSucursalDistribuidora = new Guid(dgvBusquedaDeEmpresa.DataKeys[e.Row.RowIndex].Value.ToString());

                    if (MVContrato.ListaDeSucursalesEnContrato.Exists(emp => emp.UidSucursalSuministradora == UidSucursalSuministradora && emp.UidSucursalDistribuidora == UidSucursalDistribuidora))
                    {
                        var objeto = MVContrato.ListaDeSucursalesEnContrato.Find(emp => emp.UidSucursalSuministradora == UidSucursalSuministradora && emp.UidSucursalDistribuidora == UidSucursalDistribuidora);
                        //Valida estatus de contrato en contratado
                        if (objeto.UidEstatus == new Guid("CD20F9BF-EBA2-4128-88FB-647544457B2D"))
                        {
                            imgEstatus.ImageUrl = "Img\\Iconos\\Remove.png";
                            imgIconoEstatus.ImageUrl = "Img\\Iconos\\Contratado.png"; ;
                            imgIconoEstatus.ToolTip = "Contratado";
                            btn.CssClass = "btn btn-sm btn-danger";
                            btn.ToolTip = "Cancelar contrato";
                        }
                        //Valida estatus de contrato en pendiente
                        if (objeto.UidEstatus == new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343") && objeto.BlConfirmacionDistribuidora && !objeto.BlConfirmacionSuministadora)
                        {
                            imgEstatus.ImageUrl = "Img\\Iconos\\Pendiente.png";
                            imgIconoEstatus.ImageUrl = "Img\\Iconos\\Pendiente.png";
                            imgIconoEstatus.ToolTip = "Confirmar pedido";
                            btn.CssClass = "btn btn-sm btn-info";
                            btn.ToolTip = "Confirmar o rechazar contrato";
                        }
                        else if (objeto.UidEstatus == new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343") && !objeto.BlConfirmacionDistribuidora && objeto.BlConfirmacionSuministadora)
                        {
                            imgEstatus.ImageUrl = "Img\\Iconos\\Remove.png";
                            imgIconoEstatus.ImageUrl = "Img\\Iconos\\Pendiente.png";
                            imgIconoEstatus.ToolTip = "En espera de confirmacion";
                            btn.CssClass = "btn btn-sm btn-danger";
                            btn.ToolTip = "Cancelar contrato";
                        }
                    }
                    else
                    {
                        imgEstatus.ImageUrl = "Img\\Iconos\\sd.png";
                        imgIconoEstatus.ImageUrl = "Img\\Iconos\\sd.png";
                        imgIconoEstatus.ToolTip = "Disponible";
                        btn.CssClass = "btn btn-sm btn-success";
                        btn.ToolTip = "Solicitar contrato";
                    }
                }
                else
                {
                    UidSucursalSuministradora = new Guid(dgvBusquedaDeEmpresa.DataKeys[e.Row.RowIndex].Value.ToString());
                    UidSucursalDistribuidora = new Guid(txtUidSucursal.Text);
                    if (MVContrato.ListaDeSucursalesEnContrato.Exists(emp => emp.UidSucursalSuministradora == UidSucursalSuministradora && emp.UidSucursalDistribuidora == UidSucursalDistribuidora))
                    {
                        var objeto = MVContrato.ListaDeSucursalesEnContrato.Find(emp => emp.UidSucursalSuministradora == UidSucursalSuministradora && emp.UidSucursalDistribuidora == UidSucursalDistribuidora);
                        //Valida estatus de contrato en contratado
                        if (objeto.UidEstatus == new Guid("CD20F9BF-EBA2-4128-88FB-647544457B2D"))
                        {
                            imgEstatus.ImageUrl = "Img\\Iconos\\Remove.png";
                            imgIconoEstatus.ImageUrl = "Img\\Iconos\\Contratado.png"; ;
                            imgIconoEstatus.ToolTip = "Contratado";
                            btn.CssClass = "btn btn-sm btn-danger";
                            btn.ToolTip = "Cancelar contrato";
                        }
                        //Valida estatus de contrato en pendiente
                        if (objeto.UidEstatus == new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343") && !objeto.BlConfirmacionDistribuidora && objeto.BlConfirmacionSuministadora)
                        {
                            imgEstatus.ImageUrl = "Img\\Iconos\\Pendiente.png";
                            imgIconoEstatus.ImageUrl = "Img\\Iconos\\Pendiente.png";
                            imgIconoEstatus.ToolTip = "Confirmar pedido";
                            btn.CssClass = "btn btn-sm btn-warning";
                            btn.ToolTip = "Confirmar o rechazar contrato";
                        }
                        else if (objeto.UidEstatus == new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343") && objeto.BlConfirmacionDistribuidora && !objeto.BlConfirmacionSuministadora)
                        {
                            imgEstatus.ImageUrl = "Img\\Iconos\\Remove.png";
                            imgIconoEstatus.ImageUrl = "Img\\Iconos\\Pendiente.png";
                            imgIconoEstatus.ToolTip = "En espera de confirmacion";
                            btn.CssClass = "btn btn-sm btn-danger";
                            btn.ToolTip = "Cancelar contrato";
                        }
                    }
                    else
                    {
                        imgEstatus.ImageUrl = "Img\\Iconos\\sd.png";
                        imgIconoEstatus.ImageUrl = "Img\\Iconos\\sd.png";
                        imgIconoEstatus.ToolTip = "Disponible";
                        btn.CssClass = "btn btn-sm btn-success";
                        btn.ToolTip = "Solicitar contrato";
                    }
                }
            }
        }

        protected void dgvBusquedaDeEmpresa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Contrato")
            {
                Guid UidSuministradora = Guid.Empty;
                Guid UidDistribuidora = Guid.Empty;
                int index = int.Parse(e.CommandArgument.ToString());
                LinkButton btn = dgvBusquedaDeEmpresa.Rows[index].FindControl("btnEstatusContrato") as LinkButton;
                LinkButton btnAceptar = dgvBusquedaDeEmpresa.Rows[index].FindControl("btnAceptar") as LinkButton;
                LinkButton btnCancelar = dgvBusquedaDeEmpresa.Rows[index].FindControl("btnCancelar") as LinkButton;

                System.Web.UI.WebControls.Image imgEstatus = dgvBusquedaDeEmpresa.Rows[index].FindControl("lblIconoEstatusContrato") as System.Web.UI.WebControls.Image;
                var objeto = new VMContrato();
                if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString()))
                {
                    UidSuministradora = new Guid(txtUidSucursal.Text);
                    UidDistribuidora = new Guid(dgvBusquedaDeEmpresa.DataKeys[index].Value.ToString());
                    if (MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora && cont.UidEstatus == new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343")))
                    {
                        //Contratado
                        objeto = MVContrato.ListaDeSucursalesEnContrato.Find(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora);
                        if (objeto.BlConfirmacionDistribuidora)
                        {
                            //Estatus contratado
                            if (objeto.UidEstatus == new Guid("CD20F9BF-EBA2-4128-88FB-647544457B2D"))
                            {
                                btn.Visible = false;
                                btnAceptar.Visible = true;
                                btnCancelar.Visible = true;
                            }
                            else
                            {
                                btn.Visible = false;
                                btnAceptar.Visible = true;
                                btnCancelar.Visible = true;
                            }
                        }
                        else
                        {
                            btn.Visible = false;
                            btnAceptar.Visible = true;
                            btnCancelar.Visible = true;
                        }
                    }
                    else
                    {
                        if (MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora && cont.UidEstatus == new Guid("CD20F9BF-EBA2-4128-88FB-647544457B2D")))
                        {
                            btn.Visible = false;
                            btnAceptar.Visible = true;
                            btnCancelar.Visible = true;
                        }
                        else
                        {
                            btn.Visible = false;
                            btnAceptar.Visible = true;
                            btnCancelar.Visible = true;
                        }
                    }
                }
                else
                {
                    UidSuministradora = new Guid(dgvBusquedaDeEmpresa.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
                    UidDistribuidora = new Guid(txtUidSucursal.Text);
                    if (MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora && cont.UidEstatus == new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343")))
                    {
                        objeto = MVContrato.ListaDeSucursalesEnContrato.Find(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora);
                        if (objeto.BlConfirmacionSuministadora)
                        {
                            //Estatus contratado
                            if (objeto.UidEstatus == new Guid("CD20F9BF-EBA2-4128-88FB-647544457B2D"))
                            {
                                btn.Visible = false;
                                btnAceptar.Visible = true;
                                btnCancelar.Visible = true;
                            }
                            else
                            {
                                btn.Visible = false;
                                btnAceptar.Visible = true;
                                btnCancelar.Visible = true;
                            }
                        }
                        else
                        {
                            btn.Visible = false;
                            btnAceptar.Visible = true;
                            btnCancelar.Visible = true;
                        }
                    }
                    else
                    {
                        if (MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora && cont.UidEstatus == new Guid("CD20F9BF-EBA2-4128-88FB-647544457B2D")))
                        {
                            btn.Visible = false;
                            btnAceptar.Visible = true;
                            btnCancelar.Visible = true;
                        }
                        else
                        {
                            btn.Visible = false;
                            btnAceptar.Visible = true;
                            btnCancelar.Visible = true;
                        }
                    }
                }

            }
            if (e.CommandName == "Aceptar")
            {
                Guid UidSuministradora = Guid.Empty;
                Guid UidDistribuidora = Guid.Empty;
                int index = int.Parse(e.CommandArgument.ToString());

                //Suministradora
                if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString()))
                {
                    UidSuministradora = new Guid(txtUidSucursal.Text);
                    UidDistribuidora = new Guid(dgvBusquedaDeEmpresa.DataKeys[index].Value.ToString());

                    if (!MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora))
                    {
                        //Pendiente
                        //if (MVTarifario.ListaDeTarifariosSeleccionados.Exists(t => t.GuidSucursalDistribuidora == UidDistribuidora))
                        //{
                        Guid UidContrato = Guid.NewGuid();
                        var objeto = new VMContrato() { Uid = UidContrato, UidEstatus = new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343"), UidSucursalSuministradora = UidSuministradora, UidSucursalDistribuidora = UidDistribuidora, BlConfirmacionSuministadora = true, BlConfirmacionDistribuidora = false };
                        MVContrato.ListaDeSucursalesEnContrato.Add(objeto);
                        //List<VMTarifario> tarifariosNuevos = new List<VMTarifario>();
                        //List<VMTarifario> TarifariosViejos = new List<VMTarifario>();
                        ////Envia correo electronico a la sucursal si su colonia fue retirada del tarifario a usar
                        // tarifariosNuevos = MVTarifario.ListaDeTarifariosSeleccionados.FindAll(T=>T.UidContrato == Guid.Empty);
                        // TarifariosViejos = MVTarifario.ListaDeTarifariosSeleccionados.FindAll(T=>T.UidContrato == Guid.Empty);
                        //foreach (var item in tarifariosNuevos)
                        //{
                        //    if (TarifariosViejos.Exists(t=>t.UidRelacionZE == item.UidRelacionZE))
                        //    {
                        //        VMAcceso Correo = new VMAcceso();
                        //        // queda pendiente el envio del correo electronico
                        //        string sucursalSuministradora = txtNombreDeSucursal.Text;
                        //        //string Distribuidora =  (UidDistribuidora);
                        //       // Correo.CorreoDeInformacionDeCambioDeTarifario();
                        //        var registro = MVTarifario.ListaDeTarifariosSeleccionados.Find(t=>t.UidTarifario ==item.UidTarifario);
                        //        MVTarifario.ListaDeTarifariosSeleccionados.Remove(registro);
                        //    }
                        //}

                        Guid UidSucursalDistribuidora = new Guid(dgvBusquedaDeEmpresa.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
                        MVTarifario.BuscarTarifario("Gestion", uidSucursal: UidSucursalDistribuidora.ToString(), UidZonaRecolecta: DDLDColonia.SelectedItem.Value);
                        foreach (var item in MVTarifario.ListaDeTarifarios)
                        {
                            MVTarifario.SeleccionarTarifario(item.UidTarifario, UidContrato, UidSucursalDistribuidora);
                        }
                        MVTarifario.GuardaTarifarioDeContrato(UidContrato, UidSucursalDistribuidora);
                        //}
                        //else
                        //{
                        //    PanelMensaje.Visible = true;
                        //    LblMensaje.Text = "No se puede guardar un contrato sin haber seleccionado una zona de entrega";
                        //}
                    }
                    else
                    {
                        //Pendiente a contratar
                        if (MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora && cont.UidEstatus == new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343") && !cont.BlConfirmacionSuministadora && cont.BlConfirmacionDistribuidora))
                        {
                            if (MVTarifario.ListaDeTarifariosSeleccionados.Exists(t => t.GuidSucursalDistribuidora == UidDistribuidora))
                            {
                                var objeto = MVContrato.ListaDeSucursalesEnContrato.Find(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora);
                                objeto.UidEstatus = new Guid("CD20F9BF-EBA2-4128-88FB-647544457B2D");
                                objeto.BlConfirmacionSuministadora = true;
                                Guid UidSucursalDistribuidora = new Guid(dgvBusquedaDeEmpresa.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
                                MVTarifario.GuardaTarifarioDeContrato(objeto.Uid, UidSucursalDistribuidora);
                            }
                            else
                            {
                                PanelMensaje.Visible = true;
                                LblMensaje.Text = "No se puede aceptar un contrato sin haber seleccionado una zona de entrega";
                            }
                        }
                        //Cancela el contrato pendiente
                        else if (MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora && cont.UidEstatus == new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343") && cont.BlConfirmacionSuministadora && !cont.BlConfirmacionDistribuidora))
                        {
                            var objeto = MVContrato.ListaDeSucursalesEnContrato.Find(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora);
                            MVContrato.ListaDeSucursalesEnContrato.Remove(objeto);
                            List<VMTarifario> registros = new List<VMTarifario>();
                            registros = MVTarifario.ListaDeTarifariosSeleccionados.FindAll(t => t.GuidSucursalDistribuidora == UidDistribuidora);
                            for (int i = 0; i < registros.Count; i++)
                            {
                                var c = MVTarifario.ListaDeTarifariosSeleccionados.Find(t => t.UidTarifario == registros[i].UidTarifario);
                                MVTarifario.ListaDeTarifariosSeleccionados.Remove(c);
                            }
                        }
                        else
                        //Elimina el contrato contratado
                        if (MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora && cont.UidEstatus == new Guid("CD20F9BF-EBA2-4128-88FB-647544457B2D") && cont.BlConfirmacionSuministadora && cont.BlConfirmacionDistribuidora))
                        {
                            var objeto = MVContrato.ListaDeSucursalesEnContrato.Find(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora);
                            MVContrato.ListaDeSucursalesEnContrato.Remove(objeto);
                            List<VMTarifario> registros = new List<VMTarifario>();
                            registros = MVTarifario.ListaDeTarifariosSeleccionados.FindAll(t => t.GuidSucursalDistribuidora == UidDistribuidora);
                            for (int i = 0; i < registros.Count; i++)
                            {
                                var c = MVTarifario.ListaDeTarifariosSeleccionados.Find(t => t.UidTarifario == registros[i].UidTarifario);
                                MVTarifario.ListaDeTarifariosSeleccionados.Remove(c);
                            }

                        }
                    }
                }
                //Distribuidora
                else
                {
                    UidSuministradora = new Guid(dgvBusquedaDeEmpresa.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
                    UidDistribuidora = new Guid(txtUidSucursal.Text);
                    if (!MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora))
                    {
                        //Pendiente
                        var objeto = new VMContrato() { Uid = Guid.NewGuid(), UidEstatus = new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343"), UidSucursalSuministradora = UidSuministradora, UidSucursalDistribuidora = UidDistribuidora, BlConfirmacionSuministadora = false, BlConfirmacionDistribuidora = true };
                        MVContrato.ListaDeSucursalesEnContrato.Add(objeto);
                    }
                    else
                    {
                        //Pendiente a contratar
                        if (MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora && cont.UidEstatus == new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343") && cont.BlConfirmacionSuministadora && !cont.BlConfirmacionDistribuidora))
                        {
                            var objeto = MVContrato.ListaDeSucursalesEnContrato.Find(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora);
                            objeto.UidEstatus = new Guid("CD20F9BF-EBA2-4128-88FB-647544457B2D");
                            objeto.BlConfirmacionDistribuidora = true;
                        }
                        //Cancela el contrato pendiente
                        else if (MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora && cont.UidEstatus == new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343") && !cont.BlConfirmacionSuministadora && cont.BlConfirmacionDistribuidora))
                        {
                            var objeto = MVContrato.ListaDeSucursalesEnContrato.Find(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora);
                            MVContrato.ListaDeSucursalesEnContrato.Remove(objeto);
                        }
                        else
                        //Elimina el contrato contratado
                       if (MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora && cont.UidEstatus == new Guid("CD20F9BF-EBA2-4128-88FB-647544457B2D") && cont.BlConfirmacionSuministadora && cont.BlConfirmacionDistribuidora))
                        {
                            var objeto = MVContrato.ListaDeSucursalesEnContrato.Find(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora);
                            MVContrato.ListaDeSucursalesEnContrato.Remove(objeto);
                            List<VMTarifario> registros = new List<VMTarifario>();
                            registros = MVTarifario.ListaDeTarifariosSeleccionados.FindAll(t => t.UidContrato == objeto.Uid);
                            for (int i = 0; i < registros.Count; i++)
                            {
                                var c = MVTarifario.ListaDeTarifariosSeleccionados.Find(t => t.UidTarifario == registros[i].UidTarifario);
                                MVTarifario.ListaDeTarifariosSeleccionados.Remove(c);
                            }
                        }
                    }
                }
                cargaGrid("Empresas");
            }
            if (e.CommandName == "Cancelar")
            {
                Guid UidSuministradora = Guid.Empty;
                Guid UidDistribuidora = Guid.Empty;
                VMContrato objeto = new VMContrato();
                int index = int.Parse(e.CommandArgument.ToString());
                //Suminitradora
                if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString()))
                {
                    UidSuministradora = new Guid(txtUidSucursal.Text);
                    UidDistribuidora = new Guid(dgvBusquedaDeEmpresa.DataKeys[index].Value.ToString());
                    objeto = MVContrato.ListaDeSucursalesEnContrato.Find(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora);

                    if (objeto != null)
                    {
                        if (objeto.UidEstatus == new Guid("5719c82e-6e7f-42a7-9c56-0f75dfef0343"))
                        {
                            MVTarifario.DeseleccionarTarifario(UidContrato: objeto.Uid);
                            MVContrato.ListaDeSucursalesEnContrato.Remove(objeto);
                        }
                    }
                }
                else
                {
                    UidSuministradora = new Guid(dgvBusquedaDeEmpresa.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
                    UidDistribuidora = new Guid(txtUidSucursal.Text);
                    objeto = MVContrato.ListaDeSucursalesEnContrato.Find(cont => cont.UidSucursalSuministradora == UidSuministradora && cont.UidSucursalDistribuidora == UidDistribuidora);
                    if (objeto.UidEstatus == new Guid("5719c82e-6e7f-42a7-9c56-0f75dfef0343"))
                    {
                        MVTarifario.DeseleccionarTarifario(UidContrato: objeto.Uid);
                        MVContrato.ListaDeSucursalesEnContrato.Remove(objeto);
                    }
                }
                cargaGrid("Empresas");
                LinkButton btn = dgvBusquedaDeEmpresa.Rows[index].FindControl("btnEstatusContrato") as LinkButton;
                LinkButton btnAceptar = dgvBusquedaDeEmpresa.Rows[index].FindControl("btnAceptar") as LinkButton;
                LinkButton btnCancelar = dgvBusquedaDeEmpresa.Rows[index].FindControl("btnCancelar") as LinkButton;

                btn.Visible = true;
                btnAceptar.Visible = false;
                btnCancelar.Visible = false;
            }
            if (e.CommandName == "Informacion")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                string UidSucursal = dgvBusquedaDeEmpresa.DataKeys[index].Value.ToString();
                lblIndexContrato.Text = index.ToString();
                //Obtiene los datos de la direccion de la sucursal
                VMDireccion d = new VMDireccion();
                d.ObtenerDireccionSucursal(UidSucursal);
                lblInformacionDireccionEmpresaContrato.Text = d.ObtenerNombreDeLaCiudad(d.CIUDAD) + "," + d.ObtenerNombreDeLaColonia(d.COLONIA) + "," + d.CALLE0 + "," + d.CodigoPostal;
                VMCorreoElectronico ce = new VMCorreoElectronico();

                ce.BuscarCorreos(UidPropietario: MVSucursales.ObtenerUidEmpresa(UidSucursal), strParametroDebusqueda: "Empresa");

                HlnkCorreoElectronico.NavigateUrl = "mailto:" + ce.CORREO;
                HlnkCorreoElectronico.Text = ce.CORREO;

                //Obtiene la informacion del telefono de la empresa seleccionada
                MVTelefono.ListaDeTelefonosInformacion.Clear();
                MVTelefono.ObtenerTelefonosSucursal(UidSucursal, "Informacion");
                DGVInformacionTelefonica.DataSource = MVTelefono.ListaDeTelefonosInformacion;
                DGVInformacionTelefonica.DataBind();

                lblInformacionNombreSucursal.Text = dgvBusquedaDeEmpresa.Rows[index].Cells[4].Text;
                PanelDeInformacion.Visible = true;

                //Oculta los botones para la edicion del contrato
                btnEditarContrato.Visible = false;
                btnAceptarEdicionContrato.Visible = false;
                PanelMensajeContrato.Visible = false;
                //Obtiene la informacion del tarifario dependiendo de la zona de recoleccion de la empresa suministradora
                if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString())) // si es empresa suministradora
                {
                    MVTarifario.BuscarTarifario("Gestion", uidSucursal: UidSucursal, UidZonaRecolecta: DDLDColonia.SelectedItem.Value);
                    DgvInformacionTarifario.DataSource = MVTarifario.ListaDeTarifarios;
                    DgvInformacionTarifario.DataBind();
                    //for (int i = 0; i < MVTarifario.ListaDeTarifariosSeleccionados.Count; i++)
                    //{
                    //    if (MVTarifario.ListaDeTarifariosSeleccionados[i].UidContrato == Guid.Empty)
                    //    {
                    //        var objeto = MVTarifario.ListaDeTarifariosSeleccionados[i];
                    //        MVTarifario.ListaDeTarifariosSeleccionados.Remove(objeto);
                    //        i = i - 1;
                    //    }
                    //}

                    foreach (GridViewRow item in DgvInformacionTarifario.Rows)
                    {
                        CheckBox chk = item.FindControl("chkbTarifario") as CheckBox;

                        item.Cells[2].Text = "$" + item.Cells[2].Text;

                        chk.Checked = true;
                        if (MVTarifario.ListaDeTarifariosSeleccionados.Exists(t => t.GuidSucursalDistribuidora == new Guid(UidSucursal)))
                        {
                            if (!MVTarifario.ListaDeTarifariosSeleccionados.Exists(t => t.UidTarifario.ToString() == DgvInformacionTarifario.DataKeys[item.RowIndex].Value.ToString()))
                            {
                                chk.Checked = false;
                            }
                        }
                    }
                    if (MVContrato.ListaDeSucursalesEnContrato.Exists(c => c.UidSucursalDistribuidora.ToString() == UidSucursal))
                    {
                        DgvInformacionTarifario.Enabled = false;
                        var registro = MVContrato.ListaDeSucursalesEnContrato.Find(c => c.UidSucursalDistribuidora.ToString() == UidSucursal);
                        //Los botones se muestran siempre y cuando el contrato ya haya sido contratado
                        if (registro.UidEstatus == new Guid("CD20F9BF-EBA2-4128-88FB-647544457B2D") || registro.UidEstatus == new Guid("5719C82E-6E7F-42A7-9C56-0F75DFEF0343"))
                        {
                            btnEditarContrato.Visible = true;
                            btnAceptarEdicionContrato.Visible = false;
                        }
                    }
                    else
                    {
                        DgvInformacionTarifario.Enabled = true;
                    }
                }
                else
                {
                    MVTarifario.BuscarTarifario("Informacion distribuidora", uidSucursal: UidSucursal, UidSucursalDistribuidora: txtUidSucursal.Text);
                    DGVInformacionTarifarioDistribuidora.DataSource = MVTarifario.ListaDeTarifarios;
                    DGVInformacionTarifarioDistribuidora.DataBind();
                }


            }
        }

        protected void btnBuscarEmpresa_Click(object sender, EventArgs e)
        {
            string Identificador = txtBIdentificador.Text;
            string HA = txtBHoraApertura.Text;
            string HC = txtBHoraDeCierre.Text;
            string codigo = txtBCodigo.Text;
            int TipodeEmpresa = 0;
            Guid UidColonia = new Guid(DDLDColonia.SelectedItem.Value);
            Guid UidSucursal = new Guid(txtUidSucursal.Text);
            MVSucursales.ListaDeSucursalesDeContrato.Clear();
            //Valida si la empresa es suministradora
            if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString()))
            {
                TipodeEmpresa = 1;
            }
            else
            {
                //La empresa es distribuidora
                TipodeEmpresa = 2;
            }
            if (!string.IsNullOrWhiteSpace(codigo))
            {
                txtBHoraApertura.Text = string.Empty;
                txtBHoraDeCierre.Text = string.Empty;
                txtBIdentificador.Text = string.Empty;
                MVSucursales.BuscarSucursalesContrato(StrCodigoLocalizador: codigo, StrTipoDeBusqueda: "Contrato", tipoDeEmpresa: TipodeEmpresa, IdColonia: UidColonia, UidSucursal: UidSucursal);
            }
            else
            {
                MVSucursales.BuscarSucursalesContrato(Identificador, HA, HC, IdColonia: UidColonia, StrTipoDeBusqueda: "Contrato", tipoDeEmpresa: TipodeEmpresa, UidSucursal: UidSucursal);
            }
            if (ddlCEstatus.SelectedItem.Value.ToString() != Guid.Empty.ToString())
            {
                List<VMContrato> sucursales = new List<VMContrato>();
                if (MVContrato.ListaDeSucursalesEnContrato.Exists(cont => cont.UidEstatus == new Guid(ddlCEstatus.SelectedItem.Value.ToString())))
                {
                    sucursales = MVContrato.ListaDeSucursalesEnContrato.FindAll(cont => cont.UidEstatus == new Guid(ddlCEstatus.SelectedItem.Value.ToString()));
                    if (TipodeEmpresa == 2)
                    {
                        for (int i = 0; i < MVSucursales.ListaDeSucursalesDeContrato.Count; i++)
                        {
                            if (MVContrato.ListaDeSucursalesEnContrato.Exists(suc => suc.UidSucursalDistribuidora != MVSucursales.ListaDeSucursalesDeContrato[i].ID))
                            {
                                MVSucursales.ListaDeSucursalesDeContrato.RemoveAt(i);
                                i = i - 1;
                            }
                        }

                    }
                    if (TipodeEmpresa == 1)
                    {
                        for (int i = 0; i < MVSucursales.ListaDeSucursalesDeContrato.Count; i++)
                        {
                            if (MVContrato.ListaDeSucursalesEnContrato.Exists(suc => suc.UidSucursalSuministradora != MVSucursales.ListaDeSucursalesDeContrato[i].ID))
                            {
                                MVSucursales.ListaDeSucursalesDeContrato.RemoveAt(i);
                                i = i - 1;
                            }
                        }
                    }
                }
                else
                {
                    if (TipodeEmpresa == 2)
                    {
                        for (int i = 0; i < MVSucursales.ListaDeSucursalesDeContrato.Count; i++)
                        {
                            if (MVContrato.ListaDeSucursalesEnContrato.Exists(suc => suc.UidSucursalDistribuidora == MVSucursales.ListaDeSucursalesDeContrato[i].ID))
                            {
                                MVSucursales.ListaDeSucursalesDeContrato.RemoveAt(i);
                                i = i - 1;
                            }
                        }
                    }
                    if (TipodeEmpresa == 1)
                    {
                        for (int i = 0; i < MVSucursales.ListaDeSucursalesDeContrato.Count; i++)
                        {
                            if (MVContrato.ListaDeSucursalesEnContrato.Exists(suc => suc.UidSucursalDistribuidora == MVSucursales.ListaDeSucursalesDeContrato[i].ID))
                            {
                                MVSucursales.ListaDeSucursalesDeContrato.RemoveAt(i);
                            }
                            i = i - 1;
                        }
                    }
                }
            }


            MVSucursales.ObtenerContratosDeSucursal(TipodeEmpresa, UidSucursal, UidColonia);

            cargaGrid("Empresas");
        }

        protected void BtnLimpiarFiltrosContrato_Click(object sender, EventArgs e)
        {
            txtBIdentificador.Text = string.Empty;
            txtBHoraApertura.Text = string.Empty;
            txtBHoraDeCierre.Text = string.Empty;
            txtBCodigo.Text = string.Empty;
            ddlCEstatus.SelectedIndex = -1;

        }

        protected void panelInformacionContacto_Init(object sender, EventArgs e)
        {
            txtPITxtColonia.Text = string.Empty;
            DDLPIColonias.SelectedIndex = -1;

            //El tarifario solo se le muestra a la empresa suminsitradora para que esta pueda elegirlos.
            //A la empresa distribuidora por el momento solo se leemuestra la informacion telefonica de la empresa suministradora.
            if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString()))
            {
                panelInformacionContacto.Visible = true;
                panelInformacionTarifario.Visible = false;
                //Muestra los paneles
                PanelTarifarioSuministradora.Visible = true;
                PanelTarifarioDistribuidora.Visible = false;
                liInformacionTelefono.Attributes.Add("class", "active");
                liInformacionTarifario.Attributes.Add("class", "");
            }
            else
            {
                panelInformacionContacto.Visible = true;
                panelInformacionTarifario.Visible = false;
                //Muestra los paneles
                PanelTarifarioSuministradora.Visible = false;
                PanelTarifarioDistribuidora.Visible = true;
                liInformacionTelefono.Attributes.Add("class", "active");
                liInformacionTarifario.Attributes.Add("class", "");
            }
        }


        #region Panel de informacion de la sucursal 
        protected void DgvInformacionTarifario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = e.Row.FindControl("chkbTarifario") as CheckBox;

                e.Row.Cells[2].Text = "$" + e.Row.Cells[2].Text;

                chk.Checked = true;
                //if (!MVTarifario.ListaDeTarifariosSeleccionados.Exists(t => t.UidTarifario.ToString() == DgvInformacionTarifario.DataKeys[e.Row.RowIndex].Value.ToString()))
                //{
                //    chk.Checked = false;
                //}
                //else
                //{
                //    chk.Checked = true;
                //}
            }
        }
        protected void DGVInformacionTelefonica_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTipoDeTelefono = e.Row.FindControl("txtTipoDeTelefono") as Label;
                HyperLink numero = e.Row.FindControl("HlnkTelefono") as HyperLink;
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVInformacionTelefonica, "Select$" + e.Row.RowIndex);
                if (!string.IsNullOrEmpty(e.Row.Cells[1].Text))
                {
                    lblTipoDeTelefono.Text = DDLDTipoDETelefono.Items.FindByValue(e.Row.Cells[1].Text).Text;
                }
                numero.Text = e.Row.Cells[4].Text;
                numero.NavigateUrl = "Tel:" + e.Row.Cells[4].Text;
            }
        }
        protected void btnInformacionTelefono_Click(object sender, EventArgs e)
        {
            GuardaTarifarios();
            panelInformacionContacto.Visible = true;
            panelInformacionTarifario.Visible = false;
            liInformacionTelefono.Attributes.Add("class", "active");
            liInformacionTarifario.Attributes.Add("class", "");
        }
        protected void btnInformacionTarifario_Click(object sender, EventArgs e)
        {
            GuardaTarifarios();
            panelInformacionContacto.Visible = false;
            panelInformacionTarifario.Visible = true;

            liInformacionTelefono.Attributes.Add("class", "");
            liInformacionTarifario.Attributes.Add("class", "active");
            DGVTarifario.Visible = true;

        }
        protected void btnCerrarPanelInformacion_Click(object sender, EventArgs e)
        {
            if (MVEmpresa.ObtenerTipoDeEmpresa(Session["UidEmpresaSistema"].ToString())) // si es empresa suministradora
            {
                if (!btnEditarContrato.Visible)
                {
                    GuardaTarifarios();
                    PanelDeInformacion.Visible = false;
                }
                else
                {

                    PanelDeInformacion.Visible = false;

                }
            }
            else
            {
                PanelDeInformacion.Visible = false;
            }
        }
        protected void btnAceptarEdicionContrato_Click(object sender, EventArgs e)
        {
            if (DgvInformacionTarifario.Enabled)
            {
                //Guid UidSucursalDistribuidora = new Guid(dgvBusquedaDeEmpresa.DataKeys[int.Parse(lblIndexContrato.Text)].Value.ToString());
                //bool resultado = false;
                //foreach (GridViewRow item in DgvInformacionTarifario.Rows)
                //{
                //    string Colonia = item.Cells[3].Text;
                //    if (MVTarifario.ListaDeTarifariosSeleccionados.Exists(t => t.StrNombreColoniaZE == Colonia && t.GuidSucursalDistribuidora != UidSucursalDistribuidora))
                //    {
                //        var objeto = MVTarifario.ListaDeTarifariosSeleccionados.Find(t => t.StrNombreColoniaZE == Colonia && t.GuidSucursalDistribuidora != UidSucursalDistribuidora);
                //        resultado = true;
                //        var sucursal = MVSucursales.ListaDeSucursalesDeContrato.Find(S => S.ID == objeto.GuidSucursalDistribuidora);
                //        PanelMensajeContrato.Visible = true;
                //        lblMensajeContrato.Text = "La colonia " + Colonia + " esta asociada en el contrato de " + sucursal.IDENTIFICADOR + "¿Desea sustituirlo?";
                //        break;
                //    }
                //}
                //if (!resultado)
                //{
                GuardaTarifarios();

                if (!PanelMensajeContrato.Visible)
                {
                    DgvInformacionTarifario.DataSource = MVTarifario.ListaDeTarifarios;
                    DgvInformacionTarifario.DataBind();
                    string UidSucursal = dgvBusquedaDeEmpresa.DataKeys[int.Parse(lblIndexContrato.Text)].Value.ToString();
                    foreach (GridViewRow item in DgvInformacionTarifario.Rows)
                    {
                        CheckBox chk = item.FindControl("chkbTarifario") as CheckBox;

                        item.Cells[2].Text = "$" + item.Cells[2].Text;

                        chk.Checked = true;
                        if (MVTarifario.ListaDeTarifariosSeleccionados.Exists(t => t.GuidSucursalDistribuidora == new Guid(UidSucursal)))
                        {
                            if (!MVTarifario.ListaDeTarifariosSeleccionados.Exists(t => t.UidTarifario.ToString() == DgvInformacionTarifario.DataKeys[item.RowIndex].Value.ToString()))
                            {
                                chk.Checked = false;
                            }
                        }
                    }
                    btnAceptarEdicionContrato.Visible = false;
                    DgvInformacionTarifario.Enabled = false;
                    PanelDeInformacion.Visible = false;
                }

                //}
            }
            //btnAceptarEdicionContrato.Visible = false;
            //PanelDeInformacion.Visible = false;
        }

        #region Filtros del panel tarifario
        protected void DDLPIColonias_SelectedIndexChanged(object sender, EventArgs e)
        {
            GuardaTarifarios();
            DropDownList Seleccionador = sender as DropDownList;
            switch (Seleccionador.SelectedItem.Value)
            {
                case "Todos":
                    DgvInformacionTarifario.DataSource = MVTarifario.ListaDeTarifarios;
                    DgvInformacionTarifario.DataBind();
                    break;
                case "Seleccionado":
                    List<VMTarifario> Seleccionados = new List<VMTarifario>();


                    for (int i = 0; i < MVTarifario.ListaDeTarifarios.Count; i++)
                    {
                        if (MVTarifario.ListaDeTarifariosSeleccionados.Exists(Tarifario => Tarifario.UidTarifario == MVTarifario.ListaDeTarifarios[i].UidTarifario))
                        {
                            Seleccionados.Add(MVTarifario.ListaDeTarifarios[i]);
                        }
                    }
                    DgvInformacionTarifario.DataSource = Seleccionados;
                    DgvInformacionTarifario.DataBind();

                    break;
                case "Deseleccionados":
                    List<VMTarifario> Deseleccionados = new List<VMTarifario>();


                    for (int i = 0; i < MVTarifario.ListaDeTarifarios.Count; i++)
                    {
                        if (!MVTarifario.ListaDeTarifariosSeleccionados.Exists(Tarifario => Tarifario.UidTarifario == MVTarifario.ListaDeTarifarios[i].UidTarifario))
                        {
                            Deseleccionados.Add(MVTarifario.ListaDeTarifarios[i]);
                        }
                    }
                    DgvInformacionTarifario.DataSource = Deseleccionados;
                    DgvInformacionTarifario.DataBind();
                    break;
                default:
                    break;
            }
        }
        protected void BTNPIBuscarColonia_Click(object sender, EventArgs e)
        {
            string ColoniaABuscar = txtPITxtColonia.Text.ToUpper();
            List<VMTarifario> Cololonias = new List<VMTarifario>();
            Cololonias = MVTarifario.ListaDeTarifarios.FindAll(col => col.StrNombreColoniaZE.Contains(ColoniaABuscar));
            DgvInformacionTarifario.DataSource = Cololonias;
            DgvInformacionTarifario.DataBind();
        }
        #endregion
        protected void btnEditarContrato_Click(object sender, EventArgs e)
        {
            btnAceptarEdicionContrato.Visible = true;
            DgvInformacionTarifario.Enabled = true;
        }

        /// <summary>
        /// Descartar cambios de sustitucion de colonias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelarCambiosSustituirColonia_Click(object sender, EventArgs e)
        {
            PanelMensajeContrato.Visible = false;
        }
        protected void GuardaTarifarios()
        {
            if (DgvInformacionTarifario.Visible)
            {
                Guid UidSucursalDistribuidora = new Guid(dgvBusquedaDeEmpresa.DataKeys[int.Parse(lblIndexContrato.Text)].Value.ToString());

                if (MVContrato.ListaDeSucursalesEnContrato.Exists(c => c.UidSucursalDistribuidora == UidSucursalDistribuidora))
                {
                    int registros = 0;
                    foreach (GridViewRow item in DgvInformacionTarifario.Rows)
                    {
                        CheckBox objeto = item.FindControl("chkbTarifario") as CheckBox;
                        if (!objeto.Checked)
                        {
                            registros = registros + 1;
                        }
                    }
                    if (DgvInformacionTarifario.Rows.Count == registros)
                    {
                        PanelMensajeContrato.Visible = true;
                        lblMensajeContrato.Text = "No se puede dejar un contrato sin zonas de entrega";
                    }
                    else
                    {
                        foreach (GridViewRow item in DgvInformacionTarifario.Rows)
                        {
                            CheckBox objeto = item.FindControl("chkbTarifario") as CheckBox;
                            Guid UidRegistro = new Guid(DgvInformacionTarifario.DataKeys[item.RowIndex].Value.ToString());
                            if (objeto.Checked)
                            {
                                Guid UidContrato = new Guid();
                                if (MVContrato.ListaDeSucursalesEnContrato.Exists(c => c.UidSucursalDistribuidora == UidSucursalDistribuidora && c.UidSucursalSuministradora == new Guid(txtUidSucursal.Text)))
                                {
                                    var registro = MVContrato.ListaDeSucursalesEnContrato.Find(c => c.UidSucursalDistribuidora == UidSucursalDistribuidora && c.UidSucursalSuministradora == new Guid(txtUidSucursal.Text));
                                    UidContrato = registro.Uid;
                                }
                                MVTarifario.SeleccionarTarifario(UidRegistro, UidContrato: UidContrato, UidSucursal: UidSucursalDistribuidora);
                            }
                            else
                            {
                                MVTarifario.DeseleccionarTarifario(UidRegistro);
                            }
                        }
                    }
                }
                else
                {
                    foreach (GridViewRow item in DgvInformacionTarifario.Rows)
                    {
                        CheckBox objeto = item.FindControl("chkbTarifario") as CheckBox;
                        Guid UidRegistro = new Guid(DgvInformacionTarifario.DataKeys[item.RowIndex].Value.ToString());
                        if (objeto.Checked)
                        {
                            Guid UidContrato = new Guid();
                            if (MVContrato.ListaDeSucursalesEnContrato.Exists(c => c.UidSucursalDistribuidora == UidSucursalDistribuidora && c.UidSucursalSuministradora == new Guid(txtUidSucursal.Text)))
                            {
                                var registro = MVContrato.ListaDeSucursalesEnContrato.Find(c => c.UidSucursalDistribuidora == UidSucursalDistribuidora && c.UidSucursalSuministradora == new Guid(txtUidSucursal.Text));
                                UidContrato = registro.Uid;
                            }
                            MVTarifario.SeleccionarTarifario(UidRegistro, UidContrato: UidContrato, UidSucursal: UidSucursalDistribuidora);
                        }
                        else
                        {
                            MVTarifario.DeseleccionarTarifario(UidRegistro);
                        }
                    }
                }


            }
        }
        #endregion
        #endregion

        #region Informacion de contacto

        protected void btnServicioCliente_Click(object sender, EventArgs e)
        {
            MuestraPanel("Atencion a clientes");
        }

        protected void btnAgregarMensaje_Click(object sender, EventArgs e)
        {
            MVMensaje.AgregarMensajeALista(txtMensaje.Text);
            cargaGrid("Mensajes");
            txtMensaje.Text = string.Empty;
            txtMensaje.Focus();
        }

        protected void DgvMensajes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid UidMensaje = new Guid();
            if (e.CommandName == "Eliminar")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                UidMensaje = new Guid(DgvMensajes.DataKeys[index].Value.ToString());
                MVMensaje.QuitarMensajeDeLista(UidMensaje);
                cargaGrid("Mensajes");
            }
        }

        protected void btnInformacionZonas_Click(object sender, EventArgs e)
        {
            liInformacionTelefono.Attributes.Add("class", "");
            DGVInformacionTelefonica.Visible = false;
        }

        #endregion

        #region Panel mensaje de sistema
        protected void BtnCerrarPanelMensaje_Click(object sender, EventArgs e)
        {
            PanelMensaje.Visible = false;
        }
        #endregion

        #region Tarifario
        protected System.Data.DataTable CreaTarifario(List<VMTarifario> ListaDePrecios)
        {
            var tabla = new System.Data.DataTable();
            //Creacion de columnas
            foreach (var item in ListaDePrecios.OrderBy(x => x.StrNombreColoniaZE).ToList())
            {
                if (tabla.Columns.Count == 0)
                {
                    tabla.Columns.Add("Recolecta\\Entrega", typeof(string));
                    tabla.Columns.Add(item.StrNombreColoniaZE, typeof(string));
                }
                else
                {
                    if (!tabla.Columns.Contains(item.StrNombreColoniaZE))
                    {
                        tabla.Columns.Add(item.StrNombreColoniaZE, typeof(string));
                    }
                }
            }
            //Creacion del contenido
            var registros = new List<VMTarifario>();
            for (int i = 0; i < MVDireccion.ListaColoniasSeleccionadasRecolecta.Count; i++)
            {
                MVDireccion.ListaColoniasSeleccionadasRecolecta.OrderBy(x => x.NOMBRECOLONIA).ToList();
                if (!registros.Exists(x => x.UidRelacionZR != MVDireccion.ListaColoniasSeleccionadasRecolecta[i].UidRegistro))
                {
                    registros = ListaDePrecios.FindAll(T => T.UidRelacionZR == MVDireccion.ListaColoniasSeleccionadasRecolecta[i].UidRegistro);
                    var Row = tabla.NewRow();
                    tabla.Rows.Add(Row);
                    registros.OrderBy(x => x.StrNombreColoniaZE).ToList();

                    for (int p = 0; p < registros.Count; p++)
                    {
                        var campo = "";
                        if ((p + 1) < tabla.Columns.Count)
                        {
                            campo = tabla.Columns[p + 1].ColumnName;
                        }
                        var row = tabla.Rows.Count - 1;

                        if (p == 0)
                        {
                            tabla.Rows[row][p] = registros[p].StrNombreColoniaZR + "," + registros[p].UidRelacionZR;
                            VMTarifario registro = registros.Find(x => x.StrNombreColoniaZE == campo);
                            if (registro != null)
                            {
                                tabla.Rows[row][p + 1] = registro.DPrecio.ToString() + "," + registro.UidTarifario + "," + registro.StrNombreColoniaZR;
                            }
                            else
                            {
                                tabla.Columns.RemoveAt(p);
                            }
                        }
                        else if ((p + 1) <= registros.Count)
                        {
                            VMTarifario registro = registros.Find(x => x.StrNombreColoniaZE == campo);
                            if (registro != null)
                            {
                                tabla.Rows[row][p + 1] = registro.DPrecio.ToString() + "," + registro.UidTarifario + "," + registro.StrNombreColoniaZR;
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(campo))
                                {
                                    tabla.Columns.RemoveAt(p);
                                }
                            }
                        }
                    }
                    registros.Clear();
                }
            }
            return tabla;
        }

        protected void CrearGridViewTarifario(List<VMTarifario> Precios)
        {


            for (int i = 0; DGVTarifario.Columns.Count > i;)
            {
                DGVTarifario.Columns.RemoveAt(i);
            }
            System.Data.DataTable t = CreaTarifario(Precios);
            int r = 1;

            DGVTarifario.DataSource = t;
            DGVTarifario.DataBind();
            foreach (TableRow item in DGVTarifario.Rows)
            {
                int j = 0;
                double precio;
                Guid newGuid;
                foreach (TableCell celda in item.Cells)
                {

                    TemplateField campo = new TemplateField();
                    string ColumnName = celda.Text.ToString();


                    TableCell tc = new TableCell();

                    campo.HeaderStyle.HorizontalAlign = HorizontalAlign.Justify;
                    campo.HeaderText = ColumnName;


                    if (!DGVTarifario.Columns.Equals(ColumnName))
                    {
                        DGVTarifario.Columns.Add(campo);
                    }

                    if (j != 0)
                    {
                        string[] ColumnValues = celda.Text.Split(new char[] { ',' });
                        string ColumnValue = string.Empty;
                        string ColumnaName = string.Empty;
                        string FilaZonaRecolecta = string.Empty;
                        foreach (string columnas in ColumnValues)
                        {
                            if (Guid.TryParse(columnas, out newGuid))
                            {
                                ColumnValue = columnas;
                            }
                            else if (double.TryParse(columnas, out precio))
                            {
                                ColumnaName = columnas;
                            }
                            else
                            {
                                FilaZonaRecolecta = columnas;
                            }
                        }

                        Label lblUid = new Label();
                        lblUid.Text = ColumnValue;
                        lblUid.ID = "UidRegistro" + (r + j).ToString();
                        lblUid.Visible = false;
                        celda.Controls.Add(lblUid);

                        string Id = "txt," + r + "," + j;
                        TextBox txtBox = new TextBox();
                        txtBox.ID = Id;
                        txtBox.Text = ColumnaName;
                        txtBox.TextChanged += new EventHandler(CambiarTexto);
                        txtBox.AutoPostBack = true;
                        txtBox.Width = 100;
                        txtBox.CssClass = "form-control text-center";
                        celda.Controls.Add(txtBox);

                        CompareValidator cv = new CompareValidator(); // Create validator and configure
                        cv.SetFocusOnError = true;
                        cv.Operator = ValidationCompareOperator.GreaterThan;
                        cv.ValueToCompare = "-1";
                        cv.Type = ValidationDataType.Double;
                        cv.Display = ValidatorDisplay.Dynamic;
                        cv.ErrorMessage = "<br/>Campo no valido";
                        cv.ForeColor = Color.Red;
                        cv.ControlToValidate = txtBox.ID;
                        celda.Controls.Add(cv);
                        //Create and add AsyncPostBackTrigger
                        AsyncPostBackTrigger APBT_trig = new AsyncPostBackTrigger();
                        APBT_trig.EventName = "TextChanged";
                        APBT_trig.ControlID = txtBox.UniqueID;
                        UPSucursales.Triggers.Add(APBT_trig);

                        j++;
                    }
                    else
                    {
                        string[] ColumnValues = celda.Text.Split(new char[] { ',' });
                        string ColumnValue = string.Empty;
                        foreach (string columnas in ColumnValues)
                        {
                            if (Guid.TryParse(columnas, out newGuid))
                            {
                                ColumnValue = columnas;
                            }
                            else
                            {
                                ColumnName = columnas;
                            }
                        }


                        Label etiqueta = new Label();
                        etiqueta.ID = "LblUidZonaRecolecta";
                        etiqueta.Text = ColumnValue;
                        etiqueta.Visible = false;
                        celda.Controls.Add(etiqueta);

                        Label Nombre = new Label();
                        Nombre.Text = ColumnName;

                        Nombre.Visible = true;
                        celda.Controls.Add(Nombre);


                        j++;
                    }
                }
                r++;
            }
        }

        protected void CambiarTexto(object Sender, EventArgs e)
        {
            TextBox txt = Sender as TextBox;
            var t = txt.Text;
            var y = txt.ID;
            string[] valores = y.Split(new char[] { ',' });
            if (lblCelda.Text != valores[2])
            {
                lblFila.Text = valores[1];
                lblCelda.Text = valores[2];
                lblPrecio.Text = t;
            }
        }
        protected void GuardaTarifario()
        {
            string UidZonaRecolecta = "";
            string precio = "";
            string ZonaEntrega = "";
            // verificar que la lista no se cree de nuevo al momento del postback porque le cambia los id a la lista y no deja actualizar
            foreach (GridViewRow item in DGVTarifario.Rows)
            {
                int number = 0;
                foreach (TableCell celda in item.Cells)
                {
                    if (number == 0)
                    {
                        var UidZR = celda.FindControl("LblUidZonaRecolecta") as Label;
                        if (UidZR != null)
                        {
                            UidZonaRecolecta = UidZR.Text;
                        }

                    }
                    else
                    {
                        var txtPrecio = celda.Controls[1] as TextBox;
                        var lblUidRegistro = celda.Controls[0] as Label;
                        ZonaEntrega = lblUidRegistro.Text;
                        precio = txtPrecio.Text;
                    }
                    if (!string.IsNullOrEmpty(UidZonaRecolecta) && !string.IsNullOrEmpty(precio) && !string.IsNullOrEmpty(ZonaEntrega))
                    {
                        MVTarifario.ActualizaLista(UidZonaRecolecta, precio, ZonaEntrega);
                    }
                    number++;
                }
            }
        }

        protected void BtnCopiarTarifarioArriba_Click(object sender, EventArgs e)
        {
            if (lblFila.Text == (1).ToString())
            {
                PanelMensaje.Visible = true;
                LblMensaje.Text = "No se puede copiar la primera fila hacia arriba";
            }
            else
            {
                var fila = int.Parse(lblFila.Text);
                for (int i = 1; i < DGVTarifario.Rows[fila].Cells.Count; i++)
                {
                    TextBox TxtCopiado = DGVTarifario.Rows[fila].Cells[i].Controls[1] as TextBox;
                    TextBox txtAsignarValor = DGVTarifario.Rows[fila + 1].Cells[i].Controls[1] as TextBox;
                }
            }
        }

        protected void BtnCopiarTarifarioAbajo_Click(object sender, EventArgs e)
        {
            if (lblFila.Text == DGVTarifario.Rows.Count.ToString())
            {
                PanelMensaje.Visible = true;
                LblMensaje.Text = "No se puede copiar la ultima fila hacia abajo";
            }
            else
            {
                var fila = int.Parse(lblFila.Text);
                for (int i = 1; i < DGVTarifario.Rows[fila].Cells.Count; i++)
                {
                    TextBox TxtCopiado = DGVTarifario.Rows[fila].Cells[i].Controls[1] as TextBox;
                    TextBox txtAsignarValor = DGVTarifario.Rows[fila - 1].Cells[i].Controls[1] as TextBox;
                }
            }
        }

        protected void BtnCopiarDerecha_Click(object sender, EventArgs e)
        {
            var fila = int.Parse(lblFila.Text);
            var celda = int.Parse(lblCelda.Text);
            if (lblCelda.Text == (DGVTarifario.Columns.Count).ToString())
            {
                PanelMensaje.Visible = true;
                LblMensaje.Text = "No se puede copiar la primera columna hacia la derecha";
            }
            else
            {
                for (int i = 0; i < DGVTarifario.Rows.Count; i++)
                {
                    TextBox TxtCopiado = DGVTarifario.Rows[i].Cells[celda].Controls[1] as TextBox;
                    TextBox txtAsignarValor = DGVTarifario.Rows[i].Cells[celda + 1].Controls[1] as TextBox;
                    txtAsignarValor.Text = TxtCopiado.Text;
                }
            }
        }

        protected void BtnCopiarIzquierda_Click(object sender, EventArgs e)
        {
            var fila = int.Parse(lblFila.Text);
            var celda = int.Parse(lblCelda.Text);
            if (lblCelda.Text == (1).ToString())
            {
                PanelMensaje.Visible = true;
                LblMensaje.Text = "No se puede copiar la primera columna hacia la izquierda";
            }
            else
            {
                for (int i = 0; i < DGVTarifario.Rows.Count; i++)
                {
                    TextBox TxtCopiado = DGVTarifario.Rows[i].Cells[celda].Controls[1] as TextBox;
                    TextBox txtAsignarValor = DGVTarifario.Rows[i].Cells[celda - 1].Controls[1] as TextBox;
                    txtAsignarValor.Text = TxtCopiado.Text;
                }
            }
        }

        protected void BtnCopiarTodaLaTabla_Click(object sender, EventArgs e)
        {
            var precio = lblPrecio.Text;
            for (int i = 0; i < DGVTarifario.Rows.Count; i++)
            {
                for (int j = 1; j < DGVTarifario.Rows[i].Cells.Count; j++)
                {
                    TextBox TxtCopiado = DGVTarifario.Rows[i].Cells[j].Controls[1] as TextBox;
                    TxtCopiado.Text = precio;
                }
            }
        }



        protected void BtnTarifario_Click(object sender, EventArgs e)
        {

            MuestraPanel("Zona de servicio");
            if (DLCategoria.Items.Count != 0)
            {
                ObtenerCategoriasSeleccionadas();
            }
            //Obtiene las subcategorias seleccionadas
            if (dlSubcategoria.Items.Count != 0)
            {
                ObtenerSubcategoriasSeleccionadas();
            }
            liDatosZonaDeEntrega.Attributes.Add("class", "");
            liDatosTarifario.Attributes.Add("class", "active");
            liZonaDeRecolecta.Attributes.Add("class", "");
            PanelZonasServicio.Visible = false;
            PanelZonaDeRecolecta.Visible = false;
            PanelTarifario.Visible = true;
            ColoniasSeleccionadas();
            for (int i = 0; i < MVDireccion.ListaColoniasSeleccionadasRecolecta.Count; i++)
            {
                for (int j = 0; j < MVDireccion.ListaColoniasSeleccionadasEntrega.Count; j++)
                {
                    MVTarifario.AgregaALista(MVDireccion.ListaColoniasSeleccionadasEntrega[j].UidRegistro, UidZonaRecoleccion: MVDireccion.ListaColoniasSeleccionadasRecolecta[i].UidRegistro, NombreZE: MVDireccion.ListaColoniasSeleccionadasEntrega[j].NOMBRECOLONIA, NombreZR: MVDireccion.ListaColoniasSeleccionadasRecolecta[i].NOMBRECOLONIA);
                }
            }

            for (int i = 0; i < MVTarifario.ListaDeTarifarios.Count; i++)
            {
                if (!MVDireccion.ListaColoniasSeleccionadasEntrega.Exists(o => o.UidRegistro == MVTarifario.ListaDeTarifarios[i].UidRelacionZE))
                {
                    var obj = MVTarifario.ListaDeTarifarios[i];
                    MVTarifario.ListaDeTarifarios.Remove(obj);
                }
            }
            CrearGridViewTarifario(MVTarifario.ListaDeTarifarios);
        }
        #endregion
    }
}