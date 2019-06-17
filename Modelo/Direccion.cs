using System;
using DBControl;
using System.Data;
using System.Data.SqlClient;
using Modelo.DatosDireccion;

namespace Modelo
{
    public class Direccion
    {
        #region Propiedades
        Conexion oConexion;
        private Guid UidDireccion;
        public Guid ID
        {
            get { return UidDireccion; }
            set { UidDireccion = value; }
        }

        public Pais PAIS;
        public Estado ESTADO;
        public Municipio MUNICIPIO;
        public Colonia COLONIA;
        public Ciudad CIUDAD;
        private string StrCalle0;

        public string CALLE0
        {
            get { return StrCalle0; }
            set { StrCalle0 = value; }
        }
        private string StrCalle1;
        public string CALLE1
        {
            get { return StrCalle1; }
            set { StrCalle1 = value; }
        }
        private string StrCalle2;
        public string CALLE2
        {
            get { return StrCalle2; }
            set { StrCalle2 = value; }
        }
        private string StrManzana;
        public string MANZANA
        {
            get { return StrManzana; }
            set { StrManzana = value; }
        }
        private string StrLote;
        public string LOTE
        {
            get { return StrLote; }
            set { StrLote = value; }
        }
        private string StrCodigoPostal;
        public string CodigoPostal
        {
            get { return StrCodigoPostal; }
            set { StrCodigoPostal = value; }
        }
        private string StrReferencia;
        public string REFERENCIA
        {
            get { return StrReferencia; }
            set { StrReferencia = value; }
        }
        private string _nombreColonia;
        public string NOMBRECOLONIA
        {
            get { return _nombreColonia; }
            set { _nombreColonia = value; }
        }
        private string _nombreCiudad;
        public string NOMBRECIUDAD
        {
            get { return _nombreCiudad; }
            set { _nombreCiudad = value; }
        }
        private string _identificador;
        public string IDENTIFICADOR
        {
            get { return _identificador; }
            set { _identificador = value; }
        }



        #endregion


        #region Metodos
        public bool GuardaDireccion(string Procedure, Direccion DIR, Guid IdUsuario = new Guid())
        {
            SqlCommand cmd = new SqlCommand();
            bool resultado = false;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Procedure;

                //Datos de Direccion
                if (IdUsuario != Guid.Empty)
                {
                    cmd.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@UidUsuario"].Value = IdUsuario;
                }
                cmd.Parameters.Add("@IdDireccion", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@IdDireccion"].Value = DIR.ID;
                //Dato2
                cmd.Parameters.Add("@UidPais", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidPais"].Value = DIR.PAIS.ID;
                //Dato3
                cmd.Parameters.Add("@UidEstado", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidEstado"].Value = DIR.ESTADO.IDESTADO;
                //Dato4
                cmd.Parameters.Add("@UidMunicipio", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidMunicipio"].Value = DIR.MUNICIPIO.IDMUNICIPIO;
                //Dato5
                cmd.Parameters.Add("@Colonia", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@Colonia"].Value = DIR.COLONIA.UID;
                //Dato6
                cmd.Parameters.Add("@Calle1", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Calle1"].Value = DIR.CALLE1;
                //Dato7
                cmd.Parameters.Add("@Calle2", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Calle2"].Value = DIR.CALLE2;
                //Dato8
                cmd.Parameters.Add("@Manzana", SqlDbType.NVarChar, 4);
                cmd.Parameters["@Manzana"].Value = DIR.MANZANA;
                //Dato9
                cmd.Parameters.Add("@Lote", SqlDbType.NVarChar, 4);
                cmd.Parameters["@Lote"].Value = DIR.LOTE;
                //Dato10
                cmd.Parameters.Add("@CodigoPostal", SqlDbType.NVarChar, 8);
                cmd.Parameters["@CodigoPostal"].Value = DIR.CodigoPostal;
                //Dato11
                cmd.Parameters.Add("@Referencia", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Referencia"].Value = DIR.REFERENCIA;
                //Dato 12
                cmd.Parameters.Add("@Ciudad", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@Ciudad"].Value = DIR.CIUDAD.ID;
                //Dato 13
                cmd.Parameters.Add("@Calle0", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Calle0"].Value = DIR.CALLE0;
                //Dato 14
                cmd.Parameters.Add("@Identificador", SqlDbType.NVarChar, 20);
                cmd.Parameters["@Identificador"].Value = DIR.IDENTIFICADOR;
                oConexion = new Conexion();
                resultado = oConexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool ActualizaDireccion(string Tabla, Direccion DIR)
        {
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Tabla;
                //Dato1
                cmd.Parameters.Add("@IdDireccion", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@IdDireccion"].Value = DIR.ID;
                //Dato2
                cmd.Parameters.Add("@Pais", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@Pais"].Value = DIR.PAIS.ID;
                //Dato3
                cmd.Parameters.Add("@IdEstado", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@IdEstado"].Value = DIR.ESTADO.IDESTADO;
                //Dato4
                cmd.Parameters.Add("@IdMunicipio", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@IdMunicipio"].Value = DIR.MUNICIPIO.IDMUNICIPIO;
                //Dato5
                cmd.Parameters.Add("@Ciudad", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@Ciudad"].Value = DIR.CIUDAD.ID;
                //Dato6
                cmd.Parameters.Add("@Calle1", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Calle1"].Value = DIR.CALLE1;
                //Dato7
                cmd.Parameters.Add("@Manzana", SqlDbType.NVarChar, 4);
                cmd.Parameters["@Manzana"].Value = DIR.MANZANA;
                //Dato8
                cmd.Parameters.Add("@Lote", SqlDbType.NVarChar, 4);
                cmd.Parameters["@Lote"].Value = DIR.LOTE;
                //Dato9
                cmd.Parameters.Add("@CodigoPostal", SqlDbType.NVarChar, 8);
                cmd.Parameters["@CodigoPostal"].Value = DIR.CodigoPostal;
                //Dato10
                cmd.Parameters.Add("@Referencia", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Referencia"].Value = DIR.REFERENCIA;
                //Dato 11
                cmd.Parameters.Add("@Calle2", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Calle2"].Value = DIR.CALLE2;
                //Dato 12
                cmd.Parameters.Add("@Colonia", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@Colonia"].Value = DIR.COLONIA.UID;
                //Dato 13
                cmd.Parameters.Add("@Calle0", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Calle0"].Value = DIR.CALLE0;
                //Dato 14
                cmd.Parameters.Add("@Identificador", SqlDbType.NVarChar, 20);
                cmd.Parameters["@Identificador"].Value = DIR.IDENTIFICADOR;
                oConexion = new Conexion();
                resultado = oConexion.ModificarDatos(cmd);
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
