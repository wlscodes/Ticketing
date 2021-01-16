using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.ResponseModels;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketService _ticketService;
        private readonly IClaimsService _claimsService;

        public TicketController(IEventRepository eventRepository, 
            IUserRepository userRepository, 
            ITicketRepository ticketRepository, 
            ITicketService ticketService,
            ISeatRepository seatRepository,
            IClaimsService claimsService)
        {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _ticketService = ticketService;
            _seatRepository = seatRepository;
            _claimsService = claimsService;
        }

        [Route("v1/[controller]")]
        [Authorize]
        [HttpGet]
        public async Task<List<TicketList>> UserTickets([FromQuery]bool archieve = false)
        {
            var claimId = _claimsService.GetUserId(HttpContext.User.Identity as ClaimsIdentity);
            if (claimId == 0)
            {
                return null;
            }

            return await _ticketService.GetUserTickets(archieve, claimId);
        }

        [Route("v1/[controller]/details")]
        [Authorize]
        [HttpGet]
        public async Task<TicketDetails> GetTicketById([FromQuery]int ticketId)
        {
            return await _ticketRepository.GetTicketDetails(ticketId);
        }

        [Route("v1/[controller]")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] ReserveTicketResource resource)
        {
            var claimId = _claimsService.GetUserId(HttpContext.User.Identity as ClaimsIdentity);
            if (claimId == 0)
            {
                return Unauthorized("You don't have permission to create a place");
            }
            resource.UserId = claimId;

            var user = await _userRepository.GetUserByIdAsync(resource.UserId);
            if(user is null)
            {
                return NotFound("User doesn't exists");
            }

            var e = await _eventRepository.GetEventById(resource.EventId);
            if(e is null)
            {
                return NotFound("Event doesn't exists");
            }

            if(e.BeginDate <= DateTime.UtcNow)
            {
                return BadRequest("Event is from past");
            }

            var seatExists = await _seatRepository.AreAllSeatsInEventPlace(resource.SeatId, resource.EventId);
            if(!seatExists)
            {
                return NotFound("Some seats not exists");
            }

            var seatTaken = await _ticketRepository.AreTicketsExists(resource.EventId, resource.SeatId);
            if(!seatTaken)
            {
                return BadRequest("Some seats are busy");
            }

            await _ticketService.CreateTicket(resource);

            return Ok("Ticket/s reserved");
        }

        [Route("v1/[controller]")]
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteTicket([FromQuery] int ticketId)
        {
            var claimId = _claimsService.GetUserId(HttpContext.User.Identity as ClaimsIdentity);
            if (claimId == 0)
            {
                return Unauthorized();
            }

            var isOwner = await _ticketRepository.IsUserOwnerOfTicket(ticketId, claimId);
            if(!isOwner)
            {
                return NotFound("You are not an owner of ticket");
            }

            var remove = await _ticketRepository.RemoveTicket(ticketId);

            return Ok("Ticket has been removed");
        }
    }
}
