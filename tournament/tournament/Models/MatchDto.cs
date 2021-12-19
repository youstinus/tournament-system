using System.Collections.Generic;

namespace tournament.Models
{
    public class MatchDto : BaseDto
    {
        public int TournamentId { get; set; }
        public int SequenceNr { get; set; }
        public int WinnerId { get; set; }
        public int LoserId { get; set; }
        public int? WinnerGoesToId { get; set; }
        public int? LoserGoesToId { get; set; }
        public int ScoreTeam1 { get; set; }
        public int ScoreTeam2 { get; set; }
        public ICollection<TeamDto> MatchTeams { get; set; }
    }
}
