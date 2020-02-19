using System;
using System.Collections.Generic;
using System.Text;
using ThreadsofFate.Common.Specifications;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Specifications.Elastic
{
    public class GlobalSearchElasticCollectionSpecification : ISpecification
    {
        public GlobalSearchCollectionSpecification Specification { get; set; }
    }
}
