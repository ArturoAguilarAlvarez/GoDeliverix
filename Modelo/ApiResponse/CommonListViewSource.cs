using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class CommonListViewSource<T>
    {
        public IEnumerable<T> Payload { get; set; }

        public int Count { get; set; }

        public CommonListViewSource()
        {
            this.Payload = new HashSet<T>();
            this.Count = 0;
        }
    }
}
