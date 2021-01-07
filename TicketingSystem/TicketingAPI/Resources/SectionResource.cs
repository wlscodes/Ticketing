using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.Resources
{
    public class SectionResource
    {
        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string SectionName { get; set; }

        [Required]
        [Range(1, short.MaxValue)]
        public short RowsNumber { get; set; }

        [Required]
        [Range(1, short.MaxValue)]
        public short SeatsInRow { get; set; }
    }
}
