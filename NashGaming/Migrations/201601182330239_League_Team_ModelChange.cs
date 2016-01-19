namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class League_Team_ModelChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "Wins", c => c.Int(nullable: false));
            AddColumn("dbo.Teams", "Losses", c => c.Int(nullable: false));
            AlterColumn("dbo.Leagues", "Platform", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leagues", "Platform", c => c.String());
            DropColumn("dbo.Teams", "Losses");
            DropColumn("dbo.Teams", "Wins");
        }
    }
}
