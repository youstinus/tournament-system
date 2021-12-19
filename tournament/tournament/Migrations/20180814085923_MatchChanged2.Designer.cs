﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tournament.Infrastructure.DataBase;

namespace tournament.Migrations
{
    [DbContext(typeof(TournamentDbContext))]
    [Migration("20180814085923_MatchChanged2")]
    partial class MatchChanged2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("tournament.Infrastructure.DataBase.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("LastModified");

                    b.Property<int?>("LoserGoesToId");

                    b.Property<int>("ScoreTeam1");

                    b.Property<int>("ScoreTeam2");

                    b.Property<int>("SequenceNr");

                    b.Property<int>("TournamentId");

                    b.Property<int?>("WinnerGoesToId");

                    b.Property<int>("WinnerId");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("tournament.Infrastructure.DataBase.Models.MatchTeam", b =>
                {
                    b.Property<int>("MatchId");

                    b.Property<int>("TeamId");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Id");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("MatchId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("MatchTeams");
                });

            modelBuilder.Entity("tournament.Infrastructure.DataBase.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<DateTime>("Created");

                    b.Property<string>("FirstName");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("tournament.Infrastructure.DataBase.Models.ParticipantTeam", b =>
                {
                    b.Property<int>("ParticipantId");

                    b.Property<int>("TeamId");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Id");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("ParticipantId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("ParticipantTeams");
                });

            modelBuilder.Entity("tournament.Infrastructure.DataBase.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("tournament.Infrastructure.DataBase.Models.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Game");

                    b.Property<DateTime>("LastModified");

                    b.Property<int>("MaxParticipantPerTeam");

                    b.Property<int>("NumberOfTeams");

                    b.Property<string>("Title");

                    b.Property<int?>("WinnerTeamId");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("tournament.Infrastructure.DataBase.Models.TournamentTeam", b =>
                {
                    b.Property<int>("TournamentId");

                    b.Property<int>("TeamId");

                    b.HasKey("TournamentId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("TournamentTeams");
                });

            modelBuilder.Entity("tournament.Infrastructure.DataBase.Models.Match", b =>
                {
                    b.HasOne("tournament.Infrastructure.DataBase.Models.Tournament", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tournament.Infrastructure.DataBase.Models.MatchTeam", b =>
                {
                    b.HasOne("tournament.Infrastructure.DataBase.Models.Match", "Match")
                        .WithMany("Teams")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tournament.Infrastructure.DataBase.Models.Team", "Team")
                        .WithMany("Matches")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tournament.Infrastructure.DataBase.Models.ParticipantTeam", b =>
                {
                    b.HasOne("tournament.Infrastructure.DataBase.Models.Participant", "Participant")
                        .WithMany("ParticipantTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tournament.Infrastructure.DataBase.Models.Team", "Team")
                        .WithMany("ParticipantTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("tournament.Infrastructure.DataBase.Models.TournamentTeam", b =>
                {
                    b.HasOne("tournament.Infrastructure.DataBase.Models.Team", "Team")
                        .WithMany("TournamentTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("tournament.Infrastructure.DataBase.Models.Tournament", "Tournament")
                        .WithMany("TournamentTeams")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}