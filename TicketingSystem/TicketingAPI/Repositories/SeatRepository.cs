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
    public class SeatRepository : BaseRepository, ISeatRepository
    {
        public SeatRepository(ticketingContext context) : base(context) { }

        public async Task<Seat> GetSeatById(int seatId)
        {
            return await Context.Seats.Where(x => x.SeatId == seatId).FirstAsync();
        }

        public async Task<bool> IsSeatInEventPlace(int seatId, int eventId)
        {
            var seat = from e in Context.Events
                       join sc in Context.Sections on e.PlaceId equals sc.PlaceId
                       join se in Context.Seats on sc.SectionId equals se.SectionId
                       where e.EventId == eventId && se.SeatId == seatId
                       select se;

            return seat.Count() == 1;
        }
    }
}
