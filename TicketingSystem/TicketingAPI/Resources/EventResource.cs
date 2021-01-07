using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingAPI.Resources
{
    public class EventResource : IValidatableObject
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int OrganizatorId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int UserId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int PlaceId { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime FinishDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(BeginDate < DateTime.Now)
            {
                yield return new ValidationResult("Begin date should be from future!", new[] { nameof(BeginDate) });
            }

            if(FinishDate <= BeginDate)
            {
                yield return new ValidationResult("Finish date must be greater than begin date!", new[] { nameof(FinishDate) });
            }
        }
    }
}
