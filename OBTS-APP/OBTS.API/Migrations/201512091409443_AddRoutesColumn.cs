namespace OBTS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoutesColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "Currency", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "Currency");
        }
    }
}
