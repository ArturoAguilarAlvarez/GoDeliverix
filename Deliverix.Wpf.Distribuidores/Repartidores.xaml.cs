using System;
using System.Windows;
using System.Windows.Controls;
using VistaDelModelo;

namespace Deliverix.Wpf.Distribuidores
{
    /// <summary>
    /// Lógica de interacción para Repartidores.xaml
    /// </summary>
    public partial class Repartidores : Page
    {
        VMVehiculo MVVehiculo = new VMVehiculo();
        VMUsuarios MVUsuario = new VMUsuarios();
        DeliverixSucursales.VMLicencia MVLicencia = new DeliverixSucursales.VMLicencia();
        VMSucursales MVSucusales = new VMSucursales();

        public Repartidores()
        {
            if (AccesoInternet())
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
                CargaDataGrid("Repartidores");
                CargaDataGrid("Vehiculos");
                CargaDataGrid("Bitacora");
                VisibilidadDeBotones(false);
                btneliminar.IsEnabled = false;
                btnAgregar.IsEnabled = false;
            }
        }

        private bool AccesoInternet()
        {
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.godeliverix.net");
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Sin conexion", "No hay conexion a internet");
                return false;
            }
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
            if (AccesoInternet())
            {
                if (btnAgregar.IsEnabled)
                {
                    if (DataGridRepartidores.SelectedIndex != -1 && DataGridVehiculos.SelectedIndex != -1)
                    {
                        VMUsuarios Usuario = (VMUsuarios)DataGridRepartidores.SelectedItem;
                        VMVehiculo Vehiculo = (VMVehiculo)DataGridVehiculos.SelectedItem;
                        MVSucusales.AgregaAlistaDeRepartidores(Usuario.Uid, Vehiculo.UID);
                        DataGridRepartidores.SelectedIndex = -1;
                        DataGridVehiculos.SelectedIndex = -1;
                        btnAgregar.IsEnabled = false;
                        btneliminar.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("No se selecciono repartidor o vehiculo", "Mensaje de sistema");
                    }
                }
                if (btneliminar.IsEnabled)
                {
                    if (DataGridRelacionEquipo.SelectedIndex != -1)
                    {
                        VMSucursales Registros = (VMSucursales)DataGridRelacionEquipo.SelectedItem;
                        MVSucusales.EliminaRegistroListaRepartidores(Registros.ID);
                        DataGridRelacionEquipo.SelectedIndex = -1;
                        btnAgregar.IsEnabled = false;
                        btneliminar.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Relacion no seleccionada", "Mensaje de sistema");
                    }
                }
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
                CargaDataGrid("Repartidores");
                CargaDataGrid("Vehiculos");
                CargaDataGrid("Bitacora");
                VisibilidadDeBotones(false);
            }
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
            btnAgregar.IsEnabled = false;
        }

        protected void CargaDataGrid(string STRGRID)
        {
            switch (STRGRID)
            {
                case "Vehiculos":
                    DataGridVehiculos.ItemsSource = MVVehiculo.ListaDeVehiculos;
                    DataGridVehiculos.Items.Refresh();
                    break;
                case "Repartidores":
                    DataGridRepartidores.ItemsSource = MVUsuario.LISTADEUSUARIOS;
                    DataGridRepartidores.Items.Refresh();
                    break;
                case "Bitacora":
                    DataGridRelacionEquipo.ItemsSource = MVSucusales.ListaDeRepartidoresyVehiculosEnSucursal;
                    DataGridRelacionEquipo.Items.Refresh();
                    break;
                default:
                    break;
            }
        }

        private void DataGridRepartidores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRepartidores.SelectedIndex != -1 && DataGridVehiculos.SelectedIndex != -1)
            {
                btnAgregar.IsEnabled = true;
                btneliminar.IsEnabled = false;
            }
            else
            {
                btnAgregar.IsEnabled = false;
                btneliminar.IsEnabled = false;
            }
        }

        private void DataGridVehiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRepartidores.SelectedIndex != -1 && DataGridVehiculos.SelectedIndex != -1)
            {
                btnAgregar.IsEnabled = true;
                btneliminar.IsEnabled = false;
            }
            else
            {
                btnAgregar.IsEnabled = false;
                btneliminar.IsEnabled = false;
            }
        }

        private void btnInformacionDeTrabajo_Click(object sender, RoutedEventArgs e)
        {
            object ID = ((Button)sender).CommandParameter;
            VMSucursales registro = MVSucusales.ListaDeRepartidoresyVehiculosEnSucursal.Find(o => o.ID.ToString() == ID.ToString());
            DHInformacionRepartidor.IsOpen = true;
            DHInformacionRepartidor.DataContext = registro;

        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            object ID = ((Button)sender).CommandParameter;
            VMSucursales registro = MVSucusales.ListaDeRepartidoresyVehiculosEnSucursal.Find(o => o.ID.ToString() == ID.ToString());
            MessageBox.Show(registro.MFondo.ToString());
            MVSucusales.ModificaInformacionDeTrabajoDeRepartidor(ID.ToString(), registro.MFondo);
            MVLicencia.RecuperaLicencia();
            string licencia = MVLicencia.Licencia;
            MVSucusales.ObtenerRepartidoresYVehiculos(licencia);
            CargaDataGrid("Bitacora");
        }

        private void btnCancelarInformacion_Click(object sender, RoutedEventArgs e)
        {
            MVLicencia.RecuperaLicencia();
            string licencia = MVLicencia.Licencia;
            MVSucusales.ObtenerRepartidoresYVehiculos(licencia);
            CargaDataGrid("Bitacora");
        }
    }
}
