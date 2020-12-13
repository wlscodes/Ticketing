using System;
using System.Collections.Generic;

#nullable disable

namespace TicketingAPI.DatabaseModels
{
    public partial class Event
    {
        public Event()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int EventId { get; set; }
        public int OrganizatorId { get; set; }
        public int AdministratorId { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int PlaceId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual User Administrator { get; set; }
        public virtual Organizator Organizator { get; set; }
        public virtual Place Place { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
