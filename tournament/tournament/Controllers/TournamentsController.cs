using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tournament.Constants;
using tournament.Models;
using tournament.Services.Interfaces;

namespace tournament.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IService<TournamentDto> _tournamentService;

        public TournamentsController(IService<TournamentDto> tournamentsService)
        {
            _tournamentService = tournamentsService;
        }

        [HttpGet]
        [Produces(typeof(TournamentDto[]))]
        public async Task<IActionResult> Get()
        {
            var tournaments = await _tournamentService.GetAll();

            return Ok(tournaments);
        }

        [HttpGet("{id}", Name = nameof(RoutingEnum.GetTournament))]
        [Produces(typeof(TournamentDto))]
        public async Task<IActionResult> Get(int id)
        {
            var tournament = await _tournamentService.GetById(id);

            if (tournament == null)
            {
                return NotFound();
            }
            return Ok(tournament);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TournamentDto newTournament)
        {
            var createdTournament = await _tournamentService.Create(newTournament);
            var tournamentUri = CreateResourceUri(createdTournament.Id);

            return Created(tournamentUri, createdTournament);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TournamentDto updatedTournament)
        {
            await _tournamentService.Update(id, updatedTournament);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tournamentService.Delete(id);
            return NoContent();
        }

        private Uri CreateResourceUri(int id)
        {
            // ReSharper disable once RedundantAnonymousTypePropertyName
            return new Uri(Url.Link(nameof(RoutingEnum.GetTournament), new { id = id }));
        }
    }
}