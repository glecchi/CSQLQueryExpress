
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;
using CSQLQueryExpress.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("EmployeeTerritories", Schema = "dbo")]
	    public class EmployeeTerritories : ISQLQueryEntity
	    {
		    [Column("EmployeeID")]
		    public int EmployeeID { get; set; }

		    [Required]
		    [Column("TerritoryID")]
		    public string TerritoryID { get; set; }

	    }
    }
}