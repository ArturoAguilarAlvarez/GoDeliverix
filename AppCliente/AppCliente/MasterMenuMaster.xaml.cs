using AppCliente.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenuMaster : ContentPage
    {
        public ListView ListView;
        HttpClient _client = new HttpClient();
        public MasterMenuMaster()
        {
            InitializeComponent();

            BindingContext = new MasterMenuMasterViewModel();
            ListView = MenuItemsListView;
            cargaUsuario();
        }

        public async void cargaUsuario()
        {
            string _URL = (@"" + Helpers.Settings.sitio + "/api/Usuario/GetBuscarUsuarios?UidUsuario=" + App.Global1.ToString() + "" +
               "&UidEmpresa=00000000-0000-0000-0000-000000000000" +
               "&UIDPERFIL=4F1E1C4B-3253-4225-9E46-DD7D1940DA19");
            var DatosObtenidos = await _client.GetAsync(_URL);
            string res = await DatosObtenidos.Content.ReadAsStringAsync();
            var asd = JsonConvert.DeserializeObject<ResponseHelper>(res).Data.ToString();
            App.MVUsuarios = JsonConvert.DeserializeObject<VistaDelModelo.VMUsuarios>(asd);
            MiNombre.Text = App.MVUsuarios.StrNombre + " " + App.MVUsuarios.StrApellidoPaterno;
        }

        class MasterMenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterMenuMenuItem> MenuItems { get; set; }

            public MasterMenuMasterViewModel()
            {
                if (App.Global1 == null || App.Global1 == string.Empty)
                {
                    MenuItems = new ObservableCollection<MasterMenuMenuItem>(new[]
                    {
                    new MasterMenuMenuItem { Id = 0, Title = "Inciar sesión", TargetType = typeof(Login), UrlResource="IconoProfileMenu"},
                    new MasterMenuMenuItem { Id = 1, Title = "Busqueda", TargetType = typeof(HomePage), UrlResource="IconoHomeMenu"},
                    new MasterMenuMenuItem { Id = 3, Title = "Direcciones", TargetType = typeof(UsuarioDirecciones),UrlResource="IconoDireccionesMenu"}
                    });
                }
                else
                {
                    MenuItems = new ObservableCollection<MasterMenuMenuItem>(new[]
                   {
                    new MasterMenuMenuItem { Id = 0, Title = "Perfil", TargetType = typeof(PerfilGeneralPage), UrlResource="IconoProfileMenu"},
                    new MasterMenuMenuItem { Id = 1, Title = "Busqueda", TargetType = typeof(HomePage), UrlResource="IconoHomeMenu"},
                    new MasterMenuMenuItem { Id = 1, Title = "Monedero", TargetType = typeof(Monedero), UrlResource="Monedero"},
                    new MasterMenuMenuItem { Id = 2, Title = "Historial", TargetType = typeof(HistorialPage),UrlResource="IconoOrdenMenu"},
                    new MasterMenuMenuItem { Id = 3, Title = "Direcciones", TargetType = typeof(UsuarioDirecciones),UrlResource="IconoDireccionesMenu"},
                    new MasterMenuMenuItem { Id = 4, Title = "Telefonos", TargetType = typeof(PerfilTelefonoPage),UrlResource="IconoTelefonoHome"},
                    new MasterMenuMenuItem { Id = 6, Title = "Salir", UrlResource="LogOutIcon"}
                });
                }
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}