using System.Collections.Generic;
using LinqToDB.Mapping;

namespace ORMTrain.Models
{
	[Table(Schema = "dbo", Name = "Categories")]
	public class Category
	{
		[Column("CategoryID"), PrimaryKey, Identity]
		public int Id { get; set; }
		[Column("CategoryName")]
		public string Name { get; set; }
		[Column]
		public string Description { get; set; }
		[Column]
		public byte[] Picture { get; set; }

        [Association(ThisKey = "Id", OtherKey = "CategoryID", CanBeNull = false)]
        public IEnumerable<Product> Products { get; set; }
	}
}
