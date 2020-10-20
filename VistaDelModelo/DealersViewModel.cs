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

        public DealersViewModel()
        {
            this.DealerDb = new DealerDataAccess();
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
    }
}
