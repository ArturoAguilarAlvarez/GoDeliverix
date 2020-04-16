using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllSuministradora.model
{
    public class Mensaje : NotifyBase
    {
        #region Propiedades
        private Guid _UidMensaje;

        public Guid UidMensaje
        {
            get { return _UidMensaje; }
            set { _UidMensaje = value; OnpropertyChanged("UidMensaje"); }
        }
        private string _strMensaje;

        public string StrMensaje
        {
            get { return _strMensaje; }
            set { _strMensaje = value; OnpropertyChanged("StrMensaje"); }
        }

        #endregion
        #region Constructor
        public Mensaje()
        {

        }
        #endregion
    }
}
