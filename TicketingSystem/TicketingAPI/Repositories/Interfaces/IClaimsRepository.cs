using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface IClaimsRepository
    {
        public Task<IList<Claim>> GetClaimsAsync(int userId);
    }
}
