using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


using VistaDelModelo;
using System.Text.RegularExpressions;

namespace AppCliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecuperarPassword : ContentPage
	{
        VMCorreoElectronico MVCorreoElectronico = new VMCorreoElectronico();
        VMAcceso MVAcceso = new VMAcceso() { };
        VMUsuarios MVUsuarios = new VMUsuarios();
        public RecuperarPassword ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCorreo.Text))
            {
                if (Email_bien_escrito(txtCorreo.Text))
                {
                    MVCorreoElectronico.BuscarCorreos(strCorreoElectronico: txtCorreo.Text, strParametroDebusqueda: "Usuario");
                    if (MVCorreoElectronico.ID != Guid.Empty)
                    {
                        string password = Guid.NewGuid().ToString().Substring(0, 18);
                        MVUsuarios.ActualizarUsuario(UidUsuario: MVCorreoElectronico.UidPropietario, password: password, perfil: "4F1E1C4B-3253-4225-9E46-DD7D1940DA19");
                        if (MVAcceso.RecuperarCuenta(MVCorreoElectronico.CORREO))
                        {
                            DisplayAlert("Alert", "Se han enviado los datos de tu cuenta", "OK");

                        }
                        else
                        {
                            DisplayAlert("Alert", "Ocurrio un problema al enviar los datos", "OK");
                        }
                    }
                    else
                    {
                        DisplayAlert("Alert", "El correo no existe en el sistema", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Error", "Correo no valido", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Ingrese su correo", "OK");
            }

        }
        private Boolean Email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}