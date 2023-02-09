using System;
using System.Data;
using Zamba;
using Zamba.Core;
using Zamba.Servers;

/// <summary>
/// Llamadas a la base de datos del UserConfig
/// </summary>
/// <history>Marcelo Created 29/10/2010</history>
/// <remarks></remarks>
public class UserPreferencesFactory
{

    /// <summary>
    /// Guarda en la base el valor de la configuracion
    /// </summary>
    /// <param name="name">Nombre de la configuracion a obtener</param>
    /// <param name="value">Valor de la configuracion</param>
    /// <param name="section">Seccion a la que pertenece la configuracion</param>
    /// <param name="userId">ID del usuario a quien pertenece la configuracion</param>
    /// <history>Marcelo Created 29/10/2010</history>
    /// <remarks></remarks>
    public static void setValueDB(string name, string value, UPSections section, Int64 userId)
    {

        if (Server.isOracle)
        {
            System.Text.StringBuilder query = new System.Text.StringBuilder();

            query.Append("SELECT NVL(c_value,'EMPTY') as c_value FROM ZUserConfig WHERE C_NAME = ");
            query.Append("'");
            query.Append(name);
            query.Append("'");
            query.Append(" AND C_SECTION = ");
            query.Append((int)section);
            query.Append(" AND C_USERID = ");
            query.Append(userId);

            object currentvalue = Server.get_Con().ExecuteScalar(CommandType.Text, query.ToString());
            query.Clear();

            if (currentvalue != null && currentvalue != System.DBNull.Value)
            {
                if (currentvalue.ToString() == "EMPTY" || currentvalue.ToString() != value)
                {
                    query.Append("UPDATE ZUserConfig set c_value = '");
                    query.Append(value);
                    query.Append("' where c_name = ");
                    query.Append("'");
                    query.Append(name);
                    query.Append("'");
                    query.Append(" and c_section = ");
                    query.Append((int)section);
                    query.Append(" and c_userId = ");
                    query.Append(userId);
                    Server.get_Con().ExecuteNonQuery(CommandType.Text, query.ToString());
                }
            }
            else
            {
                query.Append("INSERT INTO ZUserConfig (c_name, c_value, c_section, c_userid) values (");
                query.Append("'");
                query.Append(name);
                query.Append("'");
                query.Append(", '");
                query.Append(value);
                query.Append("', ");
                query.Append((int)section);
                query.Append(", ");
                query.Append(userId);
                query.Append(")");
                Server.get_Con().ExecuteNonQuery(CommandType.Text, query.ToString());
            }

        }
        else
        {
            object[] parValues = {
                name,
                value,
                (int)section,
                userId
            };
            Server.get_Con().ExecuteNonQuery("zsp_userpreferences_100_setValueDB", parValues);
        }
    }

    /// <summary>
    /// Obtiene el valor por defecto de la configuracion
    /// </summary>
    /// <param name="name">Nombre de la configuracion a obtener</param>
    /// <param name="section">Seccion donde se encuentra la configuracion</param>
    /// <param name="userId">Id del usuario, en principio no se utiliza, pero se pide por si a futuro se hacen configuracion por defecto por grupo</param>
    /// <history>Marcelo Created 29/10/2010</history>
    /// <remarks></remarks>
    public static string GetDefaultValueDB(string name, UPSections section)
    {
        if (Server.isOracle)
        {
            object ReturnValue = Server.get_Con().ExecuteScalar(CommandType.Text, "Select c_value from ZUserConfig where c_name='" + name + "' and c_section='" + Convert.ToInt32(section) + "' and c_userId=0");
            if (ReturnValue == System.DBNull.Value || ReturnValue == null)
            {
                return null;
            }
            else
            {
                return ReturnValue.ToString();
            }
        }
        else
        {
            object[] parValues = {
                name,
                section,
                0
            };
            object ReturnValue = Server.get_Con().ExecuteScalar("zsp_userpreferences_100_getValueDB", parValues);
            if (ReturnValue == System.DBNull.Value || ReturnValue == null)
            {
                return null;
            }
            else
            {
                return ReturnValue.ToString();
            }
        }
    }

