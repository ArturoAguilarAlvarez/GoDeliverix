using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class OrderHistoryDetailTimeLine
    {
        public Guid StatusUid { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
