using LinqToDB.Mapping;
using System.Collections.Generic;

namespace ORMTrain.Models
{
	[Table(Schema = "dbo", Name = "Products")]
	public class Product
	{
		[Column("ProductID"), Identity, PrimaryKey]
		public int Id { get; set; }
		[Column("ProductName")]
		public string Name { get; set; }
		[Column] 
		public int SupplierID { get; set; }
		[Column]
		public int CategoryID { get; set; }
		[Column]
		public string QuantityPerUnit { get; set; }
		[Column]
		public decimal? UnitPrice { get; set; }
		[Column]
		public int? UnitsInStock { get; set; }
		[Column]
		public int? UnitsOnOrder { get; set; }
		[Column]
		public int? ReorderLevel { get; set; }
		[Column]
		public bool Discontinued { get; set; }
		[Association(ThisKey = "CategoryID", OtherKey = "Id", CanBeNull = true)]
		public Category Category { get; set; }
		[Association(ThisKey = "SupplierID", OtherKey = "Id", CanBeNull = true)]
		public Supplier Supplier { get; set; }
		[Association(ThisKey = "Id", OtherKey = "ProductID", CanBeNull = false)]
		public IEnumerable<OrderDetail> OrderDetails { get; set; }
	}
}
