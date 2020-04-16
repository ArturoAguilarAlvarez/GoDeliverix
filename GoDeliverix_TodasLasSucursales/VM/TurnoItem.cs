using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GoDeliverix_TodasLasSucursales.VM
{
    public class TurnoItem : NotifyBase
    {
        private Guid _UidSucursal;

        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; OnPropertyChanged("_UidSucursal"); }
        }

        private string _NombreEmpresa;

        public string NombreEmpresa
        {
            get { return _NombreEmpresa; }
            set { _NombreEmpresa = value; OnPropertyChanged("_NombreEmpresa"); }
        }
        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; OnPropertyChanged("_NombreSucursal"); }
        }
        private string _HorarioSucursal;

        public string HorarioSucursal
        {
            get { return _HorarioSucursal; }
            set { _HorarioSucursal = value; OnPropertyChanged("_HorarioSucursal"); }
        }
        private Guid _UidLicencia;

        public Guid Licencia
        {
            get { return _UidLicencia; }
            set { _UidLicencia = value; }
        }



        #region Comandos
        private ICommand _EliminaLicencia;

        public ICommand EliminaLicencias
        {
            get { return _EliminaLicencia; }
            set { _EliminaLicencia = value; OnPropertyChanged("_EliminaLicencia"); }
        }

        #endregion


        #region Vistas del modelo
        VMLicenciaLocal olicencialocal;
        #endregion
        public TurnoItem()
        {
            EliminaLicencias = new CommandBase(param => this.EliminarLicencia());
        }
        protected void EliminarLicencia()
        {
            var appinstance = ControlGeneral.GetInstance();
            var obj = appinstance.MVSucursalesLocal.ListaDeSucursales.Where(x => x.UidSucursal == UidSucursal).ToList();

            olicencialocal = new VMLicenciaLocal();
            olicencialocal.EliminarLicencia(obj[0].Licencia);
            appinstance.MVSucursalesLocal.ObtenSucursales();
        }
    }
}
