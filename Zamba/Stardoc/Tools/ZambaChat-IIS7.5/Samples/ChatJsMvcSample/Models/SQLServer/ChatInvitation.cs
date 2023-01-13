using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChatJsMvcSample.Models
{
    [NotMapped]
    public class ChatInvitation: ChatUser
    { 
        public string InvitedUserName { get; set; }
        public string InvitedEmail { get; set; }
        public string InvitedCompany { get; set; }
        public bool MailCopy { get; set; }
    }

    [NotMapped]
    public class CreateAccountVM
    {
        public decimal Id { get; set; }
        public decimal InvitedId { get; set; }
        public string Email { get; set; }
        public string InvitedEmail { get; set; }
    }
    [NotMapped]
    public class InvitationVM
    {
        public ChatUser ThisUser { get; set; }

        public ChatUser InvitedUser { get; set; }    
    }

}