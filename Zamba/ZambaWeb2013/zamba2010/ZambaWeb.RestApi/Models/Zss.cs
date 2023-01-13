using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZambaWeb.RestApi.Models
{
    public class Zss
    {
        public int ConnectionId { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime TokenExpireDate { get; set; }

    }
}