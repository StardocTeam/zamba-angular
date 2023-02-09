
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web.Http;
using System.Web.Script.Services;
using ZambaWeb.RestApi.Models;

namespace ZambaWeb.RestApi.Controllers
{
    [RestAPIAuthorize]
    public class InvitedUserController : ApiController
    {

        public InvitedUserController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
           
            }
        }

        [Route("api/InvitedUser/Create")]
        [OverrideAuthorization]
        public void Create(InvitedUser user )
        {
            try
            { 
                if(user.Interno == "")
                {

                    user.Interno = "Sin definir";
                }
                StringBuilder querybldr = new StringBuilder();
                querybldr.Append("Insert into ZnewUsersRequests Values(" + "'" + user.Nombre + "'" + ',' + "'"+ user.Apellido + "'" + ',' + "'" + user.Cliente + "'" + ',' + "'" + user.Sector );
                querybldr.Append("'" + ',' + "'" + user.Puesto + "'" + ',' + "'" + user.Email + "'" + ',' + "'" + user.Telefono + "'" + ',' + "'" + user.Interno + "'" + ',' + "'" + user.Celular); 
                querybldr.Append("'" + ',' + "'" + user.img + "'" + ',' + "'" + user.Password + "'" + ',' + "'" + DateTime.Now + "'" + ',' + "'" + 0 + "'" + ',' + "'" + 0 + "'" + ")");

                string query = string.Format(querybldr.ToString());
                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, query);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }

    }
}
