namespace Kendo.Mvc.Examples.Models.TreeList
{
    using System.Linq;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Objects;
    using System.Collections.Generic;
    using System.Web;

    public static class EmployeeDirectoryIEnumerableExtensions
    {        
        public static EmployeeDirectoryModel ToEmployeeDirectoryModel(this EmployeeDirectory employee)
        {
            return new EmployeeDirectoryModel
            {
                EmployeeId = employee.EmployeeID,
                ReportsTo = employee.ReportsTo,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Address = employee.Address,
                City = employee.City,
                Country = employee.Country,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Phone = employee.Phone,
                Position = employee.Position,
                Extension = employee.Extension
            };
        }
    }

    public class EmployeeDirectoryService
    {
        private static bool UpdateDatabase = false;
        private SampleEntities db;

        public EmployeeDirectoryService(SampleEntities context)
        {
            db = context;
        }

        public EmployeeDirectoryService()
            : this(new SampleEntities())
        {
        }       

        public virtual IList<EmployeeDirectoryModel> GetAll()
        {
            var result = HttpContext.Current.Session["EmployeeDirectory"] as IList<EmployeeDirectoryModel>;

            if (result == null || UpdateDatabase)
            {
                result = db.EmployeeDirectory.ToList().Select(employee => employee.ToEmployeeDirectoryModel()).ToList();

                HttpContext.Current.Session["EmployeeDirectory"] = result;
            }

            return result;
        }

        public virtual void Insert(EmployeeDirectoryModel employee, ModelStateDictionary modelState)
        {
            if (ValidateModel(employee, modelState))
            {
                if (!UpdateDatabase)
                {
                    var first = GetAll().OrderByDescending(e => e.EmployeeId).FirstOrDefault();
                    var id = (first != null) ? first.EmployeeId : 0;

                    employee.EmployeeId = id + 1;

                    GetAll().Insert(0, employee);
                }
                else
                {
                    var entity = employee.ToEntity();

                    db.EmployeeDirectory.Add(entity);
                    db.SaveChanges();

                    employee.EmployeeId = entity.EmployeeID;
                }
            }
        }

        public virtual void Update(EmployeeDirectoryModel employee, ModelStateDictionary modelState)
        {
            if (ValidateModel(employee, modelState))
            {
                if (!UpdateDatabase)
                {
                    var target = One(e => e.EmployeeId == employee.EmployeeId);

                    if (target != null)
                    {
                        target.FirstName = employee.FirstName;
                        target.LastName = employee.LastName;
                        target.Address = employee.Address;
                        target.City = employee.City;
                        target.Country = employee.Country;
                        target.Phone = employee.Phone;
                        target.Extension = employee.Extension;
                        target.BirthDate = employee.BirthDate;
                        target.HireDate = employee.HireDate;
                        target.Position = employee.Position;
                        target.ReportsTo = employee.ReportsTo;
                    }
                }
                else
                {
                    var entity = employee.ToEntity();
                    db.EmployeeDirectory.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public virtual void Delete(EmployeeDirectoryModel employee, ModelStateDictionary modelState)
        {
            if (!UpdateDatabase)
            {
                var target = One(p => p.EmployeeId == employee.EmployeeId);
                if (target != null)
                {
                    DeleteSessionChildren(target);

                    GetAll().Remove(target);
                }
            }
            else
            {
                var entity = employee.ToEntity();

                db.EmployeeDirectory.Attach(entity);

                DeleteEntityChildren(entity);

                db.SaveChanges();
            }
        }

        private void DeleteEntityChildren(EmployeeDirectory employee)
        {
            var children = db.EmployeeDirectory.Where(e => e.ReportsTo == employee.EmployeeID);            

            foreach (var subordinate in children)
            {
                DeleteEntityChildren(subordinate);
            }

            db.EmployeeDirectory.Remove(employee);
        }

        private void DeleteSessionChildren(EmployeeDirectoryModel employee)
        {
            var allEmployees = GetAll();
            var employees = GetAll().Where(m => m.ReportsTo == employee.EmployeeId).ToList();

            foreach (var subordinate in employees)
            {
                DeleteSessionChildren(subordinate);

                allEmployees.Remove(subordinate);
            }
        }

        public EmployeeDirectoryModel One(Func<EmployeeDirectoryModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        private bool ValidateModel(EmployeeDirectoryModel employee, ModelStateDictionary modelState)
        {
            if (employee.HireDate < employee.BirthDate)
            {
                modelState.AddModelError("errors", "Employee cannot be hired before birth.");
                return false;
            }
            
            return true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}