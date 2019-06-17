using DeliverixSucursales;
using System;
using System.Windows;
using System.Windows.Controls;
namespace Deliverix.Wpf.Distribuidores
{
    /// <summary>
    /// Lógica de interacción para Licencia1.xaml
    /// </summary> 
    public partial class Licencia1 : Page
    {
        DeliverixSucursales.VMLicencia MVLicencia = new DeliverixSucursales.VMLicencia();
        VistaDelModelo.VMLicencia HostLicencia = new VistaDelModelo.VMLicencia();
        public bool ConfirmacionSupervisor;
        Main PaginaPrincipal;
        public Licencia1(Main Menu)
        {
            PaginaPrincipal = Menu;
            InitializeComponent();
            MVLicencia.RecuperaLicencia();
            if (!string.IsNullOrEmpty(MVLicencia.Licencia))
            {
                if (!HostLicencia.VerificaEstatusDeLicenciaSucursal(MVLicencia.Licencia))
                {

                    MVLicencia.EliminarLicencia();
                }

            }
            ActualizarTextosDeLicencia();
        }

        private void btnAgregarLicencia_Click(object sender, RoutedEventArgs e)
        {
            ConfirmacionSupervisor = false;
            Acceso Login = new Acceso("Administrador", ventanaLicencia: this);
            Login.ShowDialog();

            if (ConfirmacionSupervisor)
            {
                Window1 ventana = new Window1();
                ventana.ShowDialog();
                PaginaPrincipal.HabilitaBotones();
                ActualizarTextosDeLicencia();
            }
        }

        private void btnQuitarLicencia_Click(object sender, RoutedEventArgs e)
        {
            MVLicencia.RecuperaLicencia();
            if (!string.IsNullOrEmpty(MVLicencia.Licencia))
            {
                ConfirmacionSupervisor = false;
                Acceso Login = new Acceso("Administrador", ventanaLicencia: this);
                Login.ShowDialog();

                if (ConfirmacionSupervisor)
                {
                    RevocarLicencia ventana = new RevocarLicencia(this);
                    ventana.ShowDialog();
                    PaginaPrincipal.HabilitaBotones();
                    ActualizarTextosDeLicencia();
                }
            }
        }

        public void ActualizarTextosDeLicencia()
        {
            MVLicencia.RecuperaLicencia();
            string mensaje = string.Empty;
            if (!string.IsNullOrEmpty(MVLicencia.Licencia) && MVLicencia.Licencia != Guid.Empty.ToString())
            {
                mensaje = MVLicencia.Licencia.ToUpper();
                lblEstatusLicencia.Content = "ACTIVA";

                btnAgregarLicencia.Visibility = Visibility.Hidden;
                btnQuitarLicencia.Visibility = Visibility.Visible;
            }
            else
            {
                mensaje = Guid.Empty.ToString();
                lblEstatusLicencia.Content = "Sin licencia";

                btnAgregarLicencia.Visibility = Visibility.Visible;
                btnQuitarLicencia.Visibility = Visibility.Hidden;
            }
            lblLicencia.Text = mensaje;
        }
    }
}
