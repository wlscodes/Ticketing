using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;
using TicketingAPI.ResponseModels;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface IEventRepository
    {
        public Task AddEvent(Event e);

        public bool IsEventDateCollision(DateTime begin, DateTime finish, int placeId);

        public Task<Event> GetEventById(int eventId);

        public Task<IEnumerable<Event>> GetLastEvents();

        public Task<IEnumerable<EventResponse>> GetEvents();
    }
}
