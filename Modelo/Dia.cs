using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBControl;

namespace Modelo
{
    public class Dia
    {
        #region Propiedades
        Conexion Datos;
        private Guid _uidDia;

        public Guid UID
        {
            get { return _uidDia; }
            set { _uidDia = value; }
        }

        private string _strNombre;

        public string StrNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        private int _intorden;

        public int INTOrden
        {
            get { return _intorden; }
            set { _intorden = value; }
        }
        #endregion

        #region metodos
        public bool Guardar(string procedure, Guid uidUsuario)
        {
            bool resultado = false;
            Datos = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = procedure;

                Comando.Parameters.Add("@UidDia", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidDia"].Value = UID;

                Comando.Parameters.Add("@uidPropietario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@uidPropietario"].Value = uidUsuario;

                resultado = Datos.ModificarDatos(Comando);
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
