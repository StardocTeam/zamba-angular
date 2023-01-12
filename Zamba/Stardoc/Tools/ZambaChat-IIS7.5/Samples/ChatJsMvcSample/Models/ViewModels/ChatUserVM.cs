using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatJsMvcSample.Models.ViewModels
{
    public class ChatUserVM
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Company { get; set; }
    }

    //Incluye el id de usuario quien envia la solicitud
    public class ChatUserVMId
    {
        public decimal Id { get; set; }
        public ChatUserVM InvitedUser { get; set; }
    }
}