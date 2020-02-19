using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsofFate.Common.Specifications
{
    public class GetItemSpecification : ISpecification
    {
        public Guid Id { get; }

        public GetItemSpecification(Guid id)
        {
            Id = id;
        }
    }
}
