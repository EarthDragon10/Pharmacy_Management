namespace Pharmacy_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBoolControlPescrition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeMedicine", "RequiredPescrition", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeMedicine", "RequiredPescrition");
        }
    }
}
