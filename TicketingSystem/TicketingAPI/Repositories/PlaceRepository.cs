using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ContextData;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;

namespace TicketingAPI.Repositories
{
    public class PlaceRepository : BaseRepository, IPlaceRepository
    {
        public PlaceRepository(ticketingContext context) : base(context) { }

        public async Task AddPlace(Place place)
        {
            Context.Places.Add(place);

            SaveAsync();
        }

        public async Task<bool> IsPlaceExistsInOrganizator(int organizatorId, string place)
        {
            return await Task.Run(() => Context.Places.Where(x => x.OrganizatorId == organizatorId && EF.Functions.ILike(x.Name, place)).Any());
        }

        public async Task<bool> IsPlaceExistsInOrganizator(int organizatorId, int placeId)
        {
            return await Task.Run(() => Context.Places.Where(x => x.OrganizatorId == organizatorId && x.PlaceId == placeId).Any());
        }
    }
}
