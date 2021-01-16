using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingClient.Models
{
    public class Event
    { 
        [RegularExpression(@"^[0-9]{1,10}$")]
        public string OrganizatorId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]{5,128}$")]
        public string Name { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime FinishDate { get; set; }

        [RegularExpression(@"^[0-9]{1,10}$")]
        public string PlaceId { get; set; }
    }
}
