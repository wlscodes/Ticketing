using System;
using System.Collections.Generic;

#nullable disable

namespace TicketingAPI.DatabaseModels
{
    public partial class User
    {
        public User()
        {
            Administrators = new HashSet<Administrator>();
            Events = new HashSet<Event>();
            Places = new HashSet<Place>();
            Tickets = new HashSet<Ticket>();
        }

        public int UserId { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public short Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual ICollection<Administrator> Administrators { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Place> Places { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
