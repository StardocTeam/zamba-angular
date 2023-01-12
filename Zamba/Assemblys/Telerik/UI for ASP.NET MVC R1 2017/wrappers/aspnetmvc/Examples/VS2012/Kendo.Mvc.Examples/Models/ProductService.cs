using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Kendo.Mvc.Examples.Models
{
    public class ProductService : IDisposable
    {
        private static bool UpdateDatabase = false;
        private SampleEntities entities;

        public ProductService(SampleEntities entities)
        {
            this.entities = entities;
        }

        public IList<ProductViewModel> GetAll()
        {
            IList<ProductViewModel> result = new List<ProductViewModel>();

            result = entities.Products.Select(product => new ProductViewModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice.HasValue ? product.UnitPrice.Value : default(decimal),
                UnitsInStock = product.UnitsInStock.HasValue ? product.UnitsInStock.Value : default(short),
                QuantityPerUnit = product.QuantityPerUnit,
                Discontinued = product.Discontinued,
                UnitsOnOrder = product.UnitsOnOrder.HasValue ? (int)product.UnitsOnOrder.Value : default(int),
                CategoryID = product.CategoryID,
                Category = new CategoryViewModel()
                {
                    CategoryID = product.Category.CategoryID,
                    CategoryName = product.Category.CategoryName
                },
                LastSupply = DateTime.Today
            }).ToList();


            return result;
        }

        public IEnumerable<ProductViewModel> Read()
        {
            return GetAll();
        }

        public void Create(ProductViewModel product)
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
                entity.CategoryID = product.CategoryID;

                if (entity.CategoryID == null)
                {
                    entity.CategoryID = 1;
                }

                if (product.Category != null)
                {
                    entity.CategoryID = product.Category.CategoryID;
                }

                entities.Products.Add(entity);
                entities.SaveChanges();

                product.ProductID = entity.ProductID;
            }
        }

        public void Update(ProductViewModel product)
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
                    target.CategoryID = product.CategoryID;
                    target.Category = product.Category;
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
                entity.CategoryID = product.CategoryID;

                if (product.Category != null)
                {
                    entity.CategoryID = product.Category.CategoryID;
                }

                entities.Products.Attach(entity);
                entities.Entry(entity).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        public void Destroy(ProductViewModel product)
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

        public ProductViewModel One(Func<ProductViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}