using System;
using System.Collections.Generic;
using System.Text;
using Repartidores_GoDeliverix.VM;
namespace Repartidores_GoDeliverix.Resources
{
    class instancelocation
    {
        public MainViewModel VMMain { get; set; }
        public instancelocation()
        {
            this.VMMain = new MainViewModel();
        }
    }
}
