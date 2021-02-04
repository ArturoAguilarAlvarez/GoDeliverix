using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;
using VistaDelModelo;
namespace WebApplication1.Vista
{
    public partial class ConfiguracionDeEmpresa : System.Web.UI.Page
    {
        VMImagen MVImagen;
        VMTelefono MVTelefono;
        VMEmpresas MVEmpresas;
        VMDireccion MVDireccion;
        VMCorreoElectronico MVCorreoElectronico;
        ImagenHelper oImagenHelper = new ImagenHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            FUImagen.Attributes["onchange"] = "UploadFile(this)";
            if (!IsPostBack)
            {
                if (Session["MVImagen"] != null)
                {
                    MVImagen = (VMImagen)Session["MVImagen"];
                }
                else
                {
                    Session["MVImagen"] = MVImagen = new VMImagen();
                }
                if (Session["MVTelefono"] != null)
                {
                    MVTelefono = (VMTelefono)Session["MVTelefono"];
                }
                else
                {
                    Session["MVTelefono"] = MVTelefono = new VMTelefono();
                }
                if (Session["MVEmpresas"] != null)
                {
                    MVEmpresas = (VMEmpresas)Session["MVEmpresas"];
                }
                else
                {
                    Session["MVEmpresas"] = MVEmpresas = new VMEmpresas();
                }
                if (Session["MVDireccion"] != null)
                {
                    MVDireccion = (VMDireccion)Session["MVDireccion"];
                }
                else
                {
                    Session["MVDireccion"] = MVDireccion = new VMDireccion();
                }
                if (Session["MVCorreoElectronico"] != null)
                {
                    MVCorreoElectronico = (VMCorreoElectronico)Session["MVCorreoElectronico"];
                }
                else
                {
                    Session["MVCorreoElectronico"] = MVCorreoElectronico = new VMCorreoElectronico();
                }
                string UidempresaSelecciona = Session["UidEmpresaSistema"].ToString();
                MuestraEmpresaEnGestion(UidempresaSelecciona);
                EstatusControles(false);
                PanelDatosDireccion.Visible = false;
                #region Panel derecho
                MVEmpresas.TipoDeEmpresa();
                MVEmpresas.Estatus();
                MVTelefono.TipoDeTelefonos();
                #region Paneles
                //Visibilidad de paneles
                pnlDatosGenerales.Visible = true;
                pnlDireccion.Visible = false;
                pnlContacto.Visible = false;
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

                //GridView telefonos
                DGVTELEFONOS.DataSource = null;
                DGVTELEFONOS.DataBind();
                //GridView direcciones
                GVDireccion.Enabled = false;
                #endregion
                #region DropdownList

                DDLDTipoDETelefono.DataSource = MVTelefono.ListaDeTipoDeTelefono;
                DDLDTipoDETelefono.DataValueField = "UidTipo";
                DDLDTipoDETelefono.DataTextField = "StrNombreTipoDeTelefono";
                DDLDTipoDETelefono.DataBind();
                //Obtiene el pais
                DDLDPais.DataSource = MVDireccion.Paises();
                DDLDPais.DataTextField = "Nombre";
                DDLDPais.DataValueField = "UidPais";
                DDLDPais.DataBind();
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

                //Panel de mensaje
                PanelMensaje.Visible = false;
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

            }
            else
            {
                MVImagen = (VMImagen)Session["MVImagen"];
                MVTelefono = (VMTelefono)Session["MVTelefono"];
                MVDireccion = (VMDireccion)Session["MVDireccion"];
                MVCorreoElectronico = (VMCorreoElectronico)Session["MVCorreoElectronico"];
                MVEmpresas = (VMEmpresas)Session["MVEmpresas"];

            }
        }
        protected void PanelGeneral(object sender, EventArgs e)
        {
            pnlDatosGenerales.Visible = true;
            pnlDireccion.Visible = false;
            pnlContacto.Visible = false;
            PanelMensaje.Visible = false;
            liDatosGenerales.Attributes.Add("class", "active");
            liDatosDireccion.Attributes.Add("class", " ");
            liDatosContacto.Attributes.Add("class", "");

        }
        protected void PanelDireccion(object sender, EventArgs e)
        {
            pnlDatosGenerales.Visible = false;
            pnlDireccion.Visible = true;
            pnlContacto.Visible = false;
            PanelMensaje.Visible = false;
            liDatosGenerales.Attributes.Add("class", "");
            liDatosDireccion.Attributes.Add("class", "active");
            liDatosContacto.Attributes.Add("class", "");

        }
        protected void PanelContacto(object sender, EventArgs e)
        {
            pnlDatosGenerales.Visible = false;
            pnlDireccion.Visible = false;
            pnlContacto.Visible = true;
            PanelMensaje.Visible = false;
            liDatosGenerales.Attributes.Add("class", "");
            liDatosDireccion.Attributes.Add("class", "");
            liDatosContacto.Attributes.Add("class", "active");

        }

