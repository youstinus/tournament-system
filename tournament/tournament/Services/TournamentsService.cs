using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using tournament.Infrastructure.DataBase.Models;
using tournament.Infrastructure.Repositories.Interfaces;
using tournament.Models;
using tournament.Services.Interfaces;

namespace tournament.Services
{
    public class TournamentsService : IService<TournamentDto>
    {
        private readonly IRepository<Tournament> _tournamnetRepository;
        private readonly IMapper _mapper;
        private readonly ITimeService _timeService;

        public TournamentsService(IRepository<Tournament> repository,
            IMapper mapper, ITimeService timeService)
        {
            _tournamnetRepository = repository;
            _mapper = mapper;
            _timeService = timeService;
        }

        public async Task<ICollection<TournamentDto>> GetAll()
        {
            var tournaments = await _tournamnetRepository.GetAll();
            var tournamentsDto = _mapper.Map<TournamentDto[]>(tournaments);
            return tournamentsDto;
        }

        public async Task<TournamentDto> GetById(int id)
        {
            var tournament = await _tournamnetRepository.GetById(id);
            var tournamentDto = _mapper.Map<TournamentDto>(tournament);
            return tournamentDto;
        }

        public async Task<TournamentDto> Create(TournamentDto newItem)
        {
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));

            var tournament = CreateProductPoco(newItem);
            await _tournamnetRepository.Create(tournament);

            var tournamentDto = _mapper.Map<TournamentDto>(tournament);
            return tournamentDto;
        }

        public async Task Update(int id, TournamentDto updateData)
        {
            if (updateData == null) throw new ArgumentNullException(nameof(updateData));

            var itemToUpdate = await _tournamnetRepository.GetById(id);//.Result;
            if (itemToUpdate == null)
            {
                throw new InvalidOperationException($"Tournament {id} was not found");
            }

            var modificationDate = _timeService.GetCurrentTime();
            _mapper.Map(updateData, itemToUpdate);
            itemToUpdate.Id = id;
            itemToUpdate.LastModified = modificationDate;
            await _tournamnetRepository.Update(itemToUpdate);
        }

        public async Task<bool> Delete(int id)
        {
            var tournament = await _tournamnetRepository.GetById(id);
            if (tournament == null)
                return false;

            return await _tournamnetRepository.Delete(tournament);  
        }

        private Tournament CreateProductPoco(TournamentDto newItem)
        {
            var creationDate = _timeService.GetCurrentTime();
            var tournament = _mapper.Map<Tournament>(newItem);
            tournament.LastModified = creationDate;
            tournament.Created = creationDate;
            return tournament;
        }


    }
}
