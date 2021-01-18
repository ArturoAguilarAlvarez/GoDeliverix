using DBControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class VMTerminosYCondiciones
    {
        #region Propiedades
        public Conexion oConexion { get; set; }
        public DBTerminosYCondiciones oDbTerminosYCondiciones { get; set; }
        private Guid _Uid;

        public Guid Uid
        {
            get { return _Uid; }
            set { _Uid = value; }
        }
        private string _url;

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        #endregion
        public VMTerminosYCondiciones()
        {

        }

        #region Implementacion
        public bool TerminosYCondicionesAceptadas(string uidUsuario, string uidTerminosYCondiciones)
        {

            bool resultado = false;
            oConexion = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActualizaTerminosYCondicionesUsuario";

                Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidUsuario"].Value = new Guid(uidUsuario);

                Comando.Parameters.Add("@UidTerminosYCondicinoes", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidTerminosYCondicinoes"].Value = new Guid(uidTerminosYCondiciones);

                resultado = oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public void CheckUserTermsAndConditions(string UidUsuario, string Idioma)
        {
            oDbTerminosYCondiciones = new DBTerminosYCondiciones();
            var registro = oDbTerminosYCondiciones.VerificaUsuario(UidUsuario, Idioma);
            if (registro.Rows.Count > 0)
            {
                foreach (DataRow item in registro.Rows)
                {
                    Url = item["VchUrl"].ToString();
                    Uid = new Guid(item["Uid"].ToString());
                }
            }
        }
        public void BuscarTerminosYCondiciones(string Idioma, string UidLada = "", string Uidusuario = "")
        {
            oDbTerminosYCondiciones = new DBTerminosYCondiciones();
            var registro = oDbTerminosYCondiciones.BuscaTerminosYCondiciones(Idioma, UidLada, Uidusuario);
            if (registro.Rows.Count > 0)
            {
                foreach (DataRow item in registro.Rows)
                {
                    Url = item["VchUrl"].ToString();
                    Uid = new Guid(item["Uid"].ToString());
                }
            }
        }
        #endregion
    }
}
