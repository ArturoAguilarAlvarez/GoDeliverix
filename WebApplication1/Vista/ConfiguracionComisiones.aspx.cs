using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;

namespace WebApplication1.Vista
{
    public partial class ConfiguracionComisiones : System.Web.UI.Page
    {
        VMComision MVComision = new VMComision();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["MVComision"] = MVComision;
                CargaPanel("Comision");
                MuestraDatosComision();
                MostrarInformacionComisionTarjeta();
                ControlMensaje("", false);
                EstatusControlesPasarelaDeCobros(false);

            }
            else
            {
                MVComision = (VMComision)Session["MVComision"];
            }
        }

        //Paneles
        protected void PanelComisiones(object sender, EventArgs e)
        {
            CargaPanel("Comision");
        }
        protected void PanelPasarela(object sender, EventArgs e)
        {
            CargaPanel("Pasarela");
        }


        #region Panel comisiones orden y envio
        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            EstatusDeControlesComision(false);
            MuestraDatosComision();
        }
        protected void BtnEditar_OnClick(object sender, EventArgs e)
        {
            EstatusDeControlesComision(true);
        }
        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            ControlMensaje("", false);
            int ComisionPorOrdenes = 0;
            int ComisionPorEnvio = 0;
            if (!int.TryParse(txtComisionPorOrdenGoDeliverix.Text, out ComisionPorOrdenes))
            {
                ControlMensaje("La comision  de la orden no es valida, use numeros enteros.", true);
            }
            else
            {
                if (!int.TryParse(txtComisionEnvioGoDeliverix.Text, out ComisionPorEnvio))
                {
                    ControlMensaje("La comision del envio no es valida, use numeros enteros.", true);
                }
                else
                {
                    var Envio = MVComision.ListaDeComisiones.Find(c => c.StrNombreTipoDeComision == "Envio");
                    var Orden = MVComision.ListaDeComisiones.Find(c => c.StrNombreTipoDeComision == "Orden");
                    if (Envio.FValor != ComisionPorEnvio)
                    {
                        MVComision.ActualizaComisionGoDeliverix(ComisionPorEnvio, "Envio");
                    }
                    if (Orden.FValor != ComisionPorOrdenes)
                    {
                        MVComision.ActualizaComisionGoDeliverix(ComisionPorOrdenes, "Orden");
                    }
                    ControlMensaje("Comision actualizada.", true);
                    MuestraDatosComision();
                    EstatusDeControlesComision(false);
                }
            }
        }
        private void MuestraDatosComision()
        {
            MVComision.ObtenerComisiones();
            var Envio = MVComision.ListaDeComisiones.Find(c => c.StrNombreTipoDeComision == "Envio");
            var Orden = MVComision.ListaDeComisiones.Find(c => c.StrNombreTipoDeComision == "Orden");
            txtComisionPorOrdenGoDeliverix.Text = Orden.FValor.ToString();
            txtComisionEnvioGoDeliverix.Text = Envio.FValor.ToString();
        }

        #endregion

        #region Panel Comision Tarjeta
        protected void btnEditarComisionTarjeta_OnClick(object sender, EventArgs e)
        {
            EstatusControlesPasarelaDeCobros(true);
        }
        protected void btnGuardarComisionTarjeta_OnClick(object sender, EventArgs e)
        {
            int ComisionTarjeta = 0;
            if (!int.TryParse(txtComisionTarjeta.Text, out ComisionTarjeta))
            {
                ControlMensaje("La comision de la tarjeta no es valida, use numeros enteros.", true);
            }
            else
            {
                MVComision.ActualizaComisionTarjeta(ComisionTarjeta, lblUidComisionTarjeta.Text);
                ControlMensaje("Actualizacion exitosa.", true);
                MostrarInformacionComisionTarjeta();
                EstatusControlesPasarelaDeCobros(false);
            }
        }
        protected void btnCancelarComisionTarjeta_OnClick(object sender, EventArgs e)
        {
            MostrarInformacionComisionTarjeta();
            EstatusControlesPasarelaDeCobros(false);
        }
        private void MostrarInformacionComisionTarjeta()
        {
            MVComision.ObtenerComisionPasarelaDeCobro("MITec");
            txtComisionTarjeta.Text = MVComision.FValor.ToString();
            lblUidComisionTarjeta.Text = MVComision.UidComision.ToString();
        }
        private void EstatusControlesPasarelaDeCobros(bool Estatus)
        {
            //Controles visibles 
            btnGuardarComisionTarjeta.Visible = Estatus;
            btnCancelarComisionTarjeta.Visible = Estatus;
            //Compos para habilitar
            txtComisionTarjeta.Enabled = Estatus;
        }
        #endregion

        private void CargaPanel(string Panel)
        {
            switch (Panel)
            {
                case "Comision":
                    PnlDatosComision.Visible = true;
                    PnlDatosProvedoresPasarela.Visible = false;
                    liComisionDelSistema.Attributes.Add("class", "active");
                    liConfiguracionPasarela.Attributes.Add("class", "");
                    lblTituloPanel.Text = "Comisiones del sistema";
                    EstatusDeControlesComision(false);
                    break;
                case "Pasarela":
                    PnlDatosComision.Visible = false;
                    PnlDatosProvedoresPasarela.Visible = true;
                    liComisionDelSistema.Attributes.Add("class", "");
                    liConfiguracionPasarela.Attributes.Add("class", "active");
                    lblTituloPanel.Text = "Pasarela de cobros";
                    break;
                default:
                    break;
            }
        }
        private void EstatusDeControlesComision(bool Estatus)
        {
            //Controles visibles 
            btnGuardar.Visible = Estatus;
            btnCancelar.Visible = Estatus;
            //Compos para habilitar
            txtComisionPorOrdenGoDeliverix.Enabled = Estatus;
            txtComisionEnvioGoDeliverix.Enabled = Estatus;
        }
        private void ControlMensaje(string StrMensaje, bool Estatus)
        {
            //Controla la visibilidad del panel 
            PnlMensaje.Visible = Estatus;
            //Manda mensaje al usuario
            LblMensaje.Text = StrMensaje;
        }
        protected void btnCerrarMensaje_OnClick(object sender, EventArgs e)
        {
            ControlMensaje("", false);
        }

    }
}
