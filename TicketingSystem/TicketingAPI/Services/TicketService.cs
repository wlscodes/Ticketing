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
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<bool> CreateTicket(ReserveTicketResource resource)
        {
            List<Ticket> list = new List<Ticket>();

            foreach(var seat in resource.SeatId)
            {
                list.Add(new Ticket()
                {
                    EventId = resource.EventId,
                    SeatId = seat,
                    UserId =resource.UserId
                });
            }

            await _ticketRepository.AddTicketAsync(list);

            return true;
        }

        public async Task<List<TicketList>> GetUserTickets(bool getArchieve, int userId)
        {
            if (getArchieve)
            {
                return await _ticketRepository.GetTicketsByUserID(userId);
            }
            else
            {
                return await _ticketRepository.GetActiveTicketsByUserID(userId);
            }
        }
    }
}
