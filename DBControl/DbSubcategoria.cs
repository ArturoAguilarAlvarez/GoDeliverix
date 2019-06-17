using System;
using System.Data;

namespace DBControl
{
    public class DbSubcategoria
    {
        Conexion oConexion;

        public void Eliminar(Guid UidCategoria)
        {
            oConexion = new Conexion();
            string Query = "delete from Subcategoria where UidCategoria = '" + UidCategoria + "'";
            oConexion.Consultas(Query);
        }

        public DataTable SubcategoiasConImagen(string UidCategoria)
        {
            oConexion = new Conexion();
            string Query = "select S.UidSubcategoria, s.VchNombre,s.VchDescripcion, i.NVchRuta from Subcategoria S inner join ImagenSubcategoria isb on isb.UidSubcategoria = s.UidSubcategoria inner join Imagenes I on i.UIdImagen = isb.UidImagen where S.uidCategoria = '" + UidCategoria + "'";
            return oConexion.Consultas(Query);
        }

        public DataTable SubcategoriaConImagen(string UidSubcategoria)
        {
            oConexion = new Conexion();
            string Query = "select S.UidSubcategoria,s.UidCategoria, s.VchNombre,s.VchDescripcion, i.NVchRuta,s.intEstatus from Subcategoria S inner join ImagenSubcategoria isb on isb.UidSubcategoria = s.UidSubcategoria inner join Imagenes I on i.UIdImagen = isb.UidImagen where S.UidSubcategoria = '" + UidSubcategoria + "'";
            return oConexion.Consultas(Query);
        }
    }
}
