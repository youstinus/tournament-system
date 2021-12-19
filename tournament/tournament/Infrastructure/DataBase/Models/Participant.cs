using System.Collections.Generic;

namespace tournament.Infrastructure.DataBase.Models
{
    public class Participant : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public ICollection<ParticipantTeam> ParticipantTeams { get; set; }
    }
}
