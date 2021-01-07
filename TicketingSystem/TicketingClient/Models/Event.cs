using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingClient.Models
{
    public class Event
    {
        public int OrganizatorId { get; set; }
        public int AdministratorId { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int PlaceId { get; set; }
    }
}
