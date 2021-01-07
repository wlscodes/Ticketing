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

        public TestController(
            //IHashService hash, 
            //IUserRepository userRepository, 
            //IOrganizatorRepository organizatorRepository
            IClaimsService claimsService
            )
        {
            //_hash = hash;
            //_userRepository = userRepository;
            //_organizatorRepository = organizatorRepository;
            _claimsService = claimsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            //return _hash.HashPassword(pass);
            //return _hash.VerifyPassword("Qwerty", "$2a$11$AZwYTefeVqTac6ox25Exu.JnNpQsHLp3JjINAs590AieGIjg56xkS").ToString();

            return Ok();
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
