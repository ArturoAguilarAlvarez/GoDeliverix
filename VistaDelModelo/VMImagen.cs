using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;

using System.IO;

namespace VistaDelModelo
{
    public class VMImagen
    {

        #region Propiedades
        private DbImagen Conexion = new DbImagen();
        private Imagen _Imagen;
        private Guid _UidImagen;

        private Stream _bitimage;

        public Stream BitImage
        {
            get { return _bitimage; }
            set { _bitimage = value; }
        }


        public Guid ID
        {
            get { return _UidImagen; }
            set { _UidImagen = value; }
        }

        private string _Ruta;

        public string STRRUTA
        {
            get { return _Ruta; }
            set { _Ruta = value; }
        }
        private string _descripcion;

        public string STRDESCRIPCION
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public List<VMImagen> listaDeImagenes = new List<VMImagen>();

        public Imagen Img
        {
            get { return _Imagen = new Imagen(); }
            set { _Imagen = value; }
        }

        private Guid _UidPropietario;

        public Guid UidPropietario
        {
            get { return _UidPropietario; }
            set { _UidPropietario = value; }
        }
        private string _strNombrePropietario;

        public string NombrePropietario
        {
            get { return _strNombrePropietario; }
            set { _strNombrePropietario = value; }
        }


        #endregion
        public bool GuardaImagen(string Ruta, string UidUsuario, string StoreProcedure, string Descripcion = "")
        {
            bool Resultado = false;
            Imagen IMG = new Imagen() { ID = Guid.NewGuid(), STRRUTA = Ruta, STRDESCRIPCION = Descripcion };
            Resultado = Img.GUARDARIMAGEN(IMG, new Guid(UidUsuario), StoreProcedure);

            return Resultado;
        }
        public void GuardarImagenes(List<VMImagen> lista, string StoreProcedure)
        {
            foreach (var item in lista)
            {
                EliminarImagenSubcategoria(item.UidPropietario);
                Img.GUARDARIMAGEN(
                new Imagen() { ID = item.ID, STRRUTA = item.STRRUTA, STRDESCRIPCION = item.STRDESCRIPCION }
                , item.UidPropietario
                , StoreProcedure);
            }

        }

        protected void EliminarImagenSubcategoria(Guid IdSubcategoria)
        {
            Conexion.EliminarImagenSubcategoria(IdSubcategoria);
        }

        public bool ActualizarImagenEmpresa(string UidImagen, string Ruta, Guid uidempresa, string Descripcon = "")
        {
            bool Resultado = false;
            Imagen IMG = new Imagen() { ID = new Guid(UidImagen), STRRUTA = Ruta, STRDESCRIPCION = Descripcon };
            Img.ACTUALIZAIMAGEN(IMG, uidempresa);
            return Resultado;
        }
        public bool ValidarExtencionImagen(string extencion)
        {
            bool correcto = false;
            string[] extensionePerfmitidas = { ".png", ".jpg", ".jpeg" };

            for (int i = 0; i < extensionePerfmitidas.Length; i++)
            {
                if (extencion == extensionePerfmitidas[i])
                {
                    correcto = true;
                }
            }
            return correcto;
        }

        /// <summary>
        /// Retorna la ruta de la imagen asociada a una empresa
        /// </summary>
        /// <param name="UidEmpresa"></param>
        /// <returns></returns>
        public void ObtenerImagenPerfilDeEmpresa(string UidEmpresa)
        {
            ID = new Guid();
            STRRUTA = string.Empty;
            STRDESCRIPCION = string.Empty;
            foreach (DataRow item in Conexion.RutaImagenEmpresa(UidEmpresa).Rows)
            {
                ID = new Guid(item["UidImagen"].ToString());
                STRRUTA = item["NVchRuta"].ToString();
                STRDESCRIPCION = item["VchDescripcion"].ToString();
            }
        }
        public void ObtenerImagenCategoria(string UidEmpresa)
        {
            STRRUTA = null;
            ID = Guid.Empty;
            foreach (DataRow item in Conexion.RutaImagenCategoria(UidEmpresa).Rows)
            {
                ID = new Guid(item["UidImagen"].ToString());
                STRRUTA = item["NVchRuta"].ToString();
                STRDESCRIPCION = item["VchDescripcion"].ToString();
            }
        }
        public void ObtenerImagenGiro(string UidGiro)
        {
            STRRUTA = null;
            ID = Guid.Empty;
            foreach (DataRow item in Conexion.RutaImagenGiro(UidGiro).Rows)
            {
                ID = new Guid(item["UidImagen"].ToString());
                STRRUTA = item["NVchRuta"].ToString();
                STRDESCRIPCION = item["VchDescripcion"].ToString();
            }
        }
        public void ObtenerImagenSubcategoria(string UidSubcategoria)
        {
            STRRUTA = null;
            ID = Guid.Empty;
            foreach (DataRow item in Conexion.RutaImagenSubcategoria(UidSubcategoria).Rows)
            {
                ID = new Guid(item["UidImagen"].ToString());
                STRRUTA = item["NVchRuta"].ToString();
                STRDESCRIPCION = item["VchDescripcion"].ToString();
            }
        }

