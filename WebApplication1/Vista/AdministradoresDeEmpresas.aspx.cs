using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;

namespace WebApplication1.Vista
{
    public partial class AdministradoresDeEmpresas : System.Web.UI.Page
    {
        #region Propiedades
        VMUsuarios MVUsuarios;
        VMDireccion MVDireccion;
        VMEstatus MVEstatus;
        VMTelefono MVTelefono;
        VMEmpresas MVEmpresa;
        VMCorreoElectronico MVCorreoElectronico;
        VMValidaciones MVValidaciones;
        string AccionesDeLaPagina = "";
        //Manja el Control par instanciar el Guid de la empresa en el sistema
        TextBox txtUidEmpresaSeleccionadaSistema;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MVUsuarios = new VMUsuarios();
                MVDireccion = new VMDireccion();
                MVEstatus = new VMEstatus();
                MVTelefono = new VMTelefono();
                MVEmpresa = new VMEmpresas();
                MVValidaciones = new VMValidaciones();
                MVCorreoElectronico = new VMCorreoElectronico();
                //Elimina las variables de sesion al entrar
                Session.Remove("Accion");
                Session["MVUsuarios"] = MVUsuarios;
                Session["MVDireccion"] = MVDireccion;
                Session["MVEstatus"] = MVEstatus;
                Session["MVTelefono"] = MVTelefono;
                Session["MVEmpresa"] = MVEmpresa;
                Session["MVCorreoElectronico"] = MVCorreoElectronico;
                Session["MVValidaciones"] = MVValidaciones;



                MVUsuarios.CargaPerfilesDeUsuario("76a96ff6-e720-4092-a217-a77a58a9bf0d");
                txtUidEmpresaSeleccionadaSistema = Master.FindControl("txtUidEmpresaSistema") as TextBox;

                #region Panel derecho
                #region Paneles
                //Botones
                btnEditar.Enabled = false;
                //Visibilidad de paneles
                pnlDatosGenerales.Visible = true;
                pnlDireccion.Visible = false;
                pnlContacto.Visible = false;
                panelDatosEmpresa.Visible = false;
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
                txtDApellidoPaterno.Attributes.Add("placeholder", "Apellido Paterno");
                txtDApellidoMaterno.Attributes.Add("placeholder", "Apellido Materno");
                txtDUsuario.Attributes.Add("placeholder", "Usuario");
                txtDReferencia.Attributes.Add("placeholder", "Referencia");
                txtDNombre.Attributes.Add("placeholder", "Nombre");
                txtDCorreoElectronico.Attributes.Add("placeholder", "Correo electronico");
                txtDTelefono.Attributes.Add("placeholder", "Telefono");
                txtdContrasena.Attributes.Add("placeholder", "Contraseña");
                //Desabilita los textbox al cargar la pagina
                TextboxActivados();
                //GridView telefonos
                DGVTELEFONOS.DataSource = null;
                DGVTELEFONOS.DataBind();
                #endregion
                #region DropdownList


                //Obtiene datos de estatus
                DDLDEstatus.DataSource = MVEstatus.ObtenerListaActiva();
                DDLDEstatus.DataTextField = "NOMBRE";
                DDLDEstatus.DataValueField = "IdEstatus";
                DDLDEstatus.DataBind();

                MVTelefono.TipoDeTelefonos();
                DDLDTipoDETelefono.DataSource = MVTelefono.TIPOTELEFONO;
                DDLDTipoDETelefono.DataValueField = "ID";
                DDLDTipoDETelefono.DataTextField = "NOMBRE";
                DDLDTipoDETelefono.DataBind();

                #endregion
                #region Limites
                txtDUsuario.MaxLength = 150;
                txtDNombre.MaxLength = 100;
                txtDApellidoPaterno.MaxLength = 30;
                txtDApellidoMaterno.MaxLength = 30;
                txtCalle1.MaxLength = 100;
                txtCalle2.MaxLength = 100;
                txtDManzana.MaxLength = 4;
                txtDLote.MaxLength = 8;
                txtDReferencia.MaxLength = 500;
                #endregion

                #endregion
                #region Panel izquierdo
                #region Filtros
                //Panel de filtros          
                PnlFiltros.Visible = false;
                lblVisibilidadfiltros.Text = " Mostrar";
                //Placeholders del panel de filtros

                txtFApellido.Attributes.Add("placeholder", "Apellido");
                txtFUsuario.Attributes.Add("placeholder", "Usuario");
                txtFNombreDeUsuario.Attributes.Add("placeholder", "Nombre");
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

                DDLFEstatus.DataSource = MVEstatus.ListaEstatus;
                DDLFEstatus.DataTextField = "NOMBRE";
                DDLFEstatus.DataValueField = "ID";
                DDLFEstatus.DataBind();

                //Alimenta dropdownlist del pais en busqueda avanzada.
                DDLDBAPAIS.DataSource = MVDireccion.Paises();
                DDLDBAPAIS.DataTextField = "Nombre";
                DDLDBAPAIS.DataValueField = "UidPais";
                DDLDBAPAIS.DataBind();


                //DDLFPERFILDEUSUARIO.DataTextField = "NOMBRE";
                //DDLFPERFILDEUSUARIO.DataValueField = "ID";
                //DDLFPERFILDEUSUARIO.DataSource = MVUsuarios.Perfil;
                //DDLFPERFILDEUSUARIO.DataBind();

                #endregion

                CargaGrid("Normal");
                #endregion


                #region Panel de direccion

                PanelDatosDireccion.Visible = false;
                GVDireccion.DataSource = null;
                GVDireccion.DataBind();
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

                //Obtiene estatus
                DDLBAEstatus.DataSource = MVEstatus.ListaEstatus;
                DDLBAEstatus.DataTextField = "NOMBRE";
                DDLBAEstatus.DataValueField = "ID";
                DDLBAEstatus.DataBind();

                //Obtiene datos de los perfiles de usuario
                //DDLBAPERFIL.DataTextField = "NOMBRE";
                //DDLBAPERFIL.DataValueField = "ID";
                //DDLBAPERFIL.DataSource = MVUsuarios.Perfil;
                //DDLBAPERFIL.DataBind();

                MuestraEstados("00000000-0000-0000-0000-000000000000", "Filtro");
                MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Filtro");
                MuestraCiudad("00000000-0000-0000-0000-000000000000", "Filtro");
                MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");
                #endregion

                #region Placeholders
                txtBaApellido.Attributes.Add("placeholder", "Apellido");
                txtBANombre.Attributes.Add("placeholder", "Nombre");
                txtBACorreoElectronico.Attributes.Add("placeholder", "Correo electronico");
                txtBACalle.Attributes.Add("placeholder", "Calle");
                txtBACOdigoPostal.Attributes.Add("placeholder", "Codigo Postal");
                txtBAUSuario.Attributes.Add("placeholder", "Usuario");
                #endregion

                #region Gridview Busqueda ampliada

                #endregion

