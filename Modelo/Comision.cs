using DBControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Comision
    {
        #region Propiedades

        private Guid _UidComision;

        public Guid UidComision
        {
            get { return _UidComision; }
            set { _UidComision = value; }
        }
        private Guid _UidEmpresa;

        public Guid UidEmpresa
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }
        private Guid _TipoDeComision;

        public Guid UidTipoDeComision
        {
            get { return _TipoDeComision; }
            set { _TipoDeComision = value; }
        }
        private string _strNombreTipo;

        public string StrNombreTipoDeComision
        {
            get { return _strNombreTipo; }
            set { _strNombreTipo = value; }
        }

        private float _FValor;

        public float FValor
        {
            get { return _FValor; }
            set { _FValor = value; }
        }
        private bool _BAbsorveComision;

        public bool BAbsorveComision
        {
            get { return _BAbsorveComision; }
            set { _BAbsorveComision = value; }
        }

        Conexion oConexion;
        string query;
        #endregion

        #region Metodos
        public DataTable ObtenerTipoDeComision()
        {
            oConexion = new Conexion();
            query = "select * from tipodecomision";
            return oConexion.Consultas(query);
        }

        public void ComisionDeEmpresa(Guid UidEmpresa)
        {
            SqlCommand Comando = new SqlCommand();

            oConexion = new Conexion();
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.CommandText = "asp_BuscarComisiones";

            Comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidEmpresa"].Value = UidEmpresa;

            foreach (DataRow item in oConexion.Busquedas(Comando).Rows)
            {
                UidComision = new Guid(item["UidComision"].ToString());
                UidTipoDeComision = new Guid(item["UidTipoDeComision"].ToString());
                FValor = float.Parse(item["DValor"].ToString());
                BAbsorveComision = bool.Parse(item["BAboserveComision"].ToString());
            }
        }
        #endregion
    }
}
