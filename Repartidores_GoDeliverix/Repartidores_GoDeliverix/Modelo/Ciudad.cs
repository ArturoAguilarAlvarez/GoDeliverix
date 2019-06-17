using System;
using System.Collections.Generic;
using System.Text;

namespace Repartidores_GoDeliverix.Modelo
{
    public class Ciudad: ControlsController
    {
        private Guid _UidCiudad;
        public Guid UidCiudad
        {
            get { return _UidCiudad; }
            set { SetValue(ref _UidCiudad, value); }
        }
        private string _NombreCiudad;
        public string NombreCiudad
        {
            get { return _NombreCiudad; }
            set { SetValue(ref _NombreCiudad, value); }
        }
    }
}
