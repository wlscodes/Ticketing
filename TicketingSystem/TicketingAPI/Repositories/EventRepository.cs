using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ContextData;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.ResponseModels;

namespace TicketingAPI.Repositories
{
    public class EventRepository : BaseRepository, IEventRepository
    {
        public EventRepository(ticketingContext context) : base(context){ }

        public async Task AddEvent(Event e)
        {
            Context.Events.Add(e);

            SaveAsync();
        }

        public async Task<Event> GetEventById(int eventId)
        {
            return await Context.Events.Where(x => x.EventId == eventId).FirstOrDefaultAsync();
        }

        public bool IsEventDateCollision(DateTime begin, DateTime finish, int placeId)
        {
            return Context.Events.Where(x => x.PlaceId == placeId && ((begin >= x.BeginDate && begin <= x.FinishDate) || (finish >= x.BeginDate && finish <= x.FinishDate))).Any();
        }

        public async Task<IEnumerable<Event>> GetLastEvents()
        {
            return await Context.Events.Where(x => x.BeginDate > DateTime.Now).OrderBy(x => x.BeginDate).Take(3).ToListAsync();
        }

        public async Task<IEnumerable<EventResponse>> GetEvents()
        {
            var events = from e in Context.Events
                         join o in Context.Organizators on e.OrganizatorId equals o.OrganizatorId
                         where e.BeginDate > DateTime.Now
                         orderby e.EventId
                         select new EventResponse() { EventId = e.EventId, EventName = e.Name, OrganizatorName = o.Name, EventBeginDate = e.BeginDate };

            return await events.ToListAsync();
        }
    }
}
