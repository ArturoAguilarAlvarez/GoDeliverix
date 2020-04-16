using GoDeliverix_TodasLasSucursales.VM;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoDeliverix_TodasLasSucursales
{
    /// <summary>
    /// Interaction logic for ConfiguracionSucursal.xaml
    /// </summary>
    public partial class ConfiguracionSucursal : Page
    {
        MainWindow ventana;
        public ConfiguracionSucursal(MainWindow main)
        {
            InitializeComponent();
            var locatorMVVM = ControlGeneral.GetInstance();
            if (locatorMVVM.MVSucursalesLocal == null)
            {
                locatorMVVM.MVSucursalesLocal = new VMSucursalesLocal();
            }
            else
            {
                locatorMVVM.MVSucursalesLocal.ObtenSucursales();
            }
            DataContext = locatorMVVM.MVSucursalesLocal;
            
            ventana = main;
        }

        private void btnAgregarLicencia_Click(object sender, RoutedEventArgs e)
        {
            var vent = ventana.FindName("dhAgregarLicencia") as DialogHost;
            vent.IsOpen = true;
        }


        
    }
}
