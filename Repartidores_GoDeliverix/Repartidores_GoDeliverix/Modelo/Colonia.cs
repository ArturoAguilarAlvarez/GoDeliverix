using System;
using System.Collections.Generic;
using System.Text;

namespace Repartidores_GoDeliverix.Modelo
{
    public class Colonia:ControlsController
    {
        private Guid _UidColonia;
        public Guid UidColonia
        {
            get { return _UidColonia; }
            set { SetValue(ref _UidColonia, value); }
        }
        private string _NombreColonia;
        public string NombreColonia
        {
            get { return _NombreColonia; }
            set { SetValue(ref _NombreColonia, value); }
        }
    }
}
