using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsofFate.Common.Services.Abstractions
{
    public interface IElasticObjectTypeToAliasMapService
    {
        int TextSearchObjectTypeCount { get; }
        void CheckTextSearchObjectTypeOrThrow(string[] objectType);
        void CheckObjectTypeOrThrow(string[] objectType);
        string GetTextSearchIndexName();
        bool AreAllSearchObjectTypes(string[] objectTypes);
    }
}
