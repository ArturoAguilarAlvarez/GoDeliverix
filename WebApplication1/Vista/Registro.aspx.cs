using System;
using VistaDelModelo;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Vista
{
    public partial class Registro : System.Web.UI.Page
    {
        #region Propiedades
        VMAcceso MVAcceso = new VMAcceso() { };
        VMUsuarios MVUsuarios = new VMUsuarios();
        VMCorreoElectronico MVCorreoElectronico;
        VMTelefono MVTelefono;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNombreRegistro.Attributes.Add("placeholder","Nombre");
            txtApellidoPRegistro.Attributes.Add("placeholder", "Apellido Paterno");
            txtApellidoMRegistro.Attributes.Add("placeholder", "Apellido Materno");
            txtUsuarioRegistro.Attributes.Add("placeholder", "Usuario");
            txtpasswordRegistro.Attributes.Add("placeholder", "Contraseña");
            txtpasswordConfirmacionRegistro.Attributes.Add("placeholder", "Confirmar contraseña");
            txtFechaDeNacimiento.Attributes.Add("placeholder", "DD-MM-AAAA");
            txtTelefonoRegistro.Attributes.Add("placeholder", "Telefono");
            txtEmailRegistro.Attributes.Add("placeholder", "Correo electronico");
            TxtConfirmacionEmail.Attributes.Add("placeholder", "Confirmacion de correo");
        }

        protected void BtnRegistroUsuario_Click(object sender, EventArgs e)
        {
            CamposObligatoriosRegistro();
            if (txtpasswordRegistro.Text == txtpasswordConfirmacionRegistro.Text)
            {
                if (chkTerminosYcondiciones.Checked == true)
                {
                    if (txtEmailRegistro.Text == TxtConfirmacionEmail.Text)
                    {
                        if (MVUsuarios.validarExistenciaDeUsuario(txtUsuarioRegistro.Text))
                        {
                            if (MVUsuarios.ValidarCorreoElectronicoDelUsuario(txtEmailRegistro.Text))
                            {
                                Guid uidusuaro = Guid.NewGuid();
                                Guid uidcorreo = Guid.NewGuid();
                                Guid uidTelefono = Guid.NewGuid();
                                string nombre = txtNombreRegistro.Text;
                                string paterno = txtApellidoPRegistro.Text;
                                string materno = txtApellidoMRegistro.Text;
                                string usuario = txtUsuarioRegistro.Text;
                                string password = txtpasswordRegistro.Text;
                                string fechadenacimiento = txtFechaDeNacimiento.Text;
                                string correo = txtEmailRegistro.Text;
                                string telefono = txtTelefonoRegistro.Text;
                                if (MVUsuarios.GuardaUsuario(UidUsuario: uidusuaro, Nombre: nombre, ApellidoPaterno: paterno, ApellidoMaterno: materno, usuario: usuario, password: password, fnacimiento: fechadenacimiento, perfil: "4f1e1c4b-3253-4225-9e46-dd7d1940da19", estatus: "2", TIPODEUSUARIO: "Cliente"))
                                {
                                    MVCorreoElectronico = new VMCorreoElectronico();
                                    MVTelefono = new VMTelefono();
                                    MVTelefono.AgregaTelefonoALista( "f7bdd1d0-28e5-4f52-bc26-a17cd5c297de", telefono, "Principal");
                                    if (MVCorreoElectronico.AgregarCorreo(uidusuaro, "Usuario", correo, uidcorreo))
                                    {
                                        MVAcceso.CorreoDeConfirmacion(uidusuaro, correo, usuario, password, nombre, paterno + " " + materno);
                                    }
                                    
                                    if (MVTelefono.ListaDeTelefonos != null)
                                    {
                                        if (MVTelefono.ListaDeTelefonos.Count != 0)
                                        {
                                            MVTelefono.GuardaTelefono(uidusuaro, "Usuario");
                                        }
                                    }
                                    lblCorreoDeVerificacion.Text = txtEmailRegistro.Text;
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#myModal').modal('show');</script>", false);
                                }
                                //Limpia los campos de registro 
                                txtNombreRegistro.Text = string.Empty;
                                txtApellidoMRegistro.Text = string.Empty;
                                txtApellidoPRegistro.Text = string.Empty;
                                txtUsuarioRegistro.Text = string.Empty;
                                txtpasswordRegistro.Text = string.Empty;
                                // DTFechaNacimientoRegistro.Text = string.Empty;
                                txtEmailRegistro.Text = string.Empty;
                                txtTelefonoRegistro.Text = string.Empty;
                                txtpasswordConfirmacionRegistro.Text = string.Empty;
                                txtFechaDeNacimiento.Text = string.Empty;
                                chkTerminosYcondiciones.Checked = false;


                            }
                            else
                            {
                                txtEmailRegistro.BorderColor = Color.Red;
                                txtEmailRegistro.Focus();
                                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('El correo " + txtEmailRegistro.Text + " ya existe.')", true);

                            }
                        }
                        else
                        {
                            txtUsuarioRegistro.BorderColor = Color.Red;
                            txtUsuarioRegistro.Focus();
                        }
                    }
                    else
                    {
                        TxtConfirmacionEmail.BorderColor = Color.Red;
                        TxtConfirmacionEmail.ToolTip = "No coincide el correo electronico";
                        TxtConfirmacionEmail.Focus();
                    }
                }
                if (chkTerminosYcondiciones.Checked == false)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Debe de aceptar los terminos y condiciones')", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Las Contraseñas no coinciden')", true);
                txtpasswordRegistro.BorderColor = Color.Red;
                txtpasswordConfirmacionRegistro.BorderColor = Color.Red;
            }
        }

       
        protected void CamposObligatoriosRegistro()
        {
            txtNombreRegistro.BorderColor = Color.Empty;
            txtApellidoPRegistro.BorderColor = Color.Empty;
            txtApellidoMRegistro.BorderColor = Color.Empty;
            txtTelefonoRegistro.BorderColor = Color.Empty;
            txtTelefonoRegistro.BorderColor = Color.Empty;
            txtpasswordRegistro.BorderColor = Color.Empty;
            txtUsuarioRegistro.BorderColor = Color.Empty;
            //DTFechaNacimientoRegistro.BorderColor = Color.Empty;
            txtEmailRegistro.BorderColor = Color.Empty;

            if (string.IsNullOrEmpty(txtNombreRegistro.Text))
            {
                txtNombreRegistro.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtApellidoPRegistro.Text))
            {
                txtApellidoPRegistro.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtApellidoMRegistro.Text))
            {
                txtApellidoMRegistro.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtTelefonoRegistro.Text))
            {
                txtTelefonoRegistro.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtTelefonoRegistro.Text))
            {
                txtTelefonoRegistro.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtpasswordRegistro.Text))
            {
                txtpasswordRegistro.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtUsuarioRegistro.Text))
            {
                txtUsuarioRegistro.BorderColor = Color.Red;
            }
            //if (string.IsNullOrWhiteSpace(DTFechaNacimientoRegistro.Text))
            //{
            //    DTFechaNacimientoRegistro.BorderColor = Color.Red;
            //}
            if (string.IsNullOrEmpty(txtEmailRegistro.Text))
            {
                txtEmailRegistro.BorderColor = Color.Red;
            }

        }

        protected void TxtConfirmacionEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmailRegistro.Text != TxtConfirmacionEmail.Text)
            {
                TxtConfirmacionEmail.BorderColor = Color.Red;
                TxtConfirmacionEmail.ToolTip = "No coincide el correo electronico";
                TxtConfirmacionEmail.Focus();
            }
        }

        protected void txtUsuarioRegistro_TextChanged(object sender, EventArgs e)
        {
            MVUsuarios.BusquedaDeUsuario(USER: txtUsuarioRegistro.Text, UIDPERFIL: new Guid("4f1e1c4b-3253-4225-9e46-dd7d1940da19"));
            if (MVUsuarios.LISTADEUSUARIOS.Count >0)
            {
                txtUsuarioRegistro.BorderColor = Color.Red;
            }
            else
            {
                txtUsuarioRegistro.BorderColor = Color.Empty;
            }
        }
    }
}