using System;
using System.Collections.Generic;
using System.Text;

namespace Repartidores_GoDeliverix.Modelo
{
    public class Session :ControlsController
    {
        public Guid UidUsuario { get; set; }
        private string _Nombre;

        public string Nombre
        {
            get { return _Nombre; }
            set { SetValue(ref _Nombre, value); }
        }

        public string Sucursal { get; set; }

    }
}
