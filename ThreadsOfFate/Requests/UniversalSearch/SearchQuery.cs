﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreadsOfFate.Requests.UniversalSearch
{
    [Serializable]
    public class SearchQuery
    {
        /// <summary>
        /// Текстовый поисковый запрос
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Признак выключения проверки текста поискового запроса.
        /// True - проверка не выполняется, подсказки по исправлению не возвращаются клиенту.
        /// False - проверка выполняется, подсказки по исправлению формируются и возвращаются клиенту.
        /// </summary>
        public bool ExactSearch { get; set; }

        public SearchQueryFilter Filter { get; set; }

        /// <summary>
        /// Количество найденных элементов, которые пропускаются - от 1 до Skip включительно.
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// Количество найденных элементов, которые необходимо вернуть.
        /// </summary>
        public int? Take { get; set; }
    }
}
