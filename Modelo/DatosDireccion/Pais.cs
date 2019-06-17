using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Modelo.DatosDireccion
{
    public class Pais
    {
        #region Propiedades
        private Guid _uidPais;
        public Guid ID
        {
            get { return _uidPais; }
            set { _uidPais = value; }
        }

        private string _nombre;
        public string NOMBRE
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        #endregion

        #region Constructor
        public Pais()
        {

        }
        public Pais(Guid id)
        {
            ID = id;
        }
        public Pais(Guid id, string nombre)
        {
            ID = id;
            NOMBRE = nombre;
        }
        #endregion

    }
}
