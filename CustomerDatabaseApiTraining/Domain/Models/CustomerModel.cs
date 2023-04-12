using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        public string Forename
        {
            get => Forename;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }

        [Required]
        public string Surname
        {
            get => Surname;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email
        {
            get => Email;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }
    }
}