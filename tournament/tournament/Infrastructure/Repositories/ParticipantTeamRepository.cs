using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tournament.Infrastructure.DataBase;
using tournament.Infrastructure.DataBase.Models;
using tournament.Infrastructure.Repositories.Interfaces;

namespace tournament.Infrastructure.Repositories
{
    public class ParticipantTeamRepository : IParticipantTeamRepository
    {
        private readonly TournamentDbContext _context;

        public ParticipantTeamRepository(TournamentDbContext context)
        {
            _context = context;
        }

        public async Task<ParticipantTeam> GetById(int teamId, int participantId)
        {
            var participantTeam = await _context.ParticipantTeams
                .FirstOrDefaultAsync(x => x.TeamId == teamId && x.ParticipantId == participantId);

            return participantTeam;
        }

        public async Task<ICollection<ParticipantTeam>> GetByTeamId(int teamId)
        {
            var participants = await _context.ParticipantTeams
                .Where(x => x.TeamId == teamId).ToArrayAsync();
            return participants;
        }

        public async Task Create(ParticipantTeam item)
        {
            _context.ParticipantTeams.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(ParticipantTeam item)
        {
            _context.ParticipantTeams.Remove(item);
            var changed = await _context.SaveChangesAsync();

            return changed > 0;
        }
    }
}
