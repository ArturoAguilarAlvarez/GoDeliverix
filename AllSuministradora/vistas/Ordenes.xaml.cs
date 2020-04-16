using AllSuministradora.model;
using AllSuministradora.Recursos;
using AllSuministradora.VistasDelModelo;
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

namespace AllSuministradora.vistas
{
    /// <summary>
    /// Interaction logic for Ordenes.xaml
    /// </summary>
    public partial class Ordenes : Page
    {
        public Ordenes()
        {
            InitializeComponent();
            var instance = ControlGeneral.GetInstance();
            if (instance.VMSucursalesLocal == null)
            {
                instance.VMSucursalesLocal = new VMSucursalesLocal();
            }
            if (instance.MVOrdenes == null)
            {
                instance.MVOrdenes = new VMOrdenes();
                instance.MVOrdenes.Timer.Start();
            }
            else
            {
                instance.MVOrdenes.StrBusquedaDeOrdenes = "Confirmar";
                instance.MVOrdenes.CargaOrdenes();
            }
            instance.MVOrdenes.oSeleccionado = new Orden();
            instance.MVOrdenes.oOrdenParaEnvio = new Orden();
            instance.MVOrdenes.oSeleccionElaboracion = new Orden();
            DataContext = instance.MVOrdenes;

        }

        private void TurnoSucursal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Orden fila = (Orden)TurnoSucursal.SelectedItem;
            if (fila != null)
            {
                var instance = ControlGeneral.GetInstance();
                VMOrden MVOrden = new VMOrden();
                MVOrden.ObtenerProductosDeOrden(fila.UidOrden.ToString());

                fila.ListaDeProductos = new List<Producto>();
                fila.VControlConfirmar = Visibility.Visible;
                fila.VCancelarConfirmar = Visibility.Visible;
                foreach (var item in MVOrden.ListaDeProductos)
                {
                    fila.ListaDeProductos.Add(
                        new Producto()
                        {
                            StrNombre = item.StrNombreProducto,
                            IntCantidad = item.intCantidad,
                        });
                }
                instance.MVOrdenes.oSeleccionado = fila;
                instance.MVOrdenes.oSeleccionElaboracion = null;
            }
            else
            {
                TurnoSucursal.SelectedItem = null;
            }
        }

        private void tabPaginas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedTab = (sender as TabControl).SelectedItem as TabItem;
            var instance = ControlGeneral.GetInstance();

            switch (selectedTab.Name)
            {
                case "tabConfirmacion":
                    instance.MVOrdenes.StrBusquedaDeOrdenes = "Confirmar";
                    instance.MVOrdenes.CargaOrdenes();
                    break;
                case "TIRecibidas":
                    instance.MVOrdenes.StrBusquedaDeOrdenes = "Elaborar";
                    instance.MVOrdenes.CargaOrdenes();
                    break;
                case "TIAsignadas":
                    instance.MVOrdenes.UidCodigoEntrega = string.Empty;
                    instance.MVOrdenes.StrBusquedaDeOrdenes = "Recolectar";
                    instance.MVOrdenes.CargaOrdenes();
                    break;
                case "TICanceladas":
                    instance.MVOrdenes.StrBusquedaDeOrdenes = "Canceladas";
                    instance.MVOrdenes.CargaOrdenes();
                    break;
                default:
                    break;
            }
        }

        private void dtOrdenesEnElaboracion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Orden fila = (Orden)dtOrdenesEnElaboracion.SelectedItem;
            if (fila != null)
            {
                var instance = ControlGeneral.GetInstance();
                VMOrden MVOrden = new VMOrden();
                MVOrden.ObtenerProductosDeOrden(fila.UidOrden.ToString());

                fila.ListaDeProductos = new List<Producto>();
                fila.VControlConfirmar = Visibility.Visible;
                fila.VCancelarConfirmar = Visibility.Visible;
                foreach (var item in MVOrden.ListaDeProductos)
                {
                    fila.ListaDeProductos.Add(
                        new Producto()
                        {
                            StrNombre = item.StrNombreProducto,
                            IntCantidad = item.intCantidad,
                        });
                }
                instance.MVOrdenes.oSeleccionElaboracion = fila;
                instance.MVOrdenes.oSeleccionado = new Orden();
            }
            else
            {
                dtOrdenesEnElaboracion.SelectedItem = null;
            }
        }
    }
}
