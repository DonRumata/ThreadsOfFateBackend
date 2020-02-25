using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreadsOfFate.Requests.SearchFilterOptions
{
    public class SearchFilterOptionsQuery
    {
        public string Search { get; set; }
        public bool SearchExactMatch { get; set; }

        /// <summary>
        /// Текстовый поисковый запрос для опции фильтра.
        /// </summary>
        public string OptionsSearch { get; set; }

        /// <summary>
        /// Количество найденных элементов опций, которые пропускаются - от 1 до Skip включительно.
        /// </summary>
        public int? OptionsSkip { get; set; }

        /// <summary>
        /// Количество найденных элементов опций, которые необходимо вернуть.
        /// </summary>
        public int? OptionsTake { get; set; }

        private SpellSearchFilterOptionsFilter Spell { get; set; }
    }
}
