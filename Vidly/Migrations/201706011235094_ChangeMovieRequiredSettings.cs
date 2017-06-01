namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeMovieRequiredSettings : DbMigration
    {
        public override void Up()
        {
            //to se ne odnosi na ef, odnosilo bi se ako bi stavio DateTime?, ane zato sto sam maknuo [Required]
        }
        
        public override void Down()
        {
        }
    }
}
