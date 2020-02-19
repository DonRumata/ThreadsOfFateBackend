using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsOfFate.ReadDomain.Model
{
    public class CollectionDto<TItem>
    {
        /// <summary>
        /// Коллекция найденных и возвращаемых объектов.
        /// </summary>
        public ICollection<TItem> Items { get; set; }

        /// <summary>
        /// Общее количество найденных объектов удовлетворяющих критериям поиска.
        /// </summary>
        public long? Count { get; set; }
    }
}
