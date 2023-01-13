using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using ChatJsMvcSample.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ChatJsMvcSample.Models
{
    //  [Table("CHATS", Schema = "ZAMBA")]    //ORACLE
   public class Chat
    {
        //public Chat(int adminId)//int chatId,
        //{
        //   // ChatId = chatId;
        //    AdminId = adminId;
        //    //ChatPeople = new List<ChatPeople>();
        //    //ChatHistory = new List<ChatHistory>();
        //}
        //[Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]//, TypeName="integer"
        //[NotMapped]
        public decimal Id { get; set; }


        //[Key]
        //public int IdInt
        //{
        //    get { return Convert.ToInt32(Id); }
        //    private set { Id = Convert.ToInt32(value); }
        //}

        [Column("ADMINID")]
        public decimal AdminId { get; set; }

        [Column("LASTMESSAGE")]
        public DateTime LastMessage { get; set; }

        [Column("CHATNAME")]
        public string ChatName { get; set; }

        [Column("DocId")]
        public int? DocId { get; set; }

        //[Column("TYPE")]
        //public decimal Type { get; set; }

        [NotMapped]
        public ChatType ChatType { get; set; }
 
        [Column("CHATTYPE")]
        public decimal ChatTypeDecimal
        {
            get { return Convert.ToDecimal(ChatType); }
            private set { ChatType = EnumExtensions.ParseEnum<ChatType>(value); }
        }
       
        public virtual ICollection<ChatPeople> ChatPeople { get; set; }

        public virtual ICollection<ChatHistory> ChatHistory { get; set; }

    }
    public enum ChatType
    {
        Single = 1,
        Group = 2       
    }
}
