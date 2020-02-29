using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsOfFate.Domain.Dal.Dto.SearchStructure
{
    [Serializable]
    public class SearchItemDto
    {
        public string Id { get; set; }
        public string Hash { get; set; }
        public string ObjectType { get; set; }
        public string Source { get; set; }
        public string[] Highlights { get; set; }
        public SearchContentItemDto Content { get; set; }
    }
}
