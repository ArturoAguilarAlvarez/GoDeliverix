﻿using DeliverixSucursales;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using VistaDelModelo;

namespace Deliverix.Wpf.Distribuidores
{
    /// <summary>
    /// Lógica de interacción para Ordenes1.xaml
    /// </summary>
    public partial class Ordenes1 : Page
    {
        VMOrden MVOrden = new VMOrden();
        VMUsuarios MVUsuario = new VMUsuarios();
        VMSucursales MVSucursal = new VMSucursales();
        VMAcceso MVAcceso = new VMAcceso();
        DispatcherTimer Timer = new DispatcherTimer();
        VMContrato MVContrato = new VMContrato();
        DeliverixSucursales.VMLicencia MVLicencia = new DeliverixSucursales.VMLicencia();

        public Ordenes1()
        {
            if (AccesoInternet())
            {
                InitializeComponent();
                LblUidRepartidor.Content = string.Empty;
                LblNumeroDeOrden.Content = string.Empty;
                CargaDatosVentanaAsignacionDeOrdenes();
                //Activa el reload de la pagina (Carga toda la informacion)
                Timer.Tick += new EventHandler(Windows_Reload);
                Timer.Interval = new TimeSpan(0, 0, 1);
                Timer.Start();
            }

        }

        private bool AccesoInternet()
        {
            try
            {
                // System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.godeliverix.net");
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
                MVOrden.ObtenerOrdenesAsignadas(MVLicencia.Licencia);
                for (int i = 0; i < MVOrden.ListaDeBitacoraDeOrdenes.Count; i++)
                {
                    if (MVSucursal.ListaDeOrdenesAsignadas.Exists(obj => obj.lngFolio == MVOrden.ListaDeBitacoraDeOrdenes[i].LNGFolio))
                    {
                        MVOrden.ListaDeBitacoraDeOrdenes.RemoveAt(i);
                        i = i - 1;
                    }
                }

                if (DataGridOrdenes.SelectedIndex != -1)
                {
                    VMOrden FilaSeleccionada = (VMOrden)DataGridOrdenes.SelectedItem;
                    DataGridOrdenes.ItemsSource = MVOrden.ListaDeBitacoraDeOrdenes;
                    DataGridOrdenes.Items.Refresh();
                    foreach (VMOrden item in DataGridOrdenes.ItemsSource)
                    {
                        if (item.LNGFolio == FilaSeleccionada.LNGFolio)
                        {
                            DataGridOrdenes.SelectedItem = item;
                        }
                    }
                }
                else
                {
                    DataGridOrdenes.ItemsSource = MVOrden.ListaDeBitacoraDeOrdenes;
                    DataGridOrdenes.Items.Refresh();
                }
                CargaDatosVentanaAsignacionDeOrdenes();
            }

        }

        private void btnAsignarRepartidor_Click(object sender, RoutedEventArgs e)
        {
            if (AccesoInternet())
            {
                bool Resultado = false;
                btnAsignarRepartidor.IsEnabled = false;
                if (!string.IsNullOrEmpty(LblUidOrden.Content.ToString()))
                {
                    Resultado = true;
                }
                else
                {
                    Resultado = false;
                    MessageBox.Show("No se ha seleccionado una orden", "Mensaje del sistema");
                }
                if (!string.IsNullOrEmpty(LblUidRepartidor.Content.ToString()))
                {
                    Resultado = true;
                }
                else
                {
                    Resultado = false;
                    MessageBox.Show("No se ha asignado un repartidor", "Mensaje del sistema");
                }
                if (Resultado)
                {
                    Guid UidRepartidor = new Guid(LblUidRepartidor.Content.ToString());
                    Guid UidTurnoRepartidor = new Guid(LblUidTurnoRepartidor.Content.ToString());
                    Guid UidOrden = new Guid(LblUidOrden.Content.ToString());
                    MVLicencia.RecuperaLicencia();
                    string licencia = MVLicencia.Licencia;
                    Guid UidOrdenRepartidor = Guid.NewGuid();
                    MVSucursal.AsignarOrdenRepartidor(UidTurnoRepartidor, UidOrden, UidOrdenRepartidor);
                    MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("42A97847-20CF-4338-994F-9E26A91619A3"), "D", MVLicencia.Licencia, UidOrden: UidOrden);
                    //Crea la orden pendiente de confirmacion
                    MVAcceso.BitacoraRegistroRepartidores(char.Parse("O"), UidRepartidor, new Guid("6294DACE-C9D1-4F9F-A942-FF12B6E7E957"), UidOrdenRepartidor);
                    MVSucursal.ObtenerOrdenesAsignadasARepartidor(licencia);
                    MVOrden.ObtenerOrdenesAsignadas(licencia);
                    DataGridBitacoraDeAsignaciones.ItemsSource = MVSucursal.ListaDeOrdenesAsignadas;
                    DataGridBitacoraDeAsignaciones.Items.Refresh();

                    for (int i = 0; i < MVOrden.ListaDeBitacoraDeOrdenes.Count; i++)
                    {
                        if (MVSucursal.ListaDeOrdenesAsignadas.Exists(obj => obj.lngFolio == MVOrden.ListaDeBitacoraDeOrdenes[i].LNGFolio))
                        {
                            MVOrden.ListaDeBitacoraDeOrdenes.RemoveAt(i);
                            i = i - 1;
                        }
                    }
                    DataGridOrdenes.ItemsSource = MVOrden.ListaDeBitacoraDeOrdenes;
                    DataGridOrdenes.Items.Refresh();
                    LblUidOrden.Content = string.Empty;
                    LblUidOrden.Content = string.Empty;
                    LblNombreRepartidor.Content = string.Empty;
                    LblNumeroDeOrden.Content = string.Empty;
                }
                else
                {
                    Resultado = false;
                    MessageBox.Show("Orden no creada.", "Mensaje del sistema");
                }
                btnAsignarRepartidor.IsEnabled = true;
            }
        }

