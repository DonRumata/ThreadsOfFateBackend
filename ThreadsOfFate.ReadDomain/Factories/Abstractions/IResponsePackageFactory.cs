using System;
using System.Collections.Generic;
using System.Text;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch.Search;

namespace ThreadsOfFate.ReadDomain.Factories.Abstractions
{
    interface IResponsePackageFactory
    {
        void BuildFilterQuery(SearchFilterSpecification specification);
    }
}
