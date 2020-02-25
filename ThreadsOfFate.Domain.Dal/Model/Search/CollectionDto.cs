using System.Collections.Generic;

namespace ThreadsOfFate.Domain.Dal.Model.Search
{
    public class CollectionDto<TItem>
    {
        /// <summary>
        /// Коллекция найденных и возвращаемых объектов.
        /// </summary>
        public IEnumerable<TItem> Items { get; set; }

        /// <summary>
        /// Общее количество найденных объектов удовлетворяющих критериям поиска.
        /// </summary>
        public long? Count { get; set; }
    }
}
