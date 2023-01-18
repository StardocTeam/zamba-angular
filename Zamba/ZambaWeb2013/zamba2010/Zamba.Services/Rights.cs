using System;
using System.Collections.Generic;
using Zamba.Core;

namespace Zamba.Services
{
    public class Rights : IService
    {
        #region IService Members
        ServicesTypes IService.ServiceType()
        {
            return ServicesTypes.Rights;
        }
        #region Singleton
        private static Rights _rights = null;

        private Rights()
        {
        }

        public static IService GetInstance()
        {
            if (_rights == null)
                _rights = new Rights();

            return _rights;
        }
        #endregion
        #endregion
        //D:\Zamba2007\Zamba.AdminControls\Controls\UCWFStepsRights.vb

        /// <summary>
        /// Determines whether an user can derive a task
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static Boolean CanDerive(Int64 userId, Int64 taskId)
        {
            return false;
        }

        /// <summary>
        /// Determines whether an user can derive some tasks
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskIds"></param>
        /// <returns></returns>
        public static Boolean CanDerive(Int64 userId, List<Int64> taskIds)
        {
            return false;
        }

        /// <summary>
        /// Determines whether an user can reject some tasks
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static Boolean CanReject(Int64 userId, Int64 taskId)
        {
            return false;
        }

        /// <summary>
        /// Determines whether an user can reject a task
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskIds"></param>
        /// <returns></returns>
        public static Boolean CanReject(Int64 userId, List<Int64> taskIds)
        {
            return false;
        }

        public static Zamba.Core.IUser ValidateLogIn(string User, string Password, ClientType clientType)
        {
            return Zamba.Core.UserBusiness.Rights.ValidateLogIn(User, Password, clientType);
        }

        public static Zamba.Core.IUser ValidateLogIn(int ID, ClientType clientType)
        {
            return Zamba.Core.UserBusiness.Rights.ValidateLogIn(ID, clientType);
        }
    }
}