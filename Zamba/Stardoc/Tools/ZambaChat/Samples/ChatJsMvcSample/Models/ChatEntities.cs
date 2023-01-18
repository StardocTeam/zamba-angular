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
        public ChatEntities(): base ("ChatContext")
        {
        }
      
        public DbSet<ChatUser> ChatUser { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<ChatPeople> ChatPeople { get; set; }
        public DbSet<ChatHistory> ChatHistory { get; set; }
    
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{           
        //    modelBuilder.HasDefaultSchema("ZAMBA");
        //   //modelBuilder.Conventions.Remove<ColumnTypeCasingConvention>();
        //    modelBuilder.Entity<ChatUser>().ToTable("CHATUSERS", schemaName: "ZAMBA");
        //    modelBuilder.Entity<Chat>().ToTable("CHATS", schemaName: "ZAMBA");
        //    modelBuilder.Entity<ChatPeople>().ToTable("CHATPEOPLES", schemaName: "ZAMBA");
        //    modelBuilder.Entity<ChatHistory>().ToTable("CHATHISTORIES", schemaName: "ZAMBA");
        //}
    }
}