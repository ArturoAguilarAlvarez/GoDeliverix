using DataAccess;
using DataAccess.Models;
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

        public IEnumerable<ListboxView> ReadAllCountries(string defaultValue = "")
        {
            var result = this.DbAddress.ReadAllCountries();
            if (defaultValue != string.Empty)
            {
                var tmp = result.ToList();
                tmp.Insert(0, new ListboxView() { Uid = Guid.Empty, Name = defaultValue });
                result = tmp;
            }
            return result;
        }
        public IEnumerable<ListboxView> ReadAllStates(Guid uid, string defaultValue = "")
        {
            var result = this.DbAddress.ReadAllStates(uid);
            if (defaultValue != string.Empty)
            {
                var tmp = result.ToList();
                tmp.Insert(0, new ListboxView() { Uid = Guid.Empty, Name = defaultValue });
                result = tmp;
            }
            return result;
        }
        public IEnumerable<ListboxView> ReadAllMunicipalities(Guid uid, string defaultValue = "")
        {
            var result = this.DbAddress.ReadAllMunicipalities(uid);
            if (defaultValue != string.Empty)
            {
                var tmp = result.ToList();
                tmp.Insert(0, new ListboxView() { Uid = Guid.Empty, Name = defaultValue });
                result = tmp;
            }
            return result;
        }
        public IEnumerable<ListboxView> ReadAllCities(Guid uid, string defaultValue = "")
        {
            var result = this.DbAddress.ReadAllCities(uid);
            if (defaultValue != string.Empty)
            {
                var tmp = result.ToList();
                tmp.Insert(0, new ListboxView() { Uid = Guid.Empty, Name = defaultValue });
                result = tmp;
            }
            return result;
        }
        public IEnumerable<ListboxView> ReadAllNeighborhoods(Guid uid, string defaultValue = "")
        {
            var result = this.DbAddress.ReadAllNeighborhoods(uid);
            if (defaultValue != string.Empty)
            {
                var tmp = result.ToList();
                tmp.Insert(0, new ListboxView() { Uid = Guid.Empty, Name = defaultValue });
                result = tmp;
            }
            return result;
        }
    }
}
