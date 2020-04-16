using AllSuministradora.model;
using AllSuministradora.Recursos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VistaDelModelo;
using System.Windows.Media;

namespace AllSuministradora.VistasDelModelo
{
    public class VMSucursalesLocal : NotifyBase
    {
        #region Propiedades
        VMSucursales MVSucursales;
        VMEmpresas MVEmpresa;
        Licencia oLicencia;

        private ObservableCollection<SucursalItem> _ListaSucursales;

        public ObservableCollection<SucursalItem> ListaDeSucursales
        {
            get { return _ListaSucursales; }
            set { _ListaSucursales = value; OnpropertyChanged("ListaDeSucursales"); }
        }

        #endregion
        #region Comandos


        #endregion

        #region Constructor
        public VMSucursalesLocal()
        {
            ObtenSucursales();
        }
        #endregion

        #region Metodos
        public void ObtenSucursales()
        {
            MVSucursales = new VMSucursales();
            ListaDeSucursales = new ObservableCollection<SucursalItem>();
            oLicencia = new Licencia();
            MVEmpresa = new VMEmpresas();
            var instance = ControlGeneral.GetInstance();
            foreach (DataRow item in oLicencia.obtenerLicencias().Rows)
            {
                MVSucursales.BuscarSucursales(UidSucursal: MVSucursales.ObtenSucursalDeLicencia(item["UidLicencia"].ToString()));
                MVEmpresa.BuscarEmpresas(UidEmpresa: MVSucursales.UidEmpresa);
                Turno turno = new Turno();
                var oturno = new Turno();
                VMTurno MVTUrno = new VMTurno();
                var estatusturno = "";
                Brush ocolor = null;
                oturno.UidUsuario = new Guid(instance.Principal.UidUsuario);
                if (turno.EstatusTurno(new Guid(item["UidLicencia"].ToString()), UidSucursal: MVSucursales.ID))
                {
                    MVTUrno.ConsultarUltimoTurnoSuministradora(item["UidLicencia"].ToString());
                    turno = new Turno() { UidTurno = MVTUrno.UidTurno, StrHoraInicio = MVTUrno.DtmHoraInicio.ToString() };
                    oturno = turno;
                    estatusturno = "Terminar";
                    ocolor = Brushes.Red;
                }
                else
                {
                    oturno = null;
                    estatusturno = "Comenzar";
                    ocolor = Brushes.Green;
                }
                SucursalItem control = new SucursalItem()
                {
                    Licencia = new Guid(item["UidLicencia"].ToString()),
                    UidSucursal = MVSucursales.ID,
                    NombreEmpresa = MVEmpresa.NOMBRECOMERCIAL,
                    NombreSucursal = MVSucursales.IDENTIFICADOR,
                    StrEstatusTurno = estatusturno,
                    HorarioSucursal = MVSucursales.HORAAPARTURA + " - " + MVSucursales.HORACIERRE,
                    oTurno = oturno,
                    CcolorTurno = ocolor
                };
                if (ListaDeSucursales.Where(x => x.UidSucursal == MVSucursales.ID).ToList().Count == 0)
                {
                    ListaDeSucursales.Add(control);
                }
            }
        }

        #endregion
    }
}
