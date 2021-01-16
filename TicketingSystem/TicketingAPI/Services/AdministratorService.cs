using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.ResponseModels;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IOrganizatorRepository _organizatorRepository;

        public AdministratorService(IAdministratorRepository administratorRepository, IOrganizatorRepository organizatorRepository)
        {
            _administratorRepository = administratorRepository;
            _organizatorRepository = organizatorRepository;
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

        public async Task<IEnumerable<OrganizatorSelect>> GetOrganizatorSelectsAsync(int userId)
        {
            var organizators = await _organizatorRepository.GetOrganizatorsByAdministratorId(userId);

            var list = new HashSet<OrganizatorSelect>();

            if (!(organizators is null))
            {
                foreach (var o in organizators)
                {
                    list.Add(new OrganizatorSelect { Id = o.OrganizatorId, Name = o.Name });
                }
            }

            return list;
        }
    }
}
