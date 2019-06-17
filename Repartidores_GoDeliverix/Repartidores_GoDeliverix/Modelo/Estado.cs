using System;
using System.Collections.Generic;
using System.Text;

namespace Repartidores_GoDeliverix.Modelo
{
    public class Estado: ControlsController
    {
        private Guid _UidEstado;
        public Guid UidEstado
        {
            get { return _UidEstado; }
            set { SetValue(ref _UidEstado, value); }
        }
        private string _NombreEstado;
        public string NombreEstado
        {
            get { return _NombreEstado; }
            set { SetValue(ref _NombreEstado, value); }
        }
    }
}
