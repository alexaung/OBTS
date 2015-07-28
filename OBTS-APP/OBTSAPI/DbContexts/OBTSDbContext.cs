using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OBTSAPI.Models;
using System.Configuration;

namespace OBTSAPI.DbContexts
{
    public class OBTSDbContext: DbContext
    {
        //private static string OBTSConn = ;
        public OBTSDbContext()
        : base("test")
        {
        }

        public OBTSDbContext(string connString)
            : base(connString)
        {
        }
        
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public System.Data.Entity.DbSet<OBTSAPI.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<OBTSAPI.Models.Bus> Buses { get; set; }

        public System.Data.Entity.DbSet<OBTSAPI.Models.CodeTable> CodeTables { get; set; }

        public System.Data.Entity.DbSet<OBTSAPI.Models.Operator> Operators { get; set; }

        public System.Data.Entity.DbSet<OBTSAPI.Models.BusFeature> BusFeatures { get; set; }

        public System.Data.Entity.DbSet<OBTSAPI.Models.Agent> Agents { get; set; }

        public System.Data.Entity.DbSet<OBTSAPI.Models.OperatorAgent> OperatorAgents { get; set; }

        public System.Data.Entity.DbSet<OBTSAPI.Models.Route> Routes { get; set; }

        public System.Data.Entity.DbSet<OBTSAPI.Models.RouteDetail> RouteDetails { get; set; }

        public System.Data.Entity.DbSet<OBTSAPI.Models.RoutePoint> RoutePoints { get; set; }
        
    }

}