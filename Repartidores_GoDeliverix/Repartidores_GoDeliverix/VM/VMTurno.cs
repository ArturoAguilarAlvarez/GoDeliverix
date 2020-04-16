using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Repartidores_GoDeliverix.Helpers;
using Repartidores_GoDeliverix.Views;
using Repartidores_GoDeliverix.Views.Popup;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMTurno : ControlsController
    {
        string url = "";
        VistaDelModelo.VMTurno MVTurno;
        VistaDelModelo.VMTurno MVTurnoInformacion;
        Timer tiempo;

        private Guid _UidTurnoRepartidor;
        public Guid UidTurnoRepartidor
        {
            get { return _UidTurnoRepartidor; }
            set { SetValue(ref _UidTurnoRepartidor, value); }
        }

        private string _TextoLiquidar;
        public string TextoLiquidar
        {
            get { return _TextoLiquidar; }
            set { SetValue(ref _TextoLiquidar, value); }
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
        private decimal _mFondo;

        public decimal DFondo
        {
            get { return _mFondo; }
            set { SetValue(ref _mFondo, value); }
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

        private decimal _DPropina;

        public decimal DPropina
        {
            get { return _DPropina; }
            set { SetValue(ref _DPropina, value); }
        }

        private decimal __TotalEnvio;

        public decimal TotalEnvio
        {
            get { return __TotalEnvio; }
            set { SetValue(ref __TotalEnvio, value); }
        }

        private decimal _MEfectivoActual;

        public decimal MEfectivoEnCaja
        {
            get { return _MEfectivoActual; }
            set { SetValue(ref _MEfectivoActual, value); }
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

        private VMTurno _oTurno;
        public VMTurno OTurno
        {
            get { return _oTurno; }
            set
            {
                SetValue(ref _oTurno, value);
                _oTurno.OrdenesHistoricoTurno();
            }
        }

        public ICommand Activar { get { return new RelayCommand(ActivarTurno); } }
        public ICommand OrdenesActuales { get { return new RelayCommand(VerOrdenesActuales); } }

        public ICommand Historico { get { return new RelayCommand(HistoricoTurno); } }
        public ICommand HistoricoOrdenTurno { get { return new RelayCommand(OrdenesHistoricoTurno); } }

        public Timer Tiempo { get => tiempo; set => tiempo = value; }

        private async void VerOrdenesActuales()
        {
            var AppInstance = MainViewModel.GetInstance();
            if (AppInstance.Session_.UidUsuario != Guid.Empty)
            {
                AppInstance.MVTurnoOrden = new VMTurnoOrden(UidTurnoRepartidor.ToString());
                await Application.Current.MainPage.Navigation.PushAsync(new Historico_DetalleDia());
            }
        }
        private async void OrdenesHistoricoTurno()
        {
            var AppInstance = MainViewModel.GetInstance();
            if (AppInstance.Session_.UidUsuario != Guid.Empty)
            {
                AppInstance.MVTurnoOrden = new VMTurnoOrden(HUidTurnoRepartidor.ToString());
                await Application.Current.MainPage.Navigation.PushAsync(new Historico_DetalleDia());
            }
        }

        private async void HistoricoTurno()
        {
            var AppInstance = MainViewModel.GetInstance();
            if (AppInstance.Session_.UidUsuario != Guid.Empty)
            {
                using (var _WebApiGoDeliverix = new HttpClient())
                {
                    url = "" + settings.Sitio + "api/Turno/GetConsultaHisstorico?UidUsuario=" + AppInstance.Session_.UidUsuario + "";
                    var datos = await _WebApiGoDeliverix.GetStringAsync(url);
                    var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                    MVTurno = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                }
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
        }

        private async void ActivarTurno()
        {
            try
            {
                var AppInstance = MainViewModel.GetInstance();
                if (UidTurnoRepartidor == Guid.Empty)
                {
                    var obj = string.Empty;
                    using (var _webApi = new HttpClient())
                    {
                        url = "" + settings.Sitio + "api/Turno/GetConsultaEstatusUltimoTurnoRepartidor?UidUsuario=" + AppInstance.Session_.UidUsuario + "";
                        var datos = await _webApi.GetStringAsync(url);
                        obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                    }
                    Guid UidEstatusTurno = new Guid();
                    if (string.IsNullOrEmpty(obj))
                    {
                        UidEstatusTurno = Guid.Empty;
                    }
                    else
                    {
                        UidEstatusTurno = new Guid(obj);
                    }

                    //Validacion por estatus de turno liquidado o cerrado o es el primer turno que abre
                    if (UidEstatusTurno == new Guid("38FA16DF-4727-41FD-A03E-E2E43FA78F3F") || UidEstatusTurno == new Guid("3BE9EF83-4A39-4A60-9FA9-7F50AD60CA3A") || UidEstatusTurno == Guid.Empty)
                    {
                        UidTurnoRepartidor = Guid.NewGuid();
                        AppInstance.Session_.UidTurnoRepartidor = UidTurnoRepartidor;
                        //Cambia el estatus a abierto
                        using (var _webApi = new HttpClient())
                        {
                            url = "" + settings.Sitio + "api/Turno/GetAgregaEstatusTurnoRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "&UidEstatusTurno=81494F49-F416-4431-99F4-E0AA4CF7E9F6";
                            await _webApi.GetStringAsync(url);
                        }
                    }
                    else
                    {
                        GenerateMessage("Aviso del sistema", "Para abrir el turno debes de liquidar el turno anterior", "Ok");
                    }
                }
                else
                {
                    using (var _webApi = new HttpClient())
                    {
                        //Cambia el estatus a cerrado
                        url = "" + settings.Sitio + "api/Turno/GetAgregaEstatusTurnoRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "&UidEstatusTurno=3BE9EF83-4A39-4A60-9FA9-7F50AD60CA3A";
                        await _webApi.GetStringAsync(url);
                    }
                    MEfectivoEnCaja = MEfectivoEnCaja + DFondo;
                    Total = 0;
                    DFondo = 0;
                    if (MEfectivoEnCaja > 0)
                    {
                        using (var _webApi = new HttpClient())
                        {
                            //Cambia el estatus a liquidando
                            url = "" + settings.Sitio + "api/Turno/GetAgregaEstatusTurnoRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "&UidEstatusTurno=AE28F243-AA0D-43BD-BF10-124256B75B00";
                            await _webApi.GetStringAsync(url);
                        }
                    }
                }
                using (var _webApi = new HttpClient())
                {
                    url = "" + settings.Sitio + "api/Turno/GetTurnoRepartidor?UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidTurno=" + UidTurnoRepartidor + "";
                    await _webApi.GetStringAsync(url);
                }
                CargarTurno();
            }
            catch (Exception)
            {
                GenerateMessage("Aviso del sistema", "No se puede establecer conexion", "OK");
            }
        }

        public VMTurno()
        {
            tiempo = new Timer
            {
                Interval = 6000
            };
            //enlazas un metodo al evento elapsed que es el que se ejecutara
            //cada vez que el intervalo de tiempo se cumpla
            tiempo.Elapsed += new ElapsedEventHandler(VerificaEstatusTurno);
            tiempo.Start();
            CargarTurno();
        }

        private async void VerificaEstatusTurno(object sender, ElapsedEventArgs e)
        {
            var AppInstance = MainViewModel.GetInstance();
            if (AppInstance.Session_.UidTurnoRepartidor != Guid.Empty)
            {
                using (var _webApi = new HttpClient())
                {
                    string url = "" + settings.Sitio + "api/Turno/GetConsultaEstatusTurnoRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "";
                    var datos = await _webApi.GetStringAsync(url);
                    var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                    if (obj.ToUpper() == "38FA16DF-4727-41FD-A03E-E2E43FA78F3F")
                    {
                        CargarTurno();
                        TextoLiquidar = "Liquidado";
                    }
                    else if (obj.ToUpper() == "AE28F243-AA0D-43BD-BF10-124256B75B00")
                    {
                        TextoLiquidar = "Liquidando";
                        CargarTurno();
                    }
                    else if (obj.ToUpper() == "81494F49-F416-4431-99F4-E0AA4CF7E9F6")
                    {
                        TextoLiquidar = "Abierto";
                        CargarTurno();
                    }
                    else if (obj.ToUpper() == "3BE9EF83-4A39-4A60-9FA9-7F50AD60CA3A")
                    {
                        TextoLiquidar = "Cerrado";
                        CargarTurno();
                    }
                }
            }
        }

        public async void CargarTurno()
        {
            MVTurno = new VistaDelModelo.VMTurno();
            var AppInstance = MainViewModel.GetInstance();
            string consulta2 = "" + settings.Sitio + "api/Turno/GetInformacionDeOrdenesPorTuno?UidTurno=" + AppInstance.Session_.UidTurnoRepartidor + "";
            using (var _webApi = new HttpClient())
            {
                url = "" + settings.Sitio + "api/Turno/GetConsultaUltimoTurno?UidUsuario=" + AppInstance.Session_.UidUsuario + "";
                var datos = await _webApi.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                MVTurno = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
            }

            UidTurnoRepartidor = MVTurno.UidTurno;
            AppInstance.Session_.UidTurnoRepartidor = MVTurno.UidTurno;
            if (MVTurno.DtmHoraFin == DateTime.Parse("01/01/0001 12:00:00 a. m.") && MVTurno.DtmHoraInicio != DateTime.Parse("01/01/0001 12:00:00 a. m."))
            {
                Texto = "Cerrar turno";
                ColorProp = Color.Red;
                LngFolio = MVTurno.LngFolio.ToString();
                DtmHoraInicio = MVTurno.DtmHoraInicio.ToString();
                using (var _WepApi = new HttpClient())
                {
                    var dato = await _WepApi.GetStringAsync(consulta2);
                    string obj = JsonConvert.DeserializeObject<ResponseHelper>(dato).Data.ToString();
                    MVTurnoInformacion = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                }

                CantidadDeOrdenes = MVTurnoInformacion.intTotalOrdenes;
                TotalSuministros = MVTurnoInformacion.DTotalSucursal;
                TotalEnvio = MVTurnoInformacion.DTotalEnvio;
                Total = TotalSuministros + TotalEnvio;
                DPropina = MVTurnoInformacion.DPropina;
                DFondo = MVTurno.DFondoRepartidor;
                MEfectivoEnCaja = MVTurnoInformacion.DEfectivoActual;

                //Obtiene el monto maximo a cargar por repartidor
                decimal montoMaximo = await ObtenMontoRepartidor(AppInstance.Session_.UidUsuario);

                if (montoMaximo <= MEfectivoEnCaja)
                {
                    //Cambia el estatus a liquidando
                    CambiaEstatusTurnoRepartidor("Liquidando", AppInstance.Session_.UidTurnoRepartidor.ToString());
                }
            }
            else
            {
                Guid UidEstatusTurno = new Guid();
                using (var _webApi = new HttpClient())
                {
                    url = "" + settings.Sitio + "api/Turno/GetConsultaEstatusUltimoTurnoRepartidor?UidUsuario=" + AppInstance.Session_.UidUsuario + "";
                    string datos = await _webApi.GetStringAsync(url);
                    string obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                    if (obj != null && obj != string.Empty)
                    {
                        UidEstatusTurno = new Guid(obj);
                    }
                    else
                    {
                        UidEstatusTurno = Guid.Empty;
                    }
                }
                if (UidEstatusTurno == new Guid("AE28F243-AA0D-43BD-BF10-124256B75B00"))
                {
                    using (var _webApi = new HttpClient())
                    {
                        url = "" + settings.Sitio + "api/Turno/GetInformacionDeOrdenesPorTuno?UidTurno=" + AppInstance.Session_.UidTurnoRepartidor + "";
                        string datos = await _webApi.GetStringAsync(url);
                        string obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                        MVTurnoInformacion = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                    }
                    DPropina = 0.0m;
                    LngFolio = string.Empty;
                    DtmHoraInicio = string.Empty;
                    if (MVTurnoInformacion.DTotal.ToString() == "0.0")
                    {
                        CantidadDeOrdenes = 0;
                    }
                    else
                    {
                        CantidadDeOrdenes = int.Parse(MVTurnoInformacion.DTotal.ToString());
                    }

                    TotalSuministros = MVTurnoInformacion.DTotalSucursal;
                    TotalEnvio = MVTurnoInformacion.DTotalEnvio;

                    if (MVTurno.VerificaTurnoCerrado(AppInstance.Session_.UidTurnoRepartidor.ToString()))
                    {
                        if (MEfectivoEnCaja == 0)
                        {
                            MEfectivoEnCaja = MVTurnoInformacion.DEfectivoActual;
                            MEfectivoEnCaja = MEfectivoEnCaja + MVTurno.VerFondoRepartidor(AppInstance.Session_.UidUsuario.ToString());
                        }
                    }
                    else
                    {
                        Total = TotalSuministros + TotalEnvio;
                        DFondo = MVTurno.DFondoRepartidor;
                    }

                    Texto = "Liquidando turno";
                    ColorProp = Color.Gray;
                }
                else
                {
                    DPropina = 0.0m;
                    UidTurnoRepartidor = Guid.Empty;
                    LngFolio = string.Empty;
                    DtmHoraInicio = string.Empty;
                    CantidadDeOrdenes = 0;
                    TotalSuministros = 0.0m;
                    MEfectivoEnCaja = 0;
                    TotalEnvio = 0.0m;
                    Total = 0.0m;
                    AppInstance.Session_.UidTurnoRepartidor = UidTurnoRepartidor;
                    Texto = "Abrir turno";
                    ColorProp = Color.Green;
                }
            }
        }


        private async void CambiaEstatusTurnoRepartidor(string Estatus, string UidTRepartidor)
        {
            using (var _webApi = new HttpClient())
            {
                switch (Estatus)
                {
                    case "Liquidando":
                        url = "" + settings.Sitio + "api/Turno/GetAgregaEstatusTurnoRepartidor?UidTurnoRepartidor=" + UidTRepartidor + "&UidEstatusTurno=AE28F243-AA0D-43BD-BF10-124256B75B00";
                        await _webApi.GetStringAsync(url);
                        break;
                }
            }
        }

        private async Task<decimal> ObtenMontoRepartidor(Guid uidUsuario)
        {
            var obj = "";
            using (var _ApiRequest = new HttpClient())
            {
                string uril = "" + settings.Sitio + "api/Turno/GetConsultaCantidadMaximaAPortar?UidRepartidor=" + uidUsuario + "";
                var datos = await _ApiRequest.GetStringAsync(uril);
                obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
            }

            return decimal.Parse(obj);
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
