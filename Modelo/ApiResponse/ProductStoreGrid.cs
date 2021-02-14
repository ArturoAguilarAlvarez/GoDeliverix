using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class ProductStoreGrid
    {
        public Guid Uid { get; set; }
        public Guid UidCompany { get; set; }
        public string ImgUrl { get; set; }
        public string CompanyImgUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }

        /// <summary>
        /// Indicador para la disponibilidad de la Seccion del productos
        /// </summary>
        [JsonIgnore]
        public int SecAvailable { get; set; }

        /// <summary>
        /// Indicador para la disponibilidad de la sucursal
        /// </summary>
        [JsonIgnore]
        public int SucAvailable { get; set; }

        /// <summary>
        /// Indicador para la disponibilidad del turno de la suminitradora
        /// </summary>
        [JsonIgnore]
        public int TsAvailable { get; set; }

        /// <summary>
        /// Indicador para la disponibilidad del turnos del distribuidor
        /// </summary>
        /// 
        [JsonIgnore]
        public int TdAvailable { get; set; }

        public bool Available { get; set; }
    }
}
