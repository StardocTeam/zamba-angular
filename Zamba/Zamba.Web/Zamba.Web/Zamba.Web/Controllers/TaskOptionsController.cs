using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zamba.Core;
using Zamba.Data;
using Zamba.Servers;

namespace Zamba.Web.Controllers
{
    public class TaskOptionsController : Controller
    {
        // GET: TaskOptions
        public TaskOptionsController()
        {
            try
            {
                if (Zamba.Servers.Server.ConInitialized == false)
                {
                    Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                    ZC.InitializeSystem("Zamba.Web");
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public void UpdateFavorite(Int64 docId, int docTypeId, int userId, bool val)
        {
            Result r = new Result(docId, new Zamba.Core.DocType(docTypeId), string.Empty, DateTime.Now, DateTime.Now);
            r.IsFavorite = val;
            DocumentLabelsBusiness DLB = new DocumentLabelsBusiness();
            DLB.UpdateFavoriteLabel(r);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public void UpdateImportant(Int64 docId, int docTypeId, int userId, bool val)
        {
            Result r = new Result(docId, new Zamba.Core.DocType(docTypeId), string.Empty, DateTime.Now, DateTime.Now);
            r.IsImportant = val;
            DocumentLabelsBusiness DLB = new DocumentLabelsBusiness();
            DLB.UpdateImportanceLabel(r);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public void AddNews(Int64 docId, int docTypeId, int userId, string val)
        {
            Transaction t = new Transaction();
            try
            {
                Int64 id = CoreData.GetNewID(IdTypes.News);

                var zNewsQuery = String.Format("INSERT INTO ZNews (NEWSID, DOCID, DOCTYPEID, VALUE, CRDATE) VALUES ({0}, {1}, {2}, '{3}', '{4}')", id, docId, docTypeId, val, DateTime.Now);
                var zNewsUserQuery = String.Format("INSERT INTO ZNewsUsers (NEWSID, USERID, STATUS) VALUES ({0}, {1}, {2})", id, userId, 0);

                t.Con.ExecuteNonQuery(t.Transaction, System.Data.CommandType.Text, zNewsQuery);
                t.Con.ExecuteNonQuery(t.Transaction, System.Data.CommandType.Text, zNewsUserQuery);
                t.Commit();
            }
            catch (Exception ex)
            {
                if (t != null && t.Transaction != null && t.Transaction.Connection.State != System.Data.ConnectionState.Closed)
                {
                    t.Rollback();
                }
                ZClass.raiseerror(ex);
            }
            finally
            {
                if (t != null)
                {
                    if (t.Con != null)
                    {
                        t.Con.Close();
                        t.Con.dispose();
                        t.Con = null;
                    }
                    t.Dispose();
                    t = null;
                }
            }
        }
    }
}