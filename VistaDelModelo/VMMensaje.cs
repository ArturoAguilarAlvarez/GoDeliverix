using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Modelo;
using DBControl;

namespace VistaDelModelo
{
    public class VMMensaje
    {
        #region Propiedades

        Conexion Datos;
        Mensaje oMensaje;
        DBMensajes oDbMensaje;
        private Guid _UidMensaje;

        public Guid Uid
        {
            get { return _UidMensaje; }
            set { _UidMensaje = value; }
        }
        private string _VchMensaje;

        public string StrMensaje
        {
            get { return _VchMensaje; }
            set { _VchMensaje = value; }
        }
        public List<VMMensaje> ListaDeMensajes;
        #endregion

        #region Metodos
        public bool GuardarMensaje(Guid UidMensaje, string strMensaje)
        {
            try
            {
                oMensaje = new Mensaje();
                oMensaje.Uid = UidMensaje;
                oMensaje.StrMensaje = strMensaje;
                return oMensaje.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ActualizarMensaje(Guid UidMensaje, string strMensaje)
        {
            try
            {
                oMensaje = new Mensaje();
                oMensaje.Uid = UidMensaje;
                oMensaje.StrMensaje = strMensaje;
                return oMensaje.Actualizar();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AsociarMensajeSucursal(Guid UidMensaje, Guid UidSucursal)
        {
            try
            {
                oMensaje = new Mensaje();
                oMensaje.Uid = UidMensaje;
                return oMensaje.AsociarConSucursal(UidSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Buscar(Guid UidSucursal = new Guid(), string strLicencia = "")
        {
            SqlCommand Comando = new SqlCommand();
            ListaDeMensajes = new List<VMMensaje>();
            Datos = new Conexion();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BuscarMensajes";

                if (UidSucursal != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidSucursal"].Value = UidSucursal;
                }
                if (!string.IsNullOrEmpty(strLicencia))
                {
                    Comando.Parameters.Add("@UidLicencia", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidLicencia"].Value = new Guid(strLicencia);
                }


                foreach (DataRow item in Datos.Busquedas(Comando).Rows)
                {
                    Guid uidmensaje = new Guid(item["UidMensaje"].ToString());
                    string mensaje = item["VchMensaje"].ToString().ToUpper();
                    ListaDeMensajes.Add(new VMMensaje() { Uid = uidmensaje, StrMensaje = mensaje });
                }


            }
            catch (Exception)
            {

                throw;
            }
        }




        public void AgregarMensajeALista(String mensaje)
        {
            ListaDeMensajes.Add(new VMMensaje() { Uid = Guid.NewGuid(), StrMensaje = mensaje });
        }
        public void QuitarMensajeDeLista(Guid UidMensaje)
        {
            if (ListaDeMensajes.Exists(men => men.Uid == UidMensaje))
            {
                var objeto = ListaDeMensajes.Find(men => men.Uid == UidMensaje);
                ListaDeMensajes.Remove(objeto);
            }
        }

        public void EliminaMensajesSucursal(string uIDSUCURSAL)
        {
            oDbMensaje = new DBMensajes();
            oDbMensaje.EliminarMensajesSucursal(uIDSUCURSAL);
        }


        #endregion
    }
}
