using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VistaDelModelo;

namespace GoDeliverix_TodasLasSucursales.VM
{
    public class VMSucursalesLocal : NotifyBase
    {

        public ObservableCollection<TurnoItem> ListaDeSucursales { get; set; }
        private TurnoItem _oItem;

        public TurnoItem SeleccionadoItem
        {
            get { return _oItem; }
            set { _oItem = value; OnPropertyChanged("_oItem"); }
        }

        VMSucursales MVSucursales;
        VMLicenciaLocal oLicenciaLocal;
        VMEmpresas MVEmpresa;


        public VMSucursalesLocal()
        {
            ObtenSucursales();
        }

        public void ObtenSucursales()
        {
            MVSucursales = new VMSucursales();
            ListaDeSucursales = new ObservableCollection<TurnoItem>();
            oLicenciaLocal = new VMLicenciaLocal();
            MVEmpresa = new VMEmpresas();
            foreach (DataRow item in oLicenciaLocal.obtenerLicencias().Rows)
            {
                MVSucursales.BuscarSucursales(UidSucursal: MVSucursales.ObtenSucursalDeLicencia(item["UidLicencia"].ToString()));
                MVEmpresa.BuscarEmpresas(UidEmpresa: MVSucursales.UidEmpresa);
                TurnoItem control = new TurnoItem()
                {
                    Licencia = new Guid(item["UidLicencia"].ToString()),
                    UidSucursal = MVSucursales.ID,
                    NombreEmpresa = MVEmpresa.NOMBRECOMERCIAL,
                    NombreSucursal = MVSucursales.IDENTIFICADOR,
                    HorarioSucursal = MVSucursales.HORAAPARTURA + " - " + MVSucursales.HORACIERRE
                };
                if (ListaDeSucursales.Where(x => x.UidSucursal == MVSucursales.ID).ToList().Count == 0)
                {
                    ListaDeSucursales.Add(control);
                }
            }
        }
    }
}
