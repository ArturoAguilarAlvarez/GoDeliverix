using DeliverixSucursales;
using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VistaDelModelo;

namespace Deliverix.Wpf.Distribuidores
{
    /// <summary>
    /// Lógica de interacción para Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        VistaDelModelo.VMLicencia HostingLicencia = new VistaDelModelo.VMLicencia();
        DeliverixSucursales.VMLicencia MVLicencia = new DeliverixSucursales.VMLicencia();
        VMSucursales MVSucursal = new VMSucursales();
        VMEmpresas MVEmpresa = new VMEmpresas();
        VMOrden MVOrden = new VMOrden();
        VMAcceso MVAcceso = new VMAcceso();
        public string SourceRegistro { get; set; }

        public Main()
        {
            //Limpia los valores de las conexiones
            //Properties.Settings.Default.Reset();
            //Properties.Settings.Default.Reload();


            if (AccesoInternet())
            {
                SourceRegistro = string.Empty;
                try
                {
                    SourceRegistro = Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixDistribuidores", "Source", "").ToString();
                }
                catch (Exception)
                {
                    SourceRegistro = string.Empty;
                }
                if (!string.IsNullOrEmpty(SourceRegistro))
                {
                    if (PruebaConexionRegistro(SourceRegistro))
                    {
                        Properties.Settings.Default["Source"] = SourceRegistro;
                        if (Application.Current.Windows.OfType<Main>().Any())
                        {
                            Application.Current.Windows.OfType<Main>().First().Activate();
                        }
                        else
                        {
                            InitializeComponent();
                            HabilitaBotones();
                        }
                    }
                    else
                    {
                        DataBase wBDLocal = new DataBase();
                        wBDLocal.Show();
                    }
                }
                else
                {
                    DataBase wBDLocal = new DataBase();
                    wBDLocal.Show();
                }
            }

        }

