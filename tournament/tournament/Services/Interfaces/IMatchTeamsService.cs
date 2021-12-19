using System.Collections.Generic;
using System.Threading.Tasks;
using tournament.Models;

namespace tournament.Services.Interfaces
{
    public interface IMatchTeamsService
    {
        Task<ICollection<int>> GetTeamIds(int matchId);
        Task AddTeam(int matchId, NewMatchTeamDto newMatchTeam);
        Task<bool> RemoveMatchTeam(int tagId, int productId);
    }
}