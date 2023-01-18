using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Zamba.WebAdmin.Models.News;

namespace Zamba.WebAdmin.Models
{
    public class ContextDB : DbContext
    {

        public ContextDB(string connectionstring) : base(connectionstring)
        {
        }
        public DbSet<ZInformation> ZInformation { get; set; }
        public DbSet<ZInformationUser> ZInformationUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<Zamba.WebAdmin.Models.USRNOTES> USRNOTES { get; set; }

        public System.Data.Entity.DbSet<Zamba.WebAdmin.Models.USERPARAM> USERPARAMs { get; set; }

        public System.Data.Entity.DbSet<Zamba.WebAdmin.Models.USERADMIN> USERADMINs { get; set; }
    }
}