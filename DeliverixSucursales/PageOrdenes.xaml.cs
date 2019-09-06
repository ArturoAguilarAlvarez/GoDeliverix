using Deliverix.Wpf.Distribuidores;
using LibPrintTicket;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using VistaDelModelo;

namespace DeliverixSucursales
{
    /// <summary>
    /// Lógica de interacción para PageOrdenes.xaml
    /// </summary>
    public partial class PageOrdenes : Page
    {
        VMLicencia MVLicencia = new VMLicencia();
        VMOrden MVOrden = new VMOrden();
        VMTarifario MVTarifario = new VMTarifario();
        VMSucursales MVSucursal = new VMSucursales();
        VMEstatus MVEstatus = new VMEstatus();
        VMDireccion MVDireccion = new VMDireccion();
        DispatcherTimer Timer = new DispatcherTimer();

        Main MenuPrincipal;
        public PageOrdenes(Main Pagina)
        {
            if (AccesoInternet())
            {
                InitializeComponent();
                MVLicencia.RecuperaLicencia();
                MVEstatus.ObtnenerEstatusDeOrdenEnSucursal();

                //cmbOACEstatus.ItemsSource = MVEstatus.ListaEstatus;

                Timer.Tick += new EventHandler(Windows_Reload);
                Timer.Interval = new TimeSpan(0, 0, 1);
                Timer.Start();

                MenuPrincipal = Pagina;
            }
        }

        private bool AccesoInternet()
        {
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.godeliverix.net");
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Sin conexion", "No hay conexion a internet");
                return false;
            }
        }


        private void Windows_Reload(object sender, EventArgs e)
        {
            if (AccesoInternet())
            {
                MVLicencia.RecuperaLicencia();
                TabItem selectedTab = tabPaginas.SelectedItem as TabItem;
                //indx 0
                if (selectedTab.Name == "tabConfirmacion")
                {
                    CargaContenido("Confirmacion");
                }
                //Index 1
                if (selectedTab.Name == "TIRecibidas")
                {
                    CargaContenido("Recibidas");
                }
                //Index 2
                if (selectedTab.Name == "TIAsignadas")
                {
                    CargaContenido("Asignadas");
                }
                //Index 3
                if (selectedTab.Name == "TICanceladas")
                {
                    CargaContenido("Canceladas");
                }
            }


        }

