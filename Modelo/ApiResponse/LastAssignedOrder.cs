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

        public string LatSucursal { get; set; }

        public string LongSucursal { get; set; }

        public string LatCliente { get; set; }

        public string LongCliente { get; set; }

        public string NombreEmpresa { get; set; }

        public string UrlLogoEmpresa { get; set; }

        public string DireccionSucursal { get; set; }

        public LastAssignedOrder()
        {
            this.Productos = new HashSet<DeliveryOrderProductDetail>();
        }
    }
}
