using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Modelo.Usuario
{
    public class Perfiles
    {
        #region Propiedades
        private Guid _Uiderfil;

        public Guid ID
        {
            get { return _Uiderfil; }
            set { _Uiderfil = value; }
        }

        private string _nombrePerfil;
        private string perfil;

        public string NOMBRE
        {
            get { return _nombrePerfil; }
            set { _nombrePerfil = value; }
        }


        #endregion

        #region Constructores
        public Perfiles()
        {

        }
        public Perfiles(Guid id)
        {
            ID = id;
        }
        public Perfiles(Guid id, string nombre)
        {
            ID = id;
            NOMBRE = nombre;
        }

        public Perfiles(string perfil)
        {
            this.perfil = perfil;
        }

        #endregion
    }
}
