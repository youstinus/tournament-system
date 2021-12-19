using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tournament.Models;
using tournament.Services.Interfaces;

namespace tournament.Controllers
{
    [Route("api/participantTeams")]
    [ApiController]
    public class ParticipantTeamController : ControllerBase
    {
        private readonly IParticipantTeamService _participantTeamsService;

        public ParticipantTeamController(IParticipantTeamService participantTeamService)
        {
            _participantTeamsService = participantTeamService;
        }

        [HttpGet]
        [Produces(typeof(int[]))]
        public async Task<IActionResult> GetTeams(int teamId)
        {
            var teams = await _participantTeamsService.GetParticipantIds(teamId);

            return Ok(teams);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int teamId, [FromBody] NewParticipantTeamDto newParticipantTeam)
        {
            await _participantTeamsService.AddParticipant(teamId, newParticipantTeam);

            return NoContent();
        }

        [HttpDelete("{teamId}")]
        public async Task<IActionResult> Delete(int teamId, int participantId)
        {
            await _participantTeamsService.RemoveParticipantTeam(teamId, participantId);

            return NoContent();
        }
    }
}
