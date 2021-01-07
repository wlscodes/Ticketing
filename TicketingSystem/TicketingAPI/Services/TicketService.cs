﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;
using TicketingAPI.Resources;
using TicketingAPI.Services.Interfaces;

namespace TicketingAPI.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<bool> CreateTicket(ReserveTicketResource resource)
        {
            Ticket ticket = new Ticket
            {
                EventId = resource.EventId,
                SeatId = resource.SeatId,
                UserId = resource.UserId
            };

            await _ticketRepository.AddTicketAsync(ticket);

            return true;
        }
    }
}
