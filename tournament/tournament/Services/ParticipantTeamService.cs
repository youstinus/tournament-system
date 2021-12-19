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
    public class ParticipantTeamService : IParticipantTeamService
    {
        private readonly IParticipantTeamRepository _repository;

        public ParticipantTeamService(IParticipantTeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<int>> GetParticipantIds(int teamId)
        {
            var participantTeams = await _repository.GetByTeamId(teamId);
            var participantIds = participantTeams
                .Select(x => x.ParticipantId)
                .ToArray();

            return participantIds;
        }

        public async Task AddParticipant(int teamId, NewParticipantTeamDto newParticipantTeam)
        {
            if (newParticipantTeam == null) throw new ArgumentNullException(nameof(newParticipantTeam));

            var participantTeam = new ParticipantTeam()
            {
                ParticipantId = newParticipantTeam.TeamId,
                TeamId = teamId
            };
            await _repository.Create(participantTeam);
        }

        public async Task<bool> RemoveParticipantTeam(int teamId, int participantId)
        {
            var participantTeam = await _repository.GetById(teamId, participantId);
            if (participantTeam == null)
            {
                return false;
            }

            var removed = await _repository.Delete(participantTeam);
            return removed;
        }
    }
}
