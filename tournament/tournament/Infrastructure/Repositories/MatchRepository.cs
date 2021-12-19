using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tournament.Infrastructure.DataBase;
using tournament.Infrastructure.DataBase.Models;

namespace tournament.Infrastructure.Repositories
{
    public class MatchRepository : RepositoryBase<Match>
    {
        protected override DbSet<Match> ItemSet { get; }

        public MatchRepository(TournamentDbContext context) : base(context)
        {
            ItemSet = context.Matches;
        }

        protected override IQueryable<Match> IncludeDependencies(IQueryable<Match> queryable)
        {
            var test = queryable.Include(x => x.Teams).ThenInclude(x => x.Team)
                .Include(x => x.Tournament);
            return test;
        }

        public async Task<ICollection<Match>> GetByTournamentId(int tournamentId)
        {
            var matches = await IncludeDependencies(ItemSet).Where(x => x.TournamentId == tournamentId).ToArrayAsync();
            return matches;
        }
    }
}
