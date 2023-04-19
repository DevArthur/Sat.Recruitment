using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sat.Recruitment.Persistence.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The email is required")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The phone is required")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "A phone number should has at least 10 characters.")]
        public string Phone { get; set; }

        public string UserType { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Money { get; set; }
    }
}
