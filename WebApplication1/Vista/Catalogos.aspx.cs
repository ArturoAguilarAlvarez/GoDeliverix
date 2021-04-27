using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using VistaDelModelo;
using System.Drawing;
using System.Web.Services;
using System.Drawing.Imaging;

namespace WebApplication1.Vista
{
    public partial class Catalogos : System.Web.UI.Page
    {
        string Acciones = string.Empty;
        VMImagen MVImagen = new VMImagen();
        VMCategoria MVCategoria = new VMCategoria();
        VMSubCategoria MVSubCategoria = new VMSubCategoria();
        VMGiro MVGiro = new VMGiro();
        VMEstatus MVEstatus = new VMEstatus();
        ImagenHelper oImagenHelper = new ImagenHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            FUGiro.Attributes["onchange"] = "UploadFileGiro(this)";
            if (!IsPostBack)
            {
                Session["MVCategoria"] = MVCategoria;
                Session["MVSubCategoria"] = MVSubCategoria;
                Session["MVImagen"] = MVImagen;
                Session["MVGiro"] = MVGiro;
                Session["MVEstatus"] = MVEstatus;
                MVEstatus.OBTENERLISTA();
                Acciones = string.Empty;
                #region Panel izquierdo
                //Deshabilita controles
                BtnBLimpiar.Enabled = false;
                BtnBBuscar.Enabled = false;
                //Coloca estilos Css
                BtnBLimpiar.CssClass = "btn btn-sm btn-default disabled";
                BtnBBuscar.CssClass = "btn btn-sm btn-default disabled";
                //Agrega placeholder
                txtFiltroNombre.Attributes.Add("placeholder", "Nombre");
                //Incializa en vacio el grid View de Giro
                DGVGiro.DataSource = null;
                DGVGiro.DataBind();
                //Inicializa el grid en vacio
                DGVCategorias.DataSource = null;
                DGVCategorias.DataBind();
                //Incializa en vacio el grid View de subcategoias
                DGVSubcategorias.DataSource = null;
                DGVSubcategorias.DataBind();
                //Filtro de estatus
                ddlFiltroEstatus.DataSource = MVEstatus.ListaEstatus;
                ddlFiltroEstatus.DataValueField = "ID";
                ddlFiltroEstatus.DataTextField = "NOMBRE";
                ddlFiltroEstatus.DataBind();

                //Texto al boton de visibilidad del panel
                lblBAFiltrosVisibilidad.Text = "Mostrar";
                //Visibilidad de panel de filtros 
                PnlFiltrosCategoria.Visible = false;
                //Visibilidad del panel
                MuestraPanelGridView("Giro");
                #endregion
                #region Panel derecho

                //Estatus de Giro
                DDLEstatusGIro.DataSource = MVEstatus.ObtenerListaActiva();
                DDLEstatusGIro.DataValueField = "IdEstatus";
                DDLEstatusGIro.DataTextField = "NOMBRE";
                DDLEstatusGIro.DataBind();


                //Visibilidad de controles
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;

                //Deshabilita cajas de texto
                DesactivaControlesGiro();

                //Placeholder de cajas de texto
                txtNombreGiro.Attributes.Add("placeholder", "Nombre");
                txtDescripcionGiro.Attributes.Add("placeholder", "Descripcion");


                ImgGiro.ImageUrl = "Img/Categoria/Default.jpg";

                #region Giro
                DesactivaControlesGiro();
                #endregion
                #endregion
            }
            else
            {
                MVCategoria = (VMCategoria)Session["MVCategoria"];
                MVSubCategoria = (VMSubCategoria)Session["MVSubCategoria"];
                MVImagen = (VMImagen)Session["MVImagen"];
                MVGiro = (VMGiro)Session["MVGiro"];
                MVEstatus = (VMEstatus)Session["MVEstatus"];
            }
        }

        #region Panel izquierdo

        protected void BtnBALimpiar_Click(object sender, EventArgs e)
        {
            txtFiltroNombre.Text = string.Empty;
        }

