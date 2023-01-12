using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using Zamba.Core;
using Zamba.Data;
using Zamba.Servers;

namespace ZambaWeb.RestApi.Controllers.Class
{
    public class CacheFactory
    {
        public CacheFactory()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
        }

    

        public int CheckDesignVersion()
        {
            try
            {
                var query = "SELECT value from zopt where item='LASTDESIGNVERSION'";
                var select = Server.get_Con().ExecuteScalar(CommandType.Text, query);
                if (select != null)
                    return Convert.ToInt32(select);
                else
                    return 1;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return 1;
            }
        }

        internal int CheckCurrentDesignVersion(Int64 userId)
        {
            try
            {
                if (Server.isOracle)
                {
                    var query = string.Format("SELECT c_value from zuserconfig where c_name='LASTDESIGNVERSION' and c_userid = {0}", userId);
                    var select = Server.get_Con().ExecuteScalar(CommandType.Text, query);
                    return Convert.ToInt32(select);
                }
                else
                {
                    var query = string.Format("SELECT value from zuserconfig where name='LASTDESIGNVERSION' and userid = {0}", userId);
                    var select = Server.get_Con().ExecuteScalar(CommandType.Text, query);
                    return Convert.ToInt32(select);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        internal void SetCurrentDesignVersion(string version, Int64 userId)
        {
            UserPreferencesFactory.setValueDB("LASTDESIGNVERSION", version, Zamba.UPSections.UserPreferences, userId);
        }
    }
}