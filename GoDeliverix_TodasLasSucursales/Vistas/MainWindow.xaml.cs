using GoDeliverix_TodasLasSucursales.VM;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
namespace GoDeliverix_TodasLasSucursales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string SourceRegistro { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            RedireccionarBasico();

            //var instance = ControlGeneral.GetInstance();
            //if (instance.MVConfiguracion == null)
            //{
            //    instance.MVConfiguracion = new VMConfiguracion();
            //}
            //lblUidUsuario.Content = "";
            //this.DataContext = instance.MVConfiguracion;
            DataContext = new VMConfiguracion();
            controlDeModulosVisibilidad(false);
        }

        #region Menu lateral
        private void btnTurnos_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lblUidUsuario.Content.ToString()))
            {
                FrameContenido.Content = new ControlTurnos();
            }
            else
            {
                MessageBox.Show("Debe ingresar al sistema para ver este modulo");
            }
        }

        private void btnConfiguracionSucursales_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lblUidUsuario.Content.ToString()))
            {
                FrameContenido.Content = new ConfiguracionSucursal(this);
            }
            else
            {
                MessageBox.Show("Debe ingresar al sistema para ver este modulo");
            }
        }

        #endregion
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            lblDeliverix.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            lblDeliverix.Visibility = Visibility.Hidden;
        }

        private void btnIniciarTurno_Click(object sender, RoutedEventArgs e)
        {
            dhTransferirTurno.IsOpen = true;
            txtUsuario.Text = string.Empty;
            txtContrasena.Password = string.Empty;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                MessageBox.Show("Usuario vacio");
            }
            else if (string.IsNullOrEmpty(txtContrasena.Password))
            {
                MessageBox.Show("Contraseña vacia");
            }
            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtContrasena.Password))
            {
                string usuario = txtUsuario.Text;
                string password = txtContrasena.Password;
                VMAcceso MVAcceso = new VMAcceso();
                if (!string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(password))
                {
                    Guid Uidusuario = MVAcceso.Ingresar(usuario, password);
                    if (Uidusuario != Guid.Empty)
                    {
                        string perfil = MVAcceso.PerfilDeUsuario(Uidusuario.ToString());

                        //Dios Maya
                        if (perfil.ToUpper() == "8D2E2925-A2A7-421F-A72B-56F2E8296D77")
                        {
                            VMUsuarios mvusuario = new VMUsuarios();
                            mvusuario.BusquedaDeUsuario(UidUsuario: Uidusuario, UIDPERFIL: new Guid(perfil));
                            lblNombreUsuario.Text = mvusuario.StrNombre;
                            dhTransferirTurno.IsOpen = false;
                            FrameContenido.Content = new ControlTurnos();
                            lblUidUsuario.Content = Uidusuario.ToString();
                            controlDeModulosVisibilidad(true);
                        }
                        else
                        {
                            MessageBox.Show("Solo el dios maya puede usar esta applicación");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Acceso incorrecto");
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(usuario))
                    {
                        txtUsuario.BorderBrush = Brushes.Red;
                    }
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        txtContrasena.BorderBrush = Brushes.Red;
                    }
                }
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            VMSucursales MVSucursal = new VMSucursales();
            VMLicencia HostingMvLicencia = new VMLicencia();
            VMLicenciaLocal oLicencia = new VMLicenciaLocal();
            string licencia = txtLicencia.Text.Trim();
            if (string.IsNullOrEmpty(licencia) || licencia.Length < 36)
            {
                MessageBox.Show("Debe ingresar una licencia valida");
            }
            else
            {
                if (MVSucursal.ObtenerElTipoDeSucursal(licencia))
                {
                    if (HostingMvLicencia.VerificaDisponibilidad(licencia))
                    {
                        int resultado = oLicencia.AgregarLicencia(licencia);
                        txtLicencia.Text = string.Empty;
                        switch (resultado)
                        {
                            case 0:
                                //HostingMvLicencia.CambiaDisponibilidadDeLicencia(licencia);

                                dhAgregarLicencia.IsOpen = false;
                                var instance = ControlGeneral.GetInstance();
                                instance.MVSucursalesLocal.ObtenSucursales();
                                MessageBox.Show("Sucursal agregada");
                                break;
                            case 1:
                                MessageBox.Show("Esta licencia ya ha sido vinculada");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Licencia no disponible!!");
                    }
                }
                else
                {
                    MessageBox.Show("La licencia no pertenece a una sucursal suministradora!!");
                }
            }
        }

        private void btnCerrarTurno_Click(object sender, RoutedEventArgs e)
        {
            lblUidUsuario.Content = string.Empty;
            controlDeModulosVisibilidad(false);
        }

        public void controlDeModulosVisibilidad(bool acceso)
        {
            if (acceso)
            {
                btnIniciarTurno.Visibility = Visibility.Hidden;
                btnConfiguracion.Visibility = Visibility.Visible;
                btnCerrarTurno.Visibility = Visibility.Visible;
            }
            else
            {
                btnIniciarTurno.Visibility = Visibility.Visible;
                btnConfiguracion.Visibility = Visibility.Hidden;
                btnCerrarTurno.Visibility = Visibility.Hidden;
            }
        }

        public void RedireccionarBasico()
        {
            string sourceRegistro = string.Empty;
            try
            {
                sourceRegistro = Registry.GetValue(@"HKEY_CURRENT_USER\MasterDeliverix", "Source", "NULL").ToString();
            }
            catch (Exception) { sourceRegistro = string.Empty; }

            //Validar si el registro no existe o tiene un valor nulo 
            if (!string.IsNullOrEmpty(sourceRegistro))
            {
                //Prueba la conexión con el source guardado en el registro de windows 
                if (PruebaConexionRegistro(sourceRegistro))
                {
                    GoDeliverix_TodasLasSucursales.Properties.Settings.Default["Source"] = sourceRegistro;
                }
                else
                {
                    DataBase wBDLocal = new DataBase();
                    wBDLocal.Show();
                }
            }
            //Validar si el registro existe o tiene un valor nulo 
            else
            {
                DataBase wBDLocal = new DataBase();
                wBDLocal.Show();
                this.Close();
            }
        }

        public bool PruebaConexionRegistro(string source)
        {
            int intentos = 3;
            bool aux = false;
            SqlConnection _sqlConeccion;
            string stringConnection = string.Empty;

            stringConnection = @"Data Source=" + source + ";Initial Catalog=DeliverixMaster;Integrated Security=True;Connection Timeout=1";

            for (int i = 0; i < intentos; i++)
                try
                {
                    _sqlConeccion = new SqlConnection(stringConnection);
                    _sqlConeccion.Open();
                    aux = true;
                    _sqlConeccion.Close();
                    break;
                }
                catch (Exception) { }

            return aux;
        }
    }
}