        protected void BtnBOcultar_Click(object sender, EventArgs e)
        {
            string Visibilidad = lblBAFiltrosVisibilidad.Text;
            if (Visibilidad == "Mostrar")
            {
                BtnBBuscar.Enabled = true;
                BtnBLimpiar.Enabled = true;

                BtnBBuscar.CssClass = "btn btn-sm btn-default";
                BtnBLimpiar.CssClass = "btn btn-sm btn-default";

                PnlFiltrosCategoria.Visible = true;

                lblBAFiltrosVisibilidad.Text = "Ocultar";
            }
            if (Visibilidad == "Ocultar")
            {
                BtnBBuscar.Enabled = false;
                BtnBLimpiar.Enabled = false;

                BtnBBuscar.CssClass = "btn btn-sm btn-default disabled";
                BtnBLimpiar.CssClass = "btn btn-sm btn-default disabled";

                PnlFiltrosCategoria.Visible = false;

                lblBAFiltrosVisibilidad.Text = "Mostrar";
            }
        }
        protected void BtnBBuscar_Click(object sender, EventArgs e)
        {
            txtUidGiro.Text = string.Empty;
            txtIdCategoria.Text = string.Empty;
            txtUidSubCategoria.Text = string.Empty;
            lblEstado.Text = string.Empty;

            if (PanelGridGiro.Visible == true)
            {

                if (txtFiltroNombre.Text == "" && txtFiltroNombre.Text == string.Empty && ddlFiltroEstatus.SelectedValue.ToString() == "-1")
                {
                    MVGiro.BuscarGiro();
                }
                else if (txtFiltroNombre.Text != "" || txtFiltroNombre.Text != string.Empty || ddlFiltroEstatus.SelectedValue.ToString() != "-1")
                {
                    MVGiro.BuscarGiro(Nombre: txtFiltroNombre.Text, Estatus: ddlFiltroEstatus.SelectedValue.ToString());
                }
                CargaGrid("Giro");
                DGVGiro.SelectedIndex = -1;
            }
            if (PanelGridCategoria.Visible == true)
            {
                if (txtUidGiro.Text == "" && txtUidGiro.Text == string.Empty)
                {
                    lblEstado.Text = "Seleccione un giro para mostrar categorias";
                }
                else
                if (txtFiltroNombre.Text == "" && txtFiltroNombre.Text == string.Empty && ddlFiltroEstatus.SelectedValue.ToString() == "-1")
                {
                    MVCategoria.BuscarCategorias(UidGiro: txtUidGiro.Text);
                }
                else if (txtFiltroNombre.Text != "" || txtFiltroNombre.Text != string.Empty || ddlFiltroEstatus.SelectedValue.ToString() != "-1")
                {
                    MVCategoria.BuscarCategorias(UidGiro: txtUidGiro.Text, Nombre: txtFiltroNombre.Text, Estatus: ddlFiltroEstatus.SelectedValue.ToString());
                }
                CargaGrid("Categoria");
                DGVCategorias.SelectedIndex = -1;
            }
            if (PanelGridSubcategoria.Visible == true)
            {
                if (txtIdCategoria.Text == "" && txtIdCategoria.Text == string.Empty)
                {
                    lblEstado.Text = "Seleccione una categoria para mostrar subcategorias";
                }
                else
                if (txtFiltroNombre.Text == "" && txtFiltroNombre.Text == string.Empty && ddlFiltroEstatus.SelectedValue.ToString() == "-1")
                {
                    MVSubCategoria.BuscarSubCategoria(UidCategoria: txtIdCategoria.Text);
                }
                else if (txtFiltroNombre.Text != "" || txtFiltroNombre.Text != string.Empty || ddlFiltroEstatus.SelectedValue.ToString() != "-1")
                {
                    MVSubCategoria.BuscarSubCategoria(UidCategoria: txtIdCategoria.Text, Nombre: txtFiltroNombre.Text, estatus: ddlFiltroEstatus.SelectedValue.ToString());
                }
                CargaGrid("Subcategoria");
                DGVSubcategorias.SelectedIndex = -1;
            }
        }


        #region Barra de navegacion inferior
        protected void BtnGridGiro_Click(object sender, EventArgs e)
        {
            MuestraPanelGridView("Giro");
        }

        protected void btnGridCategoria_Click(object sender, EventArgs e)
        {
            MuestraPanelGridView("Categoria");
        }

        protected void btnGridSubcategoria_Click(object sender, EventArgs e)
        {
            MuestraPanelGridView("Subcategoria");
        }

        protected void MuestraPanelGridView(string Panel)
        {
            switch (Panel)
            {
                case "Giro":
                    //Visibilidad de panel de busqueda
                    PanelGridGiro.Visible = true;
                    PanelGridCategoria.Visible = false;
                    PanelGridSubcategoria.Visible = false;
                    //Clase de bootstrap
                    libtnGridGiro.Attributes.Add("class", "active");
                    libtnGridCategoria.Attributes.Add("class", "");
                    libtnGridSubcategoria.Attributes.Add("class", "");
                    break;
                case "Categoria":
                    //Visibilidad de panel de busqueda 
                    PanelGridGiro.Visible = false;
                    PanelGridCategoria.Visible = true;
                    PanelGridSubcategoria.Visible = false;
                    //Clase de bootstrap
                    libtnGridGiro.Attributes.Add("class", "");
                    libtnGridCategoria.Attributes.Add("class", "active");
                    libtnGridSubcategoria.Attributes.Add("class", "");
                    break;
                case "Subcategoria":
                    //Visibilidad de panel de busqueda
                    PanelGridGiro.Visible = false;
                    PanelGridCategoria.Visible = false;
                    PanelGridSubcategoria.Visible = true;
                    //Clase de bootstrap
                    libtnGridGiro.Attributes.Add("class", "");
                    libtnGridCategoria.Attributes.Add("class", "");
                    libtnGridSubcategoria.Attributes.Add("class", "active");
                    break;
                default:

                    break;
            }
        }
        /// <summary>
        /// Giro, Categoria, Subcategoria
        /// </summary>
        /// <param name="Grid"></param>
        protected void CargaGrid(String Grid)
        {
            switch (Grid)
            {
                case "Giro":
                    DGVGiro.DataSource = MVGiro.LISTADEGIRO;
                    DGVGiro.DataBind();
                    break;
                case "Categoria":
                    DGVCategorias.DataSource = MVCategoria.LISTADECATEGORIAS;
                    DGVCategorias.DataBind();
                    break;
                case "Subcategoria":
                    DGVSubcategorias.DataSource = MVSubCategoria.LISTADESUBCATEGORIAS;
                    DGVSubcategorias.DataBind();
                    break;
                default:
                    break;
            }
        }
        #endregion
        #endregion

