using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Services
{
    public class OrganizatorService : IOrganizatorService
    {
        private readonly IOrganizatorRepository _organizatorRepository;
        private readonly IAdministratorService _administratorService;
        private readonly IUserRepository _userRepository;

        public OrganizatorService(IOrganizatorRepository organizatorRepository, IAdministratorService administratorService, IUserRepository userRepository)
        {
            _organizatorRepository = organizatorRepository;
            _administratorService = administratorService;
            _userRepository = userRepository;
        }

        public async Task<Organizator> CreateNewOrganizator(OrganizatorResource resource)
        {
            try
            {
                Organizator organizator = new Organizator()
                {
                    Name = resource.Name
                };

                await _organizatorRepository.AddOrganizator(organizator);

                var createAdminResult = await _administratorService.CreateNewAdministrator(resource.CreatorId, organizator.OrganizatorId);

                if (!createAdminResult) return null;

                var updateUserRole = await _userRepository.UpdateUserRoleToAdmin(resource.CreatorId);

                if (!updateUserRole) return null;

                return organizator;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
