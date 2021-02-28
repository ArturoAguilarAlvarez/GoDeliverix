using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class CompaniesSearchRequest
    {
        public int PageSize { get; set; } = 25;
        public int PageNumber { get; set; } = 0;

        public string SortField { get; set; }
        public string SortDirection { get; set; }

        public Guid UidEstado { get; set; }
        public Guid UidColonia { get; set; }
        public string Dia { get; set; }

        public string TipoFiltro { get; set; }
        public Guid UidFiltro { get; set; }
        public string Filtro { get; set; } = null;

        public bool? Available { get; set; } = null;
    }
}
