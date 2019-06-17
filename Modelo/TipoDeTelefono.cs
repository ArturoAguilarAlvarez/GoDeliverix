using System;

namespace Modelo
{
    public class TipoDeTelefono
    {
        #region Propiedades
        private Guid _uidTipo;

        public Guid ID
        {
            get { return _uidTipo; }
            set { _uidTipo = value; }
        }
        private string _nombre;

        public string NOMBRE
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        #endregion

        #region Constructores

        public TipoDeTelefono()
        {

        }
        public TipoDeTelefono(Guid id)
        {
            ID = id;
        }
        public TipoDeTelefono(Guid id, string nombre)
        {
            ID = id;
            NOMBRE = nombre;
        }
        #endregion
    }
}
