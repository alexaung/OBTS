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
