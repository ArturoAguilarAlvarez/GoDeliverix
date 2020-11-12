using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VistaDelModelo
{
    public class VMSeccion
    {
        #region Propiedades

        Conexion Consultas;
        Seccion oSeccion;
        DbSeccion datos;

        private Guid _UidSeccion;

        public Guid UID
        {
            get { return _UidSeccion; }
            set { _UidSeccion = value; }
        }

        private Guid _UidSucursal;

        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }


        private string _VchNombre;

        public string StrNombre
        {
            get { return _VchNombre; }
            set { _VchNombre = value; }
        }

        private string _VchHoraInicio;

        public string StrHoraInicio
        {
            get { return _VchHoraInicio; }
            set { _VchHoraInicio = value; }
        }

        private string _vchHoraFin;

        public string StrHoraFin
        {
            get { return _vchHoraFin; }
            set { _vchHoraFin = value; }
        }

        private int _intEstatus;

        public int IntEstatus
        {
            get { return _intEstatus; }
            set { _intEstatus = value; }
        }

        public List<VMSeccion> ListaDeSeccion = new List<VMSeccion>();

        #endregion

        #region Metodos

        public bool Guardar(Guid UIDSECCION, Guid UIDOFERTA, string NOMBRE, string HORAINICIO, string HORAFIN, int Estatus)
        {
            try
            {
                oSeccion = new Seccion();
                oSeccion.UID = UIDSECCION;
                oSeccion.StrNombre = NOMBRE;
                oSeccion.StrHoraInicio = HORAINICIO;
                oSeccion.StrHoraFin = HORAFIN;
                oSeccion.oOferta = new Oferta() { UID = UIDOFERTA };
                oSeccion.oEstatus = new Estatus() { ID = Estatus };
                return oSeccion.Agrega();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Actualiza(Guid UIDSECCION, string NOMBRE, string HORAINICIO, string HORAFIN, int Estatus)
        {
            try
            {
                oSeccion = new Seccion();
                oSeccion.UID = UIDSECCION;
                oSeccion.StrNombre = NOMBRE;
                oSeccion.StrHoraInicio = HORAINICIO;
                oSeccion.StrHoraFin = HORAFIN;
                oSeccion.oEstatus = new Estatus() { ID = Estatus };
                return oSeccion.Actualiza();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BuscarSeccion(string UidSeccionProducto)
        {
            SqlCommand Comando = new SqlCommand();
            ListaDeSeccion.Clear();
            Consultas = new Conexion();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ObtenerHoraFinalDeSeccion";

                if (!string.IsNullOrEmpty(UidSeccionProducto))
                {

                    Comando.Parameters.Add("@UidSeccionProducto", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidSeccionProducto"].Value = new Guid(UidSeccionProducto);

                    foreach (DataRow item in Consultas.Busquedas(Comando).Rows)
                    {
                        StrHoraFin = item["HoraFinal"].ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Buscar(Guid UIDSECCION = new Guid(), Guid UIDOFERTA = new Guid(), string NOMBRE = "", string HORAINICIO = "", string HORAFIN = "", string Estatus = "", Guid UidDirecccion = new Guid(), string UidEstado = "", string UidColonia = "")
        {
            SqlCommand Comando = new SqlCommand();
            ListaDeSeccion.Clear();
            Consultas = new Conexion();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BuscarSeccion";

                if (UIDSECCION == Guid.Empty)
                {

                    Comando.Parameters.Add("@UidOferta", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidOferta"].Value = UIDOFERTA;

                    //if (UidDirecccion != Guid.Empty)
                    //{
                    //    Comando.Parameters.Add("@UidDireccion", SqlDbType.UniqueIdentifier);
                    //    Comando.Parameters["@UidDireccion"].Value = UidDirecccion;
                    //}
                    if (!string.IsNullOrEmpty(UidEstado))
                    {
                        Comando.Parameters.Add("@UidEstado", SqlDbType.UniqueIdentifier);
                        Comando.Parameters["@UidEstado"].Value = new Guid(UidEstado);
                    }
                    if (!string.IsNullOrEmpty(UidColonia))
                    {
                        Comando.Parameters.Add("@UidColonia", SqlDbType.UniqueIdentifier);
                        Comando.Parameters["@UidColonia"].Value = new Guid(UidColonia);
                    }

                    if (NOMBRE != string.Empty && NOMBRE != "")
                    {
                        Comando.Parameters.Add("@VchNombre", SqlDbType.VarChar, 30);
                        Comando.Parameters["@VchNombre"].Value = NOMBRE;
                    }
                    if (HORAINICIO != string.Empty && HORAINICIO != "" && HORAFIN != string.Empty && HORAFIN != "")
                    {
                        Comando.Parameters.Add("@VchHoraInicio", SqlDbType.VarChar, 10);
                        Comando.Parameters["@VchHoraInicio"].Value = HORAINICIO;

                        Comando.Parameters.Add("@VchHoraFin", SqlDbType.VarChar, 10);
                        Comando.Parameters["@VchHoraFin"].Value = HORAFIN;
                    }
                    if (Estatus != "-1" && Estatus != "")
                    {
                        Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                        Comando.Parameters["@IntEstatus"].Value = int.Parse(Estatus);
                    }
                    foreach (DataRow item in Consultas.Busquedas(Comando).Rows)
                    {
                        Guid uidseccion = new Guid(item["UidSeccion"].ToString());
                        string nombre = item["VchNombre"].ToString().ToUpper();
                        string horainicio = item["VchHoraInicio"].ToString();
                        string horafin = item["VchHoraFin"].ToString();
                        string estatus = item["intEstatus"].ToString();
                        ListaDeSeccion.Add(new VMSeccion() { UID = uidseccion, StrNombre = nombre, StrHoraInicio = horainicio, StrHoraFin = horafin, IntEstatus = Int32.Parse(estatus) });
                    }
                }
                else
                {
                    Comando.Parameters.Add("@UidSeccion", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidSeccion"].Value = UIDSECCION;
                    Consultas = new Conexion();
                    foreach (DataRow item in Consultas.Busquedas(Comando).Rows)
                    {
                        UID = new Guid(item["UidSeccion"].ToString());
                        StrNombre = item["VchNombre"].ToString().ToUpper();
                        StrHoraInicio = item["VchHoraInicio"].ToString();
                        StrHoraFin = item["VchHoraFin"].ToString();
                        IntEstatus = int.Parse(item["IntEstatus"].ToString());
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void RelacionConProducto(Guid UIDSECCION, Guid UIDPRODUCTO)
        {
            try
            {
                oSeccion = new Seccion();
                oSeccion.GuardaProducto(UIDSECCION, UIDPRODUCTO);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void eliminaProductos(Guid uidproducto, Guid uidseccion)
        {
            datos = new DbSeccion();

            datos.eliminarProducto(uidproducto, uidseccion);
        }

        public int EncuentraRegistro(Guid uidproducto, Guid uidseccion)
        {
            int i = 0;
            datos = new DbSeccion();
            foreach (DataRow item in datos.encuentraRegistro(uidproducto, uidseccion).Rows)
            {
                i = i + 1;
            }
            return i;
        }



        #endregion
    }
}
