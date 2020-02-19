using System;
using System.Linq;

namespace ThreadsofFate.Common.Extensions
{
    public static class StringExtensions
    {
        public static string[] SplitToWordsBySpace(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Array.Empty<string>();

            return value.Split(' ')
                .Select(w => w.Trim())
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToArray();
        }

        /// <summary>
        /// Удаляем суррогатные символы.
        /// <seealso cref="http://docs.microsoft.com/ru-ru/dotnet/api/system.char.issurrogate?view=netcore-2.2"/>
        /// </summary>
        public static string StripSurrogates(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            var chars = value.Where(c => !char.IsSurrogate(c)).ToArray();
            return new string(chars);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
