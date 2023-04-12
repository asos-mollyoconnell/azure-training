using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class CustomerModel
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

        public virtual AddressModel Address { get; set; }
        public virtual ContactModel Contact { get; set; }
    }
}