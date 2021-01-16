using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ContextData;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;

namespace TicketingAPI.Repositories
{
    public class EventSeatRepository : BaseRepository, IEventSeatRepository
    {
        public EventSeatRepository(ticketingContext context) : base(context)
        {
        }

        public async Task<IList<EventSeats>> GetEventSeatsAsync(int eventId)
        {
            return await Context.EventSeats.Where(x => x.EventId == eventId).ToListAsync();
        }
    }
}
