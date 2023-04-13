﻿using System;
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
   
        public int Id { get; set; }
      
        public int CustomerId { get; set; }
     
        public int MobileNumber { get; set; }
        public int? HomeNumber { get; set; }
  

    }
}
