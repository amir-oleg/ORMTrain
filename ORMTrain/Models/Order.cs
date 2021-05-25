using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace ORMTrain.Models
{
	[Table(Schema = "dbo", Name = "Orders")]
	public class Order
    {
		[Column("OrderID"), PrimaryKey, Identity] 
		public int Id { get; set; }
		[Column, Nullable] 
		public string CustomerID { get; set; }
		[Column, Nullable] 
		public int? EmployeeID { get; set; }
		[Column, Nullable] 
		public DateTime? OrderDate { get; set; }
		[Column, Nullable] 
		public DateTime? RequiredDate { get; set; }
		[Column, Nullable] 
		public DateTime? ShippedDate { get; set; }
		[Column, Nullable] 
		public int? ShipVia { get; set; }
		[Column, Nullable] 
		public decimal? Freight { get; set; }
		[Column, Nullable] 
		public string ShipName { get; set; }
		[Column, Nullable] 
		public string ShipAddress { get; set; }
		[Column, Nullable] 
		public string ShipCity { get; set; }
		[Column, Nullable] 
		public string ShipRegion { get; set; }
		[Column, Nullable] 
		public string ShipPostalCode { get; set; }
		[Column, Nullable] 
		public string ShipCountry { get; set; }
		[Association(ThisKey = "CustomerID", OtherKey = "Id", CanBeNull = true)]
		public Customer Customer { get; set; }
		[Association(ThisKey = "EmployeeID", OtherKey = "Id", CanBeNull = true)]
		public Employee Employee { get; set; }
		[Association(ThisKey = "ShipVia", OtherKey = "Id", CanBeNull = true)]
		public Shipper Shipper { get; set; }
		[Association(ThisKey = "Id", OtherKey = "OrderID", CanBeNull = false)]
		public IEnumerable<OrderDetail> OrderDetails { get; set; }
	}
}
