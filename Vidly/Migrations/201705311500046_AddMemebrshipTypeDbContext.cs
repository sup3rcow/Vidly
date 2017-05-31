namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddMemebrshipTypeDbContext : DbMigration
    {
        public override void Up()
        {
            //ovo nemanista, jer tablica odavno postoji u bazi, zbog foreign key-a u customer tablici
        }
        
        public override void Down()
        {
        }
    }
}
