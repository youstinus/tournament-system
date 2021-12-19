using System;
using System.Collections.Generic;
using tournament.Constants;

namespace tournament.Infrastructure.DataBase.Models
{
    public class Tournament : BaseEntity
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int MaxParticipantPerTeam { get; set; }
        public GameType Game { get; set; }
        public int NumberOfTeams { get; set; }
        public int? WinnerTeamId { get; set; }

        public ICollection<TournamentTeam> TournamentTeams { get; set; }
        public ICollection<Match> Matches { get; set; }
    }
}
