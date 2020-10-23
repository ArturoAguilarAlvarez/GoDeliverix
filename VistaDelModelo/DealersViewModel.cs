using DBControl;
using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    /// <summary>
    /// Vista del modelo del repartidor (version 2)
    /// </summary>
    public class DealersViewModel
    {
        private DealerDataAccess DealerDb { get; }

        public const string OrdenConfirmada = "A42B2588-D650-4DD9-829D-5978C927E2ED";
        public const string OrdenRecolectada = "B6791F2C-FA16-40C6-B5F5-123232773612";
        public const string OrdenEntregada = "7DA3A42F-2271-47B4-B9B8-EDD311F56864";
        public const string OrdenRechazada = "12748F8A-E746-427D-8836-B54432A38C07";
        public const string OrdenPendiente = "6294DACE-C9D1-4F9F-A942-FF12B6E7E957";

        public DealersViewModel()
        {
            this.DealerDb = new DealerDataAccess();
        }

        public bool Update(RepartidorUpdate update)
        {
            if (string.IsNullOrEmpty(update.Nombre)
                && string.IsNullOrEmpty(update.ApellidoPaterno)
                && string.IsNullOrEmpty(update.ApellidoMaterno)
                && !update.FechaNacimiento.HasValue)
            {
                return false;
            }

            return this.DealerDb.Update(update.Uid, update.Nombre, update.ApellidoPaterno, update.ApellidoMaterno, update.FechaNacimiento);
        }

        #region Address
        public IEnumerable<Address> ReadAllAddresses(Guid uidUser)
        {
            List<Address> addresses = new List<Address>();
            DataTable data = this.DealerDb.ReadAllAddressesByUserId(uidUser);

            foreach (DataRow row in data.Rows)
            {
                addresses.Add(new Address()
                {
                    Uid = (Guid)row["UidDireccion"],
                    UidPais = (Guid)row["UidPais"],
                    UidEstado = (Guid)row["UidEstado"],
                    UidMunicipio = (Guid)row["UidMunicipio"],
                    UidCiudad = (Guid)row["UidCiudad"],
                    UidColonia = (Guid)row["UidColonia"],
                    Calle0 = row.IsNull("Calle0") ? "" : (string)row["Calle0"],
                    Calle1 = row.IsNull("Calle1") ? "" : (string)row["Calle1"],
                    Calle2 = row.IsNull("Calle2") ? "" : (string)row["Calle2"],
                    CodigoPostal = row.IsNull("CodigoPostal") ? "" : (string)row["CodigoPostal"],
                    Estatus = row.IsNull("BEstatus") ? false : (bool)row["BEstatus"],
                    Identificador = row.IsNull("Identificador") ? "" : (string)row["Identificador"],
                    Lote = row.IsNull("Lote") ? "" : (string)row["Lote"],
                    Manzana = row.IsNull("Manzana") ? "" : (string)row["Manzana"],
                    Predeterminada = row.IsNull("BPredeterminada") ? false : (bool)row["BPredeterminada"],
                    Referencia = row.IsNull("Referencia") ? "" : (string)row["Referencia"]
                });
            }
            return addresses;
        }
        #endregion

        #region Telefonos
        public IEnumerable<Phone> ReadAllPhones(Guid uidUser)
        {
            List<Phone> phones = new List<Phone>();
            DataTable data = this.DealerDb.ReadAllPhonesByUserId(uidUser);

            foreach (DataRow row in data.Rows)
            {
                phones.Add(new Phone()
                {
                    Uid = (Guid)row["UidTelefono"],
                    UidTipo = (Guid)row["UidTipoDeTelefono"],
                    Tipo = row.IsNull("Numero") ? "" : (string)row["Numero"],
                    Numero = row.IsNull("Tipo") ? "" : (string)row["Tipo"]
                });
            }

            return phones;
        }
        #endregion

        #region Turnos
        public LastWorkShift GetLastWorkShift(Guid uidUser)
        {
            LastWorkShift result = null;
            DataTable data = this.DealerDb.GetLastWorkShiftByUserId(uidUser);
            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    result = new LastWorkShift()
                    {
                        Uid = (Guid)row["Uid"],
                        UidUsuario = (Guid)row["UidUsuario"],
                        UidEstatusActual = row.IsNull("UidEstatusActual") ? Guid.Empty : (Guid)row["UidEstatusActual"],
                        Folio = row.IsNull("Folio") ? 0 : (int)row["Folio"],
                        Fondo = row.IsNull("Fondo") ? 0 : (decimal)row["Fondo"],
                        FechaInicio = row.IsNull("FechaInicio") ? DateTime.Now : (DateTime)row["FechaInicio"],
                        FechaFin = row.IsNull("FechaFin") ? null : (DateTime?)row["FechaFin"]
                    };
                }
            }
            return result;
        }
        #endregion

        #region Ordenes
        public LastAssignedOrder ReadLastAssignedOrder(Guid uidTurnoRepartidor)
        {
            LastAssignedOrder result = null;
            DataTable data = this.DealerDb.ReadLastAssignedOrder(uidTurnoRepartidor);
            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    result = new LastAssignedOrder()
                    {
                        UidOrdenRepartidor = (Guid)row["UidOrdenRepartidor"],
                        UidOrdenTarifario = (Guid)row["UidOrdenTarifario"],
                        UidOrdenSucursal = (Guid)row["UidOrdenSucursal"],
                        UidSucursal = (Guid)row["UidSucursal"],
                        IdentificadorSucursal = row.IsNull("IdentificadorSucursal") ? "" : (string)row["IdentificadorSucursal"],
                        FolioOrdenSucursal = row.IsNull("FolioOrdenSucursal") ? "" : (string)row["FolioOrdenSucursal"],
                        UidDireccionCliente = (Guid)row["UidDireccionCliente"],
                        UidEstatusOrdenGeneral = (Guid)row["UidEstatusOrdenGeneral"],
                        UidEstatusOrdenRepartidor = (Guid)row["UidEstatusOrdenRepartidor"],
                        UidEstatusOrdenTarifario = (Guid)row["UidEstatusOrdenTarifario"],
                        UidOrden = (Guid)row["UidOrden"]
                    };
                }
            }

            if (result != null)
            {
                DataTable dataProducts = this.DealerDb.ReadAllProductOrder(result.UidOrden);
                List<DeliveryOrderProductDetail> products = new List<DeliveryOrderProductDetail>();
                foreach (DataRow row in dataProducts.Rows)
                {
                    products.Add(new DeliveryOrderProductDetail()
                    {
                        Nombre = (string)row["Nombre"],
                        Cantidad = (int)row["Cantidad"]
                    });
                }
                result.Productos = products.AsEnumerable();
            }

            return result;
        }
        #endregion
    }
}
