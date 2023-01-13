using ChatJsMvcSample.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ChatJsMvcSample.Controllers
{
    public class SQLEF
    {
        public static DataTable ExecuteSql(Models.ChatEntities c, string sql)
        {
            var adapter = (IObjectContextAdapter)c;
            var objectContext = adapter.ObjectContext;

            var entityConnection = (System.Data.Entity.Core.EntityClient.EntityConnection)objectContext.Connection;
            DbConnection conn = entityConnection.StoreConnection;
            ConnectionState initialState = conn.State;
            try
            {
                if (initialState != ConnectionState.Open)
                    conn.Open();  // open connection if not already open
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
            }
            finally
            {
                if (initialState != ConnectionState.Open)
                    conn.Close(); // only close connection if not initially open
            }
        }
        public static void RefreshAll(ChatEntities cc)
        {
            foreach (var entity in cc.ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }
    }
}