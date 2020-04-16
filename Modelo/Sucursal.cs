using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DBControl;
using Modelo.Usuario;
using System.Data.SqlClient;

namespace Modelo
{
    public class Sucursal
    {
        #region Propiedades
        //Variables

        Conexion Conexion;
        private Guid _uidSucursal;

        public Guid ID
        {
            get { return _uidSucursal; }
            set { _uidSucursal = value; }
        }

        private string _identificador;

        public string IDENTIFICADOR
        {
            get { return _identificador; }
            set { _identificador = value; }
        }
        private string _HoraApertura;

        public string HORAAPARTURA
        {
            get { return _HoraApertura; }
            set { _HoraApertura = value; }
        }
        private string _HoraCierre;

        public string HORACIERRE
        {
            get { return _HoraCierre; }
            set { _HoraCierre = value; }
        }
        public bool BVisibilidad { get; set; }
        public string StrCodigo { get; set; }

        private decimal _mFondo;

        public decimal MFondo
        {
            get { return _mFondo; }
            set { _mFondo = value; }
        }

        //Composiciones
        public Suministros EMPRESA;
        public Direccion DIRECCION;
        public Usuarios USUARIO;
        public Estatus Estatus;

        #endregion

        #region Metodos
        public virtual bool GUARDARSUCURSAL(Sucursal SUCURSAL)
        {
            Conexion = new Conexion();
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_AgregaSucursal";

                cmd.Parameters.Add("@Empresa", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@Empresa"].Value = SUCURSAL.EMPRESA.UIDEMPRESA;

                cmd.Parameters.Add("@Sucursal", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@Sucursal"].Value = SUCURSAL.ID;

                cmd.Parameters.Add("@Direccion", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@Direccion"].Value = SUCURSAL.DIRECCION.ID;

                cmd.Parameters.Add("@Identificador", SqlDbType.NVarChar, 50);
                cmd.Parameters["@Identificador"].Value = SUCURSAL.IDENTIFICADOR;

                cmd.Parameters.Add("@HoraApertura", SqlDbType.NVarChar, 20);
                cmd.Parameters["@HoraApertura"].Value = SUCURSAL.HORAAPARTURA;

                cmd.Parameters.Add("@HoraCierre", SqlDbType.NVarChar, 20);
                cmd.Parameters["@HoraCierre"].Value = SUCURSAL.HORACIERRE;

                cmd.Parameters.Add("@BVisibilidadInformacion", SqlDbType.Bit);
                cmd.Parameters["@BVisibilidadInformacion"].Value = SUCURSAL.BVisibilidad;

                cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 40);
                cmd.Parameters["@Codigo"].Value = SUCURSAL.StrCodigo;

                cmd.Parameters.Add("@MFondo", SqlDbType.Money);
                cmd.Parameters["@MFondo"].Value = SUCURSAL.MFondo;

                if (SUCURSAL.Estatus.ID != 0)
                {
                    cmd.Parameters.Add("@IntEstatus", SqlDbType.Int);
                    cmd.Parameters["@IntEstatus"].Value = SUCURSAL.Estatus.ID;
                }
                resultado = Conexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public virtual bool ACTUALIZASUCURSAL(Sucursal SUCURSAL)
        {
            Conexion = new Conexion();
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_ActualizaSucursal";

                cmd.Parameters.Add("@Sucursal", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@Sucursal"].Value = SUCURSAL.ID;

                cmd.Parameters.Add("@Identificador", SqlDbType.NVarChar, 50);
                cmd.Parameters["@Identificador"].Value = SUCURSAL.IDENTIFICADOR;

                cmd.Parameters.Add("@HoraApertura", SqlDbType.NVarChar, 20);
                cmd.Parameters["@HoraApertura"].Value = SUCURSAL.HORAAPARTURA;

                cmd.Parameters.Add("@HoraCierre", SqlDbType.NVarChar, 20);
                cmd.Parameters["@HoraCierre"].Value = SUCURSAL.HORACIERRE;

                cmd.Parameters.Add("@IntEstatus", SqlDbType.Int);
                cmd.Parameters["@IntEstatus"].Value = SUCURSAL.Estatus.ID;

                cmd.Parameters.Add("@BVisibilidadInformacion", SqlDbType.Bit);
                cmd.Parameters["@BVisibilidadInformacion"].Value = SUCURSAL.BVisibilidad;

                cmd.Parameters.Add("@MFondo", SqlDbType.Money);
                cmd.Parameters["@MFondo"].Value = SUCURSAL.MFondo;

                cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 40);
                cmd.Parameters["@Codigo"].Value = SUCURSAL.StrCodigo;

                resultado = Conexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;

        }
        public virtual bool RELACIONGIRO(Guid UidGiro, Guid UidSucursal)
        {
            Conexion = new Conexion();
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_AgregaSucursalGiro";

                cmd.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidSucursal"].Value = UidSucursal;

                cmd.Parameters.Add("@UidGiro", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidGiro"].Value = UidGiro;

                resultado = Conexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public virtual bool RELACIONCATEGORIA(Guid uidcategoria, Guid UidSucursal)
        {
            Conexion = new Conexion();
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_AgregaSucursalCategoria";

                cmd.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidSucursal"].Value = UidSucursal;

                cmd.Parameters.Add("@UidCategoria", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidCategoria"].Value = uidcategoria;

                resultado = Conexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public virtual bool RELACIONSUBCATEGORIA(Guid uidsubategoria, Guid UidSucursal)
        {
            Conexion = new Conexion();
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_AgregaSucursalSubcategoria";

                cmd.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidSucursal"].Value = UidSucursal;

                cmd.Parameters.Add("@UidSubcategoria", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidSubcategoria"].Value = uidsubategoria;

                resultado = Conexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public bool AgregaZonaDeServicio(Guid UidRelacion, Guid uidsucursal, Guid uidcolonia, string strParametro)
        {
            Conexion = new Conexion();
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_GuardaZonaDeServicio";

                cmd.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidSucursal"].Value = uidsucursal;

                cmd.Parameters.Add("@UidRelacion", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidRelacion"].Value = UidRelacion;

                cmd.Parameters.Add("@Parametro", SqlDbType.VarChar, 10);
                cmd.Parameters["@Parametro"].Value = strParametro;

                cmd.Parameters.Add("@UidColonia", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidColonia"].Value = uidcolonia;

                resultado = Conexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public bool GuardarRepartidor(Guid uidUsuario, Guid uidVehiculo, Guid Uid)
        {
            Conexion = new Conexion();
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_GuardarUsuarioVehiculo";

                cmd.Parameters.Add("@UidRegistro", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidRegistro"].Value = Uid;

                cmd.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidUsuario"].Value = uidUsuario;

                cmd.Parameters.Add("@UidVehiculo", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidVehiculo"].Value = uidVehiculo;

                resultado = Conexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public void AsingaOrdenRepartidor(Guid UidTurnoRepartidor, Guid UidSucursal, Guid UidOrdenRepartidor)
        {
            Conexion = new Conexion();
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_AsignarOrdenRepartidor";

                cmd.Parameters.Add("@UidTurnoRepartidor", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidTurnoRepartidor"].Value = UidTurnoRepartidor;

                cmd.Parameters.Add("@UidOrden", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidOrden"].Value = UidSucursal;

                cmd.Parameters.Add("@UidOrdenRepartidor", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidOrdenRepartidor"].Value = UidOrdenRepartidor;

                resultado = Conexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {
                throw;
            }
        }





        #endregion

    }
}
