using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Kendo.Mvc.Examples.Models
{
    public class SpreadsheetProductService : IDisposable
    {
        private static bool UpdateDatabase = false;
        private SampleEntities entities;

        public SpreadsheetProductService(SampleEntities entities)
        {
            this.entities = entities;
        }

        public virtual IList<SpreadsheetProductViewModel> GetAll()
        {
            var result = HttpContext.Current.Session["SpreadsheetProducts"] as IList<SpreadsheetProductViewModel>;

            if (result == null || UpdateDatabase)
            {
                result = entities.Products.Select(product => new SpreadsheetProductViewModel
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice.HasValue ? product.UnitPrice.Value : default(decimal),
                    UnitsInStock = product.UnitsInStock.HasValue ? product.UnitsInStock.Value : default(short),
                    Discontinued = product.Discontinued
                }).ToList();

                HttpContext.Current.Session["SpreadsheetProducts"] = result;
            }

            return result;
        }

        public IEnumerable<SpreadsheetProductViewModel> Read()
        {
            return GetAll();
        }

        public void Create(SpreadsheetProductViewModel product)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.ProductID).FirstOrDefault();
                var id = (first != null) ? first.ProductID : 0;

                product.ProductID = id + 1;

                GetAll().Insert(0, product);
            }
            else
            {
                var entity = new Product();

                entity.ProductName = product.ProductName;
                entity.UnitPrice = product.UnitPrice;
                entity.UnitsInStock = (short)product.UnitsInStock;
                entity.Discontinued = product.Discontinued;

                if (entity.CategoryID == null)
                {
                    entity.CategoryID = 1;
                }

                entities.Products.Add(entity);
                entities.SaveChanges();

                product.ProductID = entity.ProductID;
            }
        }

        public void Update(SpreadsheetProductViewModel product)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.ProductID == product.ProductID);

                if (target != null)
                {
                    target.ProductName = product.ProductName;
                    target.UnitPrice = product.UnitPrice;
                    target.UnitsInStock = product.UnitsInStock;
                    target.Discontinued = product.Discontinued;
                }
            }
            else
            {
                var entity = new Product();

                entity.ProductID = product.ProductID;
                entity.ProductName = product.ProductName;
                entity.UnitPrice = product.UnitPrice;
                entity.UnitsInStock = (short)product.UnitsInStock;
                entity.Discontinued = product.Discontinued;

                entities.Products.Attach(entity);
                entities.Entry(entity).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        public void Destroy(SpreadsheetProductViewModel product)
        {
            if (!UpdateDatabase)
            {
                var target = GetAll().FirstOrDefault(p => p.ProductID == product.ProductID);
                if (target != null)
                {
                    GetAll().Remove(target);
                }
            }
            else
            {
                var entity = new Product();

                entity.ProductID = product.ProductID;

                entities.Products.Attach(entity);
                entities.Products.Remove(entity);

                var orderDetails = entities.Order_Details.Where(pd => pd.ProductID == entity.ProductID);

                foreach (var orderDetail in orderDetails)
                {
                    entities.Order_Details.Remove(orderDetail);
                }

                entities.SaveChanges();
            }
        }

        public SpreadsheetProductViewModel One(Func<SpreadsheetProductViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}