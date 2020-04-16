using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Repartidores_GoDeliverix.Helpers;
using Repartidores_GoDeliverix.Views.Popup;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMTurnoOrden : ControlsController
    {
        VistaDelModelo.VMTurno MVTurno;

        private decimal __HOTotalEnvio;
        private string _UidTurnoSeleccionado;

        public string UidTurnoSeleccionado
        {
            get { return _UidTurnoSeleccionado; }
            set { _UidTurnoSeleccionado = value; }
        }

        public decimal HOTotalEnvio
        {
            get { return __HOTotalEnvio; }
            set { SetValue(ref __HOTotalEnvio, value); }
        }
        private decimal __HOTotalSuministros;
        public decimal HOTotalSuministros
        {
            get { return __HOTotalSuministros; }
            set { SetValue(ref __HOTotalSuministros, value); }
        }

        private string _HOLngFolio;
        public string HOLngFolio
        {
            get { return _HOLngFolio; }
            set { SetValue(ref _HOLngFolio, value); }
        }
        private string _StrNombreUsuario;
        public string StrNombreUsuario
        {
            get { return _StrNombreUsuario; }
            set { SetValue(ref _StrNombreUsuario, value); }
        }
        private string _StrNombreSucursal;
        public string StrNombreSucursal
        {
            get { return _StrNombreSucursal; }
            set { SetValue(ref _StrNombreSucursal, value); }
        }

        private Guid _UidTurnoRepartidor;
        public Guid UidTurnoRepartidor
        {
            get { return _UidTurnoRepartidor; }
            set { SetValue(ref _UidTurnoRepartidor, value); }
        }

        private int _IntCantidadDeOrdenes;
        public int IntCantidadDeOrdenes
        {
            get { return _IntCantidadDeOrdenes; }
            set { SetValue(ref _IntCantidadDeOrdenes, value); }
        }

        private decimal _MTotalEnvio;
        public decimal MTotalEnvio
        {
            get { return _MTotalEnvio; }
            set { SetValue(ref _MTotalEnvio, value); }
        }

        private decimal _MTotalOrdenes;
        public decimal MTotalOrdenes
        {
            get { return _MTotalOrdenes; }
            set { SetValue(ref _MTotalOrdenes, value); }
        }
        private DateTime _DtmFechaRegistro;
        public DateTime DtmFechaRegistro
        {
            get { return _DtmFechaRegistro; }
            set { SetValue(ref _DtmFechaRegistro, value); }
        }

        private decimal _MTotalGeneral;
        public decimal MTotalGeneral
        {
            get { return _MTotalGeneral; }
            set { SetValue(ref _MTotalGeneral, value); }
        }

        private decimal _DPropina;
        public decimal DPropina
        {
            get { return _DPropina; }
            set { SetValue(ref _DPropina, value); }
        }

        private decimal _DTotalPropina;
        public decimal DTotalPropina
        {
            get { return _DTotalPropina; }
            set { SetValue(ref _DTotalPropina, value); }
        }

        private List<VMTurnoOrden> _ListaDeHistoricoDeOrdenesTurno;
        public List<VMTurnoOrden> ListaDeHistoricoDeOrdenesTurnos
        {
            get { return _ListaDeHistoricoDeOrdenesTurno; }
            set { SetValue(ref _ListaDeHistoricoDeOrdenesTurno, value); }
        }

        private List<VMTurnoOrden> _ListaDeLiquidacionesTurno;
        public List<VMTurnoOrden> ListaDeLiquidacionesTurno
        {
            get { return _ListaDeLiquidacionesTurno; }
            set { SetValue(ref _ListaDeLiquidacionesTurno, value); }
        }
        public ICommand Liquidaciones { get { return new RelayCommand(InformacionDeLiquidaciones); } }

        private async void InformacionDeLiquidaciones()
        {
            var AppInstance = MainViewModel.GetInstance();
            if (AppInstance.Session_.UidUsuario != Guid.Empty)
            {
                using (var _WebApiGoDeliverix = new HttpClient())
                {
                    string url = "" + settings.Sitio + "api/Turno/GetConsultaLiquidacionesTurno?UidTurnoRepartidor=" + UidTurnoSeleccionado + "";
                    var datos = await _WebApiGoDeliverix.GetStringAsync(url);
                    var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                    MVTurno = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                }
                ListaDeLiquidacionesTurno = new List<VMTurnoOrden>();
                foreach (var item in MVTurno.ListaDeLiquidaciones)
                {
                    ListaDeLiquidacionesTurno.Add(new VMTurnoOrden()
                    {
                        StrNombreUsuario = item.strUsuario,
                        StrNombreSucursal = item.StrNombre,
                        DtmFechaRegistro = item.DtmHoraInicio,
                        MTotalOrdenes = item.DTotal
                    });
                }
                await App.Current.MainPage.Navigation.PushAsync(new Historico_DetalleOrdenes());
            }
        }
        public VMTurnoOrden(String UidTurnoRepartidor)
        {
            CargaOrdenes(UidTurnoRepartidor);
            UidTurnoSeleccionado = UidTurnoRepartidor;
        }

        public VMTurnoOrden()
        {
        }

        private async void CargaOrdenes(String UidTurnoRepartidor)
        {
            var AppInstance = MainViewModel.GetInstance();
            if (AppInstance.Session_.UidUsuario != Guid.Empty)
            {
                if (UidTurnoRepartidor != Guid.Empty.ToString())
                {
                    using (var _WebApiGoDeliverix = new HttpClient())
                    {
                        string url = "" + settings.Sitio + "api/Turno/GetInformacionHistoricoOrdenesTurno?UidTurno=" + UidTurnoRepartidor + "";
                        var datos = await _WebApiGoDeliverix.GetStringAsync(url);
                        var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                        MVTurno = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                    }

                    ListaDeHistoricoDeOrdenesTurnos = new List<VMTurnoOrden>();
                    if (MVTurno.ListaDeTurnos != null)
                    {
                        decimal totalenvio = 0.0m;
                        decimal totalOrden = 0.0m;
                        decimal totalPropina = 0.0m;
                        foreach (var item in MVTurno.ListaDeTurnos)
                        {
                            ListaDeHistoricoDeOrdenesTurnos.Add(new VMTurnoOrden()
                            {
                                HOTotalSuministros = item.DTotalSucursal,
                                HOTotalEnvio = item.DTotalEnvio,
                                HOLngFolio = item.LngFolio.ToString(),
                                DPropina = item.DPropina
                            });
                            totalenvio += item.DTotalEnvio;
                            totalOrden += item.DTotalSucursal;
                            totalPropina += item.DPropina;
                        }
                        this.DTotalPropina = totalPropina;
                        this.MTotalEnvio = totalenvio;
                        this.MTotalOrdenes = totalOrden;
                        MTotalGeneral = this.MTotalEnvio + this.MTotalOrdenes;
                        IntCantidadDeOrdenes = ListaDeHistoricoDeOrdenesTurnos.Count;
                    }
                }

            }
            else
            {
            }
        }
    }
}
