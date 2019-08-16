using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Repartidores_GoDeliverix.Helpers;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMTurnoOrden : ControlsController
    {
        HttpClient _WebApiGoDeliverix = new HttpClient();
        VistaDelModelo.VMTurno MVTurno;

        private decimal __HOTotalEnvio;

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

        private decimal _MTotalGeneral;
        public decimal MTotalGeneral
        {
            get { return _MTotalGeneral; }
            set { SetValue(ref _MTotalGeneral, value); }
        }

        private List<VMTurnoOrden> _ListaDeHistoricoDeOrdenesTurno;

        public List<VMTurnoOrden> ListaDeHistoricoDeOrdenesTurnos
        {
            get { return _ListaDeHistoricoDeOrdenesTurno; }
            set { SetValue(ref _ListaDeHistoricoDeOrdenesTurno, value); }
        }


        public VMTurnoOrden(String UidTurnoRepartidor)
        {
            CargaOrdenes(UidTurnoRepartidor);
        }

        public VMTurnoOrden()
        {
        }

        private async void CargaOrdenes(String UidTurnoRepartidor)
        {
            var AppInstance = MainViewModel.GetInstance();
            if (AppInstance.Session_.UidUsuario != Guid.Empty)
            {
                string url = "http://www.godeliverix.net/api/Turno/GetInformacionHistoricoOrdenesTurno?UidTurno=" + UidTurnoRepartidor + "";
                var datos = await _WebApiGoDeliverix.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                MVTurno = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                ListaDeHistoricoDeOrdenesTurnos = new List<VMTurnoOrden>();
                if (MVTurno.ListaDeTurnos != null)
                {
                    decimal totalenvio = 0.0m;
                    decimal totalOrden = 0.0m;
                    foreach (var item in MVTurno.ListaDeTurnos)
                    {
                        ListaDeHistoricoDeOrdenesTurnos.Add(new VMTurnoOrden()
                        {
                            HOTotalSuministros = item.DTotalSucursal,
                            HOTotalEnvio = item.DTotalEnvio,
                            HOLngFolio = item.LngFolio.ToString()
                        });

                        totalenvio = totalenvio + item.DTotalEnvio;
                        totalOrden = totalOrden + item.DTotalSucursal;

                    }
                    this.MTotalEnvio = totalenvio;
                    this.MTotalOrdenes = totalOrden;
                    MTotalGeneral = this.MTotalEnvio + this.MTotalOrdenes;
                    IntCantidadDeOrdenes = ListaDeHistoricoDeOrdenesTurnos.Count;
                }
            }
            else
            {
            }
        }
    }
}
