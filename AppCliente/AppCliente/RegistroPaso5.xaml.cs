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

namespace AppCliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistroPaso5 : ContentPage
	{
        VMAcceso MVAcceso = new VMAcceso() { };
        VMUsuarios MVUsuarios = new VMUsuarios();
        VMCorreoElectronico MVCorreoElectronico;
        VMDireccion MVDireccion = new VMDireccion();
        VMTelefono MVTelefono;

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
            InitializeComponent ();
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
                //string url = RestService.Servidor+"api/Usuario/GetGuardarusuarioCliente?" +
                //    "nombre=" + nombre +
                //    "&apellidoP=" + apellidoP +
                //    "&ApellidoP=" + apellidoP +
                //    "&apellidoM=" + apellidoM +
                //    "&usuario=" +usuario+
                //    "&contrasena=" + contraseña +
                //    "&fechaNacimiento=" + fechaNacimiento +
                //    "&telefono=" + telefono +
                //    "&correo=" + correo;
                //HttpClient _client = new HttpClient();
                //string strDirecciones = await _client.GetStringAsync(url);
                //Application.Current.MainPage = new MasterMenu();

                if (MVUsuarios.GuardaUsuario(UidUsuario: uidusuaro, Nombre: nombre, ApellidoPaterno: apellidoP, ApellidoMaterno: apellidoM, usuario: usuario, password: contraseña, fnacimiento: fechaNacimiento, perfil: "4f1e1c4b-3253-4225-9e46-dd7d1940da19", estatus: "2", TIPODEUSUARIO: "Cliente"))
                {
                    MVTelefono = new VMTelefono();
                    MVCorreoElectronico = new VMCorreoElectronico();
                    MVTelefono = new VMTelefono();
                    MVTelefono.AgregaTelefonoALista("f7bdd1d0-28e5-4f52-bc26-a17cd5c297de", telefono, "Principal");
                    if (MVCorreoElectronico.AgregarCorreo(uidusuaro, "Usuario", correo, uidcorreo))
                    {
                        MVAcceso.CorreoDeConfirmacion(uidusuaro, correo, usuario, contraseña, nombre, apellidoM + " " + apellidoM);

                        //Application.Current.MainPage = new MasterDetailPage1();

                        AppCliente.App.Global1 = uidusuaro.ToString();
                        //MVTelefono.TipoDeTelefonos();
                        //MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");
                        MVUsuarios.obtenerUsuario(AppCliente.App.Global1);
                        MVDireccion.ObtenerDireccionesUsuario(AppCliente.App.Global1);

                        AppCliente.App.MVDireccion = MVDireccion;
                        AppCliente.App.MVTelefono = MVTelefono;
                        AppCliente.App.MVUsuarios = MVUsuarios;

                        Application.Current.MainPage = new MasterMenu();
                    }
                    if (MVTelefono.ListaDeTelefonos != null)
                    {
                        if (MVTelefono.ListaDeTelefonos.Count != 0)
                        {
                            MVTelefono.GuardaTelefono(uidusuaro, "Usuario");
                        }
                    }
                }

            }


        }
    }
}