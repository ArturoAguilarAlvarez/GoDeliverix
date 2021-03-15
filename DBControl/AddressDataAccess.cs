using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    /// <summary>
    /// Realizar consultas y manipulacion de datos 
    /// </summary>
    public class AddressDataAccess
    {
        /// <summary>
        /// Conexion a la base de datos
        /// </summary>
        protected readonly Conexion dbConexion;

        public AddressDataAccess()
        {
            this.dbConexion = new Conexion();
        }

        
    }
}
