using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.ResponseModels;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    //[Route("v1/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventService _eventService;
        private readonly IClaimsService _claimsService;

        public EventController(IAdministratorRepository administratorRepository, IPlaceRepository placeRepository, IEventRepository eventRepository, IEventService eventService, IClaimsService claimsService)
        {
            _administratorRepository = administratorRepository;
            _placeRepository = placeRepository;
            _eventRepository = eventRepository;
            _eventService = eventService;
            _claimsService = claimsService;
        }

        [Route("v1/[controller]")]
        [HttpGet]
        public async Task<EventResponse> GetEvent([FromQuery] int eventId)
        {
            return await _eventService.GetEventData(eventId);
        }

        [Route("v1/[controller]/last")]
        [HttpGet]
        public async Task<IList<EventResponse>> GetLastEvents()
        {
            return await _eventService.GetLastEvents();
        }

        [Route("v1/[controller]/list")]
        [HttpGet]
        public async Task<IEnumerable<EventResponse>> GetEventList()
        {
            return await _eventRepository.GetEvents();
        }

        [Route("v1/[controller]")]
        [Authorize(Roles = "admin, superadmin, editor")]
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] EventResource resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var claimId = _claimsService.GetUserId(HttpContext.User.Identity as ClaimsIdentity);
            if (claimId == 0)
            {
                return Unauthorized("You don't have permission to create a place");
            }
            resource.UserId = claimId;

            var isAdminOfOrganization = await _administratorRepository.IsUserAdministratorOfOrganizator(resource.UserId, resource.OrganizatorId);
            if(!isAdminOfOrganization)
            {
                return NotFound("You are not an administrator of this organizator");
            }

            var isPlaceExists = await _placeRepository.IsPlaceExistsInOrganizator(resource.OrganizatorId, resource.PlaceId);
            if (!isPlaceExists)
            {
                return NotFound("Place doesn't exists or is not connected with organizator");
            }

            var isEventDateCollision = _eventRepository.IsEventDateCollision(resource.BeginDate, resource.FinishDate, resource.PlaceId);
            if(isEventDateCollision)
            {
                return BadRequest("There is other event in this place between your dates");
            }

            await _eventService.AddEvent(resource);

            return Ok("Event has been created");
        }
    }
}
