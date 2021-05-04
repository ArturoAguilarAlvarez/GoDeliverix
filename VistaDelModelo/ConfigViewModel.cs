using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class ConfigViewModel
    {
        private ConfigDb Db { get; }

        public ConfigViewModel()
        {
            this.Db = new ConfigDb();
        }

        public IEnumerable<ConfiguracionClienteMovil> GetConfig(string names)
        {
            return this.Db.GetConfig(names);
        }
    }
}
