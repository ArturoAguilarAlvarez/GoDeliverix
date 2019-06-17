using System;

namespace Modelo.DatosDireccion
{
    public class Estado
    {
        #region Propiedades
        private Guid _idEstado;
        public Guid IDESTADO
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        private string _strNombre;
        public string NOMBRE
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        #endregion
        #region Constructores
        public Estado()
        {

        }
        public Estado(Guid id)
        {
            IDESTADO = id;
        }
        public Estado(Guid id, string nombre)
        {
            IDESTADO = id;
            NOMBRE = nombre;
        }
        #endregion
    }
}
