using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Resources;

namespace TicketingAPI.Services.Interfaces
{
    public interface IOrganizatorService
    {
        public Task<Organizator> CreateNewOrganizator(OrganizatorResource resource);
    }
}
