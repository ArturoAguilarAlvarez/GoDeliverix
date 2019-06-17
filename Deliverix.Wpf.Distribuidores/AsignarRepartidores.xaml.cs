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
using System.Windows.Threading;
using System.Data;
 
namespace DeliverixSucursales
{
    /// <summary>
    /// Lógica de interacción para AsignarRepartidores.xaml
    /// </summary>
    public partial class AsignarRepartidores : Window
    {
        VMVehiculo MVVehiculo = new VMVehiculo();
        VMUsuarios MVUsuario = new VMUsuarios();
        VMLicencia MVLicencia = new VMLicencia();
        VMSucursales MVSucusales = new VMSucursales();
        public AsignarRepartidores()
        {
            InitializeComponent();
            MVLicencia.RecuperaLicencia();
            string licencia = MVLicencia.Licencia;
            MVUsuario.RepartidoresEnSucursal(licencia);
            MVVehiculo.ObtenerVehiculosDeSucursal(licencia);
            MVSucusales.ObtenerRepartidoresYVehiculos(licencia);

            foreach (VMSucursales item in MVSucusales.ListaDeRepartidoresyVehiculosEnSucursal)
            {
                MVUsuario.EliminarRepartidorDeLista(item.UidUsuario);
                MVVehiculo.EliminarDeLista(item.UidVehiculo);
            }


            DataGridRepartidores.ItemsSource = MVUsuario.LISTADEUSUARIOS;
            DataGridVehiculos.ItemsSource = MVVehiculo.ListaDeVehiculos;
            DataGridRelacionEquipo.ItemsSource = MVSucusales.ListaDeRepartidoresyVehiculosEnSucursal;
            VisibilidadDeBotones(false);
            btneliminar.IsEnabled = true;
            btnAgregar.IsEnabled = true;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            VisibilidadDeBotones(true);
            btneliminar.IsEnabled = false;
            btnAgregar.IsEnabled = true;

        }

        private void btneliminar_Click(object sender, RoutedEventArgs e)
        {
            VisibilidadDeBotones(true);
            btneliminar.IsEnabled = true;
            btnAgregar.IsEnabled = false;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (btnAgregar.IsEnabled)
            {
                if (DataGridRepartidores.SelectedIndex != -1 && DataGridVehiculos.SelectedIndex != -1)
                {
                    VMUsuarios Usuario = (VMUsuarios)DataGridRepartidores.SelectedItem;
                    VMVehiculo Vehiculo = (VMVehiculo)DataGridVehiculos.SelectedItem;
                    MVSucusales.AgregaAlistaDeRepartidores(Usuario.Uid, Vehiculo.UID);
                    DataGridRelacionEquipo.ItemsSource = MVSucusales.ListaDeRepartidoresyVehiculosEnSucursal;
                }
                else
                {
                    MessageBox.Show("No se selecciono repartidor o vehiculo", "Informacion de sistema");
                }
            }
            if (btneliminar.IsEnabled)
            {
                VMSucursales Registros = (VMSucursales)DataGridRelacionEquipo.SelectedItem;
                MVSucusales.EliminaRegistroListaRepartidores(Registros.ID);
                DataGridRelacionEquipo.ItemsSource = MVSucusales.ListaDeRepartidoresyVehiculosEnSucursal;
                btneliminar.IsEnabled = false;
            }
            string licencia = MVLicencia.Licencia;
            MVUsuario.RepartidoresEnSucursal(licencia);
            MVVehiculo.ObtenerVehiculosDeSucursal(licencia);

            foreach (VMSucursales item in MVSucusales.ListaDeRepartidoresyVehiculosEnSucursal)
            {
                MVUsuario.EliminarRepartidorDeLista(item.UidUsuario);
                MVVehiculo.EliminarDeLista(item.UidVehiculo);
            }

            DataGridRepartidores.ItemsSource = MVUsuario.LISTADEUSUARIOS;
            DataGridVehiculos.ItemsSource = MVVehiculo.ListaDeVehiculos;
            DataGridRepartidores.Items.Refresh();
            DataGridVehiculos.Items.Refresh();
            DataGridRelacionEquipo.Items.Refresh();
            VisibilidadDeBotones(false);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            VisibilidadDeBotones(false);
            btneliminar.IsEnabled = false;
            btnAgregar.IsEnabled = true;
            DataGridVehiculos.SelectedIndex = -1;
            DataGridRelacionEquipo.SelectedIndex = -1;
            DataGridRepartidores.SelectedIndex = -1;
        }

        public void VisibilidadDeBotones(bool estatus)
        {
            Visibility visibilidad = new Visibility();
            switch (estatus)
            {
                case true:
                    visibilidad = Visibility.Visible;
                    break;
                case false:
                    visibilidad = Visibility.Hidden;
                    break;
                default:
                    break;
            }
            btnAceptar.Visibility = visibilidad;
            btnCancelar.Visibility = visibilidad;
        }

        private void DataGridRelacionEquipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btneliminar.IsEnabled = true;
            btnAgregar.IsEnabled = true;
        }
    }
}
