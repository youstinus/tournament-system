namespace tournament.Infrastructure.DataBase.Models
{
    public class ParticipantTeam : BaseEntity
    {
        public int TeamId { get; set; }
        public int ParticipantId { get; set; }

        public Team Team { get; set; }
        public Participant Participant { get; set; }
    }
}
