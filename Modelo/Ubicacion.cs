using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBControl;

namespace Modelo
{
    public class Ubicacion
    {
        #region Propiedades
        Conexion Datos = new Conexion();

        private Guid _Uidubicacion;

        public Guid UID
        {
            get { return _Uidubicacion; }
            set { _Uidubicacion = value; }
        }


        private string _strLatitud;

        public string VchLatitud
        {
            get { return _strLatitud; }
            set { _strLatitud = value; }
        }

        private string _strLongitud;

        public string VchLongitud
        {
            get { return _strLongitud; }
            set { _strLongitud = value; }
        }

        #endregion

        #region Metodos
        public bool Guardar(string procedure, Guid uidPropietario)
        {
            bool resultado = false;
            Datos = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = procedure;

                Comando.Parameters.Add("@UidUbicacion", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidUbicacion"].Value = UID;
                if (uidPropietario != Guid.Empty)
                {
                    Comando.Parameters.Add("@uidPropietario", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@uidPropietario"].Value = uidPropietario;
                }

                Comando.Parameters.Add("@VchLatitud", SqlDbType.VarChar, 500);
                Comando.Parameters["@VchLatitud"].Value = VchLatitud;

                Comando.Parameters.Add("@VchLongiud", SqlDbType.VarChar, 500);
                Comando.Parameters["@VchLongiud"].Value = VchLongitud;

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
