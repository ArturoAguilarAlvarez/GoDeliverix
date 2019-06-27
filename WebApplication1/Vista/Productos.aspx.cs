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
    public partial class Productos : System.Web.UI.Page
    {
        string Acciones = string.Empty;
        VMImagen MVImagen = new VMImagen();
        VMProducto MVProducto = new VMProducto();
        VMEstatus MVEstatus = new VMEstatus();
        VMGiro MVGiro = new VMGiro();
        VMCategoria MVCategoria = new VMCategoria();
        VMSubCategoria MVSubcategoria = new VMSubCategoria();
        ImagenHelper oImagenHelper = new ImagenHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            FUImagen.Attributes["onchange"] = "UploadFile(this)";

            if (!IsPostBack)
            {
                Session["MVImagen"] = MVImagen;
                Session["MVProducto"] = MVProducto;
                Session["MVGiro"] = MVGiro;
                Session["MVCategoria"] = MVCategoria;
                Session["MVSubcategoria"] = MVSubcategoria;

                Session.Remove("RutaImagen");

                txtFNombre.Attributes.Add("placeholder", "Nombre");
                txtNombre.Attributes.Add("placeholder", "Nombre");
                txtDescripcion.Attributes.Add("placeholder", "Descripcion");

                dgvProductos.DataSource = null;
                dgvProductos.DataBind();

                ImgProducto.ImageUrl = "Img/Productos/Default.jpg";

                MVEstatus.OBTENERLISTA();
                ddlestatus.DataSource = MVEstatus.ListaEstatus;
                ddlestatus.DataValueField = "ID";
                ddlestatus.DataTextField = "NOMBRE";
                ddlestatus.DataBind();
                ManejoDeAcciones();
                EstatusDeGestion(false);

                MuestraPanel("General");


                MVGiro.ListaDeGiroConimagen();
                CargaDropDownList("Giro");
                CargaListBox("Giro");

                //Muestra el tooltip de cada registro del giro
                DLGiro.DataSource = MVGiro.LISTADEGIRO;
                DLGiro.DataBind();
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

                MuestraFiltrosMultiSelect("Giro");
                MVGiro.BuscarGiro(Estatus: "1");
                CargaDropDownList("Giro");

                PanelMensaje.Visible = false;
            }
            else
            {
                MVImagen = (VMImagen)Session["MVImagen"];
                MVProducto = (VMProducto)Session["MVProducto"];
                MVGiro = (VMGiro)Session["MVGiro"];
                MVCategoria = (VMCategoria)Session["MVCategoria"];
                MVSubcategoria = (VMSubCategoria)Session["MVSubcategoria"];
            }
        }

        protected void SeleccionarImagen(object sender, EventArgs e)
        {
            btnImagen.Attributes.Add("onclick", "document.getElementById('" + FUImagen.ClientID + "').click()");
        }

        protected void MuestraFoto(object sender, EventArgs e)
        {
            if (FUImagen.HasFile)
            {
                GuardaFoto(FUImagen, "Img/Productos/", Session["UidEmpresaSistema"].ToString());

            }
        }

        protected void GuardaFoto(FileUpload FU, string RUTA, string UidEmpresa)
        {
            if (MVImagen.ValidarExtencionImagen(Path.GetExtension(FU.FileName).ToLower()))
            {
                GuardarImagenGiro:
                //Valida si el directorio existe en el servidor
                if (Directory.Exists(Server.MapPath(RUTA)))
                {
                    //Crea el directorio de la empresa

                    RUTA = RUTA + UidEmpresa;
                    CrearCarpetaDeEmpresa:
                    if (Directory.Exists(Server.MapPath(RUTA)))
                    {
                        CrearArchivoServidor:
                        //El archivo no existe en el servidor
                        if (!File.Exists(Server.MapPath(txtNombreDeImagen.Text)))
                        {
                            long Random = new Random().Next(999999999);
                            string RutaCompleta = RUTA + "/" + Random + ".png";
                            txtNombreDeImagen.Text = RutaCompleta;

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
                                ImgProducto.ImageUrl = almacenamiento;
                            }
                            else
                            {
                                lblEstado.Text = "Imagen existente en el sistema, favor de agregar otra.";
                            }
                        }
                        //Si el archivo existe lo elimina
                        else
                        {
                            File.Delete(Server.MapPath("~/Vista/" + txtNombreDeImagen.Text));
                            txtNombreDeImagen.Text = string.Empty;
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


        protected void EstatusDeGestion(bool estatus)
        {
            txtNombre.Enabled = estatus;
            txtDescripcion.Enabled = estatus;
            btnImagen.Enabled = estatus;
            ddlestatus.Enabled = estatus;
            DLGiro.Enabled = estatus;
            DLGiroSeleccionado.Enabled = estatus;
            DLCategoria.Enabled = estatus;
            DlCategoriaSeleccionada.Enabled = estatus;
            dlSubcategoria.Enabled = estatus;
        }
        protected void LimpiaDatosGestion()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            ImgProducto.ImageUrl = "Img/Productos/Default.jpg";
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Guid UidEmpresa = new Guid(Session["UidEmpresaSistema"].ToString());
            string Nombre = txtNombre.Text;
            string Descripcion = txtDescripcion.Text;
            string estatus = ddlestatus.SelectedItem.Value;
            QuitaColorCamposObligatorios();
            if (Nombre != string.Empty && Nombre != "" && Descripcion != string.Empty && Descripcion != "" && estatus != "-1")
            {
                if (txtUidProducto.Text == string.Empty)
                {
                    if (Session["RutaImagen"] != null)
                    {
                        Guid Uid = Guid.NewGuid();
                        if (MVProducto.Guardar(Nombre, Descripcion, UidEmpresa, Uid, estatus))
                        {
                            if (MVImagen.GuardaImagen(Session["RutaImagen"].ToString(), Uid.ToString(), "asp_InsertaImagenProducto"))
                            {
                                if (MVGiro.LISTADEGIROSELECCIONADO != null)
                                {
                                    foreach (var item in MVGiro.LISTADEGIROSELECCIONADO)
                                    {
                                        MVProducto.RelacionGiro(item.UIDVM.ToString(), Uid);
                                    }
                                }
                                if (MVCategoria.LISTADECATEGORIASELECIONADA != null)
                                {
                                    foreach (var item in MVCategoria.LISTADECATEGORIASELECIONADA)
                                    {
                                        MVProducto.RelacionCategoria(item.UIDCATEGORIA.ToString(), Uid);
                                    }
                                }
                                if (MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS != null)
                                {
                                    foreach (var item in MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS)
                                    {
                                        MVProducto.RelacionSubategoria(item.UID.ToString(), Uid);
                                    }
                                }
                                PanelMensaje.Visible = true;
                                LblMensaje.Text = "Producto agregado";
                            }
                            else
                            {
                                PanelMensaje.Visible = true;
                                LblMensaje.Text = "Ocurrio un problema al agregar la imagen. /n Estamos trabajando en ello";
                            }
                        }
                        else
                        {
                            PanelMensaje.Visible = true;
                            LblMensaje.Text = "Ocurrio un problema al agregar. /n Estamos trabajando en ello";
                        }
                        ManejoDeAcciones();
                    }
                    else
                    {
                        btnImagen.BorderColor = Color.Red;
                    }
                }
                else
                {
                    if (MVProducto.Actualizar(Nombre, Descripcion, new Guid(txtUidProducto.Text), estatus))
                    {
                        MVProducto.EliminaGiro(txtUidProducto.Text);
                        MVProducto.EliminaCategoria(txtUidProducto.Text);
                        MVProducto.EliminaSubcategoria(txtUidProducto.Text);

                        ObtenerGirosSeleccionados();
                        ObtenerCategoriasSeleccionadas();
                        ObtenerSubcategoriasSeleccionadas();

                        if (MVGiro.LISTADEGIROSELECCIONADO != null)
                        {
                            foreach (var item in MVGiro.LISTADEGIROSELECCIONADO)
                            {
                                MVProducto.RelacionGiro(item.UIDVM.ToString(), new Guid(txtUidProducto.Text));
                            }
                        }
                        if (MVCategoria.LISTADECATEGORIASELECIONADA != null)
                        {
                            foreach (var item in MVCategoria.LISTADECATEGORIASELECIONADA)
                            {
                                MVProducto.RelacionCategoria(item.UIDCATEGORIA.ToString(), new Guid(txtUidProducto.Text));
                            }
                        }
                        if (MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS != null)
                        {
                            foreach (var item in MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS)
                            {
                                MVProducto.RelacionSubategoria(item.UID.ToString(), new Guid(txtUidProducto.Text));
                            }
                        }


                        if (Session["RutaImagen"] != null)
                        {
                            MVImagen.EliminaImagenProducto(txtUidProducto.Text);
                            MVImagen.GuardaImagen(Session["RutaImagen"].ToString(), txtUidProducto.Text, "asp_InsertaImagenProducto");
                        }
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Producto actualizado";
                    }
                    else
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Ocurrio un problema al actualizar. /n Estamos trabajando en ello";
                    }
                    Acciones = "Edicion";
                    ManejoDeAcciones("Desactivado");
                }

                MVProducto.Buscar(UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()));
                dgvProductos.DataSource = MVProducto.ListaDeProductos;
                dgvProductos.DataBind();
            }
            else
            {
                if (Nombre == string.Empty && Nombre == "")
                {
                    txtNombre.BorderColor = Color.Red;
                }
                if (Descripcion == string.Empty && Descripcion == "")
                {
                    txtDescripcion.BorderColor = Color.Red;
                }
                if (estatus == "-1")
                {
                    ddlestatus.BorderColor = Color.Red;
                }
            }

        }

        public void QuitaColorCamposObligatorios()
        {
            txtNombre.BorderColor = Color.Empty;
            txtDescripcion.BorderColor = Color.Empty;
            ddlestatus.BorderColor = Color.Empty;
            btnImagen.BorderColor = Color.Empty;
        }

        protected void BtnBABuscar_Click(object sender, EventArgs e)
        {

            string Giro = string.Empty;
            string Categoria = string.Empty;
            string Subcategoria = string.Empty;
            if (LBFFGiro.Visible)
            {
                foreach (ListItem item in LBFFGiro.Items)
                {
                    if (item.Selected)
                    {
                        if (string.IsNullOrEmpty(Giro))
                        {
                            Giro = "" + item.Value + "";
                        }
                        else
                        {
                            Giro = Giro + "," + "" + item.Value + "";

                        }
                    }
                }
            }
            if (LBFCategoria.Visible)
            {
                foreach (ListItem item in LBFCategoria.Items)
                {
                    if (item.Selected)
                    {
                        if (string.IsNullOrEmpty(Categoria))
                        {
                            Categoria = "" + item.Value + "";

                        }
                        else
                        {
                            Categoria = Categoria + "," + "" + item.Value + "";

                        }
                    }
                }
            }
            if (LBFSubcategoria.Visible)
            {
                foreach (ListItem item in LBFSubcategoria.Items)
                {
                    if (item.Selected)
                    {
                        if (string.IsNullOrEmpty(Subcategoria))
                        {
                            Subcategoria = "" + item.Value + "";

                        }
                        else
                        {
                            Subcategoria = Subcategoria + "," + "" + item.Value + "";

                        }
                    }
                }
            }
            MVProducto.Buscar(UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()), Nombre: txtFNombre.Text, Giro: Giro, Categoria: Categoria, Subcategoria: Subcategoria);
            dgvProductos.DataSource = MVProducto.ListaDeProductos;
            dgvProductos.DataBind();
        }

        protected void GridSeleccionado(string GridView)
        {
            switch (GridView)
            {
                case "Giro":
                    DLGiroSeleccionado.DataSource = null;
                    DLGiroSeleccionado.DataBind();
                    break;
                case "Categoria":
                    DLCategoria.DataSource = null;
                    DLCategoria.DataBind();
                    DlCategoriaSeleccionada.DataSource = null;
                    DlCategoriaSeleccionada.DataBind();
                    break;
                case "Subcategoria":
                    dlSubcategoria.DataSource = null;
                    dlSubcategoria.DataBind();
                    break;
                default:
                    break;
            }
        }


        protected void ValidaAccion()
        {
            if (Session["Accion"] != null)
            {
                Acciones = Session["Accion"].ToString();
                ManejoDeAcciones(ControlDeAccion: "Activado");
            }
            else
            {
                ManejoDeAcciones(ControlDeAccion: "Desactivado");
            }
        }
        protected void ManejoDeAcciones(string ControlDeAccion = "")
        {
            if (Acciones == "NuevoRegistro")
            {
                EstatusDeGestion(true);
                LimpiaDatosGestion();
                txtUidProducto.Text = string.Empty;
                //Visibilidad de boton guardar y cancelar
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
                lblEstado.Text = "";
                //Habilita el boton de nuevo
                btnNuevo.Enabled = true;
                btnNuevo.CssClass = "btn btn-sm btn-default ";
                lblGuardarDatos.CssClass = "glyphicon glyphicon-ok";
                //Deshabilita el boton de editar
                btnEditar.Enabled = false;
                btnEditar.CssClass = "btn btn-sm btn-default disabled";
                MVGiro.LISTADEGIROSELECCIONADO.Clear();
                MVCategoria.LISTADECATEGORIASELECIONADA.Clear();
                MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Clear();
                GridSeleccionado("Giro");
                GridSeleccionado("Categoria");
                GridSeleccionado("Subcategoria");
                SeleccionaGiro();
                SeleccionaCategorias();
                SeleccionaSubcategoria();

            }
            if (Acciones == "Edicion" && ControlDeAccion == "Activado")
            {
                EstatusDeGestion(true);
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
                EstatusDeGestion(false);
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

                //Elimina una foto si ha sido cargada
                if (!string.IsNullOrEmpty(txtNombreDeImagen.Text))
                {
                    File.Delete(Server.MapPath("~/Vista/" + txtNombreDeImagen.Text));
                }
                LimpiaDatosGestion();
                //Limpia las listas
                MVGiro.LISTADEGIROSELECCIONADO.Clear();
                MVCategoria.LISTADECATEGORIAS.Clear();
                MVCategoria.LISTADECATEGORIASELECIONADA.Clear();
                MVSubcategoria.LISTADESUBCATEGORIAS.Clear();
                MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Clear();
                //Vacia los controles
                
                DLGiro.DataSource = MVGiro.LISTADEGIRO;
                DLGiro.DataBind();
                DLGiroSeleccionado.DataSource = MVGiro.LISTADEGIROSELECCIONADO;
                DlCategoriaSeleccionada.DataSource = MVCategoria.LISTADECATEGORIASELECIONADA;
                DLCategoria.DataSource = MVCategoria.LISTADECATEGORIAS;
                dlSubcategoria.DataSource = MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS;
                dlSubcategoria.DataSource = MVSubcategoria.LISTADESUBCATEGORIAS;
                //Recarga los controles
                DLGiro.DataBind();
                DLGiro.DataBind();
                DLGiroSeleccionado.DataBind();
                DlCategoriaSeleccionada.DataBind();
                DLCategoria.DataBind();
                dlSubcategoria.DataBind();
                dlSubcategoria.DataBind();
                EstatusDeGestion(false);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Session.Remove("RutaImagen");
            PanelMensaje.Visible = false;
            MVGiro.LISTADEGIROSELECCIONADO.Clear();
            MVCategoria.LISTADECATEGORIASELECIONADA.Clear();
            MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Clear();
            Acciones = "NuevoRegistro";
            Session["Accion"] = Acciones;
            ManejoDeAcciones();
            txtUidProducto.Text = string.Empty;
            lblGuardarDatos.CssClass = "glyphicon glyphicon-ok";
        }

        protected void dgvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvProductos, "Select$" + e.Row.RowIndex);

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

        protected void dgvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Acciones = "Edicion";
            ManejoDeAcciones("Desactivado");
            string UidProductoSeleccionado = dgvProductos.SelectedDataKey.Value.ToString();
            MuestraProducto(UidProductoSeleccionado);
        }

        private void MuestraProducto(string UidProductoSeleccionado)
        {
            MVProducto.Buscar(UidProducto: new Guid(UidProductoSeleccionado), UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()));
            MVImagen.ObtenerImagenProducto(UidProductoSeleccionado);
            //asigna los campos encontrados en la base de datos con los controles web
            txtUidProducto.Text = MVProducto.UID.ToString();
            txtNombre.Text = MVProducto.STRNOMBRE;
            txtDescripcion.Text = MVProducto.STRDESCRIPCION;
            ddlestatus.SelectedIndex = ddlestatus.Items.IndexOf(ddlestatus.Items.FindByValue(MVProducto.ESTATUS.ToString()));

            //Busca la imagen del propietario y la muesta en el control web image 
            ImgProducto.ImageUrl = MVImagen.STRRUTA;
            txtNombreDeImagen.Text = MVImagen.STRRUTA;

            //Vacia la lista de elementos seleccionados 
            MVGiro.LISTADEGIROSELECCIONADO.Clear();
            MVCategoria.LISTADECATEGORIASELECIONADA.Clear();
            MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Clear();
            //Recupera los giros asociados a la sucursal
            MVProducto.RecuperaGiro(UidProductoSeleccionado);
            //Recupera las categorias asociadas a la sucursal
            MVProducto.RecuperaCategoria(UidProductoSeleccionado);
            //Recupera las subcategorias asociadas a la sucursal
            MVProducto.RecuperaSubcategoria(UidProductoSeleccionado);

            //Varia los datos seleccionados en el data list giro
            foreach (DataListItem item in DLGiro.Items)
            {
                CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkGiro");
                ObjectoCheckBox.Checked = false;
            }


            //Muestra los giros seleccionados 
            if (MVProducto.ListaDeGiro.Count != 0)
            {
                DLGiroSeleccionado.SelectedIndex = -1;

                MVGiro.LISTADEGIROSELECCIONADO.Clear();

                foreach (var item in MVProducto.ListaDeGiro)
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
            if (MVProducto.ListaDeCategorias.Count != 0)
            {
                DlCategoriaSeleccionada.SelectedIndex = -1;

                MVCategoria.LISTADECATEGORIASELECIONADA.Clear();

                foreach (var item in MVProducto.ListaDeCategorias)
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
            if (MVProducto.ListaDeSubcategorias.Count != 0)
            {
                MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Clear();

                foreach (var item in MVProducto.ListaDeSubcategorias)
                {
                    MVSubcategoria.SeleccionarSubcategoria(item.UIDSUBCATEGORIA.ToString());
                }
            }
            DLCategoria.DataSource = null;
            DLCategoria.DataBind();

            dlSubcategoria.DataSource = null;
            dlSubcategoria.DataBind();
            SeleccionaGiro();

        }

        protected void SeleccionaGiro()
        {
            foreach (DataListItem item in DLGiro.Items)
            {
                CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkGiro");
                if (ObjectoCheckBox != null)
                {
                    if (MVGiro.LISTADEGIROSELECCIONADO.Exists(Giro => Giro.UIDVM.ToString() == DLGiro.DataKeys[item.ItemIndex].ToString()))
                    {
                        ObjectoCheckBox.Checked = true;
                    }
                    else
                    {
                        ObjectoCheckBox.Checked = false;
                    }
                }
            }
        }

        protected void BtnBALimpiar_Click(object sender, EventArgs e)
        {
            txtFNombre.Text = string.Empty;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Acciones = "Edicion";
            PanelMensaje.Visible = false;
            Session["Accion"] = Acciones;
            ManejoDeAcciones("Activado");
            lblGuardarDatos.CssClass = "glyphicon glyphicon-refresh";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            QuitaColorCamposObligatorios();
            ManejoDeAcciones();
        }
        protected void MuestraPanel(string Panel)
        {
            switch (Panel)
            {
                case "General":
                    liDatosGenerales.Attributes.Add("class", "active");
                    liDatosGiro.Attributes.Add("class", "");
                    liDatosCategoria.Attributes.Add("class", "");
                    liDatosSubcategoria.Attributes.Add("class", "");

                    panelGeneral.Visible = true;
                    panelGiro.Visible = false;
                    panelCategoria.Visible = false;
                    panelSubcategoria.Visible = false;
                    break;
                case "Giro":
                    liDatosGenerales.Attributes.Add("class", "");
                    liDatosGiro.Attributes.Add("class", "active");
                    liDatosCategoria.Attributes.Add("class", "");
                    liDatosSubcategoria.Attributes.Add("class", "");

                    panelGeneral.Visible = false;
                    panelGiro.Visible = true;
                    panelCategoria.Visible = false;
                    panelSubcategoria.Visible = false;
                    break;
                case "Categoria":
                    liDatosGenerales.Attributes.Add("class", "");
                    liDatosGiro.Attributes.Add("class", "");
                    liDatosCategoria.Attributes.Add("class", "active");
                    liDatosSubcategoria.Attributes.Add("class", "");

                    panelGeneral.Visible = false;
                    panelGiro.Visible = false;
                    panelCategoria.Visible = true;
                    panelSubcategoria.Visible = false;
                    break;
                case "Subcategoria":
                    liDatosGenerales.Attributes.Add("class", "");
                    liDatosGiro.Attributes.Add("class", "");
                    liDatosCategoria.Attributes.Add("class", "");
                    liDatosSubcategoria.Attributes.Add("class", "active");

                    panelGeneral.Visible = false;
                    panelGiro.Visible = false;
                    panelCategoria.Visible = false;
                    panelSubcategoria.Visible = true;
                    break;
            }
        }


        #region Barra de navegacion panel derecho
        protected void btnDatosGenerales_Click(object sender, EventArgs e)
        {
            MuestraPanel("General");
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

        protected void btnDatosGiro_Click(object sender, EventArgs e)
        {
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

        protected void btnGridCategoria_Click(object sender, EventArgs e)
        {
            MuestraPanel("Categoria");

            MVGiro.LISTADEGIROSELECCIONADO = new List<VMGiro>();

            if (DLGiro.Items.Count != 0)
            {
                ObtenerGirosSeleccionados();
            }
            else
            {
                foreach (var item in MVProducto.ListaDeGiro)
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
                foreach (var item in MVProducto.ListaDeCategorias)
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
                foreach (var item in MVProducto.ListaDeSubcategorias)
                {
                    if (!MVProducto.ListaDeSubcategorias.Exists(Subcat => Subcat.UIDSUBCATEGORIA == item.UIDSUBCATEGORIA))
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
            MuestraPanel("Subcategoria");
            if (DLCategoria.Items.Count != 0)
            {
                ObtenerCategoriasSeleccionadas();
            }
            else
            {
                foreach (var item in MVProducto.ListaDeCategorias)
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
                foreach (var item in MVProducto.ListaDeSubcategorias)
                {
                    if (!MVProducto.ListaDeSubcategorias.Exists(Subcat => Subcat.UIDSUBCATEGORIA == item.UIDSUBCATEGORIA))
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

            SeleccionaCategorias();
            Session["MVCategoria"] = MVCategoria;
        }
        protected void SeleccionaCategorias()
        {
            foreach (DataListItem item in DLCategoria.Items)
            {
                //Obtiene el control de checkbox del datalist
                CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkCategoria");
                if (ObjectoCheckBox != null)
                {
                    //Verifica que el checkbox no este seleccionado
                    if (MVCategoria.LISTADECATEGORIASELECIONADA.Exists(Categoria => Categoria.UIDCATEGORIA.ToString() == DLCategoria.DataKeys[item.ItemIndex].ToString()))
                    {
                        ObjectoCheckBox.Checked = true;
                    }
                    else
                    {
                        ObjectoCheckBox.Checked = false;
                    }
                }
            }
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

            SeleccionaSubcategoria();
        }
        protected void SeleccionaSubcategoria()
        {
            foreach (DataListItem item in dlSubcategoria.Items)
            {
                //Obtiene el control de checkbox del datalist
                CheckBox ObjectoCheckBox = (CheckBox)item.FindControl("ChkSubcategoria");
                if (ObjectoCheckBox != null)
                {
                    //Verifica que el checkbox no este seleccionado
                    if (MVSubcategoria.LISTADESUBCATEGORIASSELECCIONADAS.Exists(Sub => Sub.UID.ToString() == dlSubcategoria.DataKeys[item.ItemIndex].ToString()))
                    {
                        ObjectoCheckBox.Checked = true;
                    }
                    else
                    {
                        ObjectoCheckBox.Checked = false;
                    }
                }
            }
        }

        protected void ChkFGiro_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkFGiro.Checked)
            {
                MuestraFiltrosMultiSelect("Giro");
            }
            else
            {
                DDLFGiro.Visible = true;
                LBFFGiro.Visible = false;
                MVGiro.BuscarGiro(Estatus: "1");
                CargaDropDownList("Giro");
                MVCategoria.BuscarCategorias(UidGiro: Guid.Empty.ToString(), Estatus: "1", tipo: "Mostrar");
                CargaDropDownList("Categoria");
                MVSubcategoria.BuscarSubCategoria(UidCategoria: Guid.Empty.ToString(), estatus: "1", Tipo: "Mostrar");
                CargaDropDownList("Subcategoria");
            }
        }

        protected void ChkFCategoria_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkFCategoria.Checked)
            {
                //Busca los giros y selecciona el primero
                MVGiro.BuscarGiro(Estatus: "1");
                CargaDropDownList("Giro");
                DDLFGiro.SelectedIndex = 0;
                MVCategoria.BuscarCategorias(UidGiro: DDLFGiro.SelectedItem.Value, Estatus: "1", tipo: "Mostrar");
                MuestraFiltrosMultiSelect("Categoria");
            }
            else
            {
                //Busca los giros y selecciona el primero
                MVGiro.BuscarGiro(Estatus: "1");
                CargaDropDownList("Giro");
                DDLFGiro.SelectedIndex = 0;
                MVCategoria.BuscarCategorias(UidGiro: DDLFGiro.SelectedItem.Value, Estatus: "1", tipo: "Mostrar");
                CargaDropDownList("Categoria");
                MVSubcategoria.BuscarSubCategoria(UidCategoria: Guid.Empty.ToString(), estatus: "1", Tipo: "Mostrar");
                CargaDropDownList("Subcategoria");
                //Muestra el control de dropdownlist
                DDLFCategoria.Visible = true;
                LBFCategoria.Visible = false;
            }
        }

        protected void ChkFSubcategoria_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkFSubcategoria.Checked)
            {
                MVGiro.BuscarGiro(Estatus: "1");
                CargaDropDownList("Giro");
                DDLFGiro.SelectedIndex = 0;
                string UidGiro = DDLFGiro.SelectedItem.Value;
                MVCategoria.BuscarCategorias(UidGiro: UidGiro, Estatus: "1", tipo: "Mostrar");
                CargaDropDownList("Categoria");
                DDLFCategoria.SelectedIndex = 0;
                MVSubcategoria.BuscarSubCategoria(UidCategoria: DDLFCategoria.SelectedItem.Value, estatus: "1", Tipo: "Mostrar");
                MuestraFiltrosMultiSelect("Subcategoria");
            }
            else
            {
                MVGiro.BuscarGiro(Estatus: "1");
                CargaDropDownList("Giro");
                DDLFGiro.SelectedIndex = 0;
                string UidGiro = DDLFGiro.SelectedItem.Value;
                MVCategoria.BuscarCategorias(UidGiro: UidGiro, Estatus: "1", tipo: "Mostrar");
                CargaDropDownList("Categoria");
                DDLFCategoria.SelectedIndex = 0;
                string UidCategoria = DDLFCategoria.SelectedItem.Value;
                MVSubcategoria.BuscarSubCategoria(UidCategoria: UidCategoria, estatus: "1", Tipo: "Mostrar");
                CargaDropDownList("Subcategoria");
                DDLFSubcategoria.Visible = true;
                LBFSubcategoria.Visible = false;
            }
        }
        /// <summary>
        /// Carga el dropdownlist 
        /// </summary>
        /// <param name="DropdownListAMostrar">Giro,Categoria,Subcategoria</param>
        protected void CargaDropDownList(string DropdownListAMostrar)
        {
            switch (DropdownListAMostrar)
            {
                case "Giro":
                    DDLFGiro.DataSource = MVGiro.LISTADEGIRO;
                    DDLFGiro.DataTextField = "STRNOMBRE";
                    DDLFGiro.DataValueField = "UIDVM";
                    DDLFGiro.DataBind();
                    break;
                case "Categoria":
                    DDLFCategoria.DataSource = MVCategoria.LISTADECATEGORIAS;
                    DDLFCategoria.DataTextField = "STRNOMBRE";
                    DDLFCategoria.DataValueField = "UIDCATEGORIA";
                    DDLFCategoria.DataBind();
                    break;
                case "Subcategoria":
                    DDLFSubcategoria.DataSource = MVSubcategoria.LISTADESUBCATEGORIAS;
                    DDLFSubcategoria.DataTextField = "STRNOMBRE";
                    DDLFSubcategoria.DataValueField = "UID";
                    DDLFSubcategoria.DataBind();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Carga el listbox a mostrar
        /// </summary>
        /// <param name="ListBoxAMostrar">Giro,Categoria,Subcategoria</param>
        protected void CargaListBox(string ListBoxAMostrar)
        {
            switch (ListBoxAMostrar)
            {
                case "Giro":
                    LBFFGiro.DataSource = MVGiro.LISTADEGIRO;
                    LBFFGiro.DataTextField = "STRNOMBRE";
                    LBFFGiro.DataValueField = "UIDVM";
                    LBFFGiro.DataBind();
                    break;
                case "Categoria":
                    LBFCategoria.DataSource = MVCategoria.LISTADECATEGORIAS;
                    LBFCategoria.DataTextField = "STRNOMBRE";
                    LBFCategoria.DataValueField = "UIDCATEGORIA";
                    LBFCategoria.DataBind();
                    break;
                case "Subcategoria":
                    LBFSubcategoria.DataSource = MVSubcategoria.LISTADESUBCATEGORIAS;
                    LBFSubcategoria.DataTextField = "STRNOMBRE";
                    LBFSubcategoria.DataValueField = "UID";
                    LBFSubcategoria.DataBind();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Giro,Categoria,Subcategoria
        /// </summary>
        /// <param name="FiltroAMostrar"></param>
        protected void MuestraFiltrosMultiSelect(string FiltroAMostrar)
        {
            switch (FiltroAMostrar)
            {
                case "Giro":
                    //Giro
                    DDLFGiro.Visible = false;
                    LBFFGiro.Visible = true;
                    //Categoria
                    DDLFCategoria.Visible = true;
                    LBFCategoria.Visible = false;
                    //Subcategoria
                    DDLFSubcategoria.Visible = true;
                    LBFSubcategoria.Visible = false;
                    //Checkbox
                    ChkFGiro.Checked = true;
                    ChkFCategoria.Checked = false;
                    ChkFSubcategoria.Checked = false;
                    //ListBox y dropdownlist
                    CargaListBox("Giro");
                    break;
                case "Categoria":
                    //Giro
                    DDLFGiro.Visible = true;
                    LBFFGiro.Visible = false;
                    //Categoria
                    DDLFCategoria.Visible = false;
                    LBFCategoria.Visible = true;
                    //Subcategoria
                    DDLFSubcategoria.Visible = true;
                    LBFSubcategoria.Visible = false;
                    //Checkbox
                    ChkFGiro.Checked = false;
                    ChkFCategoria.Checked = true;
                    ChkFSubcategoria.Checked = false;
                    //ListBox y dropdownlist
                    CargaListBox("Categoria");
                    break;
                case "Subcategoria":
                    //Giro
                    DDLFGiro.Visible = true;
                    LBFFGiro.Visible = false;
                    //Categoria
                    DDLFCategoria.Visible = true;
                    LBFCategoria.Visible = false;
                    //Subcategoria
                    DDLFSubcategoria.Visible = false;
                    LBFSubcategoria.Visible = true;
                    //Checkbox
                    ChkFCategoria.Checked = false;
                    ChkFGiro.Checked = false;
                    ChkFSubcategoria.Checked = true;
                    //ListBox y dropdownlist
                    CargaListBox("Subcategoria");
                    break;
                default:
                    break;
            }
        }

        protected void DDLFGiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLFGiro.SelectedItem != null)
            {
                MVCategoria.BuscarCategorias(Estatus: "1", UidGiro: DDLFGiro.SelectedItem.Value);
                if (LBFCategoria.Visible)
                {
                    CargaListBox("Categoria");
                }
                else
                {
                    CargaDropDownList("Categoria");
                }
            }
        }

        protected void DDLFCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLFCategoria.SelectedItem != null)
            {
                MVSubcategoria.BuscarSubCategoria(UidCategoria: DDLFCategoria.SelectedItem.Value);
                if (LBFSubcategoria.Visible)
                {
                    CargaListBox("Subcategoria");
                }
                else
                {
                    CargaDropDownList("Subcategoria");
                }
            }
        }

        #region Panel mensaje de sistema
        protected void BtnCerrarPanelMensaje_Click(object sender, EventArgs e)
        {
            PanelMensaje.Visible = false;
        }
        #endregion
    }
}