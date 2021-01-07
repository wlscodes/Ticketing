using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingClient.Models
{
    public class Section
    {
        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string SectionName { get; set; }

        [Required]
        [Range(1, 256)]
        public int RowsNumber { get; set; }

        [Required]
        [Range(1, 256)]
        public int SeatsInRow { get; set; }
    }
}
