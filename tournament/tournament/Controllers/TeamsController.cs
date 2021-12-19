using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using tournament.Constants;
using tournament.Models;
using tournament.Services.Interfaces;

namespace tournament.Controllers
{
    [Route("api/Teams")]
    public class TeamsController : ControllerBase
    {
        private readonly IService<TeamDto> _teamsService;

        public TeamsController(IService<TeamDto> service)
        {
            _teamsService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teams = await _teamsService.GetAll();

            return Ok(teams);
        }

        [HttpGet("{id}", Name = nameof(RoutingEnum.GetTeam))]
        public async Task<IActionResult> Get(int id)
        {
            var team = await _teamsService.GetById(id);

            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TeamDto newTeam)
        {
            var createdTeam = await _teamsService.Create(newTeam);
            var teamUri = CreateResourceUri(createdTeam.Id);

            return Created(teamUri, createdTeam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TeamDto newTeam)
        {
            await _teamsService.Update(id, newTeam);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamsService.Delete(id);

            return NoContent();
        }
        private Uri CreateResourceUri(int id)
        {
            // ReSharper disable once RedundantAnonymousTypePropertyName
            return new Uri(Url.Link(nameof(RoutingEnum.GetTeam), new { id = id }));
        }
    }
}
