using DataAccess;
using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class AddressViewModel
    {
        private AddressDb DbAddress { get; }

        public AddressViewModel()
        {
            this.DbAddress = new AddressDb();
        }

        public IEnumerable<AddressCustomer> GetAllByUserId(Guid uid)
        {
            return this.DbAddress.ReadAllUserAddress(uid);
        }

        public IEnumerable<NeighborhoodSearch> SearchNeighborhood(string filter)
        {
            return this.DbAddress.SearchNeighborhood(filter);
        }
    }
}
