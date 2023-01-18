
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using Zamba.Servers;

/// <summary>
/// Esta clase se encarga de ejecutar consultas respecto a la base de datos
/// </summary>
/// <remarks></remarks>
public class DBFactory
{
    /// <summary>
    /// Return the columns names
    /// </summary>
    /// <param name="name">Nombre de la tabla</param>
    /// <returns></returns>
    /// <history>Marcelo 01/10/08 Created</history>
    /// <remarks></remarks>
    public static DataSet GetColumns(string strServer, string strDatabase, string strUser, string tableName)
    {
        DataSet ds = null;
        string sql = null;
        StringBuilder strRuta = new StringBuilder();

        if (!string.IsNullOrEmpty(strServer))
        {
            strRuta.Append(strServer);
            strRuta.Append(".");
        }
        if (!string.IsNullOrEmpty(strDatabase))
        {
            strRuta.Append(strDatabase);
            strRuta.Append(".");
        }
        if (Server.isSQLServer)
        {
            if (strRuta.Length > 0)
            {
                strRuta.Append(".");
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(strUser))
            {
                strRuta.Append(strUser);
                strRuta.Append(".");
            }
        }

        if (Server.isSQLServer)
        {
            sql = "exec " + strRuta.ToString() + "sp_columns '" + tableName + "'";
            ds = Server.get_Con().ExecuteDataset(CommandType.Text, sql);
            if ((ds != null) && ds.Tables.Count > 0)
            {
                ds.Tables[0].Columns.Remove("Table_owner");
                ds.Tables[0].Columns.Remove("Table_qualifier");
                ds.Tables[0].Columns.Remove("Table_name");
            }
        }
        else
        {
            sql = "Desc " + strRuta.ToString() + tableName;
            ds = Server.get_Con().ExecuteDataset(CommandType.Text, sql);
        }

        sql = null;
        strRuta = null;

        return ds;
    }

    /// <summary>
    /// Devuelve una lista con todas las tablas y vistas de la base de datos
    /// </summary>
    /// <returns></returns>
    /// <history>Marcelo 01/10/08 Created</history>
    /// <remarks></remarks>
    public static DataSet GetTablesAndViews(string strServer, string strDatabase, string strUser)
    {
        DataSet ds = null;
        string sql = null;
        StringBuilder strRuta = new StringBuilder();

        if (!string.IsNullOrEmpty(strServer))
        {
            if (strServer.Contains("."))
            {
                strRuta.Append("[");
                strRuta.Append(strServer);
                strRuta.Append("]");
            }
            else
            {
                strRuta.Append(strServer);
            }
            strRuta.Append(".");
        }
        if (!string.IsNullOrEmpty(strDatabase))
        {
            strRuta.Append(strDatabase);
            strRuta.Append(".");
        }
        if (Server.isSQLServer)
        {
            if (strRuta.Length > 0)
            {
                strRuta.Append(".");
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(strUser))
            {
                strRuta.Append(strUser);
                strRuta.Append(".");
            }
        }

        if (Server.isSQLServer)
        {
            sql = "select name from " + strRuta.ToString() + "sysobjects where xtype = 'V' or xtype = 'U' order by name";
            ds = Server.get_Con().ExecuteDataset(CommandType.Text, sql);
        }
        else
        {
            sql = "select table_name from " + strRuta.ToString() + "all_views where OWNER = '" + oracleOwner + "';";
            DataSet dsaux = null;
            ds = Server.get_Con().ExecuteDataset(CommandType.Text, sql);
            sql = "select table_name from " + strRuta.ToString() + "all_tables where OWNER = '" + oracleOwner + "';";
            dsaux = Server.get_Con().ExecuteDataset(CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                ds.Tables[0].Merge(dsaux.Tables[0]);
            }
            else
            {
                ds = dsaux;
            }
        }

        sql = null;
        strRuta = null;

        return ds;
    }

    /// <summary>
    /// Owner en oracle, necesario para algunas consultas
    /// </summary>
    /// <remarks></remarks>
    private static string oracleOwner
    {
        get
        {
            try
            {
                string tmpValue = Server.get_Con().ConString.Split(Char.Parse(";"))[1];
                tmpValue = tmpValue.Replace("User Id=", string.Empty);
                return tmpValue.ToUpper();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
