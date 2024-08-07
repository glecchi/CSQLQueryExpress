using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("CustomerDemographics", Schema = "dbo")]
	    public class CustomerDemographics : ISQLQueryEntity
	    {
		    [Required]
		    [Column("CustomerTypeID")]
		    public string CustomerTypeID { get; set; }

		    [Column("CustomerDesc")]
		    public string CustomerDesc { get; set; }

	    }
    }
}