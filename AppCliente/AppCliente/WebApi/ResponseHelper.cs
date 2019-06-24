using System;
using System.Collections.Generic;
using System.Text;

namespace AppCliente.WebApi
{
    public  class ResponseHelper
    {
        public string Message { set; get; }
        public bool Status { set; get; }
        public object Data { set; get; }
    }
}
