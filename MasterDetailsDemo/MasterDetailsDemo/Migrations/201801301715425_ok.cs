namespace MasterDetailsDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ok : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.String(nullable: false, maxLength: 36),
                        Name = c.String(maxLength: 50),
                        Address = c.String(maxLength: 200),
                        PhoneNo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 36),
                        Name = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitMeasurementId = c.String(maxLength: 36),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.UnitMeasurement", t => t.UnitMeasurementId)
                .Index(t => t.UnitMeasurementId);
            
            CreateTable(
                "dbo.UnitMeasurement",
                c => new
                    {
                        UnitMeasurementId = c.String(nullable: false, maxLength: 36),
                        Code = c.String(maxLength: 50),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.UnitMeasurementId);
            
            CreateTable(
                "dbo.SaleInvoiceHeader",
                c => new
                    {
                        SaleInvoiceHeaderId = c.String(nullable: false, maxLength: 36),
                        Code = c.String(nullable: false, maxLength: 50),
                        TotalAmount = c.Decimal(precision: 18, scale: 2),
                        CustomerId = c.String(nullable: false, maxLength: 36),
                    })
                .PrimaryKey(t => t.SaleInvoiceHeaderId)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.SaleInvoiceItem",
                c => new
                    {
                        SaleInvoiceItemId = c.String(nullable: false, maxLength: 36),
                        SaleInvoiceHeaderId = c.String(nullable: false, maxLength: 36),
                        ProductId = c.String(nullable: false, maxLength: 36),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SaleInvoiceItemId)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.SaleInvoiceHeader", t => t.SaleInvoiceHeaderId, cascadeDelete: true)
                .Index(t => t.SaleInvoiceHeaderId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleInvoiceItem", "SaleInvoiceHeaderId", "dbo.SaleInvoiceHeader");
            DropForeignKey("dbo.SaleInvoiceItem", "ProductId", "dbo.Product");
            DropForeignKey("dbo.SaleInvoiceHeader", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Product", "UnitMeasurementId", "dbo.UnitMeasurement");
            DropIndex("dbo.SaleInvoiceItem", new[] { "ProductId" });
            DropIndex("dbo.SaleInvoiceItem", new[] { "SaleInvoiceHeaderId" });
            DropIndex("dbo.SaleInvoiceHeader", new[] { "CustomerId" });
            DropIndex("dbo.Product", new[] { "UnitMeasurementId" });
            DropTable("dbo.SaleInvoiceItem");
            DropTable("dbo.SaleInvoiceHeader");
            DropTable("dbo.UnitMeasurement");
            DropTable("dbo.Product");
            DropTable("dbo.Customer");
        }
    }
}
