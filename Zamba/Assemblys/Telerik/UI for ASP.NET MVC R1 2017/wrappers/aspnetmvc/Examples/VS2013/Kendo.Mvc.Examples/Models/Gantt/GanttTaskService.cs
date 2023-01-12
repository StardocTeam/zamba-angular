namespace Kendo.Mvc.Examples.Models.Gantt
{
    using System.Linq;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using System;
    using System.Data;
    using System.Web;
    using System.Collections.Generic;

    public class GanttTaskService
    {
        private static bool UpdateDatabase = false;
        private SampleEntities db;

        public GanttTaskService(SampleEntities context)
        {
            db = context;
        }

        public GanttTaskService()
            : this(new SampleEntities())
        {
        }

        public virtual IList<TaskViewModel> GetAll()
        {
            var result = HttpContext.Current.Session["GanttTasks"] as IList<TaskViewModel>;

            if (result == null || UpdateDatabase)
            {
                result = db.GanttTasks.ToList().Select(task => new TaskViewModel
                {
                    TaskID = task.ID,
                    Title = task.Title,
                    Start = DateTime.SpecifyKind(task.Start, DateTimeKind.Utc),
                    End = DateTime.SpecifyKind(task.End, DateTimeKind.Utc),
                    ParentID = task.ParentID,
                    PercentComplete = task.PercentComplete,
                    OrderId = task.OrderID,
                    Expanded = task.Expanded,
                    Summary = task.Summary
                }).ToList();

                HttpContext.Current.Session["GanttTasks"] = result;
            }

            return result;
        }

        public virtual void Insert(TaskViewModel task, ModelStateDictionary modelState)
        {
            if (ValidateModel(task, modelState))
            {
                if (!UpdateDatabase)
                {
                    var first = GetAll().OrderByDescending(e => e.TaskID).FirstOrDefault();
                    var id = (first != null) ? first.TaskID : 0;

                    task.TaskID = id + 1;

                    GetAll().Insert(0, task);
                }
                else
                {
                    if (string.IsNullOrEmpty(task.Title))
                    {
                        task.Title = "";
                    }

                    var entity = task.ToEntity();

                    db.GanttTasks.Add(entity);
                    db.SaveChanges();

                    task.TaskID = entity.ID;
                }
            }
        }

        public virtual void Update(TaskViewModel task, ModelStateDictionary modelState)
        {
            if (ValidateModel(task, modelState))
            {
                if (!UpdateDatabase)
                {
                    var target = One(e => e.TaskID == task.TaskID);

                    if (target != null)
                    {
                        target.Title = task.Title;
                        target.Start = task.Start;
                        target.End = task.End;
                        target.PercentComplete = task.PercentComplete;
                        target.OrderId = task.OrderId;
                        target.ParentID = task.ParentID;
                        target.Summary = task.Summary;
                        target.Expanded = task.Expanded;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(task.Title))
                    {
                        task.Title = "";
                    }

                    var entity = task.ToEntity();
                    db.GanttTasks.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public virtual void Delete(TaskViewModel task, ModelStateDictionary modelState)
        {
            if (!UpdateDatabase)
            {
                var target = One(p => p.TaskID == task.TaskID);
                if (target != null)
                {
                    DeleteSessionChildren(target);

                    GetAll().Remove(target);
                }
            }
            else
            {
                var entity = task.ToEntity();
                db.GanttTasks.Attach(entity);

                DeleteEntityChildren(entity);

                db.GanttTasks.Remove(entity);
                db.SaveChanges();
            }
        }

        private void DeleteEntityChildren(GanttTask task)
        {
            var childTasks = db.GanttTasks.Where(t => t.ParentID == task.ID);

            foreach (var childTask in childTasks)
            {
                DeleteEntityChildren(childTask);
                db.GanttTasks.Remove(childTask);
            }
        }

        private void DeleteSessionChildren(TaskViewModel task)
        {
            var allTasks = GetAll();
            var childTasks = allTasks.Where(t => t.ParentID == task.TaskID).ToList();

            foreach (var childTask in childTasks)
            {
                DeleteSessionChildren(childTask);
                allTasks.Remove(childTask);
            }
        }

        private bool ValidateModel(TaskViewModel task, ModelStateDictionary modelState)
        {
            if (task.Start > task.End)
            {
                modelState.AddModelError("errors", "End date must be greater or equal to Start date.");
                return false;
            }
            
            return true;
        }

        public TaskViewModel One(Func<TaskViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}