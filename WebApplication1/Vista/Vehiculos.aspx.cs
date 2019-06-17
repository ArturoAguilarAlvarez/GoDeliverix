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
    public partial class Vehiculos : System.Web.UI.Page
    {

        string AccionesDeLaPagina = "";
        VMVehiculo MVVehiculo = new VMVehiculo();
        VMSucursales MVSucursales;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Panel derecho
                //Instancia de la clase
                MVSucursales = new VMSucursales();
                AccionesDeLaPagina = string.Empty;
                EstatusDeControlPorAccion(false);
                Session["MVSucursales"] = MVSucursales;
                MVVehiculo.BuscarVehiculo(UidEmpresa: new Guid(Session["UidEmpresaSistema"].ToString()));
                CargaGrid("Vehiculos");
                MuestraPanel("General");
                //Dropdownlist tipo devehiculo de gestion
                DDLGTipoDeVehiculo.DataSource = MVVehiculo.ObtenerTipoDeVehiculo();
                DDLGTipoDeVehiculo.DataTextField = "VchNombre";
                DDLGTipoDeVehiculo.DataValueField = "UidTipoDevehiculo";
                DDLGTipoDeVehiculo.DataBind();
                #endregion

                #region Panel izquierdo
                PanelFiltro.Visible = false;
                //Dropdownlist tipo devehiculo filtro de busqueda
                DDLFTipoDeVehiculo.DataSource = MVVehiculo.ObtenerTipoDeVehiculoFitros();
                DDLFTipoDeVehiculo.DataTextField = "VchNombre";
                DDLFTipoDeVehiculo.DataValueField = "UidTipoDevehiculo";
                DDLFTipoDeVehiculo.DataBind();
                #endregion
            }
            else
            {
                MVSucursales = (VMSucursales)Session["MVSucursales"];
            }
        }


        #region Panel derecho
        protected void HabilitarControlesGestion(bool ESTATUS)
        {//Cajas de texto
            txtGAnio.Enabled = ESTATUS;
            txtGCilindrada.Enabled = ESTATUS;
            txtGColor.Enabled = ESTATUS;
            txtGMarca.Enabled = ESTATUS;
            txtGModelo.Enabled = ESTATUS;
            txtGNoSerie.Enabled = ESTATUS;
            txtGPlaca.Enabled = ESTATUS;
            //DropdownList
            DDLGTipoDeVehiculo.Enabled = ESTATUS;
            //Botones
            btnGImagen.Enabled = ESTATUS;
            btnBuscarEmpresa.Enabled = ESTATUS;
            DGVBUSQUEDADEEMPRESA.Enabled = ESTATUS;
            btnCambiarEmpresa.Enabled = ESTATUS;
            if (ESTATUS)
            {
                btnGImagen.CssClass = "btn btn-sm btn-default ";
                btnBuscarEmpresa.CssClass = "btn btn-sm btn-default ";
            }
            else
            {
                btnGImagen.CssClass = "btn btn-sm btn-default disabled";
                btnBuscarEmpresa.CssClass = "btn btn-sm btn-default disabled";
                btnCambiarEmpresa.CssClass = "btn btn-sm btn-default disabled";
            }
        }
        protected void LimpiaControlesDeGestion()
        {
            //Cajas de texto
            txtGAnio.Text = string.Empty;
            txtGCilindrada.Text = string.Empty;
            txtGColor.Text = string.Empty;
            txtGMarca.Text = string.Empty;
            txtGModelo.Text = string.Empty;
            txtGNoSerie.Text = string.Empty;
            txtGPlaca.Text = string.Empty;
            //DropdownList
            DDLGTipoDeVehiculo.SelectedIndex = -1;

        }
        protected void EstatusDeControlPorAccion(bool Accion = false)
        {
            if (AccionesDeLaPagina == "Edicion" && !Accion)
            {
                HabilitarControlesGestion(false);
                btnEditar.Enabled = true;
                btnNuevo.Enabled = true;

                btnEditar.CssClass = "btn btn-sm btn-default";
                btnNuevo.CssClass = "btn btn-sm btn-default";

                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
            }
            else
            if (AccionesDeLaPagina == "Edicion" && Accion)
            {
                HabilitarControlesGestion(true);

                btnEditar.Enabled = true;
                btnNuevo.Enabled = false;

                btnEditar.CssClass = "btn btn-sm btn-default ";
                btnNuevo.CssClass = "btn btn-sm btn-default disabled";

                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
            }
            else
            if (AccionesDeLaPagina == "Nuevo")
            {
                dgvVehiculos.SelectedIndex = -1;
                HabilitarControlesGestion(true);
                LimpiaControlesDeGestion();

                btnEditar.Enabled = false;
                btnNuevo.Enabled = true;

                btnEditar.CssClass = "btn btn-sm btn-default disabled";
                btnNuevo.CssClass = "btn btn-sm btn-default";

                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
            }
            else
            {
                dgvVehiculos.SelectedIndex = -1;
                HabilitarControlesGestion(false);
                LimpiaControlesDeGestion();

                btnEditar.Enabled = false;
                btnNuevo.Enabled = true;

                btnEditar.CssClass = "btn btn-sm btn-default disabled";
                btnNuevo.CssClass = "btn btn-sm btn-default";

                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
            }
        }
        protected void CargaGrid(string Grid)
        {
            switch (Grid)
            {
                case "Vehiculos":
                    dgvVehiculos.DataSource = MVVehiculo.ListaDeVehiculos;
                    dgvVehiculos.DataBind();
                    break;
                default:
                    break;
            }
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            AccionesDeLaPagina = "Nuevo";
            EstatusDeControlPorAccion();
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            AccionesDeLaPagina = "Edicion";
            EstatusDeControlPorAccion(true);
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            if (dgvVehiculos.SelectedIndex != -1)
            {
                AccionesDeLaPagina = "Edicion";
                EstatusDeControlPorAccion(false);
            }
            else
            {
                EstatusDeControlPorAccion();
            }

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            QuitarCamposObligatorios();
            if (ValidacionDeControlesRequeridos())
            {
                string anio = txtGAnio.Text;
                string no_serie = txtGNoSerie.Text;
                string cilindrada = txtGCilindrada.Text;
                string marca = txtGMarca.Text;
                string modelo = txtGModelo.Text;
                string color = txtGColor.Text;
                string placa = txtGPlaca.Text;
                Guid tipodevehiculo = new Guid(DDLGTipoDeVehiculo.SelectedItem.Value);
                Guid Uid = new Guid();
                Guid UidEmpresa = new Guid(Session["UidEmpresaSistema"].ToString());
                Guid UidSucursal = new Guid(txtUidSucursal.Text);
                if (string.IsNullOrEmpty(txtUidVehiculo.Text))
                {
                    Uid = Guid.NewGuid();
                    if (MVVehiculo.GuardarVehiculo(Uid, UidEmpresa, no_serie, cilindrada, marca, modelo, color, anio, placa, tipodevehiculo))
                    {
                        MVVehiculo.RelacionConSucursal(Uid, UidSucursal);
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Registro agregado!";
                    }
                    else
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Registro no agregado!";
                    }

                    EstatusDeControlPorAccion();
                }
                else
                {
                    Uid = new Guid(txtUidVehiculo.Text);
                    if (MVVehiculo.ActualizaVehiculo(Uid, no_serie, cilindrada, marca, modelo, color, anio, placa, tipodevehiculo))
                    {
                        MVVehiculo.EliminarRelacionSucursal(Uid);
                        MVVehiculo.RelacionConSucursal(Uid, UidSucursal);

                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Registro actualizado!";
                    }
                    else
                    {
                        PanelMensaje.Visible = true;
                        LblMensaje.Text = "Registro no actualizado!";
                    }
                    AccionesDeLaPagina = "Edicion";
                    EstatusDeControlPorAccion(false);
                }
                MVVehiculo.BuscarVehiculo(UidEmpresa: UidEmpresa);
                CargaGrid("Vehiculos");
            }
        }
        protected bool ValidacionDeControlesRequeridos()
        {
            bool Resultado = true;
            if (string.IsNullOrEmpty(txtGAnio.Text))
            {
                txtGAnio.BorderColor = Color.Red;
                Resultado = false;
            }
            if (string.IsNullOrEmpty(txtGCilindrada.Text))
            {
                txtGCilindrada.BorderColor = Color.Red;
                Resultado = false;
            }
            if (string.IsNullOrEmpty(txtGColor.Text))
            {
                txtGColor.BorderColor = Color.Red;
                Resultado = false;
            }
            if (string.IsNullOrEmpty(txtGMarca.Text))
            {
                txtGMarca.BorderColor = Color.Red;
                Resultado = false;
            }
            if (string.IsNullOrEmpty(txtGModelo.Text))
            {
                txtGModelo.BorderColor = Color.Red;
                Resultado = false;
            }
            if (string.IsNullOrEmpty(txtGNoSerie.Text))
            {
                txtGNoSerie.BorderColor = Color.Red;
                Resultado = false;
            }
            if (string.IsNullOrEmpty(txtGPlaca.Text))
            {
                txtGPlaca.BorderColor = Color.Red;
                Resultado = false;
            }
            if (DDLGTipoDeVehiculo.SelectedItem.Value == Guid.Empty.ToString())
            {
                DDLGTipoDeVehiculo.BorderColor = Color.Red;
                Resultado = false;
            }
            return Resultado;
        }
        protected void QuitarCamposObligatorios()
        {
            txtGAnio.BorderColor = Color.Empty;
            txtGCilindrada.BorderColor = Color.Empty;
            txtGColor.BorderColor = Color.Empty;
            txtGMarca.BorderColor = Color.Empty;
            txtGModelo.BorderColor = Color.Empty;
            txtGNoSerie.BorderColor = Color.Empty;
            txtGPlaca.BorderColor = Color.Empty;
            DDLGTipoDeVehiculo.BorderColor = Color.Empty;
        }
        #endregion

        #region Panel Izquierdo
        protected void BtnBAOcultar_Click(object sender, EventArgs e)
        {
            if (PanelFiltro.Visible)
            {
                PanelFiltro.Visible = false;
                lblBAFiltrosVisibilidad.Text = " Mostrar";
                IconoBotonMostrar.CssClass = "glyphicon glyphicon-eye-open";
            }
            else
            {
                PanelFiltro.Visible = true;
                lblBAFiltrosVisibilidad.Text = " Ocultar";
                IconoBotonMostrar.CssClass = "glyphicon glyphicon-eye-close";
            }
        }
        #endregion

        protected void BtnBALimpiar_Click(object sender, EventArgs e)
        {
            txtFAnio.Text = string.Empty;
            txtFCilindrada.Text = string.Empty;
            txtFColor.Text = string.Empty;
            txtFMarca.Text = string.Empty;
            txtFModelo.Text = string.Empty;
            txtFNo_serie.Text = string.Empty;
            txtFPlaca.Text = string.Empty;

            DDLFTipoDeVehiculo.SelectedIndex = -1;
        }

        protected void BtnBABuscar_Click(object sender, EventArgs e)
        {
            MVVehiculo.BuscarVehiculo(new Guid(), new Guid(Session["IDEMPRESA"].ToString()), txtFNo_serie.Text, txtFCilindrada.Text, txtFMarca.Text, txtFModelo.Text, txtFColor.Text, txtFAnio.Text, txtFPlaca.Text, new Guid(DDLFTipoDeVehiculo.SelectedItem.Value));
            CargaGrid("Vehiculos");
            dgvVehiculos.SelectedIndex = -1;
        }

        protected void dgvVehiculos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvVehiculos, "Select$" + e.Row.RowIndex);
            }
        }

        protected void dgvVehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid Uid = new Guid(dgvVehiculos.SelectedDataKey.Value.ToString());
            MVVehiculo.BuscarVehiculo(UidVehiculo: Uid);
            txtUidVehiculo.Text = MVVehiculo.UID.ToString();
            txtGAnio.Text = MVVehiculo.StrAnio;
            txtGCilindrada.Text = MVVehiculo.IntCilindrada.ToString();
            txtGColor.Text = MVVehiculo.StrColor;
            txtGMarca.Text = MVVehiculo.StrMarca;
            txtGModelo.Text = MVVehiculo.StrModelo;
            txtGNoSerie.Text = MVVehiculo.LngNumeroDeSerie;
            txtGPlaca.Text = MVVehiculo.StrNoDePLaca;
            DDLGTipoDeVehiculo.SelectedIndex = DDLGTipoDeVehiculo.Items.IndexOf(DDLGTipoDeVehiculo.Items.FindByValue(MVVehiculo.UidTipoDeVehiculo.ToString()));


            //Campos de la sucursal asociada
            MVSucursales.ObtenerSucursal(MVSucursales.ObtenerUidSucursalVehiculo(Uid));

            txtUidSucursal.Text = MVSucursales.SUCURSAL.ID.ToString();
            txtdidentificador.Text = MVSucursales.SUCURSAL.IDENTIFICADOR;
            txtdHoraApertura.Text = MVSucursales.SUCURSAL.HORAAPARTURA;
            txtdHoraDeCierre.Text = MVSucursales.SUCURSAL.HORACIERRE;

            AccionesDeLaPagina = "Edicion";
            EstatusDeControlPorAccion(false);
        }

        protected void btnDatosGenerales_Click(object sender, EventArgs e)
        {
            MuestraPanel("General");
        }

        private void MuestraPanel(string panel)
        {
            if (panel == "General")
            {
                liDatosGenerales.Attributes.Add("class", "active");
                panelDatosGenerales.Visible = true;
            }
            else
            {
                liDatosGenerales.Attributes.Add("class", "");
                panelDatosGenerales.Visible = false;
            }
            if (panel == "Sucursal")
            {
                liDatosDeEmpresa.Attributes.Add("class", "active");
                panelDatosEmpresa.Visible = true;
            }
            else
            {
                liDatosDeEmpresa.Attributes.Add("class", "");
                panelDatosEmpresa.Visible = false;
            }
        }

        #region Panel de busqueda de Sucursales
        protected void BuscaEmpresa(object sender, EventArgs e)
        {
            string Identificador = txtdidentificador.Text;
            string HA = txtdHoraApertura.Text;
            string HC = txtdHoraDeCierre.Text;

            if (Identificador == string.Empty && HA == string.Empty && HC == string.Empty)
            {
                MVSucursales.BuscarSucursales(Uidempresa: Session["UidEmpresaSistema"].ToString());
                DGVBUSQUEDADEEMPRESA.DataSource = MVSucursales.LISTADESUCURSALES;
                DGVBUSQUEDADEEMPRESA.DataBind();
            }
            else
            {
                MVSucursales.BuscarSucursales(Identificador, HA, HC, Uidempresa: Session["UidEmpresaSistema"].ToString());
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
            MVSucursales.ObtenerSucursal(valor);

            txtUidSucursal.Text = MVSucursales.SUCURSAL.ID.ToString();
            txtdidentificador.Text = MVSucursales.SUCURSAL.IDENTIFICADOR;
            txtdHoraApertura.Text = MVSucursales.SUCURSAL.HORAAPARTURA;
            txtdHoraDeCierre.Text = MVSucursales.SUCURSAL.HORACIERRE;

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

        protected void btnDatosSucursal_Click(object sender, EventArgs e)
        {
            MuestraPanel("Sucursal");
        }
        #region Panel de mensaje
        protected void BtnCerrarPanelMensaje_Click(object sender, EventArgs e)
        {
            PanelMensaje.Visible = false;
        }
        #endregion
    }
}