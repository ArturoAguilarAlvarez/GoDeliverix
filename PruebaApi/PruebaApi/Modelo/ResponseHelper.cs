using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaApi.Modelo
{
    class ResponseHelper
    {
        public string Message { set; get; }
        public bool Status { set; get; }
        public object Data { set; get; }
    }
}
