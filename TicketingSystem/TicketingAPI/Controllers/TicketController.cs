using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketService _ticketService;

        public TicketController(IEventRepository eventRepository, 
            IUserRepository userRepository, 
            ITicketRepository ticketRepository, 
            ITicketService ticketService,
            ISeatRepository seatRepository)
        {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _ticketService = ticketService;
            _seatRepository = seatRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] ReserveTicketResource resource)
        {
            var user = await _userRepository.GetUserByIdAsync(resource.UserId);
            if(user is null)
            {
                return NotFound();
            }

            var e = await _eventRepository.GetEventById(resource.EventId);
            if(e is null)
            {
                return NotFound();
            }

            var seatExists = await _seatRepository.IsSeatInEventPlace(resource.SeatId, resource.EventId);
            if(!seatExists)
            {
                return NotFound();
            }

            var seatTaken = await _ticketRepository.IsTicketExists(resource.EventId, resource.SeatId);
            if(seatTaken)
            {
                return BadRequest();
            }

            await _ticketService.CreateTicket(resource);

            return Ok();
        }

    }
}
