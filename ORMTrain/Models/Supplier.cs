using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace ORMTrain.Models
{
	[Table(Schema = "dbo", Name = "Suppliers")]
	public class Supplier
	{
		[Column("SupplierID"), PrimaryKey, Identity] 
		public int Id { get; set; }
		[Column, NotNull] 
		public string CompanyName { get; set; }
		[Column, Nullable] 
		public string ContactName { get; set; }
		[Column, Nullable] 
		public string ContactTitle { get; set; }
		[Column, Nullable]
		public string Address { get; set; }
		[Column, Nullable] 
		public string City { get; set; }
		[Column, Nullable] 
		public string Region { get; set; }
		[Column, Nullable] 
		public string PostalCode { get; set; }
		[Column, Nullable] 
		public string Country { get; set; }
		[Column, Nullable] 
		public string Phone { get; set; }
		[Column, Nullable] 
		public string Fax { get; set; }
		[Column, Nullable] 
		public string HomePage { get; set; }

		[Association(ThisKey = "Id", OtherKey = "SupplierID")]
		public IEnumerable<Product> Products { get; set; }
	}
}
