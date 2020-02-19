using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreadsofFate.Common.Const;
using ThreadsofFate.Common.Services.Abstractions;

namespace ThreadsofFate.Common.Services
{
    class ElasticObjectTypeToAliasMapService : IElasticObjectTypeToAliasMapService
    {
        private readonly Dictionary<string, string> _textSearchObjectTypeToAliasMap;
        private readonly Dictionary<string, string> _allObjectTypeToAliasMap;
        private readonly string _aliasNames;

        public ElasticObjectTypeToAliasMapService()
        {
            _textSearchObjectTypeToAliasMap = new Dictionary<string, string>
            {
                { GlobalSearchObjectsConst.Spell, "Spell"},
            };

            _aliasNames = string.Join(",", _textSearchObjectTypeToAliasMap.Values.Distinct());
        }

        public int TextSearchObjectTypeCount => _textSearchObjectTypeToAliasMap.Count;

        public void CheckTextSearchObjectTypeOrThrow(string[] objectType)
        {
            if (objectType.All(ot => _textSearchObjectTypeToAliasMap.ContainsKey(ot)))
                return;

            throw new ArgumentException(nameof(objectType));
        }

        public void CheckObjectTypeOrThrow(string[] objectType)
        {
            if (objectType.All(ot => _allObjectTypeToAliasMap.ContainsKey(ot)))
                return;

            throw new ArgumentException(nameof(objectType));
        }

        public string GetTextSearchIndexName()
        {
            return _aliasNames;
        }

        public bool AreAllSearchObjectTypes(string[] objectTypes)
        {
            if (objectTypes == null)
                return false;

            if (!objectTypes.Any())
                return false;

            if (objectTypes.Length != _textSearchObjectTypeToAliasMap.Count)
                return false;

            return _textSearchObjectTypeToAliasMap.Keys.All(objectTypes.Contains);
        }
    }
}
