using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core;

namespace ZambaWeb.RestApi.Controllers.Dashboard.DB
{
    public class PermissionsDatabase
    {
        public ArrayList getUserPermissions(long zambaUserId) {
            UserGroupBusiness userGroupBussiness = new UserGroupBusiness();
            return userGroupBussiness.getUserGroups(zambaUserId);
        }
    }
}