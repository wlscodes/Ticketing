using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;

        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public Task<bool> AreAllSeatsInEventPlace(List<int> seats, int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AreSeatsFreeForEvent(List<int> seats, int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
