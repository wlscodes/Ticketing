using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface IOrganizatorRepository
    {
        public Task<Organizator> GetOrganizatorAsync(int id);

        public IEnumerable<Organizator> GetOrganizators();
    }
}
