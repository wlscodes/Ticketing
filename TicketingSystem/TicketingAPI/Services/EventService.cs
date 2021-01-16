using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.ResponseModels;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IOrganizatorRepository _organizatorRepository;
        private readonly IEventSeatRepository _eventSeatRepository;

        public EventService(IEventRepository eventRepository, IOrganizatorRepository organizatorRepository, IEventSeatRepository eventSeatRepository)
        {
            _eventRepository = eventRepository;
            _organizatorRepository = organizatorRepository;
            _eventSeatRepository = eventSeatRepository;
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

        public async Task<EventResponse> GetEventData(int eventId)
        {
            EventResponse eventResponse = new EventResponse();

            var eve = await _eventRepository.GetEventById(eventId);
            if(eve is null)
            {
                return null;
            }

            eventResponse.EventId = eventId;
            eventResponse.EventName = eve.Name;
            eventResponse.EventBeginDate = eve.BeginDate;
            eventResponse.EventFinishDate = eve.FinishDate;

            var organizator = await _organizatorRepository.GetOrganizatorByIdAsync(eve.OrganizatorId);
            if(organizator is null)
            {
                return null;
            }

            eventResponse.OrganizatorId = organizator.OrganizatorId;
            eventResponse.OrganizatorName = organizator.Name;
            eventResponse.Sections = await _eventSeatRepository.GetEventSeatsAsync(eventId);

            return eventResponse;
        }

        public async Task<IList<EventResponse>> GetLastEvents()
        {
            var lastEvents = await _eventRepository.GetLastEvents();

            IList <EventResponse> list = new List<EventResponse>();
            foreach(var eve in lastEvents)
            {
                list.Add(new EventResponse() { EventId = eve.EventId, EventName = eve.Name , EventBeginDate = eve.BeginDate});
            }
            return list;

        }
    }
}
