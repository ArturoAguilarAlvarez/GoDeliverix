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
//using LibPrintTicket;
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

            PaginaPrincipal.lblNombreRepartidor.Content = string.Empty;
            PaginaPrincipal.lblUidTurnoRepartidor.Content = string.Empty;
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
            MVTurno.RepartidoresConFondoAEntregar(licencia);
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
                    intOrdenes = intOrdenes + 1;
                    if (item[4].ToString() == "Recarga")
                    {
                        total = total - decimal.Parse(item[3].ToString());
                    }
                    else if (item[4].ToString() == "Liquidacion")
                    {
                        total = total + decimal.Parse(item[3].ToString());
                    }
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
                PaginaPrincipal.lblNombreRepartidor.Content = Objeto.StrNombre;
                PaginaPrincipal.lblUidTurnoRepartidor.Content = Objeto.UidTurno;
                PaginaPrincipal.lblUidRepartidor.Content = Objeto.UidUsuario;
                PaginaPrincipal.lblMontoALiquidar.Content = "$" + Objeto.DTotalEnvio;
                PaginaPrincipal.DHAccionRepartidor.IsOpen = true;
                PaginaPrincipal.pnlInformacionTrabajoRepartidor.Visibility = Visibility.Visible;
                PaginaPrincipal.pnlRecarga.Visibility = Visibility.Hidden;

                VMTurno mvturno = new VMTurno();
                mvturno.ConsultaUltimoTurno(Objeto.UidUsuario);
                //Verifica que el turno haya sido cerrado
                if (mvturno.DtmHoraFin != DateTime.MinValue)
                {
                    PaginaPrincipal.btnLiquidarRepartidor.Visibility = Visibility.Visible;
                    PaginaPrincipal.btnRecargar.Visibility = Visibility.Hidden;
                    PaginaPrincipal.lblTituloInformacionTurnoRepartidor.Content = "Informacion de cierre de turno";
                }
                else
                {
                    if (Objeto.StrAccionTurnoRepartidor == "Recargando")
                    {
                        PaginaPrincipal.btnLiquidarRepartidor.Visibility = Visibility.Hidden;
                        PaginaPrincipal.btnRecargar.Visibility = Visibility.Visible;
                    }

                    if (Objeto.StrAccionTurnoRepartidor == "Liquidando")
                    {
                        PaginaPrincipal.btnLiquidarRepartidor.Visibility = Visibility.Visible;
                        PaginaPrincipal.btnRecargar.Visibility = Visibility.Hidden;
                    }


                    PaginaPrincipal.lblTituloInformacionTurnoRepartidor.Content = "Informacion de turno";
                }

            }
        }
    }
}
