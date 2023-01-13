using System.Collections.Generic;
using System.Data;
using Zamba.Servers;
using System;

namespace ZambaWeb.RestApi.Models
{
    public class SearchSuggestions
    {
        public DataSet getData(string text="")
        {
            DataSet ds = new DataSet();
            List<string> words; 
            //TODO pasar a zamba server y agregar filtrado por permisos
            string query = "SELECT top 1000 ZSearchValues.Word, WordId, IndexId, DTID FROM ZSearchValues_dt inner join ZSearchValues on wordId = ZSearchValues.Id";
 
            if(text != "")
            {
                words = new List<string>(text.Split(new Char[] { ' ' }));
                if(words.Count > 0)
                {
                    query += " where ";
                    foreach(string word in words)
                    {
                        query += "ZSearchValues.Word like '" + word + "%' OR ";
                    }
                    query += " 1=2"; 
                }
            }
             ds = Server.get_Con().ExecuteDataset(CommandType.Text, query);    
                   
             //TODO procesar el dataset para mostrar sugerencias con palabras multiples
            
            
           return ds;
        }

        public List<Dictionary<string, object>> GetJson(DataTable dtData)
        {
            List<Dictionary<string, object>>
            lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;

            foreach (DataRow dr in dtData.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in dtData.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }
            return lstRows;
        }
    }
}