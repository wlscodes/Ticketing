using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface IOrganizatorRepository
    {
        public Task<Organizator> GetOrganizatorByIdAsync(int id);

        public Task<Organizator> GetOrganizatorByNameAsync(string name);

        public IEnumerable<Organizator> GetOrganizators();

        public Task AddOrganizator(Organizator organizator);

        public Task<List<Organizator>> GetOrganizatorsByAdministratorId(int administratorId);
    }
}
