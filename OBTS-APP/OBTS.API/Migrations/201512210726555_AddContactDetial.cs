namespace OBTS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactDetial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        ContactDetailId = c.Guid(nullable: false),
                        RouteId = c.Guid(nullable: false),
                        Email = c.String(),
                        Mobile = c.String(),
                    })
                .PrimaryKey(t => t.ContactDetailId);
            
            CreateTable(
                "dbo.Passengers",
                c => new
                    {
                        PassengerId = c.Guid(nullable: false),
                        ContactDetailId = c.Guid(nullable: false),
                        FullName = c.String(),
                        Age = c.Short(nullable: false),
                        Gender = c.Short(nullable: false),
                        IDType = c.Short(nullable: false),
                        IDNumber = c.String(),
                        isPrimaryContact = c.Boolean(nullable: false),
                        SeatNo = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.PassengerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Passengers");
            DropTable("dbo.ContactDetails");
        }
    }
}
