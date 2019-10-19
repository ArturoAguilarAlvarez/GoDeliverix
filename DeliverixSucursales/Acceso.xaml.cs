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
    /// Lógica de interacción para Acceso.xaml
    /// </summary>
    public partial class Acceso : Window
    {
        VMOrden MVOrden = new VMOrden();
        VMLicencia MVLicencia = new VMLicencia();
        VMAcceso MVAcceso = new VMAcceso();
        VistaDelModelo.VMLicencia HostingLicencia = new VistaDelModelo.VMLicencia();
        VMSucursales MVSucursal = new VMSucursales();
        VMEmpresas MVEmpresa = new VMEmpresas();
        VMUsuarios MVUsuario = new VMUsuarios();
        VMTurno MVTurno = new VMTurno();
        Main ventanaPrincial;
        Licencia1 MenuLicencia;
        string ModuloAIngresar;
        public Acceso(string modulo, Licencia1 ventanaLicencia = null, Main VentanaPrincipal = null)
        {
            InitializeComponent();
            ModuloAIngresar = modulo;
            if (ventanaLicencia != null)
            {
                MenuLicencia = ventanaLicencia;
            }
            if (VentanaPrincipal != null)
            {
                ventanaPrincial = VentanaPrincipal;

            }
            txtUsuario.Focus();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public bool VerificaEstatusDeLasucursal()
        {
            Window1 ventanaLicencia = new Window1();
            LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
            bool estatus = false;
            if (MVLicencia.VerificaExistenciaDeLicenciaLocal().Rows.Count < 1)
            {

                if (ModuloAIngresar == "Administrador")
                {
                    estatus = true;
                }
                else
                {
                    //Muestra ventnaa de licencia si no existe en la base de datos local
                    ventanaLicencia.ShowDialog();
                    estatus = false;
                }

            }
            else
            {
                estatus = true;
                MVLicencia.RecuperaLicencia();
                if (!HostingLicencia.ValidaExistenciaDeLicencia(MVLicencia.Licencia))
                {
                    estatus = false;
                    //Muestra ventnaa de licencia si no existe en la nube
                    ventanaLicencia.ShowDialog();
                }
                else
                {
                    estatus = true;
                    if (!HostingLicencia.VerificaEstatusDeLicenciaSucursal(MVLicencia.Licencia))
                    {
                        estatus = false;
                        //Muestra ventnaa de licencia si no esta  activa
                        Label lblMensaje = VentanaMensaje.FindName("lblMensaje") as Label;
                        lblMensaje.Content = "Licencia inactiva ";
                        VentanaMensaje.ShowDialog();
                    }
                    else
                    {
                        estatus = true;
                        //Obtiene la sucursal a partir de la licencia
                        string sucursal = MVSucursal.ObtenSucursalDeLicencia(MVLicencia.Licencia);
                        if (!MVSucursal.VerificaEstatusSucursal(sucursal))
                        {
                            estatus = false;
                            //Muestra ventnaa de licencia si no esta activa la sucursal
                            Label lblMensaje = VentanaMensaje.FindName("lblMensaje") as Label;
                            lblMensaje.Content = "Sucursal inactiva!";
                            VentanaMensaje.ShowDialog();
                        }
                        else
                        {
                            estatus = true;

                            String empresa = MVSucursal.ObtenerUidEmpresa(sucursal).ToString();
                            if (!MVEmpresa.VerificaEstatusEmpresa(empresa))
                            {
                                estatus = false;
                                //Muestra ventnaa de licencia si no esta activa la empresa
                                Label lblMensaje = VentanaMensaje.FindName("lblMensaje") as Label;
                                lblMensaje.Content = "Empresa inactiva!";
                                VentanaMensaje.ShowDialog();
                            }
                            else
                            {
                                estatus = true;
                            }
                        }
                    }
                }
            }
            return estatus;

        }
        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (VerificaEstatusDeLasucursal())
                {
                    string usuario = txtUsuario.Text;
                    string password = txtPassword.Password;
                    if (!string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(password))
                    {
                        Guid Uidusuario = MVAcceso.Ingresar(usuario, password);
                        if (Uidusuario != Guid.Empty)
                        {
                            string perfil = MVAcceso.PerfilDeUsuario(Uidusuario.ToString());
                            if (ModuloAIngresar == "Supervisor")
                            {
                                //Supervisor
                                if (perfil.ToUpper() == "81232596-4C6B-4568-9005-8D4A0A382FDA")
                                {
                                    MVLicencia = new VMLicencia();
                                    MVLicencia.RecuperaLicencia();
                                    string sucursal = MVSucursal.ObtenSucursalDeLicencia(MVLicencia.Licencia);
                                    if (MVSucursal.VerificaExistenciaDeSupervisor(Uidusuario.ToString(), sucursal))
                                    {

                                        Label uidUsuario = ventanaPrincial.FindName("lblUidusuario") as Label;
                                        TextBlock Usuario = ventanaPrincial.FindName("txtUsuario") as TextBlock;
                                        TextBlock Sucursal = ventanaPrincial.FindName("txtSucursal") as TextBlock;
                                        TextBlock lblEmpresa = ventanaPrincial.FindName("lblEmpresa") as TextBlock;
                                        Label LblUidTurno = ventanaPrincial.FindName("LblUidTurno") as Label;
                                        Label lblHoraInicioTurno = ventanaPrincial.FindName("lblHoraInicioTurno") as Label;
                                        Label lblFolioTurno = ventanaPrincial.FindName("lblFolioTurno") as Label;

                                        MVUsuario.obtenerDatosDeSupervisor(Uidusuario);
                                        uidUsuario.Content = MVUsuario.Uid;

                                        Usuario.Text = MVUsuario.StrNombre;
                                        Sucursal.Text = MVUsuario.Sucursal;

                                        //Bitacora de supervisor

                                        lblEmpresa.Text = MVUsuario.NombreEmpresa;

                                        VMTurno MVTurno = new VMTurno();
                                        MVTurno.ConsultarUltimoTurnoSuministradora(MVLicencia.Licencia);

                   if (MVTurno.DtmHoraFin == DateTime.MinValue && MVTurno.DtmHoraInicio != DateTime.MinValue)
                                        {
                                            LblUidTurno.Content = MVTurno.UidTurno;
                                            lblHoraInicioTurno.Content = MVTurno.DtmHoraInicio;
                                            lblFolioTurno.Content = MVTurno.LngFolio;
                                        }
                                        else
                                        {
                                            MVTurno = new VMTurno();
                                            Guid UidTurnoDistribuidor = Guid.NewGuid();
                                            MVTurno.TurnoSuministradora(MVUsuario.Uid, UidTurnoDistribuidor);

                                            LblUidTurno.Content = UidTurnoDistribuidor.ToString();
                                        }

                                        Close();
                                    }

                                    else
                                    {
                                        LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
                                        TextBlock lblMensaje = VentanaMensaje.FindName("lblMensaje") as TextBlock;
                                        lblMensaje.Text = "El usuario no corresponde a la sucursal";
                                        VentanaMensaje.ShowDialog();
                                    }
                                }
                            }
                            else
                            if (ModuloAIngresar == "Administrador")
                            {
                                LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
                                //Administrador
                                if (perfil.ToUpper() == "76A96FF6-E720-4092-A217-A77A58A9BF0D")
                                {
                                    if (!string.IsNullOrEmpty(MVLicencia.Licencia))
                                    {
                                        //Validacion para determinar si el administrador que ingresa pertenece a la empresa la cual se vincula con la licencia
                                        if (MVUsuario.ValidaExistenciaDeAdministracidor(MVLicencia.Licencia, Uidusuario))
                                        {
                                            MenuLicencia.ConfirmacionSupervisor = true;
                                            Close();
                                        }
                                        else
                                        {
                                            TextBlock lblMensaje = VentanaMensaje.FindName("lblMensaje") as TextBlock;
                                            lblMensaje.Text = "Solo los administradores asociados a la sucursal\n pueden quitar la licencia";
                                            VentanaMensaje.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        MenuLicencia.ConfirmacionSupervisor = true;
                                        Close();
                                    }
                                }
                                else
                                {
                                    MVLicencia.RecuperaLicencia();

                                    TextBlock lblMensaje = VentanaMensaje.FindName("lblMensaje") as TextBlock;
                                    if (!string.IsNullOrEmpty(MVLicencia.Licencia))
                                    {
                                        lblMensaje.Text = "Solo los administradores pueden quitar la licencia";
                                    }
                                    else
                                    {
                                        lblMensaje.Text = "Solo los administradores pueden ingresar licencias";
                                    }
                                    VentanaMensaje.ShowDialog();
                                }
                            }
                            else
                            {
                                LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
                                TextBlock lblMensaje = VentanaMensaje.FindName("lblMensaje") as TextBlock;
                                lblMensaje.Text = "Acceso denegado";
                                VentanaMensaje.ShowDialog();
                            }
                        }
                        else
                        {
                            LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
                            TextBlock lblMensaje = VentanaMensaje.FindName("lblMensaje") as TextBlock;
                            lblMensaje.Text = "Acceso incorrecto";
                            VentanaMensaje.ShowDialog();
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
                            txtPassword.BorderBrush = Brushes.Red;
                        }
                    }
                }
            }
            catch (Exception es)
            {

                MessageBox.Show("Aviso del sistema", "Sin internet " + es.Message);
            }
        }
    }
}
