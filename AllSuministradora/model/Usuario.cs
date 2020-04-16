using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllSuministradora.model
{
    public class Usuario : NotifyBase
    {
        private int _Numero;

        public int Test
        {
            get { return _Numero; }
            set { _Numero = value; OnpropertyChanged("Test"); }
        }

    }
}
