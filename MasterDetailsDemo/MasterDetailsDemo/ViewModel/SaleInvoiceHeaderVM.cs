using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MasterDetailsDemo.Models;

namespace MasterDetailsDemo.ViewModel
{
    public class SaleInvoiceHeaderVM
    {
        [Key]
        [StringLength(36)]
        public string SaleInvoiceHeaderId { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public decimal? TotalAmount { get; set; }

        public string CustomerId { get; set; }

        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
        public List<SaleInvoiceItemVM> saleInvoiceItemVMs { get; set; }
    }
}