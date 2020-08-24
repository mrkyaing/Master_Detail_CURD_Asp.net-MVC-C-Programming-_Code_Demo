using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MasterDetailsDemo.Models
{
    public class SaleInvoiceHeader
    {
        [Key]
        [StringLength(36)]
        public string SaleInvoiceHeaderId { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public decimal? TotalAmount { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        
    }
}