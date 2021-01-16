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

        public int UserId { get; set; }

        [Required]
        public List<int> SeatId { get; set; }
    }
}
