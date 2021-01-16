using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        //private readonly IHashService _hash;
        //private readonly IUserRepository _userRepository;
        //private readonly IOrganizatorRepository _organizatorRepository;
        //private readonly IPlaceRepository _placeRepository;
        private readonly IClaimsService _claimsService;
        private readonly IEventService _eventService;
        private readonly IEventRepository _eventRepository;

        public TestController(
            //IHashService hash, 
            //IUserRepository userRepository, 
            //IOrganizatorRepository organizatorRepository
            IClaimsService claimsService,
            IEventService eventService,
            IEventRepository eventRepository
            )
        {
            //_hash = hash;
            //_userRepository = userRepository;
            //_organizatorRepository = organizatorRepository;
            _claimsService = claimsService;
            _eventService = eventService;
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {

            return Ok(await _eventService.GetEventData(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TestIndex([FromForm] CreatePlaceResource resource)
        {
            Console.WriteLine($"Name: {resource.PlaceName}");
            foreach(var r in resource.Sections)
            {
                Console.WriteLine($"Section: {r.SectionName} Rows: {r.RowsNumber} Seats: {r.SeatsInRow}");
            }
            return Ok("Test OK");
        }
    }
}
