using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MasterDetailsDemo.Models
{
    public class SaleInvoiceItem
    {
        [Key]
        [StringLength(36)]
        public string SaleInvoiceItemId { get; set; }

        [Required]
        [ForeignKey("SaleInvoiceHeader")]
        [StringLength(36)]
        public string SaleInvoiceHeaderId { get; set; }

        [Required]
        [ForeignKey("Product")]
        [StringLength(36)]
        public string ProductId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal Amount { get; set; }
        
        
        public virtual SaleInvoiceHeader SaleInvoiceHeader { get; set; }
        public virtual Product Product { get; set; }
    }
}