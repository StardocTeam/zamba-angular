using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamba.Core;
using Zamba.Servers;
using Zamba.Tools;

namespace Zamba.PreLoad
{
  public  class RDB
    {
        Byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };
        Byte[] iv = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };

        /// <summary>
        /// Guarda los valores del app.ini en la base de datos
        /// </summary>
        /// <param name="SERVERTYPE"></param>
        /// <param name="SERVER"></param>
        /// <param name="DB"></param>
        /// <param name="USER"></param>
        /// <param name="PASSWORD"></param>
        /// <param name="Empresa"></param>
        public void saveAppOnDB(DBTYPES SERVERTYPE, string SERVER, string DB, string USER, string PASSWORD)
        {
            try
            {

            String Clean = "Delete from zopt where Item = 'SERVERTYPE'";
            Server.get_Con().ExecuteNonQuery(System.Data.CommandType.Text, Clean);
             Clean = "Delete from zopt where Item = 'SERVER'";
            Server.get_Con().ExecuteNonQuery(System.Data.CommandType.Text, Clean);
             Clean = "Delete from zopt where Item = 'DB'";
            Server.get_Con().ExecuteNonQuery(System.Data.CommandType.Text, Clean);
             Clean = "Delete from zopt where Item = 'USER'";
            Server.get_Con().ExecuteNonQuery(System.Data.CommandType.Text, Clean);
             Clean = "Delete from zopt where Item = 'PASSWORD'";
            Server.get_Con().ExecuteNonQuery(System.Data.CommandType.Text, Clean);

            StringBuilder query = new StringBuilder();
            query.Append("Insert into Zopt (Item, Value) values ('SERVERTYPE','");
            query.Append(Encryption.EncryptString(SERVERTYPE.ToString().Trim(), key, iv));
            query.Append("')");           
            Server.get_Con().ExecuteNonQuery(System.Data.CommandType.Text, query.ToString());

            query.Remove(0, query.Length);
            query.Append("Insert into Zopt (Item, Value) values ('SERVER','");
            query.Append(Encryption.EncryptString(SERVER.Trim(), key, iv));
            query.Append("')");           
            Servers.Server.get_Con().ExecuteNonQuery(System.Data.CommandType.Text, query.ToString());

            query.Remove(0, query.Length);
            query.Append("Insert into Zopt (Item, Value) values ('DB','");
            query.Append(Encryption.EncryptString(DB.Trim(), key, iv));
            query.Append("')");          
            Servers.Server.get_Con().ExecuteNonQuery(System.Data.CommandType.Text, query.ToString());

            query.Remove(0, query.Length);
            query.Append("Insert into Zopt (Item, Value) values ('USER','");
            query.Append(Encryption.EncryptString(USER.Trim(), key, iv));
            query.Append("')");         
            Servers.Server.get_Con().ExecuteNonQuery(System.Data.CommandType.Text, query.ToString());

            query.Remove(0, query.Length);
            query.Append("Insert into Zopt (Item, Value) values ('PASSWORD','");
            query.Append(Encryption.EncryptString(PASSWORD.Trim(), key, iv));
            query.Append("')");         
            Servers.Server.get_Con().ExecuteNonQuery(System.Data.CommandType.Text, query.ToString());
            }
            catch (Exception)
            {
            }

        }

    }
}