        public void ObtenerImagenesSubcaterias(Guid Subcategoria)
        {
            listaDeImagenes = new List<VMImagen>();
            foreach (DataRow item in Conexion.ImagenesSubCategoria(Subcategoria.ToString()).Rows)
            {
                Guid UidImagen = new Guid(item["UidImagen"].ToString());
                string ruta = item["NVchRuta"].ToString();
                string propietario = item["UidSubcategoria"].ToString();
                listaDeImagenes.Add(new VMImagen() { ID = UidImagen, STRRUTA = ruta, UidPropietario = Guid.Parse(propietario) });
            }
        }
        public void ObtenerImagenProducto(string uidProductoSeleccionado)
        {
            listaDeImagenes.Clear();
            foreach (DataRow item in Conexion.ImagenProducto(uidProductoSeleccionado).Rows)
            {
                Guid UidImagen = new Guid(item["UidImagen"].ToString());
                string ruta = item["NVchRuta"].ToString();
                string propietario = item["UidProducto"].ToString();
                ID = UidImagen;
                STRRUTA = ruta;
                UidPropietario = Guid.Parse(propietario);
            }
        }

        public int ValidaExistenciaDeImagen(string Ruta)
        {
            int existe = 0;

            foreach (DataRow item in Conexion.ValidarExistencia(Ruta).Rows)
            {
                existe = existe + 1;
            }
            return existe;
        }
        public void CrearListaDeImagenes(string Ruta, Guid UidUsuario, string Descripcion = "")
        {
            VMImagen Imagen = new VMImagen() { ID = Guid.NewGuid(), STRRUTA = Ruta, STRDESCRIPCION = Descripcion, UidPropietario = UidUsuario };

            if (listaDeImagenes != null)
            {
                listaDeImagenes.Add(Imagen);
            }
            else if (listaDeImagenes == null)
            {
                listaDeImagenes.Add(Imagen);
            }
        }

        public void AcualizaListaDeImagenes(Guid UidImagen, string Ruta, Guid UidUsuario, string Descripcion = "")
        {
            VMImagen Imagen = new VMImagen();
            Imagen = listaDeImagenes.Find(img => img.ID == UidImagen);
            VMImagen NuevaImagen = new VMImagen() { ID = UidImagen, STRRUTA = Ruta, STRDESCRIPCION = Descripcion, UidPropietario = UidUsuario };
            listaDeImagenes.Remove(Imagen);
            listaDeImagenes.Add(NuevaImagen);
        }
        public void ObtenerRutaDeImagenDeLista(Guid UidUsuario)
        {
            VMImagen Imagen = new VMImagen();
            Imagen = listaDeImagenes.Find(imagen => imagen.UidPropietario == UidUsuario);
            if (Imagen != null)
            {
                STRRUTA = Imagen.STRRUTA;
                STRDESCRIPCION = Imagen.STRDESCRIPCION;
            }
            else
            {
                STRRUTA = null;
            }
        }


        public void EliminaImagenEmpresa(string StrRuta)
        {
            Conexion.EliminaImagenEmpresa(StrRuta);
        }
        public void EliminaImagenCategoria(string UidCategoria)
        {
            Conexion.EliminaImagenCategoria(UidCategoria);
        }
        public void EliminaImagenGiro(string UidGiro)
        {
            Conexion.EliminarImagenGiro(UidGiro);
        }

        public void EliminaImagenProducto(string uidproducto)
        {
            Conexion.EliminaImagenProducto(uidproducto);
        }

        public void EliminaImagenSubcategoria(Guid uidSubcategoria)
        {
            Conexion.EliminarImagenSubcategoria(uidSubcategoria);
        }


        public void obtenerImagenDePortadaEmpresa(string uidempresaSelecciona)
        {
            ID = new Guid();
            STRRUTA = string.Empty;
            STRDESCRIPCION = string.Empty;
            foreach (DataRow item in Conexion.RutaImagenPortadaEmpresa(uidempresaSelecciona).Rows)
            {
                ID = new Guid(item["UidImagen"].ToString());
                STRRUTA = item["NVchRuta"].ToString();
                STRDESCRIPCION = item["VchDescripcion"].ToString();
            }
        }

        public void EliminaImagenPortadaEmpresa(string strruta)
        {
            Conexion.EliminaImagenPortadaEmpresa(strruta);
        }
    }
}
