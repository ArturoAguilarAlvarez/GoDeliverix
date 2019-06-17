using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Modelo;
using DBControl;

namespace VistaDelModelo
{
    public class VMCorreoElectronico
    {
        #region Propiedades
        Conexion oConexion;
        public DbCorreoElectronico ODbCorreoElectronico { get; set; }
        public CorreoElectronico OCorreoElectronico { get; set; }
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
        public Guid UidPropietario { get; set; }
        #endregion

        #region Metodos
        /// <summary>
        /// BUsca el correo electronico
        /// </summary>
        /// <param name="UidPropietario"></param>
        /// <param name="strParametroDebusqueda"> Empresa/Usuario</param>
        /// <param name="strCorreoElectronico"></param>
        /// <param name="UidCorreoElectronico"></param>
        public void BuscarCorreos(Guid UidPropietario = new Guid(), string strParametroDebusqueda = "", string strCorreoElectronico = "", Guid UidCorreoElectronico = new Guid())
        {
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "Asp_ObtenerCorreosElectronicos";

                if (UidPropietario != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidPropietario", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidPropietario"].Value = UidPropietario;
                }
                Comando.Parameters.Add("@ParametroDeBusqueda", SqlDbType.VarChar, 30);
                Comando.Parameters["@ParametroDeBusqueda"].Value = strParametroDebusqueda;


                if (UidCorreoElectronico != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidCorreoElectronico", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidCorreoElectronico"].Value = UidCorreoElectronico;
                }
                if (!string.IsNullOrEmpty(strCorreoElectronico))
                {
                    Comando.Parameters.Add("@StrCorreoElectronico", SqlDbType.NVarChar, 200);
                    Comando.Parameters["@StrCorreoElectronico"].Value = strCorreoElectronico;
                }
                oConexion = new Conexion();
                ID = Guid.Empty;
                CORREO = string.Empty;
                foreach (DataRow item in oConexion.Busquedas(Comando).Rows)
                {
                    ID = new Guid(item["IdCorreo"].ToString());
                    CORREO = item["Correo"].ToString();
                    if (item.Table.Columns.Contains("UidUsuario"))
                    {
                        this.UidPropietario = new Guid(item["UidUsuario"].ToString());
                    }
                    if (item.Table.Columns.Contains("IdEmpresa"))
                    {
                        this.UidPropietario = new Guid(item["IdEmpresa"].ToString());
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EliminaCorreoEmpresa(string uidempresa)
        {
            ODbCorreoElectronico = new DbCorreoElectronico();
            ODbCorreoElectronico.EliminaCorreosDeEmpresa(uidempresa);
        }
        /// <summary>
        /// //Agrega los correos electronicos de cualquier usuario especificando el parametro de insercion
        /// </summary>
        /// <param name="UidPropietario"></param>
        /// <param name="strParametroDeInsercion">Empresa,Usuario</param>
        /// <param name="strCorreoElectronico"></param>
        /// <param name="UidCorreoElectronico"></param>
        /// <returns></returns>
        public bool AgregarCorreo(Guid UidPropietario, string strParametroDeInsercion, string strCorreoElectronico, Guid UidCorreoElectronico)
        {
            OCorreoElectronico = new CorreoElectronico() { UidPropietario = UidPropietario, StrParametro = strParametroDeInsercion, CORREO = strCorreoElectronico, ID = UidCorreoElectronico };
            return OCorreoElectronico.Guardar();
        }

        public void EliminaCorreoUsuario(string UidUsuario)
        {
            ODbCorreoElectronico = new DbCorreoElectronico();
            ODbCorreoElectronico.EliminarCorreosUsuario(UidUsuario);
        }
        #endregion
    }
}
