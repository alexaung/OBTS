namespace OBTS.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using OBTS.API.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<OBTS.API.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OBTS.API.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            var toyota = new CodeTable { CodeTableId = Guid.Parse("fd04b5df-c172-49cf-91f4-04b7edb3d1eb"), KeyCode = 1, Title = "Brand", Value = "Toyota" };
            var honda = new CodeTable { CodeTableId = Guid.Parse("96d27e43-12af-4ca4-867c-69c99ad76395"), KeyCode = 2, Title = "Brand", Value = "Honda" };
            var scania = new CodeTable { CodeTableId = Guid.Parse("05051711-5008-49db-aec7-73c048583908"), KeyCode = 3, Title = "Brand", Value = "Scania" };
            var volvo = new CodeTable { CodeTableId = Guid.Parse("75e3b687-1291-428d-ace9-a350e1af113f"), KeyCode = 4, Title = "Brand", Value = "Volvo" };
            var bmw = new CodeTable { CodeTableId = Guid.Parse("23CAA849-1996-4D14-B85B-1E90935B7163"), KeyCode = 5, Title = "Brand", Value = "BMW" };
            context.CodeTables.AddOrUpdate(c => c.CodeTableId, toyota, honda, scania, volvo, bmw);

            var mm = new Country { CountryId=Guid.Parse("FCF16A65-991D-41F5-AA1B-21118422AD74"),CountryDesc="Myanmar",CountryCode="MM",CurrencyCode="MMK",Currency="Kyat",Rate=1000 };
            var sg =  new Country { CountryId = Guid.Parse("C91005AA-410A-423E-8877-6DFEC8742C1B"), CountryDesc = "Singapore", CountryCode = "SG", CurrencyCode = "SGD", Currency = "SGD", Rate = (decimal)1.4};
            context.Countries.AddOrUpdate(
              c => c.CountryId,
              mm,sg
            );

            var yangon=new Region { RegionId=Guid.NewGuid(),RegionDesc="Yangon",CountryId=mm.CountryId};
            context.Regions.AddOrUpdate(
                r => r.RegionId,
                yangon
                );
        }
    }
}
