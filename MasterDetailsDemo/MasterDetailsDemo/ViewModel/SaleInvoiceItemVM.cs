using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MasterDetailsDemo.Models;

namespace MasterDetailsDemo.ViewModel
{
    public class SaleInvoiceItemVM
    {
        public string SaleInvoiceItemId { get; set; }
        public string SaleInvoiceHeaderId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        
        public int Index { get; set; }
    }
}