using System;
using System.Collections.Generic;
using System.Text;
using ThreadsofFate.Common.Specifications;
using ThreadsOfFate.Domain.Dal.Specifications.Abstractions;

namespace ThreadsOfFate.ReadDomain.Specifications.Filters
{
    public class SearchFilterOptionsSpecification : IOptionsSearch, IOptionsSkipTake, ISpecification
    {
        public string Search { get; set; }
        public bool SearchExactMatch { get; set; }
        public string OptionsSearch { get; set; }
        public int? OptionsSkip { get; set; }
        public int? OptionsTake { get; set; }

        public SpellFilterOptionsSpecification Spell { get; set; }
        
        
    }
}
