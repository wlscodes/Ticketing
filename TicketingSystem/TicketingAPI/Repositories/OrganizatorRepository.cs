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

        public Task<Organizator> GetOrganizatorAsync(int id)
        {
            return Context.Organizators.AsNoTracking().Where(x => x.OrganizatorId == id).FirstOrDefaultAsync();
        }

        public IEnumerable<Organizator> GetOrganizators()
        {
            return Context.Organizators;
        }
    }
}
