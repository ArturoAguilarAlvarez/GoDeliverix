using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VistaDelModelo
{
    public class VMGiro
    {
        #region Propiedades

        public Giro CLASSGIRO { get; set; }
        public Conexion CON { get; set; }
        public DbGiro Datos { get; set; }

        private string _strRutaImagen;

        public string RUTAIMAGEN
        {
            get { return _strRutaImagen; }
            set { _strRutaImagen = value; }
        }

        private Guid _UidvmGiro;
        public Guid UIDVM
        {
            get { return _UidvmGiro; }
            set { _UidvmGiro = value; }
        }

        private string _VchNombre;
        public string STRNOMBRE
        {
            get { return _VchNombre; }
            set { _VchNombre = value; }
        }
        private int _intEstatus;

        public int INTESTATUS
        {
            get { return _intEstatus; }
            set { _intEstatus = value; }
        }
        private string _strDescripcion;

        public string STRDESCRIPCION
        {
            get { return _strDescripcion; }
            set { _strDescripcion = value; }
        }

        public List<VMGiro> LISTADEGIRO = new List<VMGiro>();

        public List<VMGiro> LISTADEGIROSELECCIONADO = new List<VMGiro>();







        #endregion

        #region Metodos

        public bool GuardaGiro(Guid UidGiro, string strnombre, string Descripcion, string Estatus)
        {
            CLASSGIRO = new Giro();
            return CLASSGIRO.Guardar(new Giro() { UID = UidGiro, STRNOMBRE = strnombre, STRDESCRIPCION = Descripcion, ESTATUS = int.Parse(Estatus) });
        }
        public bool ActualizaGiro(Guid UIDGIRO, string STRNOMBRE, string StrDEscripcion, string Estatus)
        {
            CLASSGIRO = new Giro();
            return CLASSGIRO.Actualizar(new Giro() { UID = UIDGIRO, STRNOMBRE = STRNOMBRE, STRDESCRIPCION = StrDEscripcion, ESTATUS = int.Parse(Estatus) });
        }
        public void BuscarGiro(string Nombre = "", string UidGiro = "", string Estatus = "")
        {
            SqlCommand Comando = new SqlCommand();
            CON = new Conexion();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BuscarGiro";

                if (UidGiro == "")
                {

                    LISTADEGIRO = new List<VMGiro>();
                    if (Nombre != string.Empty && Nombre != "")
                    {
                        Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
                        Comando.Parameters["@VchNombre"].Value = Nombre;
                    }
                    if (Estatus != "-1" && Estatus != "")
                    {
                        Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                        Comando.Parameters["@IntEstatus"].Value = int.Parse(Estatus);
                    }
                    foreach (DataRow item in CON.Busquedas(Comando).Rows)
                    {
                        Guid id = new Guid(item["UidGiro"].ToString());
                        string nombre = item["VchNombre"].ToString();
                        int estatus = int.Parse(item["intEstatus"].ToString());
                        LISTADEGIRO.Add(new VMGiro() { UIDVM = id, STRNOMBRE = nombre, INTESTATUS = estatus });
                    }
                }
                else
                {
                    if (UidGiro != "")
                    {
                        Comando.Parameters.Add("@UidGiro", SqlDbType.UniqueIdentifier);
                        Comando.Parameters["@UidGiro"].Value = new Guid(UidGiro);
                    }
                    foreach (DataRow item in CON.Busquedas(Comando).Rows)
                    {
                        UIDVM = new Guid(item["UidGiro"].ToString());
                        STRNOMBRE = item["VchNombre"].ToString();
                        STRDESCRIPCION = item["VchDescripcion"].ToString();
                        INTESTATUS = int.Parse(item["intEstatus"].ToString());
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SeleccionDeGiro(string UidGiro)
        {
            VMGiro Objeto = new VMGiro();
            Datos = new DbGiro();
            foreach (DataRow item in Datos.GiroConImagen(UidGiro).Rows)
            {
                Guid Id = new Guid(item["UidGiro"].ToString());
                string Nombre = item["VchNombre"].ToString().ToUpper();
                string RutaDeImagen = item["NVchRuta"].ToString();

                Objeto = new VMGiro() { UIDVM = Id, STRNOMBRE = Nombre, RUTAIMAGEN = RutaDeImagen };
            }

            if (LISTADEGIROSELECCIONADO == null)
            {
                LISTADEGIROSELECCIONADO.Add(Objeto);
            }
            if (LISTADEGIROSELECCIONADO != null)
            {
                LISTADEGIROSELECCIONADO.Add(Objeto);
            }
        }
        public void EliminaSeleccionDeGiro(string UidGiro)
        {
            VMGiro ObjetoAEliminar = LISTADEGIROSELECCIONADO.Find(Giro => Giro.UIDVM == new Guid(UidGiro));
            LISTADEGIROSELECCIONADO.Remove(ObjetoAEliminar);
        }

        /// <summary>
        /// Obtiene todos los giros con su imagen de la base de datos
        /// </summary>
        /// <param name="tipo"></param>
        public void ListaDeGiroConimagen()
        {
            Datos = new DbGiro();
            foreach (DataRow item in Datos.ListaDeGiroConImagen().Rows)
            {

                Guid UidGiro = new Guid(item["UidGiro"].ToString());
                string Nombre = item["VchNombre"].ToString().ToUpper();
                string Descripcion = item["VchDescripcion"].ToString();
                string RutaDeImagen = item["NVchRuta"].ToString();

                //Valida si la descripcion no contiene mas de 10 caracteres

                string NuevaDescripcion = "";
                for (int i = 0; i < Nombre.Length; i++)
                {
                    if (i == 10)
                    {
                        NuevaDescripcion += Nombre.Substring(i, 1) + "\n";
                    }
                    if (i < 10 || i > 10)
                    {
                        NuevaDescripcion += Nombre.Substring(i, 1);
                    }
                }
                Nombre = NuevaDescripcion;
                RutaDeImagen = "../" + RutaDeImagen;
                VMGiro Objeto = new VMGiro() { UIDVM = UidGiro, STRNOMBRE = Nombre, STRDESCRIPCION = Descripcion, RUTAIMAGEN = RutaDeImagen };
                LISTADEGIRO.Add(Objeto);
            }
        }



        #endregion
    }
}
