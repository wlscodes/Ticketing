using System;
using System.Collections.Generic;

#nullable disable

namespace TicketingAPI.DatabaseModels
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int SeatId { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual Event Event { get; set; }
        public virtual Seat Seat { get; set; }
        public virtual User User { get; set; }
    }
}
