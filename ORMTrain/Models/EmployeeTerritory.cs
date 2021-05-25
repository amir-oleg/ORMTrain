using LinqToDB.Mapping;

namespace ORMTrain.Models
{
	[Table(Schema = "dbo", Name = "EmployeeTerritories")]
	public class EmployeeTerritory
    {
		[PrimaryKey(1), NotNull] 
		public int EmployeeID { get; set; }
		[PrimaryKey(2), NotNull] 
		public string TerritoryID { get; set; }
		[Association(ThisKey = "EmployeeID", OtherKey = "Id")]
		public Employee Employee { get; set; }
		[Association(ThisKey = "TerritoryID", OtherKey = "Id")]
		public Territory Territory { get; set; }
	}
}
