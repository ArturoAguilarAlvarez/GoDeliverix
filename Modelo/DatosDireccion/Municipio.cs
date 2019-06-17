using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Modelo.DatosDireccion
{
    public class Municipio
    {
        #region Propiedades
        private Guid _idMunicipio;
        public Guid IDMUNICIPIO
        {
            get { return _idMunicipio; }
            set { _idMunicipio = value; }
        }
        private string _strNombre;
        public string NOMBRE
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        public Estado ESTADO;
        #endregion
        #region Constructores
        public Municipio()
        {

        }
        public Municipio(Guid id)
        {
            IDMUNICIPIO = id;
        }
        public Municipio(Guid id, string nombre, Guid idestado)
        {
            IDMUNICIPIO = id;
            NOMBRE = nombre;
            ESTADO = new Estado(idestado);
        }
        #endregion
    }
}
