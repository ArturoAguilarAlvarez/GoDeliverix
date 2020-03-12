using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistroPaso3 : ContentPage
	{
        string usuario;
        string contrasena;
        public RegistroPaso3 (string usuario, string contrasena)
		{
			InitializeComponent ();
            this.usuario = usuario;
            this.contrasena = contrasena;
        }
        private void Button_Siguiente(object sender, EventArgs e)
        {

            DateTime fecharegistro1 = DateTime.Parse(txtFechaNacimiento.Date.ToString());
            var horas = (DateTime.Now - fecharegistro1).TotalDays;
            if (horas> 5110)
            {
                if (!string.IsNullOrEmpty(txtNombre.Text)
                 && !string.IsNullOrEmpty(txtApellidoP.Text)
                 && !string.IsNullOrEmpty(txtApellidoM.Text)
                 && !string.IsNullOrEmpty(txtFechaNacimiento.Date.ToString()))
                {
                    Navigation.PushAsync(new RegistroPaso4(usuario, contrasena, txtNombre.Text, txtApellidoP.Text, txtApellidoM.Text, txtFechaNacimiento.Date.ToString("dd-MM-yyyy")));
                }
                else
                {
                    DisplayAlert("Error", "Ingrese los datos requeridos", "Aceptar");
                }
            }
            else
            {
                DisplayAlert("Error", "Debes ser mayor de 14 años", "Aceptar");
            }
            

        }
    }
}