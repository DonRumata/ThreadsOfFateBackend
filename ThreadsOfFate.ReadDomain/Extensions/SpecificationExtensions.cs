using System.Linq;
using ThreadsofFate.Common.Extensions;
using ThreadsOfFate.Domain.Dal.Specifications.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.Abstractions.UniversalSearch;
using IOptionsSearch = ThreadsOfFate.ReadDomain.Specifications.Abstractions.Elastic.IOptionsSearch;

namespace ThreadsOfFate.ReadDomain.Extensions
{
    static class SpecificationExtensions
    {
        public static void AdjustSkipTake(this ISkipTake specification)
        {
            if (specification.Skip == null || specification.Skip < 0)
                specification.Skip = 0;

            if (specification.Take == null || specification.Take <= 0)
                specification.Take = 20;
            else if (specification.Take > 100)
                specification.Take = 100;
        }


        public static void AdjustOptionsSkipTake(this ISkipTake specification)
        {
            if (specification.Skip == null || specification.Take < 0)
                specification.Skip = 0;

            if (specification.Take == null || specification.Take <= 0)
                specification.Take = 20;
            else if (specification.Take > 100)
                specification.Take = 100;
        }

        public static void AdjustSearchForElastic(this IGlobalSearch specification)
        {
            if (string.IsNullOrWhiteSpace(specification.GlobalSearchText))
            {
                specification.GlobalSearchText = null;
                return;
            }

            specification.GlobalSearchText = NormalizeSearchText(specification.GlobalSearchText);
        }

        public static void NormalizeQuery(this ISearch specification)
        {
            if (string.IsNullOrWhiteSpace(specification.Search))
            {
                specification.Search = null;
                return;
            }

            specification.Search = NormalizeSearchText(specification.Search);
        }

        public static void AdjustOptionsSearchForElastic(this IOptionsSearch specification)
        {
            if (string.IsNullOrWhiteSpace(specification.OptionsSearch))
            {
                specification.OptionsSearch = null;
                return;
            }

            specification.OptionsSearch = NormalizeSearchText(specification.OptionsSearch);
        }

        public static bool HasSearch(this IGlobalSearch specification)
        {
            return !string.IsNullOrWhiteSpace(specification.GlobalSearchText);
        }

        /// <summary>
        /// Удаляем лишние пробелы и суррогатные символы.
        /// </summary>
        private static string NormalizeSearchText(string query)
        {
            var words = query.SplitToWordsBySpace();
            words = words
                .Select(w => w.StripSurrogates())
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToArray();

            return string.Join(" ", words);
        }
    }
}
