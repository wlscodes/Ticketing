using System;
using System.Collections.Generic;

#nullable disable

namespace TicketingAPI.DatabaseModels
{
    public partial class Administrator
    {
        public int AdministratorId { get; set; }
        public int UserId { get; set; }
        public int OrganizatorId { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual Organizator Organizator { get; set; }
        public virtual User User { get; set; }
    }
}
