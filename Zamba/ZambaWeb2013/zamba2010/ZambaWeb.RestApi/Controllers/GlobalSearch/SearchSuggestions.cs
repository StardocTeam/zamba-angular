using System.Collections.Generic;
using System.Data;
using Zamba.Servers;
using System;
using System.Linq;
using Zamba.Core;
using System.Text;

namespace ZambaWeb.RestApi.Models
{
    public class SearchSuggestions
    {

        public SearchSuggestions()
        {

            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
                // IUser currentUser = UserBusiness.ValidateLogIn(22242, ClientType.WebApi);
            }

        }

        private const string qty = " top 20 ";
        private string etapaQ = "select distinct" + ((Server.isSQLServer) ? qty : string.Empty) + "s.step_Id as Id ,s.name as word from wfstep s inner join WFDocument w on w.step_Id = s.step_Id";
        private string asignadoQ = "select distinct" + ((Server.isSQLServer) ? qty : string.Empty) + "s.Id, s.name as word from usrtable s inner join WFDocument w on w.User_Asigned = s.id";
        private string estadoQ = "select distinct" + ((Server.isSQLServer) ? qty : string.Empty) + "s.Doc_State_ID as Id, s.name as word from WFStepStates s inner join WFDocument w on w.Do_State_ID = s.Doc_State_ID";
        private const string joinQ = " inner join ZSearchValues_DT e on e.ResultId = w.Doc_ID inner join ZSearchValues z on z.Id = e.WordId ";
        public DataSet SuggestionsByIndex(int index, int[] entities, string word)
        {
            DataSet ds = new DataSet();

            StringBuilder query = new StringBuilder();
            query.Append("select distinct");
            query.Append((Server.isSQLServer) ? " top 20 " : string.Empty);
            query.Append(" word as word from ZSearchValues_DT e inner join ZSearchValues w on w.Id = e.WordId where indexid = ");
            query.Append(index);
            query.Append(" and word like '%");
            query.Append(word);
            query.Append("%' and dtid in(");
            query.Append(String.Join(",", entities.Select(o => o.ToString()).ToArray()));
            query.Append(")");

            query.Append((Server.isOracle) ? " and rownum <= 20 " : string.Empty);

            ds = Server.get_Con().ExecuteDataset(CommandType.Text, query.ToString());

            return ds;
        }

        public DataSet getData(string text = "")
        {
            DataSet ds = new DataSet();
            List<string> words;

//            SELECT distinct  w.Word as Word, e.WordId as WordId , e.IndexId as IndexId, e.DTID as DTID
//FROM ZSearchValues_dt e inner join(select w.Word, w.id  from ZSearchValues w where w.Word like 'la%' OR w.Word like 'na%' and rownum <= 20
//) w on e.wordId = w.Id and rownum <= 20

            //TODO pasar a zamba server y agregar filtrado por permisos
            StringBuilder query = new StringBuilder();
            query.Append("SELECT distinct ");

            query.Append((Server.isSQLServer) ? " top 20 " : string.Empty);

            query.Append(" w.Word as Word, e.WordId as WordId , e.IndexId as IndexId, e.DTID as DTID FROM ZSearchValues_dt e inner join(select");

            query.Append((Server.isSQLServer) ? " top 20 " : string.Empty);

            query.Append(" w.Word, w.id  from ZSearchValues w ");

            if (text != "")
            {
                words = new List<string>(text.Split(new Char[] { ' ' }));
                if (words.Count > 0)
                {
                    query.Append(" where ");
                    foreach (string word in words)
                    {
                        query.Append("w.Word like '");
                        query.Append(word);
                        query.Append("%' ");
                        //query.Append("OR ");
                    }
                    //query.Append(" 1=2");
                }
                query.Append((Server.isOracle) ? " and rownum <= 20 " : string.Empty);
            }
            else
            {
                query.Append((Server.isOracle) ? " where rownum <= 20 " : string.Empty);
            }

            query.Append(") w on e.wordId = w.Id ");
            query.Append((Server.isOracle) ? " and rownum <= 20 " : string.Empty);

            ds = Server.get_Con().ExecuteDataset(CommandType.Text, query.ToString());

            //TODO procesar el dataset para mostrar sugerencias con palabras multiples


            return ds;
        }

