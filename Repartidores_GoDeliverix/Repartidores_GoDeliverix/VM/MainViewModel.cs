using System.Collections.ObjectModel;
using Repartidores_GoDeliverix.Modelo;
namespace Repartidores_GoDeliverix.VM
{
    public class MainViewModel : ControlsController
    {

        #region Propierties
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        public VMLogin MVLogin { get; set; }
        public VMAjustes MVAjustes { get; set; }
        public VMAjustesTelefono MVAjustesTelefono { get; set; }
        public VMHome MVHome { get; set; }
        public VMHomeOrden MVHomeOrden { get; set; }
        public VMAjustesDireccion vmAjustesDireccion { get; set; }
        public Session Session_ { get; set; }
        private string _Nombre;

        public string Nombre
        {
            get { return _Nombre; }
            set { SetValue(ref _Nombre , value); }
        }
        

        #endregion
        #region Metodos
        public void LoadFrontEndModules()
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>();
            MenuItems.Add(new MenuItemViewModel() { Icon = "Home.png", PageName = "Home", Titulo = "INICIO" });
            MenuItems.Add(new MenuItemViewModel() { Icon = "Settings.png", PageName = "Ajustes", Titulo = "CONFIGURACION" });
        }
        #endregion
        #region Contructor
        public MainViewModel()
        {
            Instance = this;
            this.MVLogin = new VMLogin();
        }
        #endregion
        #region Singleton
        public static MainViewModel Instance;
        public static MainViewModel GetInstance()
        {
            if (Instance == null)
            {
                Instance = new MainViewModel();
            }

            return Instance;
        }
        #endregion
    }
}
