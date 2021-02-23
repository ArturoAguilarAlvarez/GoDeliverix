using DBControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Modelo.Usuario
{
    public class Usuarios
    {
        #region Propiedades

        Conexion oConexion;
        private Guid idUser;

        public Guid ID
        {
            get { return idUser; }
            set { idUser = value; }
        }

        private string strUsuario;

        public string USUARIO
        {
            get { return strUsuario; }
            set { strUsuario = value; }
        }

        private string strPassword;

        public string PASSWORD
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        private string _nombre;

        public string NOMBRE
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellllidoPaterno;

        public string APELLIDOPATERNO
        {
            get { return _apellllidoPaterno; }
            set { _apellllidoPaterno = value; }
        }

        private string _apellllidoMaterno;

        public string APELLIDOMATERNO
        {
            get { return _apellllidoMaterno; }
            set { _apellllidoMaterno = value; }
        }

        private string _fechaDeNacimiento;

        public string FECHADENACIMIENTO
        {
            get { return _fechaDeNacimiento; }
            set { _fechaDeNacimiento = value; }
        }


        private string _Nombre;

        public string NOMBREDELAEMPRESA
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private string _NombreEstatus;

        public string NOMBREESTATUS
        {
            get { return _NombreEstatus; }
            set { _NombreEstatus = value; }
        }

        private string _NombreDePerfil;

        public string NOMBREDEPERFIL
        {
            get { return _NombreDePerfil; }
            set { _NombreDePerfil = value; }
        }

        public Perfiles perfil;
        public Estatus ESTATUS;
        public Sucursal oSucursal = new Sucursal();
        public Suministros EMPRESA;
        public CorreoElectronico CORREO;
        public List<Direccion> ListaDeDirecciones;



        #endregion

        #region Constructores
        public Usuarios()
        {

        }

        public Usuarios(Guid IdUsuario, string Nombre, string ApPaterno, string ApMaterno, string Usuario, string Password, string Fnacimeinto, Guid Perfil, int estatus, Guid idempresa, Guid UidCorreo, string Correo)
        {
            ID = IdUsuario;
            NOMBRE = Nombre;
            APELLIDOPATERNO = ApPaterno;
            APELLIDOMATERNO = ApMaterno;
            USUARIO = Usuario;
            PASSWORD = Password;
            EMPRESA = new Suministros() { UIDEMPRESA = idempresa };
            FECHADENACIMIENTO = Fnacimeinto;
            perfil = new Perfiles(Perfil);
            ESTATUS = new Estatus(estatus);
        }
        public Usuarios(string Usuario, string password)
        {

            USUARIO = Usuario;
            PASSWORD = password;
        }

        public Usuarios(string Nombre, string ApPaterno, string ApMaterno, string Usuario, string Password, string Fnacimeinto, Guid Perfil, int estatus, Guid idempresa)
        {
            NOMBRE = Nombre;
            APELLIDOPATERNO = ApPaterno;
            APELLIDOMATERNO = ApMaterno;
            USUARIO = Usuario;
            PASSWORD = Password;
            EMPRESA = new Suministros() { UIDEMPRESA = idempresa };
            FECHADENACIMIENTO = Fnacimeinto;
            perfil = new Perfiles(Perfil);
            ESTATUS = new Estatus(estatus);
        }
        #endregion

        #region Metodos

        public bool GuardaUsuario(Usuarios user, string Parametro, Guid Sucursal = new Guid())
        {
            oConexion = new Conexion();
            bool Resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_AgregaUsuario";

                cmd.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidUsuario"].Value = user.ID;

                cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Nombre"].Value = user.NOMBRE;

                cmd.Parameters.Add("@ApellidoPaterno", SqlDbType.NVarChar, 100);
                cmd.Parameters["@ApellidoPaterno"].Value = user.APELLIDOPATERNO;

                cmd.Parameters.Add("@APellidoMaterno", SqlDbType.NVarChar, 100);
                cmd.Parameters["@APellidoMaterno"].Value = user.APELLIDOMATERNO;

                cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 200);
                cmd.Parameters["@Usuario"].Value = user.USUARIO;

                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Password"].Value = user.PASSWORD;

                cmd.Parameters.Add("@FechaDeNacimiento", SqlDbType.VarChar, 10);
                cmd.Parameters["@FechaDeNacimiento"].Value = user.FECHADENACIMIENTO;

                if (user.EMPRESA != null)
                {
                    cmd.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@UidEmpresa"].Value = user.EMPRESA.UIDEMPRESA;
                }
                else
                {
                    cmd.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@UidEmpresa"].Value = Guid.Empty;
                }

                cmd.Parameters.Add("@Perfil", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@Perfil"].Value = user.perfil.ID;

                cmd.Parameters.Add("@estatus", SqlDbType.Int);
                cmd.Parameters["@estatus"].Value = user.ESTATUS.ID;

                if (Sucursal != Guid.Empty)
                {
                    cmd.Parameters.Add("@Sucursal", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@Sucursal"].Value = Sucursal;
                }

                cmd.Parameters.Add("@Parametro", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Parametro"].Value = Parametro;

                Resultado = oConexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
            return Resultado;
        }

        public bool ActualizaUsuario(Usuarios user, Guid Sucursal = new Guid())
        {
            oConexion = new Conexion();
            bool Resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_ActualizarUsuario";

                cmd.Parameters.Add("@IdUsuario", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@IdUsuario"].Value = user.ID;

                if (!string.IsNullOrEmpty(user.PASSWORD))
                {
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100);
                    cmd.Parameters["@Password"].Value = user.PASSWORD;
                }


                cmd.Parameters.Add("@Perfil", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@Perfil"].Value = user.perfil.ID;

                if (!string.IsNullOrEmpty(user.NOMBRE))
                {
                    cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100);
                    cmd.Parameters["@Nombre"].Value = user.NOMBRE;
                }

                if (!string.IsNullOrEmpty(user.APELLIDOPATERNO))
                {
                    cmd.Parameters.Add("@ApellidoPaterno", SqlDbType.NVarChar, 100);
                    cmd.Parameters["@ApellidoPaterno"].Value = user.APELLIDOPATERNO;
                }
                if (!string.IsNullOrEmpty(user.APELLIDOMATERNO))
                {
                    cmd.Parameters.Add("@APellidoMaterno", SqlDbType.NVarChar, 100);
                    cmd.Parameters["@APellidoMaterno"].Value = user.APELLIDOMATERNO;
                }
                if (!string.IsNullOrEmpty(user.USUARIO))
                {
                    cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 100);
                    cmd.Parameters["@Usuario"].Value = user.USUARIO;
                }


                if (!string.IsNullOrEmpty(user.FECHADENACIMIENTO))
                {
                    cmd.Parameters.Add("@FechaDeNacimiento", SqlDbType.NVarChar, 100);
                    cmd.Parameters["@FechaDeNacimiento"].Value = user.FECHADENACIMIENTO;
                }

                if (user.EMPRESA.UIDEMPRESA != Guid.Empty)
                {
                    cmd.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@UidEmpresa"].Value = user.EMPRESA.UIDEMPRESA;
                }
                if (user.ESTATUS.ID != 0)
                {
                    cmd.Parameters.Add("@estatus", SqlDbType.Int);
                    cmd.Parameters["@estatus"].Value = user.ESTATUS.ID;
                }


                if (Sucursal != Guid.Empty)
                {
                    cmd.Parameters.Add("@Sucursal", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@Sucursal"].Value = Sucursal;
                }

                Resultado = oConexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
            return Resultado;
        }

        public bool AgragaUsuarioASucursal()
        {
            oConexion = new Conexion();
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaEmpleadoSucursal";

                Comando.Parameters.Add("@UidEmpleado", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidEmpleado"].Value = ID;

                Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSucursal"].Value = oSucursal.ID;

                resultado = oConexion.ModificarDatos(Comando);
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
