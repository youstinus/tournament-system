using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using tournament.Constants;
using tournament.Models;
using tournament.Services.Interfaces;

namespace tournament.Controllers
{
    [Route("api/Participants")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly IService<ParticipantDto> _participantsService;

        public ParticipantsController(IService<ParticipantDto> service)
        {
            _participantsService = service;
        }

        [HttpGet]
        [Produces(typeof(ParticipantDto))]
        public async Task<IActionResult> Get()
        {
            var participants = await _participantsService.GetAll();

            return Ok(participants);
        }

        [HttpGet("{id}", Name = nameof(RoutingEnum.GetParticipant123))]
        [Produces(typeof(ParticipantDto))]
        public async Task<IActionResult> Get(int id)
        {
            var participant = await _participantsService.GetById(id);

            if (participant == null)
            {
                return NotFound();
            }
            return Ok(participant);
        }

        [HttpPost]
        [Produces(typeof(ParticipantDto))]
        public async Task<IActionResult> Post([FromBody] ParticipantDto newParticipant)
        {
            var createdParticipant = await _participantsService.Create(newParticipant);
            var participantUri = CreateResourceUri(createdParticipant.Id);

            return Created(participantUri, createdParticipant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ParticipantDto newParticipant)
        {
            await _participantsService.Update(id, newParticipant);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _participantsService.Delete(id);

            return NoContent();
        }
        private Uri CreateResourceUri(int id)
        {
            // ReSharper disable once RedundantAnonymousTypePropertyName
            const string name = nameof(RoutingEnum.GetParticipant123);
            var asd = Url.Link(name, new {id = id});
            var naujas = new Uri(asd);
            return naujas;
        }
    }
}
