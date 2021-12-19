using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tournament.Infrastructure.DataBase;
using tournament.Infrastructure.DataBase.Models;
using tournament.Infrastructure.Repositories.Interfaces;

namespace tournament.Infrastructure.Repositories
{
    public class MatchTeamRepository : IMatchTeamRepository
    {
        private readonly TournamentDbContext _context;

        public MatchTeamRepository(TournamentDbContext context)
        {
            _context = context;
        }

        public async Task<MatchTeam> GetById(int matchId, int teamId)
        {
            var teamPerformance = await _context.MatchTeams
                .FirstOrDefaultAsync(x => x.MatchId == matchId && x.TeamId == teamId);
            return teamPerformance;
        }

        public async Task<ICollection<MatchTeam>> GetByMatchId(int matchId)
        {
            var teamPerformances = await _context.MatchTeams
                .Where(x => x.MatchId == matchId).ToArrayAsync();
            return teamPerformances;
        }

        public async Task<ICollection<MatchTeam>> GetByTeamId(int teamId)
        {
            var teamPerformances = await _context.MatchTeams
                .Where(x => x.TeamId == teamId).ToArrayAsync();
            return teamPerformances;
        }

        public async Task Create(MatchTeam item)
        {
            await _context.MatchTeams.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(MatchTeam matchTeam)
        {
            _context.MatchTeams.Remove(matchTeam);
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
