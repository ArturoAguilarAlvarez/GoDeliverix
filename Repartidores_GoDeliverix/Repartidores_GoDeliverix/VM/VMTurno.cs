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
        HttpClient _WebApiGoDeliverix = new HttpClient();
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
        public ICommand OrdenesActuales { get { return new RelayCommand(VerOrdenesActuales); } }
        public ICommand Liquidar { get { return new RelayCommand(ModoLiquidar); } }

        public ICommand Historico { get { return new RelayCommand(HistoricoTurno); } }
        public ICommand HistoricoOrdenTurno { get { return new RelayCommand(OrdenesHistoricoTurno); } }

        private void ModoLiquidar()
        {
            //GenerateMessage("Aviso del sistema", "Comenzando liquidacion", "OK");
            //var AppInstance = MainViewModel.GetInstance();
            //if (TextoLiquidar == "Liquidando ando")
            //{
            //    TextoLiquidar = "Liquidar";
            //    //Cambia el esstatus a liquidado
            //    string url = "http://www.godeliverix.net/api/Turno/GetAgregaEstatusTurnoRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "&UidEstatusTurno=38FA16DF-4727-41FD-A03E-E2E43FA78F3F";
            //    await _WebApiGoDeliverix.GetStringAsync(url);
            //}
            //else
            //{
            //    TextoLiquidar = "Liquidando ando";
            //    //Cambia el esstatus a liquidando
            //    string url = "http://www.godeliverix.net/api/Turno/GetAgregaEstatusTurnoRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "&UidEstatusTurno=AE28F243-AA0D-43BD-BF10-124256B75B00";
            //    await _WebApiGoDeliverix.GetStringAsync(url);
            //}
        }

        private async void VerOrdenesActuales()
        {
            var AppInstance = MainViewModel.GetInstance();
            if (AppInstance.Session_.UidUsuario != Guid.Empty)
            {
                AppInstance.MVTurnoOrden = new VMTurnoOrden(UidTurnoRepartidor.ToString());
                await Application.Current.MainPage.Navigation.PushAsync(new Historico_DetalleDia());
            }
            else
            {
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
            else
            {
            }
        }

        private async void HistoricoTurno()
        {
            var AppInstance = MainViewModel.GetInstance();
            if (AppInstance.Session_.UidUsuario != Guid.Empty)
            {
                url = "https://www.godeliverix.net/api/Turno/GetConsultaHisstorico?UidUsuario=" + AppInstance.Session_.UidUsuario + "";
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
            try
            {
                var AppInstance = MainViewModel.GetInstance();
                //_WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");
                if (UidTurnoRepartidor == Guid.Empty)
                {
                    var obj = string.Empty;

                    using (var _webApi = new HttpClient())
                    {
                        url = "https://www.godeliverix.net/api/Turno/GetConsultaEstatusUltimoTurnoRepartidor?UidUsuario=" + AppInstance.Session_.UidUsuario + "";
                        var datos = await _WebApiGoDeliverix.GetStringAsync(url);
                        obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                    }

                    Guid UidEstatusTurno = new Guid(obj);
                    //Validacion por estatus de turno liquidado o cerrado
                    if (UidEstatusTurno == new Guid("38FA16DF-4727-41FD-A03E-E2E43FA78F3F") || UidEstatusTurno == new Guid("3BE9EF83-4A39-4A60-9FA9-7F50AD60CA3A"))
                    {
                        UidTurnoRepartidor = Guid.NewGuid();
                        AppInstance.Session_.UidTurnoRepartidor = UidTurnoRepartidor;
                        //Cambia el estatus a abierto
                        using (var _webApi = new HttpClient())
                        {
                            url = "https://www.godeliverix.net/api/Turno/GetAgregaEstatusTurnoRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "&UidEstatusTurno=81494F49-F416-4431-99F4-E0AA4CF7E9F6";
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
                    //Cambia el estatus a cerrado
                    url = "https://www.godeliverix.net/api/Turno/GetAgregaEstatusTurnoRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "&UidEstatusTurno=3BE9EF83-4A39-4A60-9FA9-7F50AD60CA3A";
                    await _WebApiGoDeliverix.GetStringAsync(url);

                    if (Total > 0)
                    {
                        //Cambia el estatus a liquidando
                        url = "https://www.godeliverix.net/api/Turno/GetAgregaEstatusTurnoRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "&UidEstatusTurno=AE28F243-AA0D-43BD-BF10-124256B75B00";
                        await _WebApiGoDeliverix.GetStringAsync(url);
                    }
                }
                url = "https://www.godeliverix.net/api/Turno/GetTurnoRepartidor?UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidTurno=" + UidTurnoRepartidor + "";
                await _WebApiGoDeliverix.GetStringAsync(url);
                CargarTurno();
            }
            catch (Exception)
            {
                GenerateMessage("Aviso del sistema", "No se puede establecer conexion", "OK");
            }

        }

        public VMTurno()
        {
            tiempo = new Timer();
            tiempo.Interval = 6000;
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
                    string url = "https://www.godeliverix.net/api/Turno/GetConsultaEstatusTurnoRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "";
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
                }

            }
        }

        public async void CargarTurno()
        {
            MVTurno = new VistaDelModelo.VMTurno();
            var AppInstance = MainViewModel.GetInstance();

            string consulta2 = "https://www.godeliverix.net/api/Turno/GetInformacionDeOrdenesPorTuno?UidTurno=" + AppInstance.Session_.UidTurnoRepartidor + "";

            using (var _webApi = new HttpClient())
            {
                url = "https://www.godeliverix.net/api/Turno/GetConsultaUltimoTurno?UidUsuario=" + AppInstance.Session_.UidUsuario + "";
                var datos = await _webApi.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                MVTurno = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
            }


            UidTurnoRepartidor = MVTurno.UidTurno;
            LngFolio = MVTurno.LngFolio.ToString();
            DtmHoraInicio = MVTurno.DtmHoraInicio.ToString();
            AppInstance.Session_.UidTurnoRepartidor = MVTurno.UidTurno;
            if (MVTurno.DtmHoraFin == DateTime.Parse("01/01/0001 12:00:00 a. m.") && MVTurno.DtmHoraInicio != DateTime.Parse("01/01/0001 12:00:00 a. m."))
            {
                Texto = "Cerrar turno";
                ColorProp = Color.Red;
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

                //Obtiene el monto maximo a cargar por repartidor
                decimal montoMaximo = await ObtenMontoRepartidor(AppInstance.Session_.UidUsuario);

                if (montoMaximo <= Total)
                {
                    //Cambia el estatus a liquidando
                    CambiaEstatusTurnoRepartidor("Liquidando", AppInstance.Session_.UidTurnoRepartidor.ToString());
                    TextoLiquidar = "Liquidando";
                }
                else
                {
                    TextoLiquidar = "Liquidado";
                }
            }
            else
            {
                Guid UidEstatusTurno = new Guid();
                using (var _webApi = new HttpClient())
                {
                    url = "https://www.godeliverix.net/api/Turno/GetConsultaEstatusUltimoTurnoRepartidor?UidUsuario=" + AppInstance.Session_.UidUsuario + "";
                    string datos = await _webApi.GetStringAsync(url);
                    string obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                    UidEstatusTurno = new Guid(obj);
                }



                if (UidEstatusTurno == new Guid("AE28F243-AA0D-43BD-BF10-124256B75B00"))
                {
                    using (var _webApi = new HttpClient())
                    {
                        url = "https://www.godeliverix.net/api/Turno/GetInformacionDeOrdenesPorTuno?UidTurno=" + AppInstance.Session_.UidTurnoRepartidor + "";
                        string datos = await _webApi.GetStringAsync(url);
                        string obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                        MVTurnoInformacion = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                    }

                    CantidadDeOrdenes = int.Parse(MVTurnoInformacion.DTotal.ToString());
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


        private async void CambiaEstatusTurnoRepartidor(string Estatus, string UidTRepartidor)
        {
            using (var _webApi = new HttpClient())
            {
                switch (Estatus)
                {
                    case "Liquidando":
                        url = "https://www.godeliverix.net/api/Turno/GetAgregaEstatusTurnoRepartidor?UidTurnoRepartidor=" + UidTRepartidor + "&UidEstatusTurno=AE28F243-AA0D-43BD-BF10-124256B75B00";
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
                string uril = "https://www.godeliverix.net/api/Turno/GetConsultaCantidadMaximaAPortar?UidRepartidor=" + uidUsuario + "";
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
