using System;
using System.Collections.Generic;
using DBControl;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class Suministros : Empresa
    {
        #region Propiedades para armar los datos en el gridview


        private string status;

        public string STATUS
        {
            get { return status; }
            set { status = value; }
        }
        private string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        #endregion
        #region Constructores
        public Suministros()
        {

        }

        #endregion
        #region Metodos
        public override bool GUARDAREMPRESA(Empresa EMP)
        {
            return base.GUARDAREMPRESA(EMP);
        }
        public override bool ACTUALIZAREMPRESA(Empresa EMP)
        {
            return base.ACTUALIZAREMPRESA(EMP);
        }


        #endregion
    }
}
