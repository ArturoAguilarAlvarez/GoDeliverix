using AppCliente.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public partial class PerfilDireccionesPage2 : ContentPage
    {
        HttpClient _client = new HttpClient();

        public PerfilDireccionesPage2()
        {
            InitializeComponent();


            for (int i = 0; i < App.MVDireccion.ListaDIRECCIONES.Count; i++)
            {
                App.MVDireccion.ListaDIRECCIONES[i].Clicked = false;
            }

            MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;

        }

        private void MyListViewDirecciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e;
            VMDireccion ObjItem = (VMDireccion)item.Item;
            if (ObjItem.Clicked)
            {
                ObjItem.Clicked = false;
                txtIDDireccionn.Text = "0";
            }
            else
            {
                for (int i = 0; i < App.MVDireccion.ListaDIRECCIONES.Count; i++)
                {
                    App.MVDireccion.ListaDIRECCIONES[i].Clicked = false;
                }
                ObjItem.Clicked = true;
                txtIDDireccionn.Text = ObjItem.ID.ToString();
            }
            MyListViewDirecciones.ItemsSource = null;
            MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
        }

        private void BtnNuevo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeleccionTuUbicacionMapa(MyListViewDirecciones));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_Eliminar(object sender, EventArgs e)
        {
            EliminarDireccion();
        }

        private void Button_Clicked_Editar(object sender, EventArgs e)
        {
            var item = sender as Button;
            var ObjItem = item.BindingContext as VMDireccion;

            Navigation.PushAsync(new SeleccionTuUbicacionMapa(ObjItem));
        }

        public async void EliminarDireccion()
        {
            if (txtIDDireccionn.Text != "0" && !string.IsNullOrEmpty(txtIDDireccionn.Text))
            {
                var action = await DisplayAlert("Eliminar?", "Estas seguro de eliminar esta direccion?", "Si", "No");
                if (action)
                {
                    Guid Gui = new Guid(txtIDDireccionn.Text);
                    int index = App.MVDireccion.ListaDIRECCIONES.FindIndex(x => x.ID == Gui);
                    //Elimina direccion
                    var tex = ("" + Helpers.Settings.sitio + "/api/Direccion/DeleteDireccionUsuario?UidDireccion=" + txtIDDireccionn.Text);
                    string strDirecciones = await _client.GetStringAsync(tex);
                    //Obtiene direcciones
                    tex = ("" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + App.Global1);
                    strDirecciones = await _client.GetStringAsync(tex);
                    var obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                    App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
                    MyListViewDirecciones.ItemsSource = null;
                    MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
                    txtIDDireccionn.Text = "0";
                }
            }
        }
    }
}