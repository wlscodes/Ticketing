using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ContextData;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;

namespace TicketingAPI.Repositories
{
    public class AdministratorRepository : BaseRepository, IAdministratorRepository
    {
        public AdministratorRepository(ticketingContext context) : base(context){ }

        public async Task AddAdministrator(Administrator administrator)
        {
            Context.Administrators.Add(administrator);

            SaveAsync();
        }

        public async Task<bool> IsUserAdministratorOfOrganizator(int userId, int organizatorId)
        {
            return await Task.Run( () => Context.Administrators.Where(x => x.UserId == userId && x.OrganizatorId == organizatorId).Any());
        }
    }
}
