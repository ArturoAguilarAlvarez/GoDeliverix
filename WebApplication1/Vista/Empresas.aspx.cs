
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using VistaDelModelo;
using System.Drawing.Imaging;

namespace WebApplication1.Vista
{
    public partial class EmpSuministradora : System.Web.UI.Page
    {
        #region Propiedades
        VMEmpresas MVEmpresas;
        VMDireccion MVDireccion;
        VMImagen MVImagen;
        VMComision MVComision;
        VMTelefono MVTelefono;
        VMCorreoElectronico MVCorreoElectronico;
        ImagenHelper oImagenHelper = new ImagenHelper();
        string AccionesDeLaPagina = "";

        #endregion
        public void Page_Load(object sender, EventArgs e)
        {
            FUImagen.Attributes["onchange"] = "UploadFile(this)";
            //Inicio de listas
            if (!IsPostBack)
            {
                Session.Remove("Accion");
                //Instancias de la vista del modelo
                MVTelefono = new VMTelefono();
                MVImagen = new VMImagen();
                MVDireccion = new VMDireccion();
                MVEmpresas = new VMEmpresas();
                MVCorreoElectronico = new VMCorreoElectronico();
                MVComision = new VMComision();
                AccionesDeLaPagina = string.Empty;
                Session["MVEmpresas"] = MVEmpresas;
                Session["MVComision"] = MVComision;
                Session["MVDireccion"] = MVDireccion;
                Session["MVTelefono"] = MVTelefono;
                Session["MVImagen"] = MVImagen;
                Session["MVCorreoElectronico"] = MVCorreoElectronico;
                #region Panel derecho
                MVEmpresas.TipoDeEmpresa();
                MVEmpresas.Estatus();
                MVTelefono.TipoDeTelefonos();
                #region Paneles
                //Botones
                btnEditar.Enabled = false;
                //Visibilidad de paneles
                pnlDatosGenerales.Visible = true;
                pnlDireccion.Visible = false;
                pnlContacto.Visible = false;
                PnlComisiones.Visible = false;
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                btnGuardarTelefono.Visible = false;
                btnCancelarTelefono.Visible = false;
                btnEditarTelefono.Enabled = false;
                btnEdiarDireccion.Enabled = false;
                //Agregacion de clase 'active' al boton general
                liDatosGenerales.Attributes.Add("class", "active");
                liDatosDireccion.Attributes.Add("class", " ");
                liDatosContacto.Attributes.Add("class", "");
                //Placeholders
                txtDManzana.Attributes.Add("placeholder", "Manzana");
                txtCalle1.Attributes.Add("plcaeholder", "Calle");
                txtCalle2.Attributes.Add("plcaeholder", "Calle");
                txtDRfc.Attributes.Add("placeholder", "RFC");
                txtDRazonSocial.Attributes.Add("placeholder", "Razon social");
                txtDReferencia.Attributes.Add("placeholder", "Referencia");
                txtDNombreComercial.Attributes.Add("placeholder", "Nombre Comercial");
                txtDCorreoElectronico.Attributes.Add("placeholder", "Correo electronico");
                txtDTelefono.Attributes.Add("palceholder", "Telefono");
                //Desabilita los textbox al cargar la pagina
                TextboxActivados();
                //GridView telefonos
                DGVTELEFONOS.DataSource = null;
                DGVTELEFONOS.DataBind();
                //GridView direcciones
                GVDireccion.Enabled = false;
                #endregion
                #region DropdownList


                //Obtiene datos de estatus
                DDLDEstatus.DataSource = MVEmpresas.ESTATUS;
                DDLDEstatus.DataTextField = "NOMBRE";
                DDLDEstatus.DataValueField = "ID";
                DDLDEstatus.DataBind();
                //Obtiene datos de los municipios
                DDLDTipoDeEmpresa.DataSource = MVEmpresas.ObtenerTipoDeEmpresasGestion();
                DDLDTipoDeEmpresa.DataTextField = "NOMBRE";
                DDLDTipoDeEmpresa.DataValueField = "IdTipoDeEmpresa";
                DDLDTipoDeEmpresa.DataBind();

                DDLDTipoDETelefono.DataSource = MVTelefono.ListaDeTipoDeTelefono;
                DDLDTipoDETelefono.DataValueField = "UidTipo";
                DDLDTipoDETelefono.DataTextField = "StrNombreTipoDeTelefono";
                DDLDTipoDETelefono.DataBind();



                #endregion
                #region Limites
                txtDRazonSocial.MaxLength = 100;
                txtDNombreComercial.MaxLength = 100;
                txtDRfc.MaxLength = 13;
                txtCalle1.MaxLength = 100;
                txtCalle2.MaxLength = 100;
                txtDManzana.MaxLength = 4;
                txtDLote.MaxLength = 8;
                txtDReferencia.MaxLength = 500;
                #endregion
                #region Imagen
                //Carga imagen por default
                ImageEmpresa.ImageUrl = "Img/Default.jpg";
                ImageEmpresa.DataBind();
                #endregion
                //Panel de mensaje
                PanelMensaje.Visible = false;
                #endregion
                #region Panel izquierdo
                #region Filtros
                //Panel de filtros          
                PnlFiltros.Visible = false;
                lblVisibilidadfiltros.Text = " Mostrar";
                //Placeholders del panel de filtros
                txtFNombreComercial.Attributes.Add("placeholder", "Nombre Comercial");
                txtFRazonSocial.Attributes.Add("placeholder", "Razon social");
                txtFRfc.Attributes.Add("placeholder", "RFC");
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

                //Obtiene estatus
                DDLFEstatus.DataSource = MVEmpresas.ESTATUS;
                DDLFEstatus.DataTextField = "NOMBRE";
                DDLFEstatus.DataValueField = "ID";
                DDLFEstatus.DataBind();

                //obtiene tipo de empresa
                DDLFTipoDeEmpresa.DataTextField = "NOMBRE";
                DDLFTipoDeEmpresa.DataValueField = "ID";
                DDLFTipoDeEmpresa.DataSource = MVEmpresas.TEMPRESA;
                DDLFTipoDeEmpresa.DataBind();

                //Obtiene el pais
                DDLDPais.DataSource = MVDireccion.Paises();
                DDLDPais.DataTextField = "Nombre";
                DDLDPais.DataValueField = "UidPais";
                DDLDPais.DataBind();

                //Alimenta dropdownlist del pais en busqueda avanzada.
                DDLDBAPAIS.DataSource = MVDireccion.Paises();
                DDLDBAPAIS.DataTextField = "Nombre";
                DDLDBAPAIS.DataValueField = "UidPais";
                DDLDBAPAIS.DataBind();

                #endregion
                #region GridView empresa simple
                MVEmpresas.BuscarEmpresas();
                CargaGrid("Normal");
                #endregion
                #endregion

                #region Panel de direccion
                PanelDatosDireccion.Visible = false;
                GVDireccion.DataSource = null;
                GVDireccion.DataBind();
                txtIdentificadorDeDireccion.Attributes.Add("placeholder", "Identificador");
                txtIdentificadorDeDireccion.Text = "Predeterminado";

                #region DropdownList
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

                //obtiene tipo de empresa

                DDLBATipo.DataTextField = "NOMBRE";
                DDLBATipo.DataValueField = "ID";
                DDLBATipo.DataSource = MVEmpresas.TEMPRESA;
                DDLBATipo.DataBind();

                //Obtiene estatus
                DDLBAEstatus.DataSource = MVEmpresas.ESTATUS;
                DDLBAEstatus.DataTextField = "NOMBRE";
                DDLBAEstatus.DataValueField = "ID";
                DDLBAEstatus.DataBind();

                MuestraEstados("00000000-0000-0000-0000-000000000000", "Filtro");
                MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Filtro");
                MuestraCiudad("00000000-0000-0000-0000-000000000000", "Filtro");
                MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");
                #endregion

                #region Placeholders
                txtBARazonSocial.Attributes.Add("placeholder", "Razon Social");
                txtBANombreComercial.Attributes.Add("placeholder", "Nombre Comercial");
                txtBARF.Attributes.Add("placeholder", "RFC");
                txtBACorreoElectronico.Attributes.Add("placeholder", "Correo electronico");
                txtBACalle.Attributes.Add("placeholder", "Calle");
                txtBACOdigoPostal.Attributes.Add("placeholder", "Codigo Postal");

                #endregion

                #region Gridview Busqueda ampliada

                #endregion

                #endregion
            }
            else
            {
                MVEmpresas = (VMEmpresas)Session["MVEmpresas"];
                MVComision = (VMComision)Session["MVComision"];
                MVDireccion = (VMDireccion)Session["MVDireccion"];
                MVTelefono = (VMTelefono)Session["MVTelefono"];
                MVImagen = (VMImagen)Session["MVImagen"];
                MVCorreoElectronico = (VMCorreoElectronico)Session["MVCorreoElectronico"];
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
            txtFNombreComercial.Text = string.Empty;
            txtFRazonSocial.Text = string.Empty;
            txtFRfc.Text = string.Empty;
            DDLFEstatus.SelectedIndex = 0;
            DDLFTipoDeEmpresa.SelectedIndex = 0;
        }

        #endregion
        #region GridView

        #region Busqueda Normal
        protected void GVWEmpresaNormal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                GridViewRow PagerRow = DGVEMPRESAS.TopPagerRow;

#pragma warning disable IDE0019 // Usar coincidencia de patrones
                Label InformacionDeLaCantidadDeRegistrosMostrados = PagerRow.Cells[0].FindControl("lblTotalDeRegistros") as Label;
#pragma warning restore IDE0019 // Usar coincidencia de patrones
                ImageButton DobleDerecho = PagerRow.Cells[0].FindControl("btnUltimo") as ImageButton;
                ImageButton DobleIzquierdo = PagerRow.Cells[0].FindControl("btnPrimero") as ImageButton;
                ImageButton Izquierda = PagerRow.Cells[0].FindControl("btnAnterior") as ImageButton;
                ImageButton Derecha = PagerRow.Cells[0].FindControl("btnSiguiente") as ImageButton;
                DropDownList PaginasBusquedaNormal = PagerRow.Cells[0].FindControl("DDLDNUMERODEPAGINAS") as DropDownList;

                int PaginaActual = DGVEMPRESAS.PageIndex + 1;
                int Total = DGVEMPRESAS.PageCount;

                if (InformacionDeLaCantidadDeRegistrosMostrados != null)
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
                    else if (DGVEMPRESAS.PageSize >= MVEmpresas.LISTADEEMPRESAS.Count)
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
                    //Propiedades del GridView
                    int Paginas = (DGVEMPRESAS.PageIndex + 1);
                    int Registros = MVEmpresas.LISTADEEMPRESAS.Count;
                    int FilasPorPagina = DGVEMPRESAS.PageSize;
                    int Filas = DGVEMPRESAS.Rows.Count;

                    //Obtener la cantidad de registros dentro de la lista
                    string CantidadDeRegistros = MVEmpresas.LISTADEEMPRESAS.Count.ToString();

                    //Operaciones matematicas
                    int RegistroFinal = Paginas * FilasPorPagina;
                    int RegistroInicial = ((Paginas * FilasPorPagina) - FilasPorPagina) + 1;

                    //Se asigna el texto al control de label con los resultados optenidos de las operaciones.
                    string TextoAMostrar = string.Empty;
                    if ((RegistroInicial + FilasPorPagina) > Registros)
                    {
                        RegistroFinal = Convert.ToInt32(CantidadDeRegistros);
                        TextoAMostrar = "" + RegistroInicial + " - " + RegistroFinal + " de " + CantidadDeRegistros + "";
                    }
                    else
                    {
                        TextoAMostrar = "" + RegistroInicial + " - " + RegistroFinal + " de " + CantidadDeRegistros + "";
                    }
                    //Asigna el texto del control Label de la variable llamada TextoAMostrar
                    InformacionDeLaCantidadDeRegistrosMostrados.Text = TextoAMostrar;
                }
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVEMPRESAS, "Select$" + e.Row.RowIndex);


