using System;
using System.Collections.Generic;
using System.Text;

namespace Repartidores_GoDeliverix.Modelo
{
    public class Productos : ControlsController
    {
        private string _StrNombreProducto;
        public string StrNombreProducto
        {
            get { return _StrNombreProducto; }
            set { SetValue(ref _StrNombreProducto, value); }
        }
        private int _intCantidad;

        public int IntCantidad
        {
            get { return _intCantidad; }
            set { SetValue(ref _intCantidad, value); }
        }
        private decimal _MTotal;

        public decimal MTotal
        {
            get { return _MTotal; }
            set { SetValue(ref _MTotal, value); }
        }

        private decimal _MSubTotal;

        public decimal MSubTotal
        {
            get { return _MSubTotal; }
            set { SetValue(ref _MSubTotal, value); }
        }
    }
}
