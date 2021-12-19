using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tournament.Models;

namespace tournament.Services.Interfaces
{
    public interface IMatchService : IService<MatchDto>
    {
        Task<ICollection<MatchDto>> GetByTournamentId(int tournamentId);
    }
}
