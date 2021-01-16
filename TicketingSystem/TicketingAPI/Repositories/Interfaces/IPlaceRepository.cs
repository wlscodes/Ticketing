using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface IPlaceRepository
    {
        public Task<bool> IsPlaceExistsInOrganizator(int organizatorId, string place);

        public Task<bool> IsPlaceExistsInOrganizator(int organizatorId, int placeId);

        public Task AddPlace(Place place);

        public Task<List<Place>> GetPlacesByOrganizatorId(int organizatorId);
    }
}
