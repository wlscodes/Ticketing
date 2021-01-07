using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.Resources
{
    public class ReserveTicketResource
    {
        [Range(1, Int32.MaxValue)]
        public int EventId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int UserId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int SeatId { get; set; }
    }
}