        public DataSet SuggestionsAdvEnt(int entity, string entWord, int[] index, string[] word, string filter, string filterword)
        {
            DataSet ds = new DataSet();
            StringBuilder query = new StringBuilder();
            query.Append(firstQuery(filter));

            if (index.Length > 0)
            {
                query.Append(joinQ);
                query.Append("where e.dtid =");
                query.Append(entity);
                query.Append(" and (");

                for (var i = 0; i <= index.Length - 1; i++)
                {
                    query.Append("(e.IndexId =");
                    query.Append(index[i]);
                    query.Append("and z.Word = '");
                    query.Append(word[i]);
                    query.Append("') or ");
                }
                query.Remove(query.Length - 4, 4);
                query.Append(")");
                //quito ultimo or              
            }
            else
                query.Append(" where w.DOC_TYPE_ID = ");
            query.Append(entity);

            if (entWord != "") { query.Append(" and z.Word = '"); query.Append(entWord); query.Append("'"); }
            if (filterword != "") { query.Append(" and s.Name like '%"); query.Append(filterword); query.Append("%'"); }


            query.Append((Server.isOracle) ? " where rownum <= 20 " : string.Empty);

            ds = Server.get_Con().ExecuteDataset(CommandType.Text, query.ToString());
            return ds;
        }
        public DataSet SuggestionsAdvInd(int index, string indWord, int[] entity, string filter, string filterword)
        {
            DataSet ds = new DataSet();
            StringBuilder query = new StringBuilder();
            query.Append(firstQuery(filter));

            query.Append(joinQ);
            query.Append("where (e.IndexId= ");
            query.Append(index);
            query.Append((indWord != "" ? " and z.Word = '" + indWord + "')" : ")"));

            if (entity.Length > 0)
            {
                query.Append(" and (");
                for (var i = 0; i <= entity.Length - 1; i++)
                {
                    query.Append("(e.DTID =");
                    query.Append(entity[i]);
                    query.Append(") or ");
                }
                query.Remove(query.Length - 4, 4);
                query.Append(")");
                //quito ultimo or              
            }

            if (filterword != "")
            {
                query.Append("and s.Name like '%");
                query.Append(filterword);
                query.Append("%'");
            }


            query.Append((Server.isOracle) ? " where rownum <= 20 " : string.Empty);

            ds = Server.get_Con().ExecuteDataset(CommandType.Text, query.ToString());
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

            StringBuilder query = new StringBuilder();
            query.Append("select distinct ");
            query.Append((Server.isSQLServer) ? " top 20 " : string.Empty);
            query.Append(" word as word from ZSearchValues_DT e inner join ZSearchValues w on w.Id = e.WordId where indexid = ");
            query.Append(index[0]);
            query.Append(" and word like '%");
            query.Append(word[0]);
            query.Append("%' and dtid =");
            query.Append(entities[0]);

            for (var i = 1; i <= index.Length - 1; i++)
            {
                query.Append(" and resultid in (select ResultId from ZSearchValues_DT e inner join ZSearchValues w on w.Id = e.WordId inner join WFDocument f on f.Doc_ID = e.ResultId ");
                query.Append("where DTID = ");
                query.Append(entities[i]);
                query.Append(" and(IndexId = ");
                query.Append(index[i]);
                query.Append(" and Word = '");
                query.Append(word[i]);
                query.Append("')");
            }

            query.Append((Server.isOracle) ? " and rownum <= 20 " : string.Empty);

            ds = Server.get_Con().ExecuteDataset(CommandType.Text, query.ToString());
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
                    dictRow.Add(col.ColumnName.ToLower(), dr[col]);
                }
                lstRows.Add(dictRow);
            }
            return lstRows;
        }
    }
}