        private void chbxSeleccionRepartidor_Checked(object sender, RoutedEventArgs e)
        {
            VMUsuarios Usuario = (VMUsuarios)DataGridRepartidores.SelectedItem;
            MVLicencia.RecuperaLicencia();
            string licencia = MVLicencia.Licencia;
            if (string.IsNullOrEmpty(LblNumeroDeOrden.Content.ToString()))
            {
                MVUsuario.SeleccionarUsuario(Usuario.Uid);
                AgregarRelacionBitacora(UidRepartidor: Usuario.Uid);
                DataGridBitacoraDeAsignaciones.ItemsSource = MVSucursal.ListaDeOrdenesAsignadas;
                LblUidRepartidor.Content = Usuario.Uid;
                LblUidTurnoRepartidor.Content = Usuario.uidTurnoRepartidor;
                LblNombreRepartidor.Content = Usuario.StrNombre;
            }
            else
            {
                if (MVContrato.VerificaPagoARecolectar(LblUidSucursal.Content.ToString(), licencia) || LblUidSucursal.Content == null)
                {
                    // MessageBox.Show("Paga al recolectar");
                    var orden = MVOrden.ListaDeBitacoraDeOrdenes.Find(u => u.Uidorden.ToString() == LblUidOrden.Content.ToString());
                    if (Usuario.MEfectivoEnMano < (orden.MTotal))
                    {
                        MessageBox.Show("No puedes asignar la orden a un repartidor sin que este tenga el dinero para pagarla al recolectar");
                    }
                    else
                    {
                        MVUsuario.SeleccionarUsuario(Usuario.Uid);
                        AgregarRelacionBitacora(UidRepartidor: Usuario.Uid);
                        DataGridBitacoraDeAsignaciones.ItemsSource = MVSucursal.ListaDeOrdenesAsignadas;
                        LblUidRepartidor.Content = Usuario.Uid;
                        LblUidTurnoRepartidor.Content = Usuario.uidTurnoRepartidor;
                        LblNombreRepartidor.Content = Usuario.StrNombre;
                    }
                }
                else
                {
                    //MessageBox.Show("No paga al recolectar");
                    MVUsuario.SeleccionarUsuario(Usuario.Uid);
                    AgregarRelacionBitacora(UidRepartidor: Usuario.Uid);
                    DataGridBitacoraDeAsignaciones.ItemsSource = MVSucursal.ListaDeOrdenesAsignadas;
                    LblUidRepartidor.Content = Usuario.Uid;
                    LblUidTurnoRepartidor.Content = Usuario.uidTurnoRepartidor;
                    LblNombreRepartidor.Content = Usuario.StrNombre;
                }
            }

        }

