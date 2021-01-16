using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.DatabaseModels
{
    public class EventSeats
    {
        public int EventId { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int SeatId { get; set; }
        public short SeatRow { get; set; }
        public short SeatNumber { get; set; }
        public bool IsSeatFree { get; set; }
    }
}
