using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    /// <summary>
    /// Acceso a datos para gestion de contenido del repartidor (version 2)
    /// </summary>
    public class DealerDataAccess
    {
        /// <summary>
        /// Conexion a la base de datos
        /// </summary>
        protected readonly Conexion dbConexion;

        public DealerDataAccess()
        {
            this.dbConexion = new Conexion();
        }
    }
}
