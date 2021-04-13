using DeliverixSucursales;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VistaDelModelo;
using LibPrintTicket;
using Deliverix.Wpf.Distribuidores.Reportes;

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
        VMTurno MVTurno = new VMTurno();

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

                        CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
                        Thread.CurrentThread.CurrentCulture = culture;
                        Thread.CurrentThread.CurrentUICulture = culture;
                        if (Application.Current.Windows.OfType<Main>().Any())
                        {
                            Application.Current.Windows.OfType<Main>().First().Activate();
                            InitializeComponent();
                            MVLicencia.RecuperaLicencia();
                            if (!string.IsNullOrEmpty(MVLicencia.Licencia))
                            {
                                VMTurno MVTurno = new VMTurno();
                                MVTurno.ConsultarUltimoTurnoDistribuidora(MVLicencia.Licencia);

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

                                    HabilitaBotones();
                                    if (!string.IsNullOrEmpty(lblUidusuario.Content.ToString()))
                                    {
                                        btnordenes.IsEnabled = true;
                                        btnMenuPrincipal.IsEnabled = true;
                                        btnReportes.IsEnabled = true;
                                        btnRepartidores.IsEnabled = true;
                                        btnInciarSesion.Visibility = Visibility.Hidden;
                                        btnConfiguracion.Visibility = Visibility.Visible;
                                        btnAyuda.Visibility = Visibility.Visible;
                                        btnCerrarSesion.Visibility = Visibility.Visible;
                                    }
                                }
                            }

                        }
                        else
                        {
                            InitializeComponent();
                            HabilitaBotones();
                            MVLicencia.RecuperaLicencia();
                            VMTurno MVTurno = new VMTurno();
                            MVTurno.ConsultarUltimoTurnoDistribuidora(MVLicencia.Licencia);

                            if (MVTurno.DtmHoraFin == DateTime.Parse("01/01/0001 12:00:00 a. m.") && MVTurno.DtmHoraInicio != DateTime.Parse("01/01/0001 12:00:00 a. m."))
                            {

                                lblHoraInicioTurno.Content = MVTurno.DtmHoraInicio;
                                lblFolioTurno.Content = MVTurno.LngFolio;
                                lblUidusuario.Content = MVTurno.UidUsuario;
                                LblUidTurno.Content = MVTurno.UidTurno;
                                VMUsuarios MVUsuario = new VMUsuarios();
                                MVUsuario.obtenerDatosDeSupervisor(MVTurno.UidUsuario);
                                txtUsuario.Text = MVUsuario.StrNombre;
                                txtSucursal.Text = MVUsuario.Sucursal;

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
                    else
                    {
                        FrameContenido.Content = new PageLicencia(this);
                    }
                }
                else
                {
                    //FrameContenido.Content = new PageLicencia(this);
                }
            }
        }

        private bool PruebaConexionRegistro(string sourceRegistro)
        {
            //int intentos = 3;
            bool aux = false;
            SqlConnection _Conexion;
            string CadenaConexion = string.Empty;
            CadenaConexion = @"Data Source =" + sourceRegistro + ";Initial Catalog =DeliverixDistribuidores;Integrated Security=True;";

            //for (int i = 0; i < intentos; i++)
            //{
            try
            {
                _Conexion = new SqlConnection(CadenaConexion);
                _Conexion.Open();
                aux = true;
                _Conexion.Close();

            }
            catch (Exception)
            {

                throw;
            }
            //}
            return aux;
        }

        private bool AccesoInternet()
        {
            try
            {
                //System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.godeliverix.net");
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
                if (!MVLicencia.VerificaExistenciaDeLicenciaLocal())
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
                    VMTurno MVTurno = new VMTurno();
                    MVTurno.ConsultarUltimoTurnoDistribuidora(MVLicencia.Licencia);

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
        }

        private void BtnLiquidar_Click(object sender, RoutedEventArgs e)
        {
            LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
            TextBlock lblMensaje = VentanaMensaje.FindName("lblMensaje") as TextBlock;

            if (!string.IsNullOrEmpty(lblUidTurnoRepartidor.Content.ToString()))
            {
                MVTurno = new VMTurno();

                MVTurno.LiquidarARepartidor(lblUidTurnoRepartidor.Content.ToString(), LblUidTurno.Content.ToString(), lblMontoALiquidar.Content.ToString().Substring(1), "Liquidacion");
                MVTurno.AgregaEstatusTurnoRepartidor(lblUidTurnoRepartidor.Content.ToString(), "38FA16DF-4727-41FD-A03E-E2E43FA78F3F");

                Ticket t = new Ticket();
                VMUsuarios MVusuario = new VMUsuarios();
                MVusuario.obtenerDatosDeSupervisor(new Guid(lblUidusuario.Content.ToString()));

                //Informacion de la empresa
                t.AddHeaderLine("" + MVusuario.NombreEmpresa + "");
                t.AddHeaderLine("Sucursal: " + MVusuario.Sucursal + "");

                t.AddHeaderLine("Usuario: " + MVusuario.StrNombre + "");
                //Obtene informacion del turno
                MVTurno = new VMTurno();
                MVLicencia = new DeliverixSucursales.VMLicencia();
                MVLicencia.RecuperaLicencia();
                MVTurno.ConsultarUltimoTurnoDistribuidora(MVLicencia.Licencia);
                t.AddSubHeaderLine("");
                t.AddHeaderLine("Informacion del liquidacion");
                //Informacion del turno
                t.AddHeaderLine("Repartidor: " + lblNombreRepartidor.Content + "");
                t.AddTotal("Total liquidado ", lblMontoALiquidar.Content.ToString());
                t.AddSubHeaderLine("");
                t.AddTotal("Firma de Supervisor ", "__________");
                t.AddTotal("Firma de Repartidor ", "__________");
                t.FontSize = 6;
                t.AddFooterLine("www.godeliverix.com.mx");
                t.PrintTicket("PDFCreator");


                lblNombreRepartidor.Content = string.Empty;
                lblMontoALiquidar.Content = string.Empty;
                lblUidTurnoRepartidor.Content = string.Empty;
                MVTurno = new VMTurno();
                lblMensaje.Text = "La liquidacion ha sido confirmada";
                VentanaMensaje.ShowDialog();
                DHAccionRepartidor.IsOpen = false;
                FrameContenido.Content = new Liquidacion(this);
            }
            else
            {
                lblMensaje.Text = "Para liquidar debes seleccionar un repartidor";
                VentanaMensaje.ShowDialog();
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
            FrameContenido.Content = new Liquidacion(this);
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
                MVLicencia = new DeliverixSucursales.VMLicencia();
                MVLicencia.RecuperaLicencia();
                MVTurno = new VMTurno();
                MVTurno.TurnoDistribuidora(new Guid(lblUidusuario.Content.ToString()), new Guid(LblUidTurno.Content.ToString()));
                ReporteCierreTurnoSucursal obj = new ReporteCierreTurnoSucursal(MVLicencia.Licencia, LblUidTurno.Content.ToString(), lblUidusuario.Content.ToString());
                obj.Show();
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
                        btnInciarSesion.Visibility = Visibility.Visible;
                        btnConfiguracion.Visibility = Visibility.Visible;
                        btnAyuda.Visibility = Visibility.Visible;
                        btnCerrarSesion.Visibility = Visibility.Visible;
                    }
                }
                FrameContenido.Source = null;
            }
        }

        private void btnRecargar_Click(object sender, RoutedEventArgs e)
        {
            pnlInformacionTrabajoRepartidor.Visibility = Visibility.Hidden;
            pnlRecarga.Visibility = Visibility.Visible;
            DHRecargar.IsOpen = true;
            DHAccionRepartidor.IsOpen = false;
            MVTurno = new VMTurno();
            MVTurno.ConsultaUltimoTurno(new Guid(lblUidRepartidor.Content.ToString()));
            decimal recargaRecomendada = 0.0m;

            if (decimal.Parse(lblMontoALiquidar.Content.ToString().Replace("$", "")) > MVTurno.DFondoRepartidor)
            {
                recargaRecomendada = 0.0m;
            }
            else
            {
                recargaRecomendada = MVTurno.DFondoRepartidor - (decimal.Parse(lblMontoALiquidar.Content.ToString().Replace("$", "")));
            }
            lblEfectivoMaximoRepartidor.Content = MVTurno.ObtenerMontoAPortar(lblUidRepartidor.Content.ToString());
            lblEfectivoActual.Content = lblMontoALiquidar.Content;
            lblMontoRecomendadoARecargar.Content = recargaRecomendada.ToString();
            lblMontoMaximoRepartidor.Content = decimal.Parse(MVTurno.ObtenerMontoAPortar(lblUidRepartidor.Content.ToString())) - decimal.Parse(lblMontoALiquidar.Content.ToString().Replace("$", ""));
            lblTituloInformacionTurnoRepartidor.Content = "Agregar recarga";
        }

        private void btnCancelarRecarga_Click(object sender, RoutedEventArgs e)
        {
            pnlInformacionTrabajoRepartidor.Visibility = Visibility.Visible;
            pnlRecarga.Visibility = Visibility.Hidden;
            lblTituloInformacionTurnoRepartidor.Content = "Informacion de turno";
            DHRecargar.IsOpen = false;
            DHAccionRepartidor.IsOpen = true;
        }

        private void btnAceptarRecarga_Click(object sender, RoutedEventArgs e)
        {
            decimal recarga = 0.0m;
            decimal MontoMaximo = 0.0m;
            if (decimal.TryParse(txtMontoRecarga.Text, out recarga))
            {
                MontoMaximo = decimal.Parse(lblMontoMaximoRepartidor.Content.ToString());
                if (MontoMaximo >= recarga)
                {
                    MVTurno = new VMTurno();
                    MVTurno.LiquidarARepartidor(lblUidTurnoRepartidor.Content.ToString(), LblUidTurno.Content.ToString(), recarga.ToString(), "Recarga");
                    MVTurno.AgregaEstatusTurnoRepartidor(lblUidTurnoRepartidor.Content.ToString(), "CCAFB7D6-A27C-4F5B-A4A6-13D35138471F");
                    MessageBox.Show("Recarga exitosa");
                    DHRecargar.IsOpen = false;
                    FrameContenido.Content = new Liquidacion(this);
                }
                else
                {
                    MessageBox.Show("No se puede agregar una cantidad mayor a la maxima a recargar");
                }
            }
            else
            {
                MessageBox.Show("Inserte solo numeros");
            }
        }
    }
}