        private bool PruebaConexionRegistro(string sourceRegistro)
        {
            int intentos = 3;
            bool aux = false;
            SqlConnection _Conexion;
            string CadenaConexion = string.Empty;
            CadenaConexion = @"Data Source =" + sourceRegistro + ";Initial Catalog =DeliverixDistribuidores;Integrated Security=True;";

            for (int i = 0; i < intentos; i++)
            {
                try
                {
                    _Conexion = new SqlConnection(CadenaConexion);
                    _Conexion.Open();
                    aux = true;
                    _Conexion.Close();
                    break;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return aux;
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

        public void HabilitaBotones()
        {
            if (AccesoInternet())
            {
                //Inhabilita los botones del menu y de las opciones de la ventana dialog            
                btnordenes.IsEnabled = false;
                btnReportes.IsEnabled = false;
                btnRepartidores.IsEnabled = false;
                btnMenuPrincipal.IsEnabled = false;
                btnConfiguracion.Visibility = Visibility.Hidden;
                btnAyuda.Visibility = Visibility.Hidden;
                btnCerrarSesion.Visibility = Visibility.Hidden;
                LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
                //Verifica la existencia de la licencia de manera local
                if (MVLicencia.VerificaExistenciaDeLicenciaLocal().Rows.Count < 1)
                {
                    btnInciarSesion.IsEnabled = false;
                    btnLicencias.IsEnabled = true;
                }
                else
                {
                    MVLicencia.RecuperaLicencia();
                    //Valida si no existe en la nube
                    if (!HostingLicencia.ValidaExistenciaDeLicencia(MVLicencia.Licencia))
                    {
                        btnInciarSesion.IsEnabled = false;
                        btnLicencias.IsEnabled = true;
                    }
                    else
                    {
                        //Verifica si el estatus esta activo
                        if (!HostingLicencia.VerificaEstatusDeLicenciaSucursal(MVLicencia.Licencia))
                        {
                            btnInciarSesion.IsEnabled = false;
                            btnLicencias.IsEnabled = true;


                            Label lblMensaje = VentanaMensaje.FindName("lblMensaje") as Label;
                            lblMensaje.Content = "Licencia inactiva!";
                            VentanaMensaje.ShowDialog();
                        }
                        else
                        {
                            string sucursal = MVSucursal.ObtenSucursalDeLicencia(MVLicencia.Licencia);
                            if (!MVSucursal.VerificaEstatusSucursal(sucursal))
                            {
                                btnInciarSesion.IsEnabled = false;
                                btnLicencias.IsEnabled = true;
                                //Manda mensaje en pantalla

                                Label lblMensaje = VentanaMensaje.FindName("lblMensaje") as Label;
                                lblMensaje.Content = "Sucursal inactiva!";
                                VentanaMensaje.ShowDialog();
                                Hide();
                            }
                            else
                            {
                                String empresa = MVSucursal.ObtenerUidEmpresa(sucursal).ToString();
                                if (!MVEmpresa.VerificaEstatusEmpresa(empresa))
                                {
                                    //Muestra ventnaa de licencia si no esta activa la empresa

                                    Label lblMensaje = VentanaMensaje.FindName("lblMensaje") as Label;
                                    lblMensaje.Content = "Empresa inactiva!";
                                    VentanaMensaje.Show();
                                    Hide();
                                }
                                else
                                {
                                    btnInciarSesion.IsEnabled = true;
                                    btnLicencias.IsEnabled = true;


                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnPopupSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnCerrarMenu_Click(object sender, RoutedEventArgs e)
        {
            btnAbrirMenu.Visibility = Visibility.Visible;
            btnCerrarMenu.Visibility = Visibility.Collapsed;
            lblDeliverix.Visibility = Visibility.Collapsed;
        }

        private void btnAbrirMenu_Click(object sender, RoutedEventArgs e)
        {
            btnAbrirMenu.Visibility = Visibility.Collapsed;
            btnCerrarMenu.Visibility = Visibility.Visible;
            lblDeliverix.Visibility = Visibility.Visible;
        }

        private void btnInciarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (AccesoInternet())
            {
                Acceso ventana = new Acceso("Supervisor", VentanaPrincipal: this);
                ventana.ShowDialog();
                HabilitaBotones();
                if (lblUidusuario.Content != null)
                {
                    if (!string.IsNullOrEmpty(lblUidusuario.Content.ToString()))
                    {
                        btnordenes.IsEnabled = true;
                        btnMenuPrincipal.IsEnabled = true;
                        btnReportes.IsEnabled = true;
                        btnRepartidores.IsEnabled = true;
                        btnConfiguracion.Visibility = Visibility.Visible;
                        btnAyuda.Visibility = Visibility.Visible;
                        btnCerrarSesion.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void btnordenes_Click(object sender, RoutedEventArgs e)
        {
            FrameContenido.Content = new Ordenes1();
        }

        private void btnRepartidores_Click(object sender, RoutedEventArgs e)
        {
            FrameContenido.Source = new Uri("Repartidores.xaml", UriKind.Relative);
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnLicencias_Click(object sender, RoutedEventArgs e)
        {
            FrameContenido.Content = new PageLicencia(this);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnMenuPrincipal_Click(object sender, RoutedEventArgs e)
        {
            FrameContenido.Source = null;
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (AccesoInternet())
            {
                MVAcceso.BitacoraRegistroSupervisores(new Guid(lblUidusuario.Content.ToString()), new Guid("83D5135E-95A4-4FFB-8F74-B6BAC980DFA3"));
                lblUidusuario.Content = string.Empty;
                txtUsuario.Text = string.Empty;
                txtSucursal.Text = string.Empty;
                HabilitaBotones();
                if (lblUidusuario.Content != null)
                {
                    if (!string.IsNullOrEmpty(lblUidusuario.Content.ToString()))
                    {
                        btnordenes.IsEnabled = true;
                        btnReportes.IsEnabled = true;
                        btnMenuPrincipal.IsEnabled = true;
                        btnConfiguracion.Visibility = Visibility.Visible;
                        btnAyuda.Visibility = Visibility.Visible;
                        btnCerrarSesion.Visibility = Visibility.Visible;
                    }
                }
                FrameContenido.Source = null;
            }
        }
    }
}
