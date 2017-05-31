namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateMembershipTypesNewModel : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET Name = CASE WHEN DurationInMonths = 0 THEN 'No membership' WHEN DurationInMonths = 1 THEN 'One month membership' WHEN DurationInMonths = 3 THEN 'Three months membership' WHEN DurationInMonths = 12 THEN 'One year membership' END WHERE Name = ''");
        }
        
        public override void Down()
        {
        }
    }
}
