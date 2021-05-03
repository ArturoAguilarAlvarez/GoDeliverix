using DataAccess;
using DataAccess.Entities;
using Modelo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class TranslationsViewModel
    {
        protected EntityTranslationsDb Db { get; }

        public TranslationsViewModel()
        {
            this.Db = new EntityTranslationsDb();
        }

        public IEnumerable<EntityTranslation> ReadAllTranslations(EntityType? type = null, Guid? uid = null)
        {
            return this.Db.ReadAllTranslations(type, uid);
        }
    }
}
