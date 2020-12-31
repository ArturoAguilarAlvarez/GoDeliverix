using AllSuministradora.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VistaDelModelo;

namespace AllSuministradora.model
{
    public class Turno : NotifyBase
    {
        #region Propiedades
        private Guid _UidTurno;

        public Guid UidTurno
        {
            get { return _UidTurno; }
            set { _UidTurno = value; OnpropertyChanged("UidTurno"); }
        }
        private string _StrHoraInicio;

        public string StrHoraInicio
        {
            get { return _StrHoraInicio; }
            set { _StrHoraInicio = value; OnpropertyChanged("StrHoraInicio"); }
        }

        private string _StrHoraFin;

        public string StrHoraFin
        {
            get { return _StrHoraFin; }
            set { _StrHoraFin = value; OnpropertyChanged("StrHoraFin"); }
        }
        private int _intCantidadDeOrdenes;

        public int IntCantidadDeOrdenes
        {
            get { return _intCantidadDeOrdenes; }
            set { _intCantidadDeOrdenes = value; OnpropertyChanged("IntCantidadDeOrdenes"); }
        }
        private decimal _dcmTotal;

        public decimal DcmTotal
        {
            get { return _dcmTotal; }
            set { _dcmTotal = value; OnpropertyChanged("DcmTotal"); }
        }
        private long _lngFolio;

        public long LngFolio
        {
            get { return _lngFolio; }
            set { _lngFolio = value; OnpropertyChanged("LngFolio"); }
        }

        private Guid _UidUsuario;

        public Guid UidUsuario
        {
            get { return _UidUsuario; }
            set { _UidUsuario = value; OnpropertyChanged("UidUsuario"); }
        }
        private Guid _UidLicencia;

        public Guid UidLicencia
        {
            get { return _UidLicencia; }
            set { _UidLicencia = value; OnpropertyChanged("UidLicencia"); }
        }

        #endregion

        #region Commandos
        private ICommand _cmdControlDeEstatusTurno;

        public ICommand CMDControlDeEstatus
        {
            get { return _cmdControlDeEstatusTurno; }
            set { _cmdControlDeEstatusTurno = value; }
        }

        #endregion
        #region Constructor
        public Turno()
        {
            CMDControlDeEstatus = new CommandBase(param => ControlTurno());
        }
        #endregion

        #region Metodos

        protected void ControlTurno()
        {
            ControlDeTurno();
        }
        public void ControlDeTurno()
        {
            if (UidUsuario != null && UidUsuario != Guid.Empty && UidLicencia != null && UidLicencia != Guid.Empty)
            {
                VMTurno oturno = new VMTurno();
                oturno.TurnoSuministradora(uid: UidUsuario, licencia: UidLicencia.ToString());

            }
        }

        internal bool EstatusTurno(Guid UidLicencia, Guid UidSucursal)
        {
            bool respuesta = false;
            VMTurno oturno = new VMTurno();
            var instance = ControlGeneral.GetInstance();

            respuesta = oturno.TurnoAbierto(UidSucursal, instance.Principal.UidUsuario);
            if (respuesta)
            {
                oturno.ConsultarUltimoTurnoSuministradora(UidLicencia.ToString());
                UidTurno = oturno.UidTurno;
                StrHoraInicio = oturno.DtmHoraInicio.ToString();
                LngFolio = oturno.LngFolio;
            }

            return respuesta;
        }
        #endregion
    }
}
