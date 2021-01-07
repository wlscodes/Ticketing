using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IPlaceRepository _placeRepository;

        private Place _place;
        private IList<Section> sections;

        public PlaceService(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<bool> AddPlace(CreatePlaceResource resource)
        {
            _place = new Place
            {
                CreatorId = resource.UserId,
                Name = resource.PlaceName,
                OrganizatorId = resource.OrganizatorId,
                Sections = GenerateSections(resource.Sections).ToList()
            };

            await _placeRepository.AddPlace(_place);

            return true;
        }

        private IEnumerable<Section> GenerateSections(IList<SectionResource> sections)
        {
            foreach(var s in sections)
            {
                yield return new Section
                {
                    Name = s.SectionName,
                    Seats = AddSeats(s.RowsNumber, s.SeatsInRow).ToList()
                };
            }
        }

        private IEnumerable<Seat> AddSeats(short rows, short numberOfSeats)
        {
            for(short i = 1; i <= rows; i++)
            {
                for (short j = 1; j <= numberOfSeats; j++)
                    yield return new Seat { SeatRow = i, SeatNumber = j };
            }
        }
    }
}
