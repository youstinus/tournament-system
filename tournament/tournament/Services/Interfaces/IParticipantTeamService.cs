using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tournament.Models;

namespace tournament.Services.Interfaces
{
    public interface IParticipantTeamService
    {
        Task<ICollection<int>> GetParticipantIds(int paricipantId);
        Task AddParticipant(int teamId, NewParticipantTeamDto newParticipantTeam);
        Task<bool> RemoveParticipantTeam(int tagId, int productId);
    }
}
