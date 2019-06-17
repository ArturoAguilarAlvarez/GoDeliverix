using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBControl;

namespace Modelo
{
    public class CorreoElectronico
    {
        #region Propiedades
        Conexion oConexion;
        private Guid UidEmail;
        public Guid ID
        {
            get { return UidEmail; }
            set { UidEmail = value; }
        }
        private string StrCorreo;
        public string CORREO
        {
            get { return StrCorreo; }
            set { StrCorreo = value; }
        }

        public string StrParametro { get; set; }
        public Guid UidPropietario { get; set; }
        #endregion

        /// <summary>
        /// El metodo guardar sirve para actualizar porque en el store procedure borra los registros.
        /// </summary>
        /// <returns></returns>
        public bool Guardar()
        {
            bool resultado = false;
            oConexion = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "Asp_AgregarCorreoElectronico";

                Comando.Parameters.Add("@UidPropietario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidPropietario"].Value = UidPropietario;

                Comando.Parameters.Add("@UidCorreoElectronico", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidCorreoElectronico"].Value = ID;

                Comando.Parameters.Add("@StrParametoDeInsercion", SqlDbType.VarChar, 30);
                Comando.Parameters["@StrParametoDeInsercion"].Value = StrParametro;

                Comando.Parameters.Add("@StrCorreoElectronico", SqlDbType.NVarChar, 200);
                Comando.Parameters["@StrCorreoElectronico"].Value = CORREO;

                resultado = oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
    }
}
