using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace VistaDelModelo
{
    public class VMTurno
    {
        #region Propiedades
        Turno oTurno;

        private long _LngFolio;

        public long LngFolio
        {
            get { return _LngFolio; }
            set { _LngFolio = value; }
        }

        private DateTime _HoraIncio;

        public DateTime DtmHoraInicio
        {
            get { return _HoraIncio; }
            set { _HoraIncio = value; }
        }

        private DateTime _HoraFin;

        public DateTime DtmHoraFin
        {
            get { return _HoraFin; }
            set { _HoraFin = value; }
        }

        private Guid _UidTurno;

        public Guid UidTurno
        {
            get { return _UidTurno; }
            set { _UidTurno = value; }
        }

        private Guid _UidUsuario;

        public Guid UidUsuario
        {
            get { return _UidUsuario; }
            set { _UidUsuario = value; }
        }
        //Propiedades para el control del turno
        private int _DTotal;


        public int DTotal
        {
            get { return _DTotal; }
            set { _DTotal = value; }
        }

        private decimal _DTotalSucursal;

        public decimal DTotalSucursal
        {
            get { return _DTotalSucursal; }
            set { _DTotalSucursal = value; }
        }

        private decimal _DTotalEnvio;

        public decimal DTotalEnvio
        {
            get { return _DTotalEnvio; }
            set { _DTotalEnvio = value; }
        }
        private long _lngCantidad;

        public long CantiadDeOrdenes
        {
            get { return _lngCantidad; }
            set { _lngCantidad = value; }
        }


        #endregion


        #region Metodos

        public void EstatusTurno(Guid UidUsuario, Guid UidTurno)
        {
            oTurno = new Turno() { UidTurno = UidTurno, UidUsuario = UidUsuario };
            oTurno.CreaOActualiza();


        }
        /// <summary>
        /// Consulta el ultimo turno del repartidor.
        /// </summary>
        /// <returns></returns>
        public void ConsultaUltimoTurno(Guid UidUsuario)
        {
            try
            {
                oTurno = new Turno() { UidUsuario = UidUsuario };
                foreach (DataRow item in oTurno.VerificaEstatusDeTurno().Rows)
                {
                    UidTurno = new Guid(item["UidTurnoRepartidor"].ToString());
                    LngFolio = long.Parse(item["LngFolio"].ToString());
                    this.UidUsuario = new Guid(item["UidUsuario"].ToString());
                    DtmHoraInicio = DateTime.Parse(item["DtmHoraInicio"].ToString());
                    if (!string.IsNullOrEmpty(item["DtmHoraFin"].ToString()))
                    {
                        DtmHoraFin = DateTime.Parse(item["DtmHoraFin"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InformacionDeOrdenesPorTuno(Guid UidTurno)
        {
            try
            {
                oTurno = new Turno() { UidUsuario = UidUsuario };
                foreach (DataRow item in oTurno.InformacionDeOrdenes(UidTurno).Rows)
                {
                    if (!string.IsNullOrEmpty(item["TotalOrdenes"].ToString()))
                    {
                        DTotal = int.Parse(item["TotalOrdenes"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["totalEnvio"].ToString()))
                    {
                        DTotalEnvio = decimal.Parse(item["totalEnvio"].ToString());
                    }
                    if (!string.IsNullOrEmpty(item["totalSucursal"].ToString()))
                    {
                        DTotalSucursal = decimal.Parse(item["totalSucursal"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion





    }
}
