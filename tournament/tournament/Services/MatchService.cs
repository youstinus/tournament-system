using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using tournament.Infrastructure.DataBase.Models;
using tournament.Infrastructure.Repositories;
using tournament.Infrastructure.Repositories.Interfaces;
using tournament.Models;
using tournament.Services.Interfaces;

namespace tournament.Services
{
    public class MatchService : IMatchService
    {
        private readonly MatchRepository _repository;
        private readonly IMatchTeamRepository _matchTeamRepository;
        private readonly IMapper _mapper;
        private readonly ITimeService _timeService;

        public MatchService(MatchRepository repository, IMatchTeamRepository matchTeamRepository,
            ITimeService timeService, IMapper mapper)
        {
            _repository = repository;
            _timeService = timeService;
            _mapper = mapper;
            _matchTeamRepository = matchTeamRepository;
        }

        public async Task<MatchDto> GetById(int id)
        {
            var match = await _repository.GetById(id);
            var matchDto = _mapper.Map<MatchDto>(match);
            return matchDto;
        }

        public async Task<ICollection<MatchDto>> GetAll()
        {
            var matches = await _repository.GetAll();
            var matchesDto = _mapper.Map<MatchDto[]>(matches).ToList();
            return matchesDto;
        }

        public async Task<MatchDto> Create(MatchDto newItem)
        {
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));

            var match = CreateMatchPoco(newItem);
            await _repository.Create(match);

            var matchDto = _mapper.Map<MatchDto>(match);
            return matchDto;
        }

        public async Task Update(int id, MatchDto updateData)
        {
            if (updateData == null) throw new ArgumentNullException(nameof(updateData));

            var itemToUpdate = await _repository.GetById(id);
            if (itemToUpdate == null)
            {
                throw new InvalidOperationException($"Match {id} was not found");
            }

            updateData.Id = id;
            await UpdateMatchTeam(updateData);
            var modificationDate = _timeService.GetCurrentTime();
            _mapper.Map(updateData, itemToUpdate);
            itemToUpdate.LastModified = modificationDate;
            itemToUpdate.Id = id;
            await _repository.Update(itemToUpdate);
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _repository.GetById(id);
            if (item == null)
            {
                return false;
            }

            var deleted = await _repository.Delete(item);
            return deleted;
        }

        public async Task<ICollection<MatchDto>> GetByTournamentId(int tournamentId)
        {
            var matches = await _repository.GetByTournamentId(tournamentId);
            var matchesDto = _mapper.Map<MatchDto[]>(matches);
            return matchesDto;
        }

        private Match CreateMatchPoco(MatchDto newItem)
        {
            var creationDate = _timeService.GetCurrentTime();
            var match = _mapper.Map<Match>(newItem);
            match.LastModified = creationDate;
            match.Created = creationDate;
            return match;
        }

        private async Task UpdateMatchTeam(MatchDto match)
        {
            foreach (var team in match.MatchTeams)
            {
                var matchTeam = await _matchTeamRepository.GetById(match.Id, team.Id);
                if (matchTeam == null)
                {
                    await _matchTeamRepository.Create(new MatchTeam()
                    {
                        MatchId = match.Id,
                        TeamId = team.Id
                    });
                }
            }
        }

    }
}
