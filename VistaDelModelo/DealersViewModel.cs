using DBControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    /// <summary>
    /// Vista del modelo del repartidor (version 2)
    /// </summary>
    public class DealersViewModel
    {
        private DealerDataAccess DealerDb { get; }

        public DealersViewModel()
        {
            this.DealerDb = new DealerDataAccess();
        }
    }
}
