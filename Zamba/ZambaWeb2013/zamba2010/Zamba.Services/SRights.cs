using System;
using System.Collections.Generic;
using Zamba.Core;
using System.Collections;
using System.Data;

namespace Zamba.Services
{
    public class SRights : IService
    {
        #region IService Members
        ServicesTypes IService.ServiceType()
        {
            return ServicesTypes.Rights;
        }
        #region Singleton
        private static SRights _rights = null;

        public static IService GetInstance()
        {
            if (_rights == null)
                _rights = new SRights();

            return _rights;
        }
        #endregion
        #endregion

        #region Attributes
        private RightsBusiness RB = new RightsBusiness();
        private UserBusiness UB = new UserBusiness();
        #endregion
              

        /// <summary>
        /// Determines whether an user can derive a task
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public Boolean CanDerive(Int64 userId, Int64 taskId)
        {
            return false;
        }

        /// <summary>
        /// Determines whether an user can derive some tasks
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskIds"></param>
        /// <returns></returns>
        public Boolean CanDerive(Int64 userId, List<Int64> taskIds)
        {
            return false;
        }

        /// <summary>
        /// Determines whether an user can reject some tasks
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public Boolean CanReject(Int64 userId, Int64 taskId)
        {
            return false;
        }

        /// <summary>
        /// Determines whether an user can reject a task
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskIds"></param>
        /// <returns></returns>
        public Boolean CanReject(Int64 userId, List<Int64> taskIds)
        {
            return false;
        }

      

      

        public DsDoctypeRight GetDocTypeUserRightFromArchive(Int64 Doc_GroupID)
        {
            return RB.GetDocTypeUserRightFromArchive(Doc_GroupID);
        }

        public void SaveActionWebView(Int64 ObjectId, ObjectTypes ObjectType, string machineName, RightsType ActionType, string S_Object_ID)
        {
            RB.SaveActionWebView(ObjectId, ObjectType, ActionType, S_Object_ID, Membership.MembershipHelper.CurrentUser.ID);
            //SaveActionWebView(ObjectId, ObjectType, Environment.MachineName, ActionType, S_Object_ID, _currUserid);
        }

        public DataSet GetArchivosUserRight()
        {
            return RB.GetArchivosUserRight();
        }

        public void SaveActionWebView(Int64 ObjectId, ObjectTypes ObjectType, RightsType ActionType, string S_Object_ID)
        {
            RB.SaveActionWebView(ObjectId, ObjectType, ActionType, S_Object_ID, Membership.MembershipHelper.CurrentUser.ID);
        }



      

        public IUser ValidateLogIn(string User, string Password, ClientType clientType)
        {
            return  UB.ValidateLogIn(User, Password, clientType);
        }


        public void RemoveConnectionFromWeb(Int64 connectionId)
        {
            Ucm ucm = new Ucm();
            ucm.RemoveConnectionFromWeb(connectionId);
        }
             

     
    }
}