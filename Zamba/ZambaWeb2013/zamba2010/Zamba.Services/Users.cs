using System;
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Data;
using System.Collections;

namespace Zamba.Services
{
    public class Users : IService
    {
        #region IService Members
        ServicesTypes IService.ServiceType()
        {
            return ServicesTypes.Users;
        }
        #region Get
        /// <summary>
        /// Returns a IUser by its Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IUser GetUser(Int32 userId)
        {
            return UserBusiness.GetUserById(userId);
        }

        /// <summary>
        /// Returns a list of users by its Ids
        /// </summary>
        /// <param name="usersId"></param>
        /// <returns></returns>
        public static List<IUser> GetUsers(List<Int64> usersIds)
        {
            return UserBusiness.GetUsers(usersIds);
        }

        /// <summary>
        /// Returns a list of users assigned to to a Workflow
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public static List<IUser> GetUsersByWorkflow(Int64 workflowId)
        {
            return null;
        }

        /// <summary>
        /// Returns a list of users assigned to to Workflows
        /// </summary>
        /// <param name="workflowsId"></param>
        /// <returns></returns>
        public static List<IUser> GetUsersByWorkflows(List<Int64> workflowsId)
        {
            return null;
        }
        #endregion
        #endregion
        public static void DeleteUser(Int64 userId)
        {

        }
        
        public static Int32 GetAsignedUsersCountByStep(Int64 stepid)
        {
            Int32 UsersCounts;
            UsersCounts = WFUserBusiness.GetAsignedUsersCountByStep(stepid);
            return UsersCounts;
        }

        public static Int32 GetAsignedUsersCountByWorkflow(Int64 workflowid)
        {
            Int32 UsersCounts;
            UsersCounts = WFUserBusiness.GetAsignedUsersCountByWorkflow(workflowid);
            return UsersCounts;
        }

        public static String GetUserorGroupNamebyId(Int64 userid)
        {
            String name = UserGroupBusiness.GetUserorGroupNamebyId(userid);
            return name;
        }

        public static List<User> GetUsersArrayList() 
        {
            List<User> lista_usuarios = new List<User>();
            ArrayList array_usuarios = new ArrayList();

            array_usuarios = UserFactory.GetUsersArrayList();

            foreach (User user in array_usuarios)
                lista_usuarios.Add(user);
            
            return lista_usuarios;
        }
    }
}
