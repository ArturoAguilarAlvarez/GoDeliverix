using System;
using System.Collections.Generic;
using System.Text;

namespace Repartidores_GoDeliverix.Helpers
{
    public class ResponseHelper
    {
        public string Message { set; get; }
        public bool Status { set; get; }
        public object Data { set; get; }
    }
}
