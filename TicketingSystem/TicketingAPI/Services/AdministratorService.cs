using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository;

        public AdministratorService(IAdministratorRepository administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }

        public async Task<bool> CreateNewAdministrator(int userId, int organizatorId)
        {
            Administrator administrator = new Administrator()
            {
                UserId = userId,
                OrganizatorId = organizatorId
            };

            try
            {
                await _administratorRepository.AddAdministrator(administrator);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
