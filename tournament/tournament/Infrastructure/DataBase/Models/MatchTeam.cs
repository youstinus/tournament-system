namespace tournament.Infrastructure.DataBase.Models
{
    public class MatchTeam : BaseEntity
    {
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
