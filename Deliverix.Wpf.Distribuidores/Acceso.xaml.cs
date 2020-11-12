using DeliverixSucursales;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VistaDelModelo;

namespace Deliverix.Wpf.Distribuidores
{
    /// <summary>
    /// Lógica de interacción para Acceso.xaml
    /// </summary> 
    public partial class Acceso : Window
    {
        VMOrden MVOrden = new VMOrden();
        DeliverixSucursales.VMLicencia MVLicencia = new DeliverixSucursales.VMLicencia();
        VMAcceso MVAcceso = new VMAcceso();
        VistaDelModelo.VMLicencia HostingLicencia = new VistaDelModelo.VMLicencia();
        VMSucursales MVSucursal = new VMSucursales();
        VMEmpresas MVEmpresa = new VMEmpresas();
        VMUsuarios MVUsuario = new VMUsuarios();
        VMTurno MVTurno = new VMTurno();

        Main ventanaPrincial;
        PageLicencia MenuLicencia;
        Liquidacion ModuloLiquidacion;
        string ModuloAIngresar;
        public Acceso(string modulo, PageLicencia ventanaLicencia = null, Main VentanaPrincipal = null, Liquidacion VentanaLiquidacion = null)
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
            if (VentanaLiquidacion != null)
            {
                ModuloLiquidacion = VentanaLiquidacion;
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
            if (!MVLicencia.VerificaExistenciaDeLicenciaLocal())
            {
                if (ModuloAIngresar == "Administrador")
                {
                    estatus = true;
                }
                else
                {
                    //Muestra ventnaa de licencia si no existe en la base de datos local
                    //  ventanaLicencia.ShowDialog();
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
                    if (ModuloAIngresar != "Administrador")
                    {
                        //Muestra ventnaa de licencia si no existe en la nube
                        ventanaLicencia.ShowDialog();
                    }
                    else
                    {
                        estatus = true;
                    }
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
                                    MVLicencia.RecuperaLicencia();
                                    string sucursal = MVSucursal.ObtenSucursalDeLicencia(MVLicencia.Licencia);

                                    if (MVSucursal.VerificaExistenciaDeSupervisor(Uidusuario.ToString(), sucursal))
                                    {

                                        Label uidUsuario = ventanaPrincial.FindName("lblUidusuario") as Label;
                                        TextBlock Usuario = ventanaPrincial.FindName("txtUsuario") as TextBlock;
                                        TextBlock Sucursal = ventanaPrincial.FindName("txtSucursal") as TextBlock;
                                        Label LblUidTurno = ventanaPrincial.FindName("LblUidTurno") as Label;
                                        Label lblHoraInicioTurno = ventanaPrincial.FindName("lblHoraInicioTurno") as Label;
                                        Label lblFolioTurno = ventanaPrincial.FindName("lblFolioTurno") as Label;
                                        Button btnInciarSesion = ventanaPrincial.FindName("btnInciarSesion") as Button;

                                        MVUsuario.obtenerDatosDeSupervisor(Uidusuario);
                                        uidUsuario.Content = MVUsuario.Uid;
                                        Usuario.Text = MVUsuario.StrNombre;
                                        Sucursal.Text = MVUsuario.Sucursal;

                                        VMTurno MVTurno = new VMTurno();
                                        MVTurno.ConsultarUltimoTurnoDistribuidora(MVLicencia.Licencia);

                                        if (MVTurno.DtmHoraFin == DateTime.Parse("01/01/0001 12:00:00 a. m.") && MVTurno.DtmHoraInicio != DateTime.Parse("01/01/0001 12:00:00 a. m."))
                                        {
                                            LblUidTurno.Content = MVTurno.UidTurno;
                                            lblHoraInicioTurno.Content = MVTurno.DtmHoraInicio;
                                            lblFolioTurno.Content = MVTurno.LngFolio;
                                        }
                                        else
                                        {
                                            MVTurno = new VMTurno();
                                            Guid UidTurnoDistribuidor = Guid.NewGuid();
                                            MVTurno.TurnoDistribuidora(MVUsuario.Uid, UidTurnoDistribuidor);

                                            LblUidTurno.Content = UidTurnoDistribuidor.ToString();
                                        }
                                        btnInciarSesion.Visibility = Visibility.Hidden;
                                        //MVAcceso.BitacoraRegistroSupervisores(MVUsuario.Uid, new Guid("C82A94F6-DEB0-4FCD-BCD6-22C1B2603041"));

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
                                        //Valida que la licencia exista en la nube
                                        if (HostingLicencia.ValidaExistenciaDeLicencia(MVLicencia.Licencia))
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
                            if (ModuloAIngresar == "Repartidor")
                            {
                                LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
                                //Repartidor
                                if (perfil.ToUpper() == "DFC29662-0259-4F6F-90EA-B24E39BE4346")
                                {
                                    if (!string.IsNullOrEmpty(MVLicencia.Licencia))
                                    {
                                        //Valida que la licencia exista en la nube
                                        if (HostingLicencia.ValidaExistenciaDeLicencia(MVLicencia.Licencia))
                                        {
                                            //Validacion para determinar si el administrador que ingresa pertenece a la empresa la cual se vincula con la licencia
                                            if (MVUsuario.ValidaExistenciaDeRepartidor(MVLicencia.Licencia, Uidusuario))
                                            {
                                                ModuloLiquidacion.ConfirmacionRepartidor = true;
                                                Close();
                                            }
                                            else
                                            {
                                                TextBlock lblMensaje = VentanaMensaje.FindName("lblMensaje") as TextBlock;
                                                lblMensaje.Text = "Solo los repartidores asociados a la sucursal\n pueden liquidar";
                                                VentanaMensaje.ShowDialog();
                                            }
                                        }
                                        else
                                        {
                                            Close();
                                        }
                                    }
                                    else
                                    {
                                        Close();
                                    }
                                }
                                else
                                {
                                    MVLicencia.RecuperaLicencia();
                                    TextBlock lblMensaje = VentanaMensaje.FindName("lblMensaje") as TextBlock;

                                    if (ModuloLiquidacion != null)
                                    {
                                        lblMensaje.Text = "Solo los repartidores pueden autentificarse para liquidar";

                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(MVLicencia.Licencia))
                                        {
                                            lblMensaje.Text = "Solo los administradores pueden quitar la licencia";
                                        }
                                        else
                                        {
                                            lblMensaje.Text = "Solo los administradores pueden ingresar licencias";
                                        }
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
            catch (Exception)
            {
                MessageBox.Show("Aviso del sistema", "No hay internet");
            }

        }
    }
}
