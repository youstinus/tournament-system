using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tournament.Constants;
using tournament.Models;
using tournament.Services.Interfaces;

namespace tournament.Controllers
{
    [Route("api/matches")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchesController(IMatchService service)
        {
            _matchService = service;
        }

        [HttpGet]
        [Produces(typeof(MatchDto[]))]
        public async Task<IActionResult> Get()
        {
            var matches = await _matchService.GetAll();

            return Ok(matches);
        }

        [HttpGet("{id}", Name = nameof(RoutingEnum.GetMatches))]
        [Produces(typeof(MatchDto))]
        public async Task<IActionResult> Get(int id)
        {
            var match = await _matchService.GetById(id);

            if (match == null)
            {
                return NotFound();
            }
            return Ok(match);
        }

        [HttpGet("tournament/{id}")]
        [Produces(typeof(MatchDto))]
        public async Task<IActionResult> GetByTournamentId(int id)
        {
            var matches = await _matchService.GetByTournamentId(id);

            if (matches == null)
            {
                return NotFound();
            }
            return Ok(matches);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MatchDto newMatch)
        {
            var createdMatch = await _matchService.Create(newMatch);
            var matchUri = CreateResourceUri(createdMatch.Id);

            return Created(matchUri, createdMatch);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MatchDto newMatch)
        {
            await _matchService.Update(id, newMatch);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _matchService.Delete(id);

            return NoContent();
        }

        private Uri CreateResourceUri(int id)
        {
            // ReSharper disable once RedundantAnonymousTypePropertyName
            return new Uri(Url.Link(nameof(RoutingEnum.GetMatches), new { id = id }));
        }
    }
}
