using DataAccess.Common;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ConfigDb : BaseDapper
    {
        public IEnumerable<ConfiguracionClienteMovil> GetConfig(string names)
        {
            IEnumerable<string> ltsNames = names.Split(';');
            string query = $"select * from ConfiguracionClienteMovil where VchNombre in ({string.Join(",", ltsNames.Select(t => $"'{t}'"))})";
            return this.Query<ConfiguracionClienteMovil>(query, null);
        }
    }
}
