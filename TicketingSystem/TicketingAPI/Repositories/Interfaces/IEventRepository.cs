using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface IEventRepository
    {
        public Task AddEvent(Event e);

        public bool IsEventDateCollision(DateTime begin, DateTime finish, int placeId);

        public Task<Event> GetEventById(int eventId);
    }
}
