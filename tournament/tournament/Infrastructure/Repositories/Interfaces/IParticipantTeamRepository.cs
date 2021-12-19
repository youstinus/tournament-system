using System.Collections.Generic;
using System.Threading.Tasks;
using tournament.Infrastructure.DataBase.Models;

namespace tournament.Infrastructure.Repositories.Interfaces
{
    public interface IParticipantTeamRepository
    {
        Task<ParticipantTeam> GetById(int teamId, int participantId);
        Task<ICollection<ParticipantTeam>> GetByTeamId(int teamId);
        Task Create(ParticipantTeam item);
        Task<bool> Delete(ParticipantTeam item);
        //Task<ParticipantTeam[]> GetParticipantIdsByTeamId(int teamId);
    }
}
