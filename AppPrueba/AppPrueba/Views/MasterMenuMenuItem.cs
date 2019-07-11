using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPrueba.Views
{

    public class MasterMenuMenuItemMenuItem
    {
        public MasterMenuMenuItemMenuItem()
        {
            TargetType = typeof(MasterMenuDetailDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public string UrlResource { get; set; }

        public Type TargetType { get; set; }
    }
}