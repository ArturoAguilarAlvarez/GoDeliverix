using DeliverixSucursales;
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
using System.Windows.Threading;
using VistaDelModelo;
using LibPrintTicket;
using System.Data;

namespace Deliverix.Wpf.Distribuidores
{
    /// <summary>
    /// Lógica de interacción para Liquidacion.xaml
    /// </summary>
    public partial class Liquidacion : Page
    {
        VMUsuarios MVUsuario = new VMUsuarios();
        VMTurno MVTurno = new VMTurno();
        public bool ConfirmacionRepartidor;
        Main PaginaPrincipal;
        DispatcherTimer Timer = new DispatcherTimer();
        DeliverixSucursales.VMLicencia MVLicencia = new DeliverixSucursales.VMLicencia();

        public Liquidacion(Main pagina)
        {
            InitializeComponent();
            MVLicencia.RecuperaLicencia();
            PaginaPrincipal = pagina;
            string licencia = MVLicencia.Licencia;

            lblNombreRepartidor.Content = string.Empty;
            lblUidTurnoRepartidor.Content = string.Empty;
            //Obtiene el uid del turno y consulta sus liquidaciones

            Timer.Tick += new EventHandler(ObtenerHistorico);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
            RefrescaBitacora();
        }
        /// <summary>
        /// Obtiene el historico de las ordenes
        /// </summary>
        private void ObtenerHistorico(object sender, EventArgs e)
        {
            RefrescarDatosLiquidadores();
        }

        private void RefrescarDatosLiquidadores()
        {
            MVLicencia.RecuperaLicencia();
            string licencia = MVLicencia.Licencia;
            MVTurno.ObtenerRepartidoresALiquidar(licencia);
            DataGridRepartidores.ItemsSource = MVTurno.ListaDeRepartidores;
        }

        protected void RefrescaBitacora()
        {
            try
            {
                Label TurnoDistribuidora = PaginaPrincipal.FindName("LblUidTurno") as Label;
                dgLiquidaciones.ItemsSource = MVTurno.ObtenerLiquidacionesDeTurnoDistribuidora(TurnoDistribuidora.Content.ToString()).DefaultView;
                decimal total = 0.0m;
                int intOrdenes = 0;
                foreach (DataRowView item in dgLiquidaciones.Items)
                {
                    intOrdenes = intOrdenes + int.Parse(item[5].ToString());
                    total = total + decimal.Parse(item[6].ToString());
                }
                lblCantidadOrdenes.Content = intOrdenes;
                lblMonto.Content = "$" + total.ToString("N2");
            }
            catch (Exception)
            {
                MessageBox.Show("Aviso del sistema", "Se debe abrir un turno para consultar este modulo");
            }

        }
        private void DataGridRepartidores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRepartidores.SelectedItem != null)
            {
                VMTurno Objeto = new VMTurno();
                Objeto = (VMTurno)DataGridRepartidores.SelectedItem;
                lblNombreRepartidor.Content = Objeto.StrNombre;
                lblUidTurnoRepartidor.Content = Objeto.UidTurno;
                lblMontoALiquidar.Content = "$" + Objeto.DTotalEnvio;
            }
        }
        private void BtnLiquidar_Click(object sender, RoutedEventArgs e)
        {
            LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
            TextBlock lblMensaje = VentanaMensaje.FindName("lblMensaje") as TextBlock;

            if (!string.IsNullOrEmpty(lblUidTurnoRepartidor.Content.ToString()))
            {
                //ConfirmacionRepartidor = false;
                //Acceso Login = new Acceso("Repartidor", VentanaLiquidacion: this);
                //Login.ShowDialog();

                //if (ConfirmacionRepartidor)
                //{
                //Contenido de la ventana despues de que se loguea correctamente
                MVTurno = new VMTurno();
                Label TurnoDistribuidora = PaginaPrincipal.FindName("LblUidTurno") as Label;
                MVTurno.LiquidarARepartidor(lblUidTurnoRepartidor.Content.ToString(), TurnoDistribuidora.Content.ToString(), lblMontoALiquidar.Content.ToString().Substring(1));
                MVTurno.AgregaEstatusTurnoRepartidor(lblUidTurnoRepartidor.Content.ToString(), "38FA16DF-4727-41FD-A03E-E2E43FA78F3F");

                lblNombreRepartidor.Content = string.Empty;
                lblMontoALiquidar.Content = string.Empty;
                lblUidTurnoRepartidor.Content = string.Empty;
                MVTurno = new VMTurno();
                lblMensaje.Text = "La liquidacion ha sido confirmada";
                VentanaMensaje.ShowDialog();
                RefrescarDatosLiquidadores();
                RefrescaBitacora();
                //}
                //else
                //{
                //    lblMensaje.Text = "No ingresaste";
                //    VentanaMensaje.ShowDialog();
                //}
            }
            else
            {
                lblMensaje.Text = "Para liquidar debes seleccionar un repartidor";
                VentanaMensaje.ShowDialog();
            }
        }
    }
}
