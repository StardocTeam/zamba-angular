using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamba.Core
{
    public class TokenInfo
    {
        public string token { get; set; }
        public string tokenExpire { get; set; }
        public string userName { get; set; }
        public string refreshToken { get; set; } = "";
        public bool useRefreshTokens { get; set; } = false;
        public string UserId { get; set; }

        public string connectionId { get; set; }

    }
}
