using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zamba.Framework
{
    public class Zss
    {
        public int ConnectionId { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public String OktaAccessToken { get; set; }
        public String OktaIdToken { get; set; }
        public string TokenQueryString
        {
            get
            {
                return Token;
            }
        }

    }
}