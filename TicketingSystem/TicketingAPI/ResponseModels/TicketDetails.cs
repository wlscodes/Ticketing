using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.ResponseModels
{
    public class TicketDetails
    {
        public int EventId { get; set; }
        public int SeatRow { get; set; }
        public int SeatNumber { get; set; }
        public string EventName { get; set; }
        public string OrganizatorName { get; set; }
        public string SectionName { get; set; }
        public string PlaceName { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}
