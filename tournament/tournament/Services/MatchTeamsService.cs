using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tournament.Infrastructure.DataBase.Models;
using tournament.Infrastructure.Repositories.Interfaces;
using tournament.Models;
using tournament.Services.Interfaces;

namespace tournament.Services
{
    public class MatchTeamsService : IMatchTeamsService
    {
        private readonly IMatchTeamRepository _repository;

        public MatchTeamsService(IMatchTeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<int>> GetTeamIds(int matchId)
        {
            var matchTeams = await _repository.GetByMatchId(matchId);
            var teamsIds = matchTeams
                .Select(x => x.TeamId)
                .ToArray();

            return teamsIds;
        }

        public async Task AddTeam(int matchId, NewMatchTeamDto newMatchTeam)
        {
            if (newMatchTeam == null) throw new ArgumentNullException(nameof(newMatchTeam));

            var matchTeam = new MatchTeam()
            {
                TeamId = newMatchTeam.TeamId,
                MatchId = matchId
            };
            await _repository.Create(matchTeam);
        }

        public async Task<bool> RemoveMatchTeam(int tagId, int productId)
        {
            var matchTeam = await _repository.GetById(tagId, productId);
            if (matchTeam == null)
            {
                return false;
            }

            var removed = await _repository.Delete(matchTeam);
            return removed;
        }
    }
}
