using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tournament.Infrastructure.DataBase.Models;

namespace tournament.Infrastructure.Repositories.Interfaces
{
    public interface IMatchTeamRepository
    {
        Task<MatchTeam> GetById(int matchId, int teamId);
        Task<ICollection<MatchTeam>> GetByMatchId(int matchId);
        Task<ICollection<MatchTeam>> GetByTeamId(int teamId);
        Task Create(MatchTeam item);
        Task<bool> Delete(MatchTeam matchTeam);
    }
}
