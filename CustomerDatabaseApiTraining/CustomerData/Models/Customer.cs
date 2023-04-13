using System.ComponentModel.DataAnnotations;

namespace CustomerData.Models
{
    public class Customer
    {
        public int Id { get; set; }

   
        public string? Forename { get; set; }


        public string? Surname { get; set; }
     
        public DateTime DateOfBirth { get; set; }
    
        public string? Email { get; set; }

        public virtual Address? Address { get; set; }
        public virtual Contact? Contact { get; set; }

    }
}