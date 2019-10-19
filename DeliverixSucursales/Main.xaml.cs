using DeliverixSucursales;
using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using VistaDelModelo;
using Application = System.Windows.Application;
using LibPrintTicket;
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
        VMInstalacion MVInstalacion = new VMInstalacion();
        public string SourceRegistro { get; set; }
        public Main()
        {
            if (AccesoInternet())
            {
                //Limpia los valores de las conexiones
                //DeliverixSucursales.Properties.Settings.Default.Reset();
                //DeliverixSucursales.Properties.Settings.Default.Reload();

                SourceRegistro = string.Empty;
                try
                {
                    SourceRegistro = Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixSuministradora", "Source", "").ToString();
                }
                catch (Exception)
                {
                    SourceRegistro = string.Empty;
                }
                if (!string.IsNullOrEmpty(SourceRegistro))
                {
                    if (PruebaConexionRegistro(SourceRegistro))
                    {
                        InitializeComponent();
                        if (Application.Current.Windows.OfType<Main>().Any())
                        {
                            Application.Current.Windows.OfType<Main>().First().Activate();
                            HabilitaBotones();
                        }
                        else
                        {
                            HabilitaBotones();
                        }
                        DeliverixSucursales.Properties.Settings.Default["Source"] = SourceRegistro;

                        VMTurno MVTurno = new VMTurno();
                        MVLicencia = new DeliverixSucursales.VMLicencia();
                        MVLicencia.RecuperaLicencia();
                        MVTurno.ConsultarUltimoTurnoSuministradora(MVLicencia.Licencia);

                        if (MVTurno.DtmHoraFin == DateTime.MinValue && MVTurno.DtmHoraInicio != DateTime.MinValue)
                        {
                            lblHoraInicioTurno.Content = MVTurno.DtmHoraInicio;
                            lblFolioTurno.Content = MVTurno.LngFolio;
                            lblUidusuario.Content = MVTurno.UidUsuario;
                            LblUidTurno.Content = MVTurno.UidTurno;
                            VMUsuarios MVUsuario = new VMUsuarios();
                            MVUsuario.obtenerDatosDeSupervisor(MVTurno.UidUsuario);
                            txtUsuario.Text = MVUsuario.StrNombre;
                            txtSucursal.Text = MVUsuario.Sucursal;
                            lblEmpresa.Text = MVUsuario.NombreEmpresa;
                            if (!string.IsNullOrEmpty(lblUidusuario.Content.ToString()))
                            {
                                btnordenes.IsEnabled = true;
                                btnReportes.IsEnabled = true;
                                btnConfiguracion.Visibility = Visibility.Visible;
                                btnAyuda.Visibility = Visibility.Visible;
                                btnCerrarSesion.Visibility = Visibility.Visible;
                            }
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
            CadenaConexion = @"Data Source =" + sourceRegistro + ";Initial Catalog =GoDeliverixSuministradora;Integrated Security=True;";

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

        public void Redireccionar()
        {
            try
            {

                string Source = DeliverixSucursales.Properties.Settings.Default["Source"].ToString();

                if (string.IsNullOrEmpty(Source))
                {
                    DataBase wBDLocal = new DataBase();
                    wBDLocal.Show();
                }
            }
            catch (Exception e) { System.Windows.MessageBox.Show(e.Message); this.Close(); }
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
                System.Windows.MessageBox.Show("Sin conexion", "No hay conexion a internet");
                return false;
            }
        }

        private void BtnProbar_Click(object sender, RoutedEventArgs e)
        {
            if (AccesoInternet())
            {
                System.Windows.Forms.MessageBox.Show("Acceso a internet:)");
                //Instrucciones en caso de tener acceso a internet
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Sin acceso a internet: (");
                //Instrucciones en caso de no tener acceso a internet
            }
        }

        public void HabilitaBotones()
        {
            if (AccesoInternet())
            {
                VistaDelModelo.VMLicencia HostingLicencia = new VistaDelModelo.VMLicencia();
                DeliverixSucursales.VMLicencia MVLicencia = new DeliverixSucursales.VMLicencia();
                VMSucursales MVSucursal = new VMSucursales();
                VMEmpresas MVEmpresa = new VMEmpresas();
                VMOrden MVOrden = new VMOrden();
                //Inhabilita los botones del menu y de las opciones de la ventana dialog            
                btnordenes.IsEnabled = false;
                btnReportes.IsEnabled = false;
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
                    try
                    {
                        MVLicencia.RecuperaLicencia();
                        //Valida si no existe en la nube
                        try
                        {
                            if (!HostingLicencia.ValidaExistenciaDeLicencia(MVLicencia.Licencia))
                            {
                                btnInciarSesion.IsEnabled = false;
                                btnLicencias.IsEnabled = true;
                            }
                            else
                            {
                                try
                                {
                                    if (!HostingLicencia.VerificaEstatusDeLicenciaSucursal(MVLicencia.Licencia))
                                    {
                                        btnInciarSesion.IsEnabled = false;
                                        btnLicencias.IsEnabled = true;
                                        System.Windows.Forms.MessageBox.Show("Licencia inactiva!");
                                        Hide();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            string sucursal = MVSucursal.ObtenSucursalDeLicencia(MVLicencia.Licencia);
                                            if (!MVSucursal.VerificaEstatusSucursal(sucursal))
                                            {
                                                btnInciarSesion.IsEnabled = false;
                                                btnLicencias.IsEnabled = true;
                                                //Manda mensaje en pantalla
                                                System.Windows.Forms.MessageBox.Show("Sucursal inactiva!");
                                                Hide();
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    String empresa = MVSucursal.ObtenerUidEmpresa(sucursal).ToString();
                                                    if (!MVEmpresa.VerificaEstatusEmpresa(empresa))
                                                    {
                                                        //Muestra ventnaa de licencia si no esta activa la empresa
                                                        System.Windows.Forms.MessageBox.Show("Empresa inactiva!");
                                                        Hide();
                                                    }
                                                    else
                                                    {
                                                        btnInciarSesion.IsEnabled = true;
                                                        btnLicencias.IsEnabled = true;
                                                    }
                                                }
                                                catch (Exception)
                                                {

                                                    System.Windows.Forms.MessageBox.Show("No obtiene el estatus de la empresa");
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        {

                                            System.Windows.Forms.MessageBox.Show("No valida estatus sucursal");
                                        }
                                    }
                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            System.Windows.Forms.MessageBox.Show("No se conecta al host para validar existencia de licencia");
                        }
                    }
                    catch (Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("No se pudo recuperar la licencia");
                    }
                }
            }
        }

        private void BtnPopupSalir_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void BtnCerrarMenu_Click(object sender, RoutedEventArgs e)
        {
            btnAbrirMenu.Visibility = Visibility.Visible;
            btnCerrarMenu.Visibility = Visibility.Collapsed;
            lblDeliverix.Visibility = Visibility.Collapsed;
        }

        private void BtnAbrirMenu_Click(object sender, RoutedEventArgs e)
        {
            btnAbrirMenu.Visibility = Visibility.Collapsed;
            btnCerrarMenu.Visibility = Visibility.Visible;
            lblDeliverix.Visibility = Visibility.Visible;
        }

        private void BtnInciarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (AccesoInternet())
            {
                Acceso ventana = new Acceso("Supervisor", VentanaPrincipal: this);
                ventana.ShowDialog();
                HabilitaBotones();
                if (lblUidusuario.Content != null)
                {
                    MVLicencia.RecuperaLicencia();
                    VMTurno MVTurno = new VMTurno();
                    MVTurno.ConsultarUltimoTurnoSuministradora(MVLicencia.Licencia);

                    if (MVTurno.DtmHoraFin == DateTime.MinValue && MVTurno.DtmHoraInicio != DateTime.MinValue)
                    {
                        lblHoraInicioTurno.Content = MVTurno.DtmHoraInicio;
                        lblFolioTurno.Content = MVTurno.LngFolio;
                        lblUidusuario.Content = MVTurno.UidUsuario;
                        LblUidTurno.Content = MVTurno.UidTurno;
                        VMUsuarios MVUsuario = new VMUsuarios();
                        MVUsuario.obtenerDatosDeSupervisor(MVTurno.UidUsuario);
                        txtUsuario.Text = MVUsuario.StrNombre;
                        txtSucursal.Text = MVUsuario.Sucursal;
                        lblEmpresa.Text = MVUsuario.NombreEmpresa;
                        if (!string.IsNullOrEmpty(lblUidusuario.Content.ToString()))
                        {
                            btnordenes.IsEnabled = true;
                            btnReportes.IsEnabled = true;
                            btnConfiguracion.Visibility = Visibility.Visible;
                            btnAyuda.Visibility = Visibility.Visible;
                            btnCerrarSesion.Visibility = Visibility.Visible;
                        }
                    }
                }
            }

        }

        private void Btnordenes_Click(object sender, RoutedEventArgs e)
        {
            FrameContenido.Content = new PageOrdenes(this);
        }

        private void BtnReportes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnLicencias_Click(object sender, RoutedEventArgs e)
        {
            FrameContenido.Content = new Licencia1(this);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (AccesoInternet())
            {
                MVAcceso.BitacoraRegistroSupervisores(new Guid(lblUidusuario.Content.ToString()), new Guid("83D5135E-95A4-4FFB-8F74-B6BAC980DFA3"));
                var MVTurno = new VMTurno();
                MVTurno.TurnoSuministradora(new Guid(lblUidusuario.Content.ToString()), new Guid(LblUidTurno.Content.ToString()));
                lblUidusuario.Content = string.Empty;
                txtUsuario.Text = string.Empty;
                txtSucursal.Text = string.Empty;
                LblUidTurno.Content = string.Empty;
                lblFolioTurno.Content = string.Empty;
                lblHoraInicioTurno.Content = string.Empty;
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

        private void BtnMenuPrincipal_Click(object sender, RoutedEventArgs e)
        {
            FrameContenido.Source = null;
        }


    }
}
