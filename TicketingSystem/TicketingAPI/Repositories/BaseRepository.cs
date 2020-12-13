using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ContextData;

namespace TicketingAPI.Repositories
{
    public abstract class BaseRepository
    {
        protected ticketingContext Context { get; private set; }

        public BaseRepository(ticketingContext context)
        {
            Context = context;
        }

        protected void SaveAsync()
        {
            Context.SaveChanges();
        }
    }
}
