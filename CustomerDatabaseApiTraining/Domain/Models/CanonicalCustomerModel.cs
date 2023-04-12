using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class CanonicalCustomerModel
    {
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string FullName
        {
            get => FullName;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }
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
        [Required]
        public int MobileNumber { get; set; }
        [Required]
        public string Country
        {
            get => Country;
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
