using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        public MasterMenuMaster()
        {
            InitializeComponent();

            BindingContext = new MasterMenuMasterViewModel();
            ListView = MenuItemsListView;
            App.MVUsuarios.obtenerUsuario(App.Global1);
            MiNombre.Text = App.MVUsuarios.StrNombre+" "+ App.MVUsuarios.StrApellidoPaterno;
        }

        class MasterMenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterMenuMenuItem> MenuItems { get; set; }
            
            public MasterMenuMasterViewModel()
            {

                MenuItems = new ObservableCollection<MasterMenuMenuItem>(new[]
{
                    new MasterMenuMenuItem { Id = 0, Title = "MI GoDeliverix", TargetType = typeof(PerfilGeneralPage), UrlResource="IconoProfileMenu"},
                    new MasterMenuMenuItem { Id = 1, Title = "Home", TargetType = typeof(HomePage), UrlResource="IconoHomeMenu"},
                    new MasterMenuMenuItem { Id = 2, Title = "Mi Historial", TargetType = typeof(HistorialPage),UrlResource="IconoOrdenMenu"},
                    new MasterMenuMenuItem { Id = 3, Title = "Mis Direcciones", TargetType = typeof(UsuarioDirecciones),UrlResource="IconoDireccionesMenu"},
                    new MasterMenuMenuItem { Id = 4, Title = "Mis Telefonos", TargetType = typeof(PerfilTelefonoPage),UrlResource="IconoTelefonoHome"},
                    new MasterMenuMenuItem { Id = 6, Title = "Salir", UrlResource="IconoSalir"},
                });
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