using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Lógica de interacción para CancelarOrden.xaml
    /// </summary>
    public partial class CancelarOrden : Window
    {
        TabControl control;
        VMOrden MVOrden;
        VMLicencia MVLicencia;
        VMMensaje MVMensaje;
        public CancelarOrden(TabControl componente)
        {
            InitializeComponent();
            control = componente;
            MVMensaje = new VMMensaje();
            MVLicencia = new VMLicencia();
            MVLicencia.RecuperaLicencia();
            MVMensaje.Buscar(strLicencia:MVLicencia.Licencia);
            cmbMensaje.ItemsSource = MVMensaje.ListaDeMensajes;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //Valida si esta seleccionado un elemento en el combobox del mensaje
            if (cmbMensaje.SelectedIndex != -1)
            {
                MVOrden = new VMOrden();
                MVLicencia = new VMLicencia();
                MVLicencia.RecuperaLicencia();
                Guid UidOrden = new Guid();
                decimal total = 0.0m;
                VMMensaje oMensaje = (VMMensaje)cmbMensaje.SelectedValue;
                //Valida si fue llamado desde la ventana de ordenes recibidas
                if (control.SelectedIndex == 0)
                {
                    TabItem elemento = (TabItem)control.SelectedItem;
                    TextBlock txtOrden = (TextBlock)elemento.FindName("txtCNumeroOrden");
                    TextBlock txtUidOrden = (TextBlock)elemento.FindName("txtConfirmarUidOrden");
                    TextBlock txtCMMonto = (TextBlock)elemento.FindName("txtCMMonto");
                    UidOrden = new Guid(txtUidOrden.Text);
                    total = decimal.Parse(txtCMMonto.Text);
                    MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EAE7A7E6-3F19-405E-87A9-3162D36CE21B"), "S", MVLicencia.Licencia, LngFolio: long.Parse(txtOrden.Text), UidMensaje: oMensaje.Uid);
                }
                //Valida si fue llamado desde la ventana de ordenes confirmadas
                if (control.SelectedIndex == 1)
                {
                    TabItem elemento = (TabItem)control.SelectedItem;
                    TextBlock txtOrden = (TextBlock)elemento.FindName("txbNumerodeOrden");
                    TextBlock txtEMmonto = (TextBlock)elemento.FindName("txtEMmonto");
                    TextBlock txtElaborarUidOrden = (TextBlock)elemento.FindName("txtElaborarUidOrden");
                    total = decimal.Parse(txtEMmonto.Text);
                    UidOrden = new Guid(txtElaborarUidOrden.Text);
                    MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EAE7A7E6-3F19-405E-87A9-3162D36CE21B"), "S", MVLicencia.Licencia, LngFolio: long.Parse(txtOrden.Text), UidMensaje: oMensaje.Uid);
                }

                VMMonedero obj = new VMMonedero();
                obj.uidOrdenSucursal = UidOrden;
                obj.UidTipoDeMovimiento = new Guid("E85F0486-1FBE-494C-86A2-BFDDC733CA5D");
                obj.UidConcepto = new Guid("2AABDF7F-EDCE-455F-B775-6283654D7DA0");
                obj.MMonto = total;
                obj.MovimientoMonedero();
                //string _Url = $"https://godeliverix.net/api/Monedero/GetMovimientosMonedero?" +
                //                $"UidOrdenSucursal={UidOrden}" + $"&TipoDeMovimiento=E85F0486-1FBE-494C-86A2-BFDDC733CA5D" +
                //                $"&Concepto=2AABDF7F-EDCE-455F-B775-6283654D7DA0" +
                //                $"&Monto=" + total.ToString("N2").Replace(",",".") + "";
                //using (HttpClient _client = new HttpClient())
                //{
                //    await _client.GetStringAsync(_Url);
                //}
                Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un mensaje para poder cancelar una orden");
            }
          
        }
    }
}
