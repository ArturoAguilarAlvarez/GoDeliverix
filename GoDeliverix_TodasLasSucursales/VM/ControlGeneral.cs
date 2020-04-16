using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDeliverix_TodasLasSucursales.VM
{
    public class ControlGeneral : NotifyBase
    {
        #region Propiedades
        private VMSucursalesLocal _MVSucursalesLocal;

        public VMSucursalesLocal MVSucursalesLocal
        {
            get { return _MVSucursalesLocal; }
            set { _MVSucursalesLocal = value; OnPropertyChanged("_MVSucursalesLocal"); }
        }

        private VMControlTurno _MVControlTurno;

        public VMControlTurno MVControlTurno
        {
            get { return _MVControlTurno; }
            set { _MVControlTurno = value; OnPropertyChanged("_MVControlTurno"); }
        }
        private VMConfiguracion _MVConfiguracion;

        public VMConfiguracion MVConfiguracion
        {
            get { return _MVConfiguracion; }
            set { _MVConfiguracion = value; OnPropertyChanged("_MVConfiguracion"); }
        }

        #endregion
        public ControlGeneral()
        {
            Instance = this;
        }
        #region Singleton
        public static ControlGeneral Instance;
        public static ControlGeneral GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ControlGeneral();
            }
            return Instance;
        }
        #endregion

    }
}
