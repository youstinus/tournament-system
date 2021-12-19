using Microsoft.Extensions.DependencyInjection;
using tournament.Infrastructure;
using tournament.Infrastructure.DataBase.Models;
using tournament.Infrastructure.Repositories;
using tournament.Infrastructure.Repositories.Interfaces;
using tournament.Models;
using tournament.Services;
using tournament.Services.Interfaces;

namespace tournament.Configurations
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAllDependencies(this IServiceCollection service)
        {
            return service
                .AddInfrastructureDependencies()
                .AddApplicationDependencies();
        }

        /*
         * Method to add DI for Infrastructure folder
         */
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection service)
        {
            return service
                .AddScoped<IRepository<Tournament>, TournamentRepository>()
                .AddScoped<IRepository<Participant>, ParticipantRepository>()
                .AddScoped<MatchRepository>()
                .AddScoped<IRepository<Team>, TeamRepository>()
                .AddScoped<IMatchTeamRepository, MatchTeamRepository>()
                .AddScoped<IParticipantTeamRepository, ParticipantTeamRepository>();
        }

        /*
         * Method to add all other DI for the application (e.g. services)
         */
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection service)
        {
            return service
                .AddScoped<IService<TournamentDto>, TournamentsService>()
                .AddScoped<IService<ParticipantDto>, ParticipantsService>()
                .AddScoped<IMatchService, MatchService>()
                .AddScoped<IService<TeamDto>, TeamService>()
                .AddScoped<IService<MatchDto>, MatchService>()
                .AddScoped<IMatchTeamsService, MatchTeamsService>()
                .AddScoped<IParticipantTeamService, ParticipantTeamService>()
                .AddSingleton<ITimeService, TimeService>();
        }
    }
}
