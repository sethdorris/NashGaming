namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class June2_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gamers", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gamers", "DisplayName");
        }
    }
}