                var icono = e.Row.FindControl("lblTipo") as Label;
                if (e.Row.Cells[6].Text == "1")
                {
                    icono.CssClass = "glyphicon glyphicon-cutlery";
                    icono.ToolTip = "Suministradora";

                }
                if (e.Row.Cells[6].Text == "2")
                {
                    icono.CssClass = "glyphicon glyphicon-send";
                    icono.ToolTip = "Distribuidora";
                }

                var ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                if (e.Row.Cells[4].Text == "1")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                    ESTATUS.ToolTip = "ACTIVO";
                }
                if (e.Row.Cells[4].Text == "2")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                    ESTATUS.ToolTip = "INACTIVO";
                }
            }

        }
        protected void GridViewBusquedaNormal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGVEMPRESAS.PageIndex = e.NewPageIndex;
            CargaGrid("Normal");
        }
        protected void GVWEmpresaBusquedaNormal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Paneles"] != null)
            {
                Session.Remove("Paneles");
            }

            AccionesDeLaPagina = string.Empty;
            Session.Remove("Accion");
            TextboxActivados();

            string valor = DGVEMPRESAS.SelectedDataKey.Value.ToString();

            btnEditar.CssClass = "btn btn-sm btn-default";
            btnEditar.Enabled = true;

            PanelBusquedaAmpliada.Visible = false;
            PanelDerecho.Visible = true;
            PanelIzquierdo.Visible = true;


            MuestraEmpresaEnGestion(valor);
        }
        protected void DGVEMPRESASNORMAL_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            Session["sortExpression"] = sortExpression;

            if (Session["SortDirection"] != null && Session["SortDirection"].ToString() == SortDirection.Descending.ToString())
            {
                Session["SortDirection"] = SortDirection.Ascending;
                Sort(sortExpression, "ASC", "Normal");
            }
            else
            {
                Session["SortDirection"] = SortDirection.Descending;
                Sort(sortExpression, "DESC", "Normal");
            }
        }
        protected void PaginaSeleccionadaBusquedaNormal(object sender, EventArgs e)
        {
            int valor = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            DGVEMPRESAS.PageIndex = valor - 1;
            CargaGrid("Normal");
        }
        protected void GVWEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Paneles"] != null)
            {
                Session.Remove("Paneles");
            }
            AccionesDeLaPagina = string.Empty;
            Session.Remove("Accion");
            TextboxActivados();

            string valor = DGVEMPRESAS.SelectedDataKey.Value.ToString();

            btnEditar.CssClass = "btn btn-sm btn-default";
            btnEditar.Enabled = true;

            MuestraEmpresaEnGestion(valor);
        }
        #endregion

        #region Busqueda Ampliada
        protected void GVWEmpresaAmpliada_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label NumeroDePagina = new Label();
                Label TotalDePaginas = new Label();


                GridViewRow PagerRow = DGVBUSQUEDAAMPLIADA.TopPagerRow;

                Label InformacionDeLaCantidadDeRegistrosMostrados = PagerRow.Cells[0].FindControl("lblTotalDeRegistros") as Label;
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
                    else if (DGVBUSQUEDAAMPLIADA.PageSize >= MVEmpresas.LISTADEEMPRESAS.Count)
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

                    //Propiedades del GridView
                    int Paginas = (DGVBUSQUEDAAMPLIADA.PageIndex + 1);
                    int Registros = MVEmpresas.LISTADEEMPRESAS.Count;
                    int FilasPorPagina = DGVBUSQUEDAAMPLIADA.PageSize;
                    int Filas = DGVBUSQUEDAAMPLIADA.Rows.Count;

                    //Obtener la cantidad de registros dentro de la lista
                    string CantidadDeRegistros = MVEmpresas.LISTADEEMPRESAS.Count.ToString();

                    //Operaciones matematicas
                    int RegistroFinal = Paginas * FilasPorPagina;
                    int RegistroInicial = ((Paginas * FilasPorPagina) - FilasPorPagina) + 1;

                    //Se asigna el texto al control de label con los resultados optenidos de las operaciones.
                    string TextoAMostrar = string.Empty;
                    if ((RegistroInicial + FilasPorPagina) > Registros)
                    {
                        RegistroFinal = Convert.ToInt32(CantidadDeRegistros);
                        TextoAMostrar = "" + RegistroInicial + " - " + RegistroFinal + " de " + CantidadDeRegistros + "";
                    }
                    else
                    {
                        TextoAMostrar = "" + RegistroInicial + " - " + RegistroFinal + " de " + CantidadDeRegistros + "";
                    }
                    //Asigna el texto del control Label de la variable llamada TextoAMostrar
                    InformacionDeLaCantidadDeRegistrosMostrados.Text = TextoAMostrar;


                    NumeroDePagina.Text = PaginaActual.ToString();
                    TotalDePaginas.Text = Total.ToString();
                }

                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVBUSQUEDAAMPLIADA, "Select$" + e.Row.RowIndex);
                var icono = e.Row.FindControl("lblTipo") as Label;
                if (e.Row.Cells[7].Text == "Suministradora")
                {
                    icono.CssClass = "glyphicon glyphicon-cutlery";
                    icono.ToolTip = "Suministradora";

                }
                if (e.Row.Cells[7].Text == "Distribuidora")
                {
                    icono.CssClass = "glyphicon glyphicon-send";
                    icono.ToolTip = "Distribuidora";
                }

                Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                if (e.Row.Cells[5].Text == "ACTIVO")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                    ESTATUS.ToolTip = "ACTIVO";
                }
                if (e.Row.Cells[5].Text == "INACTIVO")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                    ESTATUS.ToolTip = "INACTIVO";
                }


            }

        }
        protected void GridViewBusquedaAmpliada_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGVBUSQUEDAAMPLIADA.PageIndex = e.NewPageIndex;
            CargaGrid("Ampliada");
        }
        protected void GVBusquedaAvanzadaEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Paneles"] != null)
            {
                Session.Remove("Paneles");
            }
            AccionesDeLaPagina = string.Empty;
            TextboxActivados();
            Session.Remove("Accion");

            string valor = DGVBUSQUEDAAMPLIADA.SelectedDataKey.Value.ToString();

            btnEditar.CssClass = "btn btn-sm btn-default";
            btnEditar.Enabled = true;


            PanelBusquedaAmpliada.Visible = false;
            PanelDerecho.Visible = true;
            PanelIzquierdo.Visible = true;

            MuestraEmpresaEnGestion(valor);
        }
        protected void TamanioGrid(object sender, EventArgs e)
        {
            int cantidad = int.Parse(((DropDownList)sender).SelectedValue);
            DGVBUSQUEDAAMPLIADA.PageSize = cantidad;
            DGVBUSQUEDAAMPLIADA.DataSource = MVEmpresas.LISTADEEMPRESAS;
            DGVBUSQUEDAAMPLIADA.DataBind();
        }
        protected void PaginaSeleccionadaBusquedaAmpliada(object sender, EventArgs e)
        {
            int valor = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            DGVBUSQUEDAAMPLIADA.PageIndex = valor - 1;
            CargaGrid("Ampliada");
        }
        protected void GridViewBusquedaAmplicada_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            Session["sortExpression"] = sortExpression;

            if (Session["SortDirection"] != null && Session["SortDirection"].ToString() == SortDirection.Descending.ToString())
            {
                Session["SortDirection"] = SortDirection.Ascending;
                Sort(sortExpression, "ASC", "Ampliada");
            }
            else
            {
                Session["SortDirection"] = SortDirection.Descending;
                Sort(sortExpression, "DESC", "Ampliada");
            }
        }
        #endregion

        protected void GridViewPreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            GridViewRow pagerRow = (GridViewRow)gv.TopPagerRow;

            if (pagerRow != null && pagerRow.Visible == false)
                pagerRow.Visible = true;
        }

        private void Sort(string sortExpression, string Valor, string GridView)
        {

            switch (GridView)
            {
                case "Normal":
                    DGVEMPRESAS.DataSource = MVEmpresas.Sort(sortExpression, Valor); ;
                    DGVEMPRESAS.DataBind();
                    break;
                case "Ampliada":
                    DGVBUSQUEDAAMPLIADA.DataSource = MVEmpresas.Sort(sortExpression, Valor); ;
                    DGVBUSQUEDAAMPLIADA.DataBind();
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// Metodo que controla que GridView al que se le hace binding
        /// </summary>
        /// <param name="Grid">Nombre del GridView a cargar</param>
        protected void CargaGrid(string Grid)
        {
            switch (Grid)
            {
                case "Normal":

                    DGVEMPRESAS.DataSource = MVEmpresas.LISTADEEMPRESAS;
                    DGVEMPRESAS.DataBind();
                    break;
                case "Ampliada":

                    DGVBUSQUEDAAMPLIADA.DataSource = MVEmpresas.LISTADEEMPRESAS;
                    DGVBUSQUEDAAMPLIADA.DataBind();
                    break;
                case "Direccion":

                    GVDireccion.DataSource = MVDireccion.ListaDIRECCIONES;
                    GVDireccion.DataBind();
                    break;
                case "Telefono":
                    DGVTELEFONOS.DataSource = MVTelefono.ListaDeTelefonos;
                    DGVTELEFONOS.DataBind();
                    break;
                default:
                    break;
            }
        }
        protected void MuestraEmpresaEnGestion(string valor)
        {
            MVEmpresas.ObtenerNombreComercial("", IdEmpresa: valor);
            PanelMensaje.Visible = false;
            Label lblNombreEmpresaSeleccionada = Master.FindControl("lblNombreDeEmpresa") as Label;
            TextBox txtUidEmpresaSeleccionadaSistema = Master.FindControl("txtUidEmpresaSistema") as TextBox;
            //Obtiene los botones de los modulos para que cuando se seleccione una empresa estos cambien dinamicamente acuerdo a la empresa seleccionada.
            var btnRepartidores = (Master.FindControl("btnRepartidores") as LinkButton);
            var btnVehiculos = (Master.FindControl("btnVehiculos") as LinkButton);
            var btnModuloProductos = (Master.FindControl("btnModuloProductos") as LinkButton);
            var btnModuloMenu = (Master.FindControl("btnModuloMenu") as LinkButton);

            //Obtiene el usuario que esta dentro del sistema
            string UidUsuarioEnSistema = (Master.FindControl("txtUidUsuarioSistema") as TextBox).Text;
            string UidEmpresa = (Master.FindControl("txtUidEmpresaSistema") as TextBox).Text;
            panelComisionGoDeliverix.Visible = false;
            //Cambia el menu de navegacion del sistema
            if (MVEmpresas.ObtenerTipoDeEmpresa(valor))
            {
                //Modulos distribuidora
                btnRepartidores.Visible = false;
                btnVehiculos.Visible = false;
                //Modulo suministradora
                btnModuloProductos.Visible = true;
                btnModuloMenu.Visible = true;
                panelComisionGoDeliverix.Visible = true;
                chkbxComisionTarjeta.Enabled = true;
            }
            else if (!MVEmpresas.ObtenerTipoDeEmpresa(valor))
            {
                //Modulo distribuidora
                btnRepartidores.Visible = true;
                btnVehiculos.Visible = true;
                //Modulo suministradora
                btnModuloProductos.Visible = false;
                btnModuloMenu.Visible = false;
                chkbxComisionTarjeta.Enabled = false;
            }

            Session["UidEmpresaSistema"] = MVEmpresas.UIDEMPRESA;
            lblNombreEmpresaSeleccionada.Text = MVEmpresas.NOMBRECOMERCIAL;
            txtUidEmpresaSeleccionadaSistema.Text = MVEmpresas.UIDEMPRESA.ToString();

            PanelCargando.Visible = true;
            //Obtiene lo datos de la empresa
            MVEmpresas.BuscarEmpresas(UidEmpresa: new Guid(valor));
            //Obtiene las direcciones asociadas a la empresa
            MVDireccion.ObtenerDireccionesEmpresa(valor);
            //Obtiene los tenefonos asociados en la empresa
            MVTelefono.ObtenerTelefonoEmpresa(valor, "gestion");
            //Obtiene la imagen de la empresa
            MVImagen.ObtenerImagenPerfilDeEmpresa(valor);
            //Obtiene el correo electronico
            MVCorreoElectronico.BuscarCorreos(UidPropietario: new Guid(valor), strParametroDebusqueda: "Empresa");

            //Datos del correo electronico
            txtDCorreoElectronico.Text = MVCorreoElectronico.CORREO;
            txtUidCorreoElectronico.Text = MVCorreoElectronico.ID.ToString();

            lblUidEmpresa.Text = MVEmpresas.UIDEMPRESA.ToString();
            txtDRazonSocial.Text = MVEmpresas.RAZONSOCIAL;
            txtDNombreComercial.Text = MVEmpresas.NOMBRECOMERCIAL;
            txtDRfc.Text = MVEmpresas.RFC;
            DDLDEstatus.SelectedIndex = DDLDEstatus.Items.IndexOf(DDLDEstatus.Items.FindByValue(MVEmpresas.StrEstatus));
            DDLDTipoDeEmpresa.SelectedIndex = DDLDTipoDeEmpresa.Items.IndexOf(DDLDTipoDeEmpresa.Items.FindByValue(MVEmpresas.StrTipoDeEmpresa));

            CargaGrid("Direccion");

            MVComision.ObtenerComisionPorEmpresa(new Guid(valor));
            chkbxComision.Checked = MVComision.BAbsorveComision;
            chkbxComisionTarjeta.Checked = MVComision.BIncluyeComisionTarjeta;
            //Obtiene el nombre del tipo de teleofno
            foreach (var item in MVTelefono.ListaDeTelefonos)
            {
                item.StrNombreTipoDeTelefono = DDLDTipoDETelefono.Items.FindByValue(item.UidTipo.ToString()).Text;
            }

            //Carga el gridview de los telefonos
            CargaGrid("Telefono");
            if (MVImagen.STRRUTA != null)
            {
                txtRutaImagen.Text = MVImagen.STRRUTA.ToString();
                if (txtRutaImagen.Text == Guid.Empty.ToString())
                {

                    ImageEmpresa.ImageUrl = "Img/Default.jpg";
                    ImageEmpresa.DataBind();
                }
                else
                {
                    ImageEmpresa.ImageUrl = MVImagen.STRRUTA;
                    ImageEmpresa.DataBind();
                }
            }

            GVDireccion.SelectedIndex = -1;
            DGVTELEFONOS.SelectedIndex = -1;

            lblEstado.Visible = false;
            PanelCargando.Visible = false;
        }

        #endregion
        #region Busqueda
        protected void BuscarEmpresasBusquedaNormal(object sender, EventArgs e)
        {
            if (txtFNombreComercial.Text == string.Empty && txtFRazonSocial.Text == string.Empty && txtFRfc.Text == string.Empty && DDLFEstatus.SelectedItem.Value == "0" && DDLFTipoDeEmpresa.SelectedItem.Value == "0")
            {
                MVEmpresas.BuscarEmpresas();
                CargaGrid("Normal");
                lblVisibilidadfiltros.Text = " Mostrar";
                PnlFiltros.Visible = false;
                btnBuscar.Enabled = false;
                btnBorrarFiltros.Enabled = false;
                btnBuscar.CssClass = "btn btn-sm btn-default disabled";
                btnBorrarFiltros.CssClass = "btn btn-sm btn-default disabled";
            }
            else
            {
                MVEmpresas.BuscarEmpresas(RazonSocial: txtFRazonSocial.Text, NombreComercial: txtFNombreComercial.Text, RFC: txtFRfc.Text, tipo: Convert.ToInt32(DDLFTipoDeEmpresa.SelectedItem.Value), status: Convert.ToInt32(DDLFEstatus.SelectedItem.Value));
                CargaGrid("Ampliada");
                CargaGrid("Normal");

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
            MVEmpresas.BuscarEmpresas();
            string RazonSocial = txtBARazonSocial.Text;
            string NombreComercial = txtBANombreComercial.Text;
            string RFC = txtBARF.Text;
            string CorreoElectronico = txtBACorreoElectronico.Text;
            string Calle = txtBACalle.Text;

            Guid Pais = new Guid(DDLDBAPAIS.SelectedItem.Value.ToString());
            Guid Estado = new Guid(DDLDBAESTADO.SelectedItem.Value.ToString());
            Guid Municipio = new Guid(DDLDBAMUNICIPIO.SelectedItem.Value.ToString());
            Guid Ciudad = new Guid(DDLDBACIUDAD.SelectedItem.Value.ToString());
            Guid Colonia = new Guid(DDLDBACOLONIA.SelectedItem.Value.ToString());

            string Estatus = DDLBAEstatus.SelectedItem.Value.ToString();
            string TipoDeEmpresa = DDLBATipo.SelectedItem.Value.ToString();


            if (NombreComercial == string.Empty && RazonSocial == string.Empty && RFC == string.Empty && Estatus == "0" && TipoDeEmpresa == "0" && CorreoElectronico == string.Empty && Calle == string.Empty && Pais == new Guid("00000000-0000-0000-0000-000000000000") && Estado == new Guid("00000000-0000-0000-0000-000000000000") && Municipio == new Guid("00000000-0000-0000-0000-000000000000") && Ciudad == new Guid("00000000-0000-0000-0000-000000000000") && Colonia == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                CargaGrid("Ampliada");
                lblBAFiltrosVisibilidad.Text = " Mostrar";
                PanelFiltrosBusquedaAmpliada.Visible = false;
                BtnBABuscar.Enabled = false;
                BtnBALimpiar.Enabled = false;
                BtnBABuscar.CssClass = "btn btn-sm btn-default disabled";
                BtnBALimpiar.CssClass = "btn btn-sm btn-default disabled";

            }
            else
            {
                MVEmpresas.BuscarEmpresas(RazonSocial: RazonSocial, NombreComercial: NombreComercial, RFC: RFC, tipo: Convert.ToInt32(TipoDeEmpresa), status: Convert.ToInt32(Estatus));
                CargaGrid("Ampliada");
                CargaGrid("Normal");

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
            lblUidEmpresa.Text = string.Empty;
            //Cambia el texto del boton para guardar
            lblGuardarDatos.CssClass = "glyphicon glyphicon-ok";
            //Limpia las cajas de texto del panel de gestion de datos
            LimpiarCajasDeTexto();
            QuitarColorACamposObligatorios();
            //Activa las cajas de texto del panel de gestion de datos
            TextboxActivados(ControlDeACcion: "Activado");
            //Manda valores por defecto a los dropdown list del panel de gestion de datos
            DDLDEstatus.SelectedIndex = 0;
            DDLDTipoDeEmpresa.SelectedIndex = 0;
            //Deshabilita boton de edicion
            btnEditar.CssClass = "btn btn-sm btn-default disabled";
            btnEditar.Enabled = false;
            DGVEMPRESAS.SelectedIndex = -1;
            //Imagen por default
            ImageEmpresa.ImageUrl = "Img/Default.jpg";
            ImageEmpresa.DataBind();

            //Limpia todo
            MVEmpresas = new VMEmpresas();
            MVDireccion = new VMDireccion();
            Session["MVEmpresas"] = MVEmpresas;
            Session["MVDireccion"] = MVDireccion;
            CargaGrid("Direccion");
            CargaGrid("Telefono");
        }
        protected void CancelarAgregacion(object sender, EventArgs e)
        {
            QuitarColorACamposObligatorios();


            if (string.IsNullOrEmpty(lblUidEmpresa.Text))
            {
                Session.Remove("Paneles");
                AccionesDeLaPagina = string.Empty;
                TextboxActivados();
                BorrarCamposDeGestion();
                QuitarEstiloCamposGestion();

                DDLDEstatus.Style.Add("background-color", "");
                DDLDTipoDeEmpresa.Style.Add("background-color", "");
                MVEmpresas = new VMEmpresas();
                MVDireccion = new VMDireccion();

                CargaGrid("Direccion");
                CargaGrid("Telefono");

                //Obtiene la rua a borrar
                if (Session["RutaImagen"] != null)
                {
                    string Ruta = Session["RutaImagen"].ToString();

                    //Borra la imagen de la empresa
                    if (File.Exists(Server.MapPath(Ruta)))
                    {
                        File.Delete(Server.MapPath(Ruta));
                    }
                    //Recarga el controlador de la imagen con una imagen default
                    ImageEmpresa.ImageUrl = "Img/Default.jpg";
                    ImageEmpresa.DataBind();
                }
                GVDireccion.Enabled = false;

            }
            if (!string.IsNullOrEmpty(lblUidEmpresa.Text))
            {
                AccionesDeLaPagina = string.Empty;
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                QuitarEstiloCamposGestion();
                btnEditar.CssClass = "btn btn-sm btn-default";
                btnEditar.Enabled = true;
                btnEditarTelefono.Enabled = false;
                btnEditarTelefono.CssClass = "btn btn-sm btn-default disabled";
                btnEdiarDireccion.CssClass = "btn btn-sm btn-default disabled";
                btnEdiarDireccion.Enabled = false;
                EstatusDeControlesPanelDerecho(false);
                MuestraEmpresaEnGestion(lblUidEmpresa.Text);
                btnNuevo.CssClass = "btn btn-sm btn-default";
                btnNuevo.Enabled = true;
                CargaGrid("Telefono");

                CargaGrid("Direccion");

                //Recargar el controlador con la imagen de la empresa
            }
            PanelDatosDireccion.Visible = false;
            PanelDeBusqueda.Visible = true;

            Session.Remove("Accion");
            DGVEMPRESAS.SelectedIndex = -1;
            GVDireccion.SelectedIndex = -1;
            DGVTELEFONOS.SelectedIndex = -1;

        }

        protected void BorrarCamposDeGestion()
        {
            btnEditar.Attributes.Add("CssClass", "btn btn-sm btn-default disabled");
            //Datos generales
            txtDRazonSocial.Text = string.Empty;
            txtDNombreComercial.Text = string.Empty;
            txtDRfc.Text = string.Empty;
            DDLDEstatus.SelectedIndex = 0;
            DDLDTipoDeEmpresa.SelectedIndex = 0;

            //Datos de direccion
            DDLDPais.SelectedIndex = 0;
            txtCalle1.Text = string.Empty;
            txtCalle2.Text = string.Empty;
            txtDManzana.Text = string.Empty;
            txtDLote.Text = string.Empty;
            txtDCodigoPostal.Text = string.Empty;
            txtDReferencia.Text = string.Empty;
            //Datos de contacto
            txtDTelefono.Text = string.Empty;
            txtDCorreoElectronico.Text = string.Empty;
        }

        protected void QuitarEstiloCamposGestion()
        {
            //Quitar estilo 
            txtDRazonSocial.Style.Add("background-color", "");
            txtDNombreComercial.Style.Add("background-color", "");
            txtDRfc.Style.Add("background-color", "");
        }
        #endregion
        #region Paneles de cajas de texto

        protected void EstatusDeControlesPanelDerecho(bool estatus)
        {
            //Visibilidad del boton guardar y cancelar
            btnGuardar.Visible = estatus;
            btnCancelar.Visible = estatus;
            //Descativar campos del panel de gestion
            txtDCorreoElectronico.Enabled = estatus;
            txtDNombreComercial.Enabled = estatus;
            txtDCorreoElectronico.Enabled = estatus;
            txtDNombreComercial.Enabled = estatus;
            txtDRazonSocial.Enabled = estatus;
            txtDRfc.Enabled = estatus;

            DDLDTipoDeEmpresa.Enabled = estatus;
            DDLDEstatus.Enabled = estatus;
            chkbxComision.Enabled = estatus;
            txtIdentificadorDeDireccion.Enabled = estatus;
            btnNuevaDireccion.Enabled = estatus;
            btnNuevoTelefono.Enabled = estatus;
            btnEdiarDireccion.Enabled = estatus;
            btnEditarTelefono.Enabled = estatus;
            BtnCargarImagen.Enabled = estatus;
            if (estatus)
            {
                BtnCargarImagen.CssClass = "btn btn-sm btn-default";
                btnNuevoTelefono.CssClass = "btn btn-sm btn-default";
                btnNuevaDireccion.CssClass = "btn btn-sm btn-default";
            }
            else
            {
                BtnCargarImagen.CssClass = "btn btn-sm btn-default disabled";
                btnNuevoTelefono.CssClass = "btn btn-sm btn-default disabled";
                btnNuevaDireccion.CssClass = "btn btn-sm btn-default disabled";
            }
            //Cambia el estatus de los gridview del panel
            GVDireccion.Enabled = estatus;
            DGVTELEFONOS.Enabled = estatus;
            //Carga los gridview del panel derecho
            CargaGrid("Telefono");
            CargaGrid("Direccion");
        }

        private void TextboxActivados(string ControlDeACcion = "")
        {
            if (AccionesDeLaPagina == "Edicion" && ControlDeACcion == "Desactivado")
            {
                EstatusDeControlesPanelDerecho(true);
                lblEstado.Text = "";
                btnNuevo.Enabled = false;
                btnNuevo.CssClass = "btn btn-sm btn-default disabled";
                lblGuardarDatos.CssClass = "glyphicon glyphicon-refresh";
                //Visibilidad de boton guardar y cancelar
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
                GVDireccion.Enabled = true;
            }
            else
         if (string.IsNullOrEmpty(lblUidEmpresa.Text) && ControlDeACcion == "Activado")
            {
                GVDireccion.Enabled = true;
                EstatusDeControlesPanelDerecho(true);
                lblEstado.Text = "";
                txtRutaImagen.Text = string.Empty;
                LimpiarCajasDeTexto();
                btnNuevo.Enabled = true;
                btnNuevo.CssClass = "btn btn-sm btn-default ";
                //Visibilidad de boton guardar y cancelar
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
            }
            else if (ControlDeACcion == "")
            {
                GVDireccion.Enabled = false;
                EstatusDeControlesPanelDerecho(false);
                btnNuevo.Enabled = true;
                LimpiarCajasDeTexto();
                btnNuevo.CssClass = "btn btn-sm btn-default ";
            }
        }


        private void LimpiarCajasDeTexto()
        {

            //Borrar datos de textbox
            txtDCorreoElectronico.Text = string.Empty;
            txtDNombreComercial.Text = string.Empty;
            txtDRazonSocial.Text = string.Empty;
            txtDRfc.Text = string.Empty;
            txtDTelefono.Text = string.Empty;
            lblEstado.Text = "";
            DDLDTipoDETelefono.SelectedIndex = -1;

            //Limpia los datos de direccion y de telefono
            MVDireccion.ListaDIRECCIONES = new List<VMDireccion>();
            MVTelefono.ListaDeTelefonos = new List<VMTelefono>();
        }
        protected void PanelGeneral(object sender, EventArgs e)
        {
            pnlDatosGenerales.Visible = true;
            pnlDireccion.Visible = false;
            pnlContacto.Visible = false;
            PanelDatosDireccion.Visible = false;
            PanelDeBusqueda.Visible = true;
            PanelMensaje.Visible = false;
            PnlComisiones.Visible = false;
            PnlComisiones.Visible = false;

            liDatosGenerales.Attributes.Add("class", "active");
            liDatosDireccion.Attributes.Add("class", " ");
            liDatosContacto.Attributes.Add("class", "");
            LiDatosComision.Attributes.Add("class", "");
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            TextboxActivados(ControlDeACcion: "Desactivado");
        }
        protected void PanelDireccion(object sender, EventArgs e)
        {
            pnlDatosGenerales.Visible = false;
            pnlDireccion.Visible = true;
            pnlContacto.Visible = false;
            PanelMensaje.Visible = false;
            PnlComisiones.Visible = false;
            PnlComisiones.Visible = false;
            liDatosGenerales.Attributes.Add("class", "");
            liDatosDireccion.Attributes.Add("class", "active");
            liDatosContacto.Attributes.Add("class", "");
            LiDatosComision.Attributes.Add("class", "");
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            TextboxActivados(ControlDeACcion: "Desactivado");
        }
        protected void PanelContacto(object sender, EventArgs e)
        {
            pnlDatosGenerales.Visible = false;
            pnlDireccion.Visible = false;
            pnlContacto.Visible = true;
            PanelDatosDireccion.Visible = false;
            PanelDeBusqueda.Visible = true;
            PanelMensaje.Visible = false;
            PnlComisiones.Visible = false;
            liDatosGenerales.Attributes.Add("class", "");
            liDatosDireccion.Attributes.Add("class", "");
            liDatosContacto.Attributes.Add("class", "active");
            LiDatosComision.Attributes.Add("Class", "");
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            TextboxActivados(ControlDeACcion: "Desactivado");
        }
        protected void PanelCoMision(object sender, EventArgs e)
        {
            pnlDatosGenerales.Visible = false;
            pnlDireccion.Visible = false;
            pnlContacto.Visible = false;
            PanelDatosDireccion.Visible = false;
            PanelDeBusqueda.Visible = true;
            PanelMensaje.Visible = false;
            PnlComisiones.Visible = true;
            liDatosGenerales.Attributes.Add("class", "");
            liDatosDireccion.Attributes.Add("class", "");
            liDatosContacto.Attributes.Add("class", "");
            LiDatosComision.Attributes.Add("Class", "active");
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            TextboxActivados(ControlDeACcion: "Desactivado");
        }
        protected void ActivarEdicion(object sender, EventArgs e)
        {
            CargaGrid("Telefono");
            CargaGrid("Direccion");
            AccionesDeLaPagina = "Edicion";
            TextboxActivados(ControlDeACcion: "Desactivado");
            PanelMensaje.Visible = false;
        }

        #endregion
        #region Imagen
        protected void SeleccionarImagen(object sender, EventArgs e)
        {
            BtnCargarImagen.Attributes.Add("onclick", "document.getElementById('" + FUImagen.ClientID + "').click()");
        }
        protected void AdjuntaImagen(object sender, EventArgs e)
        {
            FUImagen.Attributes.Add("onchange", "document.getElementById('" + btnSubirImagen.ClientID + "').click()");
        }

        protected void MuestraFoto(object sender, EventArgs e)
        {
            if (FUImagen.HasFile)
            {
                FileUpload FU = FUImagen;
                if (MVImagen.ValidarExtencionImagen(Path.GetExtension(FU.FileName).ToLower()))
                {
                    GuardarImagenGiro:
                    //Valida si el directorio existe en el servidor
                    string RUTA = "Img/Empresa/FotoPerfil";
                    if (Directory.Exists(Server.MapPath(RUTA)))
                    {
                        //Crea el directorio de la empresa

                        CrearCarpetaDeEmpresa:
                        if (Directory.Exists(Server.MapPath(RUTA)))
                        {
                            CrearArchivoServidor:
                            //El archivo no existe en el servidor
                            if (!File.Exists(Server.MapPath(txtRutaImagen.Text)))
                            {
                                string Nombre = Path.GetFileNameWithoutExtension(FU.FileName);
                                long Random = new Random().Next(999999999);
                                string RutaCompleta = RUTA + "/" + Random + Nombre + ".png";
                                txtRutaImagen.Text = RutaCompleta;

                                //Valida si el archivo existe
                                if (!File.Exists(RutaCompleta))
                                {
                                    System.Drawing.Image img = oImagenHelper.RedimensionarImagen(System.Drawing.Image.FromStream(FU.FileContent));
                                    //Guarda la imagen en el servidor
                                    img.Save(Server.MapPath("~/Vista/" + RutaCompleta), ImageFormat.Png);

                                    //Guarda la ruta en una session par poder ser manipulada en las acciones de guardar y actualizar
                                    Session["RutaImagen"] = RutaCompleta;

                                    //Numero Random para evitar el almacenamiento cache en el navegador
                                    string almacenamiento = RutaCompleta + "?" + (Random - 1);

                                    //Muestra la imagen en el control image de la vista
                                    ImageEmpresa.ImageUrl = almacenamiento;
                                }
                                else
                                {
                                    lblEstado.Text = "Imagen existente en el sistema, favor de agregar otra.";
                                }
                            }
                            //Si el archivo existe lo elimina
                            else
                            {
                                File.Delete(Server.MapPath("~/Vista/" + txtRutaImagen.Text));
                                txtRutaImagen.Text = string.Empty;
                                goto CrearArchivoServidor;
                            }
                        }
                        else
                        {
                            Directory.CreateDirectory(Server.MapPath(RUTA));
                            goto CrearCarpetaDeEmpresa;
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(Server.MapPath(RUTA));
                        goto GuardarImagenGiro;
                    }
                }
                else
                {
                    lblEstado.Text = "Formato de imagen incompatible./nFormatos validos: .jpg,.jpeg,.png";
                }
            }
        }
        #endregion

        protected void GuardarDatos(object sender, EventArgs e)
        {
            QuitarColorACamposObligatorios();
            if (txtDRazonSocial.Text != string.Empty && txtDNombreComercial.Text != string.Empty && txtDRfc.Text != string.Empty && DDLDEstatus.SelectedItem.Value != "0" && DDLDTipoDeEmpresa.SelectedItem.Value != "0")
            {
                PanelCargando.Visible = true;
                #region Variables
                //Datos generales
                string RS = txtDRazonSocial.Text;
                string NC = txtDNombreComercial.Text;
                string Rfc = txtDRfc.Text;
                int Estatus = Convert.ToInt32(DDLDEstatus.SelectedItem.Value);
                int TipoDeEmpresa = Convert.ToInt32(DDLDTipoDeEmpresa.SelectedItem.Value);


                string Referencia = txtDReferencia.Text;

                //Datos de contacto
                string Telefono = txtDTelefono.Text;
                string Correo = txtDCorreoElectronico.Text;

                //Variable de resultado
                bool resultado = false;


                #endregion

                if (string.IsNullOrEmpty(lblUidEmpresa.Text))
                {

                    if (MVEmpresas.ValidaRfc(Rfc) != true)
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "No se puede agregar un RFC ya existente en el sistema";
                    }
                    else if (MVEmpresas.ValidaCorreoElectronico(Correo) != true)
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "No se puede agregar un correo electronico ya existente en el sistema";
                    }

                    #region Guardar datos

                    Guid UidEmpresa = Guid.NewGuid();
                    resultado = MVEmpresas.GuardarEmpresaSuministradora(UidEmpresa, RS, NC, Rfc, TipoDeEmpresa, Estatus, chkbxComision.Checked, chkbxComisionTarjeta.Checked);

                    //Guarda los telefonos
                    if (MVTelefono.ListaDeTelefonos != null)
                    {
                        if (MVTelefono.ListaDeTelefonos.Count != 0)
                        {
                            MVTelefono.GuardaTelefono(UidEmpresa, "Empresa");
                        }
                    }
                    //Guarda las direcciones de la empresa
                    if (MVDireccion.ListaDIRECCIONES != null)
                    {
                        if (MVDireccion.ListaDIRECCIONES.Count != 0)
                        {
                            MVDireccion.GuardaListaDeDirecciones(MVDireccion.ListaDIRECCIONES, UidEmpresa, "asp_AgregaDireccionEmpresa", "Empresa");

                        }
                    }
                    //Guarda el correo electronico
                    if (!string.IsNullOrEmpty(txtDCorreoElectronico.Text))
                    {
                        MVCorreoElectronico.AgregarCorreo(UidEmpresa, "Empresa", txtDCorreoElectronico.Text, Guid.NewGuid());
                    }
                    //Guarda la imagen de la empresa
                    if (txtRutaImagen.Text != string.Empty)
                    {
                        MVImagen.GuardaImagen(txtRutaImagen.Text, UidEmpresa.ToString(), "asp_InsertaImagenEmpresa");
                    }
                    //Verifica si la empresa se ha guardado bien
                    if (resultado == true)
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Empresa agregada";
                    }
                    else if (resultado == false)
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Empresa no agregada";
                    }


                    Session.Remove("Paneles");
                    Session.Remove("Accion");
                    MVEmpresas = new VMEmpresas();
                    MVEmpresas.BuscarEmpresas();
                    CargaGrid("Normal");
                    AccionesDeLaPagina = string.Empty;
                    TextboxActivados();

                    #endregion


                }
                else
                if (!string.IsNullOrEmpty(lblUidEmpresa.Text))
                {
                    #region Actualizar datos
                    Guid UIDEMPRESA = new Guid(lblUidEmpresa.Text);
                    Guid IdCorreo = new Guid();
                    //Si la empresa no contiene un correo electronico y se agrega uno entonces crea un nuevo Guid para el correo
                    if (txtUidCorreoElectronico.Text != Guid.Empty.ToString())
                    {
                        IdCorreo = new Guid(txtUidCorreoElectronico.Text);
                    }
                    else
                    {
                        IdCorreo = Guid.NewGuid();
                    }
                    resultado = MVEmpresas.ActualizarDatos(UidEmpresa: UIDEMPRESA,
                        RazonSocial: RS,
                        NombreComercial: NC,
                        Rfc: Rfc,
                        TipoDeACtualizacion: "BackSite",
                        Tipo: TipoDeEmpresa,
                        Estatus: Estatus,
                        AbsorveComision: chkbxComision.Checked, IncluyeComisionTarjeta: chkbxComisionTarjeta.Checked);

                    if (resultado == true)
                    {
                        //Elimina y guarda las direcciones.
                        if (MVDireccion.ListaDIRECCIONES != null)
                        {
                            MVDireccion.EliminaDireccionesEmpresa(UIDEMPRESA);
                            if (MVDireccion.ListaDIRECCIONES.Count != 0)
                            {
                                MVDireccion.GuardaListaDeDirecciones(MVDireccion.ListaDIRECCIONES, UIDEMPRESA, "asp_AgregaDireccionEmpresa", "Empresa");

                            }
                        }
                        //Elimina y guarda los telefonos
                        if (MVTelefono.ListaDeTelefonos != null)
                        {
                            MVTelefono.EliminaTelefonoEmpresa(UIDEMPRESA.ToString());
                            if (MVTelefono.ListaDeTelefonos.Count != 0)
                            {
                                MVTelefono.GuardaTelefono(UIDEMPRESA, "Empresa");
                            }
                        }
                        //Elimina y guarda los correos electronicos
                        MVCorreoElectronico.EliminaCorreoEmpresa(UIDEMPRESA.ToString());
                        if (!string.IsNullOrEmpty(txtDCorreoElectronico.Text))
                        {
                            MVCorreoElectronico.AgregarCorreo(UIDEMPRESA, "Empresa", txtDCorreoElectronico.Text, Guid.NewGuid());
                        }
                        //Elimina y guarda la imagen
                        if (txtRutaImagen.Text != string.Empty)
                        {
                            MVImagen.ObtenerImagenPerfilDeEmpresa(UIDEMPRESA.ToString());

                            string Ruta = MVImagen.STRRUTA;
                            //Evalua que la ruta de imagen sea diferente a la ruta recuperada para saber si actualiza o no
                            if (Ruta != txtRutaImagen.Text)
                            {
                                if (File.Exists(Server.MapPath(Ruta)))
                                {
                                    File.Delete(Server.MapPath(Ruta));
                                }

                                MVImagen.EliminaImagenEmpresa(UIDEMPRESA.ToString());
                                MVImagen.ActualizarImagenEmpresa(MVImagen.ID.ToString(), txtRutaImagen.Text);
                            }

                        }
                        //Mensaje al usuario
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Empresa actualizada";
                    }
                    else if (resultado == false)
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Servicio no disponible, intente más tarde.";
                    }

                    Session.Remove("Accion");
                    MVEmpresas = new VMEmpresas();
                    MVEmpresas.BuscarEmpresas();
                    CargaGrid("Normal");
                    AccionesDeLaPagina = string.Empty;
                    TextboxActivados();
                    DGVEMPRESAS.SelectedIndex = -1;
                    #endregion
                }

                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                PanelIzquierdo.Visible = true;
                pnlDireccion.Visible = false;
                PanelDeBusqueda.Visible = true;

                GVDireccion.SelectedIndex = -1;
                GVDireccion.Enabled = false;
                PanelCargando.Visible = false;
            }
            else
            {
                if (txtDRazonSocial.Text == string.Empty || txtDRazonSocial.Text == " ")
                {
                    txtDRazonSocial.BorderColor = System.Drawing.Color.Red;
                }
                if (txtDNombreComercial.Text == string.Empty || txtDNombreComercial.Text == " ")
                {
                    txtDNombreComercial.BorderColor = System.Drawing.Color.Red;
                }
                if (txtDRfc.Text == string.Empty || txtDRfc.Text == " ")
                {
                    txtDRfc.BorderColor = System.Drawing.Color.Red;
                }
                if (DDLDEstatus.SelectedIndex < 0)
                {
                    DDLDEstatus.BorderColor = System.Drawing.Color.Red;
                }

                if (DDLDTipoDeEmpresa.SelectedIndex < 0)
                {
                    DDLDTipoDeEmpresa.BorderColor = System.Drawing.Color.Red;
                }
            }

        }

        protected void QuitarColorACamposObligatorios()
        {
            txtDRazonSocial.BorderColor = System.Drawing.Color.White;
            txtDNombreComercial.BorderColor = System.Drawing.Color.White;
            txtDRfc.BorderColor = System.Drawing.Color.White;
            DDLDEstatus.BorderColor = System.Drawing.Color.White;
            DDLDTipoDeEmpresa.BorderColor = System.Drawing.Color.White;
        }

        #endregion

        #region Panel de direccion

        #region Dropdownlist
        protected void MuestraEstados(string id, string tipo)
        {
            Guid Pais = new Guid(id);
            if (tipo == "Gestion")
            {
                DDLDEstado.DataSource = MVDireccion.Estados(Pais);
                DDLDEstado.DataValueField = "IdEstado";
                DDLDEstado.DataTextField = "Nombre";
                DDLDEstado.DataBind();
            }

            if (tipo == "Filtro")
            {
                DDLDBAESTADO.DataSource = MVDireccion.Estados(Pais);
                DDLDBAESTADO.DataValueField = "IdEstado";
                DDLDBAESTADO.DataTextField = "Nombre";
                DDLDBAESTADO.DataBind();
            }
        }
        protected void MuestraMunicipio(string id, string tipo)
        {
            Guid estado = new Guid(id);
            if (tipo == "Gestion")
            {
                DDLDMunicipio.DataSource = MVDireccion.Municipios(estado);
                DDLDMunicipio.DataTextField = "NOMBRE";
                DDLDMunicipio.DataValueField = "IDMUNICIPIO";
                DDLDMunicipio.DataBind();
            }
            if (tipo == "Filtro")
            {
                DDLDBAMUNICIPIO.DataSource = MVDireccion.Municipios(estado);
                DDLDBAMUNICIPIO.DataTextField = "NOMBRE";
                DDLDBAMUNICIPIO.DataValueField = "IDMUNICIPIO";
                DDLDBAMUNICIPIO.DataBind();
            }
        }
        protected void MuestraCiudad(string id, string tipo)
        {
            Guid Municipio = new Guid(id);
            if (tipo == "Gestion")
            {
                DDLDCiudad.DataSource = MVDireccion.Ciudades(Municipio);
                DDLDCiudad.DataTextField = "Nombre";
                DDLDCiudad.DataValueField = "IdCiudad";
                DDLDCiudad.DataBind();
            }
            if (tipo == "Filtro")
            {
                DDLDBACIUDAD.DataSource = MVDireccion.Ciudades(Municipio);
                DDLDBACIUDAD.DataTextField = "Nombre";
                DDLDBACIUDAD.DataValueField = "IdCiudad";
                DDLDBACIUDAD.DataBind();
            }
        }
        protected void MuestraColonia(string id, string tipo)
        {
            Guid Ciudad = new Guid(id);
            if (tipo == "Gestion")
            {
                DDLDColonia.DataSource = MVDireccion.Colonias(Ciudad);
                DDLDColonia.DataTextField = "Nombre";
                DDLDColonia.DataValueField = "IdColonia";
                DDLDColonia.DataBind();
            }
            if (tipo == "Filtro")
            {
                DDLDBACOLONIA.DataSource = MVDireccion.Colonias(Ciudad);
                DDLDBACOLONIA.DataTextField = "Nombre";
                DDLDBACOLONIA.DataValueField = "IdColonia";
                DDLDBACOLONIA.DataBind();
            }
        }

        protected void ObtenerEstado(object sender, EventArgs e)
        {
            if (DDLDPais.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                MuestraEstados(DDLDPais.SelectedItem.Value.ToString(), "Gestion");
                MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Gestion");
                MuestraCiudad("00000000-0000-0000-0000-000000000000", "Gestion");
                MuestraColonia("00000000-0000-0000-0000-000000000000", "Gestion");
            }
            else
            if (DDLDBAPAIS.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                MuestraEstados(DDLDBAPAIS.SelectedItem.Value.ToString(), "Filtro");
                MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Filtro");
                MuestraCiudad("00000000-0000-0000-0000-000000000000", "Filtro");
                MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");
            }
        }
        protected void ObtenerMunicipio(object sender, EventArgs e)
        {
            if (DDLDEstado.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                MuestraMunicipio(DDLDEstado.SelectedItem.Value.ToString(), "Gestion");
                MuestraCiudad("00000000-0000-0000-0000-000000000000", "Gestion");
                MuestraColonia("00000000-0000-0000-0000-000000000000", "Gestion");
            }
            else
            if (DDLDBAESTADO.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                MuestraMunicipio(DDLDBAESTADO.SelectedItem.Value.ToString(), "Filtro");
                MuestraCiudad("00000000-0000-0000-0000-000000000000", "Filtro");
                MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");
            }
        }
        protected void ObtenerCiudad(object sender, EventArgs e)
        {
            if (DDLDMunicipio.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                MuestraCiudad(DDLDMunicipio.SelectedItem.Value.ToString(), "Gestion");
                MuestraColonia("00000000-0000-0000-0000-000000000000", "Gestion");

            }
            else
            if (DDLDBAMUNICIPIO.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                MuestraCiudad(DDLDBAMUNICIPIO.SelectedItem.Value.ToString(), "Filtro");
                MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");
            }
        }
        protected void ObtenerColonia(object sender, EventArgs e)
        {
            if (DDLDMunicipio.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                MuestraColonia(DDLDCiudad.SelectedItem.Value.ToString(), "Gestion");
            }
            else
            if (DDLDBAMUNICIPIO.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                MuestraColonia(DDLDBACIUDAD.SelectedItem.Value.ToString(), "Filtro");
            }
        }
        protected void ObtenerCP(object sender, EventArgs e)
        {
            Guid Colonia = new Guid(DDLDColonia.SelectedItem.Value.ToString());
            txtDCodigoPostal.Text = MVDireccion.ObtenerCodigoPostal(Colonia);
        }

        #endregion

        protected void NuevaDireccion(object sender, EventArgs e)
        {
            txtIdDireccion.Text = string.Empty;
            PanelDeBusqueda.Visible = false;
            PanelDatosDireccion.Visible = true;
            btnGuardarDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;
            btnEdiarDireccion.Enabled = false;
            btnEdiarDireccion.CssClass = "btn btn-default btn-sm disabled";
            ActivarCamposDeDireccion();
            LimpiarCamposDeDireccion();
            QuitabordesCajasDeTexto();
            lblDatosDireccion.CssClass = "glyphicon glyphicon-ok";
        }
        protected void ActivaEdicionDeDireccion(object sender, EventArgs e)
        {
            ActivarCamposDeDireccion();
            btnGuardarDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;
            lblDatosDireccion.CssClass = "glyphicon glyphicon-refresh";
        }
        protected void OcultarPanelDireccion(object sender, EventArgs e)
        {
            PanelDeBusqueda.Visible = true;
            PanelDatosDireccion.Visible = false;
            btnEdiarDireccion.Enabled = false;
            btnEdiarDireccion.CssClass = "btn btn-sm btn-default disabled";
            LimpiarCamposDeDireccion();
            DesactivarCamposDeDireccion();
        }
        protected void ActivarCamposDeDireccion()
        {
            //Datos de la direccion
            txtIdentificadorDeDireccion.Enabled = true;
            DDLDMunicipio.Enabled = true;
            DDLDColonia.Enabled = true;
            DDLDEstado.Enabled = true;
            txtDReferencia.Enabled = true;
            txtDManzana.Enabled = true;
            DDLDPais.Enabled = true;
            txtDLote.Enabled = true;
            txtCalle0.Enabled = true;
            txtCalle1.Enabled = true;
            txtCalle2.Enabled = true;
            txtDCodigoPostal.Enabled = true;
            DDLDCiudad.Enabled = true;
        }
        protected void DesactivarCamposDeDireccion()
        {
            //Datos de la direccion
            txtIdentificadorDeDireccion.Enabled = false;
            DDLDMunicipio.Enabled = false;
            DDLDColonia.Enabled = false;
            DDLDEstado.Enabled = false;
            txtDReferencia.Enabled = false;
            txtDManzana.Enabled = false;
            DDLDPais.Enabled = false;
            txtDLote.Enabled = false;
            txtCalle0.Enabled = false;
            txtCalle1.Enabled = false;
            txtCalle2.Enabled = false;
            txtDCodigoPostal.Enabled = false;
            DDLDCiudad.Enabled = false;
        }
        protected void AgregaDireccion(object sender, EventArgs e)
        {
            Guid UidPais = new Guid(DDLDPais.SelectedItem.Value.ToString());
            Guid UidEstado = new Guid(DDLDEstado.SelectedItem.Value);
            Guid UidMunicipio = new Guid(DDLDMunicipio.SelectedItem.Value);
            Guid UidCiudad = new Guid(DDLDCiudad.SelectedItem.Value.ToString());
            Guid UidColonia = new Guid(DDLDColonia.SelectedItem.Value.ToString());
            string NOMBRECIUDAD = MVDireccion.ObtenerNombreDeLaCiudad(DDLDCiudad.SelectedItem.Value.ToString());
            string NOMBRECOLONIA = MVDireccion.ObtenerNombreDeLaColonia(DDLDColonia.SelectedItem.Value.ToString());
            string Calle = txtCalle0.Text;
            string Calle1 = txtCalle1.Text;
            string Calle2 = txtCalle2.Text;
            string Manzana = txtDManzana.Text;
            string CodigoPostal = txtDCodigoPostal.Text;
            string Lote = txtDLote.Text;
            string Identificador = txtIdentificadorDeDireccion.Text;

            //Campos requeridos de panel de direccion
            if (UidPais.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                DDLDPais.BorderColor = Color.Red;
            }
            if (UidEstado.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                DDLDEstado.BorderColor = Color.Red;
            }
            if (UidMunicipio.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                DDLDMunicipio.BorderColor = Color.Red;
            }
            if (UidCiudad.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                DDLDCiudad.BorderColor = Color.Red;
            }
            if (UidColonia.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                DDLDColonia.BorderColor = Color.Red;
            }
            if (Calle == string.Empty)
            {
                txtCalle0.BorderColor = Color.Red;
            }
            if (Calle1 == string.Empty)
            {
                txtCalle1.BorderColor = Color.Red;
            }
            if (Calle2 == string.Empty)
            {
                txtCalle2.BorderColor = Color.Red;
            }
            if (Manzana == string.Empty)
            {
                txtDManzana.BorderColor = Color.Red;
            }
            if (CodigoPostal == string.Empty)
            {
                txtDCodigoPostal.BorderColor = Color.Red;
            }
            if (Lote == string.Empty)
            {
                txtDLote.BorderColor = Color.Red;
            }
            if (Calle != string.Empty && Calle1 != string.Empty && Calle2 != string.Empty && Manzana != string.Empty && CodigoPostal != string.Empty && Lote != string.Empty)
            {
                if (txtIdDireccion.Text != string.Empty)
                {
                    MVDireccion.ActualizaListaDireccion(txtIdDireccion.Text, UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, Calle, Calle1, Calle2, Manzana, Lote, CodigoPostal, txtDReferencia.Text, Identificador, NOMBRECIUDAD, NOMBRECOLONIA);
                }
                else
                {
                    Guid UidDireccion = Guid.NewGuid();
                    MVDireccion.AgregaDireccionALista(UidDireccion, UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, Calle, Calle1, Calle2, Manzana, Lote, CodigoPostal, txtDReferencia.Text, NOMBRECOLONIA, NOMBRECIUDAD, Identificador);
                }
                GVDireccion.DataSource = MVDireccion.ListaDIRECCIONES;
                GVDireccion.DataBind();
                PanelDatosDireccion.Visible = false;
                PanelDeBusqueda.Visible = true;
                LimpiarCamposDeDireccion();

            }


        }
        protected void QuitabordesCajasDeTexto()
        {
            DDLDPais.BorderColor = Color.Empty;
            DDLDEstado.BorderColor = Color.Empty;
            DDLDMunicipio.BorderColor = Color.Empty;
            DDLDCiudad.BorderColor = Color.Empty;
            DDLDColonia.BorderColor = Color.Empty;
            txtCalle0.BorderColor = Color.Empty;
            txtCalle1.BorderColor = Color.Empty;
            txtCalle2.BorderColor = Color.Empty;
            txtDManzana.BorderColor = Color.Empty;
            txtDCodigoPostal.BorderColor = Color.Empty;
            txtDLote.BorderColor = Color.Empty;
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
        protected void CierraVentanaDireccion(object sender, EventArgs e)
        {
            PanelDatosDireccion.Visible = false;
            PanelDeBusqueda.Visible = true;
            btnEdiarDireccion.Enabled = false;
            GVDireccion.SelectedIndex = -1;
            btnEdiarDireccion.CssClass = "btn btn-default btn-sm disabled";
            QuitabordesCajasDeTexto();
        }
        protected void GVDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuitabordesCajasDeTexto();
            string valor = GVDireccion.SelectedDataKey.Value.ToString();
            DesactivarCamposDeDireccion();
            MVDireccion.ObtenDireccion(valor);
            PanelDatosDireccion.Visible = true;
            PanelDeBusqueda.Visible = false;
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
            txtIdentificadorDeDireccion.Text = MVDireccion.IDENTIFICADOR;

            //Activacion de botones
            btnEdiarDireccion.Enabled = true;
            btnEdiarDireccion.CssClass = "btn btn-sm btn-default";
            btnGuardarDireccion.Visible = false;
            btnCancelarDireccion.Visible = false;
        }
        protected void GVDireccion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GVDireccion, "Select$" + e.Row.RowIndex);
                var icono = e.Row.FindControl("lblEliminarTelefono") as Label;
                icono.CssClass = "glyphicon glyphicon-trash";

                LinkButton Eliminar = e.Row.FindControl("EliminaDireccion") as LinkButton;
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
        }
        protected void GVDireccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow Seleccionado = GVDireccion.Rows[index];
                GridView valor = (GridView)sender;
                string ID = valor.DataKeys[Seleccionado.RowIndex].Value.ToString();
                MVDireccion.QuitaDireeccionDeLista(ID);
                CargaGrid("Direccion");
            }
        }


        #endregion

        #region Telefonos

        protected void NuevoTelefono(object sender, EventArgs e)
        {
            btnGuardarTelefono.Visible = true;
            btnCancelarTelefono.Visible = true;
            btnEditarTelefono.Enabled = false;
            btnEditarTelefono.CssClass = "btn btn-sm btn-default disabled";
            DDLDTipoDETelefono.Enabled = true;
            txtDTelefono.Enabled = true;
            txtDTelefono.Text = string.Empty;
            DDLDTipoDETelefono.SelectedIndex = -1;
            IconActualizaTelefono.CssClass = "glyphicon glyphicon-ok";

        }

        protected void CancelarTelefono(object sender, EventArgs e)
        {
            btnGuardarTelefono.Visible = false;
            btnCancelarTelefono.Visible = false;
            DDLDTipoDETelefono.Enabled = false;
            txtDTelefono.Enabled = false;
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
            DGVTELEFONOS.DataSource = MVTelefono.ListaDeTelefonos;
            DGVTELEFONOS.DataBind();
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
            DGVTELEFONOS.DataSource = MVTelefono.ListaDeTelefonos;
            DGVTELEFONOS.DataBind();
        }


        protected void DGVTELEFONOS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVTELEFONOS, "Select$" + e.Row.RowIndex);

                LinkButton Eliminar = e.Row.FindControl("EliminaTelefono") as LinkButton;

                var icono = e.Row.FindControl("lblEliminarTelefono") as Label;

                icono.CssClass = "glyphicon glyphicon-trash";

                if (Session["Accion"] != null)
                {
                    AccionesDeLaPagina = Session["Accion"].ToString();
                }
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

                CargaGrid("Telefono");

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

                MVTelefono.ObtenTelefono(valor);
                btnEditarTelefono.Enabled = true;
                btnEditarTelefono.CssClass = "btn btn-sm btn-default";

                txtIdTelefono.Text = MVTelefono.ID.ToString();
                DDLDTipoDETelefono.SelectedIndex = DDLDTipoDETelefono.Items.IndexOf(DDLDTipoDETelefono.Items.FindByValue(MVTelefono.UidTipo.ToString()));
                txtDTelefono.Text = MVTelefono.NUMERO;

            }
        }
        protected void EditaTelefono(object sender, EventArgs e)
        {
            btnGuardarTelefono.Visible = true;
            btnCancelarTelefono.Visible = true;
            DDLDTipoDETelefono.Enabled = true;
            txtDTelefono.Enabled = true;
            IconActualizaTelefono.CssClass = "glyphicon glyphicon-refresh";
        }
        #endregion

        #region Panel Busqueda ampliada

        protected void BusquedaAvanzada(object sender, EventArgs e)
        {
            PanelBusquedaAmpliada.Visible = true;
            PanelDerecho.Visible = false;
            PanelIzquierdo.Visible = false;
            PanelDatosDireccion.Visible = false;
            PanelFiltrosBusquedaAmpliada.Visible = true;
            VisivilidadDeFiltros("Ampliada");


            txtBARazonSocial.Text = txtFRazonSocial.Text;

            txtBANombreComercial.Text = txtFNombreComercial.Text;

            txtBARF.Text = txtFRfc.Text;


            if (DDLFEstatus.SelectedItem.Value != "0")
            {
                DDLBAEstatus.SelectedIndex = DDLBAEstatus.Items.IndexOf(DDLBAEstatus.Items.FindByValue(DDLFEstatus.SelectedItem.Value.ToString()));
            }
            else
            {
                DDLBAEstatus.SelectedIndex = -1;
            }
            if (DDLFTipoDeEmpresa.SelectedItem.Value != "0")
            {
                DDLBATipo.SelectedIndex = DDLBATipo.Items.IndexOf(DDLBATipo.Items.FindByValue(DDLFTipoDeEmpresa.SelectedItem.Value.ToString()));
            }
            else
            {
                DDLBATipo.SelectedIndex = -1;
            }
        }
        protected void BusquedaNormal(object sender, EventArgs e)
        {
            PanelBusquedaAmpliada.Visible = false;
            PanelDerecho.Visible = true;
            PanelIzquierdo.Visible = true;
            PanelDatosDireccion.Visible = false;


            txtFRazonSocial.Text = txtBARazonSocial.Text;

            txtFNombreComercial.Text = txtBANombreComercial.Text;

            txtFRfc.Text = txtBARF.Text;


            if (DDLBAEstatus.SelectedItem.Value != "0")
            {
                DDLFEstatus.SelectedIndex = DDLFEstatus.Items.IndexOf(DDLFEstatus.Items.FindByValue(DDLBAEstatus.SelectedItem.Value.ToString()));
            }
            else
            {
                DDLFEstatus.SelectedIndex = -1;
            }
            if (DDLBATipo.SelectedItem.Value != "0")
            {
                DDLFTipoDeEmpresa.SelectedIndex = DDLFTipoDeEmpresa.Items.IndexOf(DDLFTipoDeEmpresa.Items.FindByValue(DDLBATipo.SelectedItem.Value.ToString()));
            }
            else
            {
                DDLFTipoDeEmpresa.SelectedIndex = -1;
            }
        }
        protected void BorrarFiltrosBusquedaAvanzada(object sender, EventArgs e)
        {
            //Cajas de texto 
            txtBARazonSocial.Text = string.Empty;
            txtBANombreComercial.Text = string.Empty;
            txtBARF.Text = string.Empty;
            txtBACorreoElectronico.Text = string.Empty;
            txtBACalle.Text = string.Empty;
            txtBACOdigoPostal.Text = string.Empty;

            //Dropdown list
            DDLBAEstatus.SelectedIndex = -1;
            DDLBATipo.SelectedIndex = -1;
            DDLDBAPAIS.SelectedIndex = -1;

            MuestraEstados("00000000-0000-0000-0000-000000000000", "Filtro");
            MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Filtro");
            MuestraCiudad("00000000-0000-0000-0000-000000000000", "Filtro");
            MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");

        }





        #endregion

        protected void TxtDRfc_TextChanged(object sender, EventArgs e)
        {
            MVEmpresas.BuscarEmpresas(RFC: txtDRfc.Text);
            if (MVEmpresas.LISTADEEMPRESAS.Count > 0 && lblUidEmpresa.Text == string.Empty)
            {
                txtDRfc.BorderColor = Color.Red;
                txtDRfc.Focus();
                PanelMensaje.Visible = true;
                LblMensaje.Text = "No se puede agregar un RFC ya existente en el sistema";
            }
            else
            {
                txtDRfc.BorderColor = Color.Empty;
            }
        }

        protected void BtnCerrarPanelMensaje_Click(object sender, EventArgs e)
        {
            PanelMensaje.Visible = false;
        }

        protected void DDLDTipoDeEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empresa = DDLDTipoDeEmpresa.SelectedItem.Text;
            switch (empresa.ToLower())
            {
                case "distribuidora":
                    chkbxComisionTarjeta.Checked = true;
                    chkbxComisionTarjeta.Enabled = false;
                    panelComisionGoDeliverix.Visible = false;
                    break;
                case "suministradora":
                    chkbxComisionTarjeta.Checked = false;
                    chkbxComisionTarjeta.Enabled = true;
                    panelComisionGoDeliverix.Visible = true;
                    break;
                default:
                    break;
            }
        }
    }
}