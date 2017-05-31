namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplayAnotaionsToCustomerName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Customers", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "MyProperty", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String());
        }
    }
}
