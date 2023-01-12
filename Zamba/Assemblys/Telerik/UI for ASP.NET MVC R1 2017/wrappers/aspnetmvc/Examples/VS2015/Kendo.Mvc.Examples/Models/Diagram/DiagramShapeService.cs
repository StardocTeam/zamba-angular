namespace Kendo.Mvc.Examples.Models
{
    using System.Linq;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using System;
    using System.Data;
    using System.Web;
    using System.Collections.Generic;

    public class DiagramShapeService
    {
        private static bool UpdateDatabase = false;
        private SampleEntities db;

        public DiagramShapeService(SampleEntities context)
        {
            db = context;
        }

        public DiagramShapeService()
            : this(new SampleEntities())
        {
        }

        public virtual IList<OrgChartShape> GetAll()
        {
            var result = HttpContext.Current.Session["DiagramShapes"] as IList<OrgChartShape>;

            if (result == null || UpdateDatabase)
            {
                result = db.OrgChartShapes.AsNoTracking().ToList();

                HttpContext.Current.Session["DiagramShapes"] = result;
            }

            return result;
        }

        public virtual void Insert(OrgChartShape shape, ModelStateDictionary modelState)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.Id).FirstOrDefault();
                var id = (first != null) ? first.Id : 0;

                shape.Id = id + 1;

                GetAll().Insert(0, shape);
            }
            else
            {
                db.OrgChartShapes.Add(shape);
                db.SaveChanges();

                shape.Id = shape.Id;
            }
        }

        public virtual void Update(OrgChartShape shape, ModelStateDictionary modelState)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.Id == shape.Id);

                if (target != null)
                {
                    target.JobTitle = shape.JobTitle;
                    target.Color = shape.Color;
                }
            }
            else
            {
                db.OrgChartShapes.Attach(shape);
                db.Entry(shape).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public virtual void Delete(OrgChartShape shape, ModelStateDictionary modelState)
        {
            if (!UpdateDatabase)
            {
                var target = One(p => p.Id == shape.Id);
                if (target != null)
                {
                    GetAll().Remove(target);
                }
            }
            else
            {
                db.OrgChartShapes.Attach(shape);

                Delete(shape);

                db.OrgChartShapes.Remove(shape);
                db.SaveChanges();
            }
        }

        private void Delete(OrgChartShape shape)
        {
            var result = db.OrgChartShapes.Where(t => t.Id == shape.Id).First();

            db.OrgChartShapes.Remove(result);
        }

        public OrgChartShape One(Func<OrgChartShape, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}