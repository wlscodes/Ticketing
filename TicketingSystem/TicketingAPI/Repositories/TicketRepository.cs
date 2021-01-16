using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ContextData;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.ResponseModels;

namespace TicketingAPI.Repositories
{
    public class TicketRepository : BaseRepository, ITicketRepository
    {
        public TicketRepository(ticketingContext context) : base(context)
        {
        }

        public async Task AddTicketAsync(List<Ticket> ticket)
        {
            await Context.Tickets.AddRangeAsync(ticket);

            SaveAsync();
        }

        public async Task<bool> IsTicketExists(int eventId, int seatId)
        {
            return Context.Tickets.Where(x => x.EventId == eventId && x.SeatId == seatId).Any();
        }

        public async Task<bool> AreTicketsExists(int eventId, List<int> seatsId)
        {
            var seats = await Context.Tickets.Where(x => x.EventId == eventId).Select(x => x.SeatId).ToListAsync();

            return seats.Intersect(seatsId).Count() == 0;
        }

        public async Task<List<TicketList>> GetActiveTicketsByUserID(int userId)
        {
            var tickets = from t in Context.Tickets
                          join e in Context.Events on t.EventId equals e.EventId
                          where t.UserId == userId && e.BeginDate > DateTime.UtcNow
                          select new TicketList() { EventId = e.EventId, EventName = e.Name, TicketId = t.TicketId, BeginDate = e.BeginDate };

            return await tickets.ToListAsync();
        }

        public async Task<List<TicketList>> GetTicketsByUserID(int userId)
        {
            var tickets = from t in Context.Tickets
                          join e in Context.Events on t.EventId equals e.EventId
                          where t.UserId == userId && e.BeginDate < DateTime.UtcNow
                          select new TicketList() { EventId = e.EventId, EventName = e.Name, TicketId = t.TicketId, BeginDate = e.BeginDate };

            return await tickets.ToListAsync();
        }

        public async Task<bool> IsUserOwnerOfTicket(int ticketId, int userId)
        {
            return await Context.Tickets.Where(x => x.TicketId == ticketId && x.UserId == userId).AnyAsync();
        }

        public async Task<bool> RemoveTicket(int ticketId)
        {
            var ticket = await Context.Tickets.Where(x => x.TicketId == ticketId).FirstOrDefaultAsync();

            Context.Remove(ticket);
            SaveAsync();

            return true;
        }

        public async Task<TicketDetails> GetTicketDetails(int ticketId)
        {
            var ticket = from t in Context.Tickets
                         join e in Context.Events on t.EventId equals e.EventId
                         join o in Context.Organizators on e.OrganizatorId equals o.OrganizatorId
                         join sea in Context.Seats on t.SeatId equals sea.SeatId
                         join sec in Context.Sections on sea.SectionId equals sec.SectionId
                         join p in Context.Places on sec.PlaceId equals p.PlaceId
                         where t.TicketId == ticketId
                         select new TicketDetails() { 
                             EventId = e.EventId, 
                             EventName = e.Name, 
                             BeginDate = e.BeginDate, 
                             FinishDate = e.FinishDate, 
                             OrganizatorName = o.Name,
                             PlaceName = p.Name,
                             SectionName = sec.Name,
                             SeatNumber = sea.SeatNumber,
                             SeatRow = sea.SeatRow
                         };

            return await ticket.FirstOrDefaultAsync();
        }
    }
}
