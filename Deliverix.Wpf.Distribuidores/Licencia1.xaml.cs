using DeliverixSucursales;
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
namespace Deliverix.Wpf.Distribuidores
{
    /// <summary>
    /// Lógica de interacción para Licencia1.xaml
    /// </summary> 
    public partial class Licencia1 : Page
    {
        DeliverixSucursales.VMLicencia MVLicencia = new DeliverixSucursales.VMLicencia();
        public Licencia1()
        {
            InitializeComponent();
            MVLicencia.RecuperaLicencia();
            string mensaje = string.Empty;
            if (!string.IsNullOrEmpty(MVLicencia.Licencia))
            {
                mensaje = MVLicencia.Licencia.ToUpper();
                lblEstatusLicencia.Content = "ACTIVA";
            }
            else
            {
                mensaje = "Sin licencia";
                lblEstatusLicencia.Content = "";
            }
            lblLicencia.Text = mensaje;
        }

        private void btnAgregarLicencia_Click(object sender, RoutedEventArgs e)
        {
            Window1 ventana = new Window1();
            ventana.ShowDialog();
        }

        private void btnQuitarLicencia_Click(object sender, RoutedEventArgs e)
        {
            RevocarLicencia ventana = new RevocarLicencia();
            ventana.ShowDialog();
        }
    }
}
