using DBControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class VMConfiguracionCliente
    {
        #region Propiedade sstaticas
        static Conexion oConexion = new Conexion();
        static DBConfiguracionClientemovil oConfiguracion = new DBConfiguracionClientemovil();

        #endregion
        #region Propiedades
        private Guid _Uid;

        public Guid Uid
        {
            get { return _Uid; }
            set { _Uid = value; }
        }
        private string _Nombre;

        public string StrNombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        private string _Valor;

        public string StrValor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
        #endregion
        public VMConfiguracionCliente()
        {

        }
        #region Implementacion
        public void ObtenerConfiguracionCliente()
        {
            oConfiguracion = new DBConfiguracionClientemovil();
            foreach (DataRow item in oConfiguracion.ObtenerNumeroDeProductosAMostrar().Rows)
            {
                Uid = new Guid(item["uid"].ToString());
                StrValor = item["vchValor"].ToString();
                StrNombre = item["vchNombre"].ToString();
            }
        }
        #endregion
    }
}
