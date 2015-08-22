namespace OBTS.API.Migrations
{
    using OBTS.API.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OBTS.API.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OBTS.API.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.


            /******* Code Table *******/
            var toyota = new CodeTable { CodeTableId = Guid.Parse("fd04b5df-c172-49cf-91f4-04b7edb3d1eb"), KeyCode = 1, Title = "Brand", Value = "Toyota" };
            var honda = new CodeTable { CodeTableId = Guid.Parse("96d27e43-12af-4ca4-867c-69c99ad76395"), KeyCode = 2, Title = "Brand", Value = "Honda" };
            var scania = new CodeTable { CodeTableId = Guid.Parse("05051711-5008-49db-aec7-73c048583908"), KeyCode = 3, Title = "Brand", Value = "Scania" };
            var volvo = new CodeTable { CodeTableId = Guid.Parse("75e3b687-1291-428d-ace9-a350e1af113f"), KeyCode = 4, Title = "Brand", Value = "Volvo" };
            var bmw = new CodeTable { CodeTableId = Guid.Parse("23CAA849-1996-4D14-B85B-1E90935B7163"), KeyCode = 5, Title = "Brand", Value = "BMW" };
            context.CodeTables.AddOrUpdate(c => c.CodeTableId, toyota, honda, scania, volvo, bmw);

            var vipBusType = new CodeTable { CodeTableId = Guid.Parse("F9A56039-E52F-4C02-90F7-FD8A1FEC20DA"), KeyCode = 1, Title = "BusType", Value = "VIP" };
            var normalBusType = new CodeTable { CodeTableId = Guid.Parse("F6963AC3-1C3F-4E4C-9070-E3FFC8C80492"), KeyCode = 2, Title = "BusType", Value = "Normal" };
            context.CodeTables.AddOrUpdate(c => c.CodeTableId, vipBusType, normalBusType);

            var operatorUserType = new CodeTable { CodeTableId = Guid.Parse("5A125691-0AB5-4BF7-B873-C46A47A17C90"), KeyCode = 1, Title = "UserType", Value = "Operator" };
            var agentUserType = new CodeTable { CodeTableId = Guid.Parse("DADA8FBE-5F8E-42B1-8EC6-9E80E2354769"), KeyCode = 2, Title = "UserType", Value = "Agent" };
            context.CodeTables.AddOrUpdate(c => c.CodeTableId, operatorUserType, agentUserType);


            /******* Country *******/
            var mm = new Country { CountryId = Guid.Parse("FCF16A65-991D-41F5-AA1B-21118422AD74"), CountryDesc = "Myanmar", CountryCode = "MM", CurrencyCode = "MMK", Currency = "Kyat", Rate = 1000 };
            var sg = new Country { CountryId = Guid.Parse("C91005AA-410A-423E-8877-6DFEC8742C1B"), CountryDesc = "Singapore", CountryCode = "SG", CurrencyCode = "SGD", Currency = "SGD", Rate = (decimal)1.4 };
            context.Countries.AddOrUpdate(
              c => c.CountryId,
              mm, sg
            );

            /******* Region *******/
            var yangon = new Region { RegionId = Guid.NewGuid(), RegionDesc = "Yangon", CountryId = mm.CountryId };
            context.Regions.AddOrUpdate(
                r => r.RegionId,
                yangon
                );

            /******* City *******/
            var yangoncity = new City { CityId = Guid.Parse("14FA2223-6214-46EC-A690-7607328D15DF"), CityDesc = "Yangon", RegionId = yangon.RegionId };
            context.Cities.AddOrUpdate(
              c => c.CityId,
              yangoncity
            );

            /******* Operator *******/
            var elit = new Operator { OperatorId = Guid.Parse("F9688FDF-1BEF-4E32-B70C-4494856BB94D"), Address = "3A Myanmar Gone Yaung Condominum, Than Thu Mar Road. Tarmwe, Yangon.", CityId = yangoncity.CityId, Company = "Elite", CompanyPhone = "96715923", Status = true };
            context.Operators.AddOrUpdate(
              o => o.OperatorId,
              elit
            );

            /******* Agent *******/
            var agent = new Agent { AgentId = Guid.Parse("C7100EB5-3CCD-4564-B6A3-342FAB6364D1"), Comapany = "Take me to myanmar", UserName = "AMO", AccountId = Guid.NewGuid(), BalanceCredit = 10000, Name = "Alex Aung", Email = "alex@gmail.com", PanNumber = "11111", Address = "No1, Bo Aung Kyaw Road", CityId = yangoncity.CityId,PinCode="11222",Mobile="12345678",OfficePhone="123456",Fax="123456",UserName2="alex",Password="",Logo="", Status = true };
            context.Agents.AddOrUpdate(
              o => o.AgentId,
              agent
            );

            /******* Bank *******/
            var cb = new Bank { BankId = Guid.Parse("F631127E-D094-4D71-A06F-E13069502087"), UserId = elit.OperatorId, UserTypeCode = operatorUserType.KeyCode, BankName = "Co-Operative Bank", Branch = "Bo Ta Htaung", AccountNumber = "1234567890", LFSCCode = "1111", Logo="" };
            var kbz = new Bank { BankId = Guid.Parse("1C560934-5157-402D-ACB3-7A9F609B1C5B"), UserId = agent.AgentId, UserTypeCode = agentUserType.KeyCode, BankName = "Kambawza Bank", Branch = "Bo Ta Htaung", AccountNumber = "1234567890", LFSCCode = "2222", Logo =""};
            context.Banks.AddOrUpdate(
              b => b.BankId,
              cb, kbz
            );

            /******* Bus *******/
            var bus = new Bus { BusId = Guid.Parse("73A2436E-EE50-4254-B649-6EAA4E56CD3F"), Company = "Mya mar lar", Brand = toyota.KeyCode, BusType = normalBusType.KeyCode, RegistrationNo = "11111111", PermitNumber = "1234567890", PermitRenewDate =DateTime.Parse("2015/01/01"), InsurancePolicyNumber = "123445", InsuranceCompany = "Pru", InsuranceValidFrom =DateTime.Parse("2015/01/01"), InsuranceValidTo =DateTime.Parse("2015-12-30"), VechiclePhoneNo = "111111", DriverName = "Mg Ba", Description = "Weekly yangon to mandalay", Status = true, OperatorId=elit.OperatorId };
            context.Buses.AddOrUpdate(
              b => b.BusId,
              bus
            );

            /******* Seat *******/
            var seat1 = new Seat { SeatId = Guid.Parse("EFB1F89B-4960-490F-8949-105D6BE2E0B4"), BusId = bus.BusId, SeatNo = "1", Bookable = true, Space = false, SpecialSeat = true, Status = true, UpperLower =1};
            var seat2 = new Seat { SeatId = Guid.Parse("58D66967-2F6D-415B-B58C-1F62504C3137"), BusId = bus.BusId, SeatNo = "2", Bookable = false, Space = true, SpecialSeat = false, Status = true, UpperLower = 2 };
            context.Seats.AddOrUpdate(
              b => b.SeatId,
              seat1, seat2
            );
        }
    }
}
