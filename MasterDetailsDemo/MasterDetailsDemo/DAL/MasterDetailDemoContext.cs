using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using MasterDetailsDemo.Models;

namespace MasterDetailsDemo.DAL
{
    public class MasterDetailDemoContext : DbContext
    {
        public MasterDetailDemoContext() : base("MasterDetailDemoContext") {
        }
        public DbSet<UnitMeasurement> UnitMeasurements { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleInvoiceHeader> SaleInvoiceHeaders { get; set; }
        public DbSet<SaleInvoiceItem> SaleInvoiceItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}