using System;
using System.Collections.Generic;

#nullable disable

namespace TicketingAPI.DatabaseModels
{
    public partial class Section
    {
        public Section()
        {
            Seats = new HashSet<Seat>();
        }

        public int SectionId { get; set; }
        public int PlaceId { get; set; }
        public string Name { get; set; }

        public virtual Place Place { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
