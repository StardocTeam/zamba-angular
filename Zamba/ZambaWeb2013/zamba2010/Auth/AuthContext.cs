using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ZambaWeb.RestApi.Models;

namespace ZambaWeb.RestApi.Auth
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
       // public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}