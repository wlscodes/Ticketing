using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.ResponseModels
{
    public class TicketList
    {
        public int EventId { get; set; }
        public int TicketId { get; set; }
        public string EventName { get; set; }
        public DateTime BeginDate { get; set; }
    }
}
