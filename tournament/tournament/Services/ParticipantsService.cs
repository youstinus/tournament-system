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
    public class ParticipantsService : IService<ParticipantDto>
    {
        private readonly IRepository<Participant> _repository;
        private readonly IMapper _mapper;
        private readonly ITimeService _timeService;

        public ParticipantsService(IRepository<Participant> repository, 
            IMapper mapper, ITimeService timeService)
        {
            _repository = repository;
            _mapper = mapper;
            _timeService = timeService;
        }

        public async Task<ParticipantDto> GetById(int id)
        {
            var participant = await _repository.GetById(id);
            var participantDto = _mapper.Map<ParticipantDto>(participant);
            return participantDto;
        }

        public async Task<ICollection<ParticipantDto>> GetAll()
        {
            var participants = await _repository.GetAll();
            var participantsDto = _mapper.Map<ParticipantDto[]>(participants);
            return participantsDto;
        }

        public async Task<ParticipantDto> Create(ParticipantDto newItem)
        {
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));

            var participant = CreateParticipantPoco(newItem);
            await _repository.Create(participant);

            var participantDto = _mapper.Map<ParticipantDto>(participant);
            return participantDto;
        }

        public async Task Update(int id, ParticipantDto updateData)
        {
            if (updateData == null) throw new ArgumentNullException(nameof(updateData));

            var itemToUpdate = await _repository.GetById(id);
            if(itemToUpdate == null)
            {
                throw new InvalidOperationException($"Participant {id} was not found");
            }

            var modificationDate = _timeService.GetCurrentTime();
            _mapper.Map(updateData, itemToUpdate);
            itemToUpdate.Id = id;
            itemToUpdate.LastModified = modificationDate;
            await _repository.Update(itemToUpdate);
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _repository.GetById(id);
            if(item == null)
            {
                return false;
            }

            return await _repository.Delete(item);
        }

        private Participant CreateParticipantPoco(ParticipantDto newItem)
        {
            var creationDate = _timeService.GetCurrentTime();
            var participant = _mapper.Map<Participant>(newItem);
            participant.LastModified = creationDate;
            participant.Created = creationDate;
            return participant;
        }
    }
}
