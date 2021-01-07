using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ContextData;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;

namespace TicketingAPI.Repositories
{
    public class OrganizatorRepository : BaseRepository, IOrganizatorRepository
    {
        public OrganizatorRepository(ticketingContext context) : base(context) { }

        public async Task AddOrganizator(Organizator organizator)
        {
            Context.Organizators.Add(organizator);

            SaveAsync();
        }

        public Task<Organizator> GetOrganizatorByIdAsync(int id)
        {
            return Context.Organizators.AsNoTracking().Where(x => x.OrganizatorId == id).FirstOrDefaultAsync();
        }

        public Task<Organizator> GetOrganizatorByNameAsync(string name)
        {
            return Context.Organizators.AsNoTracking().Where(x => EF.Functions.ILike(x.Name, name)).FirstOrDefaultAsync();
        }

        public IEnumerable<Organizator> GetOrganizators()
        {
            return Context.Organizators;
        }

        public async Task<List<Organizator>> GetOrganizatorsByAdministratorId(int administratorId)
        {
            var list = from o in Context.Organizators
                       join a in Context.Administrators on o.OrganizatorId equals a.OrganizatorId
                       where a.UserId == administratorId
                       select o;

            return await list.ToListAsync();
        }
    }
}
