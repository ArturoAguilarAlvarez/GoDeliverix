using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using VistaDelModelo;
using Newtonsoft.Json;
using AppCliente.WebApi;
using System.Net.Http;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPaso2 : ContentPage
    {
        VMAcceso MVAcceso = new VMAcceso() { };
        VMUsuarios MVUsuarios = new VMUsuarios();
        string UrlApi = "http://www.godeliverix.net/api/";
        string url = "";
        HttpClient _WebApiGoDeliverix = new HttpClient();
        public RegistroPaso2()
        {
            InitializeComponent();
        }
        private async void Button_Siguiente(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsuario.Text)
                && !string.IsNullOrEmpty(txtContrasena1.Text)
                & !string.IsNullOrEmpty(txtContrasena2.Text))
            {
                if (txtContrasena1.Text.Length > 7)
                {
                    url = "http://www.godeliverix.net/api/Usuario/GetBuscarUsuarios?USER=" + txtUsuario.Text + "&UIDPERFIL=4f1e1c4b-3253-4225-9e46-dd7d1940da19";
                    string content = await _WebApiGoDeliverix.GetStringAsync(url);
                    var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    MVUsuarios = JsonConvert.DeserializeObject<VMUsuarios>(obj);
                    //string content = await _WebApiGoDeliverix.GetStringAsync(url);
                    //var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    //MVUsuarios = JsonConvert.DeserializeObject<VMUsuarios>(obj);

                    //MVUsuarios.BusquedaDeUsuario(USER: txtUsuario.Text, UIDPERFIL: new Guid("4f1e1c4b-3253-4225-9e46-dd7d1940da19"));//No me trae el usuario que requiero
                    if (MVUsuarios.LISTADEUSUARIOS.Count == 0)
                    {
                        if (txtContrasena1.Text == txtContrasena2.Text)
                        {
                            await Navigation.PushAsync(new RegistroPaso3(txtUsuario.Text, txtContrasena1.Text));
                        }
                        else
                        {
                            await DisplayAlert("Error", "Los campos de contraseña no coninciden", "OK");
                        }
                    }
                    else
                    {
                         await DisplayAlert("Error", "Usuario existente", "OK");
                    }
                }
                else
                {
                     DisplayAlert("Error", "Seleccione otra contraseña mas segura que sea igual o mayor a 8 digitos ", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Ingrese todos los datos", "OK");
            }
        }

    }
}