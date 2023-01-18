using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Zamba.DB.Diff
{
    class DBDiff
    {
        private string _DBSource;
        private string _DBDest;
        private string _SPName;

        public DBDiff(string DBSource, string DBDest)
        {
            this._DBSource = DBSource;
            this._DBDest = DBDest;            
        }

        public void GenerateDiffScript(string SPName)
        {
            List<Int64> DocTypesId = new List<Int64>();
            string sql = string.Empty;
            string sql_sp = string.Empty;

            this._SPName = SPName;

            try
            {
                DocTypesId = getAllDocTypesId();

                if (DocTypesId != null)
                {
                    foreach (Int64 Id in DocTypesId)
                    {
                        sql += generateDeleteDocTScript(Id) + "\n";
                        sql += generateInsertDocTScript(Id) + "\n";
                        sql += generateInsertDocIScript(Id) + "\n\n";
                    }

                    sql_sp = string.Format("DROP PROCEDURE {0} ", _SPName) + "\n";
                    sql_sp += string.Format("CREATE PROCEDURE {0} AS {1}", _SPName, sql) + "\n\n";

                    Zamba.Servers.Server.get_Con(false, true, false).ExecuteNonQuery(CommandType.Text, sql_sp);
                }
            }                        
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);             
            }
        }

        public void DeleteDocT(Int64 Id)
        {
            string sql = generateDeleteDocTScript(Id);
            Zamba.Servers.Server.get_Con(false, true, false).ExecuteNonQuery(CommandType.Text, sql);
        }
        
        public void Migrate(Int64 Id)
        {
            string sql = string.Empty; 

            sql += generateDeleteDocTScript(Id) + "\n";
            sql += generateInsertDocTScript(Id) + "\n";
            sql += generateInsertDocIScript(Id) + "\n";

            Zamba.Servers.Server.get_Con(false, true, false).ExecuteNonQuery(CommandType.Text, sql);
        }

        public void MigrateDiffs(Int64 Id)
        {
            string sql = generateInsertDocIDiffsScript(Id);
            Zamba.Servers.Server.get_Con(false, true, false).ExecuteNonQuery(CommandType.Text, sql);
        }

        private string generateDeleteDocTScript(Int64 Id)
        {
            return string.Format("delete from {0}.dbo.DOC_T{1}", _DBDest, Id.ToString());
        }

        private string generateInsertDocTScript(Int64 Id)
        {
            return string.Format("insert into {0}.dbo.doc_t{1} select * from {2}.dbo.doc_t{1}", _DBDest, Id.ToString(), _DBSource);
        }

        private string generateInsertDocIScript(Int64 Id)
        {
            string cols = getDocIColumnNames(Id);

            return string.Format("insert into {0}.dbo.doc_i{1} ({3}) select {3} from {2}.dbo.doc_i{1}", _DBDest, Id.ToString(), _DBSource, cols);
        }

        private string generateInsertDocIDiffsScript(Int64 Id)
        {
            string cols = getDocIColumnNames(Id);

            return string.Format("insert into {0}.dbo.doc_i{1} ({3}) select {3} from {2}.dbo.doc_i{1} WHERE DOC_ID NOT IN (select DOC_ID from {0}.dbo.doc_i{1})", _DBDest, Id.ToString(), _DBSource, cols);
        }

        private List<Int64> getAllDocTypesId()
        {
            List<Int64> DocTypesId = new List<Int64>();
            string sql = "SELECT Doc_Type_ID FROM Doc_Type";

            DataSet ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sql);

            foreach (DataRow dr in ds.Tables[0].Rows)
                DocTypesId.Add(Int64.Parse(dr["Doc_Type_ID"].ToString()));

            return DocTypesId;
        }

        private string getDocIColumnNames(Int64 Id)
        {
            string sql = string.Format("select COLUMN_NAME from {0}.information_schema.columns where TABLE_NAME = 'DOC_I{1}'", _DBSource, Id.ToString());
            string cols = string.Empty;

            DataSet dsCols = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sql);

            foreach (DataRow dr in dsCols.Tables[0].Rows)
                cols += dr["COLUMN_NAME"].ToString() + ",";

            cols = cols.Remove(cols.Length - 1, 1);

            return cols;
        }
    }
}
