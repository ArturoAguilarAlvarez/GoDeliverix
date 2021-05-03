using Dapper;
using DataAccess.Common;
using DataAccess.Entities;
using Modelo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EntityTranslationsDb : BaseDapper
    {
        public IEnumerable<EntityTranslation> ReadAllTranslations(EntityType? type = null, Guid? uid = null)
        {
            if (!type.HasValue && !uid.HasValue && uid != Guid.Empty)
                throw new ArgumentException("Invalid values");

            DynamicParameters parameters = new DynamicParameters();
            string where = string.Empty;

            if (type.HasValue)
            {
                parameters.Add("@type", type.Value);
                where += " AND [EntityType] = @type ";
            }

            if (uid.HasValue)
            {
                parameters.Add("@uid", uid.Value);
                where += " AND [EntityUid] = @uid ";
            }

            string query = $@"
select [Id],
       [EntityType],
       [EntityUid],
       [Key],
       [En],
       [Es]
from EntityTranslations
where 1=1 {where}";

            return this.Query<EntityTranslation>(query, parameters);
        }
    }
}
