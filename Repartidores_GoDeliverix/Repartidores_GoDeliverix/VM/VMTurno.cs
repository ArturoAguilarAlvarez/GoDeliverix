using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Repartidores_GoDeliverix.VM
{
    public class VMTurno : ControlsController
    {
        string url = "";
        HttpClient _WebApiGoDeliverix = new HttpClient();

        private string _UidTurnoRepartidor;
        public string UidTurnoRepartidor
        {
            get { return _UidTurnoRepartidor; }
            set { SetValue(ref _UidTurnoRepartidor, value); }
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


        public ICommand IsReloading { get { return new RelayCommand(ActivarTurno); } }

        private void ActivarTurno()
        {
        }

        public VMTurno()
        {

        }
    }
}
