using System;
using System.Collections.Generic;
using System.Text;
using ThreadsofFate.Common.Specifications;

namespace ThreadsOfFate.ReadDomain.Specifications.GlobalSearch
{
    public class GetGlobalSearchItemSpecification : ISpecification
    {
        public GetGlobalSearchItemSpecification(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
    
}
