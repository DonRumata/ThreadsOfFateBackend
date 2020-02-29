using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThreadsOfFate.Domain.Dal.Dto.Spell;
using ThreadsOfFate.Domain.Dal.ResponseDto.Spell;
using ThreadsOfFate.Domain.Dal.Specifications;
using ThreadsOfFate.ReadDomain.Model;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Services.Abstractions;
using ThreadsOfFate.Requests.GlobalSearch;

namespace ThreadsOfFate.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/spell")]
    [Produces("application/json")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class SpellController : ControllerBase
    {
        private readonly ISpellService _spellService;

        public SpellController(ISpellService spellService)
        {
            _spellService = spellService;
        }

        [HttpGet("get")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<SpellExtDto>> GetSpell(Guid id)
        {
            var specification = new GetSpellSpecification(id);

            var result = await _spellService.GetSpell(specification);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("getbyname")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<SpellDto[]>> GetSpellByName(string name)
        {
            var result = await _spellService.GetSpellByName(name);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        //[HttpGet("get")]
        //[MapToApiVersion("1.0")]
        //public async Task<ActionResult<SpellDto>> GetSpell(SpellGlobalSearchQueryFilter spellData)
        //{
        //    var specification = new GetSpellSpecification(spellData);
        //}
    }
}
