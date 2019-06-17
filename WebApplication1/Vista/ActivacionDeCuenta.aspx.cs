using System;
using VistaDelModelo;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Vista
{
    public partial class ActivacionDeCuenta : System.Web.UI.Page
    {
        VMAcceso MVAcceso = new VMAcceso();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["CodigoActivacionCuentaDeliverixxdxdxdxd"] != null && Request.QueryString["UidUsuarioxdxdxdxdDDxD"] != null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('codigo de activacion = '" + Request.QueryString["CodigoActivacionCuentaDeliverixxdxdxdxd"] + "' del usuario = '" + Request.QueryString["UidUsuarioxdxdxdxdDDxD"] + "'.')", true);
                    MVAcceso.ActualizarEstatus(new Guid(Request.QueryString["CodigoActivacionCuentaDeliverixxdxdxdxd"]),new Guid(Request.QueryString["UidUsuarioxdxdxdxdDDxD"]));
                }
            }
            
        }
    }
}