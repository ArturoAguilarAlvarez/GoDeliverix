using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DBControl;

namespace Modelo
{
    public class Seccion
    {
        #region Propiedades

        Conexion Con;
        private Guid _UidSeccion;

        public Guid UID
        {
            get { return _UidSeccion; }
            set { _UidSeccion = value; }
        }

        private string _VchNombre;

        public string StrNombre
        {
            get { return _VchNombre; }
            set { _VchNombre = value; }
        }

        private string _VchHoraInicio;

        public string StrHoraInicio
        {
            get { return _VchHoraInicio; }
            set { _VchHoraInicio = value; }
        }

        private string _vchHoraFin;

        public string StrHoraFin
        {
            get { return _vchHoraFin; }
            set { _vchHoraFin = value; }
        }


        public Estatus oEstatus;


        public Oferta oOferta;


        #endregion

        #region Metodos

        public bool Agrega()
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaSeccion";

                Comando.Parameters.Add("@UidSeccion", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSeccion"].Value = this.UID;

                Comando.Parameters.Add("@UidOferta", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidOferta"].Value = this.oOferta.UID;

                Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 30);
                Comando.Parameters["@VchNombre"].Value = this.StrNombre;

                Comando.Parameters.Add("@VchHoraInicio", SqlDbType.NVarChar, 10);
                Comando.Parameters["@VchHoraInicio"].Value = this.StrHoraInicio;

                Comando.Parameters.Add("@VchHoraFin", SqlDbType.NVarChar, 10);
                Comando.Parameters["@VchHoraFin"].Value = this.StrHoraFin;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = this.oEstatus.ID;

                Con = new Conexion();
                resultado = Con.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool Actualiza()
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActualizaSeccion";

                Comando.Parameters.Add("@UidSeccion", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSeccion"].Value = this.UID;


                Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 30);
                Comando.Parameters["@VchNombre"].Value = this.StrNombre;

                Comando.Parameters.Add("@VchHoraInicio", SqlDbType.NVarChar, 10);
                Comando.Parameters["@VchHoraInicio"].Value = this.StrHoraInicio;

                Comando.Parameters.Add("@VchHoraFin", SqlDbType.NVarChar, 10);
                Comando.Parameters["@VchHoraFin"].Value = this.StrHoraFin;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = this.oEstatus.ID;

                Con = new Conexion();
                resultado = Con.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool GuardaProducto(Guid UIDSECCION, Guid UIDPRODUCTO)
        {
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_AgregaSeccionProducto";

                cmd.Parameters.Add("@UidSeccion", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidSeccion"].Value = UIDSECCION;

                cmd.Parameters.Add("@UidProducto", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidProducto"].Value = UIDPRODUCTO;
                Con = new Conexion();
                resultado = Con.ModificarDatos(cmd);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        #endregion
    }
}
