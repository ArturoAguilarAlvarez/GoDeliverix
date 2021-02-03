using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    public class DBConfiguracionClientemovil
    {
        #region Propiedades
        public Conexion oConexion { get; set; }
        #endregion
        #region Implementacion
        public DataTable ObtenerNumeroDeProductosAMostrar()
        {
            oConexion = new Conexion();
            string query = "select * from ConfiguracionClienteMovil where VchNombre = 'Numero de productos'";
            return oConexion.Consultas(query);
        }
        #endregion
    }
}
