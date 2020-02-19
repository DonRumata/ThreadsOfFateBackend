using System.Threading.Tasks;
using ThreadsOfFate.Mapping;
using Microsoft.AspNetCore.Mvc;
using ThreadsOfFate.ReadDomain.Model;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Queries.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;
using ThreadsOfFate.Requests.GlobalSearch;

namespace ThreadsOfFate.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер для операций с поиском
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/GlobalSearch")]
    [Produces("application/json")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class GlobalSearchController : ControllerBase
    {
        private readonly IGlobalSearchService _globalSearchService;

        public GlobalSearchController(IGlobalSearchService globalSearchService)
        {
            _globalSearchService = globalSearchService;
        }

        [HttpPost("collection")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CollectionWithCountersDto<GlobalSearchItemDto>>> GetGlobalSearchResultCollection([FromBody] GlobalSearchQuery query)
        {
            var specification = Mapper.Map<GlobalSearchQuery, GlobalSearchCollectionSpecification>(query);

            var result = await _globalSearchService.GlobalSearchFind(specification);

            return Ok(result);
        }
    }
}
