﻿using System;
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
        VMUsuarios MVUsuarios = new VMUsuarios();
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
                    url = "" + Helpers.Settings.sitio + "/api/Usuario/GetBuscarUsuarios?USER=" + txtUsuario.Text + "&UIDPERFIL=4f1e1c4b-3253-4225-9e46-dd7d1940da19";
                    string content = await _WebApiGoDeliverix.GetStringAsync(url);
                    var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    MVUsuarios = JsonConvert.DeserializeObject<VMUsuarios>(obj);
                    if (MVUsuarios.LISTADEUSUARIOS.Count == 0)
                    {
                        if (txtContrasena1.Text == txtContrasena2.Text)
                        {
                            await Navigation.PushAsync(new RegistroPaso3(txtUsuario.Text, txtContrasena1.Text));
                        }
                        else
                        {
                            await DisplayAlert("Datos Invalidos", "Las contraseña ingresada no coincide", "Aceptar");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Datos Invalidos", "El usuario ingresado ya existe", "Aceptar");
                    }
                }
                else
                {
                    await DisplayAlert("Datos Invalidos", "La contraseña debe contener minimo 8 digitos", "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Datos Invalidos", "Ingrese los datos requeridos", "Aceptar");
            }
        }
    }
}