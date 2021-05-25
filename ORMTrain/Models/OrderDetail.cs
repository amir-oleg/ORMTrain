using LinqToDB.Mapping;

namespace ORMTrain.Models
{
	[Table(Schema = "dbo", Name = "Order Details")]
	public class OrderDetail
    {
		[PrimaryKey(1), NotNull] 
		public int OrderID { get; set; }
		[PrimaryKey(2), NotNull] 
		public int ProductID { get; set; }
		[Column, NotNull] 
		public decimal UnitPrice { get; set; }
		[Column, NotNull]
		public short Quantity { get; set; }
		[Column, NotNull] 
		public float Discount { get; set; }
		[Association(ThisKey = "OrderID", OtherKey = "Id", CanBeNull = false)]
		public Order OrderDetailsOrder { get; set; }

		[Association(ThisKey = "ProductID", OtherKey = "Id", CanBeNull = false)]
		public Product OrderDetailsProduct { get; set; }

	}
}
