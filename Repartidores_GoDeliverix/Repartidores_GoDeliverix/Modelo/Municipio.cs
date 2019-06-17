using System;
using System.Collections.Generic;
using System.Text;

namespace Repartidores_GoDeliverix.Modelo
{
    public class Municipio: ControlsController
    {
        private Guid _UidMunicipio;
        public Guid UidMunicipio
        {
            get { return _UidMunicipio; }
            set { SetValue(ref _UidMunicipio, value); }
        }
        private string _NombreMunicipio;
        public string NombreMunicipio
        {
            get { return _NombreMunicipio; }
            set { SetValue(ref _NombreMunicipio, value); }
        }
    }
}
