using System.ComponentModel.DataAnnotations;

namespace CustomerData.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Forename { get; set; }

        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
    }
}