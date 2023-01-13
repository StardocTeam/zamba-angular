using System;
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Data;
using System.Collections;

namespace Zamba.Services
{
    public class SUsers : IService
    {
        ServicesTypes IService.ServiceType()
        {
            return ServicesTypes.Users;
        }


        private WFUserBusiness WFUserBusiness;
        private UserGroupBusiness UserGroupBusiness;
        private RightsBusiness RightsBusiness;
        private UserBusiness UserBusiness;

        public SUsers()
        {
            UserBusiness = new UserBusiness();
            WFUserBusiness = new WFUserBusiness();
            UserGroupBusiness = new UserGroupBusiness();
            RightsBusiness = new RightsBusiness();
        }

        /// <summary>
        /// Returns a IUser by its Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IUser GetUser(long userId)
        {
            return UserBusiness.GetUserById(userId);
        }

       

       

        /// <summary>
        /// Returns a list of users assigned to to Workflows
        /// </summary>
        /// <param name="workflowsId"></param>
        /// <returns></returns>
        public List<IUser> GetUsersByWorkflows(List<Int64> workflowsId)
        {
            return null;
        }

        public void DeleteUser(Int64 userId)
        {

        }
        
        public Int32 GetAsignedUsersCountByStep(Int64 stepid)
        {
            return WFUserBusiness.GetAsignedUsersCountByStep(stepid);
        }

        public Int32 GetAsignedUsersCountByWorkflow(Int64 workflowid)
        {
            return WFUserBusiness.GetAsignedUsersCountByWorkflow(workflowid);
        }

        public String GetUserorGroupNamebyId(Int64 userid, ref Boolean  IsGroup)
        {
            return UserGroupBusiness.GetUserorGroupNamebyId((long)userid, ref IsGroup);
        }

        public List<User> GetUsersArrayList() 
        {
            List<User> lista_usuarios = new List<User>();
            ArrayList array_usuarios = new ArrayList();

            array_usuarios = UserFactory.GetUsersArrayList();

            foreach (User user in array_usuarios)
                lista_usuarios.Add(user);
            
            return lista_usuarios;
        }

       

      

        public void SaveAction(Int64 objectid,ObjectTypes ObjectTypes, RightsType ActionType,string S_Object_ID)
        {            
             RightsBusiness.SaveAction(objectid,ObjectTypes.ModuleWorkFlow,RightsType.DerivateTask, "Usuario Derivo La tarea", Membership.MembershipHelper.CurrentUser.ID);
        }



       public List<Int64> GetUsersIds(Int64 userGroupID)
       {
          
           return UserGroupBusiness.GetUsersIds(userGroupID);
       }



    }
}
