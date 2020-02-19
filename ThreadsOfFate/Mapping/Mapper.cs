using AutoMapper;

namespace ThreadsOfFate.Mapping
{
    internal static class Mapper
    {
        private static readonly IMapper Instance;

        static Mapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            Instance = config.CreateMapper();
        }

        public static TDest Map<TSource, TDest>(TSource source)
        {
            return Instance.Map<TSource, TDest>(source);
        }

        public static TDest Update<TSource, TDest>(TDest dest, TSource source)
        {
            return Instance.Map(source, dest);
        }
    }
}
