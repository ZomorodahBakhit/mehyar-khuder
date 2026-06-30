using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace University.Core.Forms
{
    public class CreateCourseForm : IValidatableObject
    {
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string CourseName { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult("EndDate must be after StartDate.", [nameof(EndDate)]);
            }
        }
    }
}
