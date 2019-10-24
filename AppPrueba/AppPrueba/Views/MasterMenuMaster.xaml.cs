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

namespace AppPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenuMasterMaster : ContentPage
    {
        public ListView ListView;

        public MasterMenuMasterMaster()
        {
            InitializeComponent();

            BindingContext = new MasterMenuMasterMasterViewModel();
            ListView = MenuItemsListView;

            ListView = MenuItemsListView;
            txtNombreUsuario.Text = App.NOmbreUsuario;
            txtNombreSucursal.Text = AppPrueba.Helpers.Settings.NombreSucursal;
            txtNombreEmpresa.Text = App.NombreEmpresa;
        }

        class MasterMenuMasterMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterMenuMenuItemMenuItem> MenuItems { get; set; }

            public MasterMenuMasterMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterMenuMenuItemMenuItem>(new[]
                {
                    new MasterMenuMenuItemMenuItem { Id = 0, Title = "Ordenes Recibidas", TargetType = typeof(OrdenesRecibidas),UrlResource="IconoOrdenesRecibidas"},
                    new MasterMenuMenuItemMenuItem { Id = 1, Title = "Ordenes en preparacion", TargetType = typeof(OrdenesEnPreparacion),UrlResource="IconoOrdenesEnPreparacion" },
                    new MasterMenuMenuItemMenuItem { Id = 2, Title = "Ordenes para entregar", TargetType = typeof(OrdenesEntrega),UrlResource="IconoOrdenesParaEntregar" },
                    new MasterMenuMenuItemMenuItem { Id = 3, Title = "Ordenes canceladas",TargetType = typeof(OrdenesCanceladasPermanente),UrlResource="IconoOrdenesCanceladas" },
                    new MasterMenuMenuItemMenuItem { Id = 3, Title = "Turno",TargetType = typeof(Turno),UrlResource="IconoTurno" },
                    //new MasterMenuMenuItemMenuItem { Id = 4, Title = "Ordenes canceladas permanente",TargetType = typeof(OrdenesCanceladasPermanente),UrlResource="IconoOrdenesCanceladas" },
                    new MasterMenuMenuItemMenuItem { Id = 5, Title = "Configuracion", TargetType = typeof(Configuracion), UrlResource = "IconoConfigurar" },
                    new MasterMenuMenuItemMenuItem { Id = 6, Title = "Salir", UrlResource="IconoSalir"},
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