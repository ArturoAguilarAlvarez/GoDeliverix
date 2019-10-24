using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DBControl;
using Modelo;
using Modelo.DatosDireccion;

namespace VistaDelModelo
{
    public class VMDireccion
    {
        DbDireccion oDbDireccion;
        Conexion oConexion;
        public List<VMDireccion> ListaDIRECCIONES = new List<VMDireccion>();
        public List<VMDireccion> ListaCiudades = new List<VMDireccion>();
        public List<VMDireccion> ListaCiudadesSeleccionadasEntrega = new List<VMDireccion>();
        public List<VMDireccion> ListaColoniasSeleccionadasEntrega = new List<VMDireccion>();
        public List<VMDireccion> ListaCiudadesSeleccionadasRecolecta = new List<VMDireccion>();
        public List<VMDireccion> ListaColoniasSeleccionadasRecolecta = new List<VMDireccion>();
        public List<VMDireccion> ListaDeTarifa = new List<VMDireccion>();
        public List<VMDireccion> ListaDeEstadosSeleccionados = new List<VMDireccion>();
        private Direccion _direccion;

        public string Longitud { get; set; }
        public string Latitud { get; set; }
        public bool Clicked { get; set; }
        public Direccion Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        #region Propiedades
        public Guid UidRegistro { get; set; }
        private Guid UidDireccion;
        public Guid ID
        {
            get { return UidDireccion; }
            set { UidDireccion = value; }
        }
        public Guid UidEstado { get; set; }


        private string _PAIS;
        public string PAIS
        {
            get { return _PAIS; }
            set { _PAIS = value; }
        }
        private string _ESTADO;
        public string ESTADO
        {
            get { return _ESTADO; }
            set { _ESTADO = value; }
        }
        private string _MUNICIPIO;
        public string MUNICIPIO
        {
            get { return _MUNICIPIO; }
            set { _MUNICIPIO = value; }
        }
        private string _COLONIA;
        public string COLONIA
        {
            get { return _COLONIA; }
            set { _COLONIA = value; }
        }
        private string _CIUDAD;
        public string CIUDAD
        {
            get { return _CIUDAD; }
            set { _CIUDAD = value; }
        }
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
        private string _NombrePais;

        public string NombrePais
        {
            get { return _NombrePais; }
            set { _NombrePais = value; }
        }

        private string _NombreEstado;

        public string NombreEstado
        {
            get { return _NombreEstado; }
            set { _NombreEstado = value; }
        }
        private string _NombreMunicipio;

