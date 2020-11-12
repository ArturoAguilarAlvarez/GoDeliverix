using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DBControl;
using Modelo;

namespace VistaDelModelo
{
    public class VMOferta
    {
        #region Propiedades

        Conexion Datos;

        Oferta oOferta;

        private Guid _uidOferta;

        public Guid UID
        {
            get { return _uidOferta; }
            set { _uidOferta = value; }
        }

        private string _vchNombre;

        public string STRNOMBRE
        {
            get { return _vchNombre; }
            set { _vchNombre = value; }
        }

        private Guid _uidSucursal;

        public Guid UidSucursal
        {
            get { return _uidSucursal; }
            set { _uidSucursal = value; }
        }

        private string _strEstatus;

        public string StrEstatus
        {
            get { return _strEstatus; }
            set { _strEstatus = value; }
        }


        public List<VMOferta> ListaDeOfertas = new List<VMOferta>();
        #endregion

        #region Metodos
        public bool Guardar(Guid UIDOFERTA, Guid UIDSUCURSAL, string NOMBRE, string ESTATUS)
        {
            try
            {
                oOferta = new Oferta();
                oOferta.UID = UIDOFERTA;
                oOferta.STRNOMBRE = NOMBRE;
                oOferta.oSucursal = new Sucursal() { ID = UIDSUCURSAL };
                oOferta.oEstatus = new Estatus() { ID = int.Parse(ESTATUS) };
                return oOferta.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Actualiza(Guid UIDOFERTA, string NOMBRE, string ESTATUS)
        {
            try
            {
                oOferta = new Oferta();
                oOferta.UID = UIDOFERTA;
                oOferta.STRNOMBRE = NOMBRE;
                oOferta.oEstatus = new Estatus() { ID = int.Parse(ESTATUS) };
                return oOferta.Actualizar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void BuscarOfertasCliente(string UIDSUCURSAL, string dia)
        {
            SqlCommand Comando = new SqlCommand();
            ListaDeOfertas.Clear();
            Datos = new Conexion();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BuscarOferta";

                //Valida si esta buscando todas las ofertas o solo una

                Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSucursal"].Value = new Guid(UIDSUCURSAL);

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = 1;

                Comando.Parameters.Add("@Dia", SqlDbType.VarChar,20);
                Comando.Parameters["@Dia"].Value = dia;

                foreach (DataRow item in Datos.Busquedas(Comando).Rows)
                {
                    Guid uidoferta = new Guid(item["UidOferta"].ToString());
                    string nombre = item["VchNombre"].ToString().ToUpper();
                    string estatus = item["intEstatus"].ToString();
                    ListaDeOfertas.Add(new VMOferta() { UID = uidoferta, STRNOMBRE = nombre, StrEstatus = estatus });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Buscar(Guid UIDOFERTA = new Guid(), Guid UIDSUCURSAL = new Guid(), string NOMBRE = "", string ESTATUS = "", Guid UidEmpresa = new Guid())
        {
            SqlCommand Comando = new SqlCommand();
            ListaDeOfertas.Clear();
            Datos = new Conexion();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BuscarOferta";

                //Valida si esta buscando todas las ofertas o solo una
                if (UIDOFERTA == Guid.Empty)
                {

                    if (UidEmpresa != Guid.Empty)
                    {
                        Comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                        Comando.Parameters["@UidEmpresa"].Value = UidEmpresa;
                    }

                    if (UIDSUCURSAL != Guid.Empty)
                    {
                        Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                        Comando.Parameters["@UidSucursal"].Value = UIDSUCURSAL;
                    }

                    if (NOMBRE != string.Empty && NOMBRE != "")
                    {
                        Comando.Parameters.Add("@VchNombre", SqlDbType.VarChar, 30);
                        Comando.Parameters["@VchNombre"].Value = NOMBRE;
                    }
                    if (ESTATUS != "-1" && ESTATUS != "")
                    {
                        Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                        Comando.Parameters["@IntEstatus"].Value = int.Parse(ESTATUS);
                    }
                    foreach (DataRow item in Datos.Busquedas(Comando).Rows)
                    {
                        Guid uidoferta = new Guid(item["UidOferta"].ToString());
                        string nombre = item["VchNombre"].ToString().ToUpper();
                        string estatus = item["intEstatus"].ToString();
                        ListaDeOfertas.Add(new VMOferta() { UID = uidoferta, STRNOMBRE = nombre, StrEstatus = estatus });
                    }
                }
                else
                {
                    Comando.Parameters.Add("@UidOferta", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidOferta"].Value = UIDOFERTA;
                    Datos = new Conexion();
                    foreach (DataRow item in Datos.Busquedas(Comando).Rows)
                    {
                        UID = new Guid(item["UidOferta"].ToString());
                        STRNOMBRE = item["VchNombre"].ToString().ToUpper();
                        StrEstatus = item["IntEstatus"].ToString();
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
