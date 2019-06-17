using Deliverix.Wpf.Distribuidores;
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
using System.Windows.Shapes;
using VistaDelModelo;
namespace DeliverixSucursales
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        VMLicencia MVLicencia = new VMLicencia();
        VMEmpresas MVEmpresa = new VMEmpresas();
        VMSucursales MVSucursal = new VMSucursales();
        VistaDelModelo.VMLicencia HostingMvLicencia = new VistaDelModelo.VMLicencia();

        public Window1()
        {
            InitializeComponent();
        }

        private void btnGuardarPassword_Click(object sender, RoutedEventArgs e)
        {
            string licencia = txtLicencia1.Text.Trim();
            if (licencia.Length == 36)
            {
                if (MVSucursal.ObtenerElTipoDeSucursal(licencia))
                {
                    if (HostingMvLicencia.VerificaDisponibilidad(licencia))
                    {
                        MVLicencia.EliminarLicencia();
                        MVLicencia.GuardarLicencia(licencia);
                        HostingMvLicencia.CambiaDisponibilidadDeLicencia(licencia);
                        Close();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Licencia no disponible!!");
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("La licencia no pertenece a una sucursal suministradora!!");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Campos invalidos!!");
                txtLicencia1.Focus();
            }
        }
    }
}
