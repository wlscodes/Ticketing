using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface IEventSeatRepository
    {
        public Task<IList<EventSeats>> GetEventSeatsAsync(int eventId);
    }
}
