using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using tournament.Infrastructure.DataBase;
using tournament.Infrastructure.DataBase.Models;
using tournament.Infrastructure.Repositories.Interfaces;

namespace tournament.Infrastructure.Repositories
{
    public class TournamentTeamRepository : ITournamentTeamRepository
    {
        private readonly TournamentDbContext _context;

        public TournamentTeamRepository(TournamentDbContext context)
        {
            _context = context;
        }

        public async Task<TournamentTeam> GetById(int teamId, int tournamentId)
        {
            var tournamentTeam = await _context.TournamentTeams
                .FirstOrDefaultAsync(x => x.TeamId == teamId && x.TournamentId == tournamentId);

            return tournamentTeam;
        }

        public async Task Create(TournamentTeam item)
        {
            _context.TournamentTeams.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(TournamentTeam item)
        {
            _context.TournamentTeams.Remove(item);
            var changed = await _context.SaveChangesAsync();

            return changed > 0;
        }     
    }
}
