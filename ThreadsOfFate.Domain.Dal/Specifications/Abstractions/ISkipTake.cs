namespace ThreadsOfFate.Domain.Dal.Specifications.Abstractions
{
    public interface ISkipTake
    {
        int? Skip { get; set; }
        int? Take { get; set; }
    }
}
