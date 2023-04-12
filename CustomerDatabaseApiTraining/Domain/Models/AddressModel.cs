using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public string Line1
        {
            get => Line1;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }
        public string? Line2 { get; set; }
        [Required]
        public string City
        {
            get => City;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }
        [Required]
        public string County
        {
            get => County;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }
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
        [Required]
        public string Postcode
        {
            get => Postcode;
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
