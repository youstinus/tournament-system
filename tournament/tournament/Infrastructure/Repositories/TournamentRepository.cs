using Microsoft.EntityFrameworkCore;
using System.Linq;
using tournament.Infrastructure.DataBase;
using tournament.Infrastructure.DataBase.Models;

namespace tournament.Infrastructure.Repositories
{
    public class TournamentRepository : RepositoryBase<Tournament>
    {
        protected override DbSet<Tournament> ItemSet { get; }

        public TournamentRepository(TournamentDbContext context) : base(context)
        {
            ItemSet = context.Tournaments;
        }

        protected override IQueryable<Tournament> IncludeDependencies(IQueryable<Tournament> queryable)
        {
            var dependencies = queryable.Include(x => x.TournamentTeams).ThenInclude(x => x.Team);
            dependencies.Include(x => x.Matches);
            return dependencies;
        }
    }
}
