using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using VistaDelModelo;

namespace DeliverixSucursales
{
    /// <summary>
    /// Lógica de interacción para OrdenesAEnviar.xaml
    /// </summary>
    public partial class OrdenesAEnviar : Window
    {
        VMOrden MVOrden = new VMOrden();
        VMLicencia MVLicencia = new VMLicencia();
        VMSucursales MVSucursales = new VMSucursales();
        DispatcherTimer Timer = new DispatcherTimer();
        public OrdenesAEnviar()
        {
            InitializeComponent();
            MVLicencia.RecuperaLicencia();
            Timer.Tick += new EventHandler(Windows_Reload);
            Timer.Interval = new TimeSpan(0, 0, 10);
            Timer.Start();
            MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(MVLicencia.Licencia), EstatusSucursal: "Lista a enviar");

            MVSucursales.ObtenerSucursalesDistribuidorasContratadas(MVLicencia.Licencia);

            DGSucursalesDistribuidoras.IsEnabled = false;
            MVOrden.ObtenerOrdenSucursalDistribuidora(MVLicencia.Licencia);

            for (int i = 0; i < MVOrden.ListaDeOrdenes.Count; i++)
            {
                if (MVOrden.ListaDeBitacoraDeOrdenes.Exists(orden => orden.LNGFolio == MVOrden.ListaDeOrdenes[i].LNGFolio))
                {
                    MVOrden.ListaDeOrdenes.RemoveAt(i);
                    i = i - 1;
                }
            }
            DGSucursalesDistribuidoras.ItemsSource = MVSucursales.LISTADESUCURSALES;
            DgvBitacoraOrdenes.ItemsSource = MVOrden.ListaDeBitacoraDeOrdenes;
            dgOrdenes.ItemsSource = MVOrden.ListaDeOrdenes;
        }
        private void Windows_Reload(object sender, EventArgs e)
        {
            MVLicencia.RecuperaLicencia();
            MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(MVLicencia.Licencia), EstatusSucursal: "Lista a enviar");
            dgOrdenes.ItemsSource = MVOrden.ListaDeOrdenes;
        }
        private void dgOrdenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOrdenes.SelectedItem != null)
            {
                VMOrden filaOrden = (VMOrden)dgOrdenes.SelectedItem;
                lblOrdenAEntregar.Content = filaOrden.LNGFolio.ToString();
                MVSucursales.ObtenerSucursalesDistribuidorasContratadas(MVLicencia.Licencia);
                DGSucursalesDistribuidoras.ItemsSource = MVSucursales.LISTADESUCURSALES;
                DGSucursalesDistribuidoras.Items.Refresh();
                DGSucursalesDistribuidoras.IsEnabled = true;
            }
        }

        private void btnEnviarOrden_Click(object sender, RoutedEventArgs e)
        {
            VMOrden filaOrden = (VMOrden)dgOrdenes.SelectedItem;
            Guid Codigo = Guid.NewGuid();
            foreach (var item in MVSucursales.ListaDeSucursalesSeleccionadas)
            {
              //  MVOrden.AsociarOrdenConSucursalDistribuidora(uidorden: filaOrden.Uidorden, UidSucursal: item.ID, Codigo: Codigo,UidLicencia: new Guid(MVLicencia.Licencia));
            }
            MVLicencia.RecuperaLicencia();
            MVOrden.ObtenerOrdenSucursalDistribuidora(MVLicencia.Licencia);
            MVSucursales.ObtenerSucursalesDistribuidorasContratadas(MVLicencia.Licencia);
            for (int i = 0; i < MVOrden.ListaDeOrdenes.Count; i++)
            {
                if (MVOrden.ListaDeBitacoraDeOrdenes.Exists(orden => orden.LNGFolio == MVOrden.ListaDeOrdenes[i].LNGFolio))
                {
                    MVOrden.ListaDeOrdenes.RemoveAt(i);
                    i = i - 1;
                }
            }
            dgOrdenes.ItemsSource = MVOrden.ListaDeOrdenes;
            dgOrdenes.Items.Refresh();
            DgvBitacoraOrdenes.ItemsSource = MVOrden.ListaDeBitacoraDeOrdenes;
            DgvBitacoraOrdenes.Items.Refresh();
        }

        private void DGSucursalesDistribuidoras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGSucursalesDistribuidoras.SelectedItem != null)
            {
                VMSucursales fila = (VMSucursales)DGSucursalesDistribuidoras.SelectedItem;
                MVSucursales.EliminarDeLista(fila.ID);
                MVSucursales.AgregarALista(fila.ID);
                DGSucursalesDistribuidoras.ItemsSource = MVSucursales.LISTADESUCURSALES;
            }
        }
    }
}
