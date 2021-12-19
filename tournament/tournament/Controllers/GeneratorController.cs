using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tournament.Algorithm;
using tournament.Constants;
using tournament.Infrastructure.DataBase.Models;
using tournament.Infrastructure.Repositories;
using tournament.Infrastructure.Repositories.Interfaces;
using tournament.Models;
using tournament.Services.Interfaces;

namespace tournament.Controllers
{
    [Route("api/generator")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {
        private readonly IService<TournamentDto> _tournamentService;
        private readonly IService<MatchDto> _matchService;
        private readonly IService<TeamDto> _teamService;
        private readonly IMapper _mapper;
        private readonly MatchRepository _matchRepository;
        private readonly IMatchTeamRepository _matchTeamRepository;

        public GeneratorController(
            IService<TeamDto> teamService,
            IService<MatchDto> matchService,
            IMapper mapper,
            IService<TournamentDto> tournamentService,
            MatchRepository matchRepository,
            IMatchTeamRepository matchTeamRepository)
        {
            _tournamentService = tournamentService;
            _teamService = teamService;
            _matchService = matchService;
            _mapper = mapper;
            _matchRepository = matchRepository;
            _matchTeamRepository = matchTeamRepository;
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id, [FromBody] ICollection<TeamDto> teams)
        {
            Console.WriteLine("Postas is fronto " + id);
            var tournamentDto = await _tournamentService.GetById(id);
            var tournament = _mapper.Map<Tournament>(tournamentDto);
            var allTeams = _mapper.Map<Team[]>(teams);
            Bracket generator = new Bracket(tournament, allTeams);
            var brackets = generator.GetTour();
            foreach (var match in (brackets))
            {
                await _matchRepository.Create(match);
            }
            var matchesDirty = await _matchRepository.GetByTournamentId(id);
 
            await PutThemTeams(matchesDirty.ToArray(), teams.ToArray());
            var matchesClean = await _matchRepository.GetByTournamentId(id);
            var bracketsDto = _mapper.Map<MatchDto[]>(matchesClean);
            var bracketUri = CreateResourceUri(id);
            return Created(bracketUri, bracketsDto);
        }
 
        private async Task PutThemTeams(Match[] dirtyMatches, TeamDto[] teams)
        {
            for (int i = 0; i < teams.Length -teams.Length % 2; i+=2)
            {
                for(int k = 0; k < 2; k++)
                {
                    await _matchTeamRepository.Create(new MatchTeam()
                    {
                        TeamId = teams[i + k].Id,
                        MatchId = dirtyMatches[i / 2].Id // sequenceNr ?
                    });
                }
            }
            if(teams.Length % 2 != 0)
            {
                await _matchTeamRepository.Create(new MatchTeam() {
                    TeamId = teams[teams.Length - 1].Id,
                    MatchId = dirtyMatches[teams.Length / 2].Id
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTournamentId(int id)
        {
            var matches = await _matchRepository.GetByTournamentId(id);
            var matchesDto = _mapper.Map<MatchDto[]>(matches);
            return Ok(matchesDto);
        }
        private Uri CreateResourceUri(int id)
        {
            return new Uri(Url.Link(nameof(RoutingEnum.GetMatches), new { id = id }));
        }
    }
}