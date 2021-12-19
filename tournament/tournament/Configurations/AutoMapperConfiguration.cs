using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using tournament.Infrastructure.DataBase.Models;
using tournament.Models;

namespace tournament.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration() : this("tournament")
        {

        }

        protected AutoMapperConfiguration(string name) : base(name)
        {
            //Participants
            CreateMap<ParticipantDto, Participant>(MemberList.None);
            CreateMap<Participant, ParticipantDto>(MemberList.Destination);
            //Tournaments
            CreateMap<TournamentDto, Tournament>(MemberList.None);
            CreateMap<Tournament, TournamentDto>(MemberList.Destination);
            //Teams
            CreateMap<TeamDto, Team>(MemberList.None);
            CreateMap<Team, TeamDto>(MemberList.Destination);
            //Matches
            CreateMap<MatchDto, Match>(MemberList.None);
            CreateMap<Match, MatchDto>(MemberList.Destination)
                .ForMember(dest => dest.MatchTeams, opt => opt.MapFrom(src => src.Teams.Select(x => x.Team)));
        }
    }
}
