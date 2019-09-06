using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;

namespace VistaDelModelo
{
    public class VMLicencia
    {
        #region Propiedades
        private Guid _UidLicencia;

        public Guid UidLicencia
        {
            get { return _UidLicencia; }
            set { _UidLicencia = value; }
        }

        private bool _boolDisponibilidad;

        public bool BLUso
        {
            get { return _boolDisponibilidad; }
            set { _boolDisponibilidad = value; }
        }
        private int _idEstatus;

        public int UidEstatus
        {
            get { return _idEstatus; }
            set { _idEstatus = value; }
        }
        private Guid _UidSucursal;

        public Guid Propietario
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }

        private string _VchIdentificador;

        public string VchIdentificador
        {
            get { return _VchIdentificador; }
            set { _VchIdentificador = value; }
        }

        public bool VerificaDisponibilidad(string licencia)
        {
            oDbLicencia = new DBLicencia();
            DataTable dt = oDbLicencia.VerificaDisponibilidad(licencia);
            bool resultado = false;
            if (dt.Rows.Count > 0)
            {
                resultado = true;
            }
            return resultado;
        }

        public void CambiaDisponibilidadDeLicencia(string licencia)
        {
            oDbLicencia = new DBLicencia();
            oDbLicencia.CambiaDisponibilidad(licencia);
        }

        public List<VMLicencia> ListaDeLicencias = new List<VMLicencia>();
        Licencia oLicencia;
        Conexion oConexion;
        DBLicencia oDbLicencia;
        #endregion

        #region Metodos
        public bool GuardaRelacionSucursal(Guid UidLicencia, Guid Propietario, int IdEstatus, bool bdisponibilidad, string strIdentificador)
        {
            bool resultado = false;
            try
            {
                oLicencia = new Licencia();
                oLicencia.oEstatus = new Estatus(IdEstatus);
                oLicencia.BLUso = bdisponibilidad;
                oLicencia.UidLicencia = UidLicencia;
                oLicencia.StrIdentificador = strIdentificador;
                resultado = oLicencia.Guardar("asp_AgregarSucursalLicencia", Propietario);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool ActualizarLicenciaSucursal(Guid UidLicencia, int IdEstatus = 0, bool bdisponibilidad = false, string strIdentificador = "")
        {
            bool resultado = false;
            try
            {
                oLicencia = new Licencia();
                oLicencia.oEstatus = new Estatus(IdEstatus);
                oLicencia.BLUso = bdisponibilidad;
                oLicencia.StrIdentificador = strIdentificador;
                resultado = oLicencia.Actualizar("asp_ActualizaSucursalLicencia", UidLicencia);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public void AgregaLicenciaALista(Guid UIdPropietario, Guid UidLicencia, string StrIdentificador)
        {
            VMLicencia objeto;

            if (ListaDeLicencias.Exists(Lic => Lic.UidLicencia == Guid.Empty))
            {
                objeto = ListaDeLicencias.Find(obj => obj.UidLicencia == Guid.Empty);
                ListaDeLicencias.Remove(objeto);
            }
            //Agrega un objeto con los datos recibidos por parametros
            objeto = new VMLicencia() { VchIdentificador = StrIdentificador, UidLicencia = UidLicencia, Propietario = UIdPropietario, UidEstatus = 1, BLUso = false };
            ListaDeLicencias.Add(objeto);
            //Agrega un objeto vacio al final de la lista
            //objeto = new VMLicencia() { VchIdentificador = string.Empty, UidLicencia = Guid.Empty, Propietario = UIdPropietario, UidEstatus = 1, BLUso = false };
            //ListaDeLicencias.Add(objeto);

        }

        public void ObtenerLicenciaSucursal(string uidsucursal)
        {
            oConexion = new Conexion();
            DataTable DatoQuery = new DataTable();
            VMLicencia objeto;
            Guid UIdPropietario = new Guid();
            try
            {
                string Query = "select * from SucursalLicencia where Uidsucursal ='" + uidsucursal + "'";
                DatoQuery = oConexion.Consultas(Query);
            }
            catch (Exception)
            {

                throw;
            }
            ListaDeLicencias.Clear();
            foreach (DataRow item in DatoQuery.Rows)
            {
                Guid licencia = new Guid(item["uidlicencia"].ToString());
                UIdPropietario = new Guid(item["UidSucursal"].ToString());
                int estatus = int.Parse(item["IntEstatus"].ToString());
                string identificador = item["vchIdentificador"].ToString();
                bool uso = bool.Parse(item["BLUso"].ToString());
                objeto = new VMLicencia() { UidLicencia = licencia, Propietario = UIdPropietario, VchIdentificador = identificador, UidEstatus = estatus, BLUso = uso };
                ListaDeLicencias.Add(objeto);
            }

            objeto = new VMLicencia() { VchIdentificador = string.Empty, UidLicencia = Guid.Empty, Propietario = UIdPropietario, UidEstatus = 1, BLUso = false };
            ListaDeLicencias.Add(objeto);

        }

        public void eliminaLicenciaSucursal(Guid UidSucursal)
        {
            oDbLicencia = new DBLicencia();
            oDbLicencia.EliminaLicenciaSucursal(UidSucursal.ToString());
        }

        public void ActualizaEstatusLicenciaSucursal(Guid uidLicencia)
        {
            var licencia = ListaDeLicencias.Find(obj => obj.UidLicencia == uidLicencia);

            if (licencia.UidEstatus == 1)
            {
                licencia.UidEstatus = 0;
            }
            else
            {
                licencia.UidEstatus = 1;
            }
        }

        public void ActualizaDisponibilidadLicenciaSucursal(Guid uidLicencia)
        {
            var licencia = ListaDeLicencias.Find(obj => obj.UidLicencia == uidLicencia);
            licencia.UidLicencia = Guid.NewGuid();
            if (licencia.BLUso)
            {
                licencia.BLUso = false;
            }
            licencia.UidLicencia = Guid.NewGuid();
            licencia.UidEstatus = 1;
        }

        public void EliminaLicencia(Guid uidLicencia)
        {
            var licencia = ListaDeLicencias.Find(obj => obj.UidLicencia == uidLicencia);
            ListaDeLicencias.Remove(licencia);
            oDbLicencia = new DBLicencia();
            oDbLicencia.EliminaLicencia(UidLicencia);
        }


        public bool ValidaExistenciaDeLicencia(string Licencia)
        {
            bool resultado = false;
            oDbLicencia = new DBLicencia();
            if (oDbLicencia.VerificaLicenciaDeSucursal(Licencia).Rows.Count == 1)
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }
        public bool VerificaEstatusDeLicenciaSucursal(string Licencia)
        {
            bool resultado = false;
            oDbLicencia = new DBLicencia();
            if (oDbLicencia.verificaEstatusLicenciaSucursal(Licencia).Rows.Count == 1)
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }
            return resultado;
        }

        public void ActualizaLicenciaEnLista(string UidLicencia, string stridentificador)
        {
            var Licencia = ListaDeLicencias.Find(obj => obj.UidLicencia == new Guid(UidLicencia));
            int index = ListaDeLicencias.FindIndex(obj => obj.UidLicencia == new Guid(UidLicencia));
            ListaDeLicencias.Remove(Licencia);
            Licencia.VchIdentificador = stridentificador;
            ListaDeLicencias.Insert(index, Licencia);
        }
        #endregion
    }
}
