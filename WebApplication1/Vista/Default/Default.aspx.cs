using System;
using VistaDelModelo;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using Newtonsoft.Json;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        #region Propiedades
        VMAcceso MVAcceso = new VMAcceso() { };
        VMUsuarios MVUsuarios = new VMUsuarios();
        VMCorreoElectronico MVCorreoElectronico = new VMCorreoElectronico();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            #region PlaceHolders
            txtUser.Attributes.Add("placeholder", "Usuario o Correo electronico");
            txtPass.Attributes.Add("placeholder", "Contraseña");
            txtCorreoElectronico.Attributes.Add("placeholder", "Correo electronico");
            #endregion

            PanelRecuperarContrasenia.Visible = false;
            //Elimina todas las sesiones existentes del sistema
            if (Session["MVProducto"] != null)
            {
                Session.Remove("MVProducto");
            }
            if (Session["MVOrden"] != null)
            {
                Session.Remove("MVOrden");
            }
            if (Session["MVTarifario"] != null)
            {
                Session.Remove("MVTarifario");
            }
            if (Session["MVImagen"] != null)
            {
                Session.Remove("MVImagen");
            }
            if (Session["MVSucursales"] != null)
            {
                Session.Remove("MVSucursales");
            }
            if (Session["MVGiro"] != null)
            {
                Session.Remove("MVGiro" +
                    "");
            }
            if (Session["MVCategoria"] != null)
            {
                Session.Remove("MVCategoria");
            }
            if (Session["MVSubcategoria"] != null)
            {
                Session.Remove("MVSubcategoria");
            }
            if (Session["MVDireccion"] != null)
            {
                Session.Remove("MVDireccion");
            }
            if (Session["MVEMpresa"] != null)
            {
                Session.Remove("MVEMpresa");
            }
            if (Session["MVOferta"] != null)
            {
                Session.Remove("MVOferta");
            }
            if (Session["IdUsuario"] != null)
            {
                Session.Remove("IdUsuario");
            }
            if (Session["MVSeccion"] != null)
            {
                Session.Remove("MVSeccion");
            }





            if (!IsPostBack)
            {

                if (Response.Cookies["Usuario"] != null && Response.Cookies["Password"] != null)
                {
                    if (IngresarAsync(Response.Cookies["Usuario"].Value, Response.Cookies["Password"].Value))
                    {

                    }
                }
            }
        }
        #region Metodos

        protected bool IngresarAsync(string usuario, string password)
        {
            bool acceso = false;
            txtUser.BorderColor = Color.Empty;
            txtPass.BorderColor = Color.Empty;


            if (string.IsNullOrEmpty(usuario))
            {
                txtUser.BorderColor = Color.Red;
                txtUser.ToolTip = "Campo requerido";

            }
            if (string.IsNullOrEmpty(password))
            {
                txtUser.BorderColor = Color.Red;
                txtPass.ToolTip = "Campo requerido";
            }
            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(password))
            {

                Guid Id = Guid.Empty;

                Id = MVAcceso.Ingresar(usuario, password);
                if (Id != Guid.Empty)
                {
                    if (MVAcceso.VerificarEstatus(Id.ToString()))
                    {
                        // ProfileController GetProfileType
                        string perfil = MVAcceso.PerfilDeUsuario(Id.ToString());
                        //Obtener la empresa a la que pertenece el usuario
                        Guid UidEmpresa = MVUsuarios.ObtenerIdEmpresa(Id.ToString());
                        

                        MVUsuarios.BusquedaDeUsuario(UidUsuario: Id, UIDPERFIL: new Guid(perfil), UidEmpresa: UidEmpresa);
                        Session["IdUsuario"] = Id;
                        //Busca a la empresa perteneciente al sistema
                        if (MVUsuarios.UidEmpresa == null)
                        {
                            MVUsuarios.UidEmpresa = Guid.Empty;
                        }
                        Session["UidEmpresaSistema"] = UidEmpresa;
                        acceso = true;
                    }
                    else
                    {
                        acceso = false;
                        lblMensaje.Text = "Usuario inactivo!!! Favor de confirmar la cuenta en el correo de confirmacion.<br/> Si no recibio el correo de confirmacion da click al boton.";
                        panelMensaje.Visible = true;
                    }
                }
                else
                {
                    acceso = false;
                }
            }

            return acceso;
        }

        protected void btnAcceso_Click(object sender, EventArgs e)
        {
            if (IngresarAsync(txtUser.Text, txtPass.Text))
            {
                //Crea el proceso para recordar la sesion en las cookies
                if (chkRecuerdame.Checked == true)
                {
                    HttpCookie cookieUser = new HttpCookie("Usuario");
                    HttpCookie cookiePass = new HttpCookie("Password");
                    //Agrega tiempo en el que expira la coockie
                    cookieUser.Expires = DateTime.Now.AddDays(30);
                    cookiePass.Expires = DateTime.Now.AddDays(30);
                    //Asigna el valor que va a contener la cookie
                    cookieUser.Value = txtUser.Text;
                    cookiePass.Value = txtPass.Text;
                    Response.Cookies.Add(cookieUser);
                    Response.Cookies.Add(cookiePass);
                }
                else
                {
                    Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                }
                Guid Perfil = new Guid(MVAcceso.PerfilDeUsuario(Session["IdUsuario"].ToString()));
                if (Perfil == new Guid("4F1E1C4B-3253-4225-9E46-DD7D1940DA19"))
                {
                    Response.Redirect("../Cliente/Default.aspx");
                }
                else
                {
                    Response.Redirect("../Default.aspx");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Datos incorrectos, \n varifique sus datos.')", true);
            }
        }

        protected void btnCorreoConfirmacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("../EnviarCorreoDeConfirmacion.aspx");
        }

        protected void BtnRecuperarCuenta_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCorreoElectronico.Text))
            {
                string correo = txtCorreoElectronico.Text;
                if ( MVUsuarios.ValidarCorreoElectronicoDelUsuario(correo))
                {
                    string password = Guid.NewGuid().ToString().Substring(0, 18);
                    MVUsuarios.ActualizarUsuario(UidUsuario: MVCorreoElectronico.UidPropietario, password: password, perfil: "4F1E1C4B-3253-4225-9E46-DD7D1940DA19");
                    if (MVAcceso.RecuperarCuenta(correo))
                    {
                        LblMensajePassword.Text = "Se han enviado los datos de tu cuenta";
                    }
                    else
                    {
                        LblMensajePassword.Text = "Ocurrio un problema al enviar los datos";
                    }
                }
                else
                {
                    LblMensajePassword.Text = "El correo no existe en el sistema";
                }
            }
            else
            {
                LblMensajePassword.Text = "Introduce un correo electronico";
            }
            PanelRecuperarContrasenia.Visible = true;
        }

        protected void BtnRegistro_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Registro.aspx");
        }

        protected void BtnRecuperarPassword_Click(object sender, EventArgs e)
        {
            PanelRecuperarContrasenia.Visible = true;
            LblMensajePassword.Text = string.Empty;
        }

        protected void BtnCerrarPanelRecuperarPassword_Click(object sender, EventArgs e)
        {
            PanelRecuperarContrasenia.Visible = false;
        }


        #endregion

        //protected void LoginDeliverix_Authenticate(object sender, AuthenticateEventArgs e)
        //{
        //    string NombreDeUsuario = LoginDeliverix.UserName;
        //    string ClaveDeAutentificacion = LoginDeliverix.Password;
        //    Guid Id = Guid.Empty;

        //    Id = MVUSUARIOS.Ingresar(NombreDeUsuario, ClaveDeAutentificacion);
        //    if (Id != Guid.Empty && Id != null)
        //    {
        //        Session["UidEmpresa"] = null;
        //        Session["IdUsuario"] = Id;
        //        Session["Usuario"] = MVUSUARIOS.NombreDeUsuario(Id);
        //        e.Authenticated = true;


        //    }
        //    else
        //    {
        //        e.Authenticated = false;
        //    }
        //}
    }
}