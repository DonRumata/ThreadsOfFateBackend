using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThreadsOfFate.Domain.Dal.Model.Search;
using ThreadsOfFate.Domain.Dal.Model.Search.Filters;
using ThreadsOfFate.Mapping;
using ThreadsOfFate.ReadDomain.Services.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.Filters;
using ThreadsOfFate.Requests.SearchFilterOptions;

namespace ThreadsOfFate.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/searchfilteroptions")]
    [Produces("application/json")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class SearchFilterOptionsController : ControllerBase
    {
        private readonly ISearchFilterOptionsService _searchFilterOptionsService;

        public SearchFilterOptionsController(ISearchFilterOptionsService searchFilterOptionsService)
        {
            _searchFilterOptionsService = searchFilterOptionsService;
        }

        [HttpGet("spell/elements")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CollectionDto<FilterOptionDto>>> GetSpellElements([FromQuery]SearchFilterOptionsQuery query)
        {
            var specification = Mapper.Map<SearchFilterOptionsQuery, SearchFilterOptionsSpecification>(query);

            var result = await _searchFilterOptionsService.GetSpellElements(specification);

            return Ok(result);
        }
    }
}
