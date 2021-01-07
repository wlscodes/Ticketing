using System;
using System.Collections.Generic;

#nullable disable

namespace TicketingAPI.DatabaseModels
{
    public partial class Place
    {
        public Place()
        {
            Events = new HashSet<Event>();
            Sections = new HashSet<Section>();
        }

        public int PlaceId { get; set; }
        public string Name { get; set; }
        public int OrganizatorId { get; set; }
        public int CreatorId { get; set; }
        public DateTime? InsertDate { get; set; }

        public virtual User Creator { get; set; }
        public virtual Organizator Organizator { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
