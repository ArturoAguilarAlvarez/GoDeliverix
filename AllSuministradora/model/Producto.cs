using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllSuministradora.model
{
    public class Producto : NotifyBase
    {
        #region Propiedades
        private string _Nombre;

        public string StrNombre
        {
            get { return _Nombre; }
            set { _Nombre = value; OnpropertyChanged("StrNombre"); }
        }
        private int _IntCantidad;

        public int IntCantidad
        {
            get { return _IntCantidad; }
            set { _IntCantidad = value; OnpropertyChanged("IntCantidad"); }
        }
        private string _MTotalSucursal;

        public string MTotalSucursal
        {
            get { return _MTotalSucursal; }
            set { _MTotalSucursal = value; OnpropertyChanged("MTotalSucursal"); }
        }


        #endregion
    }
}
