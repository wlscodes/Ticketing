using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.Services.Interfaces
{
    public interface ISeatService
    {
        Task<bool> AreAllSeatsInEventPlace(List<int> seats, int eventId);

        Task<bool> AreSeatsFreeForEvent(List<int> seats, int eventId);
    }
}
