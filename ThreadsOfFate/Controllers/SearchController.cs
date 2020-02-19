using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThreadsOfFate.Domain.Dal.Dto.SearchStructure;
using ThreadsOfFate.Mapping;
using ThreadsOfFate.ReadDomain.Model;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Queries.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch.Search;
using ThreadsOfFate.Requests.GlobalSearch;
using ThreadsOfFate.Requests.UniversalSearch;
using ThreadsOfFate.ReadDomain.Services.Abstractions;

namespace ThreadsOfFate.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/search")]
    [Produces("application/json")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("collection")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CollectionWithCountersDto<SearchItemDto>>> GetSearchResultCollection([FromBody] SearchQuery query)
        {
            var specification = Mapper.Map<SearchQuery, SearchCollectionSpecification>(query);

            var result = await _searchService.SearchAll(specification);

            return Ok(result);
        }
    }
}
