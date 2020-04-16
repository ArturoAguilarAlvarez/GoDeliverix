using AllSuministradora.Recursos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllSuministradora.model
{
    public class Licencia : NotifyBase
    {
        #region Propiedades


        //Conexion dblocal
        Conexion oConexion;
        private Guid _UidLicencia;

        public Guid uidLicencia
        {
            get { return _UidLicencia; }
            set { _UidLicencia = value; OnpropertyChanged("uidLicencia"); }
        }

        #endregion
        #region Contructor
        public Licencia()
        {

        }
        #endregion
        #region Metodos
        public int GuardarLicencia(string Uidlicencia)
        {
            //respuestas de retorno 
            // 0 = Satisfactorio
            // 1 = Licencia existente
            // 2 = Error
            try
            {
                string query = string.Empty;
                int respuesta = 2;
                oConexion = new Conexion();
                query = "select * from licencias where uidlicencia = '" + Uidlicencia + "'";

                if (oConexion.Consultas(query).Rows.Count == 0)
                {
                    query = "insert into Licencias(UidLicencia)values('" + Uidlicencia + "')";
                    oConexion.Consultas(query);
                    respuesta = 0;
                }
                else
                {
                    respuesta = 1;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal DataTable obtenerLicencias()
        {
            try
            {
                string query = "select * from Licencias";
                oConexion = new Conexion();
                return oConexion.Consultas(query);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal bool eliminarLicencia(string strLicencia)
        {
            bool response = false;
            try
            {
                string query = "delete from Licencias where uidlicencia = '" + strLicencia + "'";
                oConexion = new Conexion();
                oConexion.Consultas(query);
                response = true;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion
    }
}
