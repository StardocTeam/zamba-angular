namespace Kendo.Mvc.Examples.Models.Gantt
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;

    public class GanttAssignmentService
    {
        private static bool UpdateDatabase = false;
        private SampleEntities db;

        public GanttAssignmentService(SampleEntities context)
        {
            db = context;
        }

        public GanttAssignmentService()
            : this(new SampleEntities())
        {
        }

        public virtual IList<ResourceAssignmentViewModel> GetAll()
        {
            var result = HttpContext.Current.Session["GanttAssignments"] as IList<ResourceAssignmentViewModel>;

            if (result == null || UpdateDatabase)
            {
                result = db.GanttResourceAssignments.ToList().Select(assignment => new ResourceAssignmentViewModel
                {
                    ID = assignment.ID,
                    TaskID = assignment.TaskID,
                    ResourceID = assignment.ResourceID,
                    Units = assignment.Units
                }).ToList();

                HttpContext.Current.Session["GanttAssignments"] = result;
            }

            return result;
        }

        public virtual void Insert(ResourceAssignmentViewModel assignment)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.ID).FirstOrDefault();
                var id = (first != null) ? first.ID : 0;

                assignment.ID = id + 1;

                GetAll().Insert(0, assignment);
            }
            else
            {
                var entity = assignment.ToEntity();

                db.GanttResourceAssignments.Add(entity);
                db.SaveChanges();

                assignment.ID = entity.ID;
            }
        }

        public virtual void Update(ResourceAssignmentViewModel assignment)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.ID == assignment.ID);

                if (target != null)
                {
                    target.ResourceID = assignment.ResourceID;
                    target.TaskID = assignment.TaskID;
                    target.Units = assignment.Units;
                }
            }
            else
            {
                var entity = assignment.ToEntity();

                db.GanttResourceAssignments.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public virtual void Delete(ResourceAssignmentViewModel assignment)
        {
            if (!UpdateDatabase)
            {
                var target = One(p => p.ID == assignment.ID);
                if (target != null)
                {
                    GetAll().Remove(target);
                }
            }
            else
            {
                var entity = assignment.ToEntity();

                db.GanttResourceAssignments.Attach(entity);
                db.GanttResourceAssignments.Remove(entity);
                db.SaveChanges();
            }
        }
        public ResourceAssignmentViewModel One(Func<ResourceAssignmentViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}