namespace OOADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultConnection : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "PhotoUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "PhotoUrl", c => c.String());
        }
    }
}
