using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questao2.Models
{
    public class PaginatedResponse
    {
        public int page { get; set; }

        public int per_page { get; set; }

        public int total { get; set; }

        public int total_pages { get; set; }

        public object data { get; set; }
    }
}