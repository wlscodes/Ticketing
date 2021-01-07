using System;
using System.Collections.Generic;

#nullable disable

namespace TicketingAPI.DatabaseModels
{
    public partial class Seat
    {
        public Seat()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int SeatId { get; set; }
        public int SectionId { get; set; }
        public short SeatRow { get; set; }
        public short SeatNumber { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
