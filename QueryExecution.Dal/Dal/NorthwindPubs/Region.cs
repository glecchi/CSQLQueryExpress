
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Region", Schema = "dbo")]
	    public class Region : ISQLQueryEntity
	    {
		    [Column("RegionID")]
		    public int RegionID { get; set; }

		    [Required]
		    [Column("RegionDescription")]
		    public string RegionDescription { get; set; }

	    }
    }
}