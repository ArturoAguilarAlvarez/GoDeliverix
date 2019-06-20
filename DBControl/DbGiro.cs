using System.Data;

namespace DBControl
{
    public class DbGiro
    {
        Conexion oConexion;

        public DataTable ListaDeGiroConImagen()
        {
            oConexion = new Conexion();
            string Query = "";

            Query = "select g.UidGiro, g.VchNombre, g.VchDescripcion, i.NVchRuta from Giro g inner join ImagenGiro ig on ig.UidGiro = g.UidGiro inner join Imagenes I on i.UIdImagen = ig.UidImagen";

            return oConexion.Consultas(Query);
        }

        public DataTable GiroConImagen(string uidGiro)
        {
            oConexion = new Conexion();
            string Query = "select g.UidGiro, g.VchNombre, g.VchDescripcion, i.NVchRuta from Giro g inner join ImagenGiro ig on ig.UidGiro = g.UidGiro inner join Imagenes I on i.UIdImagen = ig.UidImagen where g.uidGiro = '" + uidGiro + "'";

            return oConexion.Consultas(Query);
        }


    }
}
