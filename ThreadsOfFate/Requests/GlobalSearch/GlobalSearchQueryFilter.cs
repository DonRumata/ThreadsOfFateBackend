using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreadsOfFate.Requests.GlobalSearch
{
    public class GlobalSearchQueryFilter
    {
        public string[] ObjectType { get; set; }
        public bool SearchExactMatch { get; set; }
        public bool UseFilter { get; set; }
        public SpellGlobalSearchQueryFilter Spell { get; set; }
    }
}
