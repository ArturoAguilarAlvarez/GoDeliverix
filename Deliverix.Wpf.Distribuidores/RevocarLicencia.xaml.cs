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
using VistaDelModelo;

namespace DeliverixSucursales
{
    /// <summary>
    /// Interaction logic for RevocarLicencia.xaml
    /// </summary>
    public partial class RevocarLicencia : Window
    {
        VMLicencia MVLicencia = new VMLicencia();
        VistaDelModelo.VMLicencia hostingLicencia = new VistaDelModelo.VMLicencia();
        public RevocarLicencia()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            
            MVLicencia.RecuperaLicencia();
            hostingLicencia.ActualizarLicenciaSucursal(UidLicencia: new Guid(MVLicencia.Licencia),bdisponibilidad:false);
            MVLicencia.EliminarLicencia();
            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
