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
        private bool _BIncluyeComisionTarjeta;

        public bool BIncluyeComisionTarjeta
        {
            get { return _BIncluyeComisionTarjeta; }
            set { _BIncluyeComisionTarjeta = value; }
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
                BAbsorveComision = bool.Parse(item["BAboserveComision"].ToString());
                BIncluyeComisionTarjeta = bool.Parse(item["BAAbsorveComisionTarjeta"].ToString());
            }
        }

        public bool ActualizarComisionGoDeliverix()
        {
            bool resultado = false;
            try
            {
                SqlCommand Comando = new SqlCommand();

                oConexion = new Conexion();
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActualizaComisionGoDeliverix";

                Comando.Parameters.Add("@IntValorComision", SqlDbType.Int);
                Comando.Parameters["@IntValorComision"].Value = int.Parse(FValor.ToString());

                Comando.Parameters.Add("@StrNombreComision", SqlDbType.VarChar, 50);
                Comando.Parameters["@StrNombreComision"].Value = StrNombreTipoDeComision;

                resultado = oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public DataTable obtenerComisionesGoDeliverix()
        {
            oConexion = new Conexion();
            string query = "Select * from ComisionGoDeliverix";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerComisionPasarelaDeCobro(string strNombreProvedor)
        {
            oConexion = new Conexion();
            string query = "select * from ComisionesPasarela cp inner join ProvedoresPasarela pp on pp.UidProvedorPasarela = cp.UidProvedorPasarela where pp.StrProvedor = '" + strNombreProvedor + "'";
            return oConexion.Consultas(query);
        }

        public bool ActualizaComisionTarjeta()
        {
            bool resultado = false;
            try
            {
                SqlCommand Comando = new SqlCommand();

                oConexion = new Conexion();
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActualizaComisionPasarelaDeCobro";

                Comando.Parameters.Add("@UidComision", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidComision"].Value = UidComision;

                Comando.Parameters.Add("@IntValor", SqlDbType.Int);
                Comando.Parameters["@IntValor"].Value = FValor;

                resultado = oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public string ObtenerComisionDeGananciaRepartidor(string UidRepartidor)
        {
            string resultado = "";
            try
            {
                oConexion = new Conexion();
                string query = "select IntComisionDeGananciaPorEnvio from repartidor where uidusuario = '" + UidRepartidor + "'";
                DataTable datos = oConexion.Consultas(query);
                if (datos.Rows.Count == 1)
                {
                    foreach (DataRow item in datos.Rows)
                    {
                        resultado = item["IntComisionDeGananciaPorEnvio"].ToString();
                    }
                    if (string.IsNullOrEmpty(resultado))
                    {
                        resultado = "0";
                    }
                }
                return resultado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable ComisionSistema(string strNombreDeComision)
        {
            string query = "select * from ComisionGoDeliverix where StrNombreComision = '" + strNombreDeComision + "'";
            oConexion = new Conexion();
            return oConexion.Consultas(query);
        }
        #endregion
    }
}
