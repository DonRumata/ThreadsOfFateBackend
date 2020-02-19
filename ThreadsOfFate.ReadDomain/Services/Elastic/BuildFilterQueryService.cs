using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using ThreadsofFate.Common.Const;
using ThreadsOfFate.ReadDomain.Extensions.Elastic;
using ThreadsOfFate.ReadDomain.Services.Elastic.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Services.Elastic
{
    class BuildFilterQueryService : IBuildFilterQueryService
    {
        public QueryContainer BuildFilterQuery(GlobalSearchCollectionSpecification specification)
        {
            var filter = specification.Filter;

            if (filter == null)
                return null;

            var mustQueries = new List<QueryContainer>();

            // 1. Фильтр документов по их типам (filter.ObjectType) применяется в PostFilter.
            // 2. Фильтрация по свойствам для каждого типа документов отдельно.
            // Для каждого типа документов применяются свои условия фильтрации.

            // Кратко поясняю главную причину создания __как бы избыточных__ условий включающих все типы объектов индекса.
            // В elastic отсутствет условная фильтрация, т.е. указанные в фильтре правила применяются ко всем документам
            // индекса без исключения.
            // Свойство Filter работает как "вернуть всё, что указано и соответствует условиям",
            // поэтому, всё что не соответствует условиям фильтра или не указано в фильтре, не будет возвращено.

            // Единственный способ реализовать условную фильтрацию, т.е. привязать условие к конкретному типу документа (objectType),
            // это явно прописывать в фильтре все типы документов, с условием либо фильтрующим этот тип документа,
            // либо возвращающим все экземпляры документа этого типа.
            var shouldQueries = new List<QueryContainer>();

            AddSpellFilterQuery(filter, shouldQueries);

            return new BoolQuery
            {
                Must = mustQueries,
                Should = shouldQueries,
                MinimumShouldMatch = 1
            };
        }

        private void AddSpellFilterQuery(GlobalSearchFilterSpecification filter, ICollection<QueryContainer> queries)
        {
            var mustQueries = new List<QueryContainer>();
            mustQueries.AddTermsQuery("ObjectType", new[] {GlobalSearchObjectsConst.Spell});
            mustQueries.AddTermsQuery("SpellName", filter?.Spell?.DescriptionContains.ToArray());

            queries.Add(new BoolQuery
            {
                Must = mustQueries
            });
        }
    }
}
