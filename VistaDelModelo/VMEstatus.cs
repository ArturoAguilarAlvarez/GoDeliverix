using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;

namespace VistaDelModelo
{
    public class VMEstatus
    {
        #region Propiedades
        Conexion Datos = new Conexion();
        DBEstatus DB = new DBEstatus();
        public Estatus MyProperty { get; set; }
        private Guid _UidEstatus;

        public Guid UidEstatus
        {
            get { return _UidEstatus; }
            set { _UidEstatus = value; }
        }

        private int _idEstatus;
        public int ID
        {
            get { return _idEstatus; }
            set { _idEstatus = value; }
        }
        private string _strNombre;
        public string NOMBRE
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        private string _dtmFecha;

        public string DtmFechaDeEstatus
        {
            get { return _dtmFecha; }
            set { _dtmFecha = value; }
        }

        public List<VMEstatus> ListaEstatus = new List<VMEstatus>();
        #endregion
        #region Metodos
        public void OBTENERLISTA()
        {
            foreach (DataRow item in DB.ObtenerEstatus().Rows)
            {
                int id = Int32.Parse(item["IdEstatus"].ToString());
                string Nombre = item["Nombre"].ToString();
                ListaEstatus.Add(new VMEstatus() { ID = id, NOMBRE = Nombre });
            }
        }
        public DataTable ObtenerListaActiva()
        {
            return DB.ObtenerEstatusActivo();

        }
        public void cargaEstatusOrdenSucursal(string UidOrdenSucursal)
        {
            ListaEstatus = new List<VMEstatus>();
            foreach (DataRow item in DB.ObtenerListaDeEstatusOrdenSucursal(UidOrdenSucursal).Rows)
            {
                ListaEstatus.Add(new VMEstatus()
                {
                    DtmFechaDeEstatus = item["DtmFecha"].ToString(),
                    NOMBRE = item["VchNombre"].ToString(),
                    DtFecha = (DateTime)item["DtmFecha"]
                });
            }
        }
        public void ObtnenerEstatusDeContrato()
        {
            foreach (DataRow item in DB.ObtenerEstatusDeContrato().Rows)
            {
                Guid id = new Guid(item["UidEstatus"].ToString());
                string Nombre = item["VchNombre"].ToString();
                ListaEstatus.Add(new VMEstatus() { UidEstatus = id, NOMBRE = Nombre });
            }
        }

        public void ObtnenerEstatusDeOrdenEnSucursal()
        {
            foreach (DataRow item in DB.ObtenerEstatusDeOrdenEnSucursal().Rows)
            {
                Guid id = new Guid(item["UidEstatus"].ToString());
                string Nombre = item["VchNombre"].ToString();
                ListaEstatus.Add(new VMEstatus() { UidEstatus = id, NOMBRE = Nombre });
            }
        }
        #endregion

        #region Properties
        public DateTime DtFecha { get; set; }
        #endregion
    }
}
