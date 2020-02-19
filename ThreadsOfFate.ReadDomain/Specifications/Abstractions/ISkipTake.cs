using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsOfFate.ReadDomain.Specifications.Abstractions
{
    public interface ISkipTake
    {
        int? Skip { get; set; }
        int? Take { get; set; }
    }
}