    /// <summary>
    /// Obtiene el valor por defecto de la configuracion
    /// </summary>
    /// <param name="name">Nombre de la configuracion a obtener</param>
    /// <param name="section">Seccion donde se encuentra la configuracion</param>
    /// <param name="userId">Id del usuario</param>
    /// <history>Marcelo Created 29/10/2010</history>
    /// <remarks></remarks>
    public static string getValueDB(string name, UPSections Section, Int64 userId)
    {
        if (Server.isOracle)
        {
            object result = Server.get_Con().ExecuteScalar(CommandType.Text, "SELECT c_value from ZUserConfig where c_name='" + name + "' and c_section='" + Convert.ToInt32(Section) + "' and (c_userId= " + userId + " or c_userId = 0) order by c_userid desc");
            if (result == System.DBNull.Value || result == null)
                return null;
            return result.ToString();
        }
        else
        {
            object result = Server.get_Con().ExecuteScalar(CommandType.Text, "SELECT value from ZUserConfig where name='" + name + "' and section='" + Convert.ToInt32(Section) + "' and (userId= " + userId + " or userId = 0) order by userid desc");
            if (result == System.DBNull.Value || result == null)
                return null;
            return result.ToString();
        }
    }

    public static DataTable getAllValuesDB(Int64 UserId)
    {
        try
        {

            if (Server.isOracle)
            {
                DataSet ds = Server.get_Con().ExecuteDataset(CommandType.Text, string.Format("SELECT c_userid userid, c_name name, c_section section, c_value value from ZUserConfig where c_userid={0} union ALL SELECT c_userid, c_name, c_section, c_value from ZUserConfig d where not exists(    select 1 from ZUserConfig l where d.c_name = l.c_name and d.c_section = l.c_section and l.c_userid <> 0 and l.c_userid = {0} )  and d.c_userid = 0", UserId));
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
            else
            {
                DataSet ds = Server.get_Con().ExecuteDataset(CommandType.Text, string.Format("SELECT userid, name, section, value from ZUserConfig where userid={0} union ALL SELECT userid, name, section, value from ZUserConfig d where not exists( select 1 from ZUserConfig l where d.name = l.name and d.Section = l.section and l.UserId <> 0 and l.UserId = {0}) and d.userid = 0", UserId));
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            return null;
        }
    }

    /// <summary>
    /// Guarda en la base el valor de la configuracion
    /// </summary>
    /// <param name="name">Nombre de la configuracion a obtener</param>
    /// <param name="value">Valor de la configuracion</param>
    /// <param name="section">Seccion a la que pertenece la configuracion</param>
    /// <param name="userId">ID del usuario a quien pertenece la configuracion</param>
    /// <history>Marcelo Created 29/10/2010</history>
    /// <remarks></remarks>
    public static void setValueDBByMachine(string name, string value, UPSections section, string machineName)
    {
        if (Server.isOracle)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                object[] parValues = {
                    name,
                    value,
                    Convert.ToInt32(section),
                    machineName
                };


                string query = $"SELECT count(1) as Cantidad from ZMachineConfig where c_name = '{name}' AND c_section = {(int)section} AND c_machinename = '{machineName}'";

                //string query = "SELECT count(1) into Cantidad from ZMachineConfig where c_name=m_name and m_section=c_section and m_machinename= c_machinename";
                object result = Server.get_Con().ExecuteScalar(CommandType.Text, query);

                if (result != null && int.TryParse(result.ToString(), out int cantidad) && cantidad > 0)
                {
                    query = $"UPDATE ZMachineConfig set c_value = '{value}' where c_name = '{name}' AND c_section={(int)section} AND c_machinename = '{machineName}'";
                    Server.get_Con().ExecuteNonQuery(CommandType.Text, query);
                }
                else
                {
                    query = $"INSERT INTO ZMachineConfig(c_name, c_value, c_section, c_machinename) values('{name}', '{value}', {(int)section}, '{machineName}')";
                    Server.get_Con().ExecuteNonQuery(CommandType.Text, query);
                }
            }
        }
        else
        {
            object[] parValues = {
                name,
                value,
                Convert.ToInt32(section),
                machineName
            };
            Server.get_Con().ExecuteNonQuery("zsp_machinepreferences_100_setValueDB", parValues);
        }
    }

