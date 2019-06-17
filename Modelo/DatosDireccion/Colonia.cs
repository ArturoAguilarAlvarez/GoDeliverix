using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Modelo.DatosDireccion
{
    public class Colonia
    {
        #region Propiedades
        private Guid UidColonia;
        public Guid UID
        {
            get { return UidColonia; }
            set { UidColonia = value; }
        }
        private string strNombre;
        public string NOMBRE
        {
            get { return strNombre; }
            set { strNombre = value; }
        }
        #endregion
        #region Contructores
        public Colonia()
        {

        }
        public Colonia(Guid ID, string nombre)
        {
            NOMBRE = nombre;
            UID = ID;
        }
        public Colonia(Guid ID)
        {
            UID = ID;
        }
        #endregion
    }
}
