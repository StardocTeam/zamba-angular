using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ChatJsMvcSample.Models
{
    [NotMapped]
    public class EditChatUser : ChatJsMvcSample.Models.ChatUser
    {
        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }
        [NotMapped]
        public decimal InvSenderId { get; set; }
        public EditChatUser()
        {
        }
        public EditChatUser(ChatUser p)
        {
            foreach (FieldInfo prop in p.GetType().GetFields())
                GetType().GetField(prop.Name).SetValue(this, prop.GetValue(p));

            foreach (PropertyInfo prop in p.GetType().GetProperties())
            {
                string type = GetType().GetProperty(prop.Name).ToString();
                if (type != "System.Decimal StatusDecimal" && type != "System.Decimal RoleDecimal")
                    GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(p, null), null);
            }
        }
    }
}