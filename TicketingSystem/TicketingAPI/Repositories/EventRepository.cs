using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ContextData;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;

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
            return await Context.Events.Where(x => x.EventId == eventId).FirstAsync();
        }

        public bool IsEventDateCollision(DateTime begin, DateTime finish, int placeId)
        {
            return Context.Events.Where(x => x.PlaceId == placeId && ((begin >= x.BeginDate && begin <= x.FinishDate) || (finish >= x.BeginDate && finish <= x.FinishDate))).Any();
        }
    }
}
