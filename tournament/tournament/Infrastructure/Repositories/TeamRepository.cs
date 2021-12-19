using Microsoft.EntityFrameworkCore;
using tournament.Infrastructure.DataBase;
using tournament.Infrastructure.DataBase.Models;

namespace tournament.Infrastructure.Repositories
{
    public class TeamRepository : RepositoryBase<Team>
    {
        protected override DbSet<Team> ItemSet { get; }

        public TeamRepository(TournamentDbContext context) : base(context)
        {
            ItemSet = context.Teams;         
        }
    }

}
