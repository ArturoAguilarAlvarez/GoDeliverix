using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VistaDelModelo
{
    public class VMSubCategoria
    {
        #region Propiedades
        Conexion Con = new Conexion();
        DbSubcategoria CONSULTAS = new DbSubcategoria();
        private Guid UidSubcategoria;

        public Guid UID
        {
            get { return UidSubcategoria; }
            set { UidSubcategoria = value; }
        }
        private Guid _Uidategoria;

        public Guid UIDCATEGORIA
        {
            get { return _Uidategoria; }
            set { _Uidategoria = value; }
        }

        private string _StrNombre;

        public string STRNOMBRE
        {
            get { return _StrNombre; }
            set { _StrNombre = value; }
        }
        private string _descripcion;

        public string STRDESCRIPCION
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private int _intEstatus;

        public int ESTATUS
        {
            get { return _intEstatus; }
            set { _intEstatus = value; }
        }

        private string _strRuta;

        public string rutaImagen
        {
            get { return _strRuta; }
            set { _strRuta = value; }
        }



        public List<VMSubCategoria> LISTADESUBCATEGORIAS = new List<VMSubCategoria>();
        public List<VMSubCategoria> LISTADESUBCATEGORIASSELECCIONADAS = new List<VMSubCategoria>();
        public SubCategoria SUBCATTEGORIA = new SubCategoria();
        #endregion
        public void CrearSubCategoria(string UidSubCategoria, string Nombre, string Descripcion)
        {
            VMSubCategoria VMS = new VMSubCategoria()
            {
                UID = new Guid(UidSubCategoria),
                STRNOMBRE = Nombre,
                STRDESCRIPCION = Descripcion
            };
            if (LISTADESUBCATEGORIAS != null)
            {
                LISTADESUBCATEGORIAS.Add(VMS);
            }
            else if (LISTADESUBCATEGORIAS == null)
            {
                LISTADESUBCATEGORIAS.Add(VMS);
            }
        }

        public void EliminarSubcategoria(string UidSubcategoria)
        {
            VMSubCategoria SUBCATTEGORI = new VMSubCategoria();
            SUBCATTEGORI = LISTADESUBCATEGORIAS.Find(SubCat => SubCat.UID.ToString() == UidSubcategoria);
            LISTADESUBCATEGORIAS.Remove(SUBCATTEGORI);
        }
        public void ActualizarLista(string UidSubCategoria, string Nombre, string Descripcion)
        {
            VMSubCategoria Categoria = new VMSubCategoria();
            Categoria = LISTADESUBCATEGORIAS.Find(Cat => Cat.UID.ToString() == UidSubCategoria);
            VMSubCategoria NuevaSubCategoria = new VMSubCategoria() { UID = new Guid(UidSubCategoria), UIDCATEGORIA = Categoria.UIDCATEGORIA, STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion };
            VMSubCategoria AnteriorSubCategoria = new VMSubCategoria();

            AnteriorSubCategoria = LISTADESUBCATEGORIAS.Find(Subcat => Subcat.UID.ToString() == UidSubCategoria);
            LISTADESUBCATEGORIAS.Remove(AnteriorSubCategoria);
            LISTADESUBCATEGORIAS.Add(NuevaSubCategoria);
        }

        public void GuardarListaSubcategorias(List<VMSubCategoria> Lista, string UidCategoria)
        {
            SUBCATTEGORIA = new SubCategoria();
            foreach (var item in Lista)
            {
                SUBCATTEGORIA.Guardar(new SubCategoria() { UID = item.UID, UIDCATEGORIA = new Guid(UidCategoria), STRNOMBRE = item.STRNOMBRE, STRDESCRIPCION = item.STRDESCRIPCION });
            }
        }
        public bool GuardarSubcategoria(string UidSubCategoria, string Nombre, string Descripcion, string Estatus, string uidcategoria)
        {
            return SUBCATTEGORIA.Guardar(new SubCategoria() { UID = new Guid(UidSubCategoria), UIDCATEGORIA = new Guid(uidcategoria), STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, oEstatus = new Estatus() { ID = int.Parse(Estatus) } });

        }
        public bool ActualizaSubcategoria(string UidSubCategoria, string Nombre, string Descripcion, string Estatus)
        {
            return SUBCATTEGORIA.Actualizar(new SubCategoria() { UID = new Guid(UidSubCategoria), STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, oEstatus = new Estatus() { ID = int.Parse(Estatus) } });

        }
        public void BuscarSubCategoria(string UidSubCategoria = "", string UidCategoria = "", string Nombre = "", string estatus = "", string Tipo = "")
        {
            SqlCommand Comando = new SqlCommand();
            LISTADESUBCATEGORIAS = new List<VMSubCategoria>();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BuscarSubcategoria";

                if (UidSubCategoria == "")
                {
                    if (UidCategoria != "")
                    {
                        Comando.Parameters.Add("@UidCategoria", SqlDbType.UniqueIdentifier);
                        Comando.Parameters["@UidCategoria"].Value = new Guid(UidCategoria);
                    }
                    if (Nombre != string.Empty && Nombre != "")
                    {
                        Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
                        Comando.Parameters["@VchNombre"].Value = Nombre;
                    }
                    if (estatus != "-1" && estatus != "")
                    {
                        Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                        Comando.Parameters["@IntEstatus"].Value = int.Parse(estatus);
                    }
                    if (!string.IsNullOrWhiteSpace(Tipo))
                    {
                        Comando.Parameters.Add("@VchTipo", SqlDbType.VarChar, 20);
                        Comando.Parameters["@VchTipo"].Value = Nombre;
                    }
                    foreach (DataRow item in Con.Busquedas(Comando).Rows)
                    {
                        Guid uidCategoria = Guid.Empty;
                        if (item["UidCategoria"] != null)
                        {
                            uidCategoria = new Guid(item["UidCategoria"].ToString());
                        }
                        Guid uidSubcategoria = new Guid(item["UidSubcategoria"].ToString());
                        string nombre = item["VchNombre"].ToString();
                        int Estatus = int.Parse(item["intEstatus"].ToString());
                        string descripcion = item["VchDescripcion"].ToString();
                        LISTADESUBCATEGORIAS.Add(new VMSubCategoria() { UIDCATEGORIA = uidCategoria, UID = uidSubcategoria, STRNOMBRE = nombre, STRDESCRIPCION = descripcion, ESTATUS = Estatus });
                    }
                }
                else
                {
                    Comando.Parameters.Add("@UidSubCategoria", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidSubCategoria"].Value = new Guid(UidSubCategoria);

                    foreach (DataRow item in Con.Busquedas(Comando).Rows)
                    {
                        UIDCATEGORIA = new Guid(item["UidCategoria"].ToString());
                        UID = new Guid(item["UidSubcategoria"].ToString());
                        STRNOMBRE = item["VchNombre"].ToString();
                        STRDESCRIPCION = item["VchDescripcion"].ToString();
                        ESTATUS = int.Parse(item["intEstatus"].ToString());
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ObtenerSubcategoriaDeLista(Guid UidSubcategoria)
        {
            VMSubCategoria Objeto = new VMSubCategoria();
            Objeto = LISTADESUBCATEGORIAS.Find(subcat => subcat.UID == UidSubcategoria);
            UID = Objeto.UID;
            STRDESCRIPCION = Objeto.STRDESCRIPCION;
            STRNOMBRE = Objeto.STRNOMBRE;
            ESTATUS = Objeto.ESTATUS;
        }

        public void EliminarSubcategorias(Guid uidCategoria)
        {
            CONSULTAS.Eliminar(uidCategoria);
        }

        /// <summary>
        /// Obtiene todos las subcategoria de una categoria.
        /// </summary>
        /// <param name="UidSubcategoria"></param>
        public void SubcategoriaConImagen(string UidSubcategoria)
        {
            LISTADESUBCATEGORIAS.Clear();
            foreach (DataRow item in CONSULTAS.SubcategoiasConImagen(UidSubcategoria).Rows)
            {
                Guid idsucursal = new Guid(item["UidSubcategoria"].ToString());
                string Nombre = item["VchNombre"].ToString();
                string Descripcion = item["VchDescripcion"].ToString();
                string Ruta = item["NVchRuta"].ToString();
                LISTADESUBCATEGORIAS.Add(new VMSubCategoria() { UID = idsucursal, STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, rutaImagen = Ruta });
            }
        }

        public void SeleccionarSubcategoria(string UidSubcategoria)
        {
            foreach (DataRow item in CONSULTAS.SubcategoriaConImagen(UidSubcategoria).Rows)
            {
                Guid idsucursal = new Guid(item["UidSubcategoria"].ToString());
                string Nombre = item["VchNombre"].ToString();
                string Descripcion = item["VchDescripcion"].ToString();
                string Categoria = item["UidCategoria"].ToString();
                string Ruta = item["NVchRuta"].ToString();
                LISTADESUBCATEGORIASSELECCIONADAS.Add(new VMSubCategoria() { UID = idsucursal, STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, rutaImagen = Ruta, UIDCATEGORIA = new Guid(Categoria) });
            }
        }

        public void DeseleccionarSubcategoria(string UidSubcategoria)
        {
            var Subcateogria = LISTADESUBCATEGORIASSELECCIONADAS.Find(Subc => Subc.UID.ToString() == UidSubcategoria);
            LISTADESUBCATEGORIASSELECCIONADAS.Remove(Subcateogria);
        }

        public void Obtenersubcategoria(string guid)
        {
            foreach (DataRow item in CONSULTAS.SubcategoriaConImagen(guid).Rows)
            {
                Guid idsucursal = new Guid(item["UidSubcategoria"].ToString());
                string Nombre = item["VchNombre"].ToString();
                string Descripcion = item["VchDescripcion"].ToString();
                string Ruta = item["NVchRuta"].ToString();
                string Estatus = item["intEstatus"].ToString();
                UID = idsucursal;
                STRNOMBRE = Nombre;
                STRDESCRIPCION = Descripcion;
                rutaImagen = Ruta;
                ESTATUS = int.Parse(Estatus);
            }
        }
    }
}
