using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;
using TicketingAPI.ResponseModels;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        public Task<bool> IsTicketExists(int eventId, int seatId);

        public Task AddTicketAsync(List<Ticket> ticket);

        public Task<bool> AreTicketsExists(int eventId, List<int> seatsId);

        public Task<List<TicketList>> GetActiveTicketsByUserID(int userId);

        public Task<List<TicketList>> GetTicketsByUserID(int userId);

        public Task<bool> IsUserOwnerOfTicket(int ticketId, int userId);

        public Task<bool> RemoveTicket(int ticketId);

        public Task<TicketDetails> GetTicketDetails(int ticketId);
    }
}
