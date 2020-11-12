using DBControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public abstract class Empresa
    {
        #region Propiedades
        Conexion cn;
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
        
        //Propiedades Foraneas
        public Direccion DCN;
        public Telefono PHONE;
        public CorreoElectronico MAIL;
        public Estatus ESTATUS;
        public TipoDeEmpresa TIPO;
        public List<Direccion> LISTADEDIRECCIONES;

        private Comision _oComision;

        public Comision oComision
        {
            get { return _oComision; }
            set { _oComision = value; }
        }



        #endregion
        #region Contructores
        public Empresa()
        { }

        public Empresa(Guid ID, string RazonSocial, string NombreComercial, string Rfc, int Tipo, int Estatus, Guid IdTelefono, string Numero, Guid IdMail, string Mail, Guid IdDireccion, Guid Pais, Guid Estado, Guid Municipio, Guid Ciudad, Guid Colonia, string Calle0, string Calle1, string Calle2, string Manzana, string Lote, string CP, string Referencia, string Identificador)
        {
            UIDEMPRESA = ID;
            RAZONSOCIAL = RazonSocial;
            NOMBRECOMERCIAL = NombreComercial;
            RFC = Rfc;
            TIPO = new TipoDeEmpresa(Tipo);
            ESTATUS = new Estatus(Estatus);
            PHONE = new Telefono() { ID = IdTelefono, NUMERO = Numero };

            DCN = new Direccion() { ID = IdDireccion, PAIS = new DatosDireccion.Pais() { ID = Pais }, ESTADO = new DatosDireccion.Estado() { IDESTADO = Estado }, MUNICIPIO = new DatosDireccion.Municipio() { IDMUNICIPIO = Municipio }, CIUDAD = new DatosDireccion.Ciudad() { ID = Ciudad }, COLONIA = new DatosDireccion.Colonia() { UID = Colonia }, CALLE0 = Calle0, CALLE1 = Calle1, CALLE2 = Calle2, MANZANA = Manzana, LOTE = Lote, CodigoPostal = CP, REFERENCIA = Referencia, IDENTIFICADOR = Identificador };
        }
        #endregion
        #region Metodos
        public virtual bool GUARDAREMPRESA(Empresa EMPRESA)
        {
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_AgregarEmpresa";
                //Dato1
                cmd.Parameters.Add("@IdEmpresa", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@IdEmpresa"].Value = EMPRESA.UIDEMPRESA;
                //Dato2
                cmd.Parameters.Add("@RazonSocial", SqlDbType.NVarChar, 150);
                cmd.Parameters["@RazonSocial"].Value = EMPRESA.RAZONSOCIAL;
                //Dato3
                cmd.Parameters.Add("@NombreComercial", SqlDbType.NVarChar, 100);
                cmd.Parameters["@NombreComercial"].Value = EMPRESA.NOMBRECOMERCIAL;
                //Dato4
                cmd.Parameters.Add("@Rfc", SqlDbType.NVarChar, 30);
                cmd.Parameters["@Rfc"].Value = EMPRESA.RFC;
                //Dato5
                cmd.Parameters.Add("@Estatus", SqlDbType.Int);
                cmd.Parameters["@Estatus"].Value = EMPRESA.ESTATUS.ID;
                //Dato6
                cmd.Parameters.Add("@TipoDeEmpresa", SqlDbType.Int);
                cmd.Parameters["@TipoDeEmpresa"].Value = EMPRESA.TIPO.ID;

                //cmd.Parameters.Add("@UidTipoDeComision", SqlDbType.UniqueIdentifier);
                //cmd.Parameters["@UidTipoDeComision"].Value = EMPRESA.oComision.UidTipoDeComision;

                cmd.Parameters.Add("@ABAbsorbe", SqlDbType.Bit);
                cmd.Parameters["@ABAbsorbe"].Value = Convert.ToByte(EMPRESA.oComision.BAbsorveComision);

                cmd.Parameters.Add("@IncluyeComisionTarjeta", SqlDbType.Bit);
                cmd.Parameters["@IncluyeComisionTarjeta"].Value = Convert.ToByte(EMPRESA.oComision.BIncluyeComisionTarjeta);

                cn = new Conexion();
                //Mandar comando a ejecución
                resultado = cn.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;

        }
        public virtual bool ACTUALIZAREMPRESA(Empresa EMP, string TipoDeACtualizacion)
        {
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_actualizarDatosEmpresa";
                //Dato1
                cmd.Parameters.Add("@IdEmpresa", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@IdEmpresa"].Value = EMP.UIDEMPRESA;
                //Dato2
                cmd.Parameters.Add("@RazonSocial", SqlDbType.NVarChar, 150);
                cmd.Parameters["@RazonSocial"].Value = EMP.RAZONSOCIAL;
                //Dato3
                cmd.Parameters.Add("@NombreComercial", SqlDbType.NVarChar, 100);
                cmd.Parameters["@NombreComercial"].Value = EMP.NOMBRECOMERCIAL;
                //Dato4
                cmd.Parameters.Add("@Rfc", SqlDbType.NVarChar, 30);
                cmd.Parameters["@Rfc"].Value = EMP.RFC;
                //Dato5
                if (EMP.ESTATUS.ID != 0)
                {
                    cmd.Parameters.Add("@Estatus", SqlDbType.Int);
                    cmd.Parameters["@Estatus"].Value = EMP.ESTATUS.ID;
                }

                if (EMP.TIPO.ID != 0)
                {
                    cmd.Parameters.Add("@TipoDeEmpresa", SqlDbType.Int);
                    cmd.Parameters["@TipoDeEmpresa"].Value = EMP.TIPO.ID;
                }
                if (TipoDeACtualizacion == "BackSite")
                {
                    
                    cmd.Parameters.Add("@ABAbsorbe", SqlDbType.Bit);
                    cmd.Parameters["@ABAbsorbe"].Value = Convert.ToByte(EMP.oComision.BAbsorveComision); ;

                    cmd.Parameters.Add("@IncluyeComisionTarjeta", SqlDbType.Bit);
                    cmd.Parameters["@IncluyeComisionTarjeta"].Value = Convert.ToByte(EMP.oComision.BIncluyeComisionTarjeta);
                }


                cn = new Conexion();
                //Mandar comando a ejecución
                resultado = cn.ModificarDatos(cmd);
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