    /// <summary>
    /// Actualiza en la base el valor de la configuracion si existe
    /// </summary>
    /// <param name="name">Nombre de la configuracion a obtener</param>
    /// <param name="value">Valor de la configuracion</param>
    /// <param name="section">Seccion a la que pertenece la configuracion</param>
    /// <param name="userId">ID del usuario a quien pertenece la configuracion</param>
    /// <history>Marcelo Created 29/10/2010</history>
    /// <remarks></remarks>
    public static void updateValueDBByMachine(string name, string value, UPSections section, string machineName)
    {
        if (Server.isOracle)
        {
            object[] parValues = {
                name,
                value,
                Convert.ToInt32(section),
                machineName
            };

            Server.get_Con().ExecuteNonQuery("zsp_machinepreferences_100.updateValueDB", parValues);
        }
        else
        {
            object[] parValues = {
                name,
                value,
                section,
                machineName
            };
            Server.get_Con().ExecuteNonQuery("zsp_machinepreferences_100_updateValueDB", parValues);
        }
    }

    /// <summary>
    /// Obtiene el valor por defecto de la configuracion
    /// </summary>
    /// <param name="name">Nombre de la configuracion a obtener</param>
    /// <param name="section">Seccion donde se encuentra la configuracion</param>
    /// <param name="userId">Id del usuario</param>
    /// <history>Marcelo Created 29/10/2010</history>
    /// <remarks></remarks>
    public static string getValueDBByMachine(string name, UPSections Section, string machineName)
    {
        String strselect = "SELECT c_value from ZMachineConfig where c_name='" + name + "' and c_section='" + Convert.ToInt32(Section) + "' and c_machinename='" + machineName + "'";
        if (!Server.isOracle)
        {
            strselect = strselect.Replace("c_name", "name");
            strselect = strselect.Replace("c_value", "value");
            strselect = strselect.Replace("c_section", "section");
            strselect = strselect.Replace("c_machinename", "machinename");
        }
        object ReturnValue = Server.get_Con().ExecuteScalar(CommandType.Text, strselect);

        if (ReturnValue == System.DBNull.Value || ReturnValue == null)
        {
            return null;
        }
        else
        {
            return ReturnValue.ToString();
        }
    }

    internal static void DeleteUserSetting(string userId, string settingName)
    {
        string query = string.Empty;
        query = "DELETE FROM ZUSERCONFIG WHERE ID = " + userId + "NAME = '" + settingName + "'";
        Server.get_Con().ExecuteNonQuery(query);
    }

    public static DataTable getAllValuesDBByMachine(string machineName)
    {
        try
        {

            if (Server.isOracle)
            {
                DataSet ds = Server.get_Con().ExecuteDataset(CommandType.Text, string.Format("SELECT c_machinename machinename, c_name name, c_section section, c_value value from ZMachineConfig where c_machinename='{0}' union ALL SELECT c_machinename, c_name, c_section, c_value from ZMachineConfig d where not exists(    select 1 from ZMachineConfig l where d.c_name = l.c_name and d.c_section = l.c_section and lower(l.c_machinename) <> 'default' and l.c_machinename = '{0}' )  and lower(d.c_machinename) = 'default'", machineName));
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
            else
            {
                DataSet ds = Server.get_Con().ExecuteDataset(CommandType.Text, string.Format("SELECT machinename,name, section, value from ZMachineConfig where machinename = '{0}' union ALL SELECT machinename, name, section, value from ZMachineConfig d where not exists( select 1 from ZMachineConfig l where d.name = l.name and d.Section = l.section and l.machinename <> 'default' and l.machinename = '{0}') and d.machinename = 'default'", machineName));
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            return null;

        }

    }

}
