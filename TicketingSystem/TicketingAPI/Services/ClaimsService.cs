using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Services
{
    public class ClaimsService : IClaimsService
    {
        public int GetUserId(ClaimsIdentity identity)
        {
            if (identity is null)
                return 0;

            var claim = identity.Claims.Where(x => x.Type.Equals(ClaimTypes.SerialNumber)).FirstOrDefault().Value;

            int id = 0;

            _ = int.TryParse(claim, out id);

            return id;
        }
    }
}
