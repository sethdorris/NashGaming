namespace NashGaming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class realuserid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gamers", "RealUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Gamers", new[] { "RealUser_Id" });
            AddColumn("dbo.Gamers", "RealUserID", c => c.String());
            DropColumn("dbo.Gamers", "RealUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gamers", "RealUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Gamers", "RealUserID");
            CreateIndex("dbo.Gamers", "RealUser_Id");
            AddForeignKey("dbo.Gamers", "RealUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
