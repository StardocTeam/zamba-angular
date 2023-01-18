using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Zamba.Servers;


namespace ZItoDOC_ID
{ 
	  class ZItoDOC_ID_Factory
	{

        public static  IConnection con = Server.get_Con(true, true, true);
   
        public static   DataSet GetCompleteZI()
        {
            DataSet ds = new DataSet();

            if (Server.isOracle)
            {
                Int32 ascendente = 0;
                string[] parNames = { "orderByDesc", "io_cursor" };
                Object[] parTypes = { 13, 5 };
                Object[] parValues = { ascendente, 2 };
                ds = con.ExecuteDataset("zsp_zitodoci_100.GetCompleteZI", parNames, parTypes, parValues);
            }

            else
            {
                Object[] parameters = { 0 };

                ds = con.ExecuteDataset("zsp_ZItoDocI_100_GetCompleteZI", parameters);
            }

            return ds;

        }
        public static void UpdateDocTable(DataRow row)
        {

            StringBuilder strQuery = new StringBuilder();

            if (Server.isOracle)
            {
                strQuery.Append("update doc_t" + row["dtid"]);
                strQuery.Append(" set platter_id=" + row["UserID"]);
                strQuery.Append(" where doc_id=" + row["docid"]);
            }
            else
            {
                strQuery.Append("update doc" + row["dtid"]);
                strQuery.Append(" set platter_id=" + row["UserID"]);
                strQuery.Append(" where doc_id=" + row["docid"]);
            }
         con.ExecuteScalar(CommandType.Text, strQuery.ToString());         
            
        }

        public static void CloseConection()
        {
            con.Close();
            con.dispose();
        }

        
	}
}
