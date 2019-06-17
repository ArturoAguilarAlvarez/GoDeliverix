using System;
using System.Data;

namespace DBControl
{
    public class DbSeccion
    {
        #region propiedades
        Conexion oConexion;

        public void eliminarProducto(Guid Uidproducto, Guid uidseccion)
        {
            oConexion = new Conexion();
            string query = "delete from SeccionProducto where uidproducto = '" + Uidproducto.ToString() + "' and uidSeccion = '" + uidseccion.ToString() + "'";
            oConexion.Consultas(query);
        }

        public DataTable encuentraRegistro(Guid uidproducto, Guid uidseccion)
        {
            oConexion = new Conexion();
            string query = "select * from SeccionProducto where uidproducto = '" + uidproducto.ToString() + "' and uidSeccion = '" + uidseccion.ToString() + "'";
            return oConexion.Consultas(query);
        }


        #endregion

    }
}
