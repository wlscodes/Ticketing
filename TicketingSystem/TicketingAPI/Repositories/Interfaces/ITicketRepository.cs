using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        public Task<bool> IsTicketExists(int eventId, int seatId);

        public Task AddTicketAsync(Ticket ticket);
    }
}
