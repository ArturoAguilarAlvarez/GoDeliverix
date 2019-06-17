using System;
using System.Data;

namespace DBControl
{
    public class DbImagen
    {
        #region Propiedades
        Conexion oConexion;
        #endregion
        #region Metodos
        public DataTable RutaImagenEmpresa(string UidEmpresa)
        {
            oConexion = new Conexion();
            string Query = "Select * from Imagenes where UidImagen in (Select UidImagen from ImagenEmpresa where UidEmpresa = '" + UidEmpresa + "') and NVchRuta  like '%FotoPerfil%'";
            return oConexion.Consultas(Query);
        }

        public DataTable RutaImagenCategoria(string UidCategoria)
        {
            oConexion = new Conexion();
            string Query = "Select * from Imagenes where UidImagen in(Select UidImagen from ImagenCategoria where UidCategoria = '" + UidCategoria + "')";
            return oConexion.Consultas(Query);
        }
        public DataTable RutaImagenGiro(string UidGiro)
        {
            oConexion = new Conexion();
            string Query = "Select * from Imagenes where UidImagen in (select UidImagen from ImagenGiro where UidGiro = '" + UidGiro + "')";
            return oConexion.Consultas(Query);
        }
        public DataTable RutaImagenSubcategoria(string UidSubcategoria)
        {
            oConexion = new Conexion();
            string Query = "select * from imagenes where UIdImagen in (select UidImagen from ImagenSubcategoria where UidSubcategoria = '" + UidSubcategoria + "')";
            return oConexion.Consultas(Query);
        }
        public DataTable ImagenesSubCategoria(string UidCategoria)
        {
            oConexion = new Conexion();
            string Query = " Select IMg.NVchRuta, Sub.UidSubcategoria,Img.UidImagen from Imagenes Img "
                + " inner join ImagenSubcategoria imgsub on imgsub.UidImagen = Img.UIdImagen   inner join Subcategoria Sub on Sub.UidSubcategoria = imgsub.UidSubcategoria"
                + "  where Sub.UidCategoria = '" + UidCategoria + "'";
            return oConexion.Consultas(Query);
        }
        public void EliminaImagenEmpresa(string StrRuta)
        {
            oConexion = new Conexion();
            string Query = "delete from ImagenEmpresa where UidImagen in (select UidImagen from Imagenes where nvchRuta = '" + StrRuta + "')" +
                " delete from Imagenes where nvchRuta = '" + StrRuta + "'";
            oConexion.Consultas(Query);
        }
        public void EliminaImagenCategoria(string UidCategoria)
        {
            oConexion = new Conexion();
            string Query = "delete from Imagenes where UidImagen in (select UidImagen from ImagenCategoria where UidCategoria = '" + UidCategoria + "');delete from ImagenCategoria where UidCategoria = '" + UidCategoria + "'";
            oConexion.Consultas(Query);
        }

        public void EliminarImagenSubcategoria(Guid idSubcategoria)
        {
            oConexion = new Conexion();
            string Query = "delete from Imagenes where UidImagen in(select UidImagen from ImagenSubcategoria where UidSubcategoria = '" + idSubcategoria.ToString() + "');delete from ImagenSubcategoria where uidSubcategoria = '" + idSubcategoria.ToString() + "'";
            oConexion.Consultas(Query);
        }
        public void EliminarImagenGiro(string UidGiro)
        {
            oConexion = new Conexion();
            string Query = "delete from Imagenes where UidImagen in(select UidImagen from ImagenGiro where UidGiro = '" + UidGiro.ToString() + "');delete from ImagenGiro where UidGiro = '" + UidGiro.ToString() + "'";
            oConexion.Consultas(Query);
        }

        public DataTable ImagenProducto(string uidProductoSeleccionado)
        {
            oConexion = new Conexion();
            string Query = "select i.NVchRuta,i.UIdImagen,p.UidProducto from imagenes i inner join ImagenProducto IP on IP.UidImagen = i.UIdImagen inner join Productos p on p.UidProducto = IP.UidProducto where p.UidProducto = '" + uidProductoSeleccionado + "'";
            return oConexion.Consultas(Query);
        }

        public void EliminaImagenProducto(string uidproducto)
        {
            oConexion = new Conexion();
            string Query = "delete from Imagenes where UidImagen in (select UidImagen from ImagenProducto where UidProducto = '" + uidproducto + "');delete from ImagenProducto where UidProducto = '" + uidproducto + "'";
            oConexion.Consultas(Query);
        }

        public DataTable ValidarExistencia(string ruta)
        {
            oConexion = new Conexion();
            string Query = "select * from Imagenes where NVChRuta = '" + ruta + "'";
            return oConexion.Consultas(Query);
        }

        public DataTable RutaImagenPortadaEmpresa(string uidempresaSelecciona)
        {
            oConexion = new Conexion();
            string Query = "select i.UIdImagen,i.nvchruta,i.vchdescripcion from Imagenes i inner join ImagenEmpresa ie on ie.UidImagen = i.UIdImagen  where ie.UidEmpresa = '"+ uidempresaSelecciona + "' and i.NVchRuta like '%portada%'";
            return oConexion.Consultas(Query);
        }

        public void EliminaImagenPortadaEmpresa(object strRuta)
        {
            oConexion = new Conexion();
            string Query = "delete from ImagenEmpresa where UidImagen in (select UidImagen from Imagenes where nvchruta = '" + strRuta + "')" +
                " delete from Imagenes where nvchruta ='" + strRuta + "'";
            oConexion.Consultas(Query);
        }
        #endregion
    }
}
