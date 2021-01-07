using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IPlaceRepository _placeRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventService _eventService;

        public EventController(IAdministratorRepository administratorRepository, IPlaceRepository placeRepository, IEventRepository eventRepository, IEventService eventService)
        {
            _administratorRepository = administratorRepository;
            _placeRepository = placeRepository;
            _eventRepository = eventRepository;
            _eventService = eventService;
        }

        [Authorize(Roles = "admin, superadmin, editor")]
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] EventResource resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var isAdminOfOrganization = await _administratorRepository.IsUserAdministratorOfOrganizator(resource.UserId, resource.OrganizatorId);
            if(!isAdminOfOrganization)
            {
                return NotFound();
            }

            var isPlaceExists = await _placeRepository.IsPlaceExistsInOrganizator(resource.OrganizatorId, resource.PlaceId);
            if (!isPlaceExists)
            {
                return NotFound();
            }

            var isEventDateCollision = _eventRepository.IsEventDateCollision(resource.BeginDate, resource.FinishDate, resource.PlaceId);
            if(isEventDateCollision)
            {
                return BadRequest();
            }

            await _eventService.AddEvent(resource);

            return Ok("event dodany");
        }
    }
}
