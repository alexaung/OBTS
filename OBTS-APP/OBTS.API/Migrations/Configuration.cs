namespace OBTS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OBTS.API.Infrastructure;
    using OBTS.API.Models;
    

    internal sealed class Configuration : DbMigrationsConfiguration<OBTS.API.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        

        protected override void Seed(OBTS.API.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                //Id = "6eda40fc-0b54-4958-b52a-7f48601395da",
                UserName = "OBTSAdmin",
                Email = "obts@gmail.com",
                EmailConfirmed = true,
                FirstName = "OBTS",
                LastName = "API",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "P@assw0rd");



            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Operatpr" });
                roleManager.Create(new IdentityRole { Name = "Agent" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("OBTSAdmin");

            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });

            /******* Code Table *******/
            var toyota = new CodeTable { CodeTableId = Guid.Parse("fd04b5df-c172-49cf-91f4-04b7edb3d1eb"), KeyCode = 1, Title = "Brand", Value = "Toyota", CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var honda = new CodeTable { CodeTableId = Guid.Parse("96d27e43-12af-4ca4-867c-69c99ad76395"), KeyCode = 2, Title = "Brand", Value = "Honda", CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var scania = new CodeTable { CodeTableId = Guid.Parse("05051711-5008-49db-aec7-73c048583908"), KeyCode = 3, Title = "Brand", Value = "Scania", CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var volvo = new CodeTable { CodeTableId = Guid.Parse("75e3b687-1291-428d-ace9-a350e1af113f"), KeyCode = 4, Title = "Brand", Value = "Volvo", CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var bmw = new CodeTable { CodeTableId = Guid.Parse("23CAA849-1996-4D14-B85B-1E90935B7163"), KeyCode = 5, Title = "Brand", Value = "BMW", CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            context.CodeTables.AddOrUpdate(c => c.CodeTableId, toyota, honda, scania, volvo, bmw);

            var vipBusType = new CodeTable { CodeTableId = Guid.Parse("F9A56039-E52F-4C02-90F7-FD8A1FEC20DA"), KeyCode = 1, Title = "BusType", Value = "VIP", CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var normalBusType = new CodeTable { CodeTableId = Guid.Parse("F6963AC3-1C3F-4E4C-9070-E3FFC8C80492"), KeyCode = 2, Title = "BusType", Value = "Normal", CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            context.CodeTables.AddOrUpdate(c => c.CodeTableId, vipBusType, normalBusType);

            var operatorUserType = new CodeTable { CodeTableId = Guid.Parse("5A125691-0AB5-4BF7-B873-C46A47A17C90"), KeyCode = 1, Title = "UserType", Value = "Operator", CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var agentUserType = new CodeTable { CodeTableId = Guid.Parse("DADA8FBE-5F8E-42B1-8EC6-9E80E2354769"), KeyCode = 2, Title = "UserType", Value = "Agent", CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            context.CodeTables.AddOrUpdate(c => c.CodeTableId, operatorUserType, agentUserType);


            /******* Country *******/
            var mm = new Country { CountryId = Guid.Parse("FCF16A65-991D-41F5-AA1B-21118422AD74"), CountryDesc = "Myanmar", CountryCode = "MM", CurrencyCode = "MMK", Currency = "Kyat", Rate = 1000, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var sg = new Country { CountryId = Guid.Parse("C91005AA-410A-423E-8877-6DFEC8742C1B"), CountryDesc = "Singapore", CountryCode = "SG", CurrencyCode = "SGD", Currency = "SGD", Rate = (decimal)1.4, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            context.Countries.AddOrUpdate(
              c => c.CountryId,
              mm, sg
            );

            /******* Region *******/
            var yangon = new Region { RegionId = Guid.Parse("295b7059-0b4a-4ad2-9b57-90d492295bfd"), RegionDesc = "Yangon", CountryId = mm.CountryId, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var mandalay = new Region { RegionId = Guid.Parse("7C45BAF8-A0DB-4B7E-8BB6-2456FF7C7A4D"), RegionDesc = "Mandalay", CountryId = mm.CountryId, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            context.Regions.AddOrUpdate(
                r => r.RegionId,
                yangon, mandalay
                );

            /******* City *******/
            var yangoncity = new City { CityId = Guid.Parse("14FA2223-6214-46EC-A690-7607328D15DF"), CityDesc = "Yangon", RegionId = yangon.RegionId, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var mandalaycity = new City { CityId = Guid.Parse("89A647F4-324F-495E-80BF-B75F8249CC62"), CityDesc = "Mandalay", RegionId = mandalay.RegionId, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            context.Cities.AddOrUpdate(
              c => c.CityId,
              yangoncity, mandalaycity
            );

            /******* Operator *******/
            var elit = new Operator { OperatorId = Guid.Parse("F9688FDF-1BEF-4E32-B70C-4494856BB94D"), Address = "3A Myanmar Gone Yaung Condominum, Than Thu Mar Road. Tarmwe, Yangon.", CityId = yangoncity.CityId, Company = "Elite", CompanyPhone = "96715923", Status = true, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            context.Operators.AddOrUpdate(
              o => o.OperatorId,
              elit
            );

            /******* Agent *******/
            var agent = new Agent { AgentId = Guid.Parse("C7100EB5-3CCD-4564-B6A3-342FAB6364D1"), Comapany = "Take me to myanmar", UserName = "AMO", AccountId = Guid.NewGuid(), BalanceCredit = 10000, Name = "Alex Aung", Email = "alex@gmail.com", PanNumber = "11111", Address = "No1, Bo Aung Kyaw Road", CityId = yangoncity.CityId, PinCode = "11222", Mobile = "12345678", OfficePhone = "123456", Fax = "123456", UserName2 = "alex", Password = "", Logo = "", Status = true, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            context.Agents.AddOrUpdate(
              o => o.AgentId,
              agent
            );

            /******* Bank *******/
            var cb = new Bank { BankId = Guid.Parse("F631127E-D094-4D71-A06F-E13069502087"), UserId = elit.OperatorId, UserTypeCode = operatorUserType.KeyCode, BankName = "Co-Operative Bank", Branch = "Bo Ta Htaung", AccountNumber = "1234567890", LFSCCode = "1111", Logo = "", CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var kbz = new Bank { BankId = Guid.Parse("1C560934-5157-402D-ACB3-7A9F609B1C5B"), UserId = agent.AgentId, UserTypeCode = agentUserType.KeyCode, BankName = "Kambawza Bank", Branch = "Bo Ta Htaung", AccountNumber = "1234567890", LFSCCode = "2222", Logo = "", CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            context.Banks.AddOrUpdate(
              b => b.BankId,
              cb, kbz
            );

            /******* Bus *******/
            var bus = new Bus { BusId = Guid.Parse("73A2436E-EE50-4254-B649-6EAA4E56CD3F"), Company = "Mya mar lar", Brand = toyota.KeyCode, BusType = normalBusType.KeyCode, RegistrationNo = "11111111", PermitNumber = "1234567890", PermitRenewDate = DateTime.Parse("2015/01/01"), InsurancePolicyNumber = "123445", InsuranceCompany = "Pru", InsuranceValidFrom = DateTime.Parse("2015/01/01"), InsuranceValidTo = DateTime.Parse("2015/12/30"), VechiclePhoneNo = "111111", DriverName = "Mg Ba", Description = "Weekly yangon to mandalay", Status = true, OperatorId = elit.OperatorId, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var bus2 = new Bus { BusId = Guid.Parse("F5ECB397-88CB-4EA2-90F5-E7A3D9D9A229"), Company = "Mya mar lar2", Brand = toyota.KeyCode, BusType = normalBusType.KeyCode, RegistrationNo = "2222222", PermitNumber = "1234567890", PermitRenewDate = DateTime.Parse("2015/01/01"), InsurancePolicyNumber = "123445", InsuranceCompany = "Pru", InsuranceValidFrom = DateTime.Parse("2015/01/01"), InsuranceValidTo = DateTime.Parse("2015/12/30"), VechiclePhoneNo = "111111", DriverName = "Mg Ba2", Description = "Weekly yangon to mandalay", Status = true, OperatorId = elit.OperatorId, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };

            context.Buses.AddOrUpdate(
              b => b.BusId,
              bus, bus2
            );

            /******* Seat *******/
            var seat1 = new Seat { SeatId = Guid.Parse("EFB1F89B-4960-490F-8949-105D6BE2E0B4"), BusId = bus.BusId, SeatNo = "1", Bookable = true, Space = false, SpecialSeat = true, Status = true, UpperLower = 1, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var seat2 = new Seat { SeatId = Guid.Parse("58D66967-2F6D-415B-B58C-1F62504C3137"), BusId = bus.BusId, SeatNo = "2", Bookable = false, Space = true, SpecialSeat = false, Status = true, UpperLower = 2, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            context.Seats.AddOrUpdate(
              b => b.SeatId,
              seat1, seat2
            );

            //route
            var route = new Route { RouteId = Guid.Parse("B03A4F96-E0A6-4B31-B2BC-D62F1E28528C"), BusId = bus.BusId, Source_CityId = yangoncity.CityId, Destination_CityId = mandalaycity.CityId, Recurrsive = true, RouteDate = DateTime.Parse("2015/01/01"), DepartureTime = TimeSpan.Parse("9:00"), ArrivalTime = TimeSpan.Parse("23:00"), RouteFare = 10000, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };
            var route2 = new Route { RouteId = Guid.Parse("CCC8C693-D7C1-4192-A892-37B28824B307"), BusId = bus2.BusId, Source_CityId = yangoncity.CityId, Destination_CityId = mandalaycity.CityId, Recurrsive = true, RouteDate = DateTime.Parse("2015/01/01"), DepartureTime = TimeSpan.Parse("9:00"), ArrivalTime = TimeSpan.Parse("23:00"), RouteFare = 10000, CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };

            context.Routes.AddOrUpdate(
              b => b.RouteId,
              route, route2
            );

            //booking
            var booking = new Booking { BookingId = Guid.Parse("3BD88E86-ACF3-432F-A016-53D9EF52D11D"),BookingRefId="ABC",UserId= user.Id.ToString(), BookingOn = DateTime.Parse("2015/01/01"), MainContact = "Mg Ba", Email = "Mgba@gmail.com", ContactNo = "123123",Cupon = "", Discount = 0,CreatedBy = Guid.Parse(adminUser.Id), CreatedUtc = DateTime.UtcNow };

            context.Bookings.AddOrUpdate(
              b => b.BookingId,
              booking
            );

            context.SaveChanges();

        }
    }
}
