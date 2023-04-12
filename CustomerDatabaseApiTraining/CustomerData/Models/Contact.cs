using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerData.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("customerId")]
        public int CustomerId { get; set; }
        [Required]
        public int MobileNumber { get; set; }
        public int? HomeNumber { get; set; }
    }
}
