using System;

namespace ThreadsOfFate.ReadDomain.Services.Abstractions
{
    class ServiceBase
    {
        protected void CheckSpecificationIsNotNullOrThrow<TSpecification>(TSpecification specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));
        }
    }
}
