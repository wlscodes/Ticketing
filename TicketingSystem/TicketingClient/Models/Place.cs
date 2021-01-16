using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingClient.Models
{
    public class Place
    {
        [Required]
        //[Range(1, Int32.MaxValue)]
        [RegularExpression(@"^[0-9]{1,10}$")]
        public string OrganizatorId { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string PlaceName { get; set; }

        [Required]
        public List<Section> Sections { get; set; }
    }
}
