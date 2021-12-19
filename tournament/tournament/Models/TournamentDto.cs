using System;
using tournament.Constants;

namespace tournament.Models
{
    public class TournamentDto : BaseDto
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int MaxParticipantPerTeam { get; set; }
        public GameType Game { get; set; }
        public int NumberOfTeams { get; set; }
        public int? WinnerTeamId { get; set; }
    }
}
