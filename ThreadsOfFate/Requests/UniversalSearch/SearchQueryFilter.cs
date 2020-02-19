using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreadsOfFate.Requests.UniversalSearch
{
    [Serializable]
    public class SearchQueryFilter
    {
        public string[] ObjectType { get; set; }
        public bool SearchExactMatch { get; set; }
        public bool UseFilter { get; set; }
        public SpellSearchQueryFilter Spell { get; set; }
    }
}
