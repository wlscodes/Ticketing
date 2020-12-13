using System;
using System.Collections.Generic;

#nullable disable

namespace TicketingAPI.DatabaseModels
{
    public partial class Organizator
    {
        public Organizator()
        {
            Administrators = new HashSet<Administrator>();
            Events = new HashSet<Event>();
            Places = new HashSet<Place>();
        }

        public int OrganizatorId { get; set; }
        public string Name { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual ICollection<Administrator> Administrators { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Place> Places { get; set; }
    }
}
