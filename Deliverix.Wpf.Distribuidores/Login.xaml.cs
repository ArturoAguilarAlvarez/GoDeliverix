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
using System.Windows.Threading;
using System.Data;
using Deliverix.Wpf.Distribuidores;

namespace DeliverixSucursales
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        VMOrden MVOrden = new VMOrden();
        VMLicencia MVLicencia = new VMLicencia();
        VMAcceso MVAcceso = new VMAcceso();
        VistaDelModelo.VMLicencia HostingLicencia = new VistaDelModelo.VMLicencia();
        VMSucursales MVSucursal = new VMSucursales();
        VMEmpresas MVEmpresa = new VMEmpresas();
        VMUsuarios MVUsuario = new VMUsuarios();
        Main ventanaPrincial;
        public Login(Main principal)
        {
            InitializeComponent();
            ventanaPrincial = principal;
        }




        public bool VerificaEstatusDeLasucursal()
        {
            Window1 ventanaLicencia = new Window1();
            LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
            bool estatus = false;
            if (MVLicencia.VerificaExistenciaDeLicenciaLocal().Rows.Count < 1)
            {
                //Muestra ventnaa de licencia si no existe en la base de datos local
                ventanaLicencia.ShowDialog();
                estatus = false;
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
                        MessageBox.Show("Licencia inactiva");

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
                            MessageBox.Show("Sucursal inactiva!");
                        }
                        else
                        {
                            estatus = true;

                            String empresa = MVSucursal.ObtenerUidEmpresa(sucursal).ToString();
                            if (!MVEmpresa.VerificaEstatusEmpresa(empresa))
                            {
                                estatus = false;
                                //Muestra ventnaa de licencia si no esta activa la empresa
                                MessageBox.Show("Empresa inactiva!");
                                
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
                        if (perfil.ToUpper() == "81232596-4C6B-4568-9005-8D4A0A382FDA")
                        {
                            MVLicencia.RecuperaLicencia();
                            string sucursal = MVSucursal.ObtenSucursalDeLicencia(MVLicencia.Licencia);
                            if (MVSucursal.VerificaExistenciaDeSupervisor(Uidusuario.ToString(), sucursal))
                            {
                                Label uidUsuario = ventanaPrincial.FindName("lblUidusuario") as Label;
                                TextBlock Usuario = ventanaPrincial.FindName("txtUsuario") as TextBlock;
                                TextBlock Sucursal = ventanaPrincial.FindName("txtSucursal") as TextBlock;

                                MVUsuario.obtenerDatosDeSupervisor(Uidusuario);
                                uidUsuario.Content = MVUsuario.Uid;
                                Usuario.Text = MVUsuario.StrNombre;
                                Sucursal.Text = MVUsuario.Sucursal;
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("El usuario no corresponde a la sucursal");
                              
                            }
                        }
                        else
                        {
                            MessageBox.Show("Acceso denegado");
                          
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
                        txtPassword.BorderBrush = Brushes.Red;
                    }
                }

            }
        }

        private void btnCerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
