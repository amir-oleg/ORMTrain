using LinqToDB;
using LinqToDB.Data;

namespace ORMTrain.Models
{
    public class NorthwindConnection: DataConnection
    {
        private const string connectionString = "Northwind";
        public NorthwindConnection(): base(connectionString) 
        { 
        }
		
		public ITable<Category> Categories { get { return this.GetTable<Category>(); } }
		public ITable<Customer> Customers { get { return this.GetTable<Customer>(); } }
		public ITable<Employee> Employees { get { return this.GetTable<Employee>(); } }
		public ITable<EmployeeTerritory> EmployeeTerritories { get { return this.GetTable<EmployeeTerritory>(); } }
		public ITable<Order> Orders { get { return this.GetTable<Order>(); } }
		public ITable<OrderDetail> OrderDetails { get { return this.GetTable<OrderDetail>(); } }
		public ITable<Product> Products { get { return this.GetTable<Product>(); } }
		public ITable<Region> Regions { get { return this.GetTable<Region>(); } }
		public ITable<Shipper> Shippers { get { return this.GetTable<Shipper>(); } }
		public ITable<Supplier> Suppliers { get { return this.GetTable<Supplier>(); } }
		public ITable<Territory> Territories { get { return this.GetTable<Territory>(); } }
	}
}
