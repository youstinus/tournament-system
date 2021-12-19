using Microsoft.EntityFrameworkCore;
using System.Linq;
using tournament.Infrastructure.DataBase;
using tournament.Infrastructure.DataBase.Models;

namespace tournament.Infrastructure.Repositories
{
    public class ParticipantRepository : RepositoryBase<Participant>
    {
        protected override DbSet<Participant> ItemSet { get; }

        public ParticipantRepository(TournamentDbContext context) : base(context)
        {
            ItemSet = context.Participants;
        }

        protected override IQueryable<Participant> IncludeDependencies(IQueryable<Participant> queryable)
        {
            var test = queryable.Include(x => x.ParticipantTeams).ThenInclude(x => x.Team);
            return test;
        }
    }
}
