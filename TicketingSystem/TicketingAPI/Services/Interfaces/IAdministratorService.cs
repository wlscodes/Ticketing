﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ResponseModels;

namespace TicketingAPI.Services.Interfaces
{
    public interface IAdministratorService
    {
        public Task<bool> CreateNewAdministrator(int userId, int organizatorId);

        public Task<IEnumerable<OrganizatorSelect>> GetOrganizatorSelectsAsync(int userId);
    }
}
