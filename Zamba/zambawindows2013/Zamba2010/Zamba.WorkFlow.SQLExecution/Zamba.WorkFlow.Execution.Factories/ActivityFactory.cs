using System;
using System.Data;
using Zamba.Core;
using Zamba.Servers;

namespace Zamba.WorkFlow.Factories
{
    public class ActivityFactory
    {
        /// <summary>
        /// Return a Dataset with all the Activities from the Workflow
        /// </summary>
        /// <param name="id">Id of the Workflow</param>
        /// <returns>Dataset</returns>
        public static DataSet GetWFActivities(Int64 id)
        {
            try
            {
                string sql = "Select * from wfrules where step_Id=" + id + "  order by name";
                DataSet ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sql);
                return ds;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        /// <summary>
        /// Insert Activities on the database
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <param name="Name">Activity Name</param>
        /// <param name="Class">Activity Type</param>
        /// <param name="Step_ID">Step ID</param>
        /// <param name="ownerID">Owner Activivy ID</param>
        public static void InsertRule(Int64 id, string Name, String Class, Int64 step_id, Int64 ParentID,Int32 ParentType, Int32 Version, Boolean Enable,Int32 Type)
        {
            try
            {
                Int32 enable = 0;
                if (Enable == true)
                {
                    enable = -1;
                }
                string sql = "Insert into wfrules(Id,Name,Class,step_id,ParentID,ParentType,Version,Enable,Type) values (" + id + ",'" + Name + "','" + Class + "'," + step_id + "," + ParentID + "," + ParentType + "," + Version + "," + enable + "," + Type + ")";
                Zamba.Servers.Server.get_Con(false, true, false).ExecuteNonQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Update Activities on the database
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <param name="Name">Activity Name</param>
        /// <param name="Class">Activity Type</param>
        /// <param name="Step_ID">Step ID</param>
        /// <param name="ownerID">Owner Activivy ID</param>
        public static void UpdateRule(Int64 id, string Name, String Class, Int64 step_id, Int64 ParentID,Int32 ParentType, Int32 Version, Boolean Enable,Int32 Type)
        {
            try
            {
                Int32 enable = 0;
                if (Enable == true)
                {
                    enable = -1;
                }
                string sql = "Update wfrules set Name= '" + Name + "',Class= '" + Class + "',step_id= " + step_id + ",ParentID=" + ParentID + ",ParentType=" + ParentType + ",Version=" + Version + ",Enable=" + enable +  ",Type=" + Type +" where Id=" + id;
                Zamba.Servers.Server.get_Con(false, true, false).ExecuteNonQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Delete all activities from a workflow
        /// </summary>
        /// <param name="ID">Step ID</param>
        public static void DeleteStepActivities(Int32 ID)
        {
            try
            {
                string sql = "Delete WFRules where WFRules.Step_id=" + ID;
                Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Get the lastID insert on the database
        /// </summary>
        /// <returns>LastID</returns>
        public static Int32 GetLastID()
        {
            try
            {
                string sql = "Select max(id) as ID from WFRules";
                DataSet ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sql);
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return 0;
            }
        }

        /// <summary>
        /// Devuelve los parametros de la regla
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet GetRuleParams(Int64 id)
        {
            try            
            {
                string sql;

                if (Server.isSQLServer)
                {
                    sql = "Select value from wfruleparamItems where Rule_Id=" + id + " Order By Item";
                }
                else
                {
                    sql = "Select c_value from wfruleparamItems where Rule_Id=" + id + " Order By Item";
                }

                DataSet ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sql);
                return ds;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        /// <summary>
        /// Actualiza el parametro
        /// </summary>
        /// <param name="ItemPos">Posiicon del parametro</param>
        /// <param name="RuleId">Id de la regla</param>
        /// <param name="Value">valor del parametro</param>
        public static void UpdateParamItem(Int32 ItemPos, Int64 RuleId, String Value)
        {
            try
            {
                if (Server.isSQLServer)
                {
                    Object[] parvalues = { Value, RuleId, ItemPos };
                    Server.get_Con(false, true, false).ExecuteNonQuery("Zsp_workflow_100_UpdateParamItem", parvalues);
                }
                else
                {
                    //TODO Oracle
                    String sql = "Update WFRuleParamItems set c_value='" + Value + "' where rule_id=" + RuleId + " And Item = " + ItemPos;
                    Server.get_Con(false, true, false).ExecuteNonQuery(CommandType.Text, sql);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
}
