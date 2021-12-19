using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tournament.Models;
using tournament.Services.Interfaces;

namespace tournament.Controllers
{
    [Route("api/matchTeams/{matchId}")]
    [ApiController]
    public class MatchTeamsController : ControllerBase
    {
        private readonly IMatchTeamsService _matchTeamsService;

        public MatchTeamsController(IMatchTeamsService matchTeamsService)
        {
            _matchTeamsService = matchTeamsService;
        }

        [HttpGet]
        [Produces(typeof(int[]))]
        public async Task<IActionResult> GetTeams(int matchId)
        {
            var teams = await _matchTeamsService.GetTeamIds(matchId);

            return Ok(teams);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int matchId, [FromBody] NewMatchTeamDto newMatchTeam)
        {
            await _matchTeamsService.AddTeam(matchId, newMatchTeam);

            return NoContent();
        }

        [HttpDelete("{teamId}")]
        public async Task<IActionResult> Delete(int matchId, int teamId)
        {
            await _matchTeamsService.RemoveMatchTeam(matchId, teamId);

            return NoContent();
        }
    }
}