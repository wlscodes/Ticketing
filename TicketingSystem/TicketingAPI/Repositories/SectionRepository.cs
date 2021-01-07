using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ContextData;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;

namespace TicketingAPI.Repositories
{
    public class SectionRepository : BaseRepository, ISectionRepository
    {
        public SectionRepository(ticketingContext context) : base(context)
        {
        }

        public async Task AddSectionRangeAsync(IList<Section> sections)
        {
            await Context.Sections.AddRangeAsync(sections);

            SaveAsync();
        }
    }
}
