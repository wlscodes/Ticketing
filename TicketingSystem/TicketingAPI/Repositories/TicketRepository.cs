using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ContextData;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;

namespace TicketingAPI.Repositories
{
    public class TicketRepository : BaseRepository, ITicketRepository
    {
        public TicketRepository(ticketingContext context) : base(context)
        {
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            await Context.Tickets.AddAsync(ticket);

            SaveAsync();
        }

        public async Task<bool> IsTicketExists(int eventId, int seatId)
        {
            return Context.Tickets.Where(x => x.EventId == eventId && x.SeatId == seatId).Any();
        }
    }
}
