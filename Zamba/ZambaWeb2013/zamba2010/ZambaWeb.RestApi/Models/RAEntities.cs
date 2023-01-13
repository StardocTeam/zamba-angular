using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Zamba.Framework;

namespace ZambaWeb.RestApi.Models
{
    public class RAEntities : DbContext

    {
        public RAEntities() : base("RAEntities")
        {
        }

        public DbSet<Zss> Zss { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var schema = System.Web.Configuration.WebConfigurationManager.AppSettings["Schema"];
            schema = schema == string.Empty ? "dbo" : schema;
            modelBuilder.HasDefaultSchema(schema);
            //modelBuilder.Conventions.Remove<ColumnTypeCasingConvention>();
   
        }
    
    }
}