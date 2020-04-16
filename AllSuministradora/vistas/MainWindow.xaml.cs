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
using AllSuministradora.Recursos;
using AllSuministradora.vistas;
using AllSuministradora.VistasDelModelo;
using Microsoft.Win32;

namespace AllSuministradora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RedireccionarBasico();
            var instancia = ControlGeneral.GetInstance();
            if (instancia.Principal == null)
            {
                instancia.Principal = new VMPantallaPrincipal();
            }
            if (instancia.Usuario == null)
            {
                instancia.Usuario = new VMUsuario();
            }
            if (instancia.VMLicencia == null)
            {
                instancia.VMLicencia = new VMLicencias();
            }
            if (instancia.VMSucursalesLocal == null && instancia.Principal.UidUsuario != null)
            {
                instancia.VMSucursalesLocal = new VMSucursalesLocal();
            }
            DataContext = instancia.Principal;
            ContenedorDatosLogin.DataContext = instancia.Usuario;
            ContenedorDatosLicencia.DataContext = instancia.VMLicencia;
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dhTransferirTurno.IsOpen = true;
        }

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

        private void txtContrasena_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var instance = ControlGeneral.GetInstance();
            instance.Usuario.StrContrasena = txtContrasena.Password;

        }

        private void btnOrdenes_Click(object sender, RoutedEventArgs e)
        {
            var instance = ControlGeneral.GetInstance();
            if (!string.IsNullOrEmpty(instance.Principal.UidUsuario))
            {
                FrameContenido.Content = new Ordenes();
            }
            else
            {
                MessageBox.Show("Se necesita ingresal al sistema para ver este modulo");
            }
        }
        private void btnTurnos_Click(object sender, RoutedEventArgs e)
        {
            var instance = ControlGeneral.GetInstance();
            if (!string.IsNullOrEmpty(instance.Principal.UidUsuario))
            {
                FrameContenido.Content = new ControlTurnos();
            }
            else
            {
                MessageBox.Show("Se necesita ingresal al sistema para ver este modulo");
            }
        }

        private void btnConfiguracionSucursales_Click(object sender, RoutedEventArgs e)
        {
            var instance = ControlGeneral.GetInstance();
            if (!string.IsNullOrEmpty(instance.Principal.UidUsuario))
            {
                FrameContenido.Content = new Licencias();
            }
            else
            {
                MessageBox.Show("Se necesita ingresal al sistema para ver este modulo");
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
                    AllSuministradora.Properties.Settings.Default["Source"] = sourceRegistro;
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

        private void btnCerrarTurno_Click(object sender, RoutedEventArgs e)
        {
            var instance = ControlGeneral.GetInstance();
            bool respuesta = false;
            model.Turno turno = new model.Turno();
            foreach (var item in instance.VMSucursalesLocal.ListaDeSucursales)
            {
                if (turno.EstatusTurno(item.Licencia, UidSucursal: item.UidSucursal))
                {
                    respuesta = true;
                }
            }
            if (respuesta)
            {
                MessageBox.Show("No puede cerrar turno con un turno abierto de una sucursal, verifique sus turnos y vuelva a intentar");
                FrameContenido.Content = new ControlTurnos();
            }
            else
            {
                FrameContenido.Content = null;
            }
        }

        private void btnCancelarLogin_Click(object sender, RoutedEventArgs e)
        {
            txtContrasena.Password = string.Empty;
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            var instance = ControlGeneral.GetInstance();
            instance.Usuario.Ingresa();
            txtContrasena.Password = string.Empty;
            FrameContenido.Content = new Ordenes();
        }
    }
}
