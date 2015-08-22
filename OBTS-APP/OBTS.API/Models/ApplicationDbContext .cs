using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using OBTS.API.Infrastructure;

namespace OBTS.API.Models
{
    public class ApplicationDbContext  : IdentityDbContext<ApplicationUser>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ApplicationDbContext () : base("name=ApplicationDbContext ")
        {
        }

        public static ApplicationDbContext  Create()
        {
            return new ApplicationDbContext ();
        }

        public System.Data.Entity.DbSet<OBTS.API.Models.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<OBTS.API.Models.RefreshToken> RefreshTokens { get; set; }

        public System.Data.Entity.DbSet<OBTS.API.Models.Agent> Agents { get; set; }

        public System.Data.Entity.DbSet<OBTS.API.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<OBTS.API.Models.Region> Regions { get; set; }

        public System.Data.Entity.DbSet<OBTS.API.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<OBTS.API.Models.Operator> Operators { get; set; }

        public System.Data.Entity.DbSet<OBTS.API.Models.CodeTable> CodeTables { get; set; }

        public System.Data.Entity.DbSet<OBTS.API.Models.Bus> Buses { get; set; }

        public System.Data.Entity.DbSet<OBTS.API.Models.BusFeature> BusFeatures { get; set; }
        
        public System.Data.Entity.DbSet<OBTS.API.Models.OperatorAgent> OperatorAgents { get; set; }
        
        public System.Data.Entity.DbSet<OBTS.API.Models.Route> Routes { get; set; }
        
        public System.Data.Entity.DbSet<OBTS.API.Models.RouteDetail> RouteDetails { get; set; }
        
        public System.Data.Entity.DbSet<OBTS.API.Models.RoutePoint> RoutePoints { get; set; }

        public System.Data.Entity.DbSet<OBTS.API.Models.Seat> Seats { get; set; }

        public System.Data.Entity.DbSet<OBTS.API.Models.Bank> Banks { get; set; }
    
    }
}
