using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Zamba.Help.Models
{
	public class HelpContext : DbContext
	{
		public HelpContext(string connectionString) : base(connectionString)
		{

		}
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//Changing Database table name to Metadata
			//string schemaName = ConfigurationManager.AppSettings["DataBaseOwner"].ToString();
			//modelBuilder.Entity<HelpType>().ToTable("HelpType", schemaName);
			//modelBuilder.Entity<HelpApplication>().ToTable("HelpApplication", schemaName);
			//modelBuilder.Entity<HelpModule>().ToTable("HelpModule", schemaName);
			//modelBuilder.Entity<HelpFunction>().ToTable("HelpFunction", schemaName);
			//modelBuilder.Entity<HelpItem>().ToTable("HelpItem", schemaName);
			//modelBuilder.HasDefaultSchema(schemaName);
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<HelpType> HelpType { get; set; }
		public DbSet<HelpApplication> HelpApplication { get; set; }
		public DbSet<HelpModule> HelpModule { get; set; }
		public DbSet<HelpFunction> HelpFunction { get; set; }
		public DbSet<HelpItem> HelpItem { get; set; }
	}
}