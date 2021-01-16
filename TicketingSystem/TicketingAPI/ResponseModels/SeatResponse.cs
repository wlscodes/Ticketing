using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.ResponseModels
{
    public class SeatResponse
    {
        public int SeatId { get; set; }
        public int SeatRow { get; set; }
        public int SeatNumber { get; set; }
        public bool SeatBusy { get; set; }
    }
}