        #region Panel de ordenes a confirmar
        private void DGOrdenesAConfirmar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccesoInternet())
            {
                if (DGOrdenesAConfirmar.SelectedItem != null)
                {
                    VMOrden fila = (VMOrden)DGOrdenesAConfirmar.SelectedItem;
                    MVLicencia.RecuperaLicencia();
                    string sucursal = MVSucursal.ObtenSucursalDeLicencia(MVLicencia.Licencia);
                    txtConfirmarUidOrden.Text = fila.Uidorden.ToString();
                    txtCNumeroOrden.Text = fila.LNGFolio.ToString();
                    MVOrden.ObtenerProductosDeOrden(fila.Uidorden.ToString());
                    GridViewDetalleOrdenConfirmar.ItemsSource = MVOrden.ListaDeProductos;
                }
            }
        }

        private void btnOACBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarOrdenes("A confirmar");
        }

        private void btnConfirmarOrden_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtConfirmarUidOrden.Text))
            {
                Guid UidOrden = new Guid(txtConfirmarUidOrden.Text);
                VMOrden fila = MVOrden.ListaDeOrdenes.Find(o => o.Uidorden == UidOrden);
                MVOrden.BuscarOrdenes("Sucursal", UidOrdenSucursal: fila.Uidorden, EstatusSucursal: "Detalles de orden");
                Ticket t = new Ticket();


                //QRCodeGenerator qrGenerator = new QRCodeGenerator();
                //QRCodeData qrCodeData = qrGenerator.CreateQrCode(fila.LNGFolio.ToString(), QRCodeGenerator.ECCLevel.Q);
                //QRCode qrCode = new QRCode(qrCodeData);
                //Bitmap qrCodeImage = qrCode.GetGraphic(5);
                //t.HeaderImage = qrCodeImage;

                //Confirguracion de ticket

                //Configuracion header
                MVOrden.ObtenerProductosDeOrden(UidOrden.ToString());
                MVSucursal.BuscarSucursales(UidSucursal: MVOrden.ListaDeProductos[0].UidSucursal.ToString());

                t.AddHeaderLine("Sucursal: " + MVSucursal.IDENTIFICADOR + "");
                t.AddHeaderLine("Horario de " + MVSucursal.HORAAPARTURA + " a " + MVSucursal.HORACIERRE + "");

                MVDireccion.ObtenerDireccionDeOrden(UidOrden.ToString(), "Recolecta");
                string DireccionAEntregar = "";
                string DireccionAEntregar1 = "";
                string DireccionAEntregar2 = "";
                string DireccionAEntregar3 = "";
                Guid UidDireccionAEntregar = new Guid();
                foreach (var item in MVDireccion.ListaDIRECCIONES)
                {
                    UidDireccionAEntregar = item.ID;
                    DireccionAEntregar = " " + item.PAIS + ",  " + item.ESTADO + ", ";
                    DireccionAEntregar1 = item.MUNICIPIO + ", " + item.COLONIA + ", ";
                    DireccionAEntregar2 = item.CodigoPostal + ", Mza " + item.MANZANA + ", Lt " + item.LOTE + ",";
                    DireccionAEntregar3 = "Calle " + item.CALLE0; ;
                }

                t.AddHeaderLine(DireccionAEntregar);
                t.AddHeaderLine(DireccionAEntregar1);
                t.AddHeaderLine(DireccionAEntregar2);
                t.AddHeaderLine(DireccionAEntregar3);

                t.AddHeaderLine("Fecha: " + fila.FechaDeOrden + "");

                t.AddSubHeaderLine("Folio: " + fila.LNGFolio.ToString() + "");
                //Configuracion body
                decimal total = 0.0m;
                for (int i = 0; i < MVOrden.ListaDeProductos.Count; i++)
                {
                    VMOrden item = MVOrden.ListaDeProductos[i];
                    t.AddItem(item.intCantidad.ToString(), item.StrNombreProducto.ToString(), item.MTotal.ToString());
                    MVOrden.ObtenerNotaDeProductoEnOrden(item.UidProductoEnOrden);
                    if (!string.IsNullOrEmpty(MVOrden.StrNota))
                    {
                        t.AddItem("Nota->", MVOrden.StrNota, "");
                    }
                    if (i < (MVOrden.ListaDeProductos.Count - 1))
                    {
                        t.AddItem("------", "--------------------", "-------");
                    }
                    total = total + item.MTotal;
                }
                MVDireccion.ObtenerDireccionDeOrden(UidOrden.ToString(), "Entrega");
                DireccionAEntregar = "";
                DireccionAEntregar1 = "";
                DireccionAEntregar2 = "";
                DireccionAEntregar3 = "";

                foreach (var item in MVDireccion.ListaDIRECCIONES)
                {
                    UidDireccionAEntregar = item.ID;
                    DireccionAEntregar = " " + item.PAIS + ",  " + item.ESTADO + ", ";
                    DireccionAEntregar1 = item.MUNICIPIO + ", " + item.COLONIA + ", ";
                    DireccionAEntregar2 = item.CodigoPostal + ", Mza " + item.MANZANA + ", Lt " + item.LOTE + ",";
                    DireccionAEntregar3 = "Calle " + item.CALLE0; ;
                }

                //Configuracion header footer
                //Agrega un subtotal
                t.AddTotal("Subtotal", total.ToString());
                //Busca el tarifario y lo agrega al total
                MVTarifario.ObtenerTarifarioDeOrden(UidOrden);
                t.AddTotal("Envio", MVTarifario.DPrecio.ToString("N2"));
                //Agrega el total general
                total = total + MVTarifario.DPrecio;
                t.AddTotal("Total", total.ToString("N2"));
                //Datos del usuario
                VMUsuarios MVUsuario = new VMUsuarios();
                MVUsuario.BusquedaDeUsuario(UidUsuario: new Guid(MVOrden.ObtenerUsuarioPorUidOrdenSucursal(UidOrden)), UIDPERFIL: new Guid("4F1E1C4B-3253-4225-9E46-DD7D1940DA19"));

                t.AddFooterLine("Cliente " + MVUsuario.StrUsuario);


                t.AddFooterLine("Direccion de entrega");
                t.AddFooterLine(DireccionAEntregar);
                t.AddFooterLine(DireccionAEntregar1);
                t.AddFooterLine(DireccionAEntregar2);
                t.AddFooterLine(DireccionAEntregar3);

                t.FontSize = 6;
                t.AddFooterLine("www.godeliverix.com.mx");
                t.PrintTicket("PDFCreator");


                //Cambia el estatus interno de la sucursal confirmando la orden
                MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EC09BCDE-ADAC-441D-8CC1-798BC211E46E"), "S", MVLicencia.Licencia, UidOrden: UidOrden);
                MVOrden.AgregaEstatusALaOrden(new Guid("2d2f38b8-7757-45fb-9ca6-6ecfe20356ed"), UidOrden: UidOrden, UidLicencia: new Guid(MVLicencia.Licencia), StrParametro: "S");
                CargaContenido("Recibidas");
                MVLicencia = new VMLicencia();
                MVLicencia.RecuperaLicencia();
                MVTarifario.AgregarCodigoAOrdenTarifario(UidCodigo: Guid.NewGuid(), UidLicencia: new Guid(MVLicencia.Licencia), uidorden: UidOrden);
            }
            else
            {

            }

        }

        private void btnCancelarOrdenRecibida_Click(object sender, RoutedEventArgs e)
        {
            CancelarOrden ventana = new CancelarOrden(tabPaginas);
            ventana.ShowDialog();
            CargaContenido("Recibidas");
        }

        private void btnRestaturar_Click(object sender, RoutedEventArgs e)
        {
            Button ID = ((Button)sender);

            if (ID != null)
            {
                MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("B40D954D-D408-4769-B110-608436C490F1"), "S", MVLicencia.Licencia, LngFolio: long.Parse(ID.CommandParameter.ToString()));
            }
        }

        #endregion

        #region Panel de ordenes recibidas
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MVLicencia.RecuperaLicencia();
            Guid Licencia = new Guid(MVLicencia.Licencia);

            long Folio = long.Parse(txbNumerodeOrden.Text);
            Guid uidorden = new Guid(txtElaborarUidOrden.Text);
            if (lblTextoAccion.Text == "Elaborar")
            {
                //Cambia el estatus de la orden a creado
                MVOrden.AgregaEstatusALaOrden(new Guid("2d2f38b8-7757-45fb-9ca6-6ecfe20356ed"), UidOrden: uidorden, UidLicencia: Licencia, StrParametro: "S");
                lblTextoAccion.Text = "Finalizar";
                iconbtnAccion.Kind = MaterialDesignThemes.Wpf.PackIconKind.PackageVariantClosed;
                //Agrega el codigo al tarifario
                MVTarifario.AgregarCodigoAOrdenTarifario(UidCodigo: Guid.NewGuid(), UidLicencia: Licencia, uidorden: uidorden);
            }
            else
            if (lblTextoAccion.Text == "Finalizar")
            {
                //Cambia el estatus de la orden a elaborado
                MVOrden.AgregaEstatusALaOrden(new Guid("c412d367-7d05-45d8-aeca-b8fabbf129d9"), UidOrden: uidorden, UidLicencia: Licencia, StrParametro: "S");

                //Crea el codigo para que el repartidor pueda recoger la orden
                DGCDetallesOrden.ItemsSource = null;

                lblTextoAccion.Text = "Elaborar";
                iconbtnAccion.Kind = MaterialDesignThemes.Wpf.PackIconKind.RedoVariant;

            }
            CargaContenido("Recibidas");
        }

        private void DataGridOrdenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridOrdenes.SelectedItem != null)
            {
                VMOrden fila = (VMOrden)DataGridOrdenes.SelectedItem;
                MVLicencia.RecuperaLicencia();
                string sucursal = MVSucursal.ObtenSucursalDeLicencia(MVLicencia.Licencia);


                txtElaborarUidOrden.Text = fila.Uidorden.ToString();
                txbNumerodeOrden.Text = fila.LNGFolio.ToString();
                MVOrden.ObtenerProductosDeOrden(fila.Uidorden.ToString());
                DGCDetallesOrden.ItemsSource = MVOrden.ListaDeProductos;

                if (fila.UidEstatus == new Guid("DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC".ToLower()))
                {
                    lblTextoAccion.Text = "Elaborar";
                    iconbtnAccion.Kind = MaterialDesignThemes.Wpf.PackIconKind.RedoVariant;
                }
                if (fila.UidEstatus == new Guid("2d2f38b8-7757-45fb-9ca6-6ecfe20356ed"))
                {
                    lblTextoAccion.Text = "Finalizar";
                    iconbtnAccion.Kind = MaterialDesignThemes.Wpf.PackIconKind.PackageVariantClosed;

                }

            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarOrdenes("Confirmadas");
        }

        private void BtnLimpiarFiltros_Click(object sender, RoutedEventArgs e)
        {
            txtNumeroDeOrden.Text = string.Empty;
            DtmFechaFinal.Text = string.Empty;
            DtmFechaInicial.Text = string.Empty;

        }

        private void btnCCancelar_Click(object sender, RoutedEventArgs e)
        {
            CancelarOrden ventana = new CancelarOrden(tabPaginas);
            ventana.ShowDialog();
            CargaContenido("Confirmacion");
        }
        #endregion

        #region Panel de ordenes para envio
        private void dgOrdenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOrdenesAEnviar.SelectedItem != null)
            {
                VMOrden filaOrden = (VMOrden)dgOrdenesAEnviar.SelectedItem;
                lblNumeroDeOrden.Content = filaOrden.LNGFolio.ToString();
            }
        }

        private void btnEnviarOrden_Click(object sender, RoutedEventArgs e)
        {
            //Valida que este seleccionada una orden
            if (dgOrdenesAEnviar.SelectedIndex == -1)
            {
                MessageBox.Show("Elegir orden a asignar");
            }
            //Agrega el registro de la relacion de la orden con sucursal
            else if (dgOrdenesAEnviar.SelectedIndex != -1)
            {
                VMOrden filaOrden = (VMOrden)dgOrdenesAEnviar.SelectedItem;
                dgOrdenesAEnviar.SelectedIndex = -1;
                VerificaOrdenesEnviadas();
            }
        }

        private void VerificaOrdenesEnviadas()
        {
            //Obtiene los valores de las listas de la base de datos
            MVLicencia.RecuperaLicencia();
            MVOrden.ObtenerOrdenSucursalDistribuidora(MVLicencia.Licencia);
            MVSucursal.ObtenerSucursalesDistribuidorasContratadas(MVLicencia.Licencia);
            for (int i = 0; i < MVOrden.ListaDeOrdenes.Count; i++)
            {
                if (MVOrden.ListaDeBitacoraDeOrdenes.Exists(orden => orden.Uidorden == MVOrden.ListaDeOrdenes[i].Uidorden))
                {
                    MVOrden.ListaDeOrdenes.RemoveAt(i);
                    i = i - 1;
                }
            }
        }
        #endregion


        #region Metodos generales
        private void tabPaginas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedTab = (sender as TabControl).SelectedItem as TabItem;
            if (selectedTab != null)
            {
                if (selectedTab.Name == "TIRecibidas")
                {
                    CargaContenido("Recibidas");
                }
                if (selectedTab.Name == "TIAsignadas")
                {
                    if (!IsLoaded)
                    {
                        dgOrdenesAEnviar.SelectedIndex = -1;
                    }
                    CargaContenido("Asignadas");
                }
                if (selectedTab.Name == "tabConfirmacion")
                {
                    CargaContenido("Confirmacion");

                }
                if (selectedTab.Name == "TICanceladas")
                {
                    CargaContenido("Canceladas");
                }
            }
        }

        private void CargaDatagrid(string DataGrid)
        {
            switch (DataGrid)
            {
                case "PendienteAConfirmar":
                    DGOrdenesAConfirmar.ItemsSource = MVOrden.ListaDeOrdenes;
                    DGOrdenesAConfirmar.Items.Refresh();
                    break;
                case "Confirmadas":
                    DataGridOrdenes.ItemsSource = MVOrden.ListaDeOrdenes;
                    DataGridOrdenes.Items.Refresh();
                    break;
                case "Listas a enviar":
                    dgOrdenesAEnviar.ItemsSource = MVOrden.ListaDeOrdenes;
                    dgOrdenesAEnviar.Items.Refresh();
                    break;

                case "Sucursales":
                    //DGSucursalesDistribuidoras.ItemsSource = MVSucursal.LISTADESUCURSALES;
                    //DGSucursalesDistribuidoras.Items.Refresh();
                    break;
                case "CanceladasDefinitivas":
                    DataGridOrdenesCanceladasDefinitivas.ItemsSource = MVOrden.ListaDeOrdenesCanceladas;
                    DataGridOrdenesCanceladasDefinitivas.Items.Refresh();
                    break;
                case "Recuperar":
                    DataGridOrdenesCanceladas.ItemsSource = MVOrden.ListaDeOrdenes;
                    DataGridOrdenesCanceladas.Items.Refresh();
                    break;
                default:
                    break;
            }
        }

        private void BuscarOrdenes(string TipoDeOrden)
        {
            if (AccesoInternet())
            {
                MVLicencia.RecuperaLicencia();
                string sucursal = MVSucursal.ObtenSucursalDeLicencia(MVLicencia.Licencia);
                switch (TipoDeOrden)
                {

                    case "A confirmar":
                        if (!string.IsNullOrWhiteSpace(txtOACNumeroDeOrden.Text) || !string.IsNullOrWhiteSpace(DtpOACFechaIncial.Text) || !string.IsNullOrWhiteSpace(DtpOACFechaFinal.Text))
                        {
                            if (!string.IsNullOrEmpty(DtpOACFechaIncial.Text) && !string.IsNullOrEmpty(DtpOACFechaFinal.Text))
                            {
                                if (DateTime.Parse(DtpOACFechaIncial.Text) >= DateTime.Parse(DtpOACFechaFinal.Text))
                                {
                                    LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
                                    System.Windows.Controls.Label lblMensaje = VentanaMensaje.FindName("lblMensaje") as System.Windows.Controls.Label;
                                    lblMensaje.Content = "La fecha inicial no puede ser mayor a la final!";
                                    VentanaMensaje.ShowDialog();
                                }
                            }
                            else
                            {
                                MVOrden.BuscarOrdenes("Sucursal", FechaInicial: DtmFechaInicial.Text, FechaFinal: DtmFechaFinal.Text, NumeroOrden: txtNumeroDeOrden.Text, UidLicencia: new Guid(MVLicencia.Licencia), EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S");
                            }
                        }
                        else
                        {
                            MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(MVLicencia.Licencia), EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S");
                        }
                        CargaDatagrid("PendienteAConfirmar");
                        break;
                    case "Confirmadas":

                        if (!string.IsNullOrWhiteSpace(txtNumeroDeOrden.Text) || !string.IsNullOrWhiteSpace(DtmFechaFinal.Text) || !string.IsNullOrWhiteSpace(DtmFechaInicial.Text))
                        {
                            if (!string.IsNullOrEmpty(DtmFechaInicial.Text) && !string.IsNullOrEmpty(DtmFechaFinal.Text))
                            {
                                if (DateTime.Parse(DtmFechaInicial.Text) >= DateTime.Parse(DtmFechaFinal.Text))
                                {
                                    LicenciaRequerida VentanaMensaje = new LicenciaRequerida();
                                    System.Windows.Controls.Label lblMensaje = VentanaMensaje.FindName("lblMensaje") as System.Windows.Controls.Label;
                                    lblMensaje.Content = "La fecha inicial no puede ser mayor a la final!";
                                    VentanaMensaje.ShowDialog();
                                }
                            }
                            else
                            {
                                MVOrden.BuscarOrdenes("Sucursal", FechaInicial: DtmFechaInicial.Text, FechaFinal: DtmFechaFinal.Text, NumeroOrden: txtNumeroDeOrden.Text, UidLicencia: new Guid(MVLicencia.Licencia), EstatusSucursal: "Pendiente para elaborar", TipoDeSucursal: "S");
                            }
                        }
                        else
                        {
                            MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(MVLicencia.Licencia), EstatusSucursal: "Pendiente para elaborar", TipoDeSucursal: "S");
                        }
                        CargaDatagrid("Confirmadas");
                        break;
                    default:
                        break;
                }
            }
        }

        private void CargaContenido(string TabSeccion)
        {
            if (AccesoInternet())
            {
                switch (TabSeccion)
                {
                    case "Confirmacion":

                        //Modificacion del timer 
                        Timer.Stop();
                        Timer.Interval = new TimeSpan(0, 0, 10);
                        Timer.Start();
                        BuscarOrdenes("A confirmar");

                        //txbNumerodeOrden.Text = string.Empty;
                        DGCDetallesOrden.ItemsSource = null;
                        DGCDetallesOrden.Items.Refresh();

                        break;
                    case "Recibidas":
                        //Modificacion del timer 
                        Timer.Stop();
                        Timer.Interval = new TimeSpan(0, 0, 10);
                        Timer.Start();
                        BuscarOrdenes("Confirmadas");
                        //txbNumerodeOrden.Text = string.Empty;
                        GridViewDetalleOrdenConfirmar.ItemsSource = null;
                        GridViewDetalleOrdenConfirmar.Items.Refresh();

                        txtCNumeroOrden.Text = string.Empty;
                        break;
                    case "Asignadas":
                        //Modificacion del timer 
                        Timer.Stop();
                        Timer.Interval = new TimeSpan(0, 0, 10);
                        Timer.Start();
                        //Obtiene las ordenes listas para asignar
                        MVOrden.Uidorden = Guid.Empty;
                        MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(MVLicencia.Licencia), EstatusSucursal: "Lista a enviar", TipoDeSucursal: "S");
                        VerificaOrdenesEnviadas();
                        //Valida que no este seleccionada una orden.
                        if (dgOrdenesAEnviar.SelectedIndex == -1)
                        {
                            lblNumeroDeOrden.Content = string.Empty;
                            //DGSucursalesDistribuidoras.IsEnabled = false;
                            CargaDatagrid("Listas a enviar");
                        }
                        //Valida que no este seleccionado un registro
                        //if (DGSucursalesDistribuidoras.SelectedIndex == -1)
                        //{
                        //    lblSucursal.Content = string.Empty;
                        //}
                        //Carga la bitacora
                        CargaDatagrid("Bitacora");

                        break;
                    case "Canceladas":

                        //Modificacion del timer 
                        Timer.Stop();
                        Timer.Interval = new TimeSpan(0, 0, 1);
                        Timer.Start();
                        MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(MVLicencia.Licencia), EstatusSucursal: "Canceladas", TipoDeSucursal: "S");
                        CargaDatagrid("Recuperar");
                        CargaDatagrid("CanceladasDefinitivas");
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        private void btnFiltrosConfirmados_Click(object sender, RoutedEventArgs e)
        {
            if (pnlConfirmadasFiltros.Visibility == Visibility.Visible)
            {
                pnlConfirmadasFiltros.Visibility = Visibility.Collapsed;
            }
            else
            if (pnlConfirmadasFiltros.Visibility == Visibility.Collapsed)
            {
                pnlConfirmadasFiltros.Visibility = Visibility.Visible;
            }
        }

        private void btnFiltrosAConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (pnlFiltrosConfirmarOrden.Visibility == Visibility.Visible)
            {
                pnlFiltrosConfirmarOrden.Visibility = Visibility.Collapsed;
            }
            else
            if (pnlFiltrosConfirmarOrden.Visibility == Visibility.Collapsed)
            {
                pnlFiltrosConfirmarOrden.Visibility = Visibility.Visible;
            }
        }

        private void btnBuscarOrden_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtLectorCodigo.Text))
            {
                if (txtLectorCodigo.Text.Length == 36)
                {
                    MVLicencia = new VMLicencia();
                    MVLicencia.RecuperaLicencia();
                    MVOrden.BuscarOrdenRepartidor(txtLectorCodigo.Text.Replace("'", "-"), MVLicencia.Licencia);
                    lblUidOrdenAEnviar.Content = string.Empty;
                    lblNumeroDeOrden.Content = string.Empty;
                    lblNombreEmpresaDistribuidora.Content = string.Empty;
                    lblMensajeOrden.Content = string.Empty;

                    if (MVOrden.StrEstatusOrdenSucursal != null)
                    {
                        if (MVOrden.StrEstatusOrdenSucursal.ToString() == "C412D367-7D05-45D8-AECA-B8FABBF129D9".ToLower())
                        {
                            lblUidOrdenAEnviar.Content = MVOrden.Uidorden.ToString();
                            lblNumeroDeOrden.Content = MVOrden.LNGFolio;
                            lblNombreEmpresaDistribuidora.Content = MVOrden.StrNombreSucursal;
                            lblMensajeOrden.Content = "";

                        }
                        else if (MVOrden.StrEstatusOrdenSucursal.ToString() == "B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7".ToLower())
                        {
                            lblMensajeOrden.Content = "La orden ya ha sido enviada";
                        }
                        else
                        {
                            lblMensajeOrden.Content = "La orden no esta lista para ser entregada al repartidor";
                        }
                    }
                    else
                    {
                        lblMensajeOrden.Content = "No hay coincidencia con el codigo";
                    }
                }
                else
                {
                    lblMensajeOrden.Content = "Codigo invalido";
                }
                txtLectorCodigo.Text = string.Empty; txtLectorCodigo.Focus();
            }
        }

        private void btnAsignarOrden_Click(object sender, RoutedEventArgs e)
        {
            if (lblUidOrdenAEnviar.Content != null)
            {
                MVLicencia = new VMLicencia();
                MVLicencia.RecuperaLicencia();
                Guid UidOrden = new Guid(lblUidOrdenAEnviar.Content.ToString());
                //Cambia elestatus interno de la sucursal confirmando la orden
                MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("E2BAD7D9-9CD0-4698-959D-0A211800545F"), "S", MVLicencia.Licencia, UidOrden: UidOrden);
                MVOrden.AgregaEstatusALaOrden(new Guid("B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7"), UidOrden: UidOrden, UidLicencia: new Guid(MVLicencia.Licencia), StrParametro: "S");

                lblUidOrdenAEnviar.Content = string.Empty;
                lblNumeroDeOrden.Content = string.Empty;
                lblNombreEmpresaDistribuidora.Content = string.Empty;
            }
            else
            {
                lblMensajeOrden.Content = "No hay orden alguna para asignar";
            }
        }

        private void btnVerNota_Click(object sender, RoutedEventArgs e)
        {
            if (AccesoInternet())
            {
                VMOrden row = (VMOrden)((Button)e.Source).DataContext;
                NotasDeproductos VentanaNotas = new NotasDeproductos();
                TextBlock lblMensaje = VentanaNotas.FindName("txbNota") as TextBlock;
                Label lblNombreProducto = VentanaNotas.FindName("lblNombreProducto") as Label;
                MVOrden.ObtenerNotaDeProductoEnOrden(row.UidProductoEnOrden);
                lblMensaje.Text = MVOrden.StrNota;
                lblNombreProducto.Content = row.StrNombreProducto;
                VentanaNotas.ShowDialog();
            }
        }
    }
}
