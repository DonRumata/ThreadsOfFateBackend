using System;
using System.Collections.Generic;
using System.Text;
using ThreadsOfFate.ReadDomain.Specifications.Elastic;

namespace ThreadsOfFate.ReadDomain.Specifications.GlobalSearch
{
    public class GlobalSearchFilterSpecification
    {
        public string[] ObjectType { get; set; }
        public bool SearchExactMatch { get; set; }
        public bool UseFilter { get; set; }
        public SpellGlobalSearchFilterSpecification Spell { get; set; }
    }
}
