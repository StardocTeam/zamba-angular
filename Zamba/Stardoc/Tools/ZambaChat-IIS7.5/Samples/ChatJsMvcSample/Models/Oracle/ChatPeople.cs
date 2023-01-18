using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace ChatJsMvcSample.Models
{
    [Table("CHATPEOPLES", Schema = "ZAMBA")]//ORACLE
    public class ChatPeople
    {
        //public ChatPeople(int chatId, int userId)
        //{
        //    ChatId = chatId;
        //    UserId = userId;
        //}
          [Column("ID")]
          [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }
          [Column("CHATID")]
          public decimal ChatId { get; set; }
          [Column("USERID")]
          public decimal UserId { get; set; }

       // public virtual Chat Chat { get; set; }
    }
}
