using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.ResponseModels
{
    public class SectionResponse
    {
        public string SectionName { get; set; }
        public List<SeatResponse> Seats { get; set; }
    }
}
