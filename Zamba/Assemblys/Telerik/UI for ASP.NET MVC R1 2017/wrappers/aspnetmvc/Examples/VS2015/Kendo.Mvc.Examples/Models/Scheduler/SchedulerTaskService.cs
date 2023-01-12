namespace Kendo.Mvc.Examples.Models.Scheduler
{
    using System.Linq;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Web;

    public class SchedulerTaskService : ISchedulerEventService<TaskViewModel>
    {
        private static bool UpdateDatabase = false;
        private SampleEntities db;

        public SchedulerTaskService(SampleEntities context)
        {
            db = context;
        }

        public SchedulerTaskService()
            : this(new SampleEntities())
        {
        }

        public virtual IList<TaskViewModel> GetAll()
        {
            bool IsWebApiRequest = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
            IList <TaskViewModel> result = null;

            if(!IsWebApiRequest){
                result = HttpContext.Current.Session["SchedulerTasks"] as IList<TaskViewModel>;
            }

            if (result == null || UpdateDatabase)
            {
                result = db.Tasks.ToList().Select(task => new TaskViewModel
                {
                    TaskID = task.TaskID,
                    Title = task.Title,
                    Start = DateTime.SpecifyKind(task.Start, DateTimeKind.Utc),
                    End = DateTime.SpecifyKind(task.End, DateTimeKind.Utc),
                    StartTimezone = task.StartTimezone,
                    EndTimezone = task.EndTimezone,
                    Description = task.Description,
                    IsAllDay = task.IsAllDay,
                    RecurrenceRule = task.RecurrenceRule,
                    RecurrenceException = task.RecurrenceException,
                    RecurrenceID = task.RecurrenceID,
                    OwnerID = task.OwnerID
                }).ToList();

                if (!IsWebApiRequest)
                {
                    HttpContext.Current.Session["SchedulerTasks"] = result;
                }
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

                    db.Tasks.Add(entity);
                    db.SaveChanges();

                    task.TaskID = entity.TaskID;
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
                        target.StartTimezone = task.StartTimezone;
                        target.EndTimezone = task.EndTimezone;
                        target.Description = task.Description;
                        target.IsAllDay = task.IsAllDay;
                        target.RecurrenceRule = task.RecurrenceRule;
                        target.RecurrenceException = task.RecurrenceException;
                        target.RecurrenceID = task.RecurrenceID;
                        target.OwnerID = task.OwnerID;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(task.Title))
                    {
                        task.Title = "";
                    }

                    var entity = task.ToEntity();
                    db.Tasks.Attach(entity);
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
                    GetAll().Remove(target);

                    var recurrenceExceptions = GetAll().Where(m => m.RecurrenceID == task.TaskID).ToList();

                    foreach (var recurrenceException in recurrenceExceptions)
                    {
                        GetAll().Remove(recurrenceException);
                    }
                }
            }
            else
            {
                var entity = task.ToEntity();
                db.Tasks.Attach(entity);

                var recurrenceExceptions = db.Tasks.Where(t => t.RecurrenceID == task.TaskID);

                foreach (var recurrenceException in recurrenceExceptions)
                {
                    db.Tasks.Remove(recurrenceException);
                }

                db.Tasks.Remove(entity);
                db.SaveChanges();
            }
        }

        private bool ValidateModel(TaskViewModel appointment, ModelStateDictionary modelState)
        {
            if (appointment.Start > appointment.End)
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