                #endregion

                #region Busqueda de empresa


                txtdNombreComercial.Attributes.Add("placeholder", "Nombre comercial");
                txtDRazonSocial.Attributes.Add("placeholder", "Razon social");
                txtdRFC.Attributes.Add("placeholder", "RFC");

                txtdNombreComercial.Text = string.Empty;
                txtDRazonSocial.Text = string.Empty;
                txtdRFC.Text = string.Empty;
                #endregion

                PanelMensaje.Visible = false;
            }
            else
            {
                MVValidaciones = (VMValidaciones)Session["MVValidaciones"];
                MVUsuarios = (VMUsuarios)Session["MVUsuarios"];
                MVDireccion = (VMDireccion)Session["MVDireccion"];
                MVEstatus = (VMEstatus)Session["MVEstatus"];
                MVTelefono = (VMTelefono)Session["MVTelefono"];
                MVEmpresa = (VMEmpresas)Session["MVEmpresa"];
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
            //DDLFPERFILDEUSUARIO.SelectedIndex = 0;
            txtFApellido.Text = string.Empty;
            txtFNombreDeUsuario.Text = string.Empty;
            DDLFEstatus.SelectedIndex = 0;
        }

        #endregion
        #region GridView

        #region GridView Busqueda Normal
        protected void GVWEmpresaNormal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVEMPRESAS, "Select$" + e.Row.RowIndex);
                // Obtiene los datos de la vista del modelo
                Session["MVUsuarios"] = MVUsuarios;

                Label NumeroDePagina = new Label();
                Label TotalDePaginas = new Label();


                GridViewRow PagerRow = DGVEMPRESAS.TopPagerRow;

                NumeroDePagina = PagerRow.Cells[0].FindControl("lblPaginaActual") as Label;
                TotalDePaginas = PagerRow.Cells[0].FindControl("lblTotalDePaginas") as Label;
                Label InformacionDeLaCantidadDeRegistrosMostrados = PagerRow.Cells[0].FindControl("lblTotalDeRegistros") as Label;
                ImageButton DobleDerecho = PagerRow.Cells[0].FindControl("btnUltimo") as ImageButton;
                ImageButton DobleIzquierdo = PagerRow.Cells[0].FindControl("btnPrimero") as ImageButton;
                ImageButton Izquierda = PagerRow.Cells[0].FindControl("btnAnterior") as ImageButton;
                ImageButton Derecha = PagerRow.Cells[0].FindControl("btnSiguiente") as ImageButton;
                DropDownList PaginasBusquedNormal = PagerRow.Cells[0].FindControl("ddlPaginasBusquedaNormal") as DropDownList;


                int PaginaActual = DGVEMPRESAS.PageIndex + 1;
                int Total = DGVEMPRESAS.PageCount;

                //Limpia la lista de elementos asociada al dropdownlist de la paginacion de la busqueda normal.
                PaginasBusquedNormal.Items.Clear();
                for (int i = 0; i < Total; i++)
                {
                    int Pagina = i + 1;
                    ListItem item = new ListItem(Pagina.ToString());
                    if (i == DGVEMPRESAS.PageIndex)
                    {
                        item.Selected = true;
                    }
                    PaginasBusquedNormal.Items.Add(item);
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
                else if (DGVEMPRESAS.PageSize > MVUsuarios.LISTADEUSUARIOS.Count)
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
                int Registros = MVUsuarios.LISTADEUSUARIOS.Count;
                int FilasPorPagina = DGVEMPRESAS.PageSize;
                int Filas = DGVEMPRESAS.Rows.Count;

                //Obtener la cantidad de registros dentro de la lista
                string CantidadDeRegistros = MVUsuarios.LISTADEUSUARIOS.Count.ToString();

                //Operaciones matematicas
                int RegistroFinal = Paginas * FilasPorPagina;
                int RegistroInicial = ((Paginas * FilasPorPagina) - FilasPorPagina) + 1;

                //Se asigna el texto al control de label con los resultados optenidos de las operaciones.
                string TextoAMostrar = string.Empty;
                if ((RegistroInicial + FilasPorPagina) >= Registros)
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

                Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                Label PERFIL = e.Row.FindControl("lblPerfil") as Label;
                if (e.Row.Cells[6].Text == "ACTIVO")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                    ESTATUS.ToolTip = "ACTIVO";
                }
                if (e.Row.Cells[6].Text == "INACTIVO")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                    ESTATUS.ToolTip = "INACTIVO";
                }
                if (e.Row.Cells[8].Text == "ADMINISTRADOR")
                {
                    PERFIL.CssClass = "glyphicon glyphicon-wrench";
                    PERFIL.ToolTip = "ADMINISTRADOR";
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

            TextboxActivados();


            string valor = DGVEMPRESAS.SelectedDataKey.Value.ToString();

            btnEditar.CssClass = "btn btn-sm btn-default";
            btnEditar.Enabled = true;

            MuestraEmpresaEnGestion(valor);
        }

        #endregion

        #region Gridview busqueda ampliada
        protected void GVWEmpresaAmpliada_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtiene los datos de la vista del modelo
                Session["MVUsuarios"] = MVUsuarios;

                //Se obtienen los controles dentro del GidView para ser asignados el propiedades
                GridViewRow PagerRow = DGVBUSQUEDAAMPLIADA.TopPagerRow;
                Label InformacionDeLaCantidadDeRegistrosMostrados = PagerRow.Cells[0].FindControl("lblTotalDeRegistros") as Label;
                ImageButton DobleDerecho = PagerRow.Cells[0].FindControl("btnUltimo") as ImageButton;
                ImageButton DobleIzquierdo = PagerRow.Cells[0].FindControl("btnPrimero") as ImageButton;
                ImageButton Izquierda = PagerRow.Cells[0].FindControl("btnAnterior") as ImageButton;
                ImageButton Derecha = PagerRow.Cells[0].FindControl("btnSiguiente") as ImageButton;
                DropDownList Tamanio = PagerRow.Cells[0].FindControl("DDLTAMANOGRIDAMPLIADA") as DropDownList;
                DropDownList PaginasBusquedaAmpliada = PagerRow.Cells[0].FindControl("DDLDBANUMERODEPAGINAS") as DropDownList;


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
                else if (DGVBUSQUEDAAMPLIADA.PageSize > MVUsuarios.LISTADEUSUARIOS.Count)
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
                int Registros = MVUsuarios.LISTADEUSUARIOS.Count;
                int FilasPorPagina = DGVBUSQUEDAAMPLIADA.PageSize;
                int Filas = DGVBUSQUEDAAMPLIADA.Rows.Count;

                //Obtener la cantidad de registros dentro de la lista
                string CantidadDeRegistros = MVUsuarios.LISTADEUSUARIOS.Count.ToString();

                //Operaciones matematicas
                int RegistroFinal = Paginas * FilasPorPagina;
                int RegistroInicial = ((Paginas * FilasPorPagina) - FilasPorPagina) + 1;

                //Se asigna el texto al control de label con los resultados optenidos de las operaciones.
                string TextoAMostrar = string.Empty;
                if ((RegistroInicial + FilasPorPagina) >= Registros)
                {
                    RegistroFinal = Convert.ToInt32(CantidadDeRegistros);
                    TextoAMostrar = "" + RegistroInicial + " - " + RegistroFinal + " de " + CantidadDeRegistros + "";
                }
                else
                {
                    TextoAMostrar = "" + RegistroInicial + " - " + RegistroFinal + " de " + CantidadDeRegistros + "";
                }

                InformacionDeLaCantidadDeRegistrosMostrados.Text = TextoAMostrar;

                //Se agrega el evento onclick a la etiqueta "Select$" del GridView
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVBUSQUEDAAMPLIADA, "Select$" + e.Row.RowIndex);

                Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                Label PERFIL = e.Row.FindControl("lblPerfil") as Label;
                if (e.Row.Cells[6].Text == "ACTIVO")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                    ESTATUS.ToolTip = "ACTIVO";
                }
                if (e.Row.Cells[6].Text == "INACTIVO")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                    ESTATUS.ToolTip = "INACTIVO";
                }
                if (e.Row.Cells[8].Text == "ADMINISTRADOR")
                {
                    PERFIL.CssClass = "glyphicon glyphicon-wrench";
                    PERFIL.ToolTip = "ADMINISTRADOR";
                }
            }

        }
        protected void TamanioGrid(object sender, EventArgs e)
        {
            int cantidad = int.Parse(((DropDownList)sender).SelectedValue);


            DGVBUSQUEDAAMPLIADA.PageSize = cantidad;
            DGVBUSQUEDAAMPLIADA.DataSource = MVUsuarios.LISTADEUSUARIOS;
            DGVBUSQUEDAAMPLIADA.DataBind();


        }
        protected void PaginaSeleccionadaBusquedaAmpliada(object sender, EventArgs e)
        {
            int valor = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            DGVBUSQUEDAAMPLIADA.PageIndex = valor - 1;
            CargaGrid("Ampliada");
        }
        protected void GVBusquedaAvanzadaEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Paneles"] != null)
            {
                Session.Remove("Paneles");
            }
            AccionesDeLaPagina = string.Empty;
            Session.Remove("Accion");
            TextboxActivados();


            string valor = DGVBUSQUEDAAMPLIADA.SelectedDataKey.Value.ToString();

            btnEditar.CssClass = "btn btn-sm btn-default";
            btnEditar.Enabled = true;


            PanelBusquedaAmpliada.Visible = false;
            PanelDerecho.Visible = true;
            PanelIzquierdo.Visible = true;

            MuestraEmpresaEnGestion(valor);
        }
        protected void GridViewBusquedaAmpliada_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGVBUSQUEDAAMPLIADA.PageIndex = e.NewPageIndex;
            CargaGrid("Ampliada");
        }
        protected void BusquedaAmpliada_Sorting(object sender, GridViewSortEventArgs e)
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
        /// <summary>
        /// Carga el grid de acuerdo al que se necesite mostrar
        /// </summary>
        /// <param name="Busqueda">Los tipos de carga son Normal, Ampliada,Direccion,Telefono,Empresas</param>
        protected void CargaGrid(string Busqueda)
        {
            txtUidEmpresaSeleccionadaSistema = Master.FindControl("txtUidEmpresaSistema") as TextBox;
            switch (Busqueda)
            {
                case "Normal":
                    //Ejecuta el metodo que obtiene todos los usuarios de la base de datos y llama al metodo para cargar su respectivo gridvio pasando por parametro el grid que queemos que se cargue.
                    MVUsuarios.BusquedaDeUsuario(UIDPERFIL: new Guid("76a96ff6-e720-4092-a217-a77a58a9bf0d"), UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()));
                    DGVEMPRESAS.DataSource = MVUsuarios.LISTADEUSUARIOS;
                    DGVEMPRESAS.DataBind();
                    break;
                case "Ampliada":
                    DGVBUSQUEDAAMPLIADA.DataSource = MVUsuarios.LISTADEUSUARIOS;
                    DGVBUSQUEDAAMPLIADA.DataBind();
                    break;
                case "Direccion":
                    GVDireccion.DataSource = MVDireccion.ListaDIRECCIONES;
                    GVDireccion.DataBind();
                    break;
                case "Telefono":
                    //Obtiene el nombre del tipo de teleofno
                    foreach (var item in MVTelefono.ListaDeTelefonos)
                    {
                        item.StrNombreTipoDeTelefono = DDLDTipoDETelefono.Items.FindByValue(item.UidTipo.ToString()).Text;
                    }
                    DGVTELEFONOS.DataSource = MVTelefono.ListaDeTelefonos;
                    DGVTELEFONOS.DataBind();
                    break;
                case "Empresas":
                    DGVBUSQUEDADEEMPRESA.DataSource = MVEmpresa.LISTADEEMPRESAS;
                    DGVBUSQUEDADEEMPRESA.DataBind();
                    break;
                default:
                    break;
            }
        }
        protected void MuestraEmpresaEnGestion(string valor)
        {
            PanelMensaje.Visible = false;
            MVUsuarios.obtenerUsuario(valor);
            MVTelefono.BuscarTelefonos(UidPropietario: new Guid(valor), ParadetroDeBusqueda: "Usuario");
            MVDireccion.ObtenerDireccionesUsuario(valor);
            MVCorreoElectronico.BuscarCorreos(UidPropietario: new Guid(valor), strParametroDebusqueda: "Usuario");
            txtUidUsuario.Text = MVUsuarios.Uid.ToString();

            txtDNombre.Text = MVUsuarios.StrNombre;
            txtDApellidoPaterno.Text = MVUsuarios.StrApellidoPaterno;
            txtDApellidoMaterno.Text = MVUsuarios.StrApellidoMaterno;
            txtDUsuario.Text = MVUsuarios.StrUsuario;
            txtdContrasena.Text = MVUsuarios.StrCotrasena;
            DateTime Fecha = DateTime.Parse(MVUsuarios.DtmFechaDeNacimiento);
            txtDFechaDeNacimiento.Text = Fecha.ToString("yyyy-MM-dd");
            //txtDCorreoElectronico.Text = MVUsuarios.USUARIO.CORREO.CORREO;
            //txtdUidCorreo.Text = MVUsuarios.USUARIO.CORREO.ID.ToString();
            DDLDEstatus.SelectedIndex = DDLDEstatus.Items.IndexOf(DDLDEstatus.Items.FindByValue(MVUsuarios.StrEstatus));

            //Datos del correo electronico
            txtDCorreoElectronico.Text = MVCorreoElectronico.CORREO;
            txtUidCorreoElectronico.Text = MVCorreoElectronico.ID.ToString();

            lblEstado.Visible = false;



            CargaGrid("Direccion");
            CargaGrid("Telefono");

            //Campos de la empresa asociada
            MVEmpresa.ObtenerEmpresaUsuario(MVUsuarios.Uid.ToString());
            txtDRazonSocial.Text = MVEmpresa.RAZONSOCIAL;
            txtdNombreComercial.Text = MVEmpresa.NOMBRECOMERCIAL;
            txtdRFC.Text = MVEmpresa.RFC;
            txtIdEmpresa.Text = MVEmpresa.UIDEMPRESA.ToString();

            btnCambiarEmpresa.Enabled = true;
            btnBuscarEmpresa.Enabled = false;
        }
        private void Sort(string sortExpression, string Valor, string GridView)
        {
            switch (GridView)
            {
                case "Normal":
                    DGVEMPRESAS.DataSource = MVUsuarios.Sort(sortExpression, Valor); ;
                    DGVEMPRESAS.DataBind();
                    break;
                case "Ampliada":
                    DGVBUSQUEDAAMPLIADA.DataSource = MVUsuarios.Sort(sortExpression, Valor); ;
                    DGVBUSQUEDAAMPLIADA.DataBind();
                    break;
                default:
                    break;
            }

        }
        protected void DGVEMPRESAS_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            GridViewRow pagerRow = (GridViewRow)gv.TopPagerRow;

            if (pagerRow != null && pagerRow.Visible == false)
                pagerRow.Visible = true;
        }
        #endregion
        #region Busqueda
        protected void BuscarEmpresasBusquedaNormal(object sender, EventArgs e)
        {
            Guid UidPerfil = new Guid("76a96ff6-e720-4092-a217-a77a58a9bf0d");

            if (txtFApellido.Text == string.Empty && txtFNombreDeUsuario.Text == string.Empty && DDLFEstatus.SelectedItem.Value == "0" && txtFUsuario.Text == string.Empty)
            {
                //MVUsuarios.DatosGridViewBusquedaNormal("76a96ff6-e720-4092-a217-a77a58a9bf0d", Session["UidEmpresa"].ToString());
                MVUsuarios.BusquedaDeUsuario(UIDPERFIL: UidPerfil);
                CargaGrid("Ampliada");
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
                string Nombre = txtFNombreDeUsuario.Text;
                string Usuario = txtFUsuario.Text;
                string Apellido = txtFApellido.Text;
                string Estatus = DDLFEstatus.SelectedItem.Value;

                //MVUsuarios.BusquedaDeUsuario(Nombre, Usuario, Apellido, Estatus, new Guid("76a96ff6-e720-4092-a217-a77a58a9bf0d"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "");
                MVUsuarios.BusquedaDeUsuario(NOMBRE: Nombre, USER: Usuario, APELLIDO: Apellido, ESTATUS: Estatus, UIDPERFIL: UidPerfil);

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
        protected void BuscarEmpresasBusquedaAmpliada(object sender, EventArgs e)
        {

            string Nombre = txtBANombre.Text;
            string Usuario = txtBAUSuario.Text;
            string Apellido = txtBaApellido.Text;
            string Estatus = DDLBAEstatus.SelectedItem.Value;
            string CorreoElectronico = txtBACorreoElectronico.Text;
            string Calle = txtBACalle.Text;
            string CodigoPostal = txtBACOdigoPostal.Text;

            Guid Pais = new Guid(DDLDBAPAIS.SelectedItem.Value.ToString());
            Guid Estado = new Guid(DDLDBAESTADO.SelectedItem.Value.ToString());
            Guid Municipio = new Guid(DDLDBAMUNICIPIO.SelectedItem.Value.ToString());
            Guid Ciudad = new Guid(DDLDBACIUDAD.SelectedItem.Value.ToString());
            Guid Colonia = new Guid(DDLDBACOLONIA.SelectedItem.Value.ToString());

            Guid UidPerfil = new Guid("76a96ff6-e720-4092-a217-a77a58a9bf0d");

            if (Apellido == string.Empty && CodigoPostal == string.Empty && Nombre == string.Empty && Usuario == string.Empty && Estatus == "0")
            {
                MVUsuarios.BusquedaDeUsuario(UIDPERFIL: UidPerfil);

                CargaGrid("Normal");
                lblBAFiltrosVisibilidad.Text = " Mostrar";
                PanelFiltrosBusquedaAmpliada.Visible = false;
                BtnBABuscar.Enabled = false;
                BtnBALimpiar.Enabled = false;
                BtnBABuscar.CssClass = "btn btn-sm btn-default disabled";
                BtnBALimpiar.CssClass = "btn btn-sm btn-default disabled";
            }
            else if (Apellido != string.Empty || CodigoPostal != string.Empty || Nombre != string.Empty || Usuario != string.Empty || Estatus != "0")
            {
                MVUsuarios.BusquedaDeUsuario(NOMBRE: Nombre, USER: Usuario, APELLIDO: Apellido, ESTATUS: Estatus, UIDPERFIL: UidPerfil);
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
        protected void EstatusDeControlesPanelDerecho(bool estatus)
        {
            //Visibilidad del boton guardar y cancelar
            btnGuardar.Visible = estatus;
            btnCancelar.Visible = estatus;
            //Descativar campos del panel de gestion
            txtCalle0.Enabled = estatus;
            txtCalle1.Enabled = estatus;
            txtCalle2.Enabled = estatus;
            txtDCodigoPostal.Enabled = estatus;
            DDLDCiudad.Enabled = estatus;
            txtDCorreoElectronico.Enabled = estatus;
            txtDManzana.Enabled = estatus;
            txtDNombre.Enabled = estatus;
            DDLDPais.Enabled = estatus;
            txtDLote.Enabled = estatus;
            txtDUsuario.Enabled = estatus;
            txtdContrasena.Enabled = estatus;
            txtDFechaDeNacimiento.Enabled = estatus;
            txtDReferencia.Enabled = estatus;
            txtDApellidoPaterno.Enabled = estatus;
            txtDApellidoMaterno.Enabled = estatus;
            txtDTelefono.Enabled = estatus;
            DDLDEstado.Enabled = estatus;
            DDLDEstatus.Enabled = estatus;
            DDLDMunicipio.Enabled = estatus;
            DDLDColonia.Enabled = estatus;
            DDLDTipoDETelefono.Enabled = estatus;
            txtIdentificadorDeDireccion.Enabled = estatus;
            btnNuevaDireccion.Enabled = estatus;
            btnNuevoTelefono.Enabled = estatus;
            btnEdiarDireccion.Enabled = estatus;
            btnEditarTelefono.Enabled = estatus;
            btnBuscarEmpresa.Enabled = estatus;
            if (estatus)
            {
                btnNuevoTelefono.CssClass = "btn btn-sm btn-default";
                btnNuevaDireccion.CssClass = "btn btn-sm btn-default";
                //Panel de busqueda de empresa
                btnBuscarEmpresa.CssClass = "btn btn-sm btn-default ";
                btnCambiarEmpresa.CssClass = "btn btn-sm btn-default ";
            }
            else
            {
                //panelDeBusquedaDeEmpresa.Enabled = false;
                btnBuscarEmpresa.CssClass = "btn btn-sm btn-default disabled";
                btnCambiarEmpresa.CssClass = "btn btn-sm btn-default disabled";

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

        protected void ActivarCajasDeTexto(object sender, EventArgs e)
        {
            //Activar la sesion de panel y remover sesion de edicion
            Session["Paneles"] = "Activados";
            AccionesDeLaPagina = "NuevoRegistro";
            Session["Accion"] = AccionesDeLaPagina;
            Session.Remove("IdUsuarioSeleccionado");
            //Cambia el texto del boton para guardar
            lblGuardarDatos.CssClass = "glyphicon glyphicon-ok";
            //Limpia las cajas de texto del panel de gestion de datos
            LimpiarCajasDeTexto();
            //Activa las cajas de texto del panel de gestion de datos
            TextboxActivados(ControlDeACcion: "Activado");

            //Deshabilita boton de edicion
            btnEditar.CssClass = "btn btn-sm btn-default disabled";
            btnEditar.Enabled = false;

            DGVEMPRESAS.SelectedIndex = -1;
            MVUsuarios = new VMUsuarios();
            MVDireccion = new VMDireccion();
            MVTelefono = new VMTelefono();
            Session["MVUsuarios"] = MVUsuarios;
            Session["MVDireccion"] = MVDireccion;
            Session["MVTelefono"] = MVTelefono;
            CargaGrid("Direccion");
            CargaGrid("Telefono");
            QuitaEstiloACamposObligatorios();

        }
        protected void CancelarAgregacion(object sender, EventArgs e)
        {
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            if (Session["Paneles"] != null)
            {
                Session.Remove("Paneles");
                AccionesDeLaPagina = string.Empty;
                TextboxActivados();
                BorrarCamposDeGestion();
                QuitarEstiloCamposGestion();

                DDLDEstatus.Style.Add("background-color", "");
                MVUsuarios = new VMUsuarios();
                MVDireccion = new VMDireccion();
                Session["MVUsuarios"] = MVUsuarios;
                Session["MVDireccion"] = MVDireccion;
                CargaGrid("Direccion");
                CargaGrid("Telefono");

            }
            if (AccionesDeLaPagina == "Edicion")
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
                MuestraEmpresaEnGestion(txtUidUsuario.Text);
                btnNuevo.CssClass = "btn btn-sm btn-default";
                btnNuevo.Enabled = true;
                CargaGrid("Telefono");
                CargaGrid("Direccion");
            }
            QuitaEstiloACamposObligatorios();
            //Visibilidad del gridview de para buscar empresas
            DGVBUSQUEDADEEMPRESA.Visible = false;
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
            txtDUsuario.Text = string.Empty;
            txtDNombre.Text = string.Empty;
            txtDApellidoPaterno.Text = string.Empty;
            txtDApellidoMaterno.Text = string.Empty;
            txtdContrasena.Text = string.Empty;
            txtDFechaDeNacimiento.Text = string.Empty;
            DDLDEstatus.SelectedIndex = 0;

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
            txtDRazonSocial.Text = string.Empty;
            txtdRFC.Text = string.Empty;
            txtdNombreComercial.Text = string.Empty;

        }
        protected void QuitarEstiloCamposGestion()
        {
            //Quitar estilo 
            txtDUsuario.Style.Add("background-color", "");
            txtDNombre.Style.Add("background-color", "");
            txtDApellidoPaterno.Style.Add("background-color", "");
            txtDApellidoMaterno.Style.Add("background-color", "");
        }
        #endregion

        #region Paneles de cajas de texto
        
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
                //Habilita los datagrid
                GVDireccion.Enabled = true;
                DGVTELEFONOS.Enabled = true;
                //Carga los grid
                CargaGrid("Direccion");
                CargaGrid("Telefono");
            }
            else
            if (AccionesDeLaPagina == "NuevoRegistro" && ControlDeACcion == "Activado")
            {
                EstatusDeControlesPanelDerecho(true);
                lblEstado.Text = "";
                LimpiarCajasDeTexto();
                btnNuevo.Enabled = true;
                btnNuevo.CssClass = "btn btn-sm btn-default ";
                //Visibilidad de boton guardar y cancelar
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
                //Habilita los datagrid
                GVDireccion.Enabled = true;
                DGVTELEFONOS.Enabled = true;
                //Carga los grid
                CargaGrid("Direccion");
                CargaGrid("Telefono");
                txtUidUsuario.Text = string.Empty;
            }
            else
            if (!string.IsNullOrEmpty(txtUidUsuario.Text))
            {
                EstatusDeControlesPanelDerecho(false);
                btnNuevo.Enabled = true;
                btnNuevo.CssClass = "btn btn-sm btn-default ";
                //DesHabilita los datagrid
                GVDireccion.Enabled = false;
                DGVTELEFONOS.Enabled = false;
                //Carga los grid
                CargaGrid("Direccion");
                CargaGrid("Telefono");
            }
            else if (ControlDeACcion == "")
            {
                EstatusDeControlesPanelDerecho(false);
                btnNuevo.Enabled = true;
                LimpiarCajasDeTexto();
                btnNuevo.CssClass = "btn btn-sm btn-default ";
                //DesHabilita los datagrid
                GVDireccion.Enabled = false;
                DGVTELEFONOS.Enabled = false;
                //Carga los grid
                CargaGrid("Direccion");
                CargaGrid("Telefono");
            }
        }
        
        private void LimpiarCajasDeTexto()
        {

            //Borrar datos de textbox
            txtDCorreoElectronico.Text = string.Empty;
            txtDNombre.Text = string.Empty;
            txtDUsuario.Text = string.Empty;
            txtDApellidoPaterno.Text = string.Empty;
            txtDApellidoMaterno.Text = string.Empty;
            txtdContrasena.Text = string.Empty;
            txtDFechaDeNacimiento.Text = string.Empty;
            txtDTelefono.Text = string.Empty;
            lblEstado.Text = "";

            txtdRFC.Text = string.Empty;
            txtDRazonSocial.Text = string.Empty;
            txtdNombreComercial.Text = string.Empty;

            DDLDTipoDETelefono.SelectedIndex = -1;
        }
        protected void PanelGeneral(object sender, EventArgs e)
        {
            pnlDatosGenerales.Visible = true;
            pnlDireccion.Visible = false;
            pnlContacto.Visible = false;
            PanelDatosDireccion.Visible = false;
            panelDatosEmpresa.Visible = false;
            PanelDeBusqueda.Visible = true;

            liDatosGenerales.Attributes.Add("class", "active");
            liDatosDireccion.Attributes.Add("class", " ");
            liDatosContacto.Attributes.Add("class", "");
            liDatosDeEmpresa.Attributes.Add("class", "");
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
            panelDatosEmpresa.Visible = false;

            liDatosGenerales.Attributes.Add("class", "");
            liDatosDireccion.Attributes.Add("class", "active");
            liDatosContacto.Attributes.Add("class", "");
            liDatosDeEmpresa.Attributes.Add("class", "");
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
            panelDatosEmpresa.Visible = false;
            PanelDeBusqueda.Visible = true;

            liDatosGenerales.Attributes.Add("class", "");
            liDatosDireccion.Attributes.Add("class", "");
            liDatosContacto.Attributes.Add("class", "active");
            liDatosDeEmpresa.Attributes.Add("class", "");
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            TextboxActivados(ControlDeACcion: "Desactivado");
        }
        protected void ActivarEdicion(object sender, EventArgs e)
        {
            Session["Edicion"] = "Activado";
            lblGuardarDatos.CssClass = "glyphicon glyphicon-refresh";
            CargaGrid("Telefono");
            CargaGrid("Direccion");
            AccionesDeLaPagina = "Edicion";
            Session["Accion"] = AccionesDeLaPagina;
            TextboxActivados(ControlDeACcion: "Desactivado");
        }
        protected void PanelDeDatosDeEmpresa(object sender, EventArgs e)
        {
            pnlDatosGenerales.Visible = false;
            pnlDireccion.Visible = false;
            pnlContacto.Visible = false;
            PanelDatosDireccion.Visible = false;
            panelDatosEmpresa.Visible = true;
            PanelDeBusqueda.Visible = true;

            liDatosGenerales.Attributes.Add("class", "");
            liDatosDireccion.Attributes.Add("class", "");
            liDatosContacto.Attributes.Add("class", "");
            liDatosDeEmpresa.Attributes.Add("class", "active");
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            TextboxActivados(ControlDeACcion: "Desactivado");
        }

        #endregion

        protected void GuardarDatos(object sender, EventArgs e)
        {

            QuitaEstiloACamposObligatorios();


            if (txtDNombre.Text != string.Empty && txtDApellidoPaterno.Text != string.Empty && txtDApellidoMaterno.Text != string.Empty && DDLDEstatus.SelectedItem.Value != "0" && txtDRazonSocial.Text != string.Empty)
            {
                #region Variables
                //Datos generales
                string TIPODEUSUARIO = "Administrador";
                string Nombre = txtDNombre.Text;
                string ApellidoPaterno = txtDApellidoPaterno.Text;
                string ApellidoMaterno = txtDApellidoMaterno.Text;
                string usuario = txtDUsuario.Text;
                string password = txtdContrasena.Text;
                int Estatus = Convert.ToInt32(DDLDEstatus.SelectedItem.Value);
                string fechaDeNacimiento = txtDFechaDeNacimiento.Text;
                string PerfilDeUsuario = "76a96ff6-e720-4092-a217-a77a58a9bf0d";
                string estatus = DDLDEstatus.SelectedItem.Value.ToString();
                string IDEMpresa = txtIdEmpresa.Text;
                Guid IdEmpresa = Guid.NewGuid();
                if (IDEMpresa != string.Empty)
                {
                    IdEmpresa = new Guid(IDEMpresa);
                }
                string IDCorreo = txtdUidCorreo.Text;
                Guid UidCorreo = Guid.NewGuid();
                if (IDCorreo != string.Empty)
                {
                    UidCorreo = new Guid(IDCorreo);
                }
                string Correo = string.Empty;
                if (txtDCorreoElectronico.Text != string.Empty)
                {
                    Correo = txtDCorreoElectronico.Text;
                }


                //Variable de resultado
                bool resultado = false;
                AccionesDeLaPagina = Session["Accion"].ToString();
                #endregion

                if (string.IsNullOrEmpty(txtUidUsuario.Text))
                {
                    if (MVUsuarios.ValidarCorreoElectronicoDelUsuario(Correo) != true)
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "El correo electronico ya existe!";
                    }

                    else
                    {
                        MVUsuarios.BusquedaDeUsuario(USER:usuario);
                        if (MVUsuarios.LISTADEUSUARIOS.Count == 0)
                        {

                            #region Guardar datos
                            Guid UidUsuario = Guid.NewGuid();
                            resultado = MVUsuarios.GuardaUsuario(UidUsuario: UidUsuario, Nombre: Nombre, ApellidoPaterno: ApellidoPaterno, ApellidoMaterno: ApellidoMaterno, usuario: usuario, password: password, fnacimiento: fechaDeNacimiento, perfil: PerfilDeUsuario, estatus: estatus, IdEmpresa: IdEmpresa, TIPODEUSUARIO: TIPODEUSUARIO);

                            if (MVDireccion.ListaDIRECCIONES.Count > 0)
                            {
                                MVDireccion.GuardaListaDeDirecciones(MVDireccion.ListaDIRECCIONES, UidUsuario, "asp_AgregaDireccionUsuario", "Usuario");
                            }
                            //Guarda el correo electronico
                            if (!string.IsNullOrEmpty(txtDCorreoElectronico.Text))
                            {
                                MVCorreoElectronico.AgregarCorreo(UidUsuario, "Usuario", txtDCorreoElectronico.Text, Guid.NewGuid());
                            }
                            //Guarda los telefonos
                            if (MVTelefono.ListaDeTelefonos != null)
                            {
                                if (MVTelefono.ListaDeTelefonos.Count != 0)
                                {
                                    MVTelefono.GuardaTelefono(UidUsuario, "Usuario");
                                }
                            }
                            if (resultado == true)
                            {
                                PanelMensaje.Visible = true;
                                LblMensaje.Text = "Registro agregado!";
                            }
                            else if (resultado == false)
                            {
                                PanelMensaje.Visible = true;
                                LblMensaje.Text = "Registro no agregado!";
                            }
                            #endregion
                        }
                        else
                        {
                            PanelMensaje.Visible = true;
                            LblMensaje.Text = "El usuario ya existe!";
                        }
                    }
                }
                else
                if (!string.IsNullOrEmpty(txtUidUsuario.Text))
                {
                    #region Actualizar datos
                    Guid UidUsuario = new Guid(txtUidUsuario.Text);


                    resultado = MVUsuarios.ActualizarUsuario(UidUsuario, Nombre, ApellidoPaterno, ApellidoMaterno, usuario, password, fechaDeNacimiento, PerfilDeUsuario, estatus, IdEmpresa);

                    if (MVDireccion.ListaDIRECCIONES.Count > 0)
                    {
                        MVDireccion.GuardaListaDeDirecciones(MVDireccion.ListaDIRECCIONES, UidUsuario, "asp_AgregaDireccionUsuario", "Usuario");
                    }

                    //Elimina y guarda los correos electronicos
                    MVCorreoElectronico.EliminaCorreoUsuario(UidUsuario.ToString());
                    if (!string.IsNullOrEmpty(txtDCorreoElectronico.Text))
                    {
                        MVCorreoElectronico.AgregarCorreo(UidUsuario, "Usuario", txtDCorreoElectronico.Text, Guid.NewGuid());
                    }

                    //Elimina y Guarda los telefonos
                    if (MVTelefono.ListaDeTelefonos != null)
                    {
                        if (MVTelefono.ListaDeTelefonos.Count != 0)
                        {
                            MVTelefono.EliminaTelefonosUsuario(UidUsuario);
                            MVTelefono.GuardaTelefono(UidUsuario, "Usuario");
                        }
                    }
                    if (resultado == true)
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Registro actualizado!";
                    }
                    else if (resultado == false)
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Registro no actualizado!";
                    }
                    #endregion
                }

                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                pnlDireccion.Visible = false;
                PanelDeBusqueda.Visible = true;
                BorrarCamposDeGestion();
                Session.Remove("Accion");
                MVUsuarios = new VMUsuarios();
                txtUidEmpresaSeleccionadaSistema = Master.FindControl("txtUidEmpresaSistema") as TextBox;
                MVUsuarios.BusquedaDeUsuario(UIDPERFIL: new Guid("76a96ff6-e720-4092-a217-a77a58a9bf0d"), UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()));
                CargaGrid("Normal");
                AccionesDeLaPagina = string.Empty;
                TextboxActivados();
            }
            else
            {
                lblEstado.Text = "Favor de llenar los datos requeridos";
                if (txtDCorreoElectronico.Text == string.Empty)
                {
                    txtDCorreoElectronico.BorderColor = Color.Red;
                }
                if (txtDNombre.Text == string.Empty)
                {
                    txtDNombre.BorderColor = System.Drawing.Color.Red;
                }
                if (txtDApellidoMaterno.Text == string.Empty)
                {
                    txtDApellidoMaterno.BorderColor = System.Drawing.Color.Red;
                }
                if (txtDApellidoPaterno.Text == string.Empty)
                {
                    txtDApellidoPaterno.BorderColor = System.Drawing.Color.Red;
                }
                if (txtDUsuario.Text == string.Empty)
                {
                    txtDUsuario.BorderColor = System.Drawing.Color.Red;
                }
                if (txtdContrasena.Text == string.Empty)
                {
                    txtdContrasena.BorderColor = System.Drawing.Color.Red;
                }
                if (txtDRazonSocial.Text == string.Empty || txtdNombreComercial.Text == string.Empty || txtdRFC.Text == string.Empty)
                {
                    txtDRazonSocial.BorderColor = System.Drawing.Color.Red;
                    txtdNombreComercial.BorderColor = System.Drawing.Color.Red;
                    txtdRFC.BorderColor = System.Drawing.Color.Red;
                }
            }



        }
        protected void QuitaEstiloACamposObligatorios()
        {
            txtDCorreoElectronico.BorderColor = Color.Empty;
            txtDNombre.BorderColor = System.Drawing.Color.Empty;
            txtDApellidoMaterno.BorderColor = System.Drawing.Color.Empty;
            txtDApellidoPaterno.BorderColor = System.Drawing.Color.Empty;
            txtDUsuario.BorderColor = System.Drawing.Color.Empty;
            txtdContrasena.BorderColor = System.Drawing.Color.Empty;
            txtDRazonSocial.BorderColor = System.Drawing.Color.Empty;
            txtdNombreComercial.BorderColor = System.Drawing.Color.Empty;
            txtdRFC.BorderColor = System.Drawing.Color.Empty;
            txtDUsuario.BorderColor = Color.Empty;
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
            txtIdentificadorDeDireccion.Focus();
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
                    MVDireccion.AgregaDireccionALista(UidDireccion,UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, Calle, Calle1, Calle2, Manzana, Lote, CodigoPostal, txtDReferencia.Text, NOMBRECOLONIA, NOMBRECIUDAD, Identificador);
                }
                Session["MVDireccion"] = MVDireccion;
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
        /// <summary>
        /// Limpia los campos de la direccion
        /// </summary>
        protected void LimpiarCamposDeDireccion()
        {
            //Valida si no existe el identificador predeterminado
            string ValorDeIdentificador = string.Empty;
            if (!MVDireccion.ListaDIRECCIONES.Exists(D => D.IDENTIFICADOR == "Predeterminado"))
            {
                ValorDeIdentificador = "Predeterminado";
            }
            //Datos de direccion
            txtIdentificadorDeDireccion.Text = ValorDeIdentificador;
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
            btnEdiarDireccion.CssClass = "btn btn-default btn-sm disabled";
            QuitabordesCajasDeTexto();
        }
        protected void GVDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            if (AccionesDeLaPagina != string.Empty)
            {
                QuitabordesCajasDeTexto();
                string valor = GVDireccion.SelectedDataKey.Value.ToString();
                DesactivarCamposDeDireccion();
                Session["MVDireccion"] = MVDireccion;
                var direccion = MVDireccion.ObtenDireccion(valor);
                PanelDatosDireccion.Visible = true;
                PanelDeBusqueda.Visible = false;
                //Esconde el Id en un textbox
                txtIdDireccion.Text = direccion.ID.ToString();
                DDLDPais.SelectedIndex = DDLDPais.Items.IndexOf(DDLDPais.Items.FindByValue(direccion.PAIS));
                //Obtener lista de estado y seleccionar el del objeto
                MuestraEstados(direccion.PAIS, "Gestion");
                DDLDEstado.SelectedIndex = DDLDEstado.Items.IndexOf(DDLDEstado.Items.FindByValue(direccion.ESTADO));

                // Obtener lista de municipio y seleccionar el del objeto
                MuestraMunicipio(direccion.ESTADO, "Gestion");
                DDLDMunicipio.SelectedIndex = DDLDMunicipio.Items.IndexOf(DDLDMunicipio.Items.FindByValue(direccion.MUNICIPIO));

                // Obtener lista de ciudades y seleccionar el del objeto
                MuestraCiudad(direccion.MUNICIPIO, "Gestion");
                DDLDCiudad.SelectedIndex = DDLDCiudad.Items.IndexOf(DDLDCiudad.Items.FindByValue(direccion.CIUDAD));

                // Obtener lista de colonias y seleccionar el del objeto
                MuestraColonia(direccion.CIUDAD, "Gestion");
                DDLDColonia.SelectedIndex = DDLDColonia.Items.IndexOf(DDLDColonia.Items.FindByValue(direccion.COLONIA));

                txtCalle0.Text = direccion.CALLE0;
                txtCalle1.Text = direccion.CALLE1;
                txtCalle2.Text = direccion.CALLE2;
                txtDManzana.Text = direccion.MANZANA;
                txtDLote.Text = direccion.LOTE;
                txtDCodigoPostal.Text = direccion.CodigoPostal;
                txtDReferencia.Text = direccion.REFERENCIA;
                txtIdentificadorDeDireccion.Text = direccion.IDENTIFICADOR;

                //Activacion de botones
                btnEdiarDireccion.Enabled = true;
                btnEdiarDireccion.CssClass = "btn btn-sm btn-default";
                btnGuardarDireccion.Visible = false;
                btnCancelarDireccion.Visible = false;

                //txtDTelefono.Text = AD.SM.PHONE.NUMERO;
            }
        }
        protected void GVDireccion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GVDireccion, "Select$" + e.Row.RowIndex);
                LinkButton Eliminar = e.Row.FindControl("EliminaDireccion") as LinkButton;
                if (GVDireccion.Enabled)
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

                Session["MVDireccion"] = MVDireccion;

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
            MVTelefono.AgregaTelefonoALista( DDLDTipoDETelefono.SelectedItem.Value.ToString(), txtDTelefono.Text, DDLDTipoDETelefono.SelectedItem.Text.ToString());
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
            MVTelefono.ActualizaRegistroEnListaDeTelefonos(txtIdTelefono.Text, DDLDTipoDETelefono.SelectedItem.Value.ToString(),  txtDTelefono.Text);

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


            txtBaApellido.Text = txtFApellido.Text;

            //DDLBAPERFIL.Text = DDLFPERFILDEUSUARIO.Text;

            txtBANombre.Text = txtFNombreDeUsuario.Text;


            if (DDLFEstatus.SelectedItem.Value != "0")
            {
                DDLBAEstatus.SelectedIndex = DDLBAEstatus.Items.IndexOf(DDLBAEstatus.Items.FindByValue(DDLFEstatus.SelectedItem.Value.ToString()));
            }
            else
            {
                DDLBAEstatus.SelectedIndex = -1;
            }

        }
        protected void BusquedaNormal(object sender, EventArgs e)
        {
            PanelBusquedaAmpliada.Visible = false;
            PanelDerecho.Visible = true;
            PanelIzquierdo.Visible = true;
            PanelDatosDireccion.Visible = false;


            txtFApellido.Text = txtBaApellido.Text;

            //DDLFPERFILDEUSUARIO.Text = DDLBAPERFIL.Text;

            txtFNombreDeUsuario.Text = txtBANombre.Text;


            if (DDLBAEstatus.SelectedItem.Value != "0")
            {
                DDLFEstatus.SelectedIndex = DDLFEstatus.Items.IndexOf(DDLFEstatus.Items.FindByValue(DDLBAEstatus.SelectedItem.Value.ToString()));
            }
            else
            {
                DDLFEstatus.SelectedIndex = -1;
            }

        }
        protected void BorrarFiltrosBusquedaAvanzada(object sender, EventArgs e)
        {
            //Cajas de texto 
            txtBANombre.Text = string.Empty;
            txtBaApellido.Text = string.Empty;

            txtBACorreoElectronico.Text = string.Empty;
            txtBACalle.Text = string.Empty;
            txtBACOdigoPostal.Text = string.Empty;

            //Dropdown list
            DDLBAEstatus.SelectedIndex = -1;
            //DDLBAPERFIL.SelectedIndex = -1;
            DDLDBAPAIS.SelectedIndex = -1;

            MuestraEstados("00000000-0000-0000-0000-000000000000", "Filtro");
            MuestraMunicipio("00000000-0000-0000-0000-000000000000", "Filtro");
            MuestraCiudad("00000000-0000-0000-0000-000000000000", "Filtro");
            MuestraColonia("00000000-0000-0000-0000-000000000000", "Filtro");

        }


        #endregion

        #region Panel de busqueda de empresas

        protected void BuscaEmpresa(object sender, EventArgs e)
        {
            string NombreComercial = txtdNombreComercial.Text;
            string RazonSocial = txtDRazonSocial.Text;
            string RFc = txtdRFC.Text;

            if (NombreComercial == string.Empty && RazonSocial == string.Empty && RFc == string.Empty)
            {
                MVEmpresa.BuscarEmpresas();
                CargaGrid("Empresas");
            }
            else
            {
                MVEmpresa.BuscarEmpresas(RFC: RFc, NombreComercial: NombreComercial, RazonSocial: RazonSocial);
                CargaGrid("Empresas");
            }
            DGVBUSQUEDADEEMPRESA.Visible = true;
        }
        protected void DGVBUSQUEDADEEMPRESA_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVBUSQUEDADEEMPRESA, "Select$" + e.Row.RowIndex);

            }
        }
        protected void DGVBUSQUEDADEEMPRESA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = DGVBUSQUEDADEEMPRESA.SelectedDataKey.Value.ToString();
            var objeto = MVEmpresa.ObtenerEmpresaDeLista(valor);

            txtIdEmpresa.Text = objeto.UIDEMPRESA.ToString();
            txtdNombreComercial.Text = objeto.NOMBRECOMERCIAL;
            txtdRFC.Text = objeto.RFC;
            txtDRazonSocial.Text = objeto.RAZONSOCIAL;

            DGVBUSQUEDADEEMPRESA.Visible = false;
            btnCambiarEmpresa.Enabled = true;
            btnBuscarEmpresa.Enabled = false;
            txtdNombreComercial.Enabled = false;
            txtdRFC.Enabled = false;
            txtDRazonSocial.Enabled = false;

            btnBuscarEmpresa.CssClass = "btn btn-sm btn-default disabled";
            btnCambiarEmpresa.CssClass = "btn btn-sm btn-default";
        }

        protected void CambiarEmpresa(object sender, EventArgs e)
        {
            DGVBUSQUEDADEEMPRESA.Visible = false;

            btnCambiarEmpresa.Enabled = false;
            btnBuscarEmpresa.CssClass = "btn btn-sm btn-default ";
            btnCambiarEmpresa.CssClass = "btn btn-sm btn-default disabled";

            btnBuscarEmpresa.Enabled = true;
            txtdNombreComercial.Enabled = true;
            txtdRFC.Enabled = true;
            txtDRazonSocial.Enabled = true;
            //Visibilidad del gridview de para buscar empresas
            DGVBUSQUEDADEEMPRESA.Visible = false;
            //Limpiar cajas de textos de los filtros para la busqueda de empresa
            txtdRFC.Text = string.Empty;
            txtDRazonSocial.Text = string.Empty;
            txtdNombreComercial.Text = string.Empty;
        }

        #endregion

        protected void txtDUsuario_TextChanged(object sender, EventArgs e)
        {
            string usuario = txtDUsuario.Text;
            string uidusuario = txtUidUsuario.Text;
            txtDUsuario.BorderColor = Color.Empty;
            if (string.IsNullOrEmpty(uidusuario))
            {
                if (MVValidaciones.ValidaExistenciaDeUsuario(usuario))
                {

                    txtDUsuario.BorderColor = Color.Red;
                    PanelMensaje.Visible = true;
                    LblMensaje.Text = "El usuario ya existe!";
                }
            }
        }

        #region Panel de mensaje
        protected void BtnCerrarPanelMensaje_Click(object sender, EventArgs e)
        {
            PanelMensaje.Visible = false;
        }
        #endregion

        
    }
}