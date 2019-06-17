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
    public partial class EnviarCorreoDeConfirmacion : System.Web.UI.Page
    {
        VMAcceso MVAcceso = new VMAcceso();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnEnviarCorreo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCorreoElectronico.Text))
            {
                if (MVAcceso.RecuperarCodigoDeConfirmacion(txtCorreoElectronico.Text))
                {
                    lblMensaje.Text = "Correo enviado...!";
                    panelMensaje.Visible = true;
                }
                else
                {
                    lblMensaje.Text = "No existe el correo electronico";
                    panelMensaje.Visible = true;
                }
                
            }
            else
            {
                txtCorreoElectronico.BorderColor = Color.Red;
                txtCorreoElectronico.Focus();
            }
        }
    }
}