        #region Panel derecho

        #region Giro
        protected void ActivaControlesGiro()
        {
            txtNombreGiro.Enabled = true;
            txtDescripcionGiro.Enabled = true;
            DDLEstatusGIro.Enabled = true;

            btn2ImagenGiro.Enabled = true;
            btn2ImagenGiro.CssClass = "btn btn-sm btn-default";
        }
        protected void LimpiaDatosGiro()
        {
            txtNombreGiro.Text = string.Empty;
            txtDescripcionGiro.Text = string.Empty;
            DDLEstatusGIro.SelectedIndex = -1;
        }
        protected void DesactivaControlesGiro()
        {
            txtNombreGiro.Enabled = false;
            txtDescripcionGiro.Enabled = false;
            DDLEstatusGIro.Enabled = false;
            btn2ImagenGiro.Enabled = false;
            btn2ImagenGiro.CssClass = "btn btn-sm btn-default disabled";
        }
        protected void QuitaCamposObligatoriosGiro()
        {
            txtNombreGiro.BorderColor = Color.Empty;
            txtDescripcionGiro.BorderColor = Color.Empty;
        }

        #region GridView
        protected void DGVGiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Acciones = "Edicion";
            ManejoDeTextBox("Desactivado");
            string UidGiroSeleccionado = DGVGiro.SelectedDataKey.Value.ToString();
            MuestraGiro(UidGiroSeleccionado);
            DGVCategorias.SelectedIndex = -1;
            DGVSubcategorias.SelectedIndex = -1;
            txtIdCategoria.Text = string.Empty;
            txtUidSubCategoria.Text = string.Empty;

