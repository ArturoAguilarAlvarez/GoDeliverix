using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using VistaDelModelo;
using AppCliente.WebApi;
using System.Net.Http;
using Newtonsoft.Json;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPaso5 : ContentPage
    {
        VMAcceso MVAcceso = new VMAcceso() { };
        VMUsuarios MVUsuarios = new VMUsuarios();
        VMDireccion MVDireccion = new VMDireccion();

        HttpClient _client = new HttpClient();

        string usuario;
        string contraseña;
        string nombre;
        string apellidoP;
        string apellidoM;
        string fechaNacimiento;
        string telefono;
        string correo;
        public RegistroPaso5(string usuario, string contraseña, string nombre, string apellidoP, string apellidoM, string fechaNacimiento, string telefono, string correo)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.nombre = nombre;
            this.contraseña = contraseña;
            this.apellidoP = apellidoP;
            this.apellidoM = apellidoM;
            this.fechaNacimiento = fechaNacimiento;
            this.telefono = telefono;
            this.correo = correo;
        }
        private async void Button_Siguiente(object sender, EventArgs e)
        {
            Guid uidusuaro = Guid.NewGuid();
            Guid uidcorreo = Guid.NewGuid();

            var action = await DisplayAlert("Términos y condiciones", "He leído y acepto los términos y condiciones", "Si", "No");
            if (action)
            {
                //_client.BaseAddress = new Uri("" + Helpers.Settings.sitio + "/api/");
                string url = "" + Helpers.Settings.sitio + "/api/Usuario/GetGuardarusuarioCliente?UidUsuario=" + uidusuaro + "&nombre=" + nombre + "&apellidoP=" + apellidoP + "&apellidoM=" + apellidoM + "&usuario=" + usuario + "&contrasena=" + contraseña + "&fechaNacimiento=" + fechaNacimiento + "&correo=" + correo + "&perfil=4f1e1c4b-3253-4225-9e46-dd7d1940da19";
                string content = await _client.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                bool Resultado = bool.Parse(obj);
                if (Resultado)
                {
                    url = "" + Helpers.Settings.sitio + "/api/CorreoElectronico /GetAgregarCorreo?UidPropietario=" + uidusuaro + "&strParametroDeInsercion=Usuario&strCorreoElectronico=" + correo + "&UidCorreoElectronico=" + uidcorreo + "";
                    content = await _client.GetStringAsync(url);
                    obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    bool respuesta = bool.Parse(obj.ToString());
                    if (respuesta)
                    {
                        GenerateMessage("Registro existoso!", "Se ha enviado un correo de activacion al correo \n" + correo + "", "OK");
                        Application.Current.MainPage = new NavigationPage(new Login());

                        Guid UidTelefono = Guid.NewGuid();
                        url = "" + Helpers.Settings.sitio + "/api/Telefono/GetGuardaTelefonoApi?uidUsuario=" + uidusuaro + "&Parametro=Usuario&UidTelefono=" + UidTelefono + "&Numero=" + telefono + "&UidTipoDeTelefono=f7bdd1d0-28e5-4f52-bc26-a17cd5c297de";
                        await _client.GetStringAsync(url);

                        await Navigation.PopToRootAsync();
                    }
                }
            }
        }

        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }
    }
}