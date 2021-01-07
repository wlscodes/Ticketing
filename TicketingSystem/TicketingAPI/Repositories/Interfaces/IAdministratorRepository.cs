using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface IAdministratorRepository
    {
        public Task AddAdministrator(Administrator administrator);

        public Task<bool> IsUserAdministratorOfOrganizator(int user, int organizator);
    }
}
