namespace Pharmacy_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPriceOfMedicine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicines", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medicines", "price");
        }
    }
}
