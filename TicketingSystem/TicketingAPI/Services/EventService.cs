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
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<bool> AddEvent(EventResource resource)
        {
            Event e = new Event() 
            {
                AdministratorId = resource.UserId,
                OrganizatorId = resource.OrganizatorId,
                PlaceId = resource.PlaceId,
                BeginDate = resource.BeginDate,
                FinishDate = resource.FinishDate,
                Name = resource.Name
            };

            await _eventRepository.AddEvent(e);

            return true;
        }
    }
}
