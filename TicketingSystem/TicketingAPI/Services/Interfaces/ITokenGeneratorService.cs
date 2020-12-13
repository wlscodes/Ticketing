using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.Services.Interfaces
{
    public interface ITokenGeneratorService
    {
        public string GenerateJwtToken(int userId);
    }
}
