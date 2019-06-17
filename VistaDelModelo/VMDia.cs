using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VistaDelModelo
{
    public class VMDia
    {
        #region Propiedades
        Conexion Datos;
        DbDia DatosDia;
        Dia oDia;
        private Guid _uidDia;

        public Guid UID
        {
            get { return _uidDia; }
            set { _uidDia = value; }
        }

        private string _strNombre;

        public string StrNombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        private int _intorden;

        public int INTOrden
        {
            get { return _intorden; }
            set { _intorden = value; }
        }
        public List<VMDia> ListaDeDias = new List<VMDia>();
        public List<VMDia> ListaDeDiasSeleccionados = new List<VMDia>();
        #endregion

        #region Metodos
        public bool RelacionDiaOferta(Guid UidDia, Guid Usuario)
        {
            try
            {
                oDia = new Dia();
                oDia.UID = UidDia;
                return oDia.Guardar("asp_RelacionDiaOferta", Usuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Buscar(Guid UidDia = new Guid(), string Nombre = "")
        {
            SqlCommand Comando = new SqlCommand();
            ListaDeDias.Clear();
            Datos = new Conexion();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_buscarDia";

                foreach (DataRow item in Datos.Busquedas(Comando).Rows)
                {
                    Guid uidoferta = new Guid(item["UidDia"].ToString());
                    string nombre = item["VchNombre"].ToString().ToUpper();
                    ListaDeDias.Add(new VMDia() { UID = uidoferta, StrNombre = nombre });
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EliminaDiaOferta(string UidOferta)
        {
            DatosDia = new DbDia();
            DatosDia.EliminaRelacionDiaOferta(UidOferta);
        }
        public void ObtenerDiaOferta(Guid UidOferta)
        {
            DatosDia = new DbDia();
            ListaDeDiasSeleccionados.Clear();
            foreach (DataRow item in DatosDia.ObtenerDiaOferta(UidOferta.ToString()).Rows)
            {
                ListaDeDiasSeleccionados.Add(new VMDia() { UID = new Guid(item["UidDia"].ToString()) });
            }
        }
        #endregion
    }
}
