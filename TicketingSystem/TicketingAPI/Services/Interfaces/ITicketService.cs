using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.Resources;

namespace TicketingAPI.Services.Interfaces
{
    public interface ITicketService
    {
        public Task<bool> CreateTicket(ReserveTicketResource resource);
    }
}
