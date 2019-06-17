using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DBControl
{
    public class DbCategoria
    {
        Conexion oConexion;

        public DataTable Busquedas(SqlCommand comando)
        {
            oConexion = new Conexion();
            return oConexion.Busquedas(comando);
        }

        public DataTable CategoriaConImagen(string UidGiro)
        {
            oConexion = new Conexion();
            string Query = "select c.UidCategoria, c.VchNombre, c.NVchDescripcion, i.NVchRuta from categorias c inner join ImagenCategoria IC on IC.UidCategoria = C.UidCategoria inner join Imagenes I on I.UIdImagen = IC.UidImagen where c.UidGiro = '" + UidGiro + "'";
            return oConexion.Consultas(Query);
        }

        public DataTable Categoria(string UidCategoria)
        {
            oConexion = new Conexion();
            string Query = "select c.UidCategoria, c.VchNombre, c.NVchDescripcion, i.NVchRuta,c.UidGiro from categorias c inner join ImagenCategoria IC on IC.UidCategoria = C.UidCategoria inner join Imagenes I on I.UIdImagen = IC.UidImagen where c.UidCategoria = '" + UidCategoria + "'";
            return oConexion.Consultas(Query);
        }
    }
}
