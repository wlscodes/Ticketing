using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TicketingAPI.Services.Interfaces
{
    public interface IClaimsService
    {
        public int GetUserId(ClaimsIdentity identity);
    }
}
