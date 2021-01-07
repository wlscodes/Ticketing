using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.Resources;

namespace TicketingAPI.Services.Interfaces
{
    public interface IEventService
    {
        public Task<bool> AddEvent(EventResource resource);
    }
}
