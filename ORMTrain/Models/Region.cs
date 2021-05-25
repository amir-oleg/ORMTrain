using System.Collections.Generic;
using LinqToDB.Mapping;

namespace ORMTrain.Models
{
    [Table(Schema = "dbo", Name = "Region")]
    public class Region
    {
		[Column("RegionID"), PrimaryKey] 
        public int Id { get; set; }
        [Column] 
        public string RegionDescription { get; set; }

        [Association(ThisKey = "Id", OtherKey = "RegionID", CanBeNull = false)]
        public IEnumerable<Territory> Territories { get; set; }
	}
}
