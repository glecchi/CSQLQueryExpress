
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;
using CSQLQueryExpress.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Territories", Schema = "dbo")]
	    public class Territories : ISQLQueryEntity
	    {
		    [Required]
		    [Column("TerritoryID")]
		    public string TerritoryID { get; set; }

		    [Required]
		    [Column("TerritoryDescription")]
		    public string TerritoryDescription { get; set; }

		    [Column("RegionID")]
		    public int RegionID { get; set; }

	    }
    }
}