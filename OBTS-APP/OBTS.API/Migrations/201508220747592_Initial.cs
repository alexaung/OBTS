namespace OBTS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        AgentId = c.Guid(nullable: false),
                        Comapany = c.String(),
                        UserName = c.String(),
                        AccountId = c.Guid(nullable: false),
                        BalanceCredit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                        Email = c.String(),
                        PanNumber = c.String(),
                        Address = c.String(),
                        CityId = c.Guid(nullable: false),
                        PinCode = c.String(),
                        Mobile = c.String(),
                        OfficePhone = c.String(),
                        Fax = c.String(),
                        UserName2 = c.String(),
                        Password = c.String(),
                        Logo = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AgentId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Guid(nullable: false),
                        CityDesc = c.String(nullable: false, maxLength: 50),
                        RegionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionId = c.Guid(nullable: false),
                        RegionDesc = c.String(nullable: false, maxLength: 50),
                        CountryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.RegionId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Guid(nullable: false),
                        CountryDesc = c.String(nullable: false, maxLength: 50),
                        CountryCode = c.String(nullable: false, maxLength: 10),
                        Currency = c.String(maxLength: 20),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrencyCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        BankId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        UserTypeCode = c.Short(nullable: false),
                        BankName = c.String(),
                        Branch = c.String(),
                        AccountNumber = c.String(),
                        LFSCCode = c.String(),
                        Logo = c.String(),
                    })
                .PrimaryKey(t => t.BankId);
            
            CreateTable(
                "dbo.Buses",
                c => new
                    {
                        BusId = c.Guid(nullable: false),
                        Company = c.String(),
                        Brand = c.Short(nullable: false),
                        BusType = c.Short(nullable: false),
                        RegistrationNo = c.String(),
                        PermitNumber = c.String(),
                        PermitRenewDate = c.DateTime(nullable: false),
                        InsurancePolicyNumber = c.String(),
                        InsuranceCompany = c.String(),
                        InsuranceValidFrom = c.DateTime(nullable: false),
                        InsuranceValidTo = c.DateTime(nullable: false),
                        VechiclePhoneNo = c.String(),
                        DriverName = c.String(),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        OperatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.BusId);
            
            CreateTable(
                "dbo.BusFeatures",
                c => new
                    {
                        BusFeatureId = c.Guid(nullable: false),
                        BusId = c.Guid(nullable: false),
                        BusFeatureCode = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.BusFeatureId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ApplicationType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CodeTables",
                c => new
                    {
                        CodeTableId = c.Guid(nullable: false),
                        Title = c.String(),
                        KeyCode = c.Short(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.CodeTableId);
            
            CreateTable(
                "dbo.OperatorAgents",
                c => new
                    {
                        OperatorAgentId = c.Guid(nullable: false),
                        OperatorId = c.Guid(nullable: false),
                        AgentId = c.Guid(nullable: false),
                        DepositAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        JointDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OperatorAgentId)
                .ForeignKey("dbo.Agents", t => t.AgentId, cascadeDelete: true)
                .ForeignKey("dbo.Operators", t => t.OperatorId, cascadeDelete: true)
                .Index(t => t.OperatorId)
                .Index(t => t.AgentId);
            
            CreateTable(
                "dbo.Operators",
                c => new
                    {
                        OperatorId = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Mobile = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                        Company = c.String(),
                        CompanyPhone = c.String(),
                        Address = c.String(),
                        CityId = c.Guid(nullable: false),
                        NumberOfBuses = c.Int(nullable: false),
                        NumberOfRoutes = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.OperatorId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: false)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 50),
                        ClientId = c.String(nullable: false, maxLength: 50),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RouteDetails",
                c => new
                    {
                        RouteDetailId = c.Guid(nullable: false),
                        RouteId = c.Guid(nullable: false),
                        AmenitiesCodeId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.RouteDetailId);
            
            CreateTable(
                "dbo.RoutePoints",
                c => new
                    {
                        RoutePointId = c.Guid(nullable: false),
                        RouteId = c.Guid(nullable: false),
                        BoardingPoint = c.String(),
                        DroppingPoint = c.String(),
                        BoardingTime = c.Time(nullable: false, precision: 7),
                        DroppingTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.RoutePointId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        RouteId = c.Guid(nullable: false),
                        BusId = c.Guid(nullable: false),
                        Source = c.Guid(nullable: false),
                        Destination = c.Guid(nullable: false),
                        Recurrsive = c.Boolean(nullable: false),
                        RouteDate = c.DateTime(nullable: false),
                        DepartureTime = c.Time(nullable: false, precision: 7),
                        ArrivalTime = c.Time(nullable: false, precision: 7),
                        RouteFare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _cityDestination_CityId = c.Guid(),
                        _citySource_CityId = c.Guid(),
                    })
                .PrimaryKey(t => t.RouteId)
                .ForeignKey("dbo.Buses", t => t.BusId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t._cityDestination_CityId)
                .ForeignKey("dbo.Cities", t => t._citySource_CityId)
                .Index(t => t.BusId)
                .Index(t => t._cityDestination_CityId)
                .Index(t => t._citySource_CityId);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatId = c.Guid(nullable: false),
                        BusId = c.Guid(nullable: false),
                        SeatNo = c.String(),
                        Bookable = c.Boolean(nullable: false),
                        Space = c.Boolean(nullable: false),
                        SpecialSeat = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UpperLower = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.SeatId)
                .ForeignKey("dbo.Buses", t => t.BusId, cascadeDelete: true)
                .Index(t => t.BusId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Level = c.Byte(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Seats", "BusId", "dbo.Buses");
            DropForeignKey("dbo.Routes", "_citySource_CityId", "dbo.Cities");
            DropForeignKey("dbo.Routes", "_cityDestination_CityId", "dbo.Cities");
            DropForeignKey("dbo.Routes", "BusId", "dbo.Buses");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OperatorAgents", "OperatorId", "dbo.Operators");
            DropForeignKey("dbo.Operators", "CityId", "dbo.Cities");
            DropForeignKey("dbo.OperatorAgents", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.Agents", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Regions", "CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Seats", new[] { "BusId" });
            DropIndex("dbo.Routes", new[] { "_citySource_CityId" });
            DropIndex("dbo.Routes", new[] { "_cityDestination_CityId" });
            DropIndex("dbo.Routes", new[] { "BusId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Operators", new[] { "CityId" });
            DropIndex("dbo.OperatorAgents", new[] { "AgentId" });
            DropIndex("dbo.OperatorAgents", new[] { "OperatorId" });
            DropIndex("dbo.Regions", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "RegionId" });
            DropIndex("dbo.Agents", new[] { "CityId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Seats");
            DropTable("dbo.Routes");
            DropTable("dbo.RoutePoints");
            DropTable("dbo.RouteDetails");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.Operators");
            DropTable("dbo.OperatorAgents");
            DropTable("dbo.CodeTables");
            DropTable("dbo.Clients");
            DropTable("dbo.BusFeatures");
            DropTable("dbo.Buses");
            DropTable("dbo.Banks");
            DropTable("dbo.Countries");
            DropTable("dbo.Regions");
            DropTable("dbo.Cities");
            DropTable("dbo.Agents");
        }
    }
}
