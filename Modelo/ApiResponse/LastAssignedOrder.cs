using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    /// <summary>
    /// Ultima orden asignada al repartidor
    /// </summary>
    public class LastAssignedOrder
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public Guid UidOrdenRepartidor { get; set; }

        public Guid UidOrden { get; set; }

        public Guid UidOrdenSucursal { get; set; }

        public Guid UidOrdenTarifario { get; set; }

        public Guid UidSucursal { get; set; }

        public Guid UidDireccionCliente { get; set; }

        public Guid UidEstatusOrdenTarifario { get; set; }

        public Guid UidEstatusOrdenGeneral { get; set; }

        public Guid UidEstatusOrdenRepartidor { get; set; }

        public string IdentificadorSucursal { get; set; }

        public string FolioOrdenSucursal { get; set; }

        public IEnumerable<DeliveryOrderProductDetail> Productos { get; set; }

        public LastAssignedOrder()
        {
            this.Productos = new HashSet<DeliveryOrderProductDetail>();
        }
    }
}
