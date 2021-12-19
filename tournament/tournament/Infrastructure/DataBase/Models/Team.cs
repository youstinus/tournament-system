using System.Collections.Generic;

namespace tournament.Infrastructure.DataBase.Models
{
    public class Team : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<ParticipantTeam> ParticipantTeams { get; set; }
        public ICollection<TournamentTeam> TournamentTeams { get; set; }
        public ICollection<MatchTeam> Matches { get; set; }
    }
}
