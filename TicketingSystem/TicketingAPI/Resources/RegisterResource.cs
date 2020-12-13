using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.Resources
{
    public class RegisterResource : LoginResource, IValidatableObject
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
        public string RepeatPassword { get; set; }

        [Required]
        public bool AcceptRules { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!AcceptRules)
            {
                yield return new ValidationResult("You must accept the rules!", new[] { nameof(AcceptRules) });
            }

            if(!Password.Equals(RepeatPassword))
            {
                yield return new ValidationResult("Passwords are different!", new[] { nameof(RepeatPassword) });
            }
        }
    }
}
