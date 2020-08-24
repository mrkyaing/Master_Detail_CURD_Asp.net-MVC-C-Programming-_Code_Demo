using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MasterDetailsDemo.Models;

namespace MasterDetailsDemo.ViewModel
{
    public class OrderVM
    {
        public string Code { get; set; }
        public decimal? TotalAmount { get; set; }
        public string CustomerId { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
    }
}