using AppCliente.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class UsuarioDirecciones : ContentPage
    {

        HttpClient _client = new HttpClient();


        public UsuarioDirecciones()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Iniciar();
        }

        private void MyListViewDirecciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = ((ItemTappedEventArgs)e);
            VMDireccion ObjItem = (VMDireccion)item.Item;
            if (ObjItem.Clicked)
            {
                ObjItem.Clicked = false;
            }
            else
            {
                for (int i = 0; i < AppCliente.App.MVDireccion.ListaDIRECCIONES.Count; i++)
                {
                    AppCliente.App.MVDireccion.ListaDIRECCIONES[i].Clicked = false;
                }
                ObjItem.Clicked = true;
            }
            MyListViewDirecciones.ItemsSource = null;
            MyListViewDirecciones.ItemsSource = AppCliente.App.MVDireccion.ListaDIRECCIONES;
        }

        private void BtnNuevo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DireccionCompleta(null, MyListViewDirecciones));
        }

        private async void Button_Clicked_Eliminar(object sender, EventArgs e)
        {
            var b = (MenuItem)sender;
            var obj = AppCliente.App.MVDireccion.ListaDIRECCIONES.Find(x => x.ID.ToString() == b.CommandParameter.ToString());
            var action = await DisplayAlert("Eliminar", "¿Desea eliminar la ubicación " + obj.IDENTIFICADOR + "?", "Si", "No");
            if (action)
            {
                for (int i = 0; i < AppCliente.App.MVDireccion.ListaDIRECCIONES.Count; i++)
                {
                    AppCliente.App.MVDireccion.ListaDIRECCIONES[i].Clicked = false;
                }
                EliminarDireccion(b.CommandParameter.ToString());
            }
        }
        public async void EliminarDireccion(string UidDireccion)
        {
            if (!string.IsNullOrEmpty(App.Global1))
            {
                Guid Gui = new Guid(UidDireccion);
                int index = AppCliente.App.MVDireccion.ListaDIRECCIONES.FindIndex(x => x.ID == Gui);
                AppCliente.App.MVDireccion.QuitaDireeccionDeLista(UidDireccion);
                string _Url = $"" + Helpers.Settings.sitio + "/api/Direccion/DeleteDireccionUsuario?UidDireccion=" + UidDireccion + "";
                var content = await _client.DeleteAsync(_Url);

                App.MVDireccion.ObtenerDireccionesUsuario(App.Global1);
                MyListViewDirecciones.ItemsSource = null;
                MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
            }
            else
            {
                var action = await DisplayAlert("Eliminar?", "Estas seguro de eliminar esta direccion?", "Si", "No");
                if (action)
                {
                    Helpers.Settings.UidDireccion = string.Empty;
                    Helpers.Settings.StrPAIS = string.Empty;
                    Helpers.Settings.StrESTADO = string.Empty;
                    Helpers.Settings.StrMUNICIPIO = string.Empty;
                    Helpers.Settings.StrCIUDAD = string.Empty;
                    Helpers.Settings.StrCOLONIA = string.Empty;
                    Helpers.Settings.StrCALLE0 = string.Empty;
                    Helpers.Settings.StrCALLE1 = string.Empty;
                    Helpers.Settings.StrCALLE2 = string.Empty;
                    Helpers.Settings.StrMANZANA = string.Empty;
                    Helpers.Settings.StrLongitud = string.Empty;
                    Helpers.Settings.StrLatitud = string.Empty;
                    Helpers.Settings.StrLOTE = string.Empty;
                    Helpers.Settings.StrCodigoPostal = string.Empty;
                    Helpers.Settings.StrREFERENCIA = string.Empty;
                    Helpers.Settings.StrIDENTIFICADOR = string.Empty;
                    Helpers.Settings.StrNombreCiudad = string.Empty;
                    Helpers.Settings.StrNombreColonia = string.Empty;
                    App.MVDireccion.ListaDIRECCIONES = new List<VMDireccion>();
                    MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
                }
            }
        }

        public async void Iniciar()
        {
            if (!string.IsNullOrEmpty(App.Global1))
            {
                var tex = ("" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + App.Global1);
                string strDirecciones = await _client.GetStringAsync(tex);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
            }
            else
            {
                if (!string.IsNullOrEmpty(Helpers.Settings.UidDireccion))
                {
                    App.MVDireccion.ListaDIRECCIONES = new List<VMDireccion>();
                    App.MVDireccion.ListaDIRECCIONES.Add(new VMDireccion()
                    {
                        ID = new Guid(Helpers.Settings.UidDireccion),
                        PAIS = Helpers.Settings.StrPAIS,
                        ESTADO = Helpers.Settings.StrESTADO,
                        MUNICIPIO = Helpers.Settings.StrMUNICIPIO,
                        CIUDAD = Helpers.Settings.StrCIUDAD,
                        COLONIA = Helpers.Settings.StrCOLONIA,
                        CALLE0 = Helpers.Settings.StrCALLE0,
                        CALLE1 = Helpers.Settings.StrCALLE1,
                        CALLE2 = Helpers.Settings.StrCALLE2,
                        MANZANA = Helpers.Settings.StrMANZANA,
                        LOTE = Helpers.Settings.StrLOTE,
                        CodigoPostal = Helpers.Settings.StrCodigoPostal,
                        REFERENCIA = Helpers.Settings.StrREFERENCIA,
                        IDENTIFICADOR = Helpers.Settings.StrIDENTIFICADOR,
                        NOMBRECIUDAD = Helpers.Settings.StrNombreCiudad,
                        NOMBRECOLONIA = Helpers.Settings.StrNombreColonia
                    });
                }
            }


            if (App.MVDireccion.ListaDIRECCIONES.Count == 0)
            {
                PanelSinDirecciones.IsVisible = true;
                PanelDirecciones.IsVisible = false;
            }
            else
            {
                PanelSinDirecciones.IsVisible = false;
                PanelDirecciones.IsVisible = true;
                for (int i = 0; i < App.MVDireccion.ListaDIRECCIONES.Count; i++)
                {
                    App.MVDireccion.ListaDIRECCIONES[i].Clicked = false;
                }
                MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
            }
        }

        private void BtnEditar_Clicked(object sender, EventArgs e)
        {
            var b = (MenuItem)sender;
            var obj = AppCliente.App.MVDireccion.ListaDIRECCIONES.Find(x => x.ID.ToString() == b.CommandParameter.ToString());
            Navigation.PushAsync(new DireccionCompleta(obj, MyListViewDirecciones));
        }
    }
}