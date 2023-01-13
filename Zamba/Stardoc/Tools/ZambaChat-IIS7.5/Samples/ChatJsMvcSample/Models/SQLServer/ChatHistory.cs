using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatJsMvcSample.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatJsMvcSample.Models
{
   // [Table("CHATHISTORIES", Schema = "ZAMBA")]//ORACLE
    public class ChatHistory
    {
        //public ChatHistory( int chatId, int userId, string message, DateTime date)//int chatHistoryId,
        //{
        //  //  ChatHistoryId=chatHistoryId;
        //    ChatId = chatId;
        //    UserId = userId;
        //    Message = message;
        //    Date = date;
        //    //Chat= new List<Chat>();
        //    //ChatUser= new List<ChatUser>();
        //}
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public decimal Id { get; set; }
        [Column("CHATID")]
        public decimal ChatId { get; set; }
        [Column("USERID")]
        public decimal UserId { get; set; }
        [Column("MESSAGE")]
        public string Message { get; set; }
        [Column("DATE")]
        public DateTime Date { get; set; }
        // public virtual Chat Chat { get; set; }
        //public virtual ChatUser ChatUser { get; set; }
    }
}