            DGVSubcategorias.DataSource = null;
            DGVSubcategorias.DataBind();

        }
        protected void DGVGiro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGVGiro.PageIndex = e.NewPageIndex;
            CargaGrid("Giro");
        }
        protected void DGVGiro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVGiro, "Select$" + e.Row.RowIndex);

                Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                if (e.Row.Cells[3].Text == "1")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                    ESTATUS.ToolTip = "ACTIVO";
                }
                if (e.Row.Cells[3].Text == "2")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                    ESTATUS.ToolTip = "INACTIVO";
                }
            }
        }
        protected void MuestraGiro(string UidGiro)
        {
            //Vacia los id de categoria y subcategoria
            txtIdCategoria.Text = string.Empty;
            txtUidSubCategoria.Text = string.Empty;
            //Obtiene datos del giro.
            MVGiro.BuscarGiro(UidGiro: UidGiro);
            MVImagen.ObtenerImagenGiro(UidGiro);
            txtUidGiro.Text = MVGiro.UIDVM.ToString();
            txtNombreGiro.Text = MVGiro.STRNOMBRE;
            txtDescripcionGiro.Text = MVGiro.STRDESCRIPCION;
            DDLEstatusGIro.SelectedIndex = DDLEstatusGIro.Items.IndexOf(DDLEstatusGIro.Items.FindByValue(MVGiro.INTESTATUS.ToString()));


            //Muestra imagen relacionada con el Giro 
            txtUidImagenGiro.Text = MVImagen.ID.ToString();
            if (txtUidImagenGiro.Text == Guid.Empty.ToString())
            {
                ImgGiro.ImageUrl = "Img/Giro/Default.png";
                ImgGiro.DataBind();
            }
            else
            {
                ImgGiro.ImageUrl = MVImagen.STRRUTA;
                ImgGiro.DataBind();
            }


            //Obtiene las categorias asociadas
            MVCategoria.BuscarCategorias(UidGiro: UidGiro);
            DGVCategorias.DataSource = MVCategoria.LISTADECATEGORIAS;
            DGVCategorias.DataBind();
        }
        #endregion

        #region Imagen
        protected void ActivaFileUploadGiro(object sender, EventArgs e)
        {
            lblEstado.Text = string.Empty;
            txtUidImagenGiro.Text = string.Empty;
            btn2ImagenGiro.Attributes.Add("onclick", "document.getElementById('" + FUGiro.ClientID + "').click()");
        }
        protected void MuestraFotoGiro(object sender, EventArgs e)
        {
            if (FUGiro.HasFile)
            {
                if (PanelGridGiro.Visible == true)
                {
                    GuardaFoto(FUGiro, "Img/Giro/");
                }
                if (PanelGridCategoria.Visible == true)
                {
                    GuardaFoto(FUGiro, "Img/Categoria/");
                }
                if (PanelGridSubcategoria.Visible == true)
                {
                    GuardaFoto(FUGiro, "Img/Subcategoria/");
                }
            }
        }

        #endregion

        #endregion

        #region Categoria



        #region Imagen
        protected void GuardaFoto(FileUpload FU, string RUTA)
        {
            if (MVImagen.ValidarExtencionImagen(Path.GetExtension(FU.FileName).ToLower()))
            {
                GuardarImagenGiro:
                //Valida si el directorio existe en el servidor
                if (Directory.Exists(Server.MapPath(RUTA)))
                {
                    long Random = new Random().Next(999999999);
                    string RutaCompleta = RUTA + Random + ".png";

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
                        ImgGiro.ImageUrl = almacenamiento;
                    }
                    else
                    {
                        lblEstado.Text = "Imagen existente en el sistema, favor de agregar otra.";
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


        #endregion

        #region GridView categorias
        protected void DGVCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVCategorias, "Select$" + e.Row.RowIndex);
                Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                if (e.Row.Cells[3].Text == "1")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                    ESTATUS.ToolTip = "ACTIVO";
                }
                if (e.Row.Cells[3].Text == "2")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                    ESTATUS.ToolTip = "INACTIVO";
                }
            }
        }
        protected void DGVCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUidSubCategoria.Text = string.Empty;
            Acciones = "Edicion";
            ManejoDeTextBox("Desactivado");
            string UidCategoriaSeleccionada = DGVCategorias.SelectedDataKey.Value.ToString();
            MuestraCategoria(UidCategoriaSeleccionada);
            Session.Remove("Accion");
            DGVSubcategorias.SelectedIndex = -1;
        }

        protected void MuestraCategoria(string UidCategoria)
        {

            //vacia uid de giro
            txtUidSubCategoria.Text = string.Empty;

            MVCategoria.BuscarCategorias(UidCategoria: UidCategoria);
            MVSubCategoria.BuscarSubCategoria(UidCategoria: UidCategoria);
            MVImagen.ObtenerImagenesSubcaterias(new Guid(UidCategoria));
            MVImagen.ObtenerImagenCategoria(UidCategoria);

            txtIdCategoria.Text = MVCategoria.UIDCATEGORIA.ToString();
            txtNombreGiro.Text = MVCategoria.STRNOMBRE;
            txtDescripcionGiro.Text = MVCategoria.STRDESCRIPCION;

            DDLEstatusGIro.SelectedIndex = DDLEstatusGIro.Items.IndexOf(DDLEstatusGIro.Items.FindByValue(MVCategoria.ESTATUS.ToString()));

            txtUidImagenGiro.Text = MVImagen.ID.ToString();
            if (txtUidImagenGiro.Text == Guid.Empty.ToString())
            {
                ImgGiro.ImageUrl = "Img/Categoria/Default.jpg";
                ImgGiro.DataBind();
            }
            else
            {
                ImgGiro.ImageUrl = MVImagen.STRRUTA;
                ImgGiro.DataBind();
            }
            DGVSubcategorias.DataSource = MVSubCategoria.LISTADESUBCATEGORIAS;
            DGVSubcategorias.DataBind();

        }
        #endregion
        #endregion

        #region Subcategoria

        protected void LimpiarCajasDeTextoSubcategoria()
        {
            txtUidSubCategoria.Text = string.Empty;
            txtNombreGiro.Text = string.Empty;
            txtDescripcionGiro.Text = string.Empty;
        }

        #region GridView

        protected void DGVSubcategorias_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            string Valor = DGVSubcategorias.SelectedDataKey.Value.ToString();

            MVSubCategoria.BuscarSubCategoria(UidSubCategoria: Valor);
            MVImagen.ObtenerImagenSubcategoria(Valor);
            txtUidSubCategoria.Text = MVSubCategoria.UID.ToString();
            DDLEstatusGIro.SelectedIndex = DDLEstatusGIro.Items.IndexOf(DDLEstatusGIro.Items.FindByValue(MVSubCategoria.ESTATUS.ToString()));
            txtNombreGiro.Text = MVSubCategoria.STRNOMBRE;
            txtDescripcionGiro.Text = MVSubCategoria.STRDESCRIPCION;

            if (MVSubCategoria.rutaImagen == string.Empty)
            {
                ImgGiro.ImageUrl = "Img/Subcategoria/Default.jpg";
                ImgGiro.DataBind();
            }
            else
            {
                ImgGiro.ImageUrl = MVImagen.STRRUTA;
                ImgGiro.DataBind();
            }

        }
        protected void DGVSubcategorias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DGVSubcategorias, "Select$" + e.Row.RowIndex);
            }

        }
        #endregion



        #endregion

        //#region Paneles
        //protected void btnDatosCategoria_Click(object sender, EventArgs e)
        //{
        //    PnlGiro.Visible = false;
        //    PnlCategorias.Visible = true;
        //    PnlSubcategorias.Visible = false;

        //    liDatosGiro.Attributes.Add("class", "");
        //    liDatosCategoria.Attributes.Add("class", "active");
        //    liDatosSubCategorias.Attributes.Add("class", "");
        //    if (Session["Accion"] != null)
        //    {
        //        Acciones = Session["Accion"].ToString();
        //        ManejoDeTextBox(ControlDeAccion: "Activado");
        //    }
        //    else
        //    {
        //        ManejoDeTextBox(ControlDeAccion: "Desactivado");
        //    }
        //}
        //protected void btnDatosSubCategorias_Click(object sender, EventArgs e)
        //{
        //    PnlGiro.Visible = false;
        //    PnlCategorias.Visible = false;
        //    PnlSubcategorias.Visible = true;

        //    liDatosGiro.Attributes.Add("class", "");
        //    liDatosCategoria.Attributes.Add("class", "");
        //    liDatosSubCategorias.Attributes.Add("class", "active");
        //    if (Session["Accion"] != null)
        //    {
        //        Acciones = Session["Accion"].ToString();
        //        ManejoDeTextBox(ControlDeAccion: "Activado");
        //    }
        //    else
        //    {
        //        ManejoDeTextBox(ControlDeAccion: "Desactivado");
        //    }
        //}
        //protected void btnDatosGiro_Click(object sender, EventArgs e)
        //{
        //    PnlGiro.Visible = true;
        //    PnlCategorias.Visible = false;
        //    PnlSubcategorias.Visible = false;

        //    liDatosGiro.Attributes.Add("class", "active");
        //    liDatosCategoria.Attributes.Add("class", "");
        //    liDatosSubCategorias.Attributes.Add("class", "");

        //    if (Session["Accion"] != null)
        //    {
        //        Acciones = Session["Accion"].ToString();
        //        ManejoDeTextBox(ControlDeAccion: "Activado");
        //    }
        //    else
        //    {
        //        ManejoDeTextBox(ControlDeAccion: "Desactivado");
        //    }
        //}
        //#endregion


        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Acciones = "NuevoRegistro";
            Session["Accion"] = Acciones;
            Session.Remove("RutaImagen");
            ManejoDeTextBox();
            if (PanelGridGiro.Visible == true)
            {
                ImgGiro.ImageUrl = "Img/Giro/Default.png";
            }
            if (PanelGridCategoria.Visible == true)
            {
                ImgGiro.ImageUrl = "Img/Categoria/Default.jpg";
            }
            if (PanelGridSubcategoria.Visible == true)
            {
                ImgGiro.ImageUrl = "Img/Subcategoria/Default.jpg";
            }
            if (DGVGiro.SelectedIndex == -1)
            {
                txtUidGiro.Text = string.Empty;
            }
            if (DGVCategorias.SelectedIndex == -1)
            {
                txtIdCategoria.Text = string.Empty;
            }

            txtUidSubCategoria.Text = string.Empty;
            LimpiaDatosGiro();
            lblGuardarDatos.CssClass = "glyphicon glyphicon-ok";


        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ManejoDeTextBox();
            QuitaCamposObligatorios();
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Acciones = "Edicion";
            Session["Accion"] = Acciones;
            ManejoDeTextBox("Activado");
        }

        protected void ManejoDeTextBox(string ControlDeAccion = "")
        {
            if (PanelGridGiro.Visible == true)
            {
                if (Acciones == "NuevoRegistro")
                {
                    ActivaControlesGiro();
                    LimpiaDatosGiro();
                    txtUidGiro.Text = string.Empty;
                    //Visibilidad de boton guardar y cancelar
                    btnGuardar.Visible = true;
                    btnCancelar.Visible = true;
                    lblEstado.Text = "";
                    btnNuevo.Enabled = true;
                    btnNuevo.CssClass = "btn btn-sm btn-default ";
                    lblGuardarDatos.CssClass = "glyphicon glyphicon-ok";
                }
                if (Acciones == "Edicion" && ControlDeAccion == "Activado")
                {
                    ActivaControlesGiro();
                    //Visibilidad de boton guardar y cancelar
                    btnGuardar.Visible = true;
                    btnCancelar.Visible = true;
                    lblEstado.Text = "";
                    btnNuevo.Enabled = true;
                    btnNuevo.CssClass = "btn btn-sm btn-default ";
                    lblGuardarDatos.CssClass = "glyphicon glyphicon-ok";
                }
                if (Acciones == "Edicion" && ControlDeAccion == "Desactivado")
                {
                    DesactivaControlesGiro();
                    btnEditar.Enabled = true;
                    btnEditar.CssClass = "btn btn-sm btn-default";
                    lblEstado.Text = string.Empty;

                    btnNuevo.Enabled = true;
                    btnNuevo.CssClass = "btn btn-sm btn-default ";

                    btnGuardar.Visible = false;
                    btnCancelar.Visible = false;

                }
                if (Acciones == "" && ControlDeAccion == "")
                {
                    btnNuevo.Enabled = true;
                    btnNuevo.CssClass = "btn btn-sm btn-default ";
                    btnGuardar.Visible = false;
                    btnCancelar.Visible = false;
                    lblEstado.Text = string.Empty;

                    if (txtNombreGiro.Enabled == true)
                    {
                        DesactivaControlesGiro();
                        LimpiaDatosGiro();
                        btnGuardar.Visible = false;
                        btnCancelar.Visible = false;
                        lblEstado.Text = "";
                        btnNuevo.Enabled = true;
                        btnNuevo.CssClass = "btn btn-sm btn-default ";
                    }
                    else
                    {
                        DesactivaControlesGiro();
                        LimpiaDatosGiro();
                    }
                }
            }
            if (PanelGridCategoria.Visible == true)
            {
                if (Acciones == "Edicion" && ControlDeAccion == "Activado")
                {
                    ActivaControlesGiro();
                    lblEstado.Text = string.Empty;
                    btnNuevo.Enabled = false;
                    btnNuevo.CssClass = "btn btn-sm btn-default disabled";
                    lblGuardarDatos.CssClass = "glyphicon glyphicon-refresh";
                    //Visibilidad de boton guardar y cancelar
                    btnGuardar.Visible = true;
                    btnCancelar.Visible = true;
                    lblEstado.Text = string.Empty;
                    Session.Remove("RutaImagen");

                }
                if (Acciones == "Edicion" && ControlDeAccion == "Desactivado")
                {
                    DesactivaControlesGiro();
                    btnEditar.Enabled = true;
                    btnEditar.CssClass = "btn btn-sm btn-default";
                    lblEstado.Text = string.Empty;

                    btnNuevo.Enabled = true;
                    btnNuevo.CssClass = "btn btn-sm btn-default ";

                    btnGuardar.Visible = false;
                    btnCancelar.Visible = false;

                    Session.Remove("RutaImagen");
                }
                if (Acciones == "NuevoRegistro")
                {
                    ActivaControlesGiro();
                    lblEstado.Text = "";
                    btnNuevo.Enabled = true;
                    btnNuevo.CssClass = "btn btn-sm btn-default ";
                    //Visibilidad de boton guardar y cancelar
                    btnGuardar.Visible = true;
                    btnCancelar.Visible = true;
                    Acciones = "NuevoRegistro";
                    Session["Accion"] = Acciones;
                    //Session.Remove("IDCATEGORIA");
                    lblEstado.Text = string.Empty;
                    txtIdCategoria.Text = string.Empty;
                    txtUidImagenGiro.Text = Guid.Empty.ToString();
                    ImgGiro.ImageUrl = "Img/Subcategoria/Default.jpg";
                }

                if (Acciones == "" && ControlDeAccion == "")
                {

                    btnNuevo.Enabled = true;
                    btnNuevo.CssClass = "btn btn-sm btn-default ";
                    btnGuardar.Visible = false;
                    btnCancelar.Visible = false;
                    lblEstado.Text = string.Empty;

                    if (txtNombreGiro.Enabled == true)
                    {
                        DesactivaControlesGiro();


                        if (txtIdCategoria.Text == string.Empty)
                        {
                            LimpiaDatosGiro();

                            Session.Remove("Accion");
                        }
                        else
                        {
                            MuestraCategoria(txtIdCategoria.Text);
                            Session.Remove("Accion");
                        }
                        //Obtiene la ruta a borrar
                        if (Session["RutaImagen"] != null)
                        {
                            string Ruta = Session["RutaImagen"].ToString();

                            //Borra la imagen de la empresa
                            if (MVImagen.ValidaExistenciaDeImagen(Ruta) == 0)
                            {
                                if (File.Exists(Server.MapPath(Ruta)))
                                {
                                    File.Delete(Server.MapPath(Ruta));
                                }
                            }

                            if (txtIdCategoria.Text == string.Empty)
                            {
                                //Recarga el controlador de la imagen de la categoria con una imagen default
                                ImgGiro.ImageUrl = "Img/Categoria/Default.jpg";
                                ImgGiro.DataBind();


                            }

                            Session.Remove("RutaImagen");
                        }
                    }
                    else
                    {
                        DesactivaControlesGiro();
                        LimpiaDatosGiro();
                    }
                }


            }
            if (PanelGridSubcategoria.Visible == true)
            {
                if (Acciones == "NuevoRegistro")
                {
                    ActivaControlesGiro();
                    lblEstado.Text = string.Empty;
                    btnNuevo.Enabled = false;
                    btnNuevo.CssClass = "btn btn-sm btn-default disabled";
                    lblGuardarDatos.CssClass = "glyphicon glyphicon-ok";
                    //Visibilidad de boton guardar y cancelar
                    btnGuardar.Visible = true;
                    btnCancelar.Visible = true;
                    lblEstado.Text = string.Empty;
                    txtUidSubCategoria.Text = string.Empty;
                    LimpiarCajasDeTextoSubcategoria();
                    ImgGiro.ImageUrl = "Img/Subcategoria/Default.jpg";

                }
                if (Acciones == "Edicion" && ControlDeAccion == "Activado")
                {
                    ActivaControlesGiro();
                    lblEstado.Text = string.Empty;
                    btnNuevo.Enabled = false;
                    btnNuevo.CssClass = "btn btn-sm btn-default disabled";
                    lblGuardarDatos.CssClass = "glyphicon glyphicon-refresh";
                    //Visibilidad de boton guardar y cancelar
                    btnGuardar.Visible = true;
                    btnCancelar.Visible = true;
                    lblEstado.Text = string.Empty;
                }
                if (Acciones == "Edicion" && ControlDeAccion == "Desactivado")
                {
                    DesactivaControlesGiro();
                    btnEditar.Enabled = true;
                    btnEditar.CssClass = "btn btn-sm btn-default";
                    lblEstado.Text = string.Empty;

                    btnNuevo.Enabled = true;
                    btnNuevo.CssClass = "btn btn-sm btn-default ";

                    btnGuardar.Visible = false;
                    btnCancelar.Visible = false;
                }
                else if (Acciones == "" && ControlDeAccion == "")
                {
                    btnNuevo.Enabled = true;
                    btnNuevo.CssClass = "btn btn-sm btn-default ";
                    btnGuardar.Visible = false;
                    btnCancelar.Visible = false;
                    lblEstado.Text = string.Empty;

                    if (txtNombreGiro.Enabled == true)
                    {
                        DesactivaControlesGiro();

                        if (txtIdCategoria.Text == string.Empty)
                        {
                            LimpiarCajasDeTextoSubcategoria();

                            Session.Remove("Accion");
                        }
                        else
                        {
                            MuestraCategoria(txtIdCategoria.Text);
                            Session.Remove("Accion");
                        }
                        //Obtiene la ruta a borrar
                        if (Session["RutaImagen"] != null)
                        {
                            string Ruta = Session["RutaImagen"].ToString();

                            //Borra la imagen de la empresa
                            if (MVImagen.ValidaExistenciaDeImagen(Ruta) == 0)
                            {
                                if (File.Exists(Server.MapPath(Ruta)))
                                {
                                    File.Delete(Server.MapPath(Ruta));
                                }
                            }
                            if (txtUidSubCategoria.Text == string.Empty)
                            {
                                //Recarga el controlador de la imagen de la categoria con una imagen default
                                ImgGiro.ImageUrl = "Img/Subcategoria/Default.jpg";
                                ImgGiro.DataBind();
                            }
                            Session.Remove("RutaImagen");
                        }
                    }
                    else
                    {
                        DesactivaControlesGiro();
                        LimpiaDatosGiro();
                    }

                }

            }
        }


        #endregion
        protected void CamposObligatorios()
        {
            if (txtNombreGiro.Text == string.Empty)
            {
                txtNombreGiro.BorderColor = Color.Red;
            }
            if (txtDescripcionGiro.Text == string.Empty)
            {
                txtDescripcionGiro.BorderColor = Color.Red;
            }
            if (DDLEstatusGIro.SelectedValue == "-1")
            {
                DDLEstatusGIro.BorderColor = Color.Red;
            }
            if (Session["RutaImagen"] == null)
            {
                btn2ImagenGiro.BorderColor = Color.Red;
                btn2ImagenGiro.ToolTip = "Adjuntar imagen";
            }
        }

        protected void QuitaCamposObligatorios()
        {
            txtNombreGiro.BorderColor = Color.Empty;
            txtDescripcionGiro.BorderColor = Color.Empty;
            DDLEstatusGIro.BorderColor = Color.Empty;
            btn2ImagenGiro.BorderColor = Color.Empty;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //Propiedades
            string nombre = txtNombreGiro.Text;
            string descripcion = txtDescripcionGiro.Text;
            string Estatus = DDLEstatusGIro.SelectedValue;
            string UidGiroSeleccionado = txtUidGiro.Text;
            string CategoriaSeleccionada = txtIdCategoria.Text;


            QuitaCamposObligatorios();
            #region Manejo de datos Giro
            //Manejo de datos Giro 
            if (PanelGridGiro.Visible == true)
            {
                if (nombre == string.Empty || descripcion == string.Empty || Estatus == "-1" || Session["RutaImagen"] == null)
                {
                    CamposObligatorios();
                }
                else
                {
                    if (UidGiroSeleccionado == string.Empty)
                    {

                        Guid UidGiro = Guid.NewGuid();
                        if (MVGiro.GuardaGiro(UidGiro, nombre, descripcion, Estatus))
                        {
                            MVImagen.GuardaImagen(Session["RutaImagen"].ToString(), UidGiro.ToString(), "asp_InsertaImagenGiro");
                        }
                        lblEstado.Text = "Giro Guardado";
                        Acciones = string.Empty;
                        ManejoDeTextBox();


                    }
                    else
                    {
                        if (MVGiro.ActualizaGiro(new Guid(UidGiroSeleccionado), nombre, descripcion, Estatus))
                        {
                            if (Session["RutaImagen"] != null)
                            {
                                MVImagen.ObtenerImagenGiro(UidGiroSeleccionado.ToString());
                                string Ruta = MVImagen.STRRUTA;
                                if (File.Exists(Server.MapPath(Ruta)))
                                {
                                    File.Delete(Server.MapPath(Ruta));
                                }
                                MVImagen.EliminaImagenGiro(UidGiroSeleccionado.ToString());
                                MVImagen.GuardaImagen(Session["RutaImagen"].ToString(), UidGiroSeleccionado.ToString(), "asp_InsertaImagenGiro");
                            }
                        }
                        Acciones = "Edicion";
                        ManejoDeTextBox("Desactivado");
                    }

                    MVGiro.BuscarGiro();
                    DGVGiro.SelectedIndex = -1;
                    CargaGrid("Giro");
                    MuestraPanelGridView("Giro");
                    Session.Remove("RutaImagen");
                }
            }

            #endregion

            #region Manejo de datos Categoria 
            //Manejo de datos Categoria
            if (PanelGridCategoria.Visible == true)
            {
                #region Propiedades
                Guid UidCategoria = Guid.NewGuid();
                Acciones = Session["Accion"].ToString();

                #endregion

                if (UidGiroSeleccionado == string.Empty)
                {
                    lblEstado.Text = "Selecione un giro para poder agregar una categoria";
                }
                else

                if (nombre == string.Empty && descripcion == string.Empty && Estatus == "-1" && Session["RutaImagen"] == null)
                {
                    CamposObligatorios();
                }
                else
                {
                    if (CategoriaSeleccionada == string.Empty)
                    {
                        if (MVCategoria.Guardar(UidCategoria, nombre, descripcion, Estatus, UidGiroSeleccionado))
                        {
                            lblEstado.Text = "Categoria agregada";

                            if (Session["RutaImagen"] != null)
                            {
                                MVImagen.GuardaImagen(Session["RutaImagen"].ToString(), UidCategoria.ToString(), "asp_InsertaImagenCategoria");
                            }
                            Acciones = string.Empty;
                            ManejoDeTextBox();
                        }
                        else
                        {
                            lblEstado.Text = "Error 404";
                        }
                    }
                    else
                    if (CategoriaSeleccionada != string.Empty)
                    {
                        UidCategoria = new Guid(CategoriaSeleccionada);
                        if (MVCategoria.Actualizar(UidCategoria, nombre, descripcion, Estatus, UidGiroSeleccionado))
                        {
                            if (Session["RutaImagen"] != null)
                            {
                                MVImagen.ObtenerImagenCategoria(UidCategoria.ToString());
                                string Ruta = MVImagen.STRRUTA;
                                if (File.Exists(Server.MapPath(Ruta)))
                                {
                                    File.Delete(Server.MapPath(Ruta));
                                }
                                MVImagen.EliminaImagenCategoria(UidCategoria.ToString());
                                MVImagen.GuardaImagen(Session["RutaImagen"].ToString(), UidCategoria.ToString(), "asp_InsertaImagenCategoria");
                            }

                            lblEstado.Text = "Categoria actualizada";
                            Acciones = "Edicion";
                            ManejoDeTextBox("Desactivado");
                        }
                        else
                        {
                            lblEstado.Text = "Error al actualizar";
                        }
                    }
                    MVCategoria.BuscarCategorias(UidGiro: UidGiroSeleccionado);
                    CargaGrid("Categoria");
                    Session.Remove("Accion");
                    Session.Remove("RutaImagen");
                }
            }
            #endregion

            // Manejo de datos subcategoria
            if (PanelGridSubcategoria.Visible == true)
            {
                if (CategoriaSeleccionada == string.Empty && CategoriaSeleccionada == "")
                {
                    lblEstado.Text = "Selecione una categoria para poder agregar una subcategoria";
                }
                else
                if (nombre == string.Empty && descripcion == string.Empty && Estatus == "-1" && Session["RutaImagen"] == null)
                {
                    CamposObligatorios();
                }
                else
                {
                    string UidSubcategoria = txtUidSubCategoria.Text;
                    MVImagen = new VMImagen();

                    string UidImagen = txtUidImagenGiro.Text;
                    //Verifica si es una nueva subcategoria
                    if (UidSubcategoria == "" & UidSubcategoria == string.Empty)
                    {
                        Guid UidSubCategoria = Guid.NewGuid();
                        if (MVSubCategoria.GuardarSubcategoria(UidSubCategoria.ToString(), nombre, descripcion, Estatus, CategoriaSeleccionada))
                        {
                            if (Session["RutaImagen"] != null)
                            {
                                MVImagen.GuardaImagen(Session["RutaImagen"].ToString(), UidSubCategoria.ToString(), "asp_InsertaImagenSubcategoria");
                            }
                            Acciones = string.Empty;
                            ManejoDeTextBox();
                            MVSubCategoria.BuscarSubCategoria(UidCategoria: CategoriaSeleccionada);
                            CargaGrid("Subcategoria");
                            Session.Remove("RutaImagen");
                        }

                    }
                    //Verifica si es una actualizacion de una subcategoria
                    else if (UidSubcategoria != string.Empty)
                    {

                        if (MVSubCategoria.ActualizaSubcategoria(UidSubcategoria, nombre, descripcion, Estatus))
                        {
                            if (Session["RutaImagen"] != null)
                            {
                                MVImagen.ObtenerImagenSubcategoria(UidSubcategoria);
                                string ruta = MVImagen.STRRUTA;
                                if (File.Exists(Server.MapPath(ruta)))
                                {
                                    File.Delete(Server.MapPath(ruta));
                                }
                                MVImagen.EliminaImagenSubcategoria(new Guid(UidSubcategoria));
                                MVImagen.GuardaImagen(Session["RutaImagen"].ToString(), UidSubcategoria.ToString(), "asp_InsertaImagenSubcategoria");
                            }
                            Acciones = "Edicion";
                            ManejoDeTextBox("Desactivado");
                        }
                        MVSubCategoria.BuscarSubCategoria(UidCategoria: CategoriaSeleccionada);
                        CargaGrid("Subcategoria");
                        Session["MVSubCategoria"] = MVSubCategoria;
                        Session["MVImagen"] = MVImagen;
                        Session.Remove("RutaImagen");
                    }

                }
            }
        }
        protected void LimpiaCamposobligartorios()
        {
            txtNombreGiro.BorderColor = Color.Empty;
            txtDescripcionGiro.BorderColor = Color.Empty;
        }

        protected void DGVCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGVCategorias.PageIndex = e.NewPageIndex;
            CargaGrid("Categoria");
        }

        protected void DGVSubcategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGVSubcategorias.PageIndex = e.NewPageIndex;
            CargaGrid("Subcategoria");
        }

        protected void BTnExportarCatalogos_Click(object sender, EventArgs e)
        {
            Session["ParametroVentanaExcel"] = "Catalogos";
            string _open = "window.open('Office/ExportarMenu.aspx', '_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
        }
    }


}