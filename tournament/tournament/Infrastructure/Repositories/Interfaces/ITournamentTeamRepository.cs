using System.Threading.Tasks;
using tournament.Infrastructure.DataBase.Models;

namespace tournament.Infrastructure.Repositories.Interfaces
{
    public interface ITournamentTeamRepository
    {
        Task<TournamentTeam> GetById(int teamId, int tournamentId);
        Task Create(TournamentTeam item);
        Task<bool> Delete(TournamentTeam item);
    }
}
