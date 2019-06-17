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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VistaDelModelo;
using System.Windows.Threading;
using System.Data;
using Deliverix.Wpf.Distribuidores;

namespace DeliverixSucursales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VMOrden MVOrden = new VMOrden();
        VMLicencia MVLicencia = new VMLicencia();
        VistaDelModelo.VMLicencia HostingLicencia = new VistaDelModelo.VMLicencia();
        VMSucursales MVSucursal = new VMSucursales();
        VMEmpresas MVEmpresa = new VMEmpresas();
       

        public MainWindow()
        {
            
            InitializeComponent();
            HabilitaBotones();

            btnOrdenes.IsEnabled = false;
            btnEquipoEnvios.IsEnabled = false;
        }
        public void HabilitaBotones()
        {
            
            LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
            //Verifica la existencia de la licencia de manera local
            if (MVLicencia.VerificaExistenciaDeLicenciaLocal().Rows.Count < 1)
            {
                btnInicioDeTurno.IsEnabled = false;
                btnInicioDeSesion.IsEnabled = false;
                btnAgregaLicencia.IsEnabled = true;
                btnRevocaLicencia.IsEnabled = false;
            }
            else
            {
                MVLicencia.RecuperaLicencia();
                //Valida si no existe en la nube
                if (!HostingLicencia.ValidaExistenciaDeLicencia(MVLicencia.Licencia))
                {
                    btnInicioDeTurno.IsEnabled = false;
                    btnInicioDeSesion.IsEnabled = false;
                    btnAgregaLicencia.IsEnabled = true;
                    btnRevocaLicencia.IsEnabled = true;
                }
                else
                {
                    //Verifica si el estatus esta activo
                    if (!HostingLicencia.VerificaEstatusDeLicenciaSucursal(MVLicencia.Licencia))
                    {
                        btnInicioDeTurno.IsEnabled = false;
                        btnInicioDeSesion.IsEnabled = false;
                        btnAgregaLicencia.IsEnabled = true;
                        btnRevocaLicencia.IsEnabled = true;

                        
                        Label lblMensaje = VentanaMensaje.FindName("lblMensaje") as Label;
                        lblMensaje.Content = "Licencia inactiva!";
                        VentanaMensaje.ShowDialog();
                    }
                    else
                    {
                        string sucursal = MVSucursal.ObtenSucursalDeLicencia(MVLicencia.Licencia);
                        if (!MVSucursal.VerificaEstatusSucursal(sucursal))
                        {
                            btnInicioDeTurno.IsEnabled = false;
                            btnInicioDeSesion.IsEnabled = false;
                            btnAgregaLicencia.IsEnabled = true;
                            btnRevocaLicencia.IsEnabled = true;
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
                                btnInicioDeTurno.IsEnabled = true;
                                btnInicioDeSesion.IsEnabled = true;
                                btnAgregaLicencia.IsEnabled = true;
                                btnRevocaLicencia.IsEnabled = true;

                            }
                        }
                    }
                }
            }
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
                        //Muestra ventnaa de licencia si no esta disponible o activa
                        
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
        private void btnAgregaLicencia_Click(object sender, RoutedEventArgs e)
        {
            Window1 ventana = new Window1();
            ventana.ShowDialog();
            HabilitaBotones();
        }

        private void btnInicioDeSesion_Click(object sender, RoutedEventArgs e)
        {
            //if (VerificaEstatusDeLasucursal())
            //{
            //    Login ventana = new Login(this);
            //    ventana.ShowDialog();
            //    HabilitaBotones();
            //}
            //else
            //{
            //    this.Show();
            //}
        }

        private void btnRevocaLicencia_Click(object sender, RoutedEventArgs e)
        {
            RevocarLicencia ventana = new RevocarLicencia();
            ventana.ShowDialog();
            HabilitaBotones();
           
        }

        private void btnInicioDeTurno_Click(object sender, RoutedEventArgs e)
        {
            if (VerificaEstatusDeLasucursal())
            {
                //Login ventana = new Login(this);
                //ventana.ShowDialog();
                //HabilitaBotones();
                //if (!string.IsNullOrEmpty(lblUidusuario.Content.ToString()))
                //{
                //    btnOrdenes.IsEnabled = true;
                //    btnEquipoEnvios.IsEnabled = true;
                //}
            }
            else
            {
                this.Show();
            }
        }
        
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            lblSucursal.Content = string.Empty;
            lblUidusuario.Content = string.Empty;
            lblUsuario.Content = string.Empty;
            
        }


        private void btnEquipoEnvios_Click(object sender, RoutedEventArgs e)
        {
            AsignarRepartidores ventana = new AsignarRepartidores();
            ventana.ShowDialog();
        }


        private void btnOrdenes_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
