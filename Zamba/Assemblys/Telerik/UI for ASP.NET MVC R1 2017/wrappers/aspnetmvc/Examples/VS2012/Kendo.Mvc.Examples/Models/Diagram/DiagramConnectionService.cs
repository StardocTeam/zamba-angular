namespace Kendo.Mvc.Examples.Models
{
    using System.Linq;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using System;
    using System.Data;
    using System.Web;
    using System.Collections.Generic;

    public class DiagramConnectionService
    {
        private static bool UpdateDatabase = false;
        private SampleEntities db;

        public DiagramConnectionService(SampleEntities context)
        {
            db = context;
        }

        public DiagramConnectionService()
            : this(new SampleEntities())
        {
        }

        public virtual IList<OrgChartConnection> GetAll()
        {
            var result = HttpContext.Current.Session["DiagramConnections"] as IList<OrgChartConnection>;

            if (result == null || UpdateDatabase)
            {
                result = db.OrgChartConnections.AsNoTracking().ToList();

                HttpContext.Current.Session["DiagramConnections"] = result;
            }

            return result;
        }

        public virtual void Insert(OrgChartConnection connection, ModelStateDictionary modelState)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.Id).FirstOrDefault();
                var id = (first != null) ? first.Id : 0;

                connection.Id = id + 1;

                GetAll().Insert(0, connection);
            }
            else
            {
                db.OrgChartConnections.Add(connection);
                db.SaveChanges();

                connection.Id = connection.Id;
            }
        }

        public virtual void Update(OrgChartConnection connection, ModelStateDictionary modelState)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.Id == connection.Id);

                if (target != null)
                {
                    target.FromShapeId = connection.FromShapeId;
                    target.ToShapeId = connection.ToShapeId;
                    target.Text = connection.Text;
                    target.FromPointX = connection.FromPointX;
                    target.FromPointY = connection.FromPointY;
                    target.ToPointX = connection.ToPointX;
                    target.ToPointY = connection.ToPointY;
                }
            }
            else
            {
                db.OrgChartConnections.Attach(connection);
                db.Entry(connection).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public virtual void Delete(OrgChartConnection connection, ModelStateDictionary modelState)
        {
            if (!UpdateDatabase)
            {
                var target = One(p => p.Id == connection.Id);
                if (target != null)
                {
                    GetAll().Remove(target);
                }
            }
            else
            {
                db.OrgChartConnections.Attach(connection);

                Delete(connection);

                db.OrgChartConnections.Remove(connection);
                db.SaveChanges();
            }
        }

        private void Delete(OrgChartConnection connection)
        {
            var result = db.OrgChartShapes.Where(t => t.Id == connection.Id).First();

            db.OrgChartShapes.Remove(result);
        }

        public OrgChartConnection One(Func<OrgChartConnection, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}