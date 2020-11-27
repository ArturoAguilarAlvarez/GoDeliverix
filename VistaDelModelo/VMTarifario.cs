using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VistaDelModelo
{
    public class VMTarifario
    {
        #region Propiedades
        Tarifario oTarifario;
        DBTarifario oDbTarifario;
        private Guid _UidTarifario;

        public Guid UidTarifario
        {
            get { return _UidTarifario; }
            set { _UidTarifario = value; }
        }
        private string _StrCodigoDeEntrega;

        public string StrCodigoDeEntrega
        {
            get { return _StrCodigoDeEntrega; }
            set { _StrCodigoDeEntrega = value; }
        }


        /// <summary>
        /// Relacion de zona de recoleccion
        /// </summary>
        private Guid _UidRelacionZR;
        public Guid UidRelacionZR
        {
            get { return _UidRelacionZR; }
            set { _UidRelacionZR = value; }
        }

        private string _StrNombreColoniaZR;

        public string StrNombreColoniaZR
        {
            get { return _StrNombreColoniaZR; }
            set { _StrNombreColoniaZR = value; }
        }

        /// <summary>
        /// Relacion de zona de entrega
        /// </summary>
        private Guid _UidRelacionZE;

        public Guid UidRelacionZE
        {
            get { return _UidRelacionZE; }
            set { _UidRelacionZE = value; }
        }

        private string _StrNombreColoniaZE;
        public string StrNombreColoniaZE
        {
            get { return _StrNombreColoniaZE; }
            set { _StrNombreColoniaZE = value; }
        }
        /// <summary>
        /// Precio de la tafira
        /// </summary>
        private decimal _DPrecio;
        public decimal DPrecio
        {
            get { return _DPrecio; }
            set { _DPrecio = value; }
        }

        private Guid _GuidSucursalDistribuidora;
        public Guid GuidSucursalDistribuidora
        {
            get { return _GuidSucursalDistribuidora; }
            set { _GuidSucursalDistribuidora = value; }
        }
        /// <summary>
        /// Actualiza el tarifario despues de haber sida entregada la orden
        /// </summary>
        /// <param name="uidOrdenSucursal"></param>
        /// <param name="mPropina"></param>
        public void ModificarTarifario(Guid uidOrdenSucursal, string mPropina)
        {
            oDbTarifario = new DBTarifario();
            oDbTarifario.ActualizaTarifario(uidOrdenSucursal, mPropina);
        }

        /// <summary>
        /// La ruta de la imagen de la empresa distribuidora
        /// </summary>
        private string _StrRuta;

        public string StrRuta
        {
            get { return _StrRuta; }
            set { _StrRuta = value; }
        }
        /// <summary>
        /// Nombre de la Empresa de tarifario
        /// </summary>
        private string _StrNombreEmpresa;
        public string StrNombreEmpresa
        {
            get { return _StrNombreEmpresa; }
            set { _StrNombreEmpresa = value; }
        }
        /// <summary>
        /// Nombre de la sucursal de tarifario
        /// </summary>
        private string _StrNombreSucursal;
        public string StrNombreSucursal
        {
            get { return _StrNombreSucursal; }
            set { _StrNombreSucursal = value; }
        }

        private decimal _MPropina;

        public decimal MPropina
        {
            get { return _MPropina; }
            set { _MPropina = value; }
        }

        public List<VMTarifario> ListaDeTarifarios = new List<VMTarifario>();
        public List<VMTarifario> ListaDeTarifariosSeleccionados = new List<VMTarifario>();

        public Guid UidContrato { get; set; }

        #endregion
        #region Metodos
        public void AgregaALista(Guid UidZonaEntrega, Guid UidZonaRecoleccion, string NombreZE, string NombreZR)
        {
            if (!ListaDeTarifarios.Exists(t => t.UidRelacionZE == UidZonaEntrega && t.UidRelacionZR == UidZonaRecoleccion))
            {
                Guid UidTarifario = Guid.NewGuid();
                ListaDeTarifarios.Add(new VMTarifario() { UidTarifario = UidTarifario, UidRelacionZE = UidZonaEntrega, UidRelacionZR = UidZonaRecoleccion, DPrecio = 0.0m, StrNombreColoniaZE = NombreZE, StrNombreColoniaZR = NombreZR });
            }
        }
        /// <summary>
        /// Selecciona el tarifario a usar mediante el Uid
        /// </summary>
        /// <param name="uidTarifario"></param>
        public void SeleccionarTarifario(Guid uidTarifario, Guid UidContrato, Guid UidSucursal)
        {
            if (!ListaDeTarifariosSeleccionados.Exists(t => t.UidTarifario == uidTarifario))
            {
                oDbTarifario = new DBTarifario();
                foreach (DataRow item in oDbTarifario.RecuperaTarifario(uidTarifario).Rows)
                {
                    Guid UidTarifario = new Guid(item["Uidregistrotarifario"].ToString());
                    Guid UidRelacionZR = new Guid(item["UidRelacionZonaRecolecta"].ToString());
                    Guid UidRelacionZE = new Guid(item["UidRelacionZonaEntrega"].ToString());
                    decimal DPrecio = decimal.Parse(item["MCosto"].ToString());
                    string NombreZonaRecolecta = oDbTarifario.ObtenerNombreColoniaRecolecta(UidRelacionZR.ToString());
                    string NombreZonaEntrega = oDbTarifario.ObtenerNombreColoniaEntrega(UidRelacionZE.ToString());
                    Guid Contrato = Guid.Empty;

                    if (UidContrato != Guid.Empty)
                    {
                        if (ListaDeTarifariosSeleccionados.Exists(t => t.UidContrato == UidContrato))
                        {
                            Contrato = UidContrato;
                        }
                    }
                    var registro = new VMTarifario()
                    {
                        GuidSucursalDistribuidora = UidSucursal,
                        UidTarifario = UidTarifario,
                        UidRelacionZE = UidRelacionZE,
                        UidRelacionZR = UidRelacionZR,
                        DPrecio = DPrecio,
                        UidContrato = Contrato,
                        StrNombreColoniaZE = NombreZonaEntrega,
                        StrNombreColoniaZR = NombreZonaRecolecta
                    };

                    ListaDeTarifariosSeleccionados.Add(registro);
                }
            }
        }
        /// <summary>
        /// Quita de la lista un tarifario mediante el uid
        /// </summary>
        /// <param name="uidTarifario"></param>
        public void DeseleccionarTarifario(Guid uidTarifario = new Guid(), Guid UidContrato = new Guid())
        {
            if (uidTarifario != Guid.Empty)
            {
                if (ListaDeTarifariosSeleccionados.Exists(t => t.UidTarifario == uidTarifario))
                {
                    var objeto = ListaDeTarifariosSeleccionados.Find(t => t.UidTarifario == uidTarifario);
                    ListaDeTarifariosSeleccionados.Remove(item: objeto);
                }
            }
            if (UidContrato != Guid.Empty)
            {
                for (int i = 0; i < ListaDeTarifariosSeleccionados.Count; i++)
                {
                    if (ListaDeTarifariosSeleccionados[i].UidContrato == UidContrato)
                    {
                        ListaDeTarifariosSeleccionados.Remove(ListaDeTarifariosSeleccionados[i]);
                        i = -1;
                    }
                }
            }
        }

        public void ObtenerTarifarioDeOrden(Guid uidOrden)
        {
            oDbTarifario = new DBTarifario();
            foreach (DataRow item in oDbTarifario.ObtenerTarifarioDeOrden(uidOrden).Rows)
            {
                DPrecio = decimal.Parse(item["MCosto"].ToString());
                StrNombreEmpresa = item["NombreComercial"].ToString();
                StrCodigoDeEntrega = item["BIntCodigoEntrega"].ToString();
            }
        }

        /// <summary>
        /// Guarda el tarifario relacionado al contrato
        /// </summary>
        /// <param name="UidContrato">El uid del contrato</param>
        public void GuardaTarifarioDeContrato(Guid UidContrato, Guid UidSucursalDistribuidora)
        {
            if (ListaDeTarifariosSeleccionados.Exists(t => t.GuidSucursalDistribuidora == UidSucursalDistribuidora))
            {
                for (int i = 0; i < ListaDeTarifariosSeleccionados.Count; i++)
                {
                    if (ListaDeTarifariosSeleccionados[i].GuidSucursalDistribuidora == UidSucursalDistribuidora)
                    {
                        ListaDeTarifariosSeleccionados[i].UidContrato = UidContrato;
                    }
                }
            }
        }
        /// <summary>
        /// Agrega el codigo a la orden para que este pueda ser visto por el repartidor 
        /// </summary>
        /// <param name="UidCodigo"></param>
        /// <param name="UidLicencia"></param>
        /// <param name="uidorden"></param>
        public void AgregarCodigoAOrdenTarifario(Guid UidCodigo, Guid UidLicencia, Guid uidorden)
        {
            try
            {
                oTarifario = new Tarifario();
                oTarifario.AgregarCodigoOrden(UidCodigo: UidCodigo, UidLicencia: UidLicencia, uidorden: uidorden);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ActualizaLista(string UidZonaRecolecta, string precio, string ZonaEntrega)
        {
            if (ListaDeTarifarios.Exists(t => t.UidTarifario == new Guid(ZonaEntrega) && t.UidRelacionZR.ToString() == UidZonaRecolecta && t.DPrecio != decimal.Parse(precio)))
            {
                var ViejoValor = ListaDeTarifarios.Find(t => t.UidTarifario == new Guid(ZonaEntrega) && t.UidRelacionZR.ToString() == UidZonaRecolecta);
                ViejoValor.DPrecio = decimal.Parse(precio);
            }
        }
        public void GuardaTarifario()
        {
            for (int i = 0; i < ListaDeTarifarios.Count; i++)
            {
                oTarifario = new Tarifario() { UidTarifario = ListaDeTarifarios[i].UidTarifario, UidRelacionZE = ListaDeTarifarios[i].UidRelacionZE, UidRelacionZR = ListaDeTarifarios[i].UidRelacionZR, DPrecio = ListaDeTarifarios[i].DPrecio };
                oTarifario.Guardar();
            }
        }
        public void GuardaTarifarioConContrato()
        {
            foreach (var item in ListaDeTarifariosSeleccionados)
            {
                if (item.UidContrato != Guid.Empty)
                {
                    oTarifario = new Tarifario() { UidTarifario = item.UidTarifario, UidContrato = item.UidContrato };
                    oTarifario.GuardarTarifarioConContrato();
                }
            }
        }
        public void EliminaTarifarioDeBaseDeDatos(string uidsucursal)
        {
            oDbTarifario = new DBTarifario();
            oDbTarifario.EliminaTarifario(uidsucursal);
        }
        /// <summary>
        /// En este metodo se puede buscar el tarifario solicitado, si es necesario un filtro de rangos de precio solo se agregan las variables de precioinicial y preciofinal
        /// </summary>
        /// <param name="uidSucursal"></param>
        /// <param name="UidZonaRecolecta"></param>
        /// <param name="ZonaEntrega"></param>
        public void BuscarTarifario(string TipoDeBusqueda, string uidSucursal = "", string UidZonaRecolecta = "", string ZonaEntrega = "", string contrato = "", string UidSucursalDistribuidora = "")
        {
            ListaDeTarifarios = new List<VMTarifario>();
            try
            {
                SqlCommand CMD = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "asp_buscarTarifario"
                };

                CMD.Parameters.Add("@StrTipoDeBusqueda", SqlDbType.VarChar, 300);
                CMD.Parameters["@StrTipoDeBusqueda"].Value = TipoDeBusqueda;

                if (!string.IsNullOrEmpty(uidSucursal))
                {
                    CMD.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidSucursal"].Value = new Guid(uidSucursal);
                }
                if (!string.IsNullOrEmpty(UidSucursalDistribuidora))
                {
                    CMD.Parameters.Add("@UidSucursalDistribuidora", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidSucursalDistribuidora"].Value = new Guid(UidSucursalDistribuidora);
                }

                if (!string.IsNullOrEmpty(contrato))
                {
                    CMD.Parameters.Add("@UidContrato", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidContrato"].Value = new Guid(contrato);
                }
                if (!string.IsNullOrEmpty(UidZonaRecolecta))
                {
                    CMD.Parameters.Add("@UidRelacionZR", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidRelacionZR"].Value = new Guid(UidZonaRecolecta);
                }

                if (!string.IsNullOrEmpty(ZonaEntrega))
                {
                    CMD.Parameters.Add("@UidRelacionZE", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidRelacionZE"].Value = new Guid(ZonaEntrega);
                }
                oDbTarifario = new DBTarifario();
                //Recupera el tarifario para cualquier modulo a excepcion del modulo de clientes
                if (string.IsNullOrEmpty(ZonaEntrega))
                {
                    var tarifarios = oDbTarifario.Busquedas(CMD);
                    for (int i = 0; i < tarifarios.Rows.Count; i++)
                    {
                        if (!ListaDeTarifarios.Exists(t => t.UidRelacionZE == new Guid(tarifarios.Rows[i]["UidRelacionZonaEntrega"].ToString()) && t.UidRelacionZR == new Guid(tarifarios.Rows[i]["UidRelacionZonaRecolecta"].ToString())))
                        {
                            ListaDeTarifarios.Add(new VMTarifario()
                            {
                                UidTarifario = new Guid(tarifarios.Rows[i]["Uidregistrotarifario"].ToString()),
                                UidRelacionZE = new Guid(tarifarios.Rows[i]["UidRelacionZonaEntrega"].ToString()),
                                UidRelacionZR = new Guid(tarifarios.Rows[i]["UidRelacionZonaRecolecta"].ToString()),
                                DPrecio = decimal.Parse(tarifarios.Rows[i]["MCosto"].ToString()),
                                StrNombreColoniaZE = oDbTarifario.ObtenerNombreColoniaEntrega(UidRelacionZE.ToString()),
                                StrNombreColoniaZR = oDbTarifario.ObtenerNombreColoniaRecolecta(UidRelacionZR.ToString())
                            });
                        }
                    }
                }
                else
                {
                    foreach (DataRow item in oDbTarifario.Busquedas(CMD).Rows)
                    {
                        Guid UidTarifario = new Guid(item["Uidregistrotarifario"].ToString());
                        Guid UidRelacionZR = new Guid(item["UidRelacionZonaRecolecta"].ToString());
                        Guid UidRelacionZE = new Guid(item["UidRelacionZonaEntrega"].ToString());
                        decimal DPrecio = decimal.Parse(item["MCosto"].ToString());
                        string NombreZonaRecolecta = oDbTarifario.ObtenerNombreColoniaRecolecta(UidRelacionZR.ToString());
                        string NombreZonaEntrega = oDbTarifario.ObtenerNombreColoniaEntrega(UidRelacionZE.ToString());
                        Guid distribudiora = new Guid(item["UidSucursalDistribuidora"].ToString());
                        string NombreSucursal = item["Identificador"].ToString();
                        string NombreEmpresa = item["NombreComercial"].ToString();
                        string Imagen = "../" + item["NVchRuta"].ToString();
                        if (!ListaDeTarifarios.Exists(t => t.UidRelacionZE == UidRelacionZE && t.UidRelacionZR == UidRelacionZR))
                        {
                            ListaDeTarifarios.Add(new VMTarifario() { StrRuta = Imagen, StrNombreSucursal = NombreSucursal, GuidSucursalDistribuidora = distribudiora, UidTarifario = UidTarifario, UidRelacionZE = UidRelacionZE, UidRelacionZR = UidRelacionZR, DPrecio = DPrecio, StrNombreColoniaZE = NombreZonaEntrega, StrNombreColoniaZR = NombreZonaRecolecta, StrNombreEmpresa = NombreEmpresa });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// En este metodo se puede buscar el tarifario solicitado, si es necesario un filtro de rangos de precio solo se agregan las variables de precioinicial y preciofinal
        /// </summary>
        /// <param name="uidSucursal"></param>
        /// <param name="UidZonaRecolecta"></param>
        /// <param name="ZonaEntrega"></param>
        public void BuscarTarifarioDeContrato(string TipoDeBusqueda, string uidSucursal = "", string contrato = "")
        {

            try
            {
                SqlCommand CMD = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "asp_buscarTarifario"
                };

                CMD.Parameters.Add("@StrTipoDeBusqueda", SqlDbType.VarChar, 300);
                CMD.Parameters["@StrTipoDeBusqueda"].Value = TipoDeBusqueda;

                if (!string.IsNullOrEmpty(uidSucursal))
                {
                    CMD.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidSucursal"].Value = new Guid(uidSucursal);
                }

                if (!string.IsNullOrEmpty(contrato))
                {
                    CMD.Parameters.Add("@UidContrato", SqlDbType.UniqueIdentifier);
                    CMD.Parameters["@UidContrato"].Value = new Guid(contrato);
                }
                oDbTarifario = new DBTarifario();
                foreach (DataRow item in oDbTarifario.Busquedas(CMD).Rows)
                {
                    Guid UidTarifario = new Guid(item["Uidregistrotarifario"].ToString());
                    Guid UidRelacionZR = new Guid(item["UidRelacionZonaRecolecta"].ToString());
                    Guid UidRelacionZE = new Guid(item["UidRelacionZonaEntrega"].ToString());
                    decimal DPrecio = decimal.Parse(item["MCosto"].ToString());
                    Guid uidContrato = new Guid(item["UidContrato"].ToString());
                    Guid UidDistribuidora = new Guid(item["UidSucursalDistribuidora"].ToString());
                    string NombreZonaRecolecta = oDbTarifario.ObtenerNombreColoniaRecolecta(UidRelacionZR.ToString());
                    string NombreZonaEntrega = oDbTarifario.ObtenerNombreColoniaEntrega(UidRelacionZE.ToString());
                    if (!ListaDeTarifariosSeleccionados.Exists(t => t.UidRelacionZE == UidRelacionZE && t.UidRelacionZR == UidRelacionZR))
                    {
                        ListaDeTarifariosSeleccionados.Add(new VMTarifario() { GuidSucursalDistribuidora = UidDistribuidora, UidContrato = uidContrato, UidTarifario = UidTarifario, UidRelacionZE = UidRelacionZE, UidRelacionZR = UidRelacionZR, DPrecio = DPrecio, StrNombreColoniaZE = NombreZonaEntrega, StrNombreColoniaZR = NombreZonaRecolecta });
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public void EliminarTarifarioDeSucursal(Guid UidSucursal)
        {
            oDbTarifario = new DBTarifario();
            oDbTarifario.EliminaTarifarioDeSuministradora(UidSucursal.ToString());
        }

        public void AgregarTarifarioOrden(Guid UidOrden, Guid UidTarifario, decimal DPropina)
        {
            oTarifario = new Tarifario();
            oTarifario.RelacionConOrden(uidorden: UidOrden, uidTarifario: UidTarifario, DPropina: DPropina);
        }

        #endregion
    }
}
