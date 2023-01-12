
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using Zamba.Servers;
public class ZOptFactory
{

    public static void Insert(string key, string value)
    {
        StringBuilder QueryBuilder = new StringBuilder();
        QueryBuilder.Append("INSERT INTO Zopt(Item, Value) ");
        QueryBuilder.Append("VALUES('");
        QueryBuilder.Append(key);
        QueryBuilder.Append("','");
        QueryBuilder.Append(value);
        QueryBuilder.Append("')");

        Server.get_Con().ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString());

        QueryBuilder.Remove(0, QueryBuilder.Length);

    }

    public static void Update(string key, string value)
    {
        StringBuilder QueryBuilder = new StringBuilder();
        QueryBuilder.Append("UPDATE Zopt SET Value='");
        QueryBuilder.Append(value);
        QueryBuilder.Append("' WHERE Item='");
        QueryBuilder.Append(key);
        QueryBuilder.Append("'");

        Server.get_Con().ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString());

        QueryBuilder.Remove(0, QueryBuilder.Length);

    }

    public static string GetValue(string key)
    {
        StringBuilder QueryBuilder = new StringBuilder();
        //Ignore strings case
        if (Server.isOracle)
        {
            QueryBuilder.Append("SELECT Value FROM Zopt WHERE UPPER(Item)='");
            QueryBuilder.Append(key.ToUpper());
        }
        else
        {
            QueryBuilder.Append("SELECT Value FROM Zopt  WITH(NOLOCK)  WHERE Item='");
            QueryBuilder.Append(key);
        }
        QueryBuilder.Append("'");

        object ReturnValue = Server.get_Con().ExecuteScalar(CommandType.Text, QueryBuilder.ToString());
        QueryBuilder.Remove(0, QueryBuilder.Length);

        if (ReturnValue == null) return string.Empty;
        return ReturnValue.ToString();
    }

    public static DataSet GetValues()
    {
        StringBuilder QueryBuilder = new StringBuilder();

        QueryBuilder.Append("SELECT * FROM Zopt");

        return Server.get_Con().ExecuteDataset(CommandType.Text, QueryBuilder.ToString());
    }

    public static void InsertUpdateValue(string key, string value)
    {
        if (Server.isOracle)
        {
            object exist = Server.get_Con().ExecuteDataset(CommandType.Text, "SELECT * FROM ZOPT WHERE ITEM = '" + key + "'");
            if (exist != null && ((DataSet)exist).Tables[0].Rows.Count == 0)
            {
                Insert(key, value);
            }
            else
            {
                Update(key, value);
            }
            exist = null;
        }
        else
        {
            object count = Server.get_Con().ExecuteScalar(CommandType.Text, "SELECT COUNT(1) FROM ZOPT WITH(NOLOCK) WHERE ITEM = '" + key + "'");
            if (count != null && (int)count == 0)
            {
                Insert(key, value);
            }
            else
            {
                Update(key, value);
            }
        }
    }

}