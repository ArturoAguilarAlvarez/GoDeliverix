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
    public partial class Repartidores : System.Web.UI.Page
    {
        #region Propiedades
        VMUsuarios MVUsuarios;
        VMSucursales MVSucursales;
        VMDireccion MVDireccion;
        VMEstatus MVEstatus;
        VMTelefono MVTelefono;
        VMCorreoElectronico MVCorreoElectronico;
        VMTurno MVTurno;
        string AccionesDeLaPagina = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Elimina las variables de sesion al entrar
                Session.Remove("Accion");

                MVUsuarios = new VMUsuarios();
                MVSucursales = new VMSucursales();
                MVDireccion = new VMDireccion();
                MVEstatus = new VMEstatus();
                MVTurno = new VMTurno();
                MVTelefono = new VMTelefono();
                MVCorreoElectronico = new VMCorreoElectronico();
                Session["MVUsuarios"] = MVUsuarios;
                Session["MVDireccion"] = MVDireccion;
                Session["MVUsuarios"] = MVUsuarios;
                Session["MVEstatus"] = MVEstatus;
                Session["MVTurno"] = MVTurno;
                Session["MVTelefono"] = MVTelefono;
                Session["MVSucursales"] = MVSucursales;
                Session["MVCorreoElectronico"] = MVCorreoElectronico;

                MVUsuarios.CargaPerfilesDeUsuario("DFC29662-0259-4F6F-90EA-B24E39BE4346");


                #region Panel derecho
                MVTelefono.TipoDeTelefonos();
                #region Paneles
                //Botones
                btnEditar.Enabled = false;
                //Visibilidad de paneles
                MuestraPanel("General");
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                btnGuardarTelefono.Visible = false;
                btnCancelarTelefono.Visible = false;
                btnEditarTelefono.Enabled = false;
                btnEdiarDireccion.Enabled = false;
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


                DDLDTipoDETelefono.DataSource = MVTelefono.ListaDeTipoDeTelefono;
                DDLDTipoDETelefono.DataValueField = "UidTipo";
                DDLDTipoDETelefono.DataTextField = "StrNombreTipoDeTelefono";
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
                #region Mensaje del sistema
                PanelMensaje.Visible = false;
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
                MVEstatus.OBTENERLISTA();
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
                #region GridView empresa simple 
                //Ejecuta el metodo que obtiene todos los usuarios de la base de datos y llama al metodo para cargar su respectivo gridvie pasando por parametro del grid que qureemos que se cargue.
                MVUsuarios.BusquedaDeUsuario(UIDPERFIL: new Guid("DFC29662-0259-4F6F-90EA-B24E39BE4346"), UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()));
                CargaGrid("Normal");
                #endregion
                #endregion

                #region Panel de direccion

                PanelDatosDireccion.Visible = false;
                //BotonMuestraAddCity.Visible = false;
                //PanelAddCity.Visible = false;
                GVDireccion.DataSource = null;
                GVDireccion.DataBind();
                txtIdentificadorDeDireccion.Attributes.Add("placeholder", "Identificador");
                //txtNombreCiudadOColonia.Attributes.Add("placeholder", "Ciudad o Colonia");
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


                txtdidentificador.Attributes.Add("placeholder", "Identificador");


                txtdidentificador.Text = string.Empty;
                txtdHoraApertura.Text = string.Empty;
                txtdHoraDeCierre.Text = string.Empty;
                #endregion
            }
            else
            {

                MVSucursales = (VMSucursales)Session["MVSucursales"];
                MVCorreoElectronico = (VMCorreoElectronico)Session["MVCorreoElectronico"];
                MVUsuarios = (VMUsuarios)Session["MVUsuarios"]; MVUsuarios = (VMUsuarios)Session["MVUsuarios"];
                MVDireccion = (VMDireccion)Session["MVDireccion"];
                MVTurno = (VMTurno)Session["MVTurno"];
                MVEstatus = (VMEstatus)Session["MVEstatus"];
                MVUsuarios = (VMUsuarios)Session["MVUsuarios"];
                MVTelefono = (VMTelefono)Session["MVTelefono"];
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

                //Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                //Label PERFIL = e.Row.FindControl("lblPerfil") as Label;
                //if (e.Row.Cells[5].Text == "ACTIVO")
                //{
                //    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                //    ESTATUS.ToolTip = "ACTIVO";
                //}
                //if (e.Row.Cells[5].Text == "INACTIVO")
                //{
                //    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                //    ESTATUS.ToolTip = "INACTIVO";
                //}
                //if (e.Row.Cells[7].Text == "SUPERVISOR")
                //{
                //    PERFIL.CssClass = "glyphicon glyphicon-queen";
                //    PERFIL.ToolTip = "ADMINISTRADOR";
                //}

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

                //Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                //Label PERFIL = e.Row.FindControl("lblPerfil") as Label;
                //if (e.Row.Cells[5].Text == "ACTIVO")
                //{
                //    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                //    ESTATUS.ToolTip = "ACTIVO";
                //}
                //if (e.Row.Cells[5].Text == "INACTIVO")
                //{
                //    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                //    ESTATUS.ToolTip = "INACTIVO";
                //}
                //if (e.Row.Cells[7].Text == "SUPERVISOR")
                //{
                //    PERFIL.CssClass = "glyphicon glyphicon-queen";
                //    PERFIL.ToolTip = "ADMINISTRADOR";
                //}
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
        /// Muestra el grid que se solicite por la busqueda
        /// </summary>
        /// <param name="Busqueda">Normal,Ampliada,Direccion,Telefono,Empresas</param>
        protected void CargaGrid(string Busqueda)
        {
            Session["MVUsuarios"] = MVUsuarios;
            switch (Busqueda)
            {
                case "Normal":
                    //Obtiene el nombre de la sucursal
                    foreach (var item in MVUsuarios.LISTADEUSUARIOS)
                    {
                        item.StrNombreDeSucursal = MVSucursales.obtenerSucursalRepartidor(item.Uid);
                    }
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
                    DGVTELEFONOS.DataSource = MVTelefono.ListaDeTelefonos;
                    DGVTELEFONOS.DataBind();
                    break;
                case "Empresas":
                    DGVBUSQUEDADEEMPRESA.DataSource = MVSucursales.LISTADESUCURSALES;
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
            // MVUsuarios.ObtenerTelefonos(valor);
            MVDireccion.ObtenerDireccionesUsuario(valor);
            MVTelefono.BuscarTelefonos(UidPropietario: new Guid(valor), ParadetroDeBusqueda: "Usuario");
            
            txtMontoMaximoAPortar.Text = MVTurno.ObtenerMontoAPortar(valor); 
            txtUidUsuario.Text = MVUsuarios.Uid.ToString();

            txtDNombre.Text = MVUsuarios.StrNombre;
            txtDApellidoPaterno.Text = MVUsuarios.StrApellidoPaterno;
            txtDApellidoMaterno.Text = MVUsuarios.StrApellidoMaterno;
            txtDUsuario.Text = MVUsuarios.StrUsuario;
            txtdContrasena.Text = MVUsuarios.StrCotrasena;
            DateTime Fecha = DateTime.Parse(MVUsuarios.DtmFechaDeNacimiento);
            txtDFechaDeNacimiento.Text = Fecha.ToString("yyyy-MM-dd");


            PanelMensaje.Visible = false;

            CargaGrid("Direccion");
            CargaGrid("Telefono");
            //Campos de la sucursal asociada
            MVSucursales.ObtenerSucursal(MVSucursales.obtenerUidSucursalRepartidor(valor));

            txtUidSucursal.Text = MVSucursales.SUCURSAL.ID.ToString();
            txtdidentificador.Text = MVSucursales.SUCURSAL.IDENTIFICADOR;
            txtdHoraApertura.Text = MVSucursales.SUCURSAL.HORAAPARTURA;
            txtdHoraDeCierre.Text = MVSucursales.SUCURSAL.HORACIERRE;

        }
        private void Sort(string sortExpression, string Valor, string GridView)
        {
            Session["MVUsuarios"] = MVUsuarios;
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
            Guid UidPerfil = new Guid("DFC29662-0259-4F6F-90EA-B24E39BE4346");
            Guid UidEmpresa = new Guid(Session["UidEmpresaSistema"].ToString());
            if (txtFApellido.Text == string.Empty && txtFNombreDeUsuario.Text == string.Empty && DDLFEstatus.SelectedItem.Value == "0" && txtFUsuario.Text == string.Empty)
            {
                MVUsuarios.BusquedaDeUsuario(UIDPERFIL: UidPerfil, UidEmpresa: UidEmpresa);

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
                MVUsuarios.BusquedaDeUsuario(NOMBRE: Nombre, USER: Usuario, APELLIDO: Apellido, ESTATUS: Estatus, UIDPERFIL: UidPerfil, UidEmpresa: UidEmpresa);
                //Obtiene el nombre de la sucursal
                foreach (var item in MVUsuarios.LISTADEUSUARIOS)
                {
                    item.StrNombreDeSucursal = MVSucursales.obtenerSucursalRepartidor(item.Uid);
                }
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
            Guid UidPerfil = new Guid("DFC29662-0259-4F6F-90EA-B24E39BE4346");

            if (Apellido == string.Empty && CodigoPostal == string.Empty && Nombre == string.Empty && Usuario == string.Empty && Estatus == "0" && CorreoElectronico == string.Empty && Calle == string.Empty && Pais == new Guid("00000000-0000-0000-0000-000000000000") && Estado == new Guid("00000000-0000-0000-0000-000000000000") && Municipio == new Guid("00000000-0000-0000-0000-000000000000") && Ciudad == new Guid("00000000-0000-0000-0000-000000000000") && Colonia == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                MVUsuarios.BusquedaDeUsuario(UIDPERFIL: UidPerfil);
                //cargaGrid("Ampliada");
                CargaGrid("Normal");
                lblBAFiltrosVisibilidad.Text = " Mostrar";
                PanelFiltrosBusquedaAmpliada.Visible = false;
                BtnBABuscar.Enabled = false;
                BtnBALimpiar.Enabled = false;
                BtnBABuscar.CssClass = "btn btn-sm btn-default disabled";
                BtnBALimpiar.CssClass = "btn btn-sm btn-default disabled";
            }
            else
            {
                MVUsuarios.BusquedaDeUsuario(NOMBRE: Nombre, USER: Usuario, APELLIDO: Apellido, ESTATUS: Estatus, UIDPERFIL: UidPerfil);
                //cargaGrid("Ampliada");
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
            Session["Paneles"] = "Activados";
            AccionesDeLaPagina = "NuevoRegistro";
            Session["Accion"] = AccionesDeLaPagina;
            txtUidUsuario.Text = string.Empty;
            //Cambia el texto del boton para guardar
            lblGuardarDatos.CssClass = "glyphicon glyphicon-ok";
            //Limpia las cajas de texto del panel de gestion de datos
            LimpiarCajasDeTexto();
            //Activa las cajas de texto del panel de gestion de datos
            TextboxActivados(ControlDeACcion: "Activado");
            //Manda valores por defecto a los dropdown list del panel de gestion de datos
            DDLDEstatus.SelectedIndex = 0;
            //Deshabilita boton de edicion
            btnEditar.CssClass = "btn btn-sm btn-default disabled";
            btnEditar.Enabled = false;


            MVDireccion.ListaDIRECCIONES.Clear();
            MVTelefono.ListaDeTelefonos.Clear();
            DGVEMPRESAS.SelectedIndex = -1;
            MVUsuarios = new VMUsuarios();
            Session["MVUsuarios"] = MVUsuarios;
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
                Session["MVUsuarios"] = MVUsuarios;
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
                DesactivarCajasDeTextoPanelGestion();
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
            txtdidentificador.Text = string.Empty;
            txtdHoraApertura.Text = string.Empty;
            txtdHoraDeCierre.Text = string.Empty;

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
        protected void ActivarCajasDeTextoGestor()
        {
            EstatusControlesPanelGestion(true);

        }
        private void TextboxActivados(string ControlDeACcion = "")
        {
            if (AccionesDeLaPagina == "Edicion" && ControlDeACcion == "Desactivado")
            {
                ActivarCajasDeTextoGestor();
                PanelMensaje.Visible = false;
                btnNuevo.Enabled = false;
                btnNuevo.CssClass = "btn btn-sm btn-default disabled";
                lblGuardarDatos.CssClass = "glyphicon glyphicon-refresh";
                //Visibilidad de boton guardar y cancelar
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
            }
            else
            if (AccionesDeLaPagina == "NuevoRegistro" && ControlDeACcion == "Activado")
            {
                ActivarCajasDeTextoGestor();
                PanelMensaje.Visible = false;
                LimpiarCajasDeTexto();
                btnNuevo.Enabled = true;
                btnNuevo.CssClass = "btn btn-sm btn-default ";
                //Visibilidad de boton guardar y cancelar
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
            }
            else
            if (!string.IsNullOrEmpty(txtUidUsuario.Text))
            {
                DesactivarCajasDeTextoPanelGestion();
                btnNuevo.Enabled = true;
                btnNuevo.CssClass = "btn btn-sm btn-default ";
            }
            else if (ControlDeACcion == "")
            {
                DesactivarCajasDeTextoPanelGestion();
                btnNuevo.Enabled = true;
                LimpiarCajasDeTexto();
                btnNuevo.CssClass = "btn btn-sm btn-default ";
            }
        }

        protected void EstatusControlesPanelGestion(bool estatus)
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
            txtMontoMaximoAPortar.Enabled = estatus;
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
            txtdidentificador.Enabled = estatus;
            txtdHoraApertura.Enabled = estatus;
            txtdHoraDeCierre.Enabled = estatus;
            //Cambia el estatus de los gridview del panel
            GVDireccion.Enabled = estatus;
            DGVTELEFONOS.Enabled = estatus;
            //Carga los gridview del panel derecho
            CargaGrid("Telefono");
            CargaGrid("Direccion");
        }
        private void DesactivarCajasDeTextoPanelGestion()
        {
            EstatusControlesPanelGestion(false);
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

            txtdidentificador.Text = string.Empty;
            txtdHoraApertura.Text = string.Empty;
            txtdHoraDeCierre.Text = string.Empty;

            DDLDTipoDETelefono.SelectedIndex = -1;
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



        #endregion

        #region Dropdownlist
        //Metodos par mostrar informacion en los dropdownlist
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

                //BotonMuestraAddCity.Visible = true;
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

        #endregion Guardar datos
        protected void GuardarDatos(object sender, EventArgs e)
        {

            QuitaEstiloACamposObligatorios();
            if (txtDNombre.Text != string.Empty && txtDApellidoPaterno.Text != string.Empty && txtDApellidoMaterno.Text != string.Empty && DDLDEstatus.SelectedItem.Value != "0" && txtdidentificador.Text != string.Empty)
            {
                #region Variables
                //Datos generales
                string TIPODEUSUARIO = "Repartidor";
                Guid UidSucursal = new Guid(txtUidSucursal.Text);
                string Nombre = txtDNombre.Text;
                string ApellidoPaterno = txtDApellidoPaterno.Text;
                string ApellidoMaterno = txtDApellidoMaterno.Text;
                string usuario = txtDUsuario.Text;
                string password = txtdContrasena.Text;
                int Estatus = Convert.ToInt32(DDLDEstatus.SelectedItem.Value);
                string fechaDeNacimiento = txtDFechaDeNacimiento.Text;
                string PerfilDeUsuario = "DFC29662-0259-4F6F-90EA-B24E39BE4346";
                string estatus = DDLDEstatus.SelectedItem.Value.ToString();
                Guid UidEmpresa = new Guid(Session["UidEmpresaSistema"].ToString());

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
                AccionesDeLaPagina = Session["Accion"].ToString();
                #endregion
                if (AccionesDeLaPagina == "NuevoRegistro")
                {
                    if (MVUsuarios.ValidarCorreoElectronicoDelUsuario(Correo) != true)
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "No se puede agregar un correo electronico ya existente en el sistema";
                    }
                    MVUsuarios.BusquedaDeUsuario(USER: usuario);
                    if (MVUsuarios.LISTADEUSUARIOS.Count > 0)
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "No se puede agregar un nombre de usuario ya existente en el sistema ";
                    }
                    //else if (MVUsuarios.validaCorreoElectronicoDeLaEmpresa(Correo) != true)
                    //{
                    //    lblEstado.Text = "El correo electronico ya esta asociado a un usuario";
                    //}
                    else
                    {

                        #region Guardar datos
                        Guid UidUsuario = Guid.NewGuid();
                        if (MVUsuarios.GuardaUsuario(UidUsuario: UidUsuario, Nombre: Nombre, ApellidoPaterno: ApellidoPaterno, ApellidoMaterno: ApellidoMaterno, usuario: usuario, password: password, fnacimiento: fechaDeNacimiento, perfil: PerfilDeUsuario, estatus: estatus, TIPODEUSUARIO: TIPODEUSUARIO, IdEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()), Sucursal: UidSucursal))
                        {
                            MVUsuarios.Uid = UidUsuario;
                            MVUsuarios.RelacionRepartidorSucursal(UidSucursal);

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

                            MVTurno.AgregarInformacionRepartidor(UidUsuario, txtMontoMaximoAPortar.Text);
                            PanelMensaje.Visible = true;
                            LblMensaje.Text = "Registro agregado!";
                        }
                        else
                        {
                            PanelMensaje.Visible = true;
                            LblMensaje.Text = "Ocurrio un error al agregar";
                        }

                        btnGuardar.Visible = false;
                        btnCancelar.Visible = false;
                        pnlDireccion.Visible = false;
                        PanelDeBusqueda.Visible = true;
                        BorrarCamposDeGestion();
                        Session.Remove("Accion");
                        MVUsuarios = new VMUsuarios();

                        MVUsuarios.BusquedaDeUsuario(UIDPERFIL: new Guid("DFC29662-0259-4F6F-90EA-B24E39BE4346"), UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()));
                        CargaGrid("Normal");
                        Session["MVUsuarios"] = MVUsuarios;
                        AccionesDeLaPagina = string.Empty;
                        TextboxActivados();

                        #endregion
                    }
                }
                else
                if (AccionesDeLaPagina == "Edicion")
                {
                    #region Actualizar datos
                    Guid UidUsuario = new Guid(txtUidUsuario.Text);
                    if (MVUsuarios.ActualizarUsuario(UidUsuario, Nombre, ApellidoPaterno, ApellidoMaterno, usuario, password, fechaDeNacimiento, PerfilDeUsuario, estatus, new Guid(Session["UidEmpresaSistema"].ToString()), UidSucursal))
                    {
                        MVUsuarios.EliminarRelacionSucursal(UidUsuario);
                        MVUsuarios.Uid = UidUsuario;
                        MVUsuarios.RelacionRepartidorSucursal(UidSucursal);

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
                        MVTurno.AgregarInformacionRepartidor(UidUsuario, txtMontoMaximoAPortar.Text);
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Registro actualizado!";
                    }
                    else
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Registro no actualizado!";
                    }
                    Session.Remove("Edicion");
                    Session.Remove("Accion");
                    MVUsuarios.BusquedaDeUsuario(UIDPERFIL: new Guid("DFC29662-0259-4F6F-90EA-B24E39BE4346"), UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()));
                    CargaGrid("Normal");
                    Session["MVUsuarios"] = MVUsuarios;
                    AccionesDeLaPagina = string.Empty;
                    TextboxActivados();
                    DGVEMPRESAS.SelectedIndex = -1;
                    #endregion
                }
            }
            else
            {
                PanelMensaje.Visible = true;
                LblMensaje.Text = "Favor de llenar los datos requeridos";
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
                if (txtdidentificador.Text == string.Empty || txtdHoraDeCierre.Text == string.Empty || txtdHoraApertura.Text == string.Empty)
                {
                    txtdidentificador.BorderColor = System.Drawing.Color.Red;
                    txtdHoraDeCierre.BorderColor = System.Drawing.Color.Red;
                    txtdHoraApertura.BorderColor = System.Drawing.Color.Red;
                }
            }

        }
        protected void QuitaEstiloACamposObligatorios()
        {
            txtDNombre.BorderColor = System.Drawing.Color.White;
            txtDApellidoMaterno.BorderColor = System.Drawing.Color.White;
            txtDApellidoPaterno.BorderColor = System.Drawing.Color.White;
            txtDUsuario.BorderColor = System.Drawing.Color.White;
            txtdContrasena.BorderColor = System.Drawing.Color.White;
            txtdidentificador.BorderColor = System.Drawing.Color.White;
            txtdHoraApertura.BorderColor = System.Drawing.Color.White;
            txtdHoraDeCierre.BorderColor = System.Drawing.Color.White;
        }
        #endregion

        #region Panel de direccion

        protected void NuevaDireccion(object sender, EventArgs e)
        {
            txtIdDireccion.Text = string.Empty;
            PanelDeBusqueda.Visible = false;
            PanelDatosDireccion.Visible = true;
            ActivarCamposDeDireccion();
            LimpiarCamposDeDireccion();
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
            btnGuardarDireccion.Visible = false;
            btnCancelarDireccion.Visible = false;
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
            btnGuardarDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;
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
            if (txtIdDireccion.Text != string.Empty)
            {
                MVDireccion.ActualizaListaDireccion(txtIdDireccion.Text, UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, txtCalle0.Text, txtCalle1.Text, txtCalle2.Text, txtDManzana.Text, txtDLote.Text, txtDCodigoPostal.Text, txtDReferencia.Text, txtIdentificadorDeDireccion.Text, NOMBRECIUDAD, NOMBRECOLONIA);
            }
            else
            {
                Guid UidDireccion = Guid.NewGuid();
                MVDireccion.AgregaDireccionALista(UidDireccion, UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, txtCalle0.Text, txtCalle1.Text, txtCalle2.Text, txtDManzana.Text, txtDLote.Text, txtDCodigoPostal.Text, txtDReferencia.Text, txtIdentificadorDeDireccion.Text, NOMBRECIUDAD, NOMBRECOLONIA);
            }

            Session["MVUsuarios"] = MVUsuarios;
            GVDireccion.DataSource = MVDireccion.ListaDIRECCIONES;
            GVDireccion.DataBind();
            PanelDatosDireccion.Visible = false;
            PanelDeBusqueda.Visible = true;
            LimpiarCamposDeDireccion();
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
        protected void MuestraCamposParaAgregarColoniaCiudad(object sender, EventArgs e)
        {
            //if (!PanelAddCity.Visible)
            //{
            //    PanelAddCity.Visible = true;
            //    BotonMuestraAddCity.Visible = false;
            //}
        }
        protected void OcultaCamposParaAgregarColoniaCiudad(object sender, EventArgs e)
        {
            //if (PanelAddCity.Visible)
            //{
            //    PanelAddCity.Visible = false;
            //    BotonMuestraAddCity.Visible = true;
            //}
        }
        protected void AgregarCiudadOColonia(object sender, EventArgs e)
        {
            //string Valor = DDLCiudadColonia.SelectedItem.Text;
            //string Pais = DDLDPais.SelectedItem.Value.ToString();
            //string Estado = DDLDEstado.SelectedItem.Value.ToString();
            //string Municipio = DDLDMunicipio.SelectedItem.Value.ToString();
            //string Ciudad = DDLDCiudad.SelectedItem.Value.ToString();
            //string Nombre = txtNombreCiudadOColonia.Text;

            ////MVUsuarios.AgregaCiudadOColoniaa(Pais, Estado, Municipio, Ciudad, Nombre, Valor);

            //DDLCiudadColonia.SelectedIndex = 0;
            //txtNombreCiudadOColonia.Text = string.Empty;
            //PanelAddCity.Visible = false;
            //BotonMuestraAddCity.Visible = true;
        }
        protected void CierraVentanaDireccion(object sender, EventArgs e)
        {
            PanelDatosDireccion.Visible = false;
            PanelDeBusqueda.Visible = true;
            btnEdiarDireccion.Enabled = false;
            btnEdiarDireccion.CssClass = "btn btn-sm btn-default disabled";
        }

        protected void GVDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }

            if (AccionesDeLaPagina != string.Empty)
            {
                int index = GVDireccion.SelectedRow.RowIndex;
                string valor = GVDireccion.DataKeys[index].Value.ToString();
                DesactivarCamposDeDireccion();
                Session["MVUsuarios"] = MVUsuarios;
                var direccion = MVDireccion.ObtenDireccion(valor);
                PanelDatosDireccion.Visible = true;
                PanelDeBusqueda.Visible = false;
                //Esconde el Id en un textbox
                txtIdDireccion.Text = direccion.ID.ToString();
                DDLDPais.SelectedIndex = DDLDPais.Items.IndexOf(DDLDPais.Items.FindByValue(direccion.PAIS.ToString()));
                //Obtener lista de estado y seleccionar el del objeto
                MuestraEstados(direccion.PAIS.ToString(), "Gestion");
                DDLDEstado.SelectedIndex = DDLDEstado.Items.IndexOf(DDLDEstado.Items.FindByValue(direccion.ESTADO.ToString()));

                // Obtener lista de municipio y seleccionar el del objeto
                MuestraMunicipio(direccion.ESTADO.ToString(), "Gestion");
                DDLDMunicipio.SelectedIndex = DDLDMunicipio.Items.IndexOf(DDLDMunicipio.Items.FindByValue(direccion.MUNICIPIO.ToString()));

                // Obtener lista de ciudades y seleccionar el del objeto
                MuestraCiudad(direccion.MUNICIPIO.ToString(), "Gestion");
                DDLDCiudad.SelectedIndex = DDLDCiudad.Items.IndexOf(DDLDCiudad.Items.FindByValue(direccion.CIUDAD.ToString()));

                // Obtener lista de colonias y seleccionar el del objeto
                MuestraColonia(direccion.CIUDAD.ToString(), "Gestion");
                DDLDColonia.SelectedIndex = DDLDColonia.Items.IndexOf(DDLDColonia.Items.FindByValue(direccion.COLONIA.ToString()));

                txtCalle0.Text = direccion.CALLE0;
                txtCalle1.Text = direccion.CALLE1;
                txtCalle2.Text = direccion.CALLE2;
                txtDManzana.Text = direccion.MANZANA;
                txtDLote.Text = direccion.LOTE;
                txtDCodigoPostal.Text = direccion.CodigoPostal;
                txtDReferencia.Text = direccion.REFERENCIA;

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
                var icono = e.Row.FindControl("lblEliminarTelefono") as Label;

                LinkButton Eliminar = e.Row.FindControl("EliminaDireccion") as LinkButton;

                icono.CssClass = "glyphicon glyphicon-trash";
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


                if (Session["Accion"] != null)
                {
                    AccionesDeLaPagina = Session["Accion"].ToString();
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


                Session["MVUsuarios"] = MVUsuarios;

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
                if (Session["Accion"] != null)
                {
                    AccionesDeLaPagina = Session["Accion"].ToString();
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

        #region Panel de busqueda de Sucursales

        protected void BuscaEmpresa(object sender, EventArgs e)
        {
            string Identificador = txtdidentificador.Text;
            string HA = txtdHoraApertura.Text;
            string HC = txtdHoraDeCierre.Text;

            if (Identificador == string.Empty && HA == string.Empty && HC == string.Empty)
            {
                MVSucursales.BuscarSucursales(Uidempresa: Session["UidEmpresaSistema"].ToString());
                CargaGrid("Empresas");
            }
            else
            {
                MVSucursales.BuscarSucursales(identificador: Identificador, horaApertura: HA, horaCierre: HC, Uidempresa: Session["UidEmpresaSistema"].ToString());
                DGVBUSQUEDADEEMPRESA.DataSource = MVSucursales.LISTADESUCURSALES;
                DGVBUSQUEDADEEMPRESA.DataBind();
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
            MVSucursales.BuscarSucursales(UidSucursal: valor, Uidempresa: Session["UidEmpresaSistema"].ToString());

            txtUidSucursal.Text = MVSucursales.ID.ToString();
            txtdidentificador.Text = MVSucursales.IDENTIFICADOR;
            txtdHoraApertura.Text = MVSucursales.HORAAPARTURA;
            txtdHoraDeCierre.Text = MVSucursales.HORACIERRE;

            DGVBUSQUEDADEEMPRESA.Visible = false;
            btnCambiarEmpresa.Enabled = true;
            btnBuscarEmpresa.Enabled = false;
            txtdidentificador.Enabled = false;
            txtdHoraApertura.Enabled = false;
            txtdHoraDeCierre.Enabled = false;

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
            txtdidentificador.Enabled = true;
            txtdHoraDeCierre.Enabled = true;
            txtdHoraApertura.Enabled = true;
            //Visibilidad del gridview de para buscar empresas
            DGVBUSQUEDADEEMPRESA.Visible = false;
            //Limpiar cajas de textos de los filtros para la busqueda de empresa
            txtdidentificador.Text = string.Empty;
            txtdHoraDeCierre.Text = string.Empty;
            txtdHoraApertura.Text = string.Empty;
        }
        #endregion

        #region Panel de mensaje
        protected void BtnCerrarPanelMensaje_Click(object sender, EventArgs e)
        {
            PanelMensaje.Visible = false;
        }
        #endregion


        #region Control de paneles
        protected void PanelDireccion(object sender, EventArgs e)
        {
            MuestraPanel("Direccion");

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
            MuestraPanel("Contacto");

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
        protected void PanelDeDatosDeEmpresa(object sender, EventArgs e)
        {
            MuestraPanel("Sucursal");

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
        protected void BtnDatosDePago_Click(object sender, EventArgs e)
        {
            MuestraPanel("Trabajo");
            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            TextboxActivados(ControlDeACcion: "Desactivado");
        }
        protected void PanelGeneral(object sender, EventArgs e)
        {
            MuestraPanel("General");

            if (Session["Accion"] != null)
            {
                AccionesDeLaPagina = Session["Accion"].ToString();
            }
            TextboxActivados(ControlDeACcion: "Desactivado");
        }
        protected void MuestraPanel(string strPanel)
        {
            pnlDatosGenerales.Visible = false;
            pnlDireccion.Visible = false;
            pnlContacto.Visible = false;
            PanelDatosDireccion.Visible = false;
            panelDatosEmpresa.Visible = false;
            PanelInformacionDeTrabajo.Visible = false;
            //Estilos para seleccion
            liDatosGenerales.Attributes.Add("class", "");
            liDatosDireccion.Attributes.Add("class", "");
            liDatosContacto.Attributes.Add("class", "");
            liDatosDeEmpresa.Attributes.Add("class", "");
            liDatosDePago.Attributes.Add("class", "");
            switch (strPanel)
            {
                case "General": pnlDatosGenerales.Visible = true; liDatosGenerales.Attributes.Add("class", "active"); break;
                case "Direccion": pnlDireccion.Visible = true; PanelDatosDireccion.Visible = true; liDatosDireccion.Attributes.Add("class", "active"); break;
                case "Contacto": pnlContacto.Visible = true; liDatosContacto.Attributes.Add("class", "active"); break;
                case "Sucursal": panelDatosEmpresa.Visible = true; liDatosDeEmpresa.Attributes.Add("class", "active"); break;
                case "Trabajo": PanelInformacionDeTrabajo.Visible = true; liDatosDePago.Attributes.Add("class", "active"); break;
                default:
                    break;
            }
        }
        #endregion
        protected void txtDUsuario_TextChanged(object sender, EventArgs e)
        {
            MVUsuarios.BusquedaDeUsuario(USER: txtDUsuario.Text);
            if (MVUsuarios.LISTADEUSUARIOS.Count > 0)
            {
                txtDUsuario.BorderColor = Color.Red;
                PanelMensaje.Visible = true;
                LblMensaje.Text = "El usuario ya existe";
            }
            else
            {
                PanelMensaje.Visible = false;
                txtDUsuario.BorderColor = Color.Empty;
            }
        }
    }
}