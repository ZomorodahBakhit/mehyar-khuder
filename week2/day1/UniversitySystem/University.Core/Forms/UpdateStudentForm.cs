using System.ComponentModel.DataAnnotations;

namespace University.Core.Forms
{
    public class UpdateStudentForm
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}