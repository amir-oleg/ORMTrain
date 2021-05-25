using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace ORMTrain.Models
{
    [Table(Schema = "dbo", Name = "Employees")]
    public class Employee
    {
        [Column("EmployeeID"), Identity, PrimaryKey]
        public int Id { get; set; }
        [Column]
        public string LastName { get; set; }
        [Column]
        public string FirstName { get; set; }
        [Column]
        public string Title { get; set; }
        [Column, Nullable]
        public DateTime? BirthDate { get; set; }
        [Column, Nullable]
        public DateTime? HireDate { get; set; }
        [Column]
        public string Address { get; set; }
        [Column]
        public string City { get; set; }
        [Column]
        public string Region { get; set; }
        [Column]
        public string PostalCode { get; set; }
        [Column]
        public string Country { get; set; }
        [Column]
        public string HomePhone { get; set; }
        [Column]
        public string Extension { get; set; }
        [Column]
        public byte[] Photo { get; set; }
        [Column]
        public string Notes { get; set; }
        [Column]
        public int ReportsTo { get; set; }
        [Column]
        public string PhotoPath { get; set; }
        [Association(ThisKey = "Id", OtherKey = "EmployeeID")]
        public IEnumerable<Order> Orders { get; set; }
        [Association(ThisKey = "Id", OtherKey = "EmployeeID")]
        public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }

        [Association(ThisKey = "ReportsTo", OtherKey = "EmployeeID", CanBeNull = true)]
        public Employee FK_Employees_Employee { get; set; }
        
        [Association(ThisKey = "Id", OtherKey = "ReportsTo", CanBeNull = false)]
        public IEnumerable<Employee> FK_Employees_Employees_BackReferences { get; set; }
    }
}
