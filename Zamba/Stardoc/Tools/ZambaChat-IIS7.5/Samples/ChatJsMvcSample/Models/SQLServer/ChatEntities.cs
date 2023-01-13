using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
//using ChatJsMvcSample.Models;

namespace ChatJsMvcSample.Models
{
    public class ChatEntities : DbContext

    {
        public ChatEntities(string connectionString) : base(connectionString)
        {
        }

        public DbSet<ChatUser> ChatUser { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<ChatPeople> ChatPeople { get; set; }
        public DbSet<ChatHistory> ChatHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var schema = System.Web.Configuration.WebConfigurationManager.AppSettings["Schema"];
            schema = schema == string.Empty ? "dbo" : schema;
            modelBuilder.Ignore<ChatInvitation>();
            modelBuilder.HasDefaultSchema(schema);
            //modelBuilder.Conventions.Remove<ColumnTypeCasingConvention>();
            modelBuilder.Entity<ChatUser>().ToTable("CHATUSERS", schemaName: schema);
            modelBuilder.Entity<Chat>().ToTable("CHATS", schemaName: schema);
            modelBuilder.Entity<ChatPeople>().ToTable("CHATPEOPLES", schemaName: schema);
            modelBuilder.Entity<ChatHistory>().ToTable("CHATHISTORIES", schemaName: schema);
        }
    }
}