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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PerfilGeneralPage : ContentPage
	{
        HttpClient _client = new HttpClient();
        public PerfilGeneralPage ()
		{
			InitializeComponent ();
            App.MVCorreoElectronico.BuscarCorreos(UidPropietario: new Guid(App.Global1), strParametroDebusqueda: "Usuario");
            Cargar();
            //App.MVUsuarios.obtenerUsuario(App.Global1);
            //Cargausuario();
        }
        private void Button_EditarGuardar(object sender, EventArgs e)
        {
            if (txtNombre.IsEnabled)
            {
                try
                {
                    AppCliente.App.MVUsuarios.ActualizarUsuario(UidUsuario: new Guid(AppCliente.App.Global1),
                        Nombre: txtNombre.Text,
                        ApellidoPaterno: txtApellidoP.Text,
                        ApellidoMaterno: txtApellidoM.Text,
                        usuario: txtUsuario.Text,
                        password: txtContraseña.Text,
                        fnacimiento: txtFechaNacimiento.Date.ToString("MM-dd-yyyy"),
                        perfil: "4F1E1C4B-3253-4225-9E46-DD7D1940DA19");
                    DisplayAlert("Excelente :)", "Registro exitoso", "OK");
                    txtNombre.IsEnabled = false;
                    txtApellidoP.IsEnabled = false;
                    txtApellidoM.IsEnabled = false;
                    txtContraseña.IsEnabled = false;
                    txtCorreo.IsEnabled = false;
                    txtFechaNacimiento.IsEnabled = false;
                    btnGuardarEditar.Text = "EDITAR";
                }
                catch (Exception)
                {

                    DisplayAlert("Error", "Algo Ssalio mal, reintentar", "OK");
                }
            }
            else
            {
                txtNombre.IsEnabled = true;
                txtApellidoP.IsEnabled = true;
                txtApellidoM.IsEnabled = true;
                txtContraseña.IsEnabled = true;
                txtCorreo.IsEnabled = true;
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
            txtFechaNacimiento.Date = DateTime.Parse(App.MVUsuarios.DtmFechaDeNacimiento);
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

        public async  void Cargar()
        {
            string _URL = ("http://godeliverix.net/api/Usuario/GetUsuario?UidUsuario=" + App.Global1);
            var DatosObtenidos = await _client.GetAsync(_URL);
            string res = await DatosObtenidos.Content.ReadAsStringAsync();

            DataContractJsonSerializer asd = new DataContractJsonSerializer(typeof(VistaDelModelo.VMUsuarios));

            dynamic results = JsonConvert.DeserializeObject<dynamic>(res);





            //VistaDelModelo.VMUsuarios adsffj = (VistaDelModelo.VMUsuarios)asd;
            //var json = JsonConvert.SerializeObject(commandMessage);
            //var myCommandMessage = (VistaDelModelo.VMUsuarios)JsonConvert.DeserializeObject();
            //var content = Json.Deserialize<Model>(res);
            //var Datos = JsonConvert.DeserializeObject<VistaDelModelo.VMUsuarios>(DatosObtenidos);


            //var ArrayDatosProductos = JArray.Parse(Datos.ToString());
            //    App.MVEmpresa.LISTADEEMPRESAS = ArrayDatosProductos.Select(p => new VMEmpresas
            //    {
            //        UIDEMPRESA = (Guid)p["UIDEMPRESA"],
            //        NOMBRECOMERCIAL = (string)p["NOMBRECOMERCIAL"],
            //        StrRuta = "http://godeliverix.net/vista/" + (string)p["StrRuta"].ToString().Substring(3)
            //    }).ToList();
        }
    }
}