        protected void EstatusControles(bool estatus)
        {
            txtDRazonSocial.Enabled = estatus;
            txtDNombreComercial.Enabled = estatus;
            txtDCorreoElectronico.Enabled = estatus;
            DGVTELEFONOS.Enabled = estatus;
            GVDireccion.Enabled = estatus;
            BtnCargarImagen.Enabled = estatus;
            btnCargarImagenDePortada.Enabled = estatus;
            btnNuevaDireccion.Enabled = estatus;
            btnNuevoTelefono.Enabled = estatus;
            CargaGrid("Telefono");
            CargaGrid("Direccion");

            if (estatus)
            {
                BtnCargarImagen.CssClass = "btn btn-sm btn-default ";
                btnCargarImagenDePortada.CssClass = "btn btn-sm btn-default ";
                btnNuevaDireccion.CssClass = "btn btn-sm btn-default ";
                btnNuevoTelefono.CssClass = "btn btn-sm btn-default ";
            }
            else
            {
                BtnCargarImagen.CssClass = "btn btn-sm btn-default disabled";
                btnCargarImagenDePortada.CssClass = "btn btn-sm btn-default disabled";
                btnNuevaDireccion.CssClass = "btn btn-sm btn-default disabled";
                btnNuevoTelefono.CssClass = "btn btn-sm btn-default disabled";
            }
        }


