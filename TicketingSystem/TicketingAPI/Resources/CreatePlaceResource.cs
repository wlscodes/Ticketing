using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.Resources
{
    public class CreatePlaceResource
    {
        public int UserId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int OrganizatorId { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string PlaceName { get; set; }

        [Required]
        //TODO: Check if there are unique sections names
        public List<SectionResource> Sections { get; set; } 
    }
}
