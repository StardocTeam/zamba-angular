using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ChatJsMvcSample.Models;
using System.Data.Metadata.Edm;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zamba.Collaboration.Models
{
    public class DBCollaboration : DbContext
    {
        public DBCollaboration(string connectionString) : base(connectionString)
        {
        }

        public DbSet<ChatUser> ChatUser { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<ChatPeople> ChatPeople { get; set; }
        public DbSet<ChatHistory> ChatHistory { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(18,0));
            modelBuilder.Properties<decimal>().Configure(config => config.HasPrecision(18, 0));

            modelBuilder.Ignore<ChatInvitation>();
            modelBuilder.Entity<ChatUser>()
                .HasKey(p => p.Id)
                .Property(p => p.Id).HasPrecision(18,0)
                .HasDatabaseGeneratedOption(Databas‌​eGeneratedOption.Identity);

   
            //.StoreGeneratedPattern = StoreGeneratedPattern.None;
            //  builder.Entity<BOB>().MapSingleType().ToTable("BOB");

            //modelBuilder.HasDefaultSchema("ZAMBA");
            //modelBuilder.Conventions.Remove<ColumnTypeCasingConvention>();
            //modelBuilder.Entity<ChatUser>().ToTable("CHATUSERS", schemaName: "ZAMBA");
            //modelBuilder.Entity<Chat>().ToTable("CHATS", schemaName: "ZAMBA");
            //modelBuilder.Entity<ChatPeople>().ToTable("CHATPEOPLES", schemaName: "ZAMBA");
            //modelBuilder.Entity<ChatHistory>().ToTable("CHATHISTORIES", schemaName: "ZAMBA");
        }
    }
}