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

namespace AppPuestoTacos.View
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
            txtNombreUsuario.Text = App.NOmbreUsuario;
            txtNombreSucursal.Text = AppPuestoTacos.Helpers.Settings.NombreSucursal;
            txtNombreEmpresa.Text = App.NombreEmpresa;
        }

        class MasterMenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterMenuMenuItem> MenuItems { get; set; }
            
            public MasterMenuMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterMenuMenuItem>(new[]
                {
                    new MasterMenuMenuItem { Id = 0, Title = "Ordenes Recibidas", TargetType = typeof(OrdenesRecibidas),UrlResource="IconoOrdenesRecibidas"},
                    new MasterMenuMenuItem { Id = 1, Title = "Ordenes en preparacion", TargetType = typeof(OrdenesEnPreparacion),UrlResource="IconoOrdenesEnPreparacion" },
                    new MasterMenuMenuItem { Id = 2, Title = "Ordenes para entregar", TargetType = typeof(OrdenesEntrega),UrlResource="IconoOrdenesParaEntregar" },
                    new MasterMenuMenuItem { Id = 3, Title = "Ordenes canceladas",TargetType = typeof(OrdenesCanceladas),UrlResource="IconoOrdenesCanceladas" },
                    new MasterMenuMenuItem { Id = 4, Title = "Ordenes canceladas permanente",TargetType = typeof(OrdenesCanceladasPermanente),UrlResource="IconoOrdenesCanceladas" },
                    new MasterMenuMenuItem { Id = 5, Title = "Configuracion", TargetType = typeof(Configuracion), UrlResource = "IconoConfigurar" },
                new MasterMenuMenuItem { Id = 6, Title = "Salir", UrlResource="IconoSalir"},
                });

                if (AppPuestoTacos.Helpers.Settings.Perfil!= "76a96ff6-e720-4092-a217-a77a58a9bf0d")
                {
                    MenuItems.RemoveAt(5);
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