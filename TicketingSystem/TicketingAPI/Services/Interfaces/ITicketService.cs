using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.Resources;
using TicketingAPI.ResponseModels;

namespace TicketingAPI.Services.Interfaces
{
    public interface ITicketService
    {
        public Task<bool> CreateTicket(ReserveTicketResource resource);

        public Task<List<TicketList>> GetUserTickets(bool getArchieve, int userId);
    }
}
