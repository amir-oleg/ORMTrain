using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace ORMTrain.Models
{
	[Table(Schema = "dbo", Name = "Shippers")]
	public class Shipper
	{
		[Column("ShipperID"), PrimaryKey, Identity] 
		public int Id { get; set; }
		[Column, NotNull] 
		public string CompanyName { get; set; }
		[Column, Nullable] 
		public string Phone { get; set; }
		[Association(ThisKey = "Id", OtherKey = "ShipVia")]
		public IEnumerable<Order> Orders { get; set; }
	}
}
