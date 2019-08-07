using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Repartidores_GoDeliverix.Helpers;
using Repartidores_GoDeliverix.Views;
using Repartidores_GoDeliverix.Views.Popup;
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
        private int __CantidadDeOrdenes;

        public int CantidadDeOrdenes
        {
            get { return __CantidadDeOrdenes; }
            set { SetValue(ref __CantidadDeOrdenes, value); }
        }

        #region Propiedades
        private string _HDtmHoraInicio;
        public string HDtmHoraInicio
        {
            get { return _HDtmHoraInicio; }
            set { SetValue(ref _HDtmHoraInicio, value); }
        }

        private string _HDtmHoraFin;
        public string HDtmHoraFin
        {
            get { return _HDtmHoraFin; }
            set { SetValue(ref _HDtmHoraFin, value); }
        }

        private string _HLngFolio;
        public string HLngFolio
        {
            get { return _HLngFolio; }
            set { SetValue(ref _HLngFolio, value); }
        }

        private string _HOLngFolio;
        public string HOLngFolio
        {
            get { return _HOLngFolio; }
            set { SetValue(ref _HOLngFolio, value); }
        }
        private Guid _HUidTurnoRepartidor;
        public Guid HUidTurnoRepartidor
        {
            get { return _HUidTurnoRepartidor; }
            set { SetValue(ref _HUidTurnoRepartidor, value); }
        }
        #endregion
        private List<VMTurno> _ListaDeHistoricoDeTurnos;

        public List<VMTurno> ListaDeHistoricoDeTurnos
        {
            get { return _ListaDeHistoricoDeTurnos; }
            set { SetValue(ref _ListaDeHistoricoDeTurnos, value); }
        }

        private List<VMTurno> _ListaDeHistoricoDeOrdenesTurno;

        public List<VMTurno> ListaDeHistoricoDeOrdenesTurnos
        {
            get { return _ListaDeHistoricoDeOrdenesTurno; }
            set { SetValue(ref _ListaDeHistoricoDeOrdenesTurno, value); }
        }
        private VMTurno _oTurno;
        public VMTurno oTurno
        {
            get { return _oTurno; }
            set
            {
                SetValue(ref _oTurno, value);
                _oTurno.OrdenesHistoricoTurno();
            }
        }

        public ICommand Activar { get { return new RelayCommand(ActivarTurno); } }
        public ICommand Historico { get { return new RelayCommand(HistoricoTurno); } }
        public ICommand HistoricoOrdenTurno { get { return new RelayCommand(OrdenesHistoricoTurno); } }

        private async void OrdenesHistoricoTurno()
        {
            var AppInstance = MainViewModel.GetInstance();
            if (AppInstance.Session_.UidUsuario != Guid.Empty)
            {
                url = "http://www.godeliverix.net/api/Turno/GetInformacionHistoricoOrdenesTurno?UidTurno=" + HUidTurnoRepartidor + "";
                var datos = await _WebApiGoDeliverix.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                MVTurno = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                ListaDeHistoricoDeOrdenesTurnos = new List<VMTurno>();
                foreach (var item in MVTurno.ListaDeTurnos)
                {
                    if (!ListaDeHistoricoDeOrdenesTurnos.Exists(t => t.UidTurnoRepartidor == item.UidTurno))
                    {
                        ListaDeHistoricoDeOrdenesTurnos.Add(new VMTurno()
                        {
                            HOTotalSuministros = item.DTotalSucursal,
                            HOTotalEnvio = item.DTotalEnvio,
                            HOLngFolio = item.LngFolio.ToString()
                        });
                    }
                }
                await Application.Current.MainPage.Navigation.PushAsync(new Historico_DetalleDia());
            }
            else
            {
            }
        }

        private async void HistoricoTurno()
        {
            var AppInstance = MainViewModel.GetInstance();
            if (AppInstance.Session_.UidUsuario != Guid.Empty)
            {
                url = "http://www.godeliverix.net/api/Turno/GetConsultaHisstorico?UidUsuario=" + AppInstance.Session_.UidUsuario + "";
                var datos = await _WebApiGoDeliverix.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                MVTurno = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                ListaDeHistoricoDeTurnos = new List<VMTurno>();
                foreach (var item in MVTurno.ListaDeTurnos)
                {
                    if (!ListaDeHistoricoDeTurnos.Exists(t => t.UidTurnoRepartidor == item.UidTurno))
                    {
                        ListaDeHistoricoDeTurnos.Add(new VMTurno()
                        {
                            HDtmHoraInicio = item.DtmHoraInicio.ToString(),
                            HDtmHoraFin = item.DtmHoraFin.ToString(),
                            HUidTurnoRepartidor = item.UidTurno,
                            HLngFolio = item.LngFolio.ToString()
                        });
                    }
                }
                await Application.Current.MainPage.Navigation.PushAsync(new Historico_Bitacora());
            }
            else
            {
            }
        }

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

        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }
    }
}
