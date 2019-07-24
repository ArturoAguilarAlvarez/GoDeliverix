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
    public partial class Configuracion : System.Web.UI.Page
    {
        VMDireccion MVDireccion = new VMDireccion();
        VMUsuarios MVUsuario = new VMUsuarios();
        VMTelefono MVTelefono = new VMTelefono();
        VMCorreoElectronico MVCorreoElectronico = new VMCorreoElectronico();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] != null)
            {
                if (!IsPostBack)
                {
                    Session["MVDireccion"] = MVDireccion;
                    Session["MVUsuario"] = MVUsuario;
                    Session["MVTelefono"] = MVTelefono;
                    Session["MVCorreoElectronico"] = MVCorreoElectronico;
                    MuestraPanel("General");

                    DDLDPais.DataSource = MVDireccion.Paises();
                    DDLDPais.DataValueField = "UidPais";
                    DDLDPais.DataTextField = "Nombre";
                    DDLDPais.DataBind();

                    MVTelefono.TipoDeTelefonos();
                    DDLDTipoDETelefono.DataSource = MVTelefono.ListaDeTipoDeTelefono;
                    DDLDTipoDETelefono.DataValueField = "UidTipo";
                    DDLDTipoDETelefono.DataTextField = "StrNombreTipoDeTelefono";
                    DDLDTipoDETelefono.DataBind();

                    EstatusPanelDatosGenerales(false);
                    EstatusControlesTelefono(false);
                    Cargausuario(Session["IdUsuario"].ToString());

                    EstatusDeCamposDeDireccion(false);
                    DgvDirecciones.Enabled = true;
                    PanelMensaje.Visible = false;
                }
                else
                {
                    MVDireccion = (VMDireccion)Session["MVDireccion"];
                    MVUsuario = (VMUsuarios)Session["MVUsuario"];
                    MVTelefono = (VMTelefono)Session["MVTelefono"];
                    MVCorreoElectronico = (VMCorreoElectronico)Session["MVCorreoElectronico"];
                }
            }
            else
            {
                Response.Redirect("../Default/");
            }

        }

        #region Panel de direccion

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
                default:
                    break;
            }
        }
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
                default:
                    break;
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
        protected void LimpiarCamposDeDireccion()
        {
            //Datos de direccion
            txtCalle0.Text = string.Empty;
            txtCalle1.Text = string.Empty;
            txtCalle2.Text = string.Empty;
            txtDCodigoPostal.Text = string.Empty;

            txtIdentificadorDeDireccion.Text = string.Empty;
            txtDReferencia.Text = string.Empty;
            txtDLote.Text = string.Empty;
            txtDManzana.Text = string.Empty;



            DDLDPais.SelectedIndex = -1;
            DDLDMunicipio.SelectedIndex = -1;
            DDLDEstado.SelectedIndex = -1;
            DDLDColonia.SelectedIndex = -1;
            DDLDCiudad.SelectedIndex = -1;

        }

        protected void EstatusDeCamposDeDireccion(bool estatus)
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

            PnlDetallesDireccion.Visible = true;
        }

        protected void ActivaEdicionDeDireccion(object sender, EventArgs e)
        {
            EstatusDeCamposDeDireccion(true);
        }

        protected void btnCancelarDireccion_Click(object sender, EventArgs e)
        {
            EstatusDeCamposDeDireccion(false);
            PnlDetallesDireccion.Visible = false;
            LimpiarCamposDeDireccion();

            MVDireccion.ObtenerDireccionesUsuario(Session["IdUsuario"].ToString());
            DgvDirecciones.EditIndex = -1;
            CargaGrid("Direccion");

        }

        protected void DgvDirecciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DgvDirecciones, "Select$" + e.Row.RowIndex);
                var icono = e.Row.FindControl("lblEliminarDireccion") as Label;
                var botonEliminar = e.Row.FindControl("EliminaDireccion") as LinkButton;
                icono.CssClass = "glyphicon glyphicon-trash";

                if (DgvDirecciones.SelectedIndex == e.Row.RowIndex && MVDireccion.ListaDIRECCIONES.Count != 1)
                {
                    botonEliminar.Enabled = true;
                    botonEliminar.CssClass = "btn btn-sm btn-default ";
                }
                if (PnlDetallesDireccion.Visible)
                {
                    DgvDirecciones.Enabled = false;
                }
                else
                {
                    DgvDirecciones.Enabled = true;
                }
            }
        }

        protected void DgvDirecciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string UidDireccion = DgvDirecciones.DataKeys[DgvDirecciones.SelectedRow.RowIndex].Value.ToString();
            MVDireccion.QuitaDireeccionDeLista(UidDireccion);
            MVDireccion.EliminaDireccionUsuario(UidDireccion);
            LimpiarCamposDeDireccion();

            MVDireccion.GuardaListaDeDirecciones(MVDireccion.ListaDIRECCIONES, new Guid(Session["IdUsuario"].ToString()), "asp_AgregaDireccionUsuario", "Usuario");
            MVDireccion.ObtenerDireccionesUsuario(Session["IdUsuario"].ToString());
            CargaGrid("Direccion");
        }

        protected void DgvDirecciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            DgvDirecciones.EditIndex = -1;
            CargaGrid("Direccion");
        }

        protected void btnEdiarDireccion_Click(object sender, EventArgs e)
        {
            EstatusDeCamposDeDireccion(true);
        }

        protected void BtnAgregarDireccion_Click(object sender, EventArgs e)
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
                MVDireccion.AgregaDireccionALista(UidDireccion,UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, txtCalle0.Text, txtCalle1.Text, txtCalle2.Text, txtDManzana.Text, txtDLote.Text, txtDCodigoPostal.Text, txtDReferencia.Text, NOMBRECIUDAD, NOMBRECOLONIA, txtIdentificadorDeDireccion.Text);
            }

            MVDireccion.GuardaListaDeDirecciones(MVDireccion.ListaDIRECCIONES, new Guid(Session["IdUsuario"].ToString()), "asp_AgregaDireccionUsuario", "Usuario");
            MVDireccion.ObtenerDireccionesUsuario(Session["IdUsuario"].ToString());
            CargaGrid("Direccion");

            DgvDirecciones.SelectedIndex = -1;
            PnlDetallesDireccion.Visible = false;
        }

        protected void DgvDirecciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                DgvDirecciones.SelectedIndex = index;

            }
            if (e.CommandName == "Delete")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                DgvDirecciones.SelectedIndex = index;
            }
        }

        protected void DgvDirecciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = DgvDirecciones.SelectedIndex;
            string valor = DgvDirecciones.DataKeys[index].Value.ToString();

            Session["MVDireccion"] = MVDireccion;
            MVDireccion.ObtenDireccion(valor);
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
            //Habilita los controles al momento de mostrar el panel
            EstatusDeCamposDeDireccion(true);
            PnlDetallesDireccion.Visible = true;
        }

        #endregion
        protected void CargaGrid(string GridView)
        {
            switch (GridView)
            {
                case "Telefono":

                    DGVTELEFONOS.DataSource = MVTelefono.ListaDeTelefonos;
                    DGVTELEFONOS.DataBind();
                    break;
                case "Direccion":
                    DgvDirecciones.DataSource = MVDireccion.ListaDIRECCIONES;
                    DgvDirecciones.DataBind();
                    break;
                default:
                    break;
            }
        }

        protected void btnDatosGenerales_Click(object sender, EventArgs e)
        {
            MuestraPanel("General");
        }

        protected void btnDatosDireccion_Click(object sender, EventArgs e)
        {
            MuestraPanel("Direccion");
        }

        protected void btnDatosDeConectado_Click(object sender, EventArgs e)
        {
            MuestraPanel("Contacto");
        }

        protected void MuestraPanel(string Panel)
        {
            if (Panel == "General")
            {
                liDatosGenerales.Attributes.Add("class", "active");
                pnlDatosGenerales.Visible = true;
            }
            else
            {
                liDatosGenerales.Attributes.Add("class", "");
                pnlDatosGenerales.Visible = false;
            }
            if (Panel == "Direccion")
            {
                liDatosDireccion.Attributes.Add("class", "active");
                PnlDirecciones.Visible = true;
                DgvDirecciones.EditIndex = -1;
                DgvDirecciones.SelectedIndex = -1;
                CargaGrid("Direccion");
            }
            else
            {
                liDatosDireccion.Attributes.Add("class", "");
                PnlDirecciones.Visible = false;
            }
            if (Panel == "Contacto")
            {
                liDatosContacto.Attributes.Add("class", "active");
                pnlContacto.Visible = true;
            }
            else
            {
                liDatosContacto.Attributes.Add("class", "");
                pnlContacto.Visible = false;
            }
            PnlDetallesDireccion.Visible = false;

        }


        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            EstatusDeCamposDeDireccion(true);
            LimpiarCamposDeDireccion();
            PnlDetallesDireccion.Visible = true;
            DgvDirecciones.EditIndex = -1;
            DgvDirecciones.SelectedIndex = -1;
            CargaGrid("Direccion");
        }


        #region Telefonos
        protected void EstatusControlesTelefono(bool estatus)
        {
            txtDTelefono.Enabled = estatus;
            DDLDTipoDETelefono.Enabled = estatus;
            btnGuardarTelefono.Visible = estatus;
            btnCancelarTelefono.Visible = estatus;
        }
        protected void NuevoTelefono(object sender, EventArgs e)
        {
            EstatusControlesTelefono(true);
            btnEditarTelefono.CssClass = "btn btn-sm btn-default disabled";
            txtDTelefono.Text = string.Empty;
            DDLDTipoDETelefono.SelectedIndex = -1;
            IconActualizaTelefono.CssClass = "glyphicon glyphicon-ok";
        }

        protected void CancelarTelefono(object sender, EventArgs e)
        {
            EstatusControlesTelefono(false);
            if (string.IsNullOrEmpty(txtIdTelefono.Text))
            {
                txtDTelefono.Text = string.Empty;
                DDLDTipoDETelefono.SelectedIndex = -1;
            }
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
            DDLDTipoDETelefono.SelectedIndex = -1;
            txtDTelefono.Text = string.Empty;
            DDLDTipoDETelefono.Enabled = false;
            txtDTelefono.Enabled = false;
            btnGuardarTelefono.Visible = false;
            btnCancelarTelefono.Visible = false;
            CargaGrid("Telefono");
        }

        protected void GuardaTelefono()
        {
            if (string.IsNullOrEmpty(txtIdTelefono.Text))
            {
                MVTelefono.AgregaTelefonoALista( DDLDTipoDETelefono.SelectedItem.Value.ToString(), txtDTelefono.Text, DDLDTipoDETelefono.SelectedItem.Text.ToString());
            }
            else
            {
                MVTelefono.ActualizaRegistroEnListaDeTelefonos(txtIdTelefono.Text,  DDLDTipoDETelefono.SelectedItem.Value.ToString(), txtDTelefono.Text);
            }
            MVTelefono.EliminaTelefonosUsuario(new Guid(Session["IdUsuario"].ToString()));
            //Guarda los telefonos
            if (MVTelefono.ListaDeTelefonos != null)
            {
                if (MVTelefono.ListaDeTelefonos.Count != 0)
                {
                    MVTelefono.GuardaTelefono(new Guid(Session["IdUsuario"].ToString()), "Usuario");
                }
            }
        }
        protected void ActualizaTelefono()
        {
            MVTelefono.ActualizaRegistroEnListaDeTelefonos(txtIdTelefono.Text, DDLDTipoDETelefono.SelectedItem.Value.ToString(),  txtDTelefono.Text);
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
                MVTelefono.EliminaTelefonoUsuario(ID);
                MVTelefono.BuscarTelefonos(UidPropietario: new Guid(Session["IdUsuario"].ToString()), ParadetroDeBusqueda: "Usuario");

                CargaGrid("Telefono");

            }
        }
        protected void DGVTELEFONOS_SelectedIndexChanged(object sender, EventArgs e)
        {

            string valor = DGVTELEFONOS.SelectedDataKey.Value.ToString();
            Session["MVUsuario"] = MVUsuario;
            MVTelefono.ObtenTelefono(valor);
            btnEditarTelefono.Enabled = true;
            btnEditarTelefono.CssClass = "btn btn-sm btn-default";

            txtIdTelefono.Text = MVTelefono.ID.ToString();
            DDLDTipoDETelefono.SelectedIndex = DDLDTipoDETelefono.Items.IndexOf(DDLDTipoDETelefono.Items.FindByValue(MVTelefono.UidTipo.ToString()));
            txtDTelefono.Text = MVTelefono.NUMERO;

        }
        protected void EditaTelefono(object sender, EventArgs e)
        {
            EstatusControlesTelefono(true);
            IconActualizaTelefono.CssClass = "glyphicon glyphicon-refresh";
        }
        #endregion

        protected void btnEditarDatosGenerales_Click(object sender, EventArgs e)
        {
            EstatusPanelDatosGenerales(true);
        }

        protected void EstatusPanelDatosGenerales(bool estatus)
        {
            txtDNombre.Enabled = estatus;
            txtdContrasena.Enabled = estatus;
            txtDUsuario.Enabled = estatus;
            txtDApellidoMaterno.Enabled = estatus;
            txtDApellidoPaterno.Enabled = estatus;
            txtDFechaDeNacimiento.Enabled = estatus;
            txtDCorreoElectronico.Enabled = estatus;
            BtnGuardarDatosGenerales.Visible = estatus;
            BtnCancelarDatosGenerales.Visible = estatus;
        }

        protected void BtnGuardarDatosGenerales_Click(object sender, EventArgs e)
        {
            MVUsuario.ActualizarUsuario(UidUsuario: new Guid(Session["IdUsuario"].ToString()), Nombre: txtDNombre.Text, ApellidoPaterno: txtDApellidoPaterno.Text, ApellidoMaterno: txtDApellidoMaterno.Text, usuario: txtDUsuario.Text, password: txtdContrasena.Text, fnacimiento: txtDFechaDeNacimiento.Text, perfil: "4F1E1C4B-3253-4225-9E46-DD7D1940DA19");
            MVCorreoElectronico.EliminaCorreoUsuario(Session["IdUsuario"].ToString());
            MVCorreoElectronico.AgregarCorreo(new Guid(Session["IdUsuario"].ToString()), "Usuario", txtDCorreoElectronico.Text, Guid.NewGuid());
            Cargausuario(Session["IdUsuario"].ToString());
            EstatusPanelDatosGenerales(false);
        }

        protected void BtnCancelarDatosGenerales_Click(object sender, EventArgs e)
        {
            Cargausuario(Session["IdUsuario"].ToString());
            EstatusPanelDatosGenerales(false);
        }

        protected void Cargausuario(string Uidusuario)
        {
            //Obtener la informacion del usuario
            MVDireccion.ObtenerDireccionesUsuario(Uidusuario);
            MVUsuario.obtenerUsuario(Uidusuario);
            MVTelefono.BuscarTelefonos(UidPropietario: new Guid(Uidusuario), ParadetroDeBusqueda: "Usuario");
            MVCorreoElectronico.BuscarCorreos(UidPropietario: new Guid(Uidusuario), strParametroDebusqueda: "Usuario");
            //Manda los datos generales del usuario.
            txtDNombre.Text = MVUsuario.StrNombre;
            txtDApellidoMaterno.Text = MVUsuario.StrApellidoMaterno;
            txtDApellidoPaterno.Text = MVUsuario.StrApellidoPaterno;
            txtDUsuario.Text = MVUsuario.StrUsuario;
            txtdContrasena.Text = MVUsuario.StrCotrasena;
            DateTime Fecha = new DateTime();
            if (string.IsNullOrEmpty(MVUsuario.DtmFechaDeNacimiento))
            {
                Fecha = DateTime.Now;
            }
            else
            {
                Fecha = DateTime.Parse(MVUsuario.DtmFechaDeNacimiento);
            }
             
            txtDFechaDeNacimiento.Text = Fecha.ToString("yyyy-MM-dd");
            txtDCorreoElectronico.Text = MVCorreoElectronico.CORREO;

            //Obtiene el nombre del tipo de teleofno
            foreach (var item in MVTelefono.ListaDeTelefonos)
            {
                item.StrNombreTipoDeTelefono = DDLDTipoDETelefono.Items.FindByValue(item.UidTipo.ToString()).Text;
            }
            //Carga los gridview
            CargaGrid("Direccion");
            CargaGrid("Telefono");
        }

        protected void txtDUsuario_TextChanged(object sender, EventArgs e)
        {
            MVUsuario.BusquedaDeUsuario(UidUsuario: new Guid(Session["IdUsuario"].ToString()));

            MVUsuario.BusquedaDeUsuario(USER: txtDUsuario.Text);
            if (MVUsuario.LISTADEUSUARIOS.Count > 0)
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

        protected void BtnCerrarPanelMensaje_Click(object sender, EventArgs e)
        {
            PanelMensaje.Visible = false;
        }
    }
}