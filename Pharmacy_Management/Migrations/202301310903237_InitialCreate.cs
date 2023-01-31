namespace Pharmacy_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        IdCustomer = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 30),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Pwd = c.String(nullable: false, maxLength: 30),
                        CodFisc = c.String(nullable: false, maxLength: 16),
                        IdRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCustomer)
                .ForeignKey("dbo.Roles", t => t.IdRole)
                .Index(t => t.IdRole);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        IdOrder = c.Int(nullable: false, identity: true),
                        IdCustomer = c.Int(nullable: false),
                        IdMedicine = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        IdPrescription = c.Int(),
                        DateOrder = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.IdOrder)
                .ForeignKey("dbo.Medicines", t => t.IdMedicine)
                .ForeignKey("dbo.Pescritions", t => t.IdPrescription)
                .ForeignKey("dbo.Customers", t => t.IdCustomer)
                .Index(t => t.IdCustomer)
                .Index(t => t.IdMedicine)
                .Index(t => t.IdPrescription);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        IdMedicine = c.Int(nullable: false, identity: true),
                        IdTypeProduct = c.Int(nullable: false),
                        IdTypeMedicine = c.Int(),
                        DescriptionUse = c.String(nullable: false),
                        IdSupplierCompanies = c.Int(nullable: false),
                        IdDrawer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdMedicine)
                .ForeignKey("dbo.Drawers", t => t.IdDrawer)
                .ForeignKey("dbo.SupplierCompanies", t => t.IdSupplierCompanies)
                .ForeignKey("dbo.TypeMedicine", t => t.IdTypeMedicine)
                .ForeignKey("dbo.TypeProduct", t => t.IdTypeProduct)
                .Index(t => t.IdTypeProduct)
                .Index(t => t.IdTypeMedicine)
                .Index(t => t.IdSupplierCompanies)
                .Index(t => t.IdDrawer);
            
            CreateTable(
                "dbo.Drawers",
                c => new
                    {
                        IdDrawer = c.Int(nullable: false, identity: true),
                        Identifier = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDrawer);
            
            CreateTable(
                "dbo.SupplierCompanies",
                c => new
                    {
                        IdSupplierCompanies = c.Int(nullable: false, identity: true),
                        NameCompany = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 30),
                        PhoneNumber = c.Int(nullable: false),
                        Mail = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IdSupplierCompanies);
            
            CreateTable(
                "dbo.TypeMedicine",
                c => new
                    {
                        IdTypeMedicine = c.Int(nullable: false, identity: true),
                        DescTypeMedicine = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IdTypeMedicine);
            
            CreateTable(
                "dbo.TypeProduct",
                c => new
                    {
                        IdTypeProduct = c.Int(nullable: false, identity: true),
                        DescTypeProduct = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IdTypeProduct);
            
            CreateTable(
                "dbo.Pescritions",
                c => new
                    {
                        IdPrescription = c.Int(nullable: false, identity: true),
                        IdentifierPrescription = c.Int(nullable: false),
                        IdCustomer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPrescription)
                .ForeignKey("dbo.Customers", t => t.IdCustomer)
                .Index(t => t.IdCustomer);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        IdRole = c.Int(nullable: false, identity: true),
                        TypeRole = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.IdRole);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        IdEmployee = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 30),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Pwd = c.String(nullable: false, maxLength: 30),
                        IdRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdEmployee)
                .ForeignKey("dbo.Roles", t => t.IdRole)
                .Index(t => t.IdRole);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "IdRole", "dbo.Roles");
            DropForeignKey("dbo.Customers", "IdRole", "dbo.Roles");
            DropForeignKey("dbo.Pescritions", "IdCustomer", "dbo.Customers");
            DropForeignKey("dbo.Orders", "IdCustomer", "dbo.Customers");
            DropForeignKey("dbo.Orders", "IdPrescription", "dbo.Pescritions");
            DropForeignKey("dbo.Medicines", "IdTypeProduct", "dbo.TypeProduct");
            DropForeignKey("dbo.Medicines", "IdTypeMedicine", "dbo.TypeMedicine");
            DropForeignKey("dbo.Medicines", "IdSupplierCompanies", "dbo.SupplierCompanies");
            DropForeignKey("dbo.Orders", "IdMedicine", "dbo.Medicines");
            DropForeignKey("dbo.Medicines", "IdDrawer", "dbo.Drawers");
            DropIndex("dbo.Employees", new[] { "IdRole" });
            DropIndex("dbo.Pescritions", new[] { "IdCustomer" });
            DropIndex("dbo.Medicines", new[] { "IdDrawer" });
            DropIndex("dbo.Medicines", new[] { "IdSupplierCompanies" });
            DropIndex("dbo.Medicines", new[] { "IdTypeMedicine" });
            DropIndex("dbo.Medicines", new[] { "IdTypeProduct" });
            DropIndex("dbo.Orders", new[] { "IdPrescription" });
            DropIndex("dbo.Orders", new[] { "IdMedicine" });
            DropIndex("dbo.Orders", new[] { "IdCustomer" });
            DropIndex("dbo.Customers", new[] { "IdRole" });
            DropTable("dbo.Employees");
            DropTable("dbo.Roles");
            DropTable("dbo.Pescritions");
            DropTable("dbo.TypeProduct");
            DropTable("dbo.TypeMedicine");
            DropTable("dbo.SupplierCompanies");
            DropTable("dbo.Drawers");
            DropTable("dbo.Medicines");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
