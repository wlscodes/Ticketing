using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingClient.Models
{
    public class Section
    {
        [RegularExpression(@"^[a-zA-Z0-9]{2,128}$")]
        public string SectionName { get; set; }

        [Required]
        [Range(1, 255)]
        public int RowsNumber { get; set; }

        [Required]
        [Range(1, 255)]
        public int SeatsInRow { get; set; }
    }
}
