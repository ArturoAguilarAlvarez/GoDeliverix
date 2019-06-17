using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Modelo.DatosDireccion
{
    public class Ciudad
    {
        #region Propiedades
        private Guid _uidCiudad;
        public Guid ID
        {
            get { return _uidCiudad; }
            set { _uidCiudad = value; }
        }

        private string _strNombre;
        public string NOMBRE
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        #endregion

        #region constructores
        public Ciudad()
        {

        }

        public Ciudad(Guid id, string nombre)
        {
            ID = id;
            NOMBRE = nombre;
        }
        public Ciudad(Guid id)
        {
            ID = id;
        }
        #endregion
    }
}
