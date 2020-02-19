using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsOfFate.Domain.Dal.Contexts.Abstractions
{
    public interface IContextTransaction : IDisposable
    {
        void RollBack();
        void Commit();
    }
}
