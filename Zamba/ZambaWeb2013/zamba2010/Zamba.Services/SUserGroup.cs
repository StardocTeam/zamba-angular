using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;

namespace Zamba.Services
{
    public class SUserGroup:IService
    {
        #region IService Members

        public ServicesTypes ServiceType()
        {
            return ServicesTypes.UserGroup;
        }

        #endregion

        UserGroupBusiness UserGroupBusiness;

        public SUserGroup(ref IUser currentUser)
        {
            UserGroupBusiness = new UserGroupBusiness();
        }

        private SUserGroup() { }

        public string GetUserorGroupNamebyId(Int64 userGroupId, ref Boolean IsGroup)
        {
             return UserGroupBusiness.GetUserorGroupNamebyId(userGroupId,ref IsGroup);
        }
    }
}
