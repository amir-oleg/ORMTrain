using System.Collections.Generic;
using LinqToDB.Mapping;

namespace ORMTrain.Models
{
    [Table(Schema = "dbo", Name = "Territories")]
    public class Territory
    {
        [Column("TerritoryID"), PrimaryKey, Identity]
        public string Id { get; set; }
        [Column]
        public string TerritoryDescription { get; set; }
        [Association(ThisKey = "RegionID", OtherKey = "Id")]
        public Region Region { get; set; }
        [Column]
        public int RegionID { get; set; }

        [Association(ThisKey = "Id", OtherKey = "TerritoryID")]
        public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }
    }
}