        private void chbxSeleccionOrden_Checked(object sender, RoutedEventArgs e)
        {
            VMOrden FilaSeleccionada = (VMOrden)DataGridOrdenes.SelectedItem;
            MVLicencia.RecuperaLicencia();
            string licencia = MVLicencia.Licencia;
            if (string.IsNullOrEmpty(LblUidRepartidor.Content.ToString()) || LblUidRepartidor.Content == null)
            {
                MVOrden.SeleccionaOrden(FilaSeleccionada.Uidorden);
                AgregarRelacionBitacora(UidOrden: FilaSeleccionada.Uidorden);
                DataGridBitacoraDeAsignaciones.ItemsSource = MVSucursal.ListaDeOrdenesAsignadas;
                LblNumeroDeOrden.Content = FilaSeleccionada.LNGFolio;
                LblUidOrden.Content = FilaSeleccionada.Uidorden;
                LblUidSucursal.Content = FilaSeleccionada.UidSucursal;
            }
            else
            {
                if (MVContrato.VerificaPagoARecolectar(FilaSeleccionada.UidSucursal.ToString(), licencia))
                {
                    // MessageBox.Show("Paga al recolectar");

                    var repartidor = MVUsuario.LISTADEUSUARIOS.Find(u => u.uidTurnoRepartidor.ToString() == LblUidTurnoRepartidor.Content.ToString());
                    if (repartidor.MEfectivoEnMano < (FilaSeleccionada.MTotal))
                    {
                        MessageBox.Show("No puedes asignar la orden a un repartidor sin que este tenga el dinero para pagarla al recolectar");
                    }
                    else
                    {
                        MVOrden.SeleccionaOrden(FilaSeleccionada.Uidorden);
                        AgregarRelacionBitacora(UidOrden: FilaSeleccionada.Uidorden);
                        DataGridBitacoraDeAsignaciones.ItemsSource = MVSucursal.ListaDeOrdenesAsignadas;
                        LblNumeroDeOrden.Content = FilaSeleccionada.LNGFolio;
                        LblUidOrden.Content = FilaSeleccionada.Uidorden;
                        LblUidSucursal.Content = FilaSeleccionada.UidSucursal;
                    }
                }
                else
                {
                    //MessageBox.Show("No paga al recolectar");
                    MVOrden.SeleccionaOrden(FilaSeleccionada.Uidorden);
                    AgregarRelacionBitacora(UidOrden: FilaSeleccionada.Uidorden);
                    DataGridBitacoraDeAsignaciones.ItemsSource = MVSucursal.ListaDeOrdenesAsignadas;
                    LblNumeroDeOrden.Content = FilaSeleccionada.LNGFolio;
                    LblUidOrden.Content = FilaSeleccionada.Uidorden;
                    LblUidSucursal.Content = FilaSeleccionada.UidSucursal;
                }
            }

        }

        protected void AgregarRelacionBitacora(Guid UidOrden = new Guid(), Guid UidRepartidor = new Guid())
        {
            if (AccesoInternet())
            {
                if (MVOrden.ListaDeBitacoraDeOrdenes.Exists(o => o.Uidorden == UidOrden && o.Seleccion == true))
                {
                    VMSucursales objeto = MVSucursal.ListaDeOrdenesAsignadas.Last();
                    if (objeto.UidOrden == UidOrden || objeto.UidUsuario == UidRepartidor)
                    {
                        if (objeto.UidOrden == UidOrden && objeto.UidUsuario != UidRepartidor && objeto.UidUsuario != Guid.Empty)
                        {
                            objeto.UidUsuario = UidRepartidor;
                        }
                        else if (objeto.UidOrden != UidOrden && objeto.UidUsuario == UidRepartidor)
                        {
                            objeto.UidOrden = UidOrden;
                        }
                    }
                    else
                    {
                        MVSucursal.ListaDeOrdenesAsignadas.Add(new VMSucursales() { ID = Guid.NewGuid(), UidOrden = UidOrden, UidUsuario = UidRepartidor });
                    }
                }
            }
        }

        private void BtnEliminarRegistro_Click(object sender, RoutedEventArgs e)
        {
            if (AccesoInternet())
            {
                object ID = ((Button)sender).CommandParameter;
                VMSucursales registro = MVSucursal.ListaDeOrdenesAsignadas.Find(o => o.ID.ToString() == ID.ToString());
                MVOrden.EliminarOrdenDeRepartidor(registro.ID.ToString());
                CargaDatosVentanaAsignacionDeOrdenes();
            }
        }

