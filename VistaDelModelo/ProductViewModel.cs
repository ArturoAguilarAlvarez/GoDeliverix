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

        public CommonListViewSource<ProductStoreGrid> ReadAllToStore(StoreSearchRequest request)
        {
            CommonListViewSource<ProductStoreGrid> result = new CommonListViewSource<ProductStoreGrid>() { };

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


            List<ProductStoreGrid> products = new List<ProductStoreGrid>();

            foreach (DataRow row in data.Rows)
            {
                products.Add(new ProductStoreGrid()
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

            result.Payload = products;
            result.Count = data.Rows.Count > 0 ? (int)data.Rows[0]["Count"] : 0;

            return result;
        }

        public CommonListViewSource<ProductStoreGrid> ReadAllToStoreVersion2(StoreSearchRequest request)
        {
            CommonListViewSource<ProductStoreGrid> result = new CommonListViewSource<ProductStoreGrid>() { };

            DataTable data = this.ProductDb.ReadAllStoreVersion2(request.PageSize,
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


            List<ProductStoreGrid> products = new List<ProductStoreGrid>();

            foreach (DataRow row in data.Rows)
            {
                products.Add(new ProductStoreGrid()
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

            result.Payload = products;
            result.Count = data.Rows.Count > 0 ? (int)data.Rows[0]["Count"] : 0;

            return result;
        }

        public CommonListViewSource<ProductStoreGrid> ReadAllToStoreVersion3(StoreSearchRequest request)
        {
            CommonListViewSource<ProductStoreGrid> result = new CommonListViewSource<ProductStoreGrid>() { };

            DataTable data = this.ProductDb.ReadAllStoreVersion3(request.PageSize,
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
                request.UidEmpresa,
                request.Available);


            List<ProductStoreGrid> products = new List<ProductStoreGrid>();

            foreach (DataRow row in data.Rows)
            {
                products.Add(new ProductStoreGrid()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    UidCompany = row.IsNull("UidCompany") ? Guid.Empty : (Guid)row["UidCompany"],
                    Name = row.IsNull("Name") ? "" : (string)row["Name"],
                    CompanyName = row.IsNull("CompanyName") ? "" : (string)row["CompanyName"],
                    Description = row.IsNull("Description") ? "" : (string)row["Description"],
                    CompanyImgUrl = row.IsNull("CompanyImgUrl") ? "" : (string)row["CompanyImgUrl"],
                    ImgUrl = row.IsNull("ImgUrl") ? "" : (string)row["ImgUrl"],
                    Price = row.IsNull("Price") ? 0 : (decimal)row["Price"],
                    Available = (bool)row["Available"]
                });
            }

            result.Payload = products;
            result.Count = data.Rows.Count > 0 ? (int)data.Rows[0]["Count"] : 0;

            return result;
        }

        public CommonListViewSource<ProductStoreGrid> ReadAllToStoreStoreProcedure(StoreSearchRequest request)
        {
            CommonListViewSource<ProductStoreGrid> result = new CommonListViewSource<ProductStoreGrid>() { };

            DataTable data = this.ProductDb.ReadAllStoreStoreProcedure(request.PageSize,
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


            List<ProductStoreGrid> products = new List<ProductStoreGrid>();

            foreach (DataRow row in data.Rows)
            {
                products.Add(new ProductStoreGrid()
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

            result.Payload = products;
            result.Count = data.Rows.Count > 0 ? (int)data.Rows[0]["Count"] : 0;

            return result;
        }

        public CommonListViewSource<CompanyStoreGrid> ReadAllCompaniesToStore(CompaniesSearchRequest request)
        {
            CommonListViewSource<CompanyStoreGrid> result = new CommonListViewSource<CompanyStoreGrid>() { };

            DataTable data = this.ProductDb.ReadAllStores(request.PageSize,
                request.PageNumber,
                request.SortField,
                request.SortDirection,
                request.UidEstado,
                request.UidColonia,
                request.Dia,
                request.TipoFiltro,
                request.UidFiltro,
                request.Filtro);


            List<CompanyStoreGrid> companies = new List<CompanyStoreGrid>();

            foreach (DataRow row in data.Rows)
            {
                companies.Add(new CompanyStoreGrid()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    Name = row.IsNull("Name") ? "" : (string)row["Name"],
                    ImgUrl = row.IsNull("ImgUrl") ? "" : (string)row["ImgUrl"],
                    AvailableBranches = row.IsNull("AvailableBranches") ? 0 : (int)row["AvailableBranches"],
                });
            }

            result.Payload = companies;
            result.Count = data.Rows.Count > 0 ? (int)data.Rows[0]["Count"] : 0;

            return result;
        }

        public IEnumerable<CompanyBranch> ReadAllCompanyBranches(Guid uidEmpresa, Guid uidEstado, Guid uidColonia)
        {
            List<CompanyBranch> result = new List<CompanyBranch>() { };

            DataTable data = this.ProductDb.ReadAllCompanyBranch(uidEmpresa, uidEstado, uidColonia);

            foreach (DataRow row in data.Rows)
            {
                result.Add(new CompanyBranch()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    Identifier = row.IsNull("Identifier") ? "" : (string)row["Identifier"],
                    OpenAt = row.IsNull("CloseAt") ? "" : (string)row["CloseAt"],
                    CloseAt = row.IsNull("OpenAt") ? "" : (string)row["OpenAt"],
                    Available = row.IsNull("Available") ? false  : (bool)row["Available"],
                    Status  = row.IsNull("Status") ? 0 : (int)row["Status"]
                });
            }

            return result;
        }
    }
}
