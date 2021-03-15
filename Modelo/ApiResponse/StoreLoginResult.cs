using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class StoreLoginResult
    {
        public Guid Uid { get; set; }
        public Guid UidPerfil { get; set; }
        public string Name { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string Usuario { get; set; }
        public string Email { get; set; }
        public string ProfileName { get; set; }

        public IEnumerable<AddressCustomer> Addresses { get; set; }

        public StoreLoginResult()
        {
            this.Addresses = new HashSet<AddressCustomer>();
        }
    }
}
