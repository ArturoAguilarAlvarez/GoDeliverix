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
    public class ProductViewModel
    {
        private ProductDataAccess ProductDb { get; }

        public ProductViewModel()
        {
            this.ProductDb = new ProductDataAccess();
        }

        public IEnumerable<ProductStoreGrid> ReadAllToStore(StoreSearchRequest request)
        {
            DataTable data = this.ProductDb.ReadAllStore(request.PageSize,
                request.PageNumber,
                request.SortField,
                request.SortDirection,
                request.UidEstado,
                request.UidColonia,
                request.Dia,
                request.TipoFiltro,
                request.UidFiltro,
                request.Filtro,
                request.UidSeccion,
                request.UidOferta,
                request.UidEmpresa);


            List<ProductStoreGrid> result = new List<ProductStoreGrid>();
            foreach (DataRow row in data.Rows)
            {
                result.Add(new ProductStoreGrid()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    UidCompany = row.IsNull("UidCompany") ? Guid.Empty : (Guid)row["UidCompany"],
                    Name = row.IsNull("Name") ? "" : (string)row["Name"],
                    CompanyName = row.IsNull("CompanyName") ? "" : (string)row["CompanyName"],
                    Description = row.IsNull("Description") ? "" : (string)row["Description"],
                    CompanyImgUrl = row.IsNull("CompanyImgUrl") ? "" : (string)row["CompanyImgUrl"],
                    ImgUrl = row.IsNull("ImgUrl") ? "" : (string)row["ImgUrl"],
                    Price = row.IsNull("Price") ? 0 : (decimal)row["Price"]
                });
            }

            return result;
        }
    }
}
