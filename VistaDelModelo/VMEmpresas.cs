using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace VistaDelModelo
{
    public class VMEmpresas
    {
        #region Propiedades
        DbEmpresas oDbEmpresa;

        private Guid UIDEmpresa;
        public Guid UIDEMPRESA
        {
            get { return UIDEmpresa; }
            set { UIDEmpresa = value; }
        }
        private string StrRazonSocial;
        public string RAZONSOCIAL
        {
            get { return StrRazonSocial; }
            set { StrRazonSocial = value; }
        }
        private string StrNombreComercial;
        public string NOMBRECOMERCIAL
        {
            get { return StrNombreComercial; }
            set { StrNombreComercial = value; }
        }
        private string StrRFC;
        public string RFC
        {
            get { return StrRFC; }
            set { StrRFC = value; }
        }

        private Suministros sm = new Suministros();
        public Suministros EMPRESA
        {
            get { return sm; }
            set { sm = value; }
        }

        private DataTable dt;
        public DataTable DT
        {
            get { return dt; }
            set { dt = value; }
        }

        public string StrEstatus { get; set; }
        public string StrTipoDeEmpresa { get; set; }
        //Listas
        public List<Estatus> ESTATUS;
        public List<TipoDeEmpresa> TEMPRESA;

        public List<VMEmpresas> LISTADEEMPRESAS;
        public List<VMEmpresas> ListaDeEmpresaSeleccionadas = new List<VMEmpresas>();

        public string StrRuta { get; set; }
        #endregion

        #region Metodos
        #region Listas
        public void TipoDeTelefonos()
        {
            oDbEmpresa = new DbEmpresas();
            foreach (DataRow item in oDbEmpresa.ObtenerTipoDeTelefono().Rows)
            {
                Guid id = new Guid(item["UidTipoDeTelefono"].ToString());
                string nombre = item["Nombre"].ToString();
            }
        }

        public void Estatus()
        {
            ESTATUS = new List<Modelo.Estatus>();
            oDbEmpresa = new DbEmpresas();
            foreach (DataRow item in oDbEmpresa.ObtenerEstatus().Rows)
            {
                int ID = Convert.ToInt32(item["IdEstatus"].ToString());
                string NOMBRE = item["Nombre"].ToString();
                ESTATUS.Add(new Estatus(ID, NOMBRE));
            }
        }
        public void TipoDeEmpresa()
        {
            TEMPRESA = new List<Modelo.TipoDeEmpresa>();
            oDbEmpresa = new DbEmpresas();
            foreach (DataRow item in oDbEmpresa.ObtenerEmpresasFiltros().Rows)
            {
                int id = Convert.ToInt32(item["IdTipoDeEmpresa"].ToString());
                string Nombre = item["Nombre"].ToString();
                TEMPRESA.Add(new TipoDeEmpresa(id, Nombre));
            }
        }
        public DataTable ObtenerTipoDeEmpresasGestion()
        {
            return oDbEmpresa.ObtenerTipoDeEmpresa();
        }
        #endregion
        public void ObtenerNombreComercial(string UidUsuario, string IdEmpresa = "")
        {

            if (UidUsuario == "" && UidUsuario == string.Empty)
            {
                UidUsuario = Guid.Empty.ToString();
            }
            oDbEmpresa = new DbEmpresas();
            foreach (DataRow item in oDbEmpresa.ObtenerNombreComercial(new Guid(UidUsuario), IdEmpresa).Rows)
            {
                UIDEMPRESA = new Guid(item["UidEmpresa"].ToString());
                NOMBRECOMERCIAL = item["NombreComercial"].ToString();
            }

        }

        public void BuscarEmpresas(Guid UidEmpresa = new Guid(), string RazonSocial = "", string NombreComercial = "", string RFC = "", int tipo = 0, int status = 0)
        {
            DataTable Dt = new DataTable();
            LISTADEEMPRESAS = new List<VMEmpresas>();
            try
            {
                SqlCommand CMD = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "asp_buscarEmpresas"
                };

                if (UidEmpresa != Guid.Empty)
                {
                    CMD.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidEmpresa"].Value = UidEmpresa;

                    oDbEmpresa = new DbEmpresas();
                    DT = oDbEmpresa.Busquedas(CMD);
                    foreach (DataRow item in DT.Rows)
                    {
                        UIDEMPRESA = new Guid(item["UidEmpresa"].ToString());
                        NOMBRECOMERCIAL = item["NombreComercial"].ToString().ToUpper();
                        StrRFC = item["rfc"].ToString().ToUpper();
                        StrEstatus = item["IdEstatus"].ToString().ToUpper();
                        StrTipoDeEmpresa = item["IdTipoDeEmpresa"].ToString();
                        RAZONSOCIAL = item["RazonSocial"].ToString().ToUpper();
                    }
                }
                else
                {
                    if (RFC != string.Empty)
                    {
                        CMD.Parameters.Add("@RFC", SqlDbType.NVarChar, 30);
                        CMD.Parameters["@RFC"].Value = RFC;
                    }
                    if (NombreComercial != string.Empty)
                    {
                        CMD.Parameters.Add("@NombreComercial", SqlDbType.NVarChar, 100);
                        CMD.Parameters["@NombreComercial"].Value = NombreComercial;
                    }
                    if (RazonSocial != string.Empty)
                    {
                        CMD.Parameters.Add("@RazonSocial", SqlDbType.NVarChar, 150);
                        CMD.Parameters["@RazonSocial"].Value = RazonSocial;
                    }
                    if (tipo != 0)
                    {
                        CMD.Parameters.Add("@Tipo", SqlDbType.Int);
                        CMD.Parameters["@Tipo"].Value = tipo;
                    }
                    if (status != 0)
                    {
                        CMD.Parameters.Add("@Estatus", SqlDbType.Int);
                        CMD.Parameters["@Estatus"].Value = status;
                    }
                    oDbEmpresa = new DbEmpresas();
                    DT = oDbEmpresa.Busquedas(CMD);
                    foreach (DataRow item in DT.Rows)
                    {
                        LISTADEEMPRESAS.Add(
                            new VMEmpresas()
                            {
                                UIDEMPRESA = new Guid(item["UidEmpresa"].ToString()),
                                NOMBRECOMERCIAL = item["NombreComercial"].ToString().ToUpper(),
                                RFC = item["rfc"].ToString().ToUpper(),
                                StrEstatus = item["IdEstatus"].ToString().ToUpper(),
                                StrTipoDeEmpresa = item["IdTipoDeEmpresa"].ToString(),
                                RAZONSOCIAL = item["RazonSocial"].ToString().ToUpper()
                            });
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        public bool GuardarEmpresaSuministradora(Guid UidEmpresa, string RazonSocial, string NombreComercial, string Rfc, int Tipo, int Estatus)
        {

            bool resultado = false;
            try
            {
                resultado = sm.GUARDAREMPRESA(new Suministros() { UIDEMPRESA = UidEmpresa, RAZONSOCIAL = RazonSocial, NOMBRECOMERCIAL = NombreComercial, RFC = Rfc, TIPO = new TipoDeEmpresa() { ID = Tipo }, ESTATUS = new Estatus() { ID = Estatus } });
            }
            catch (Exception)
            {

                throw;
            }

            return resultado;

        }

        public bool ActualizarDatos(Guid UidEmpresa, string RazonSocial, string NombreComercial, string Rfc, int Tipo = 0, int Estatus = 0)
        {
            bool resultado = false;
            resultado = sm.ACTUALIZAREMPRESA(new Suministros() { UIDEMPRESA = UidEmpresa, RAZONSOCIAL = RazonSocial, NOMBRECOMERCIAL = NombreComercial, RFC = Rfc, TIPO = new TipoDeEmpresa() { ID = Tipo }, ESTATUS = new Estatus() { ID = Estatus } });
            return resultado;
        }

        public DataView Sort(string sortExpression, string valor)
        {

            DataSet ds = new DataSet();

            PropertyDescriptorCollection properties =
              TypeDescriptor.GetProperties(typeof(Suministros));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in LISTADEEMPRESAS)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            ds.Tables.Add(table);
            DataView dt = null;
            dt = new DataView(ds.Tables[0])
            {
                Sort = sortExpression + " " + valor
            };

            return dt;
        }

        public string TotalDeRegistros()
        {
            string Total = "";
            oDbEmpresa = new DbEmpresas();
            foreach (DataRow item in oDbEmpresa.ObtenerTotalDeRegistros().Rows)
            {
                Total = item["total"].ToString();
            }
            return Total;
        }
        public bool ValidaRfc(string RFC)
        {
            bool resultado = false;
            oDbEmpresa = new DbEmpresas();
            if (oDbEmpresa.ValidarRFC(RFC).Rows.Count <= 0)
            {
                resultado = true;
            }
            return resultado;
        }

        public bool ValidaCorreoElectronico(string correo)
        {
            bool resultado = false;
            oDbEmpresa = new DbEmpresas();
            if (oDbEmpresa.VAlidarCorreoElectronico(correo).Rows.Count <= 0)
            {
                resultado = true;
            }
            return resultado;
        }

        #region Validacion de estatus
        public bool VerificaEstatusEmpresa(string UidEmpresa)
        {
            bool resultado = false;
            oDbEmpresa = new DbEmpresas();
            if (oDbEmpresa.VerificaEmpresaActiva(UidEmpresa).Rows.Count == 1)
            {
                resultado = true;
            }
            return resultado;
        }
        /// <summary>
        /// Recibe como parametro un Guid de la empresa y si devuelve true es que es suministradora
        /// de lo contrario es distribuidora
        /// </summary>
        /// <param name="UidEmpresa"></param>
        /// <returns></returns>
        public bool ObtenerTipoDeEmpresa(string UidEmpresa)
        {
            bool resultado = false;
            oDbEmpresa = new DbEmpresas();
            //Si entra  empresa suministradora
            if (oDbEmpresa.ObtenElTipoDeEmpresa(UidEmpresa).Rows.Count == 1)
            {
                resultado = true;
            }
            return resultado;
        }

        public VMEmpresas ObtenerEmpresaDeLista(string valor)
        {
            return LISTADEEMPRESAS.Find(Emp => Emp.UIDEMPRESA.ToString() == valor);
        }

        public void ObtenerEmpresaUsuario(string UidUsuario)
        {
            Guid Uidempresa = new Guid();
            oDbEmpresa = new DbEmpresas();
            foreach (DataRow item in oDbEmpresa.ObtenerUidEmpresaUsuario(UidUsuario).Rows)
            {
                Uidempresa = new Guid(item["UidEmpresa"].ToString());
            }
            BuscarEmpresas(UidEmpresa: Uidempresa);

        }






        #endregion


        #region Busquedas de FrontEnd cliente

        public void BuscarEmpresaBusquedaCliente(string StrParametroBusqueda, string StrDia, Guid UidDireccion, Guid UidBusquedaCategorias, string StrNombreEmpresa = "")
        {
            DataTable Dt = new DataTable();
            LISTADEEMPRESAS = new List<VMEmpresas>();
            try
            {
                SqlCommand CMD = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "asp_BusquedaDeEmpresasDelCliete"
                };

                CMD.Parameters.Add("@strParametroBusqueda", SqlDbType.VarChar, 100);
                CMD.Parameters["@strParametroBusqueda"].Value = StrParametroBusqueda;


                if (!string.IsNullOrEmpty(StrNombreEmpresa))
                {
                    CMD.Parameters.Add("@StrNombreProducto", SqlDbType.VarChar, 200);
                    CMD.Parameters["@StrNombreProducto"].Value = StrNombreEmpresa;
                }


                CMD.Parameters.Add("@StrDia", SqlDbType.VarChar, 20);
                CMD.Parameters["@StrDia"].Value = StrDia;

                CMD.Parameters.Add("@UidDireccion", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@UidDireccion"].Value = UidDireccion;

                CMD.Parameters.Add("@UidBusquedaCategorias", SqlDbType.UniqueIdentifier);
                CMD.Parameters["@UidBusquedaCategorias"].Value = UidBusquedaCategorias;


                oDbEmpresa = new DbEmpresas();
                DT = oDbEmpresa.Busquedas(CMD);
                LISTADEEMPRESAS.Clear();

                foreach (DataRow item in oDbEmpresa.Busquedas(CMD).Rows)
                {

                    if (!LISTADEEMPRESAS.Exists(e => e.UIDEMPRESA == new Guid(item["UidEmpresa"].ToString())))
                    {
                        LISTADEEMPRESAS.Add(new VMEmpresas()
                        {
                            UIDEMPRESA = new Guid(item["UidEmpresa"].ToString()),
                            NOMBRECOMERCIAL = item["NombreComercial"].ToString(),
                            StrRuta = item["NVchRuta"].ToString()
                        });
                    }

                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        #endregion

        #endregion
    }
}
