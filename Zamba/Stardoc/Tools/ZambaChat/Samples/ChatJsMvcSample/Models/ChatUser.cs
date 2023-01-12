using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ChatJsMvcSample.Models
{
    /// <summary>
    /// Information about a chat user
    /// </summary>
 
  [Table("CHATUSERS", Schema = "ZAMBA")]//ORACLE
    public class ChatUser
    {
        /// <summary>
        /// User chat status. 
        /// </summary>
        public enum StatusType
        {
            Offline = 0,
            Online = 1,
            Busy = 2,
            DontDisturb = 3
        }

        public enum RoleType
        {
            Active = 0,
            Listener = 1,
            Blocked = 2
        }

        public ChatUser()
        {
            this.Status = StatusType.Offline;
        }

        /// <summary>
        /// User Id (preferebly the same as database user Id)
        /// </summary>      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public decimal Id { get; set; }
       
        /// User display name
        /// </summary>
        [Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// User profile picture URL (Gravatar, for instance)
        /// </summary>
        // public byte[] Avatar { get; set; }
        [Column("AVATAR")]
        public string Avatar { get; set; }
        /// <summary>
        /// User's status
        /// </summary>
        [NotMapped]
        public StatusType Status { get; set; }//StatusType

        //EF 4.1 no soporta guardar enum porque son Int, se debe tratar como decimal
        [Column("STATUS")]
        public decimal StatusDecimal
        {
            get { return Convert.ToDecimal(Status); }
            private set { Status = EnumExtensions.ParseEnum<StatusType>(value); }
        }
        public class EnumExtensions
        {
            public static T ParseEnum<T>(decimal value)
            {
                return (T)(Enum.Parse(typeof(T), value.ToString(), true));
            }
        }

        /// <summary>
        /// Last time this user has been active
        /// </summary>
        [Column("LASTACTIVEON")]//, TypeName = "datetime2"
        //[Column(TypeName = "datetime2")]
        public DateTime LastActiveOn { get; set; }
        /// User room id
        /// </summary>
        [Column("ROOMID")]
        public string RoomId { get; set; }

        //ORACLE
        [NotMapped]
        public RoleType Role { get; set; }
        [Column("ROLE")]
        public decimal RoleDecimal
        {
            get { return Convert.ToDecimal(Role); }
            private set { Role = EnumExtensions.ParseEnum<RoleType>(value); }
        }

        //SQL
        //[Column("ROLE")]
        //public RoleType Role { get; set; }

       
      

    }
}