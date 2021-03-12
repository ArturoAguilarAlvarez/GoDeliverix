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
        private OfertaDataAccess OfertaDb { get; }
        private SeccionDataAccess SeccionDb { get; }

        public ProductViewModel()
        {
            this.ProductDb = new ProductDataAccess();
            this.OfertaDb = new OfertaDataAccess();
            this.SeccionDb = new SeccionDataAccess();
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
                request.Filtro,
                request.Available);


            List<CompanyStoreGrid> companies = new List<CompanyStoreGrid>();

            foreach (DataRow row in data.Rows)
            {
                companies.Add(new CompanyStoreGrid()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    Name = row.IsNull("Name") ? "" : (string)row["Name"],
                    ImgUrl = row.IsNull("ImgUrl") ? "" : (string)row["ImgUrl"],
                    AvailableBranches = row.IsNull("AvailableBranches") ? 0 : (int)row["AvailableBranches"],
                    Available = row.IsNull("Available") ? false : (bool)row["Available"],
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
                    Available = row.IsNull("Available") ? false : (bool)row["Available"],
                    Status = row.IsNull("Status") ? 0 : (int)row["Status"]
                });
            }

            return result;
        }

        public CompanyDetail GetCompanyDetail(Guid uidEmpresa, Guid uidEstado, Guid uidColonia)
        {
            CompanyDetail result = new CompanyDetail();

            DataTable companyDt = this.ProductDb.Company(uidEmpresa);
            foreach (DataRow row in companyDt.Rows)
            {
                result.Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"];
                result.Name = row.IsNull("Name") ? "" : (string)row["Name"];
                result.ImgUrl = row.IsNull("ImgUrl") ? "" : (string)row["ImgUrl"];
            }

            DataTable branchesDt = this.ProductDb.ReadAllCompanyBranch(uidEmpresa, uidEstado, uidColonia);

            List<CompanyBranch> branches = new List<CompanyBranch>();
            foreach (DataRow row in branchesDt.Rows)
            {
                branches.Add(new CompanyBranch()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    Identifier = row.IsNull("Identifier") ? "" : (string)row["Identifier"],
                    OpenAt = row.IsNull("CloseAt") ? "" : (string)row["CloseAt"],
                    CloseAt = row.IsNull("OpenAt") ? "" : (string)row["OpenAt"],
                    Available = row.IsNull("Available") ? false : (bool)row["Available"],
                    Status = row.IsNull("Status") ? 0 : (int)row["Status"]
                });
            }

            result.Branches = branches.AsEnumerable();

            return result;
        }

        public IEnumerable<OfertaListBox> GetBranchDeals(Guid uidSucursal, string dia)
        {
            List<OfertaListBox> result = new List<OfertaListBox>() { };

            DataTable data = this.OfertaDb.ObtenerOfertasSucursal(dia, uidSucursal);

            foreach (DataRow row in data.Rows)
            {
                result.Add(new OfertaListBox()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    Available = row.IsNull("Available") ? false : (bool)row["Available"],
                    Status = row.IsNull("Status") ? 0 : (int)row["Status"],
                    Name = row.IsNull("Name") ? "" : (string)row["Name"],
                });
            }

            return result;
        }

        public IEnumerable<SeccionListbox> GetDealSections(Guid uidOferta, Guid uidEstado)
        {
            List<SeccionListbox> result = new List<SeccionListbox>() { };

            DataTable data = this.SeccionDb.ObtenerSeccionesOfertas(uidEstado, uidOferta);

            foreach (DataRow row in data.Rows)
            {
                result.Add(new SeccionListbox()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    Available = row.IsNull("Available") ? false : (bool)row["Available"],
                    Name = row.IsNull("Name") ? "" : (string)row["Name"],
                });
            }

            return result;
        }

        public StoreProductDetail GetProductDetail(ProductDetailRequest vm)
        {
            StoreProductDetail result = new StoreProductDetail();

            DataTable pData = this.ProductDb.GetProductDetail(vm.UidProducto, vm.UidEstado, vm.UidColonia, vm.Dia);
            foreach (DataRow row in pData.Rows)
            {
                result.Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"];
                result.Name = row.IsNull("Name") ? string.Empty : (string)row["Name"];
                result.Description = row.IsNull("Description") ? string.Empty : (string)row["Description"];
                result.ImgUrl = row.IsNull("ImgUrl") ? string.Empty : (string)row["ImgUrl"];
                result.Price = row.IsNull("Price") ? 0 : (decimal)row["Price"];
                result.CompanyName = row.IsNull("CompanyName") ? "" : (string)row["CompanyName"];
            }

            DataTable pBranch = this.ProductDb.GetProductBranches(vm.UidProducto, vm.UidEstado, vm.UidColonia, vm.Dia);
            List<StoreProductBranch> branches = new List<StoreProductBranch>();
            foreach (DataRow row in pBranch.Rows)
            {
                branches.Add(new StoreProductBranch()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    Identifier = row.IsNull("Identifier") ? string.Empty : (string)row["Identifier"],
                    Price = row.IsNull("Price") ? 0 : (decimal)row["Price"],
                    ClosedAt = row.IsNull("ClosedAt") ? string.Empty : (string)row["ClosedAt"],
                    OpenAt = row.IsNull("OpenAt") ? string.Empty : (string)row["OpenAt"],
                    ShortAddress = row.IsNull("ShortAddress") ? string.Empty : (string)row["ShortAddress"],
                    UidCommissionType = row.IsNull("UidCommissionType") ? Guid.Empty : (Guid)row["UidCommissionType"],
                    ViewInformation = row.IsNull("ViewInformation") ? false : (bool)row["ViewInformation"],
                    BranchIncludeCardPaymentCommission = row.IsNull("BranchIncludeCardPaymentCommission") ? false : (bool)row["BranchIncludeCardPaymentCommission"],
                });
            }
            result.Branches = branches.AsEnumerable();

            return result;
        }

        #region Giros        
        public IEnumerable<StoreGiro> GetAllGiros()
        {
            List<StoreGiro> result = new List<StoreGiro>() { };

            DataTable data = this.ProductDb.GetGiros();

            foreach (DataRow row in data.Rows)
            {
                result.Add(new StoreGiro()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    Name = row.IsNull("Name") ? "" : (string)row["Name"],
                    Description = row.IsNull("Description") ? "" : (string)row["Description"],
                    ImgUrl = row.IsNull("ImgUrl") ? "" : (string)row["ImgUrl"],
                });
            }

            return result;
        }

        public IEnumerable<CommonListBox> GetCategorias(Guid uid)
        {
            List<CommonListBox> result = new List<CommonListBox>() { };

            DataTable data = this.ProductDb.GetCategories(uid);

            foreach (DataRow row in data.Rows)
            {
                result.Add(new CommonListBox()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    Name = row.IsNull("Name") ? "" : (string)row["Name"]
                });
            }

            return result;
        }

        public IEnumerable<CommonListBox> GetSubcategorias(Guid uid)
        {
            List<CommonListBox> result = new List<CommonListBox>() { };

            DataTable data = this.ProductDb.GetSubcategories(uid);

            foreach (DataRow row in data.Rows)
            {
                result.Add(new CommonListBox()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    Name = row.IsNull("Name") ? "" : (string)row["Name"]
                });
            }

            return result;
        }
        #endregion

    }
}
