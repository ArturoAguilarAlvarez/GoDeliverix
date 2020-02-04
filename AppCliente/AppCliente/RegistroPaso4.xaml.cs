using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using VistaDelModelo;
namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPaso4 : ContentPage
    {

        string usuario;
        string contrasena;
        string nombre;
        string apellidoP;
        string apellidoM;
        string fechaNacimiento;
        VMAcceso MVAcceso = new VMAcceso() { };
        VMUsuarios MVUsuarios = new VMUsuarios();

        public RegistroPaso4(string usuario, string contrasena, string nombre, string apellidoP, string apellidoM, string fechaNacimiento)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.nombre = nombre;
            this.apellidoP = apellidoP;
            this.apellidoM = apellidoM;
            this.fechaNacimiento = fechaNacimiento;
        }

        private void Button_Guardar(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txrCorreo.Text)
                && !string.IsNullOrEmpty(txtNumeroTelefono.Text))
            {
                if (email_bien_escrito(txrCorreo.Text))
                {
                    if (MVUsuarios.ValidarCorreoElectronicoDelUsuario(txrCorreo.Text))
                    {
                        Navigation.PushAsync(new RegistroPaso5(usuario, contrasena, nombre, apellidoP, apellidoM, fechaNacimiento, txtNumeroTelefono.Text, txrCorreo.Text));
                    }
                    else
                    {
                        DisplayAlert("Error", "Correo no disponible", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Error", "Correo no valido", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Los campos de Correo y numero telefonico son obligatorios", "OK");
            }
        }


        private Boolean email_bien_escrito(String email)
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