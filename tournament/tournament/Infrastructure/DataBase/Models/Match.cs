using System;
using System.Collections.Generic;

namespace tournament.Infrastructure.DataBase.Models
{
    public class Match : BaseEntity
    {
        public int SequenceNr { get; set; }
        public int WinnerId { get; set; }
        public DateTime Date { get; set; }
        public int ScoreTeam1 { get; set; }
        public int ScoreTeam2 { get; set; }
        public ICollection<MatchTeam> Teams { get; set; }
        public Tournament Tournament { get; set; }
        // Foreign Keys
        public int TournamentId { get; set; }
        public int? WinnerGoesToId { get; set; }
        public int? LoserGoesToId { get; set; }
    }

}
