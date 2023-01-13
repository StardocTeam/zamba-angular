namespace Kendo.Mvc.Examples.Models.Gantt
{
    using System.Linq;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using System;
    using System.Data;
    using System.Web;
    using System.Collections.Generic;

    public class GanttDependencyService
    {
        private static bool UpdateDatabase = false;
        private SampleEntities db;

        public GanttDependencyService(SampleEntities context)
        {
            db = context;
        }

        public GanttDependencyService()
            : this(new SampleEntities())
        {
        }

        public virtual IList<DependencyViewModel> GetAll()
        {
            var result = HttpContext.Current.Session["GanttDependencies"] as IList<DependencyViewModel>;

            if (result == null || UpdateDatabase)
            {
                result = db.GanttDependencies.ToList().Select(dependency => new DependencyViewModel
                {
                    DependencyID = dependency.ID,
                    PredecessorID = dependency.PredecessorID,
                    SuccessorID = dependency.SuccessorID,
                    Type = (DependencyType)dependency.Type
                }).ToList();

                HttpContext.Current.Session["GanttDependencies"] = result;
            }

            return result;
        }

        public virtual void Insert(DependencyViewModel dependency, ModelStateDictionary modelState)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.DependencyID).FirstOrDefault();
                var id = (first != null) ? first.DependencyID : 0;

                dependency.DependencyID = id + 1;

                GetAll().Insert(0, dependency);
            }
            else
            {
                var entity = dependency.ToEntity();

                db.GanttDependencies.Add(entity);
                db.SaveChanges();

                dependency.DependencyID = entity.ID;
            }
        }

        public virtual void Delete(DependencyViewModel dependency, ModelStateDictionary modelState)
        {
            if (!UpdateDatabase)
            {
                var target = GetAll().FirstOrDefault(p => p.DependencyID == dependency.DependencyID);
                if (target != null)
                {
                    GetAll().Remove(target);
                }
            }
            else
            {
                var entity = dependency.ToEntity();
                db.GanttDependencies.Attach(entity);
                db.GanttDependencies.Remove(entity);
                db.SaveChanges();
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}