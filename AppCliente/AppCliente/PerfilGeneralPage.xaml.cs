using AppCliente.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilGeneralPage : ContentPage
    {
        HttpClient _client = new HttpClient();
        public PerfilGeneralPage()
        {
            InitializeComponent();

            App.MVCorreoElectronico.BuscarCorreos(UidPropietario: new Guid(App.Global1), strParametroDebusqueda: "Usuario");
            Cargar();
        }
        private async void Button_EditarGuardar(object sender, EventArgs e)
        {
            if (txtNombre.IsEnabled)
            {
                try
                {
                    string _Url = "" + Helpers.Settings.sitio + "/api/Usuario/GetActualizarUsuario?" +
                    $"UidUsuario={App.Global1}" +
                    $"&Nombre={txtNombre.Text}" +
                    $"&ApellidoPaterno={txtApellidoP.Text}" +
                    $"&ApellidoMaterno={txtApellidoM.Text}" +
                    $"&usuario={txtUsuario.Text}" +
                    $"&password={txtContraseña.Text}" +
                    $"&fnacimiento={txtFechaNacimiento.Date.ToString()}" +
                    $"&perfil=4F1E1C4B-3253-4225-9E46-DD7D1940DA19" +
                    "&estatus=0&UidEmpresa=&UidSucursal=";

                    var content = await _client.GetAsync(_Url);

                    await DisplayAlert("Informacion actualizada", "Se ha actualizado la informacion", "OK");
                    txtNombre.IsEnabled = false;
                    txtApellidoP.IsEnabled = false;
                    txtApellidoM.IsEnabled = false;
                    txtContraseña.IsEnabled = false;
                    txtCorreo.IsEnabled = false;
                    txtFechaNacimiento.IsEnabled = false;
                    Cargar();
                    btnGuardarEditar.Text = "EDITAR";
                }
                catch (Exception)
                {

                    await DisplayAlert("Error", "Algo Ssalio mal, reintentar", "OK");
                }
            }
            else
            {
                txtNombre.IsEnabled = true;
                txtApellidoP.IsEnabled = true;
                txtApellidoM.IsEnabled = true;
                txtContraseña.IsEnabled = true;
                txtCorreo.IsEnabled = false;
                txtFechaNacimiento.IsEnabled = true;
                btnGuardarEditar.Text = "GUARDAR";
            }
        }
        public void Cargausuario()
        {
            txtNombre.Text = App.MVUsuarios.StrNombre;
            txtApellidoP.Text = App.MVUsuarios.StrApellidoPaterno;
            txtApellidoM.Text = App.MVUsuarios.StrApellidoMaterno;
            txtUsuario.Text = App.MVUsuarios.StrUsuario;
            txtContraseña.Text = App.MVUsuarios.StrCotrasena;
            txtFechaNacimiento.Date = Convert.ToDateTime(App.MVUsuarios.DtmFechaDeNacimiento);
            txtCorreo.Text = App.MVCorreoElectronico.CORREO;
        }


        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            txtNombre.IsEnabled = false;
            txtApellidoP.IsEnabled = false;
            txtApellidoM.IsEnabled = false;
            txtContraseña.IsEnabled = false;
            txtCorreo.IsEnabled = false;
            txtFechaNacimiento.IsEnabled = false;

            btnGuardarEditar.Text = "EDITAR";
        }

        private void Button_PerfilTelefonos(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PerfilTelefonoPage());
        }

        private void Button_PerfilDirecciones(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PerfilDireccionesPage());
        }

        public async void Cargar()
        {
            using (HttpClient _webApi = new HttpClient())
            {
                string uril = "" + Helpers.Settings.sitio + "/api/CorreoElectronico/GetBuscarCorreo?UidPropietario=" + App.Global1 + "&strParametroDebusqueda=Usuario";
                string content = await _webApi.GetStringAsync(uril);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVCorreoElectronico = JsonConvert.DeserializeObject<VMCorreoElectronico>(obj);
            }
            using (HttpClient _client = new HttpClient())
            {
                string _URL = (@"" + Helpers.Settings.sitio + "/api/Usuario/GetBuscarUsuarios?UidUsuario=" + App.Global1.ToString() + "" +
                    "&UidEmpresa=00000000-0000-0000-0000-000000000000" +
                    "&UIDPERFIL=4F1E1C4B-3253-4225-9E46-DD7D1940DA19");
                var DatosObtenidos = await _client.GetAsync(_URL);
                string res = await DatosObtenidos.Content.ReadAsStringAsync();
                var asd = JsonConvert.DeserializeObject<ResponseHelper>(res).Data.ToString();
                App.MVUsuarios = JsonConvert.DeserializeObject<VistaDelModelo.VMUsuarios>(asd);
            }
            Cargausuario();
        }
    }
}