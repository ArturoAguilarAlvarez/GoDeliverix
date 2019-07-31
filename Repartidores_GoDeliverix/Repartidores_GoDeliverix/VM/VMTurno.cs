using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Repartidores_GoDeliverix.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMTurno : ControlsController
    {
        string url = "";
        HttpClient _WebApiGoDeliverix = new HttpClient();
        VistaDelModelo.VMTurno MVTurno;
        VistaDelModelo.VMTurno MVTurnoInformacion;
        private Guid _UidTurnoRepartidor;
        public Guid UidTurnoRepartidor
        {
            get { return _UidTurnoRepartidor; }
            set { SetValue(ref _UidTurnoRepartidor, value); }
        }

        private Color _ColorProp;
        public Color ColorProp
        {
            get { return _ColorProp; }
            set { SetValue(ref _ColorProp, value); }
        }

        private string _DtmHoraInicio;
        public string DtmHoraInicio
        {
            get { return _DtmHoraInicio; }
            set { SetValue(ref _DtmHoraInicio, value); }
        }

        private string _DtmHoraFin;
        public string DtmHoraFin
        {
            get { return _DtmHoraFin; }
            set { SetValue(ref _DtmHoraFin, value); }
        }

        private string _LngFolio;
        public string LngFolio
        {
            get { return _LngFolio; }
            set { SetValue(ref _LngFolio, value); }
        }

        private string _Texto;

        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }

        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set { SetValue(ref _Total, value); }
        }

        private decimal __TotalEnvio;

        public decimal TotalEnvio
        {
            get { return __TotalEnvio; }
            set { SetValue(ref __TotalEnvio, value); }
        }
        private decimal __TotalSuministros;

        public decimal TotalSuministros
        {
            get { return __TotalSuministros; }
            set { SetValue(ref __TotalSuministros, value); }
        }
        private int __CantidadDeOrdenes;

        public int CantidadDeOrdenes
        {
            get { return __CantidadDeOrdenes; }
            set { SetValue(ref __CantidadDeOrdenes, value); }
        }

        public ICommand Activar { get { return new RelayCommand(ActivarTurno); } }

        private async void ActivarTurno()
        {
            var AppInstance = MainViewModel.GetInstance();
            //_WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");
            if (UidTurnoRepartidor == Guid.Empty)
            {
                UidTurnoRepartidor = Guid.NewGuid();
                AppInstance.Session_.UidTurnoRepartidor = UidTurnoRepartidor;

            }
            //else
            //{
            //    AppInstance.Session_.UidTurnoRepartidor = Guid.Empty;
            //}
            url = "http://www.godeliverix.net/api/Turno/GetTurnoRepartidor?UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidTurno=" + UidTurnoRepartidor + "";
            await _WebApiGoDeliverix.GetStringAsync(url);
            CargarTurno();
        }

        public VMTurno()
        {
            CargarTurno();
        }

        public async void CargarTurno()
        {
            MVTurno = new VistaDelModelo.VMTurno();
            var AppInstance = MainViewModel.GetInstance();
            //_WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");
            url = "http://www.godeliverix.net/api/Turno/GetConsultaUltimoTurno?UidUsuario=" + AppInstance.Session_.UidUsuario + "";
            var datos = await _WebApiGoDeliverix.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
            MVTurno = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
            UidTurnoRepartidor = MVTurno.UidTurno;
            LngFolio = MVTurno.LngFolio.ToString();
            DtmHoraInicio = MVTurno.DtmHoraInicio.ToString();
            AppInstance.Session_.UidTurnoRepartidor = MVTurno.UidTurno;
            if (MVTurno.DtmHoraFin == DateTime.Parse("01/01/0001 12:00:00 a. m."))
            {
                Texto = "Cerrar turno";
                ColorProp = Color.Red;
                url = "http://www.godeliverix.net/api/Turno/GetInformacionDeOrdenesPorTuno?UidTurno=" + AppInstance.Session_.UidTurnoRepartidor + "";
                datos = await _WebApiGoDeliverix.GetStringAsync(url);
                obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                MVTurnoInformacion = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                CantidadDeOrdenes = MVTurnoInformacion.DTotal;
                TotalSuministros = MVTurnoInformacion.DTotalSucursal;
                TotalEnvio = MVTurnoInformacion.DTotalEnvio;
                Total = TotalSuministros + TotalEnvio;

            }
            else
            {
                UidTurnoRepartidor = Guid.Empty;
                LngFolio = string.Empty;
                DtmHoraInicio = string.Empty;
                CantidadDeOrdenes = 0;
                TotalSuministros = 0.0m;
                TotalEnvio = 0.0m;
                Total = 0.0m;
                AppInstance.Session_.UidTurnoRepartidor = UidTurnoRepartidor;
                Texto = "Abrir turno";
                ColorProp = Color.Green;
            }
        }
    }
}
