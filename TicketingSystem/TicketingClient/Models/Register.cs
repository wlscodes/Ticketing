using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingClient.Models
{
    public class Register : Login
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]{3,32}$")]
        [StringLength(32, MinimumLength = 3)]
        public string Login { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,32}$")]
        [StringLength(32, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{3,32}$")]
        [StringLength(32, MinimumLength = 3)]
        public string Surname { get; set; }

        [Required]
        [Compare("Password")]
        public string RepeatPassword { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true")]
        public bool AcceptRules { get; set; }
    }
}
