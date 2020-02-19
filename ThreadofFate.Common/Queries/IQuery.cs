using System.Threading.Tasks;
using ThreadsofFate.Common.Specifications;

namespace ThreadsofFate.Common.Queries
{
    public interface IQuery<TResult>
        where TResult : class
    {
        Task<TResult> Ask();
    }

    public interface IQuery<in TSpecification, TResult>
        where TSpecification : ISpecification
    {
        Task<TResult> Ask(TSpecification specification);
    }
}