        public string NombreMunicipio
        {
            get { return _NombreMunicipio; }
            set { _NombreMunicipio = value; }
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

        //public VMDireccion()
        //{
        //    //Direccion vacia
        //    if (ListaDIRECCIONES.Count <= 0)
        //    {
        //        ListaDIRECCIONES.Add(new VMDireccion() { ID = Guid.NewGuid(), PAIS = Guid.Empty.ToString(), ESTADO = Guid.Empty.ToString(), MUNICIPIO = Guid.Empty.ToString(), CIUDAD = Guid.Empty.ToString(), COLONIA = Guid.Empty.ToString(), CALLE0 = "No hay información", CALLE1 = "No hay información", CALLE2 = string.Empty, MANZANA = string.Empty, LOTE = string.Empty, CodigoPostal = "No indicado", REFERENCIA = "No hay información", IDENTIFICADOR = "Predeterminada", NOMBRECIUDAD = "No hay información", NOMBRECOLONIA = "No hay información" });
        //    }
        //}

        #region Catalogos
        public DataTable Paises()
        {
            oDbDireccion = new DbDireccion();
            return oDbDireccion.ObtenerPais();
        }
        public DataTable Estados(Guid Pais, string busqueda = "", string Nombre = "")
        {
            oDbDireccion = new DbDireccion();
            return oDbDireccion.ObtenerEstados(Pais, busqueda, Nombre);
        }
        public void BuscarDireccionPorUid(string UidDireccion)
        {
            oDbDireccion = new DbDireccion();
            foreach (DataRow item in oDbDireccion.ObtenerDireccion(new Guid(UidDireccion)).Rows)
            {
                ID = new Guid(item["UidDireccion"].ToString());
                PAIS = item["UidPais"].ToString();
                ESTADO = item["UidEstado"].ToString();
                MUNICIPIO = item["UidMunicipio"].ToString();
                CIUDAD = item["UidCiudad"].ToString();
                COLONIA = item["UidColonia"].ToString();
                CALLE0 = item["Calle0"].ToString();
                CALLE1 = item["Calle1"].ToString();
                CALLE2 = item["Calle2"].ToString();
                MANZANA = item["Manzana"].ToString();
                LOTE = item["Lote"].ToString();
                CodigoPostal = item["CodigoPostal"].ToString();
                REFERENCIA = item["Referencia"].ToString();
                IDENTIFICADOR = item["Identificador"].ToString();
            }
        }
        public DataTable Municipios(Guid Estado)
        {
            oDbDireccion = new DbDireccion();
            return oDbDireccion.ObtenerMunicipio(Estado);
        }
        public DataTable Ciudades(Guid Municipio)
        {
            oDbDireccion = new DbDireccion();
            return oDbDireccion.ObtenerCiudades(Municipio);
        }
        public DataTable Colonias(Guid Ciudad, string ubicacion = "", string Nombre = "")
        {
            oDbDireccion = new DbDireccion();
            DataTable tabla = oDbDireccion.ObtenerColonias(Ciudad, ubicacion, Nombre);

            foreach (DataRow item in tabla.Rows)
            {
                if (new Guid(item["IdColonia"].ToString()) != Guid.Empty)
                {
                    if (!ListaCiudades.Exists(objeto => objeto.ID == new Guid(item["IdColonia"].ToString())))
                    {
                        ListaCiudades.Add(new VMDireccion() { ID = new Guid(item["IdColonia"].ToString()) });
                    }
                }
            }
            return tabla;
        }

        #endregion

        #region Metodos

        #region Busquedas
        /// <summary>
        /// Obtiene la direccion completa con los nombres del pais, estado,municipio,ciudad
        /// </summary>
        /// <param name="UidDireccion"></param>
        public void ObtenerDireccionCompleta(string UidDireccion)
        {
            oDbDireccion = new DbDireccion();
            foreach (DataRow item in oDbDireccion.ObtenerDireccion(new Guid(UidDireccion)).Rows)
            {
                Guid IDDIRECCION = new Guid(item["UidDireccion"].ToString());
                PAIS = ObtenerNombrePais(item["UidPais"].ToString());
                ESTADO = ObtenerNombreDelEstado(item["UidEstado"].ToString());
                MUNICIPIO = ObtenerNombreDelMunicipio(item["UidMunicipio"].ToString());
                CIUDAD = ObtenerNombreDeLaCiudad(item["UidCiudad"].ToString());
                COLONIA = ObtenerNombreDeLaColonia(item["UidColonia"].ToString());
                CALLE0 = item["Calle0"].ToString();
                CALLE1 = item["Calle1"].ToString();
                CALLE2 = item["Calle2"].ToString();
                MANZANA = item["Manzana"].ToString();
                LOTE = item["Lote"].ToString();
                CodigoPostal = item["CodigoPostal"].ToString();
                REFERENCIA = item["Referencia"].ToString();
                IDENTIFICADOR = item["Identificador"].ToString();
            }
        }
        public void QuitaDireeccionDeLista(string id)
        {
            var direccion = new VMDireccion();
            direccion = ListaDIRECCIONES.Find(Dir => Dir.ID.ToString() == id);
            ListaDIRECCIONES.Remove(direccion);
        }
        public void ObtenerDireccionesEmpresa(string id)
        {
            ListaDIRECCIONES = new List<VMDireccion>();
            oDbDireccion = new DbDireccion();
            DataTable DTDirecciones = oDbDireccion.ObtenerDireccionesEmpresa(id);
            foreach (DataRow item in DTDirecciones.Rows)
            {
                //Datos de direccion
                Guid IDDIRECCION = new Guid(item["UidDireccion"].ToString());
                Guid PAIS = new Guid(item["UidPais"].ToString());
                Guid ESTADO = new Guid(item["UidEstado"].ToString());
                Guid MUNICIPIO = new Guid(item["UidMunicipio"].ToString());
                Guid CIUDAD = new Guid(item["UidCiudad"].ToString());
                Guid COLONIA = new Guid(item["UidColonia"].ToString());
                string CALLE0 = item["Calle0"].ToString();
                string CALLE1 = item["Calle1"].ToString();
                string CALLE2 = item["Calle2"].ToString();
                string MANZANA = item["Manzana"].ToString();
                string LOTE = item["Lote"].ToString();
                string CP = item["CodigoPostal"].ToString();
                string Referencia = item["Referencia"].ToString();
                string Identificador = item["Identificador"].ToString();
                // Nombre de campos de ciudad y colonia
                string NombreColonia = "No hay información";
                string NombreCiudad = "No hay información";
                //Validación de variables vacias
                if (Identificador == "" && Identificador == string.Empty)
                {
                    Identificador = "Predeterminada";
                }
                if (CP == "" && CP == string.Empty)
                {
                    CP = "No indicado";
                }
                if (CALLE0 == "" && CALLE0 == string.Empty)
                {
                    CALLE0 = "No hay información";
                }
                if (Referencia == "" && Referencia == string.Empty)
                {
                    Referencia = "No hay información";
                }
                //Obtiene el nombre de la colonia y de la ciudad
                NombreCiudad = ObtenerNombreDeLaCiudad(CIUDAD.ToString());
                NombreColonia = ObtenerNombreDeLaColonia(COLONIA.ToString());
                ListaDIRECCIONES.Add(new VMDireccion() { ID = IDDIRECCION, PAIS = PAIS.ToString(), ESTADO = ESTADO.ToString(), MUNICIPIO = MUNICIPIO.ToString(), CIUDAD = CIUDAD.ToString(), COLONIA = COLONIA.ToString(), CALLE0 = CALLE0, CALLE1 = CALLE1, CALLE2 = CALLE2, MANZANA = MANZANA, LOTE = LOTE, CodigoPostal = CP, REFERENCIA = Referencia, IDENTIFICADOR = Identificador, NOMBRECIUDAD = NombreCiudad, NOMBRECOLONIA = NombreColonia });
            }
        }
        public Guid ObtenerUidColonia(string UidDireccion)
        {
            var objeto = ListaDIRECCIONES.Find(Dir => Dir.ID.ToString() == UidDireccion);
            Guid Uid = Guid.Empty;
            if (objeto != null)
            {
                Uid = new Guid(objeto.COLONIA);
            }
            return Uid;
        }
        public void ObtenerDireccionesUsuario(string id)
        {
            ListaDIRECCIONES = new List<VMDireccion>();
            oDbDireccion = new DbDireccion();
            DataTable DTDirecciones = oDbDireccion.ObtenerDireccionesUsuario(id);
            foreach (DataRow item in DTDirecciones.Rows)
            {
                //Datos de direccion
                Guid IDDIRECCION = new Guid(item["UidDireccion"].ToString());
                Guid PAIS = new Guid(item["UidPais"].ToString());
                Guid ESTADO = new Guid(item["UidEstado"].ToString());
                Guid MUNICIPIO = new Guid(item["UidMunicipio"].ToString());
                Guid CIUDAD = new Guid(item["UidCiudad"].ToString());
                Guid COLONIA = new Guid(item["UidColonia"].ToString());
                string CALLE0 = item["Calle0"].ToString();
                string CALLE1 = item["Calle1"].ToString();
                string CALLE2 = item["Calle2"].ToString();
                string MANZANA = item["Manzana"].ToString();
                string LOTE = item["Lote"].ToString();
                string CP = item["CodigoPostal"].ToString();
                string Referencia = item["Referencia"].ToString();
                string Identificador = item["Identificador"].ToString();
                // Nombre de campos de ciudad y colonia
                string NombreColonia = "No hay información";
                string NombreCiudad = "No hay información";
                //Validación de variables vacias
                if (Identificador == "" && Identificador == string.Empty)
                {
                    Identificador = "Predeterminada";
                }
                if (CP == "" && CP == string.Empty)
                {
                    CP = "No indicado";
                }
                if (CALLE0 == "" && CALLE0 == string.Empty)
                {
                    CALLE0 = "No hay información";
                }
                if (Referencia == "" && Referencia == string.Empty)
                {
                    Referencia = "No hay información";
                }
                //Obtiene el nombre de la colonia y de la ciudad
                NombreCiudad = ObtenerNombreDeLaCiudad(CIUDAD.ToString());
                NombreColonia = ObtenerNombreDeLaColonia(COLONIA.ToString());
                ListaDIRECCIONES.Add(new VMDireccion() { ID = IDDIRECCION, PAIS = PAIS.ToString(), ESTADO = ESTADO.ToString(), MUNICIPIO = MUNICIPIO.ToString(), CIUDAD = CIUDAD.ToString(), COLONIA = COLONIA.ToString(), CALLE0 = CALLE0, CALLE1 = CALLE1, CALLE2 = CALLE2, MANZANA = MANZANA, LOTE = LOTE, CodigoPostal = CP, REFERENCIA = Referencia, IDENTIFICADOR = Identificador, NOMBRECIUDAD = NombreCiudad, NOMBRECOLONIA = NombreColonia });
            }
        }
        public string ObtenerCodigoPostal(Guid Colonia)
        {
            string CP = string.Empty;
            oDbDireccion = new DbDireccion();
            foreach (DataRow item in oDbDireccion.ObtenerCP(Colonia).Rows)
            {
                CP = item["CodigoPostal"].ToString();
            }
            return CP;
        }
        public VMDireccion ObtenDireccion(string id)
        {
            var direccion = new VMDireccion();
            direccion = ListaDIRECCIONES.Find(Dir => Dir.ID.ToString() == id);

            ID = direccion.ID;
            PAIS = direccion.PAIS;
            ESTADO = direccion.ESTADO;
            MUNICIPIO = direccion.MUNICIPIO;
            CIUDAD = direccion.CIUDAD;
            COLONIA = direccion.COLONIA;

            IDENTIFICADOR = direccion.IDENTIFICADOR;
            CALLE0 = direccion.CALLE0;
            CALLE1 = direccion.CALLE1;
            CALLE2 = direccion.CALLE2;
            MANZANA = direccion.MANZANA;
            LOTE = direccion.LOTE;
            CodigoPostal = direccion.CodigoPostal;
            REFERENCIA = direccion.REFERENCIA;

            return direccion;
        }
        public void ObtenerDireccionSucursal(string UidDireccion)
        {
            oDbDireccion = new DbDireccion();
            foreach (DataRow item in oDbDireccion.ObtenerDireccionSucursal(UidDireccion).Rows)
            {
                ID = new Guid(item["UidDireccion"].ToString());
                PAIS = item["UidPais"].ToString();
                ESTADO = item["UidEstado"].ToString();
                MUNICIPIO = item["UidMunicipio"].ToString();
                CIUDAD = item["UidCiudad"].ToString();
                COLONIA = item["UidColonia"].ToString();
                CALLE0 = item["Calle0"].ToString();
                CALLE1 = item["Calle1"].ToString();
                CALLE2 = item["Calle2"].ToString();
                MANZANA = item["Manzana"].ToString();
                LOTE = item["Lote"].ToString();
                CodigoPostal = item["CodigoPostal"].ToString();
                REFERENCIA = item["Referencia"].ToString();
                IDENTIFICADOR = item["Identificador"].ToString();
            }
        }
        public string ObtenerNombreDeLaCiudad(string UidCiudad)
        {
            string NOMBRE = string.Empty;
            oDbDireccion = new DbDireccion();
            foreach (DataRow item in oDbDireccion.obtenerNombreCiudad(UidCiudad).Rows)
            {
                NOMBRE = item["Nombre"].ToString();
            }
            if (NOMBRE == string.Empty)
            {
                NOMBRE = "No hay información";
            }
            return NOMBRE;
        }
        public string ObtenerNombreDeLaColonia(string UidColonia)
        {
            string NOMBRE = string.Empty;
            oDbDireccion = new DbDireccion();
            foreach (DataRow item in oDbDireccion.obtenerNombreColonia(UidColonia).Rows)
            {
                NOMBRE = item["Nombre"].ToString();
            }
            if (NOMBRE == string.Empty)
            {
                NOMBRE = "No hay información";
            }
            return NOMBRE;
        }
        /// <summary>
        /// Obtiene el uid del usuario a travez de su direccion
        /// </summary>
        /// <param name="UidDireccion"></param>
        public string ObtenerUidUsuarioDeUidDireccion(string UidDireccion)
        {
            string resultado = "";
            try
            {
                oDbDireccion = new DbDireccion();
                foreach (DataRow item in oDbDireccion.ObtenerUidDeUsuarioPorUidDireccion(UidDireccion).Rows)
                {
                    resultado = item["UidUsuario"].ToString();
                };
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        #endregion

        #region Listas
        //Zonas de entrega
        public void SeleccionarCiudadEntrega(Guid UidCiudad)
        {
            oDbDireccion = new DbDireccion();
            foreach (DataRow item in oDbDireccion.ObtenerCiudad(UidCiudad).Rows)
            {
                Guid uidciudad = new Guid(item["uidciudad"].ToString());
                string Nombre = item["Nombre"].ToString();

                if (!ListaCiudadesSeleccionadasEntrega.Exists(c => c.ID == UidCiudad) && uidciudad != Guid.Empty && !string.IsNullOrEmpty(Nombre))
                {
                    ListaCiudadesSeleccionadasEntrega.Add(new VMDireccion() { ID = uidciudad, NOMBRECIUDAD = Nombre });
                }
            }
        }
        public void DeseleccionarCiudadEntrega(Guid UidCiudad)
        {
            var Ciudad = ListaCiudadesSeleccionadasEntrega.Find(ciudad => ciudad.ID == UidCiudad);
            ListaCiudadesSeleccionadasEntrega.Remove(Ciudad);
        }
        public void SeleccionarColoniaEntrega(Guid UidReguistro, Guid UidColonia, Guid UidCiudad, string StrNombreColonia)
        {
            if (!ListaColoniasSeleccionadasEntrega.Exists(Col => Col.ID == UidColonia))
            {
                ListaColoniasSeleccionadasEntrega.Add(new VMDireccion() { UidRegistro = UidReguistro, ID = UidColonia, CIUDAD = UidCiudad.ToString(), NOMBRECOLONIA = StrNombreColonia });
            }
        }
        public void DeseleccionarColoniaEntrega(Guid UidColonia = new Guid(), Guid UidCiudad = new Guid())
        {
            if (UidColonia != Guid.Empty)
            {
                var colonia = ListaColoniasSeleccionadasEntrega.Find(Col => Col.ID == UidColonia);
                ListaColoniasSeleccionadasEntrega.Remove(colonia);
            }
            if (UidCiudad != Guid.Empty)
            {
                if (ListaColoniasSeleccionadasEntrega.Exists(Col => Col.CIUDAD == UidCiudad.ToString()))
                {
                    var colonias = ListaColoniasSeleccionadasEntrega.Find(Col => Col.CIUDAD == UidCiudad.ToString() && Col.ID == UidColonia);
                    ListaColoniasSeleccionadasEntrega.Remove(colonias);
                }
            }
        }
        //Zona de recolecta
        public void SeleccionarCiudadRecolecta(Guid UidCiudad)
        {
            oDbDireccion = new DbDireccion();
            foreach (DataRow item in oDbDireccion.ObtenerCiudad(UidCiudad).Rows)
            {
                Guid uidciudad = new Guid(item["uidciudad"].ToString());
                string Nombre = item["Nombre"].ToString();

                if (!ListaCiudadesSeleccionadasRecolecta.Exists(c => c.ID == UidCiudad) && uidciudad != Guid.Empty && !string.IsNullOrEmpty(Nombre))
                {
                    ListaCiudadesSeleccionadasRecolecta.Add(new VMDireccion() { ID = uidciudad, NOMBRECIUDAD = Nombre });
                }
            }
        }
        public void DeseleccionarCiudadRecolecta(Guid UidCiudad)
        {
            var Ciudad = ListaCiudadesSeleccionadasRecolecta.Find(ciudad => ciudad.ID == UidCiudad);
            ListaCiudadesSeleccionadasRecolecta.Remove(Ciudad);
        }
        public void SeleccionarColoniaRecolecta(Guid UidRegistro, Guid UidColonia, Guid UidCiudad, string StrNombreColonia)
        {
            if (!ListaColoniasSeleccionadasRecolecta.Exists(Col => Col.ID == UidColonia))
            {
                ListaColoniasSeleccionadasRecolecta.Add(new VMDireccion() { UidRegistro = UidRegistro, ID = UidColonia, CIUDAD = UidCiudad.ToString(), NOMBRECOLONIA = StrNombreColonia });
            }
        }
        public void DeseleccionarColoniaRecolecta(Guid UidColonia = new Guid(), Guid UidCiudad = new Guid())
        {
            if (UidColonia != Guid.Empty)
            {
                var colonia = ListaColoniasSeleccionadasRecolecta.Find(Col => Col.ID == UidColonia);
                ListaColoniasSeleccionadasRecolecta.Remove(colonia);
            }
            if (UidCiudad != Guid.Empty)
            {
                if (ListaColoniasSeleccionadasRecolecta.Exists(Col => Col.CIUDAD == UidCiudad.ToString()))
                {
                    var colonias = ListaColoniasSeleccionadasRecolecta.Find(Col => Col.CIUDAD == UidCiudad.ToString() && Col.ID == UidColonia);
                    ListaColoniasSeleccionadasRecolecta.Remove(colonias);
                }
            }
        }
        public void InciaTarifario()
        {
        }
        #endregion

        public void AgregaCiudadOColoniaa(string IdPais, string IdEstado, string IdMunicipio, string IdCiudad, string Nombre, string Tipo)
        {
            oDbDireccion = new DbDireccion();
            if (Tipo == "Ciudad")
            {
                oDbDireccion.AgregaCiudad(IdPais, IdEstado, IdMunicipio, Nombre);
            }
            else if (Tipo == "Colonia")
            {
                oDbDireccion.AgregaColonia(IdPais, IdEstado, IdMunicipio, IdCiudad, Nombre);
            }
        }

        public bool ActualizaListaDireccion(string Id, Guid pais, Guid estado, Guid municipio, Guid Ciudad, Guid colonia, string Calle0, string calle1, string calle2, string manzana, string lote, string CP, string referencia, string IDENTIFICADOR, string NOMBRECIUDAD, string NOMBRECOLONIA)
        {
            bool resultado = false;
            var direccion = new VMDireccion();
            try
            {
                direccion = ListaDIRECCIONES.Find(Dir => Dir.ID.ToString() == Id);
                ListaDIRECCIONES.Remove(direccion);
                direccion = new VMDireccion() { ID = new Guid(Id), PAIS = pais.ToString(), ESTADO = estado.ToString(), MUNICIPIO = municipio.ToString(), CIUDAD = Ciudad.ToString(), COLONIA = colonia.ToString(), CALLE0 = Calle0, CALLE1 = calle1, CALLE2 = calle2, MANZANA = manzana, LOTE = lote, CodigoPostal = CP, REFERENCIA = referencia, IDENTIFICADOR = IDENTIFICADOR, NOMBRECIUDAD = NOMBRECIUDAD, NOMBRECOLONIA = NOMBRECOLONIA };
                ListaDIRECCIONES.Add(direccion);
                resultado = true;
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool ActualizaListaDireccion(string Id, Guid pais, Guid estado, Guid municipio, Guid Ciudad, Guid colonia, string Calle0, string calle1, string calle2, string manzana, string lote, string CP, string referencia, string IDENTIFICADOR, string NOMBRECIUDAD, string NOMBRECOLONIA, string latitud, string longitud)
        {
            bool resultado = false;
            var direccion = new VMDireccion();
            try
            {
                direccion = ListaDIRECCIONES.Find(Dir => Dir.ID.ToString() == Id);
                ListaDIRECCIONES.Remove(direccion);
                direccion = new VMDireccion()
                {
                    ID = new Guid(Id),
                    PAIS = pais.ToString(),
                    ESTADO = estado.ToString(),
                    MUNICIPIO = municipio.ToString(),
                    CIUDAD = Ciudad.ToString(),
                    COLONIA = colonia.ToString(),
                    CALLE0 = Calle0,
                    CALLE1 = calle1,
                    CALLE2 = calle2,
                    MANZANA = manzana,
                    LOTE = lote,
                    CodigoPostal = CP,
                    REFERENCIA = referencia,
                    IDENTIFICADOR = IDENTIFICADOR,
                    NOMBRECIUDAD = NOMBRECIUDAD,
                    NOMBRECOLONIA = NOMBRECOLONIA,
                    Longitud = longitud,
                    Latitud = latitud
                };
                ListaDIRECCIONES.Add(direccion);
                resultado = true;
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public void AgregaDireccionALista(Guid pais, Guid estado, Guid municipio, Guid Ciudad, Guid colonia, string Calle0, string calle1, string calle2, string manzana, string lote, string CP, string referencia, string NOMBRECIUDAD, string NOMBRECOLONIA, string IDENTIFICADOR, string Latitud, string Longitud)
        {
            Guid UidDireccion = Guid.NewGuid();
            if (Ciudad == Guid.Empty)
            {
                NOMBRECIUDAD = "No hay información";
            }
            if (colonia == Guid.Empty)
            {
                NOMBRECOLONIA = "No hay información";
            }
            if (CP == "" && CP == string.Empty)
            {
                CP = "No indicado";
            }
            if (IDENTIFICADOR == "" && IDENTIFICADOR == string.Empty)
            {
                IDENTIFICADOR = "No hay información";
            }
            if (referencia == "" && referencia == string.Empty)
            {
                referencia = "No hay información";
            }
            if (ListaDIRECCIONES != null)
            {
                VMDireccion Dir = new VMDireccion() { ID = UidDireccion, PAIS = pais.ToString(), ESTADO = estado.ToString(), MUNICIPIO = municipio.ToString(), CIUDAD = Ciudad.ToString(), COLONIA = colonia.ToString(), CALLE0 = Calle0, CALLE1 = calle1, CALLE2 = calle2, MANZANA = manzana, LOTE = lote, CodigoPostal = CP, REFERENCIA = referencia, IDENTIFICADOR = IDENTIFICADOR, NOMBRECIUDAD = NOMBRECIUDAD, NOMBRECOLONIA = NOMBRECOLONIA, Latitud = Latitud, Longitud = Longitud };
                ListaDIRECCIONES.Add(Dir);
            }
            else if (ListaDIRECCIONES == null)
            {
                ListaDIRECCIONES = new List<VMDireccion>();
                VMDireccion Dir = new VMDireccion()
                {
                    ID = UidDireccion,
                    PAIS = pais.ToString(),
                    ESTADO = estado.ToString(),
                    MUNICIPIO = municipio.ToString(),
                    CIUDAD = Ciudad.ToString(),
                    COLONIA = colonia.ToString(),
                    CALLE0 = Calle0,
                    CALLE1 = calle1,
                    CALLE2 = calle2,
                    MANZANA = manzana,
                    LOTE = lote,
                    CodigoPostal = CP,
                    REFERENCIA = referencia,
                    IDENTIFICADOR = IDENTIFICADOR,
                    NOMBRECIUDAD = NOMBRECIUDAD,
                    NOMBRECOLONIA = NOMBRECOLONIA,
                    Latitud = Latitud,
                    Longitud = Longitud
                };
                ListaDIRECCIONES.Add(Dir);
            }
        }

        public void AgregaDireccionALista(Guid UidDireccion, Guid pais, Guid estado, Guid municipio, Guid Ciudad, Guid colonia, string Calle0, string calle1, string calle2, string manzana, string lote, string CP, string referencia, string NOMBRECIUDAD, string NOMBRECOLONIA, string IDENTIFICADOR)
        {

            if (Ciudad == Guid.Empty)
            {
                NOMBRECIUDAD = "No hay información";
            }
            if (colonia == Guid.Empty)
            {
                NOMBRECOLONIA = "No hay información";
            }
            if (CP == "" && CP == string.Empty)
            {
                CP = "No indicado";
            }
            if (IDENTIFICADOR == "" && IDENTIFICADOR == string.Empty)
            {
                IDENTIFICADOR = "No hay información";
            }
            if (referencia == "" && referencia == string.Empty)
            {
                referencia = "No hay información";
            }
            if (ListaDIRECCIONES != null)
            {
                VMDireccion Dir = new VMDireccion() { ID = UidDireccion, PAIS = pais.ToString(), ESTADO = estado.ToString(), MUNICIPIO = municipio.ToString(), CIUDAD = Ciudad.ToString(), COLONIA = colonia.ToString(), CALLE0 = Calle0, CALLE1 = calle1, CALLE2 = calle2, MANZANA = manzana, LOTE = lote, CodigoPostal = CP, REFERENCIA = referencia, IDENTIFICADOR = IDENTIFICADOR, NOMBRECIUDAD = NOMBRECIUDAD, NOMBRECOLONIA = NOMBRECOLONIA };
                ListaDIRECCIONES.Add(Dir);
            }
            else if (ListaDIRECCIONES == null)
            {
                ListaDIRECCIONES = new List<VMDireccion>();
                VMDireccion Dir = new VMDireccion() { ID = UidDireccion, PAIS = pais.ToString(), ESTADO = estado.ToString(), MUNICIPIO = municipio.ToString(), CIUDAD = Ciudad.ToString(), COLONIA = colonia.ToString(), CALLE0 = Calle0, CALLE1 = calle1, CALLE2 = calle2, MANZANA = manzana, LOTE = lote, CodigoPostal = CP, REFERENCIA = referencia, IDENTIFICADOR = IDENTIFICADOR, NOMBRECIUDAD = NOMBRECIUDAD, NOMBRECOLONIA = NOMBRECOLONIA };
                ListaDIRECCIONES.Add(Dir);
            }
        }

        public void GuardaListaDeDirecciones(List<VMDireccion> ListaDeDirecciones, Guid Usuario, string StoreProcedure, string TipoUsuario)
        {
            //"asp_AgregaDireccionUsuario"
            Direccion = new Direccion();
            oDbDireccion = new DbDireccion();
            if (TipoUsuario == "Empresa")
            {
                oDbDireccion.EliminaDireccionesEmpresa(Usuario);
            }
            if (TipoUsuario == "Usuario")
            {
                oDbDireccion.EliminaDireccionUsuario(Usuario);
            }
            foreach (var item in ListaDeDirecciones)
            {
                var direccion = new Direccion()
                {
                    ID = item.ID,
                    PAIS = new Pais() { ID = new Guid(item.PAIS) },
                    ESTADO = new Estado() { IDESTADO = new Guid(item.ESTADO) },
                    MUNICIPIO = new Municipio() { IDMUNICIPIO = new Guid(item.MUNICIPIO) },
                    CIUDAD = new Ciudad() { ID = new Guid(item.CIUDAD) },
                    COLONIA = new Colonia() { UID = new Guid(item.COLONIA) },
                    CALLE0 = item.CALLE0,
                    CALLE1 = item.CALLE1,
                    CALLE2 = item.CALLE2,
                    MANZANA = item.MANZANA,
                    LOTE = item.LOTE,
                    CodigoPostal = item.CodigoPostal,
                    REFERENCIA = item.REFERENCIA,
                    IDENTIFICADOR = item.IDENTIFICADOR
                };

                Direccion.GuardaDireccion(StoreProcedure, direccion, Usuario);
            }
        }

        public bool GuardaDireccion(Guid UidDireccion, Guid pais, Guid estado, Guid municipio, Guid Ciudad, Guid colonia, string Calle0, string calle1, string calle2, string manzana, string lote, string CP, string referencia, string IDENTIFICADOR)
        {
            Direccion = new Direccion();
            Direccion Objeto = new Direccion()
            {
                ID = UidDireccion,
                PAIS = new Pais() { ID = pais },
                ESTADO = new Estado() { IDESTADO = estado },
                MUNICIPIO = new Municipio() { IDMUNICIPIO = municipio },
                CIUDAD = new Ciudad() { ID = Ciudad },
                COLONIA = new Colonia() { UID = colonia },
                CALLE0 = Calle0,
                CALLE1 = calle1,
                CALLE2 = calle2,
                MANZANA = manzana,
                LOTE = lote,
                CodigoPostal = CP,
                REFERENCIA = referencia,
                IDENTIFICADOR = IDENTIFICADOR
            };


            bool result = false;
            if (Direccion.GuardaDireccion("asp_AgregaDireccion", Objeto))
            {
                result = true;
            }
            return result;
        }


        public void AgregaDireccion(string StoreProcedure, Guid Usuario, Guid uidDireccion, Guid uidPais, Guid uidEstado, Guid uidMunicipio, Guid uidCiudad, Guid uidColonia, string callePrincipal, string calleAux1, string calleAux2, string manzana, string lote, string codigoPostal, string referencia, string identificador)
        {
            var direccion = new Direccion()
            {
                ID = uidDireccion,
                PAIS = new Pais() { ID = uidPais },
                ESTADO = new Estado() { IDESTADO = uidEstado },
                MUNICIPIO = new Municipio() { IDMUNICIPIO = uidMunicipio },
                CIUDAD = new Ciudad() { ID = uidCiudad },
                COLONIA = new Colonia() { UID = uidColonia },
                CALLE0 = callePrincipal,
                CALLE1 = calleAux1,
                CALLE2 = calleAux2,
                MANZANA = manzana,
                LOTE = lote,
                CodigoPostal = codigoPostal,
                REFERENCIA = referencia,
                IDENTIFICADOR = identificador
            };
            Direccion = new Direccion();
            Direccion.GuardaDireccion(StoreProcedure, direccion, Usuario);
        }

        public void ActualizaDireccion(Guid uidDireccion, Guid uidPais, Guid uidEstado, Guid uidMunicipio, Guid uidCiudad, Guid uidColonia, string callePrincipal, string calleAux1, string calleAux2, string manzana, string lote, string codigoPostal, string referencia, string identificador)
        {
            var direccion = new Direccion()
            {
                ID = uidDireccion,
                PAIS = new Pais() { ID = uidPais },
                ESTADO = new Estado() { IDESTADO = uidEstado },
                MUNICIPIO = new Municipio() { IDMUNICIPIO = uidMunicipio },
                CIUDAD = new Ciudad() { ID = uidCiudad },
                COLONIA = new Colonia() { UID = uidColonia },
                CALLE0 = callePrincipal,
                CALLE1 = calleAux1,
                CALLE2 = calleAux2,
                MANZANA = manzana,
                LOTE = lote,
                CodigoPostal = codigoPostal,
                REFERENCIA = referencia,
                IDENTIFICADOR = identificador
            };
            Direccion = new Direccion();
            Direccion.GuardaDireccion("asp_ActualizaDireccion", direccion, Guid.Empty);
        }

        public void EliminaDireccion(string uidsucursal)
        {
            oDbDireccion = new DbDireccion();
            oDbDireccion.EliminaDireccionSucursal(new Guid(uidsucursal));
        }

        public void EliminaColoniasDeZonaDeServicio(Guid uidmunicipio, string uidsucursal)
        {
            oDbDireccion = new DbDireccion();
            oDbDireccion.EliminaColoniaDeZonaDeServicio(uidmunicipio, new Guid(uidsucursal));
        }
        /// <summary>
        /// Elimina todas las direcciones relacionadas a la empresa
        /// </summary>
        /// <param name="UIDEMPRESA">Uid de la empresa a eliminar direcciones</param>
        public void EliminaDireccionesEmpresa(Guid UIDEMPRESA)
        {
            oDbDireccion = new DbDireccion();
            oDbDireccion.EliminaDireccionesEmpresa(UIDEMPRESA);
        }
        /// <summary>
        /// Elimina una direccion del usuario
        /// </summary>
        /// <param name="UidDireccion">Uid de la direccion a eliminar del usuario</param>
        public void EliminaDireccionUsuario(string UidDireccion)
        {
            oDbDireccion = new DbDireccion();
            oDbDireccion.EliminaDireccionUsuario(UidDireccion);
        }


        #region Direccion de recolecta y entrega
        /// <summary>
        /// Obtiene la direccion de entrega y recolecta de la orden
        /// </summary>
        /// <param name="uidOrden"></param>
        /// <param name="StrParametroDeBusqueda">Entrega|Recolecta</param>
        public void ObtenerDireccionDeOrden(string uidOrden, string StrParametroDeBusqueda)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "asp_ObtenerDireccionesDeOrden";

                comando.Parameters.Add("@UidOrden", SqlDbType.UniqueIdentifier);
                comando.Parameters["@UidOrden"].Value = new Guid(uidOrden);

                comando.Parameters.Add("@StrParametroDeBusqueda", SqlDbType.VarChar, 10);
                comando.Parameters["@StrParametroDeBusqueda"].Value = StrParametroDeBusqueda;
                oConexion = new Conexion();
                ListaDIRECCIONES.Clear();
                foreach (DataRow item in oConexion.Busquedas(comando).Rows)
                {
                    if (!ListaDIRECCIONES.Exists(d => d.UidDireccion == new Guid(item["UidDireccion"].ToString())))
                    {
                        string NombrePais = ObtenerNombrePais(item["UidPais"].ToString());
                        string NombreEstado = ObtenerNombreDelEstado(item["UidEstado"].ToString());
                        string NombreMunicipio = ObtenerNombreDelMunicipio(item["UidMunicipio"].ToString());
                        string NombreCiudad = ObtenerNombreDeLaCiudad(item["UidCiudad"].ToString());
                        string NombreColonia = ObtenerNombreDeLaColonia(item["UidColonia"].ToString());
                        ListaDIRECCIONES.Add(
                            new VMDireccion()
                            {
                                ID = new Guid(item["UidDireccion"].ToString()),
                                PAIS = NombrePais,
                                ESTADO = NombreEstado,
                                MUNICIPIO = NombreMunicipio,
                                CALLE0 = item["Calle0"].ToString(),
                                MANZANA = item["Manzana"].ToString(),
                                LOTE = item["Lote"].ToString(),
                                CodigoPostal = item["CodigoPostal"].ToString(),
                                NombrePais = NombrePais,
                                NombreEstado = NombreEstado,
                                NombreMunicipio = NombreMunicipio,
                                CIUDAD = NombreCiudad,
                                COLONIA = NombreColonia,
                            });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private string ObtenerNombreDelMunicipio(string UidMunicipio)
        {
            string NOMBRE = string.Empty;
            oDbDireccion = new DbDireccion();
            foreach (DataRow item in oDbDireccion.ObtenerNombreMunicipio(UidMunicipio).Rows)
            {
                NOMBRE = item["Nombre"].ToString();
            }
            if (NOMBRE == string.Empty)
            {
                NOMBRE = "No hay información";
            }
            return NOMBRE;
        }

        private string ObtenerNombreDelEstado(string UidEstado)
        {
            string NOMBRE = string.Empty;
            oDbDireccion = new DbDireccion();
            foreach (DataRow item in oDbDireccion.ObtenerNombreEstado(UidEstado).Rows)
            {
                NOMBRE = item["Nombre"].ToString();
            }
            if (NOMBRE == string.Empty)
            {
                NOMBRE = "No hay información";
            }
            return NOMBRE;
        }

        public string ObtenerNombrePais(string UidPais)
        {
            string NOMBRE = string.Empty;
            oDbDireccion = new DbDireccion();
            foreach (DataRow item in oDbDireccion.ObtenerNombrePais(UidPais).Rows)
            {
                NOMBRE = item["Nombre"].ToString();
            }
            if (NOMBRE == string.Empty)
            {
                NOMBRE = "No hay información";
            }
            return NOMBRE;
        }

        #endregion


        public void SeleccionarEstado(string UidEstado)
        {
            if (!ListaDeEstadosSeleccionados.Exists(e => e.UidEstado == new Guid(UidEstado)))
            {
                ListaDeEstadosSeleccionados.Add(new VMDireccion() { UidEstado = new Guid(UidEstado) });
            }
        }
        public void desSeleccionarEstado(string UidEstado)
        {
            if (ListaDeEstadosSeleccionados.Exists(e => e.UidEstado == new Guid(UidEstado)))
            {
                var objeto = ListaDeEstadosSeleccionados.Find(e => e.UidEstado == new Guid(UidEstado));
                ListaDeEstadosSeleccionados.Remove(objeto);
            }
        }

        public void BuscarZonaHorariaDePais(string UidZonaHoraria = "")
        {
            SqlCommand Comando = new SqlCommand();
            ListaDeEstadosSeleccionados = new List<VMDireccion>();
            oConexion = new Conexion();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BuscarZonasHorarias";

                if (!string.IsNullOrEmpty(UidZonaHoraria))
                {
                    Comando.Parameters.Add("@UidRelacionZonaHorariaPais", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidRelacionZonaHorariaPais"].Value = new Guid(UidZonaHoraria);
                }

                foreach (DataRow item in oConexion.Busquedas(Comando).Rows)
                {
                    if (!ListaDeEstadosSeleccionados.Exists(z => z.UidEstado == new Guid(item["uidestado"].ToString())))
                    {
                        ListaDeEstadosSeleccionados.Add(new VMDireccion()
                        {
                            UidEstado = new Guid(item["uidestado"].ToString()),
                            NombreEstado = item["Nombre"].ToString()
                        });
                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