        #region Imagen
        #region Perfil
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
            if (FuImagenPortada.HasFile)
            {
                FileUpload FU = FuImagenPortada;
                if (MVImagen.ValidarExtencionImagen(Path.GetExtension(FU.FileName).ToLower()))
                {
                    GuardarImagenGiro:
                    //Valida si el directorio existe en el servidor
                    string RUTA = "Img/Empresa/Portada";
                    if (Directory.Exists(Server.MapPath(RUTA)))
                    {
                        //Crea el directorio de la empresa

                        CrearCarpetaDeEmpresa:
                        if (Directory.Exists(Server.MapPath(RUTA)))
                        {
                            CrearArchivoServidor:
                            //El archivo no existe en el servidor
                            if (!File.Exists(Server.MapPath(txtRutaImagenPortada.Text)))
                            {
                                string Nombre = Path.GetFileNameWithoutExtension(FU.FileName);
                                long Random = new Random().Next(999999999);
                                string RutaCompleta = RUTA + "/" + Random + Nombre + ".png";
                                txtRutaImagenPortada.Text = RutaCompleta;

                                //Valida si el archivo existe
                                if (!File.Exists(RutaCompleta))
                                {
                                    System.Drawing.Image img = oImagenHelper.RedimensionarImagen(System.Drawing.Image.FromStream(FU.FileContent));
                                    //Guarda la imagen en el servidor
                                    img.Save(Server.MapPath("~/Vista/" + RutaCompleta), ImageFormat.Png);

                                    //Guarda la ruta en una session par poder ser manipulada en las acciones de guardar y actualizar
                                    Session["RutaImagenPortada"] = RutaCompleta;

                                    //Numero Random para evitar el almacenamiento cache en el navegador
                                    string almacenamiento = RutaCompleta + "?" + (Random - 1);

                                    //Muestra la imagen en el control image de la vista
                                    imgPortada.ImageUrl = almacenamiento;
                                }
                                else
                                {
                                    lblEstado.Text = "Imagen existente en el sistema, favor de agregar otra.";
                                }
                            }
                            //Si el archivo existe lo elimina
                            else
                            {
                                File.Delete(Server.MapPath("~/Vista/" + txtRutaImagenPortada.Text));
                                txtRutaImagenPortada.Text = string.Empty;
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

        #endregion
        /// <summary>
        /// Carga un gridview 
        /// </summary>
        /// <param name="Grid">Direccion|Telefono</param>
        protected void CargaGrid(string Grid)
        {
            switch (Grid)
            {
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
        protected void ActivarEdicion(object sender, EventArgs e)
        {
            EstatusControles(true);
            PanelMensaje.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
        }
        protected void GuardarDatos(object sender, EventArgs e)
        {
            QuitarColorACamposObligatorios();
            if (txtDRazonSocial.Text != string.Empty && txtDNombreComercial.Text != string.Empty && txtDRfc.Text != string.Empty)
            {
                #region Variables
                //Datos generales
                string RS = txtDRazonSocial.Text;
                string NC = txtDNombreComercial.Text;
                string Rfc = txtDRfc.Text;


                string Referencia = txtDReferencia.Text;

                //Datos de contacto
                string Telefono = txtDTelefono.Text;
                string Correo = txtDCorreoElectronico.Text;

                //Variable de resultado
                bool resultado = false;


                #endregion

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
                    resultado = MVEmpresas.ActualizarDatos(UidEmpresa: UIDEMPRESA, RazonSocial: RS, NombreComercial: NC, Rfc: Rfc, TipoDeACtualizacion: "BackEnd");

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
                                MVImagen.EliminaImagenEmpresa(Ruta);
                                MVImagen.GuardaImagen(txtRutaImagen.Text, UIDEMPRESA.ToString(), "asp_InsertaImagenEmpresa");
                            }
                        }
                        //Elimina y guarda la imagen de portada
                        if (txtRutaImagenPortada.Text != string.Empty)
                        {
                            MVImagen.obtenerImagenDePortadaEmpresa(UIDEMPRESA.ToString());

                            string Ruta = MVImagen.STRRUTA;
                            //Evalua que la ruta de imagen sea diferente a la ruta recuperada para saber si actualiza o no
                            if (Ruta != txtRutaImagenPortada.Text)
                            {
                                if (File.Exists(Server.MapPath(Ruta)))
                                {
                                    File.Delete(Server.MapPath(Ruta));
                                }
                                if (!string.IsNullOrEmpty(Ruta))
                                {
                                    MVImagen.EliminaImagenPortadaEmpresa(Ruta);
                                }

                                MVImagen.GuardaImagen(txtRutaImagenPortada.Text, UIDEMPRESA.ToString(), "asp_InsertaImagenEmpresa");
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
                    #endregion
                }

                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                pnlDireccion.Visible = false;

                GVDireccion.SelectedIndex = -1;
                GVDireccion.Enabled = false;
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
            }

        }

        protected void CancelarAgregacion(object sender, EventArgs e)
        {
            QuitarColorACamposObligatorios();

            if (!string.IsNullOrEmpty(lblUidEmpresa.Text))
            {
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
                QuitarEstiloCamposGestion();
                btnEditar.CssClass = "btn btn-sm btn-default";
                btnEditar.Enabled = true;
                btnEditarTelefono.Enabled = false;
                btnEditarTelefono.CssClass = "btn btn-sm btn-default disabled";
                btnEdiarDireccion.CssClass = "btn btn-sm btn-default disabled";
                btnEdiarDireccion.Enabled = false;
                MuestraEmpresaEnGestion(lblUidEmpresa.Text);

                CargaGrid("Telefono");

                CargaGrid("Direccion");

                //Recargar el controlador con la imagen de la empresa
            }
            PanelDatosDireccion.Visible = false;


            Session.Remove("Accion");
            GVDireccion.SelectedIndex = -1;
            DGVTELEFONOS.SelectedIndex = -1;

        }

        private void MuestraEmpresaEnGestion(string UidempresaSelecciona)
        {
            MVEmpresas.ObtenerNombreComercial("", IdEmpresa: UidempresaSelecciona);
            PanelMensaje.Visible = false;
            Label lblNombreEmpresaSeleccionada = Master.FindControl("lblNombreDeEmpresa") as Label;
            TextBox txtUidEmpresaSeleccionadaSistema = Master.FindControl("txtUidEmpresaSistema") as TextBox;
            //Obtiene los botones de los modulos para que cuando se seleccione una empresa estos cambien dinamicamente acuerdo a la empresa seleccionada.

            //Obtiene el usuario que esta dentro del sistema
            string UidUsuarioEnSistema = (Master.FindControl("txtUidUsuarioSistema") as TextBox).Text;
            string UidEmpresa = (Master.FindControl("txtUidEmpresaSistema") as TextBox).Text;


            lblNombreEmpresaSeleccionada.Text = MVEmpresas.NOMBRECOMERCIAL;
            txtUidEmpresaSeleccionadaSistema.Text = MVEmpresas.UIDEMPRESA.ToString();


            //Obtiene lo datos de la empresa
            MVEmpresas.BuscarEmpresas(UidEmpresa: new Guid(UidempresaSelecciona));
            //Obtiene las direcciones asociadas a la empresa
            MVDireccion.ObtenerDireccionesEmpresa(UidempresaSelecciona);
            //Obtiene los tenefonos asociados en la empresa
            MVTelefono.ObtenerTelefonoEmpresa(UidempresaSelecciona, "gestion");
            //Obtiene la imagen de la empresa
            MVImagen.ObtenerImagenPerfilDeEmpresa(UidempresaSelecciona);

            //Obtiene el correo electronico
            MVCorreoElectronico.BuscarCorreos(UidPropietario: new Guid(UidempresaSelecciona), strParametroDebusqueda: "Empresa");

            //Datos del correo electronico
            txtDCorreoElectronico.Text = MVCorreoElectronico.CORREO;
            txtUidCorreoElectronico.Text = MVCorreoElectronico.ID.ToString();

            lblUidEmpresa.Text = MVEmpresas.UIDEMPRESA.ToString();
            txtDRazonSocial.Text = MVEmpresas.RAZONSOCIAL;
            txtDNombreComercial.Text = MVEmpresas.NOMBRECOMERCIAL;
            txtDRfc.Text = MVEmpresas.RFC;

            CargaGrid("Direccion");


            //Obtiene el nombre del tipo de teleofno
            foreach (var item in MVTelefono.ListaDeTelefonos)
            {
                item.StrNombreTipoDeTelefono = DDLDTipoDETelefono.Items.FindByValue(item.UidTipo.ToString()).Text;
            }

            txtRutaImagen.Text = MVImagen.STRRUTA;
            if (string.IsNullOrEmpty(txtRutaImagen.Text))
            {
                ImageEmpresa.ImageUrl = "Img/Default.jpg";
                ImageEmpresa.DataBind();
            }
            else
            {
                ImageEmpresa.ImageUrl = MVImagen.STRRUTA;
                ImageEmpresa.DataBind();
            }


            MVImagen.obtenerImagenDePortadaEmpresa(UidempresaSelecciona);

            txtRutaImagenPortada.Text = MVImagen.STRRUTA.ToString();
            if (string.IsNullOrEmpty(txtRutaImagenPortada.Text))
            {
                imgPortada.ImageUrl = "Img/Default.jpg";
                imgPortada.DataBind();
            }
            else
            {
                imgPortada.ImageUrl = MVImagen.STRRUTA;
                imgPortada.DataBind();
            }

            //Carga el gridview de los telefonos
            CargaGrid("Telefono");


            GVDireccion.SelectedIndex = -1;
            DGVTELEFONOS.SelectedIndex = -1;

            lblEstado.Visible = false;
        }


        protected void QuitarColorACamposObligatorios()
        {
            txtDRazonSocial.BorderColor = System.Drawing.Color.White;
            txtDNombreComercial.BorderColor = System.Drawing.Color.White;
            txtDRfc.BorderColor = System.Drawing.Color.White;
        }
        protected void QuitarEstiloCamposGestion()
        {
            //Quitar estilo 
            txtDRazonSocial.Style.Add("background-color", "");
            txtDNombreComercial.Style.Add("background-color", "");
            txtDRfc.Style.Add("background-color", "");
        }
        protected void BtnCerrarPanelMensaje_Click(object sender, EventArgs e)
        {
            PanelMensaje.Visible = false;
        }
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
        }
        protected void ObtenerMunicipio(object sender, EventArgs e)
        {
            if (DDLDEstado.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                MuestraMunicipio(DDLDEstado.SelectedItem.Value.ToString(), "Gestion");
                MuestraCiudad("00000000-0000-0000-0000-000000000000", "Gestion");
                MuestraColonia("00000000-0000-0000-0000-000000000000", "Gestion");
            }
        }
        protected void ObtenerCiudad(object sender, EventArgs e)
        {
            if (DDLDMunicipio.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                MuestraCiudad(DDLDMunicipio.SelectedItem.Value.ToString(), "Gestion");
                MuestraColonia("00000000-0000-0000-0000-000000000000", "Gestion");

            }
        }
        protected void ObtenerColonia(object sender, EventArgs e)
        {
            if (DDLDMunicipio.SelectedItem.Value.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                MuestraColonia(DDLDCiudad.SelectedItem.Value.ToString(), "Gestion");
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
            PanelDatosDireccion.Visible = true;
            PanelInformacionDeEmpresa.Visible = false;
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
            PanelDatosDireccion.Visible = false;
            PanelInformacionDeEmpresa.Visible = true;
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
            PanelInformacionDeEmpresa.Visible = true;
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

            string valor = DGVTELEFONOS.SelectedDataKey.Value.ToString();

            MVTelefono.ObtenTelefono(valor);
            btnEditarTelefono.Enabled = true;
            btnEditarTelefono.CssClass = "btn btn-sm btn-default";

            txtIdTelefono.Text = MVTelefono.ID.ToString();
            DDLDTipoDETelefono.SelectedIndex = DDLDTipoDETelefono.Items.IndexOf(DDLDTipoDETelefono.Items.FindByValue(MVTelefono.UidTipo.ToString()));
            txtDTelefono.Text = MVTelefono.NUMERO;

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

    }
}