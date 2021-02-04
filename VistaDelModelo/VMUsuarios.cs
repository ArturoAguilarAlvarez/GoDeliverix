using DBControl;
using Modelo;
using Modelo.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace VistaDelModelo
{
    public class VMUsuarios
    {

        #region Propiedades Usuario
        private Guid _uidUsuario;

        public Guid Uid
        {
            get { return _uidUsuario; }
            set { _uidUsuario = value; }
        }

        private Guid _uidTurnoRepartidor;

        public Guid uidTurnoRepartidor
        {
            get { return _uidTurnoRepartidor; }
            set { _uidTurnoRepartidor = value; }
        }

        private string strUsuario;

        public string StrUsuario
        {
            get { return strUsuario; }
            set { strUsuario = value; }
        }

        private string strPassword;

        public string StrCotrasena
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        private string _nombre;

        public string StrNombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _apellllidoPaterno;

        public string StrApellidoPaterno
        {
            get { return _apellllidoPaterno; }
            set { _apellllidoPaterno = value; }
        }

        private string _apellllidoMaterno;

        public string StrApellidoMaterno
        {
            get { return _apellllidoMaterno; }
            set { _apellllidoMaterno = value; }
        }



        private string _fechaDeNacimiento;

        public string DtmFechaDeNacimiento
        {
            get { return _fechaDeNacimiento; }
            set { _fechaDeNacimiento = value; }
        }
        private string _StrNombreDeSucursal;

        public string StrNombreDeSucursal
        {
            get { return _StrNombreDeSucursal; }
            set { _StrNombreDeSucursal = value; }
        }
        private string _strNombrePerfil;

        public string StrPerfil
        {
            get { return _strNombrePerfil; }
            set { _strNombrePerfil = value; }
        }



        private string _StrEstatus;

        public string StrEstatus
        {
            get { return _StrEstatus; }
            set { _StrEstatus = value; }
        }

        public Guid UidEmpresa { get; set; }
        #endregion
        #region Propiedades
        VMTelefono MVTelefono = new VMTelefono();

        DbUsuarios oDbusuarios = new DbUsuarios();
        Usuarios oUsuario = new Usuarios();
        private Telefono _t;

        public Telefono T
        {
            get { return _t; }
            set { _t = value; }
        }
        private Suministros _sm;

        public Suministros EMPRESA
        {
            get { return _sm; }
            set { _sm = value; }
        }
        public string Sucursal { get; set; }
        public string NombreEmpresa { get; private set; }

        private VMUsuarios _user;
        public VMUsuarios USUARIO
        {
            get { return _user; }
            set { _user = value; }
        }


        private List<Perfiles> _Perfil;
        public List<Perfiles> Perfil
        {
            get { return _Perfil; }
            set { _Perfil = value; }
        }
        public bool Seleccion { get; set; }
        private List<VMUsuarios> _ListaDeUsuarios;

        public List<VMUsuarios> LISTADEUSUARIOS
        {
            get { return _ListaDeUsuarios; }
            set { _ListaDeUsuarios = value; }
        }

        #region Repartidor
        private decimal _mEfectivoEnMano;

        public decimal MEfectivoEnMano
        {
            get { return _mEfectivoEnMano; }
            set { _mEfectivoEnMano = value; }
        }
        private decimal _MFondoRepartidor;

        public decimal MFondoRepartidor
        {
            get { return _MFondoRepartidor; }
            set { _MFondoRepartidor = value; }
        }

        #endregion

        #endregion

        #region Contructores
        public VMUsuarios()
        {

        }
        #endregion

        #region Metodos

        public string ObtenerFolio(string uidUsuario)
        {
            oDbusuarios = new DbUsuarios();
            return oDbusuarios.ObtenerFolioUsuario(uidUsuario);
        }
        public void CargaPerfilesDeUsuario(string perfil)
        {
            Perfil = new List<Perfiles>();
            foreach (DataRow item in oDbusuarios.Perfiles(perfil).Rows)
            {
                Guid id = new Guid(item["UidPerfil"].ToString());
                string Nombre = item["Nombre"].ToString();
                Perfil.Add(new Perfiles(id, Nombre));
            }
        }
        public DataView Sort(string sortExpression, string valor)
        {

            DataSet ds = new DataSet();
            PropertyDescriptorCollection properties =
             TypeDescriptor.GetProperties(typeof(Usuarios));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in LISTADEUSUARIOS)
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

        public void obtenerDatosDeSupervisor(Guid uidUsuario)
        {
            foreach (DataRow item in oDbusuarios.RecuperarSupervisor(uidUsuario).Rows)
            {
                Uid = new Guid(item["UidUsuario"].ToString());
                StrNombre = item["Usuario"].ToString();
                Sucursal = item["Identificador"].ToString();
                NombreEmpresa = item["nombrecomercial"].ToString();
            }
        }


        #region Datos de GridView
        public void DatosGridViewBusquedaNormal(string PERFIL, string UidEmpresa = "")
        {
            LISTADEUSUARIOS = new List<VMUsuarios>();

            switch (PERFIL)
            {
                //Administradores
                case "76a96ff6-e720-4092-a217-a77a58a9bf0d":
                    foreach (DataRow item in oDbusuarios.ObtenerUsuarioSimpleBusquedaNormal(PERFIL, UidEmpresa).Rows)
                    {
                        Guid Id = new Guid(item["UidUsuario"].ToString());
                        string Nombre = item["Nombre"].ToString().ToUpper();
                        string APELLIDOS = item["ApellidoPaterno"].ToString().ToUpper() + " " + item["ApellidoMaterno"].ToString().ToUpper();
                        string usuario = item["usuario"].ToString().ToUpper();
                        string perfil = item["Perfil"].ToString().ToUpper();
                        string Empresa = item["Empresa"].ToString().ToUpper();
                        string estatus = item["ESTATUS"].ToString().ToUpper();
                        LISTADEUSUARIOS.Add(new VMUsuarios() { Uid = Id, StrNombre = Nombre, StrApellidoPaterno = APELLIDOS, StrUsuario = usuario, StrNombreDeSucursal = Empresa, StrEstatus = estatus, StrPerfil = perfil });
                    }
                    break;
                //Supervisores
                case "81232596-4c6b-4568-9005-8d4a0a382fda":
                    foreach (DataRow item in oDbusuarios.ObtenerUsuarioSimpleBusquedaNormal(PERFIL, UidEmpresa).Rows)
                    {
                        Guid Id = new Guid(item["UidUsuario"].ToString());
                        string Nombre = item["Nombre"].ToString().ToUpper();
                        string APELLIDOS = item["ApellidoPaterno"].ToString().ToUpper() + " " + item["ApellidoMaterno"].ToString().ToUpper();
                        string empresa = item["Empresa"].ToString();
                        string perfil = item["Perfil"].ToString().ToUpper();
                        string estatus = item["ESTATUS"].ToString().ToUpper();
                        LISTADEUSUARIOS.Add(new VMUsuarios() { Uid = Id, StrNombre = Nombre, StrApellidoPaterno = APELLIDOS, StrNombreDeSucursal = empresa, StrEstatus = estatus });

                    }
                    break;
                //Repartidores
                case "DFC29662-0259-4F6F-90EA-B24E39BE4346":
                    foreach (DataRow item in oDbusuarios.ObtenerUsuarioSimpleBusquedaNormal(PERFIL, UidEmpresa).Rows)
                    {
                        Guid Id = new Guid(item["UidUsuario"].ToString());
                        string Nombre = item["Nombre"].ToString().ToUpper();
                        string APELLIDOS = item["ApellidoPaterno"].ToString().ToUpper() + " " + item["ApellidoMaterno"].ToString().ToUpper();
                        string Identificador = item["Identificador"].ToString().ToUpper();
                        string perfil = item["Perfil"].ToString().ToUpper();
                        string estatus = item["ESTATUS"].ToString().ToUpper();
                        string empresa = item["Empresa"].ToString();
                        LISTADEUSUARIOS.Add(new VMUsuarios() { Uid = Id, StrNombre = Nombre, StrNombreDeSucursal = empresa, StrApellidoPaterno = APELLIDOS });

                    }
                    break;
                default:
                    break;
            }
        }

        public bool ValidaExistenciaDeAdministracidor(string licencia, Guid uidusuario)
        {
            oDbusuarios = new DbUsuarios();
            bool resultado = false;
            if (oDbusuarios.ValidaExistenciaDeAdministrador(licencia, uidusuario).Rows.Count == 1)
            {
                resultado = true;
            }
            return resultado;
        }

        public void DatosGridViewBusquedaAmpliada(string PERFIL)
        {
            LISTADEUSUARIOS = new List<VMUsuarios>();
            //Administradores
            if (PERFIL == "76a96ff6-e720-4092-a217-a77a58a9bf0d")
            {
                foreach (DataRow item in oDbusuarios.ObtenerUsuarioSimpleBusquedaNormal(PERFIL).Rows)
                {
                    Guid Id = new Guid(item["UidUsuario"].ToString());
                    string Nombre = item["Nombre"].ToString().ToUpper();
                    string APELLIDOS = item["ApellidoPaterno"].ToString().ToUpper() + " " + item["ApellidoMaterno"].ToString().ToUpper();
                    string usuario = item["usuario"].ToString().ToUpper();
                    string perfil = item["Perfil"].ToString().ToUpper();
                    string Empresa = item["Empresa"].ToString().ToUpper();
                    string estatus = item["ESTATUS"].ToString().ToUpper();
                    LISTADEUSUARIOS.Add(new VMUsuarios() { Uid = Id, StrNombre = Nombre, StrApellidoPaterno = APELLIDOS, StrUsuario = usuario });

                }
            }
            //Supervisores
            if (PERFIL == "81232596-4c6b-4568-9005-8d4a0a382fda")
            {
                foreach (DataRow item in oDbusuarios.ObtenerUsuarioSimpleBusquedaNormal(PERFIL).Rows)
                {
                    Guid Id = new Guid(item["UidUsuario"].ToString());
                    string Nombre = item["Nombre"].ToString().ToUpper();
                    string APELLIDOS = item["ApellidoPaterno"].ToString().ToUpper() + " " + item["ApellidoMaterno"].ToString().ToUpper();
                    string Identificador = item["Identificador"].ToString().ToUpper();
                    string perfil = item["Perfil"].ToString().ToUpper();
                    string estatus = item["ESTATUS"].ToString().ToUpper();
                    LISTADEUSUARIOS.Add(new VMUsuarios() { Uid = Id, StrNombre = Nombre, StrApellidoPaterno = APELLIDOS });
                }
            }
        }
        #endregion



        #region Busquedas
        public void obtenerUsuario(string ID)
        {
            DataTable dt = oDbusuarios.ObtenerUsuario(ID);

            foreach (DataRow item in dt.Rows)
            {
                Uid = new Guid(item["UidUsuario"].ToString());
                StrNombre = item["Nombre"].ToString();
                StrApellidoPaterno = item["ApellidoPaterno"].ToString();
                StrApellidoMaterno = item["ApellidoMaterno"].ToString();
                StrUsuario = item["usuario"].ToString();
                StrCotrasena = item["contrasena"].ToString();
                DtmFechaDeNacimiento = item["FechaDeNacimiento"].ToString();
                StrPerfil = item["UidPerfil"].ToString();
                StrNombreDeSucursal = item["UidEmpresa"].ToString();
                StrEstatus = item["ESTATUS"].ToString();
            }
        }
        /// <summary>
        /// Valida la existencia del repartidor en la sucursal a logearse
        /// </summary>
        /// <param name="licencia"></param>
        /// <param name="uidusuario"></param>
        /// <returns></returns>
        public bool ValidaExistenciaDeRepartidor(string licencia, Guid uidusuario)
        {
            bool resultado = false;
            oDbusuarios = new DbUsuarios();
            if (oDbusuarios.ValidaRepartidorSucursal(licencia, uidusuario).Rows.Count == 1)
            {
                resultado = true;
            }
            return resultado;
        }

        /// <summary>
        /// Busqueda de usuarios a partir de diferentes criterios
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <param name="UidEmpresa"></param>
        /// <param name="NOMBRE"></param>
        /// <param name="USER"></param>
        /// <param name="APELLIDO"></param>
        /// <param name="ESTATUS"></param>
        /// <param name="UIDPERFIL"></param>
        public void BusquedaDeUsuario(Guid UidUsuario = new Guid(), Guid UidEmpresa = new Guid(), string NOMBRE = "", string USER = "", string APELLIDO = "", string ESTATUS = "", Guid UIDPERFIL = new Guid())
        {
            LISTADEUSUARIOS = new List<VMUsuarios>();
            DataTable Dt = new DataTable();
            try
            {
                SqlCommand CMD = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "asp_BuscarUsuarios"
                };
                if (UidEmpresa != Guid.Empty)
                {
                    CMD.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidEmpresa"].Value = UidEmpresa;
                }
                if (NOMBRE != string.Empty)
                {
                    CMD.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100);
                    CMD.Parameters["@Nombre"].Value = NOMBRE;
                }
                if (USER != string.Empty)
                {
                    CMD.Parameters.Add("@Usuario", SqlDbType.NVarChar, 100);
                    CMD.Parameters["@Usuario"].Value = USER;
                }
                if (APELLIDO != string.Empty)
                {
                    CMD.Parameters.Add("@Apellido", SqlDbType.NVarChar, 100);
                    CMD.Parameters["@Apellido"].Value = APELLIDO;
                }
                if (ESTATUS != "0" && !string.IsNullOrEmpty(ESTATUS))
                {
                    CMD.Parameters.Add("@Estatus", SqlDbType.NVarChar, 2);
                    CMD.Parameters["@Estatus"].Value = ESTATUS;
                }
                if (UIDPERFIL != Guid.Empty)
                {
                    CMD.Parameters.Add("@Perfil", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@Perfil"].Value = UIDPERFIL;
                }

                if (UidUsuario == Guid.Empty)
                {
                    Dt = oDbusuarios.Busquedas(CMD);
                    foreach (DataRow item in Dt.Rows)
                    {
                        Guid uidusuario = new Guid(item["UidUsuario"].ToString());
                        string Nombre = item["Nombre"].ToString().ToUpper();
                        string Apellido = item["ApellidoPaterno"].ToString().ToUpper() + " " + item["ApellidoMaterno"].ToString().ToUpper();
                        string Usuario = item["usuario"].ToString().ToUpper();
                        string FechaDeNacimiento = item["FechaDeNacimiento"].ToString();

                        if (Dt.Columns.Contains("UidEmpresa") == true)
                        {
                            string empresa = item["Empresa"].ToString();
                            LISTADEUSUARIOS.Add(new VMUsuarios()
                            {
                                Uid = uidusuario,
                                StrNombre = Nombre,
                                StrUsuario = Usuario,
                                StrApellidoPaterno = Apellido,
                                DtmFechaDeNacimiento = FechaDeNacimiento,
                                UidEmpresa = new Guid(item["UidEmpresa"].ToString()),
                                StrEstatus = item["ESTATUS"].ToString(),
                                StrPerfil = item["Perfil"].ToString().ToUpper(),

                                StrNombreDeSucursal = empresa

                            });
                        }
                        else
                        {
                            LISTADEUSUARIOS.Add(new VMUsuarios()
                            {
                                Uid = uidusuario,
                                StrNombre = Nombre,
                                StrUsuario = Usuario,
                                StrApellidoPaterno = Apellido,
                                DtmFechaDeNacimiento = FechaDeNacimiento,
                                StrEstatus = item["ESTATUS"].ToString(),
                                StrPerfil = item["Perfil"].ToString().ToUpper(),
                                StrCotrasena = item["Contrasena"].ToString()

                        });
                        }

                    }
                }
                else if (UidUsuario != Guid.Empty)
                {
                    CMD.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidUsuario"].Value = UidUsuario;

                    Dt = oDbusuarios.Busquedas(CMD);
                    foreach (DataRow item in Dt.Rows)
                    {
                        this.Uid = new Guid(item["UidUsuario"].ToString());
                        this.StrNombre = item["Nombre"].ToString().ToUpper();
                        this.StrCotrasena = item["Contrasena"].ToString().ToUpper();
                        this.StrUsuario = item["usuario"].ToString().ToUpper();
                        this.StrApellidoPaterno = item["ApellidoPaterno"].ToString().ToUpper();
                        this.StrApellidoMaterno = item["ApellidoMaterno"].ToString().ToUpper();
                        this.DtmFechaDeNacimiento = item["FechaDeNacimiento"].ToString();
                        this.StrEstatus = item["ESTATUS"].ToString();
                        this.StrPerfil = item["Perfil"].ToString().ToUpper();
                        this.StrCotrasena = item["Contrasena"].ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ObtenerHoraActual(string UidEstado)
        {
            LISTADEUSUARIOS = new List<VMUsuarios>();
            DataTable Dt = new DataTable();
            string Hora = "";
            try
            {
                SqlCommand CMD = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "asp_ObtenerHoraActual"
                };
                if (!string.IsNullOrEmpty(UidEstado))
                {
                    CMD.Parameters.Add("@UidEstado", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidEstado"].Value = new Guid(UidEstado);
                }

                Dt = oDbusuarios.Busquedas(CMD);
                foreach (DataRow item in Dt.Rows)
                {
                    Hora = item["HoraActual"].ToString();
                }
                return Hora;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidaCorreoElectronicoDeLaEmpresa(string correo)
        {
            bool resultado = false;
            if (oDbusuarios.VAlidarCorreoElectronicoUsuario(correo).Rows.Count <= 0 && oDbusuarios.ValidarCorreoDeEmpresa(correo).Rows.Count <= 0)
            {
                resultado = true;
            }
            return resultado;
        }
        public bool ValidarCorreoElectronicoDelUsuario(string correo)
        {
            bool resultado = false;
            if (oDbusuarios.VAlidarCorreoElectronicoUsuario(correo).Rows.Count <= 0)
            {
                resultado = true;
            }
            return resultado;
        }
        public bool validarExistenciaDeUsuario(string StrNombreDeUsuario)
        {
            bool resultado = false;
            oDbusuarios = new DbUsuarios();
            if (oDbusuarios.ValidarexistenciaDeUsuario(StrNombreDeUsuario).Rows.Count <= 0)
            {
                resultado = true;
            }
            return resultado;
        }
        public Guid ObtenerIdEmpresa(string id)
        {
            Guid Uid = new Guid();
            foreach (DataRow item in oDbusuarios.obtenerUidEmpresa(id).Rows)
            {
                Uid = new Guid(item["Uidempresa"].ToString());
            }
            return Uid;
        }
        #endregion

        /// <summary>
        /// Este metodo sirve para agregar un usuario en cualquier parte de la jerarquia, 
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <param name="Nombre"></param>
        /// <param name="ApellidoPaterno"></param>
        /// <param name="ApellidoMaterno"></param>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <param name="fnacimiento"></param>
        /// <param name="perfil"></param>
        /// <param name="estatus"></param>
        /// <param name="TIPODEUSUARIO"></param>
        /// <param name="IdEmpresa">Parametro opcional para clientes, obligario en administradores y usuarios del sistema</param>
        /// <param name="Sucursal">Parametro opcional unicamente para usuarios pertenecientes a una empresa</param>
        /// <returns></returns>
        public bool GuardaUsuario(Guid UidUsuario, string Nombre, string ApellidoPaterno, string usuario, string password, string fnacimiento, string perfil, string estatus, string TIPODEUSUARIO, Guid IdEmpresa = new Guid(), Guid Sucursal = new Guid(), string ApellidoMaterno = "")
        {

            Guid UidCorreo = Guid.NewGuid();

            Usuarios persona = new Usuarios()
            {
                ID = UidUsuario,
                NOMBRE = Nombre,
                APELLIDOPATERNO = ApellidoPaterno,
                APELLIDOMATERNO = ApellidoMaterno,
                USUARIO = usuario,
                PASSWORD = password,
                FECHADENACIMIENTO = fnacimiento,
                perfil = new Perfiles() { ID = new Guid(perfil) },
                ESTATUS = new Estatus() { ID = Convert.ToInt32(estatus) },
                EMPRESA = new Suministros() { UIDEMPRESA = IdEmpresa }
            };
            bool resultado = false;
            try
            {
                if (Sucursal != Guid.Empty)
                {
                    resultado = oUsuario.GuardaUsuario(persona, TIPODEUSUARIO, Sucursal);
                }
                else
                {
                    resultado = oUsuario.GuardaUsuario(persona, TIPODEUSUARIO);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public bool ActualizarUsuario(Guid UidUsuario, string Nombre = "", string ApellidoPaterno = "", string ApellidoMaterno = "", string usuario = "", string password = "", string fnacimiento = "", string perfil = "", string estatus = "0", Guid IdEmpresa = new Guid(), Guid SUCURSAL = new Guid())
        {
            Usuarios persona = new Usuarios()
            {
                ID = UidUsuario,
                APELLIDOPATERNO = ApellidoPaterno,
                APELLIDOMATERNO = ApellidoMaterno,
                NOMBRE = Nombre,
                USUARIO = usuario,
                PASSWORD = password,
                FECHADENACIMIENTO = fnacimiento,
                perfil = new Perfiles() { ID = new Guid(perfil) },
                EMPRESA = new Suministros() { UIDEMPRESA = IdEmpresa },
                ESTATUS = new Estatus()
                {
                    ID = Convert.ToInt32(estatus),
                }
            };
            return oUsuario.ActualizaUsuario(persona, SUCURSAL);
        }


        public bool RelacionRepartidorSucursal(Guid UidSucursal)
        {
            bool resultado = false;
            try
            {
                oUsuario = new Usuarios
                {
                    ID = Uid,
                    oSucursal = new Sucursal() { ID = UidSucursal }
                };
                resultado = oUsuario.AgragaUsuarioASucursal();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public void EliminarRelacionSucursal(Guid iDUsuario)
        {
            oDbusuarios.EliminaRelacionSucursal(iDUsuario);
        }


        #region Windows Presentation Foundation
        public void RepartidoresEnSucursal(string licencia)
        {
            LISTADEUSUARIOS = new List<VMUsuarios>();
            foreach (DataRow item in oDbusuarios.ObtenRepartidores(licencia).Rows)
            {
                VMUsuarios usuario = new VMUsuarios()
                {
                    Uid = new Guid(item["UidUsuario"].ToString()),
                    StrNombre = item["Nombre"].ToString(),
                    StrApellidoPaterno = item["ApellidoPaterno"].ToString(),
                    StrApellidoMaterno = item["ApellidoMaterno"].ToString(),
                    StrUsuario = item["usuario"].ToString(),
                };
                LISTADEUSUARIOS.Add(usuario);
            }
        }

        public void EliminarRepartidorDeLista(Guid UidUsuario)
        {
            if (LISTADEUSUARIOS.Exists(U => U.Uid == UidUsuario))
            {
                var objeto = LISTADEUSUARIOS.Find(U => U.Uid == UidUsuario);
                LISTADEUSUARIOS.Remove(objeto);
            }
        }


        public void ObtenerRepartidoresDisponibles(string licencia)
        {
            LISTADEUSUARIOS = new List<VMUsuarios>();
            foreach (DataRow item in oDbusuarios.ObtenerRepartidoresConVehiculosYTurnoAbierto(licencia).Rows)
            {
                //Varifica que este activo el campo
                if (item["estatus"].ToString().ToUpper() == "A298B40F-C495-4BD8-A357-4A3209FBC162")
                {
                    if (item["EstatusTurno"].ToString().ToUpper() == "81494F49-F416-4431-99F4-E0AA4CF7E9F6" || item["EstatusTurno"].ToString().ToUpper() == "38FA16DF-4727-41FD-A03E-E2E43FA78F3F" || item["EstatusTurno"].ToString().ToUpper() == "CCAFB7D6-A27C-4F5B-A4A6-13D35138471F")
                    {
                        if (string.IsNullOrEmpty(item["DtmHoraFin"].ToString()))
                        {
                            VMUsuarios usuario = new VMUsuarios()
                            {
                                Uid = new Guid(item["UidUsuario"].ToString()),
                                uidTurnoRepartidor = new Guid(item["UidTurnoRepartidor"].ToString()),
                                StrNombre = item["Nombre"].ToString(),
                                StrUsuario = item["usuario"].ToString(),
                                MEfectivoEnMano = decimal.Parse(decimal.Parse(item["Efectivo"].ToString()).ToString("N2")),
                                MFondoRepartidor = decimal.Parse(decimal.Parse(item["Fondo"].ToString()).ToString("N2"))
                            };
                            if (!LISTADEUSUARIOS.Exists(u => u.Uid == Uid))
                            {
                                LISTADEUSUARIOS.Add(usuario);
                            }
                        }
                    }
                }
            }
        }

        public void SeleccionarUsuario(Guid uid)
        {
            var objeto = new VMUsuarios();
            if (LISTADEUSUARIOS.Exists(u => u.Seleccion == true))
            {
                //objeto = LISTADEUSUARIOS.Find(u => u.Seleccion == true);
                //objeto.Seleccion = false;
                objeto = LISTADEUSUARIOS.Find(U => U.Uid == uid);
                objeto.Seleccion = false;
            }
        }

        #endregion

        #endregion
    }
}
