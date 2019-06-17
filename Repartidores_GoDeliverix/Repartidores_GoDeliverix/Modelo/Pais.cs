using System;
using System.Collections.Generic;
using System.Text;

namespace Repartidores_GoDeliverix.Modelo
{
    public class Pais:ControlsController
    {
        private Guid _UidPais;
        public Guid UidPais
        {
            get { return _UidPais; }
            set { SetValue(ref _UidPais, value); }
        }
        private string _NombrePais;
        public string NombrePais
        {
            get { return _NombrePais; }
            set { SetValue(ref _NombrePais, value); }
        }
    }
}
