namespace Pharmacy_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldUrlImg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "UrlImg", c => c.String());
            AddColumn("dbo.Medicines", "UrlImg", c => c.String());
            AddColumn("dbo.Employees", "UrlImg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "UrlImg");
            DropColumn("dbo.Medicines", "UrlImg");
            DropColumn("dbo.Customers", "UrlImg");
        }
    }
}
