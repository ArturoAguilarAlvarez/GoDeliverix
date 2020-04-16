using AllSuministradora.VistasDelModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllSuministradora.Recursos
{
    public class ControlGeneral : NotifyBase
    {
        private VMPantallaPrincipal _vmprincipal;

        public VMPantallaPrincipal Principal
        {
            get { return _vmprincipal; }
            set { _vmprincipal = value; OnpropertyChanged("Principal"); }
        }
        private VMUsuario vMUsuario;

        public VMUsuario Usuario
        {
            get { return vMUsuario; }
            set { vMUsuario = value; OnpropertyChanged("Usuario"); }
        }
        private VMLicencias _MvLicencia;

        public VMLicencias VMLicencia
        {
            get { return _MvLicencia; }
            set { _MvLicencia = value; OnpropertyChanged("VMLicencia"); }
        }
        private VMSucursalesLocal vMSucursalesLocal;

        public VMSucursalesLocal VMSucursalesLocal
        {
            get { return vMSucursalesLocal; }
            set { vMSucursalesLocal = value; OnpropertyChanged("VMSucursalesLocal"); }
        }

        private VMOrdenes _MVOrdenes;

        public VMOrdenes MVOrdenes
        {
            get { return _MVOrdenes; }
            set { _MVOrdenes = value; OnpropertyChanged("MVOrdenes"); }
        }


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