        private void CargaDatosVentanaAsignacionDeOrdenes()
        {
            try
            {
                if (AccesoInternet())
                {
                    MVLicencia.RecuperaLicencia();
                    string licencia = MVLicencia.Licencia;
                    MVSucursal.ObtenerOrdenesAsignadasARepartidor(licencia);
                    MVOrden.ObtenerOrdenesAsignadas(licencia);
                    //Obtiene los repartidores disponibles para repartir
                    MVUsuario.ObtenerRepartidoresDisponibles(licencia);
                    DataGridBitacoraDeAsignaciones.ItemsSource = MVSucursal.ListaDeOrdenesAsignadas;
                    DataGridBitacoraDeAsignaciones.Items.Refresh();
                    for (int i = 0; i < MVOrden.ListaDeBitacoraDeOrdenes.Count; i++)
                    {
                        if (MVSucursal.ListaDeOrdenesAsignadas.Exists(obj => obj.UidOrden == MVOrden.ListaDeBitacoraDeOrdenes[i].Uidorden))
                        {
                            MVOrden.ListaDeBitacoraDeOrdenes.RemoveAt(i);
                            i = i - 1;
                        }
                    }

                    for (int i = 0; i < MVUsuario.LISTADEUSUARIOS.Count; i++)
                    {
                        if (MVSucursal.ListaDeOrdenesAsignadas.Exists(obj => obj.UidTurnoRepartidor == MVUsuario.LISTADEUSUARIOS[i].uidTurnoRepartidor))
                        {
                            MVUsuario.LISTADEUSUARIOS.RemoveAt(i);
                            i = i - 1;
                        }
                    }
                    if (DataGridOrdenes.SelectedItem == null)
                    {
                        DataGridOrdenes.ItemsSource = MVOrden.ListaDeBitacoraDeOrdenes;
                        DataGridOrdenes.Items.Refresh();
                    }
                    else
                    {
                        VMOrden registroSeleccionado = (VMOrden)DataGridOrdenes.SelectedItem;
                        DataGridOrdenes.ItemsSource = MVOrden.ListaDeBitacoraDeOrdenes;

                        for (int i = 0; i < DataGridOrdenes.Items.Count; i++)
                        {
                            VMOrden registro = (VMOrden)DataGridOrdenes.Items[i];

                            if (registro.Uidorden == registroSeleccionado.Uidorden)
                            {
                                DataGridOrdenes.SelectedIndex = i;
                            }
                        }
                    }
                    if (DataGridRepartidores.SelectedItem == null)
                    {
                        DataGridRepartidores.ItemsSource = MVUsuario.LISTADEUSUARIOS;
                        DataGridRepartidores.Items.Refresh();
                    }
                    else
                    {
                        VMUsuarios registroSeleccionado = (VMUsuarios)DataGridRepartidores.SelectedItem;
                        DataGridRepartidores.ItemsSource = MVUsuario.LISTADEUSUARIOS;
                        for (int i = 0; i < DataGridRepartidores.Items.Count; i++)
                        {
                            VMUsuarios registro = (VMUsuarios)DataGridRepartidores.Items[i];
                            if (registro.Uid == registroSeleccionado.Uid)
                            {
                                DataGridRepartidores.SelectedIndex = i;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sin internet");

            }


        }

        private void BtnCerrarSession_Click(object sender, RoutedEventArgs e)
        {
            VMUsuarios Usuario = (VMUsuarios)DataGridRepartidores.SelectedItem;

            var mvturno = new VMTurno();
            mvturno.AgregaEstatusTurnoRepartidor(Usuario.uidTurnoRepartidor.ToString(), "B03E3407-F76D-4DFA-8BF9-7F059DC76141");
            MVUsuario.SeleccionarUsuario(Usuario.Uid);
            AgregarRelacionBitacora(UidRepartidor: Usuario.Uid);
            DataGridBitacoraDeAsignaciones.ItemsSource = MVSucursal.ListaDeOrdenesAsignadas;
            LblUidRepartidor.Content = Usuario.Uid;
            LblNombreRepartidor.Content = Usuario.StrNombre;

        }

        private void BtnCodigoQR_Click(object sender, RoutedEventArgs e)
        {
            if (AccesoInternet())
            {
                object ID = ((Button)sender).CommandParameter;
                var registro = MVSucursal.ListaDeOrdenesAsignadas.Find(x => x.UidOrdenTarifario == new Guid(ID.ToString()));
                var obj = new VMOrden();
                obj.ObtenerCodigoOrdenTarifario(registro.UidOrdenTarifario);
                LicenciaRequerida VentanaMensaje = new LicenciaRequerida(obj.CodigoOrdenTarifario);
                VentanaMensaje.ShowDialog();
            }
        }
    }
}
