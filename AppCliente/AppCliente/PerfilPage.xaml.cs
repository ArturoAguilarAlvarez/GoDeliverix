using Modelo;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilPage : TabbedPage
    {
        public List<string> Items = new List<string>();

        VMAcceso MVAcceso = new VMAcceso() { };
        VMDireccion MVDireccion = new VMDireccion();

        VMUsuarios MVUsuarios = new VMUsuarios();
        public ObservableCollection<VMTelefono> TelefonoLista { get; set; }

        public List<VMDireccion> DireccionesLista = new List<VMDireccion>();
        VMTelefono MVTelefono = new VMTelefono();

        public static VMCorreoElectronico MVCorreoElectronico = new VMCorreoElectronico();
        public List<TipoDeTelefono> TIPOTELEFONO;

        public PerfilPage()
        {
            InitializeComponent();
            MVTelefono = App.MVTelefono;
            MVUsuarios = App.MVUsuarios;
            MVCorreoElectronico = App.MVCorreoElectronico;
            MVDireccion = App.MVDireccion;
            CargarNombreTelefono();
            MyListView.ItemsSource = MVTelefono.ListaDeTelefonos;
            Cargausuario();
            MyPicker.ItemsSource = MVTelefono.ListaDeTipoDeTelefono;

            txtNumeroTelefonico.Text = "";
            txtIDTelefono.Text = "";

            MyListViewDirecciones.ItemsSource = MVDireccion.ListaDIRECCIONES;
        }
        public void CargarNombreTelefono()
        {
            int a = MVTelefono.ListaDeTelefonos.Count();
            a = a - 1;
            MyListView.ItemsSource = MVTelefono.ListaDeTelefonos;
            for (int i = 0; i <= a; i++)
            {
                MVTelefono.ListaDeTelefonos[i].StrNombreTipoDeTelefono = MVTelefono.ListaDeTipoDeTelefono.Where(t => t.ID == MVTelefono.ListaDeTelefonos[i].UidTipo).FirstOrDefault().StrNombreTipoDeTelefono;
            }
        }
        public void Cargausuario()
        {
            txtNombre.Text = MVUsuarios.StrNombre;
            txtApellido1.Text = MVUsuarios.StrApellidoPaterno;
            txtApellido2.Text = MVUsuarios.StrApellidoMaterno;
            txtUsuario.Text = MVUsuarios.StrUsuario;
            txtContrasena.Text = MVUsuarios.StrCotrasena;
            txtFechaNacimiento.Date = DateTime.Parse(MVUsuarios.DtmFechaDeNacimiento);
            txtCorreo.Text = MVCorreoElectronico.CORREO;
        }

        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //var item = ((VMTelefono)sender);
            //VMTelefono ObjItem = (VMTelefono)item.CommandParameter;

            ////if (e.Item == null)
            ////    return;
            ////string a=((VMTelefono)((MenuItem)sender).CommandParameter).TIPOTELEFONO.ToString();
            //await DisplayAlert("Item Tapped","An item was tapped.", "OK");

            ////Deselect Item
            ////((ListView)sender).SelectedItem = null;
        }
        private void OnMoreDirecciones(object sender, EventArgs e)
        {

        }
        private void OnMore(object sender, EventArgs e)
        {

            var item = ((MenuItem)sender);
            VMTelefono ObjItem = (VMTelefono)item.CommandParameter;
            txtNumeroTelefonico.Text = ObjItem.NUMERO;
            txtIDTelefono.Text = ObjItem.ID.ToString();
            MyPicker.SelectedIndex = -1;
        }
        private void OnDeleteDirecciones(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            VMDireccion ObjItem = (VMDireccion)item.CommandParameter;
            Guid Gui = ObjItem.ID;
            int index = MVDireccion.ListaDIRECCIONES.FindIndex(x => x.ID == Gui);
            MVDireccion.QuitaDireeccionDeLista(ObjItem.ID.ToString());
            MVDireccion.EliminaDireccionUsuario(ObjItem.ID.ToString());

            MVDireccion.ObtenerDireccionesUsuario(AppCliente.App.Global1);
            MyListView.ItemsSource = null;
            MyListViewDirecciones.ItemsSource = MVDireccion.ListaDIRECCIONES;
        }
        private void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            VMTelefono ObjItem = (VMTelefono)item.CommandParameter;
            Guid Gui = ObjItem.ID;
            int index = MVTelefono.ListaDeTelefonos.FindIndex(x => x.ID == Gui);
            MVTelefono.EliminaTelefonoUsuario(ObjItem.ID.ToString());
            //MVTelefono.ListaDeTelefonos[index].NUMERO = "1234";
            MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");
            MyListView.ItemsSource = null;
            CargarNombreTelefono();
            MyListView.ItemsSource = MVTelefono.ListaDeTelefonos;

        }
        void OnRefresh(object sender, EventArgs e)
        {
        }
        private async void Button_GuardarDatos(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());

            try
            {
                string url = "" + Helpers.Settings.sitio + "/api/Usuario/GetActualizarUsuario?UidUsuario=" + App.Global1 + "&Nombre=" + txtNombre.Text + "&ApellidoPaterno=" + txtApellido1.Text + "&ApellidoMaterno=" + txtApellido2.Text + "&perfil=4F1E1C4B-3253-4225-9E46-DD7D1940DA19&password=" + txtContrasena.Text + "&fnacimiento=" + txtFechaNacimiento.Date.ToString("MM-dd-yyyy") + "";
                //MVUsuarios.ActualizarUsuario(UidUsuario: new Guid(AppCliente.App.Global1), Nombre: txtNombre.Text, ApellidoPaterno: txtApellido1.Text, ApellidoMaterno: txtApellido2.Text, usuario: txtUsuario.Text, password: txtContrasena.Text, fnacimiento: txtFechaNacimiento.Date.ToString("MM-dd-yyyy"), perfil: "4F1E1C4B-3253-4225-9E46-DD7D1940DA19");
                HttpClient _webApi = new HttpClient();
                await _webApi.GetAsync(url);
                Cargausuario();
                await PopupNavigation.Instance.PopAsync();
                await DisplayAlert("Error", "Registro exitoso", "OK");
            }
            catch (Exception)
            {
                await PopupNavigation.Instance.PopAsync();
                await DisplayAlert("Error", "Algo Ssalio mal, reintentar", "OK");
            }

        }

        private void Button_AgregarTelefono(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroTelefonico.Text))
            {

                TipoDeTelefono a = (TipoDeTelefono)MyPicker.SelectedItem;
                string Numero = txtNumeroTelefonico.Text;
                string TipoTelefono = a.ID.ToString();
                string NombreTipoTelefono = a.NOMBRE.ToString();

                if (!string.IsNullOrEmpty(txtIDTelefono.Text))
                {
                    MVTelefono.ActualizaRegistroEnListaDeTelefonos(txtIDTelefono.Text, TipoTelefono, txtNumeroTelefonico.Text);

                }
                else
                {
                    MVTelefono.AgregaTelefonoALista(TipoTelefono, txtNumeroTelefonico.Text, NombreTipoTelefono);
                }

                //Guarda los telefonos
                if (MVTelefono.ListaDeTelefonos != null)
                {
                    if (MVTelefono.ListaDeTelefonos.Count != 0)
                    {
                        MVTelefono.EliminaTelefonosUsuario(new Guid(AppCliente.App.Global1));
                        MVTelefono.GuardaTelefono(new Guid(AppCliente.App.Global1), "Usuario");
                    }
                }
                MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");
                CargarNombreTelefono();
                MyListView.ItemsSource = null;
                MyListView.ItemsSource = MVTelefono.ListaDeTelefonos;
                Cargausuario();
                MyPicker.ItemsSource = MVTelefono.ListaDeTipoDeTelefono;

                txtNumeroTelefonico.Text = "";
                txtIDTelefono.Text = "";
            }
        }

        private void Button_Direcciones(object sender, EventArgs e)
        {
            LBIdentificador.IsVisible = true;

            txtDireccion.IsVisible = true;
            LBPais.IsVisible = true;
            LBEstado.IsVisible = true;
            LBMunicipio.IsVisible = true;
            LBCiudad.IsVisible = true;
            LBColonia.IsVisible = true;
            LBCalle.IsVisible = true;
            LBEntreCalle.IsVisible = true;
            LBmanzana.IsVisible = true;
            LBLote.IsVisible = true;
            LBCodigoPostal.IsVisible = true;
            LBReferencias.IsVisible = true;
            BtnCAncelar.IsVisible = true;
            MypickerPais.IsVisible = true;
            BtnCAncelar.IsVisible = true;
            MypickerEstado.IsVisible = true;
            MypickerMunicipio.IsVisible = true;
            MypickerCiudad.IsVisible = true;
            MypickerColonia.IsVisible = true;

        }

        private void BtnCAncelar_Clicked(object sender, EventArgs e)
        {
            LBIdentificador.IsVisible = false;

            txtDireccion.IsVisible = false;
            LBPais.IsVisible = false;
            LBEstado.IsVisible = false;
            LBMunicipio.IsVisible = false;
            LBCiudad.IsVisible = false;
            LBColonia.IsVisible = false;
            LBCalle.IsVisible = false;
            LBEntreCalle.IsVisible = false;
            LBmanzana.IsVisible = false;
            LBLote.IsVisible = false;
            LBCodigoPostal.IsVisible = false;
            LBReferencias.IsVisible = false;
            MypickerPais.IsVisible = false;
            BtnCAncelar.IsVisible = false;
            MypickerMunicipio.IsVisible = false;
            MypickerCiudad.IsVisible = false;
            MypickerColonia.IsVisible = false;
        }

        private void MypickerPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid Pais = new Guid("afd6c3b7-f5be-40c9-8385-936d275a8d6b");
            DataTable dt = new DataTable();

            dt = MVDireccion.Estados(Pais);

            DireccionesLista.Clear();
            foreach (DataRow item in dt.Rows)
            {
                MypickerEstado.Items.Add(item["Nombre"].ToString());
                DireccionesLista.Add(
                  new VMDireccion()
                  {
                      ID = new Guid(item["IdEstado"].ToString()),
                      ESTADO = item["Nombre"].ToString()
                  });
            }
        }
    }
}