using Microsoft.EntityFrameworkCore;
using tournament.Infrastructure.DataBase.Models;

namespace tournament.Infrastructure.DataBase
{
    public class TournamentDbContext : DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ParticipantTeam> ParticipantTeams { get; set; }
        public DbSet<TournamentTeam> TournamentTeams { get; set; }
        public DbSet<MatchTeam> MatchTeams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }

        public TournamentDbContext(DbContextOptions<TournamentDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUpTournament(modelBuilder);
            SetUpParticipantTeam(modelBuilder);
            SetUpTeam(modelBuilder);
            SetUpTournamentTeam(modelBuilder);
            SetUpParticipant(modelBuilder);
            SetUpMatch(modelBuilder);
            SetUpMatchTeams(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SetUpTournament(ModelBuilder modelBuilder)
        {
            var tournamnetEntity = modelBuilder.Entity<Tournament>();
            tournamnetEntity.HasKey(x => x.Id);
            tournamnetEntity.HasMany(x => x.TournamentTeams)
                .WithOne(pt => pt.Tournament)
                .HasForeignKey(pt => pt.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public void SetUpParticipant(ModelBuilder modelBuilder)
        {
            var participantEntity = modelBuilder.Entity<Participant>();
            participantEntity.HasKey(x => x.Id);
            participantEntity.HasMany(x => x.ParticipantTeams)
                .WithOne(pt => pt.Participant)
                .HasForeignKey(pt => pt.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public void SetUpParticipantTeam(ModelBuilder modelBuilder)
        {
            var participantTeamEntity = modelBuilder.Entity<ParticipantTeam>();
            participantTeamEntity
                .HasKey(x => new { x.ParticipantId, x.TeamId});
        }

        public void SetUpTeam(ModelBuilder modelBuilder)
        {
            var teamEntity = modelBuilder.Entity<Team>();
            teamEntity.HasKey(x => x.Id);
            teamEntity.HasMany(x => x.TournamentTeams)
                .WithOne(pt => pt.Team)
                .HasForeignKey(pt => pt.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
            teamEntity.HasMany(x => x.ParticipantTeams)
                .WithOne(pt => pt.Team)
                .HasForeignKey(pt => pt.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
            teamEntity.HasMany(t => t.Matches)
                .WithOne(t => t.Team)
                .HasForeignKey(t => t.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public void SetUpTournamentTeam(ModelBuilder modelBuilder)
        {
            var tournamentTeamEntity = modelBuilder.Entity<TournamentTeam>();
            tournamentTeamEntity
                .HasKey(x => new { x.TournamentId, x.TeamId });
        }

        public void SetUpMatch(ModelBuilder modelBuilder)
        {
            var matchEntity = modelBuilder.Entity<Match>();
            matchEntity.HasKey(m => m.Id);
            matchEntity.HasMany(m => m.Teams)
                .WithOne(m => m.Match)
                .HasForeignKey(m => m.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
            matchEntity.HasOne(m => m.Tournament)
                .WithMany(t => t.Matches)
                .HasForeignKey(m => m.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public void SetUpMatchTeams(ModelBuilder modelBuilder)
        {
            var matchTeamsEntity = modelBuilder.Entity<MatchTeam>()
                .HasKey(x => new {x.MatchId, x.TeamId});
        }

    }
}
