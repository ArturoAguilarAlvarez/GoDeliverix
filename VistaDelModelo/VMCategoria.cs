using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Modelo;
using DBControl;

namespace VistaDelModelo
{
    public class VMCategoria
    {
        #region Propiedades

        DbCategoria Conn = new DbCategoria();
        Categoria Cattegoria = new Categoria();
        private Guid _UidCategoria;
        public List<VMCategoria> LISTADECATEGORIAS = new List<VMCategoria>();

        public List<VMCategoria> LISTADECATEGORIASELECIONADA = new List<VMCategoria>();

        public Guid UIDCATEGORIA
        {
            get { return _UidCategoria; }
            set { _UidCategoria = value; }
        }

        private string _VchNombre;

        public string STRNOMBRE
        {
            get { return _VchNombre; }
            set { _VchNombre = value; }
        }

        private string _VchDescripcion;

        public string STRDESCRIPCION
        {
            get { return _VchDescripcion; }
            set { _VchDescripcion = value; }
        }
        private int _intEstatus;

        public int ESTATUS
        {
            get { return _intEstatus; }
            set { _intEstatus = value; }
        }

        private string _UidGiro;

        public string UIDGIRO
        {
            get { return _UidGiro; }
            set { _UidGiro = value; }
        }

        private string _Vchruta;

        public string rutaImagen
        {
            get { return _Vchruta; }
            set { _Vchruta = value; }
        }


        #endregion

        #region Metodos
        public bool Guardar(Guid UidCategoria, string Nombre, string Descripcion, string Estatus, string UidGiro)
        {
            Categoria Objeto = new Categoria() { ID = UidCategoria, STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, oEstatus = new Estatus() { ID = int.Parse(Estatus) }, OGiro = new Giro() { UID = new Guid(UidGiro) } };
            return Cattegoria.Guardar(Objeto);
        }
        public bool Actualizar(Guid UidCategoria, string Nombre, string Descripcion, string Estatus, string UidGiro)
        {
            Categoria Objeto = new Categoria() { ID = UidCategoria, STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, oEstatus = new Estatus() { ID = int.Parse(Estatus) }, OGiro = new Giro() { UID = new Guid(UidGiro) } };
            return Cattegoria.Actualizar(Objeto);
        }
        public void BuscarCategorias(string UidCategoria = "", string Nombre = "", string Estatus = "", string UidGiro = "", string tipo = "")
        {
            SqlCommand Comando = new SqlCommand();
            LISTADECATEGORIAS = new List<VMCategoria>();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BuscarCategorias";

                if (UidCategoria == "")
                {
                    if (Nombre != string.Empty && Nombre != "")
                    {
                        Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
                        Comando.Parameters["@VchNombre"].Value = Nombre;
                    }
                    if (!string.IsNullOrEmpty(tipo))
                    {
                        Comando.Parameters.Add("@VchTipo", SqlDbType.VarChar, 10);
                        Comando.Parameters["@VchTipo"].Value = tipo;
                    }
                    if (UidGiro != string.Empty && UidGiro != "")
                    {
                        Comando.Parameters.Add("@UidGiro", SqlDbType.UniqueIdentifier);
                        Comando.Parameters["@UidGiro"].Value = new Guid(UidGiro);
                    }
                    if (Estatus != "-1" && Estatus != "")
                    {
                        Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                        Comando.Parameters["@IntEstatus"].Value = int.Parse(Estatus);
                    }
                    foreach (DataRow item in Conn.Busquedas(Comando).Rows)
                    {
                        string descripcion = "";
                        int estatus = 0;
                        Guid id = new Guid(item["UidCategoria"].ToString());
                        string nombre = item["VchNombre"].ToString();
                        if (item["NVchDescripcion"] != null)
                        {
                            descripcion = item["NVchDescripcion"].ToString();
                        }
                        if (item["intEstatus"] != null)
                        {
                            estatus = int.Parse(item["intEstatus"].ToString());
                        }

                        LISTADECATEGORIAS.Add(new VMCategoria() { UIDCATEGORIA = id, STRNOMBRE = nombre, STRDESCRIPCION = descripcion, ESTATUS = estatus });
                    }
                }
                else
                {
                    Comando.Parameters.Add("@UidCategoria", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidCategoria"].Value = new Guid(UidCategoria);

                    foreach (DataRow item in Conn.Busquedas(Comando).Rows)
                    {
                        UIDCATEGORIA = new Guid(item["UidCategoria"].ToString());
                        STRNOMBRE = item["VchNombre"].ToString();
                        if (item["NVchDescripcion"] != null)
                        {
                            STRDESCRIPCION = item["NVchDescripcion"].ToString();
                        }
                        if (item["intEstatus"] != null)
                        {
                            ESTATUS = int.Parse(item["intEstatus"].ToString());
                        }

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Retorna una lista de categorias dependiendo el giro que se le envie
        /// </summary>
        /// <param name="UidGiro"></param>
        public void CategoriaConImagen(string UidGiro)
        {
            LISTADECATEGORIAS.Clear();
            foreach (DataRow item in Conn.CategoriaConImagen(UidGiro).Rows)
            {
                Guid uidCategoria = new Guid(item["UidCategoria"].ToString());
                string Nombre = item["VchNombre"].ToString();
                string Descripcion = item["NVchDescripcion"].ToString();
                string Ruta = item["NVchRuta"].ToString();
                LISTADECATEGORIAS.Add(new VMCategoria() { UIDCATEGORIA = uidCategoria, STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, rutaImagen = Ruta });
            }
        }
        public void SeleccionarCategoria(string UidCategoria)
        {
            foreach (DataRow item in Conn.Categoria(UidCategoria).Rows)
            {
                Guid uidCategoria = new Guid(item["UidCategoria"].ToString());
                string Nombre = item["VchNombre"].ToString();
                string Descripcion = item["NVchDescripcion"].ToString();
                string Ruta = item["NVchRuta"].ToString();
                string Uidgiro = item["UidGiro"].ToString();
                LISTADECATEGORIASELECIONADA.Add(new VMCategoria() { UIDCATEGORIA = uidCategoria, STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, rutaImagen = Ruta, UIDGIRO = Uidgiro });
            }
        }

        public void DeselecionarCategoria(string UidCategoria)
        {
            var Categoria = LISTADECATEGORIASELECIONADA.Find(Cat => Cat.UIDCATEGORIA.ToString() == UidCategoria);
            LISTADECATEGORIASELECIONADA.Remove(Categoria);
        }

        #endregion
    }
}
