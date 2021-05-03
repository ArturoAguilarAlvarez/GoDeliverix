using Modelo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class EntityTranslations
    {
        public int Uid { get; set; }
        public EntityType EntityType { get; set; }
        public Guid EntityUid { get; set; }
        public string Key { get; set; }
        public string En { get; set; }
        public string Es { get; set; }
    }
}
