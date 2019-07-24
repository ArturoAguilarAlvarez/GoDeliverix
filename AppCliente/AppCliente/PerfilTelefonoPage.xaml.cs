using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VistaDelModelo;
using System.Diagnostics;
using Modelo;
using Newtonsoft.Json;
using AppCliente.WebApi;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilTelefonoPage : ContentPage
    {

        HttpClient _client = new HttpClient();
        Guid UidTipoDetelefono;
        public ObservableCollection<string> Items { get; set; }
        public ObservableCollection<VMTelefono> TelefonoLista { get; set; }
        public PerfilTelefonoPage()
        {

            InitializeComponent();
            App.MVTelefono.TipoDeTelefonos();
            CargarAsync();
            //App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(App.Global1), ParadetroDeBusqueda: "Usuario");

            //string _Url = $"http://godeliverix.net/api/Telefono/GetBuscarTelefonos?UidPropietario={}&ParadetroDeBusqueda=Usuario";
            //var content = await _client.GetStreamAsync(_Url);
            //CargarNombreTelefono();




            //for (int i = 0; i < AppCliente.App.MVTelefono.ListaDeTelefonos.Count; i++)
            //{
            //    AppCliente.App.MVTelefono.ListaDeTelefonos[i].Estado = false;
            //}

            //MyPicker.ItemsSource = AppCliente.App.MVTelefono.TIPOTELEFONO;
            //MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;
        }

        public void CargarNombreTelefono()
        {
            int a = AppCliente.App.MVTelefono.ListaDeTelefonos.Count();
            a = a - 1;
            MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;
            for (int i = 0; i <= a; i++)
            {
                AppCliente.App.MVTelefono.ListaDeTelefonos[i].StrNombreTipoDeTelefono = AppCliente.App.MVTelefono.ListaDeTipoDeTelefono.Where(t => t.ID == AppCliente.App.MVTelefono.ListaDeTelefonos[i].UidTipo).FirstOrDefault().StrNombreTipoDeTelefono;

                //int index = MVTelefono.TIPOTELEFONO.FindIndex(x => x.ID == MVTelefono.ListaDeTelefonos[i].ID);
                //MVTelefono.ListaDeTelefonos[i].StrNombreTipoDeTelefono = MVTelefono.TIPOTELEFONO[index].NOMBRE;
            }
        }

        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var item = ((ItemTappedEventArgs)e);
            VMTelefono ObjItem = (VMTelefono)item.Item;
            if (ObjItem.Estado)
            {
                ObjItem.Estado = false;
                txtIDTelefono.Text = "0";
            }
            else
            {
                for (int i = 0; i < AppCliente.App.MVTelefono.ListaDeTelefonos.Count; i++)
                {
                    AppCliente.App.MVTelefono.ListaDeTelefonos[i].Estado = false;
                }
                ObjItem.Estado = true;
                txtIDTelefono.Text = ObjItem.ID.ToString();
            }
            MyListView.ItemsSource = null;
            MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;
            //var item = ((VMTelefono)sender);
            //VMTelefono ObjItem = (VMTelefono)item.CommandParameter;

            ////if (e.Item == null)
            ////    return;
            ////string a=((VMTelefono)((MenuItem)sender).CommandParameter).TIPOTELEFONO.ToString();
            //await DisplayAlert("Item Tapped","An item was tapped.", "OK");

            ////Deselect Item
            ////((ListView)sender).SelectedItem = null;
        }

        private void OnMore(object sender, EventArgs e)
        {
            btnCancelar.IsVisible = true;
            btnGuardarEditar.IsVisible = true;
            cajaDatosTelefono.IsVisible = true;
            var item = ((MenuItem)sender);
            VMTelefono ObjItem = (VMTelefono)item.CommandParameter;
            txtNumeroTelefonico.Text = ObjItem.NUMERO;

            txtIDTelefono.Text = ObjItem.ID.ToString();
            int query1 = AppCliente.App.MVTelefono.ListaDeTipoDeTelefono.FindIndex(t => t.StrNombreTipoDeTelefono == ObjItem.StrNombreTipoDeTelefono);
            MyPicker.SelectedIndex = query1;
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            VMTelefono ObjItem = (VMTelefono)item.CommandParameter;
            EliminarTelefono(ObjItem);
        }

        public async void EliminarTelefono(VMTelefono ObjItem)
        {
            var action = await DisplayAlert("Exit?", "Are you sure to close", "Yes", "No");
            if (action)
            {

                Guid Gui = ObjItem.ID;
                int index = AppCliente.App.MVTelefono.ListaDeTelefonos.FindIndex(x => x.ID == Gui);
                AppCliente.App.MVTelefono.EliminaTelefonoUsuario(ObjItem.ID.ToString());
                //MVTelefono.ListaDeTelefonos[index].NUMERO = "1234";
                AppCliente.App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");
                MyListView.ItemsSource = null;
                CargarNombreTelefono();
                MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;
            }
        }

        void OnRefresh(object sender, EventArgs e)
        {

        }

        private void BtnGuardarEditar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroTelefonico.Text))
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtIDTelefono.Text))
                    {
                        GuardarTelefono("actualizar");
                    }
                    else
                    {
                        GuardarTelefono("Nuevo");
                    }

                    //Guarda los telefonos
                    //if (AppCliente.App.MVTelefono.ListaDeTelefonos != null)
                    //{
                    //    if (AppCliente.App.MVTelefono.ListaDeTelefonos.Count != 0)
                    //    {
                    //        AppCliente.App.MVTelefono.EliminaTelefonosUsuario(new Guid(AppCliente.App.Global1));
                    //        AppCliente.App.MVTelefono.GuardaTelefono(new Guid(AppCliente.App.Global1), "Usuario");
                    //    }
                    //}
                    // AppCliente.App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");
                    //CargarNombreTelefono();
                    //MyListView.ItemsSource = null;
                    //MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;
                    //MyPicker.ItemsSource = AppCliente.App.MVTelefono.TIPOTELEFONO;

                    txtNumeroTelefonico.Text = "";
                    txtIDTelefono.Text = "";
                    MyListView.IsVisible = true;
                    btnNuevoNumero.IsVisible = true;

                    cajaDatosTelefono.IsVisible = false;
                    btnGuardarEditar.IsVisible = false;
                    btnCancelar.IsVisible = false;
                    CargarAsync();
                }
                catch (Exception)
                {
                    DisplayAlert("!Ooooops", "NO ha seleccionado el tipo de telefono", "ok");
                }

            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            cajaDatosTelefono.IsVisible = false;
            btnGuardarEditar.IsVisible = false;
            btnCancelar.IsVisible = false;
            MyListView.IsVisible = true;
            btnNuevoNumero.IsVisible = true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            btnCancelar.IsVisible = true;
            btnGuardarEditar.IsVisible = true;
            cajaDatosTelefono.IsVisible = true;
            txtNumeroTelefonico.Text = "";
            txtIDTelefono.Text = "";
            MyPicker.SelectedItem = 0;
            btnNuevoNumero.IsVisible = false;
            MyListView.IsVisible = false;
        }

        private void Button_Clicked_Eliminar(object sender, EventArgs e)
        {
            EliminarTelefono2();
        }

        public async void EliminarTelefono2()
        {
            if (txtIDTelefono.Text != "0" && !string.IsNullOrEmpty(txtIDTelefono.Text))
            {
                var action = await DisplayAlert("Eliminar?", "Estas seguro de eliminar este Telefono", "Si", "No");
                if (action)
                {

                    //Guid Gui = new Guid(txtIDTelefono.Text);
                    //int index = AppCliente.App.MVTelefono.ListaDeTelefonos.FindIndex(x => x.ID == Gui);
                    //AppCliente.App.MVTelefono.EliminaTelefonoUsuario(txtIDTelefono.Text);
                    //MVTelefono.ListaDeTelefonos[index].NUMERO = "1234";

                    string _Url = $"http://godeliverix.net/api/Telefono/DeleteTelefonoUsuario?UidTelefono={txtIDTelefono.Text}";
                    await _client.DeleteAsync(_Url);

                    CargarAsync();
                    //AppCliente.App.MVTelefono.BuscarTelefonos(UidPropietario: new Guid(AppCliente.App.Global1), ParadetroDeBusqueda: "Usuario");
                    //MyListView.ItemsSource = null;
                    //CargarNombreTelefono();
                    //MyListView.ItemsSource = AppCliente.App.MVTelefono.ListaDeTelefonos;
                }
            }

        }

        private void Button_Clicked_Editar(object sender, EventArgs e)
        {
            MyListView.IsVisible = false;
            btnCancelar.IsVisible = true;
            btnGuardarEditar.IsVisible = true;
            cajaDatosTelefono.IsVisible = true;
            var item = AppCliente.App.MVTelefono.ListaDeTelefonos.Find(t => t.ID == new Guid(txtIDTelefono.Text));
            txtNumeroTelefonico.Text = AppCliente.App.MVTelefono.ListaDeTelefonos.Find(t => t.ID == new Guid(txtIDTelefono.Text)).NUMERO;
            txtIDTelefono.Text = txtIDTelefono.Text;
            int query1 = AppCliente.App.MVTelefono.ListaDeTipoDeTelefono.FindIndex(t => t.StrNombreTipoDeTelefono == item.StrNombreTipoDeTelefono);
            MyPicker.SelectedIndex = query1;
            btnNuevoNumero.IsVisible = false;

        }

        public async void CargarAsync()
        {
            //lista
            HttpClient _client = new HttpClient();
            var _URL = ("http://godeliverix.net/api/Telefono/GetBuscarTelefonos?UidPropietario=" + App.Global1 + "&ParadetroDeBusqueda=Usuario&UidTelefono=00000000-0000-0000-0000-000000000000&strTelefono=");
            string DatosObtenidos = await _client.GetStringAsync(_URL);
            var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
            App.MVTelefono = JsonConvert.DeserializeObject<VMTelefono>(DatosGiros);

            for (int i = 0; i < AppCliente.App.MVTelefono.ListaDeTelefonos.Count; i++)
            {
                AppCliente.App.MVTelefono.ListaDeTelefonos[i].Estado = false;
            }

            if (App.MVTelefono.ListaDeTipoDeTelefono == null)
            {
                _client = new HttpClient();
                _URL = ("http://godeliverix.net/api/Telefono/GetObtenerTipoDetelefono");
                DatosObtenidos = await _client.GetStringAsync(_URL);
                DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
                JArray blogPostArray = JArray.Parse(DatosGiros.ToString());
                App.MVTelefono.ListaDeTipoDeTelefono = blogPostArray.Select(p => new VMTelefono
                {
                    UidTipo = (Guid)p["UidTipo"],
                    StrNombreTipoDeTelefono = (string)p["StrNombreTipoDeTelefono"]
                }).ToList();
            }

            MyPicker.ItemsSource = App.MVTelefono.ListaDeTipoDeTelefono;
            MyListView.ItemsSource = App.MVTelefono.ListaDeTelefonos;
        }

        public async void GuardarTelefono(string tipo)
        {
            VMTelefono a = (VMTelefono)MyPicker.SelectedItem;
            string Numero = txtNumeroTelefonico.Text;
            string TipoTelefono = UidTipoDetelefono.ToString();
            string NombreTipoTelefono = a.StrNombreTipoDeTelefono;
            string _Url = string.Empty;
            if (tipo == "actualizar")
            {
                _Url = "http://godeliverix.net/api/Telefono/GetActualizaTelefonoApi?UidTelefono=" + txtIDTelefono.Text + "&Numero=" + Numero + "&UidTipoDeTelefono=" + TipoTelefono + "";
                var s = await _client.GetStringAsync(_Url);
            }
            else if (tipo == "Nuevo")
            {
                Guid UidTelefono = Guid.NewGuid();
                _Url = "http://godeliverix.net/api/Telefono/GetGuardaTelefonoApi?uidUsuario=" + App.Global1 + "&Parametro=Usuario&UidTelefono=" + UidTelefono + "&Numero=" + txtNumeroTelefonico.Text + "&UidTipoDeTelefono=" + TipoTelefono + "";
                var s = await _client.GetStringAsync(_Url);
            }
            CargarAsync();
        }

        private void MyPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker obj = sender as Picker;
            if (obj.SelectedItem != null)
            {
                UidTipoDetelefono = ((VMTelefono)obj.SelectedItem).UidTipo;
            }
        }
    }
}
