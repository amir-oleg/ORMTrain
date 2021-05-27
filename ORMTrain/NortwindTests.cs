using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORMTrain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Linq;

namespace ORMTrain
{
    [TestClass]
    public class NortwindTests
    {
        [TestMethod]
        public void GetProductsWithCategoryAndSupplier()
        {
            using (var db = new NorthwindConnection())
            {
                var products = from p in db.Products
                    select new
                    {
                        Name = p.Name,
                        Category = p.Category.Name,
                        Supplier = p.Supplier.CompanyName
                    };
                foreach (var product in products)
                {
                    Console.WriteLine(product);
                }
            }
        }

        [TestMethod]
        public void GetEmployeesWithRegion()
        {
            using (var db = new NorthwindConnection())
            {
                var employees = from emp in db.Employees
                    select new
                    {
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        Regions = (from t in emp.EmployeeTerritories
                                select new
                                {
                                    Region = t.Territory.Region.RegionDescription
                                }).Distinct().Aggregate(new StringBuilder(),
                                (current, next) => current.Append(current.Length == 0 ? "" : ", ").Append(next))
                                .ToString()
                    };
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee);
                }
            }
        }

        [TestMethod]
        public void GetCountEmployeesByRegion()
        {
            using (var db = new NorthwindConnection())
            {
                var employees = from reg in db.Regions
                    select new
                    {
                        Region = reg.RegionDescription,
                        CountEmployees = (from emp in db.Employees
                            where emp.EmployeeTerritories.Any(et => reg.Id == (from ter in db.Territories
                                where et.TerritoryID == ter.Id
                                select ter.RegionID).First())
                            select emp).Count()
                    };

                foreach (var employee in employees)
                {
                    Console.WriteLine(employee);
                }
            }
        }

        [TestMethod]
        public void GetEmployeesShipCompany()
        {
            using (var db = new NorthwindConnection())
            {
                var employees = from emp in db.Employees
                    select new
                    {
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        ShipCompanies = (from ord in emp.Orders
                            select ord.Shipper.CompanyName).Distinct()
                                                           .Aggregate(new StringBuilder(),(current, next) => current.Append(current.Length == 0 ? "" : ", ")
                                                           .Append(next)).ToString()
                    };

                foreach (var employee in employees)
                {
                    Console.WriteLine(employee);
                }
            }
        }

        [TestMethod]
        public void CreateEmployeeWithTerritories()
        {
            int employeeId;
            using (var db = new NorthwindConnection())
            {

                employeeId = Convert.ToInt32(db.InsertWithIdentity(
                    new Employee()
                    {
                        FirstName = "Test",
                        LastName = "123",
                        ReportsTo = 1
                    }));
            }

            using (var db = new NorthwindConnection())
            {
                db.Insert(new EmployeeTerritory()
                {
                    EmployeeID = employeeId,
                    TerritoryID = "01581"
                });
                db.Insert(new EmployeeTerritory()
                {
                    EmployeeID = employeeId,
                    TerritoryID = "01730"
                });
            }
        }

        [TestMethod]
        public void ChangeProductCategory()
        {
            using (var db = new NorthwindConnection())
            {
                db.Products.Where(p => p.Category.Name == "Beverages")
                           .Set(p => p.CategoryID, 2)
                           .Update();
            }
        }

        [TestMethod]
        public void AddProduct()
        {
            var products = new Product[]
            {
                new Product()
                {
                    Category = new Category()
                    {
                        Name = "Beverages"
                    },
                    Supplier = new Supplier()
                    {
                        CompanyName = "Exotic Liquids"
                    },
                    Name = "Juice"
                },
                new Product()
                {
                    Category = new Category()
                    {
                        Name = "Something new"
                    },
                    Supplier = new Supplier()
                    {
                        CompanyName = "Saratov Airlines"
                    },
                    Name = "Energy water"
                }
            };
            using (var db = new NorthwindConnection())
            {
                foreach (var product in products)
                {
                    try
                    {
                        product.Category = db.Categories.Single(cat => cat.Name == product.Category.Name);
                        product.CategoryID = product.Category.Id;
                    }
                    catch (InvalidOperationException)
                    {
                        product.CategoryID = db.InsertWithInt32Identity(product.Category);
                    }

                    try
                    {
                        product.Supplier = db.Suppliers.Single(sup => sup.CompanyName == product.Supplier.CompanyName);
                        product.SupplierID = product.Supplier.Id;
                    }
                    catch (InvalidOperationException)
                    {
                        product.SupplierID = db.InsertWithInt32Identity(product.Supplier);
                    }
                }
                db.BulkCopy(products);
            }
        }

        [TestMethod]
        public void ReplaceProductsInUnfulfilledOrders()
        {
            var productIdOld = 20;
            var productIdNew = 85;
            Product productNew;
            using (var db = new NorthwindConnection())
            {
                try
                {
                    productNew = db.Products.First(product => product.Id == productIdNew);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    db.Dispose();
                    return;
                }
                
                var unfulfilledOrdersId = db.Orders.Where(order => order.ShippedDate == null)
                                                               .Select(order => order.Id);
                var ordersDetails = db.OrderDetails.Where(detail => unfulfilledOrdersId.Contains(detail.OrderID))
                    .Where(detail => detail.ProductID == productIdOld);
                productNew.UnitsOnOrder = ordersDetails.Sum(detail => detail.Quantity);
                db.Update(productNew);
                ordersDetails.Set(detail => detail.ProductID, productIdNew)
                             .Set(detail => detail.UnitPrice, productNew.UnitPrice)
                             .Update();
            }
        }
    }
}

