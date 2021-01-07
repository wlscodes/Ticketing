using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface ISeatRepository
    {
        public Task<bool> IsSeatInEventPlace(int seatId, int eventId);

        public Task<Seat> GetSeatById(int seatId);
    }
}
