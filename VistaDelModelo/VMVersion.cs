using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace VistaDelModelo
{
    public class VMVersion
    {
        #region Propiedades
        Conexion oConexion;
        private Guid _UidVersion;

        public Guid UidVersion
        {
            get { return _UidVersion; }
            set { _UidVersion = value; }
        }

        private Guid _UidAplicacion;

        public Guid UidAplicacion
        {
            get { return _UidAplicacion; }
            set { _UidAplicacion = value; }
        }
        private DateTime _dtmFechaDeRegistro;

        public DateTime DtmFecha
        {
            get { return _dtmFechaDeRegistro; }
            set { _dtmFechaDeRegistro = value; }
        }
        private string _strVersion;

        public string StrVersion
        {
            get { return _strVersion; }
            set { _strVersion = value; }
        }

        #endregion
        #region Metodos
        public void ObtenerVersion(Guid UidAplicacion)
        {
            SqlCommand Comando = new SqlCommand();
            oConexion = new Conexion();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ObtenerVersionDeAplicacion";

                Comando.Parameters.AddWithValue("@UidAplicacion", UidAplicacion);

                foreach (DataRow item in oConexion.Busquedas(Comando).Rows)
                {
                    StrVersion = item["VchVersion"].ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
