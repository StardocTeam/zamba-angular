using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamba.Core;
using Zamba.Data;
using Zamba.Servers;

namespace Zamba.FUS
{
  public  class WebConnections
    {


        private Boolean IsCheckinInactiveSessions = false;
        public void CheckInactiveSessions()
        {
            if (IsCheckinInactiveSessions == false)
            {
                IsCheckinInactiveSessions = true;
                try
                {
                    if (Zamba.Servers.Server.ConInitialized == true)
                    {
                        Int64 conid;
                        long count = 0;
                        while ((conid = UcmFactory.GetFirstExpiredConnection()) != 0 && count < 10)
                        {
                            count = count + 1;
                            if (UcmFactory.UserUniqueConnection(conid))
                            {
                                CloseOpenTasksByConId(conid);
                            }
                            UpdateConIDTaskStateToAsign(conid);
                            UcmFactory.RemoveConnection(conid);
                        }
                        count = 0;

                       ReleaseOpenTasksWithOutConnection();
                    }

                }
                catch (Exception ex)
                {
                    Zamba.Core.ZClass.raiseerror(ex);
                }
                finally
                {
                    IsCheckinInactiveSessions = false;
                }
            }
        }

        WFTasksFactory WTF = new WFTasksFactory();

        public void ReleaseOpenTasksWithOutConnection()
        {
            try
            {
                if (Server.ConInitialized == true)
                {
                    WTF.ReleaseOpenTasksWithOutConnection();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        public void CloseOpenTasksByConId(Int64 conId)
        {
            try
            {
                if (Server.ConInitialized == true)
                {
                    WTF.CloseOpenTasksByConId(conId);
                    Int64 UserId = UcmFactory.GetUserIdByConId(conId);
                    UnLockTasks(UserId);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        public void UpdateConIDTaskStateToAsign(Int64 userId)
        {
            try
            {
                if (Server.ConInitialized == true)
                {
                    WTF.UpdateConIDTaskStateToAsign(userId);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        public static bool UnLockTasks(Int64 UserId)
        {
            StringBuilder StrBuilder = new StringBuilder();
            string StrWhere;
            StrBuilder.Append("Update WFDocument Set ");
            if (Server.isOracle)
            {
                StrBuilder.Append(" c_exclusive = 0");
                StrBuilder.Append(",LastUpdateDate=sysdate");
                StrWhere = " where  c_exclusive = " + Membership.MembershipHelper.CurrentUser.ID;
            }
            else
            {
                StrBuilder.Append(" exclusive = 0");
                StrBuilder.Append(",LastUpdateDate=getdate()");
                StrWhere = " where exclusive = " + Membership.MembershipHelper.CurrentUser.ID;
            }

            StrBuilder.Append(StrWhere);
            Int64 AffectedRows = Server.get_Con().ExecuteNonQuery(CommandType.Text, StrBuilder.ToString());
            if (AffectedRows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
