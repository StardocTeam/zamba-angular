using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Zamba.Core;
using Zamba.WebAdmin.Models;
using Zamba.WebAdmin.Models.News;

namespace Zamba.WebAdmin.Controllers.Api
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class newsController : ApiController
    {
        public newsController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
            db = new ContextDB(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }

        private ContextDB db;

        [Route("api/news/GetTemplates")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetTemplates()
        {
            var lst = new List<string>();
            string assemblyFile = Path.GetDirectoryName((
                             new System.Uri(Assembly.GetExecutingAssembly().CodeBase)
                         ).AbsolutePath).ToString();
            var str = File.ReadAllText(assemblyFile + "\\Content\\htmltemplate\\new.html");
            lst.Add(str);
            return Ok(lst);
        }

        // GET: api/News
        public List<ZInformation> GetNewsModel()
        {
            var hml = db.ZInformation.ToList();
            return hml;
        }

        public partial class WidgetDTO
        {
            public Int64 Id { get; set; }
            public Int64 TypeId { get; set; }
            public string Name { get; set; }

            public WidgetDTO(long id, long typeId, string name)
            {
                this.Id = id;
                this.TypeId = typeId;
                this.Name = name;
            }
        }


        [Route("api/news/GetImportants")]
        public List<WidgetDTO> GetImportants(int Userid)
        {
            List<WidgetDTO> ImportantsList = new List<WidgetDTO>();
            try
            {
                StringBuilder QueryString = new StringBuilder();
                StringBuilder SecondQuery = new StringBuilder();
                QueryString.Append("SELECT distinct doctypeid FROM DocumentLabels WHERE userid = 0 AND importance = 1 ");
                string query = string.Format(QueryString.ToString());
                var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
                DataTable firstTable = dataSet.Tables[0];

                DataTable secondTable = null;
                foreach (DataRow i in firstTable.Rows)
                {
                    SecondQuery.Append("SELECT docid,name,doctypeid,ICON_ID FROM DocumentLabels L inner join doc" + i[0] + " D on D.doc_id = L.docid WHERE userid = 0 AND importance = 1 ");
                    string query2 = string.Format(SecondQuery.ToString());
                    var dataSet2 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query2);
                    secondTable = dataSet2.Tables[0];
                }
                if (secondTable != null)
                {
                    foreach (DataRow row in secondTable.Rows)
                        ImportantsList.Add(new WidgetDTO(Int64.Parse(row["docid"].ToString()), Int64.Parse(row["doctypeid"].ToString()), row["name"].ToString()));
                }

            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }
            return ImportantsList;
        }


        [Route("api/news/GetNovedades")]
        public List<WidgetDTO> GetNovedades(int Userid)
        {
            List<WidgetDTO> NovedadesList = new List<WidgetDTO>();
            try
            {
                StringBuilder QueryString = new StringBuilder();

                if (Servers.Server.isOracle)
                {
                    QueryString.Append("select znu.NewsId, zn.docid, zn.doctypeid, zn.c_value, Zn.crdate ");
                    QueryString.Append("from ZNewsUsers znu inner join ZNews Zn on znu.newsid = zn.newsid where znu.userid = '");
                }
                else
                {
                    QueryString.Append("select znu.NewsId, zn.docid, zn.doctypeid, zn.value, Zn.crdate ");
                    QueryString.Append("from ZNewsUsers as znu inner join ZNews as Zn on znu.newsid = zn.newsid where znu.userid = '");
                }

                QueryString.Append(Userid);
                QueryString.Append("' and znu.status = 0 order by zn.crdate desc");

                var dataSet = Servers.Server.get_Con().ExecuteDataset(CommandType.Text, QueryString.ToString());

                foreach (DataRow row in dataSet.Tables[0].Rows)
                    NovedadesList.Add(new WidgetDTO(Int64.Parse(row["docid"].ToString()), Int64.Parse(row["doctypeid"].ToString()), "TEST"));
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }
            return NovedadesList;
        }




        public partial class RecentTask
        {
            public int Userid { get; set; }
            public int Docid { get; set; }
            public int Doctypeid { get; set; }
            public string Name { get; set; }

            public string Date { get; set; }

            public RecentTask(int userid, int docid, int doctypeid, string name, string date)
            {
                this.Userid = userid;
                this.Docid = docid;
                this.Doctypeid = doctypeid;
                this.Name = name;
                this.Date = date;
            }
        }

        //insertRecientes
        [Route("api/news/InsertRecents")]
        public void InsertRecents(RecentTask recentTask)
        {
            try
            {

                StringBuilder QueryString = new StringBuilder();
                QueryString.Append("insert into * ZRECENTS Values(''" + recentTask.Userid + "','" + recentTask.Docid + "','" + recentTask.Doctypeid + "','" + recentTask.Name + "','" + recentTask.Date + "'')");
                string query = string.Format(QueryString.ToString());
                var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
            }

            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }

        }

        //getRecientes
        [Route("api/news/GetRecents")]
        public List<WidgetDTO> GetRecents(int Userid)
        {
            List<WidgetDTO> RecentsList = new List<WidgetDTO>();
            try
            {

                StringBuilder QueryString = new StringBuilder();

                QueryString.Append("select * from ZRECENTS where userid = '" + Userid + "'");
                
                var dataSet = Servers.Server.get_Con().ExecuteDataset(CommandType.Text, QueryString.ToString());
                DataTable firstTable = dataSet.Tables[0];

                foreach (DataRow row in firstTable.Rows)
                {

                    RecentsList.Add(new WidgetDTO(Int64.Parse(row["docid"].ToString()), Int64.Parse(row["doctypeid"].ToString()), row["name"].ToString()));
                }


            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);

            }
            return RecentsList;
        }

        //getFavorites
        [Route("api/news/GetFavorites")]
        public List<WidgetDTO> GetFavorites(int Userid)
        {
            StringBuilder QueryString = new StringBuilder();
            StringBuilder SecondQuery = new StringBuilder();
            //var Userid = Zamba.Membership.MembershipHelper.CurrentUser.ID;
            List<WidgetDTO> favoritesList = new List<WidgetDTO>();

            QueryString.Append("SELECT distinct doctypeid FROM DocumentLabels WHERE userid = " + Userid + " AND favorite = 1");
            string query = string.Format(QueryString.ToString());
            var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
            DataTable firstTable = dataSet.Tables[0];
            DataTable secondTable = null;

            if (firstTable.Rows.Count > 0)
            {
                foreach (DataRow i in firstTable.Rows)
                {
                    SecondQuery.Append("SELECT docid,name,doctypeid FROM DocumentLabels L inner join doc" + i[0] + " D on D.doc_id = L.docid WHERE userid = " + Userid + " AND favorite = 1");
                    string query2 = string.Format(SecondQuery.ToString());
                    var dataSet2 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query2);
                    secondTable = dataSet2.Tables[0];
                }
                foreach (DataRow row in secondTable.Rows)
                {
                    favoritesList.Add(new WidgetDTO(Int64.Parse(row["docid"].ToString()), Int64.Parse(row["doctypeid"].ToString()), row["name"].ToString()));
                    //string name = row["name"].ToString();
                }
            }
            return favoritesList;
        }

        //getImportants
        [Route("api/news/GetImportants")]
        public List<WidgetDTO> GetImportants(int docid, int recentId, int userId, int docId, int docTypeId, string name, int crdate)
        {
            List<WidgetDTO> ImportantsList = new List<WidgetDTO>();
            try
            {
                StringBuilder QueryString = new StringBuilder();
                StringBuilder SecondQuery = new StringBuilder();
                QueryString.Append("SELECT distinct doctypeid FROM DocumentLabels WHERE userid = 0 AND importance = 1 ");
                string query = string.Format(QueryString.ToString());
                var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
                DataTable firstTable = dataSet.Tables[0];


            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }
            return ImportantsList;
        }

        // GET: api/news/5
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/news/{id:int}")]
        [ResponseType(typeof(ZInformation))]
        public IHttpActionResult GetNewsModel(int id)
        {
            ZInformation ZInformation = db.ZInformation.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            if (ZInformation == null)
            {
                return NotFound();
            }
            return Ok(ZInformation);
        }

        // PUT: api/news/5
        [Route("api/news/{id:int}")]
        [AcceptVerbs("PUT")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNewsModel(int id, ZInformation ZInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != ZInformation.Id)
            {
                return BadRequest();
            }

            db.Entry(ZInformation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/news
        [ResponseType(typeof(ZInformation))]
        //[Route("api/news", Name = "PostNewsModel")]
        [AcceptVerbs("POST")]
        [HttpPost]
        public IHttpActionResult PostNewsModel(ZInformation ZInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ZInformation.Id > 0)
            {
                db.Entry(ZInformation).State = EntityState.Modified;
            }
            else
            {
                db.ZInformation.Add(ZInformation);
            }
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = ZInformation.Id }, ZInformation);
        }


        public IHttpActionResult DeleteNewsModel(int id)
        {
            ZInformation newModel = db.ZInformation.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            if (newModel == null)
            {
                return NotFound();
            }
            db.ZInformation.Remove(newModel);
            db.SaveChanges();
            return Ok(newModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewModelExists(int id)
        {
            return db.ZInformation.AsNoTracking().Count(e => e.Id == id) > 0;
        }
    }
}