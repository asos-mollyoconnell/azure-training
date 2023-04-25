using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        public string? Forename { get; set; }

     
        public string? Surname { get; set; }
     
        public DateTime DateOfBirth { get; set; }
      
        public string? Email { get; set; }

        public  AddressModel? Address { get; set; }
        public  ContactModel? Contact { get; set; }
    }
}