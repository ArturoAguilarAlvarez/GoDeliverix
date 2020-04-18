using DBControl;
using Modelo;
using System;
using System.Data;

namespace VistaDelModelo
{
    public class VMUbicacion
    {
        #region Propiedades
        DbUbicacion Datos = new DbUbicacion();
        Ubicacion oUbicacion;
        private Guid _Uidubicacion;

        public Guid UID
        {
            get { return _Uidubicacion; }
            set { _Uidubicacion = value; }
        }


        private string _strLatitud;

        public string VchLatitud
        {
            get { return _strLatitud; }
            set { _strLatitud = value; }
        }

        private string _strLongitud;

        public string VchLongitud
        {
            get { return _strLongitud; }
            set { _strLongitud = value; }
        }

        #endregion
        /// <summary>
        /// Guarda la ubicacion de una sucursal
        /// </summary>
        /// <param name="UidSucursal"></param>
        /// <param name="UidUbicacion"></param>
        /// <param name="LATITUD"></param>
        /// <param name="LONGITUD"></param>
        /// <returns></returns>
        public bool GuardaUbicacionsucursal(Guid UidSucursal, Guid UidUbicacion, string LATITUD, string LONGITUD)
        {
            Datos.EliminarUbicacionSucursal(UidSucursal);
            oUbicacion = new Ubicacion();
            oUbicacion.UID = UidUbicacion;
            oUbicacion.VchLatitud = LATITUD;
            oUbicacion.VchLongitud = LONGITUD;
            return oUbicacion.Guardar("asp_AgregaUbicacionSucursal", UidSucursal);
        }
        /// <summary>
        /// Guarda la  ubicacion de una direccion
        /// </summary>
        /// <param name="UidDireccion"></param>
        /// <param name="UidUbicacion"></param>
        /// <param name="LATITUD"></param>
        /// <param name="LONGITUD"></param>
        /// <returns></returns>
        public bool GuardaUbicacionDireccion(Guid UidDireccion, Guid UidUbicacion, string LATITUD, string LONGITUD)
        {
            Datos.EliminarUbicacionDireccion(UidDireccion);
            oUbicacion = new Ubicacion();
            oUbicacion.UID = UidUbicacion;
            oUbicacion.VchLatitud = LATITUD;
            oUbicacion.VchLongitud = LONGITUD;
            return oUbicacion.Guardar("asp_AgregarUbicacionDireccion", UidDireccion);
        }
        /// <summary>
        /// Recuepera la ubicacion de la sucursal
        /// </summary>
        /// <param name="UidSucursal"></param>
        public void RecuperaUbicacionSucursal(string UidSucursal)
        {
            UID = new Guid();
            VchLatitud = string.Empty;
            VchLongitud = string.Empty;
            foreach (DataRow item in Datos.UbicacionSucursal(UidSucursal).Rows)
            {
                UID = new Guid(item["UidUbicacion"].ToString());
                VchLatitud = item["VchLatitud"].ToString();
                VchLongitud = item["VchLongitud"].ToString();
            }
        }
        /// <summary>
        /// Recupera la ubicacion de cualquier direccion
        /// </summary>
        /// <param name="UidDireccion"></param>
        public void RecuperaUbicacionDireccion(string UidDireccion)
        {
            UID = new Guid();
            VchLatitud = string.Empty;
            VchLongitud = string.Empty;
            foreach (DataRow item in Datos.UbicacionDireccion(UidDireccion).Rows)
            {
                UID = new Guid(item["UidUbicacion"].ToString());
                VchLatitud = item["VchLatitud"].ToString();
                VchLongitud = item["VchLongitud"].ToString();
            }
        }

        public DataTable ObtenerDatosDireccion(string colonia, string codigopostal)
        {
            return this.Datos.ObtenerDireccionPorColoniaEstadoPais(colonia, codigopostal);
        }
    }
}
