namespace Pharmacy_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameMedicineField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicines", "NameMedicine", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medicines", "NameMedicine");
        }
    }
}
