using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


using VistaDelModelo;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using AppCliente.WebApi;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecuperarPassword : ContentPage
    {
        VMCorreoElectronico MVCorreoElectronico = new VMCorreoElectronico();
        VMAcceso MVAcceso = new VMAcceso() { };
        VMUsuarios MVUsuarios = new VMUsuarios();
        public RecuperarPassword()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCorreo.Text))
            {
                if (Email_bien_escrito(txtCorreo.Text))
                {
                    //MVCorreoElectronico.BuscarCorreos(strCorreoElectronico: txtCorreo.Text, strParametroDebusqueda: "Usuario");
                    using (HttpClient _WebApi = new HttpClient())
                    {
                        string _URL = "" + Helpers.Settings.sitio + "/api/CorreoElectronico/GetBuscarCorreo?strCorreoElectronico=" + txtCorreo.Text + "&strParametroDebusqueda=Usuario";
                        var content = await _WebApi.GetStringAsync(_URL);
                        var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        MVCorreoElectronico = JsonConvert.DeserializeObject<VMCorreoElectronico>(obj);
                    }
                    if (MVCorreoElectronico.ID != Guid.Empty)
                    {
                        string password = Guid.NewGuid().ToString().Substring(0, 18);
                        MVUsuarios.ActualizarUsuario(UidUsuario: MVCorreoElectronico.UidPropietario, password: password, perfil: "4F1E1C4B-3253-4225-9E46-DD7D1940DA19");
                        bool respuesta = false;
                        using (HttpClient _client = new HttpClient())
                        {
                            string _Url = $"" + Helpers.Settings.sitio + "/api/CorreoElectronico/GetRecuperarContrasena?" +
                                $"strCorreoElectronico={MVCorreoElectronico.CORREO}";
                            var content = await _client.GetAsync(_Url);
                            string res = await content.Content.ReadAsStringAsync();
                            var asd = JsonConvert.DeserializeObject<ResponseHelper>(res).Data.ToString();
                            respuesta = bool.Parse(asd);

                        }
                        if (respuesta)
                        {
                            await DisplayAlert("Alert", "Se han enviado los datos de tu cuenta", "OK");

                        }
                        else
                        {
                            await DisplayAlert("Alert", "Ocurrio un problema al enviar los datos", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alert", "El correo no existe en el sistema", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Correo no valido", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Ingrese su correo", "OK");
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