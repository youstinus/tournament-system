using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using tournament.Infrastructure.DataBase.Models;
using tournament.Infrastructure.Repositories.Interfaces;
using tournament.Models;
using tournament.Services.Interfaces;

namespace tournament.Services
{
    public class TeamService : IService<TeamDto>
    {
       
        private readonly IRepository<Team> _repository;
        private readonly IMapper _mapper;
        private readonly ITimeService _timeService;

        public TeamService(
            IRepository<Team> repository,
            IMapper mapper, ITimeService timeService)
        {
            _repository = repository;
            _mapper = mapper;
            _timeService = timeService;
        }

        public async Task<TeamDto> GetById(int id)
        {
            var team = await _repository.GetById(id);
            var teamDto = _mapper.Map<TeamDto>(team);
            return teamDto;
        }

        public async Task<ICollection<TeamDto>> GetAll()
        {
            var teams = await _repository.GetAll();
            var teamsDto = _mapper.Map<TeamDto[]>(teams);
            return teamsDto;
        }

        public async Task<TeamDto> Create(TeamDto newItem)
        {
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));

            var team = CreateTeamPoco(newItem);
            await _repository.Create(team);

            var teamDto = _mapper.Map<TeamDto>(team);
            return teamDto;
        }

        public async Task Update(int id, TeamDto updateData)
        {
            if (updateData == null) throw new ArgumentNullException(nameof(updateData));

            var itemToUpdate = await _repository.GetById(id);
            if (itemToUpdate == null)
            {
                throw new InvalidOperationException($"Team {id} was not found");
            }

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

        private Team CreateTeamPoco(TeamDto newItem)
        {
            var creationDate = _timeService.GetCurrentTime();
            var participant = _mapper.Map<Team>(newItem);
            participant.LastModified = creationDate;
            participant.Created = creationDate;
            return participant;
        }
    
    }
}
