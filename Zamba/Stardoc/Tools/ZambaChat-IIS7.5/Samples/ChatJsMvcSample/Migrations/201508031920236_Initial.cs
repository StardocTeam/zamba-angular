//HABILITAR EN EF 6 ABOVE

//namespace ChatJsMvcSample.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;
    
//    public partial class Initial : DbMigration
//    {
//        public override void Up()
//        {
//            CreateTable(
//                "ZAMBA.CHATS",
//                c => new
//                    {
//                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
//                        ADMINID = c.Decimal(nullable: false, precision: 10, scale: 0),
//                    })
//                .PrimaryKey(t => t.ID);
            
//            CreateTable(
//                "ZAMBA.CHATHISTORIES",
//                c => new
//                    {
//                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
//                        CHATID = c.Decimal(nullable: false, precision: 10, scale: 0),
//                        USERID = c.Decimal(nullable: false, precision: 10, scale: 0),
//                        MESSAGE = c.String(),
//                        DATE = c.DateTime(nullable: false),
//                    })
//                .PrimaryKey(t => t.ID)
//                .ForeignKey("ZAMBA.CHATS", t => t.CHATID, cascadeDelete: true)
//                .Index(t => t.CHATID);
            
//            CreateTable(
//                "ZAMBA.CHATPEOPLES",
//                c => new
//                    {
//                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
//                        CHATID = c.Decimal(nullable: false, precision: 10, scale: 0),
//                        USERID = c.Decimal(nullable: false, precision: 10, scale: 0),
//                    })
//                .PrimaryKey(t => t.ID)
//                .ForeignKey("ZAMBA.CHATS", t => t.CHATID, cascadeDelete: true)
//                .Index(t => t.CHATID);
            
//            CreateTable(
//                "ZAMBA.CHATUSERS",
//                c => new
//                    {
//                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
//                        NAME = c.String(),
//                        AVATAR = c.String(),
//                        STATUS = c.Decimal(nullable: false, precision: 10, scale: 0),
//                        LASTACTIVEON = c.DateTime(nullable: false),
//                        ROOMID = c.String(),
//                        ROLE = c.Decimal(nullable: false, precision: 10, scale: 0),
//                    })
//                .PrimaryKey(t => t.ID);
            
//        }
        
//        public override void Down()
//        {
//            DropForeignKey("ZAMBA.CHATPEOPLES", "CHATID", "ZAMBA.CHATS");
//            DropForeignKey("ZAMBA.CHATHISTORIES", "CHATID", "ZAMBA.CHATS");
//            DropIndex("ZAMBA.CHATPEOPLES", new[] { "CHATID" });
//            DropIndex("ZAMBA.CHATHISTORIES", new[] { "CHATID" });
//            DropTable("ZAMBA.CHATUSERS");
//            DropTable("ZAMBA.CHATPEOPLES");
//            DropTable("ZAMBA.CHATHISTORIES");
//            DropTable("ZAMBA.CHATS");
//        }
//    }
//}
