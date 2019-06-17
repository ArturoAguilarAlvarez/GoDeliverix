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
    /// Lógica de interacción para CancelarOrden.xaml
    /// </summary>
    public partial class CancelarOrden : Window
    {
        TabControl control;
        VMOrden MVOrden;
        VMLicencia MVLicencia;
        VMMensaje MVMensaje;
        public CancelarOrden(TabControl componente)
        {
            InitializeComponent();
            control = componente;
            MVMensaje = new VMMensaje();
            MVLicencia = new VMLicencia();
            MVLicencia.RecuperaLicencia();
            MVMensaje.Buscar(strLicencia:MVLicencia.Licencia);
            cmbMensaje.ItemsSource = MVMensaje.ListaDeMensajes;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //Valida si esta seleccionado un elemento en el combobox del mensaje
            if (cmbMensaje.SelectedIndex != -1)
            {
                MVOrden = new VMOrden();
                MVLicencia = new VMLicencia();
                MVLicencia.RecuperaLicencia();

                VMMensaje oMensaje = (VMMensaje)cmbMensaje.SelectedValue;
                //Valida si fue llamado desde la ventana de ordenes recibidas
                if (control.SelectedIndex == 0)
                {
                    TabItem elemento = (TabItem)control.SelectedItem;
                    TextBlock txtOrden = (TextBlock)elemento.FindName("txtCNumeroOrden");
                    MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EAE7A7E6-3F19-405E-87A9-3162D36CE21B"), "S", MVLicencia.Licencia, LngFolio: long.Parse(txtOrden.Text), UidMensaje: oMensaje.Uid);
                }
                //Valida si fue llamado desde la ventana de ordenes confirmadas
                if (control.SelectedIndex == 1)
                {
                    TabItem elemento = (TabItem)control.SelectedItem;
                    TextBlock txtOrden = (TextBlock)elemento.FindName("txbNumerodeOrden");
                    MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EAE7A7E6-3F19-405E-87A9-3162D36CE21B"), "S", MVLicencia.Licencia, LngFolio: long.Parse(txtOrden.Text), UidMensaje: oMensaje.Uid);
                }
                Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un mensaje para poder cancelar una orden");
            }
          
        }
    }
}
