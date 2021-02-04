using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VistaDelModelo
{
    public class VMTelefono
    {
        #region Propiedades
        Conexion oConexion;
        DbTelefono oDbTelefono;
        private Guid UidTelefono;
        public bool Estado { get; set; }
        public Guid ID
        {
            get { return UidTelefono; }
            set { UidTelefono = value; }
        }

        private string _UidLada;

        public string UidLada
        {
            get { return _UidLada; }
            set { _UidLada = value; }
        }
        private string _StrLada;

        public string StrLada
        {
            get { return _StrLada; }
            set { _StrLada = value; }
        }

        private string StrNumero;
        public string NUMERO
        {
            get { return StrNumero; }
            set { StrNumero = value; }
        }
        private Guid _Tipo;
        private string _CodigoDePais;

        public string CodigoDePais
        {
            get { return _CodigoDePais; }
            set { _CodigoDePais = value; }
        }

        public Guid UidTipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        public string StrNombreTipoDeTelefono { get; set; }

        public TipoDeTelefono TIPO;
        private List<VMTelefono> _ListaDeTelefonos;

        public List<VMTelefono> ListaDeTelefonos
        {
            get { return _ListaDeTelefonos; }
            set { _ListaDeTelefonos = value; }
        }

        public List<VMTelefono> ListaDeTelefonosInformacion = new List<VMTelefono>();
        private List<VMTelefono> _ListaDeTipoDeTelefono;

        public List<VMTelefono> ListaDeTipoDeTelefono
        {
            get { return _ListaDeTipoDeTelefono; }
            set { _ListaDeTipoDeTelefono = value; }
        }
        private List<VMTelefono> _ListaDeLadasInternacionales;

        public List<VMTelefono> ListaDeLadasInternacionales
        {
            get { return _ListaDeLadasInternacionales; }
            set { _ListaDeLadasInternacionales = value; }
        }

        #endregion

        DbTelefono MVTelefono = new DbTelefono();

        public VMTelefono()
        {
            ListaDeTelefonos = new List<VMTelefono>();
        }
        public string ObtenerTipoDeTelefono(string UidTelefono)
        {
            string nombre = string.Empty;
            foreach (DataRow item in MVTelefono.ObtenerElTipoTelefono(UidTelefono).Rows)
            {
                nombre = item["Nombre"].ToString();
            }
            if (nombre == string.Empty)
            {
                nombre = "No hay informacion para mostrar";
            }
            return nombre;
        }

        /// <summary>
        /// Agrega un telefono a la lista de telefonos
        /// </summary>
        /// <param name="IdTipoDeTelefono"></param>
        /// <param name="Tipo"></param>
        /// <param name="Numero"></param>
        /// <param name="NombreTipoDeTelefono"></param>
        public void AgregaTelefonoALista(string IdTipoDeTelefono, string Numero, string NombreTipoDeTelefono)
        {
            Guid UidTelefono = Guid.NewGuid();
            if (!ListaDeTelefonos.Exists(tel => tel.ID == UidTelefono))
            {
                ListaDeTelefonos.Add(new VMTelefono() { ID = UidTelefono, StrNombreTipoDeTelefono = NombreTipoDeTelefono, UidTipo = new Guid(IdTipoDeTelefono), NUMERO = Numero });
            }
        }


        /// <summary>
        /// Elimina un telefono de la lista
        /// </summary>
        /// <param name="UidTelefono"></param>
        public void QuitaTelefonoDeLista(string UidTelefono)
        {
            var T = new VMTelefono();
            T = ListaDeTelefonos.Find(Tel => Tel.ID.ToString() == UidTelefono);
            ListaDeTelefonos.Remove(T);
        }



        /// <summary>
        /// Guarda el telefono a la sucursal por el identificador para guardar la lista
        /// </summary>
        /// <param name="id"></param>
        public void GuardaTelefonoSucursal(Guid id)
        {
            var T = new Telefono();
            foreach (var item in ListaDeTelefonos)
            {
                var Telefono = new Telefono
                {
                    ID = item.ID,
                    NUMERO = item.NUMERO,
                    Tipo = item.UidTipo.ToString()
                };
                Telefono.GuardaTelefono(id, "Sucursal");
            }
        }
        /// <summary>
        /// Agrega el telefono a la empresa por el identificador para guardar la lista
        /// </summary>
        /// <param name="id"></param>
        public void GuardaTelefonoEmpresa(Guid id)
        {
            foreach (var item in ListaDeTelefonos)
            {
                var Telefono = new Telefono
                {
                    ID = item.ID,
                    NUMERO = item.NUMERO,
                    Tipo = item.UidTipo.ToString()
                };
                Telefono.GuardaTelefono(id, "Empresa");
            }
        }
        /// <summary>
        /// Guarda los telefonos
        /// </summary>
        /// <param name="uidUsuario">Definido por el duenio del telefono</param>
        public void GuardaTelefono(Guid uidUsuario, string Parametro)
        {
            foreach (var item in ListaDeTelefonos)
            {
                var Telefono = new Telefono
                {
                    ID = item.ID,
                    NUMERO = item.NUMERO,
                    Tipo = item.UidTipo.ToString()
                };
                Telefono.GuardaTelefono(uidUsuario, Parametro);
            }
        }

        /// <summary>
        /// Este metodo solo es para agregar registros independientes en la base de datos desde la web api
        /// </summary>
        /// <param name="uidUsuario"></param>
        /// <param name="Parametro"></param>
        /// <param name="UidTelefono"></param>
        /// <param name="Numero"></param>
        /// <param name="UidTipoDeTelefono"></param>
        public void GuardaTelefonoWepApi(Guid uidUsuario, string Parametro, Guid UidTelefono, string Numero, string UidTipoDeTelefono, string uidlada)
        {
            var Telefono = new Telefono
            {
                ID = UidTelefono,
                NUMERO = Numero,
                Tipo = UidTipoDeTelefono,
                UidLada = uidlada
            };
            Telefono.GuardaTelefono(uidUsuario, Parametro);
        }
        /// <summary>
        /// Actualiza un registro de telefono
        /// </summary>
        /// <param name="UidTelefono"></param>
        /// <param name="Numero"></param>
        /// <param name="UidTipoDeTelefono"></param>
        public void ActualizaTelefonoWepApi(Guid UidTelefono, string Numero, string UidTipoDeTelefono, string UidLada = "")
        {
            var Telefono = new Telefono();
            if (!string.IsNullOrEmpty(UidLada))
            {
                Telefono = new Telefono
                {
                    ID = UidTelefono,
                    NUMERO = Numero,
                    Tipo = UidTipoDeTelefono,
                    UidLada = UidLada
                };
            }
            else
            {
                Telefono = new Telefono
                {
                    ID = UidTelefono,
                    NUMERO = Numero,
                    Tipo = UidTipoDeTelefono
                };
            }
            
            Telefono.Actualiza();
        }

        /// <summary>
        /// Verifica que lista de direcciones tiene que mostrar, si solo informacion o gestion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipo"></param>
        public void ObtenerTelefonosSucursal(string id, string tipo)
        {
            ListaDeTelefonos = new List<VMTelefono>();
            oDbTelefono = new DbTelefono();
            foreach (DataRow item in oDbTelefono.ObtenerTelefonosSucursal(id).Rows)
            {
                Guid uidTelfono = new Guid(item["UidTelefono"].ToString());
                Guid IdTipo = new Guid(item["UidTipoDeTelefono"].ToString());
                string Numero = item["Numero"].ToString();
                if (tipo == "Informacion")
                {
                    ListaDeTelefonosInformacion.Add(new VMTelefono() { ID = uidTelfono, StrNombreTipoDeTelefono = ObtenerTipoDeTelefono(uidTelfono.ToString()), UidTipo = IdTipo, NUMERO = Numero });
                }
                if (tipo == "gestion")
                {
                    ListaDeTelefonos.Add(new VMTelefono() { ID = uidTelfono, StrNombreTipoDeTelefono = ObtenerTipoDeTelefono(uidTelfono.ToString()), UidTipo = IdTipo, NUMERO = Numero });
                }
            }
        }
        public void ObtenerTelefonoEmpresa(string id, string tipo)
        {
            ListaDeTelefonos = new List<VMTelefono>();
            oDbTelefono = new DbTelefono();
            foreach (DataRow item in oDbTelefono.ObtenerTelefonosEmpresa(id).Rows)
            {
                Guid uidTelfono = new Guid(item["UidTelefono"].ToString());
                Guid IdTipo = new Guid(item["UidTipoDeTelefono"].ToString());
                string Numero = item["Numero"].ToString();
                if (tipo == "Informacion")
                {
                    ListaDeTelefonos.Add(new VMTelefono() { ID = uidTelfono, StrNombreTipoDeTelefono = ObtenerTipoDeTelefono(uidTelfono.ToString()), UidTipo = IdTipo, NUMERO = Numero });
                }
                if (tipo == "gestion")
                {
                    ListaDeTelefonos.Add(new VMTelefono() { ID = uidTelfono, StrNombreTipoDeTelefono = ObtenerTipoDeTelefono(uidTelfono.ToString()), UidTipo = IdTipo, NUMERO = Numero });
                }
            }
        }

        public void BuscarTelefonos(Guid UidPropietario = new Guid(), string ParadetroDeBusqueda = "", Guid UidTelefono = new Guid(), string strTelefono = "")
        {
            SqlCommand Comando = new SqlCommand();

            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ObtenerTelefonos";

                Comando.Parameters.Add("@UidPropietario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidPropietario"].Value = UidPropietario;

                Comando.Parameters.Add("@ParametroDeBusqueda", SqlDbType.VarChar, 30);
                Comando.Parameters["@ParametroDeBusqueda"].Value = ParadetroDeBusqueda;

                if (UidTelefono != null)
                {
                    if (UidTelefono != Guid.Empty)
                    {
                        Comando.Parameters.Add("@UidTelefono", SqlDbType.UniqueIdentifier);
                        Comando.Parameters["@UidTelefono"].Value = UidTelefono;
                    }
                    if (!string.IsNullOrEmpty(strTelefono))
                    {
                        Comando.Parameters.Add("@vchTelefono", SqlDbType.NVarChar, 200);
                        Comando.Parameters["@vchTelefono"].Value = strTelefono;
                    }
                    oConexion = new Conexion();
                    ListaDeTelefonos = new List<VMTelefono>();
                    foreach (DataRow item in oConexion.Busquedas(Comando).Rows)
                    {
                        string tipo = string.Empty;
                        string lada = string.Empty;
                        if (item["TipoDeTelefono"] != null)
                        {
                            tipo = item["TipoDeTelefono"].ToString();
                        }
                        if (item["UidLada"] != null)
                        {
                            lada = item["UidLada"].ToString();
                        }
                        ListaDeTelefonos.Add(
                            new VMTelefono()
                            {
                                ID = new Guid(item["UidTelefono"].ToString()),
                                UidTipo = new Guid(item["UidTipoDetelefono"].ToString()),
                                NUMERO = item["Numero"].ToString(),
                                StrNombreTipoDeTelefono = tipo,
                                Estado = false,
                                UidLada = lada
                            }
                            );
                    }
                }
                else
                {
                    oConexion = new Conexion();
                    foreach (DataRow item in oConexion.Busquedas(Comando).Rows)
                    {
                        ID = new Guid(item["UidTelefono"].ToString());
                        UidTipo = new Guid(item["UidTipoDetelefono"].ToString());
                        NUMERO = item["Numero"].ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ObtenTelefono(string id)
        {
            var T = new VMTelefono();
            T = ListaDeTelefonos.Find(Tel => Tel.ID.ToString() == id);
            ID = T.ID;
            UidTipo = T.UidTipo;
            NUMERO = T.NUMERO;
        }
        /// <summary>
        /// Elimina los telefonos asociados a la empresa
        /// </summary>
        /// <param name="id"></param>
        public void EliminaTelefonoEmpresa(string id)
        {
            oDbTelefono = new DbTelefono();
            oDbTelefono.EliminaTelefonoEmpresa(id);
        }
        public void EliminaTelefonoSucursal(string id)
        {
            oDbTelefono = new DbTelefono();
            oDbTelefono.EliminaTelefonoSucursal(id);
        }
        public bool ActualizaRegistroEnListaDeTelefonos(string id, string IdTipoDeTelefono, string Numero)
        {
            bool resultado = false;
            var T = new VMTelefono();
            try
            {
                T = ListaDeTelefonos.Find(telefono => telefono.ID.ToString() == id);
                T.ID = new Guid(id);
                T.UidTipo = new Guid(IdTipoDeTelefono);
                T.NUMERO = Numero;
                resultado = true;
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public void TipoDeTelefonos()
        {
            ListaDeTipoDeTelefono = new List<VMTelefono>();
            oDbTelefono = new DbTelefono();
            foreach (DataRow item in oDbTelefono.ObtenerTipoDeTelefono().Rows)
            {
                Guid id = new Guid(item["UidTipoDeTelefono"].ToString());
                string nombre = item["Nombre"].ToString();
                ListaDeTipoDeTelefono.Add(new VMTelefono() { UidTipo = id, StrNombreTipoDeTelefono = nombre });
            }

        }
        public void ReadAllLadasInternational()
        {
            ListaDeLadasInternacionales = new List<VMTelefono>();
            oDbTelefono = new DbTelefono();
            foreach (DataRow item in oDbTelefono.ObtenerLadas().Rows)
            {
                string uidlada = item["UidLada"].ToString();
                Guid uidpais = new Guid(item["UidPais"].ToString());
                string nombre = item["VchTerminacion"].ToString();
                string codigodepais = item["CodigoPais"].ToString();
                ListaDeLadasInternacionales.Add(new VMTelefono() { UidLada = uidlada, StrLada = codigodepais + " " + nombre });
            }
        }




        public void ObtenerTelefonoPrincipalDelCliente(string UidCliente)
        {
            oDbTelefono = new DbTelefono();
            foreach (DataRow item in oDbTelefono.ObtenerTelefonoPrincipalUsuario(UidCliente).Rows)
            {
                NUMERO = item["Numero"].ToString();
            }
        }

        public void EliminaTelefonosUsuario(Guid uidUsuario)
        {
            oDbTelefono = new DbTelefono();
            oDbTelefono.EliminaTelefonosUsuario(uidUsuario.ToString());
        }
        public void EliminaTelefonoUsuario(string UidTelefono)
        {
            oDbTelefono = new DbTelefono();
            oDbTelefono.EliminarTelefonoUsuario(UidTelefono);
        }
    }
}
