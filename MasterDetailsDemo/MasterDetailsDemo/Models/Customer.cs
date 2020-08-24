using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterDetailsDemo.Models
{
    public class Customer
    {
        [Key]
        [StringLength(36)]
        public string CustomerId { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string PhoneNo { get; set; }
    }
}