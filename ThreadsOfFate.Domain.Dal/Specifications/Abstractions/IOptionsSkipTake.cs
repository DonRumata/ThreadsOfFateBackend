namespace ThreadsOfFate.Domain.Dal.Specifications.Abstractions
{
    public interface IOptionsSkipTake
    {
        int? OptionsSkip { get; set; }
        int? OptionsTake { get; set; }
    }
}
