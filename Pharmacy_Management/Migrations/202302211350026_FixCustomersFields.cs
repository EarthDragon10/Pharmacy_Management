namespace Pharmacy_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCustomersFields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "Username");
            DropColumn("dbo.Customers", "Pwd");
            DropColumn("dbo.Customers", "UrlImg");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "UrlImg", c => c.String());
            AddColumn("dbo.Customers", "Pwd", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Customers", "Username", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
