using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DBControl
{
    public class DBMensajes
    {
        Conexion oConexcion;
        public void EliminarMensajesSucursal(string UidSucursal)
        {
            string query = "delete from mensajes where uidMensaje in (select UidMensaje from Mensajes_Sucursal where UidSucursal = '" + UidSucursal + "'); delete from Mensajes_Sucursal where UidSucursal ='" + UidSucursal + "'";
            oConexcion = new Conexion();
            oConexcion.Consultas(query);
        }
    }
}
