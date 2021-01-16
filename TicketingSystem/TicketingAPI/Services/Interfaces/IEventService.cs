using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.Resources;
using TicketingAPI.ResponseModels;

namespace TicketingAPI.Services.Interfaces
{
    public interface IEventService
    {
        public Task<bool> AddEvent(EventResource resource);

        public Task<EventResponse> GetEventData(int eventId);

        public Task<IList<EventResponse>> GetLastEvents();
    }
}
