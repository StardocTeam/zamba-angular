using System.Collections.Generic;
using System.Data;
using Zamba.Servers;
using System;
using Zamba.Core;
using System.Linq;

namespace Zamba.Web.Models
{
    public class SearchSuggestions
    {
        public SearchSuggestions()
        {

            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
               // IUser currentUser = UserBusiness.ValidateLogIn(22242, ClientType.Web);
            }
        }

        private const string qty = " top 20 ";
        private const string etapaQ = "select distinct" + qty + "s.step_Id as Id ,s.name as word from wfstep s inner join WFDocument w on w.step_Id = s.step_Id";
        private const string asignadoQ = "select distinct" + qty + "s.Id, s.name as word from usrtable s inner join WFDocument w on w.User_Asigned = s.id";
        private const string estadoQ = "select distinct" + qty + "s.Doc_State_ID as Id, s.name as word from WFStepStates s inner join WFDocument w on w.Do_State_ID = s.Doc_State_ID";
        private const string joinQ = " inner join ZSearchValues_DT e on e.ResultId = w.Doc_ID inner join ZSearchValues z on z.Id = e.WordId ";
        public DataSet SuggestionsByIndex(int index, int[] entities, string word)
        {
            DataSet ds = new DataSet();

            string query = "select distinct  top 20 word from ZSearchValues_DT e inner join ZSearchValues w on w.Id = e.WordId where indexid = " + index;
            query += " and word like '%" + word + "%' and dtid in(" + String.Join(",", entities.Select(o => o.ToString()).ToArray()) + ")";

            ds = Server.get_Con().ExecuteDataset(CommandType.Text, query);

            return ds;
        }
        public DataSet SuggestionsAdvEnt(int entity, string entWord, int[] index, string[] word, string filter, string filterword)
        {
            DataSet ds = new DataSet();
            string query = firstQuery(filter);

            if (index.Length > 0)
            {
                query += joinQ + "where e.dtid =" + entity + " and (";
                for (var i = 0; i <= index.Length - 1; i++)
                {
                    query += "(e.IndexId =" + index[i] + "and z.Word = '" + word[i] + "') or ";
                }
                query = (query.Remove(query.Length - 4)) + ")";//quito ultimo or              
            }
            else
                query += " where w.DOC_TYPE_ID = " + entity;

            if (entWord != "") query += " and z.Word = '" + entWord + "'";
            if (filterword != "") query += " and s.Name like '%" + filterword + "%'";

            ds = Server.get_Con().ExecuteDataset(CommandType.Text, query);
            return ds;
        }
        public DataSet SuggestionsAdvInd(int index, string indWord, int[] entity, string filter, string filterword)
        {
            DataSet ds = new DataSet();
            string query = firstQuery(filter);
            query += joinQ + "where (e.IndexId= " + index + (indWord != "" ? " and z.Word = '" + indWord + "')" : ")");

            if (entity.Length > 0)
            {
                query += " and (";
                for (var i = 0; i <= entity.Length - 1; i++)
                {
                    query += "(e.DTID =" + entity[i] + ") or ";
                }
                query = (query.Remove(query.Length - 4)) + ")";//quito ultimo or              
            }

            if (filterword != "") query += "and s.Name like '%" + filterword + "%'";

            ds = Server.get_Con().ExecuteDataset(CommandType.Text, query);
            return ds;
        }
        private string firstQuery(string filter)
        {
            string query = "";
            switch (filter)
            {
                case "etapa":
                    query = etapaQ;
                    break;
                case "asignado":
                    query = asignadoQ;
                    break;
                case "estado":
                    query = estadoQ;
                    break;
            }
            return query;
        }
        public DataSet SuggestionsList(int[] index, int[] entities, string[] word)
        {
            DataSet ds = new DataSet();

            string query = "select distinct  top 20 word from ZSearchValues_DT e inner join ZSearchValues w on w.Id = e.WordId where indexid = " + index[0];
            query += " and word like '%" + word[0] + "%' and dtid =" + entities[0];

            for (var i = 1; i <= index.Length - 1; i++)
            {
                query += " and resultid in (select ResultId from ZSearchValues_DT e inner join ZSearchValues w on w.Id = e.WordId inner join WFDocument f on f.Doc_ID = e.ResultId ";
                query += "where DTID = " + entities[i] + " and(IndexId = " + index[i] + " and Word = '" + word[i] + "')";
            }

            ds = Server.get_Con().ExecuteDataset(CommandType.Text, query);
            return ds;
        }
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
                        query += "ZSearchValues.Word like '" + word + "%' ";
                        //query += "ZSearchValues.Word like '" + word + "%' OR ";
                    }
                    //query += " 1=2"; 
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