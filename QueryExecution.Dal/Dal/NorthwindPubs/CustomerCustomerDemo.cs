
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("CustomerCustomerDemo", Schema = "dbo")]
	    public class CustomerCustomerDemo : ISQLQueryEntity
	    {
		    [Required]
		    [Column("CustomerID")]
		    public string CustomerID { get; set; }

		    [Required]
		    [Column("CustomerTypeID")]
		    public string CustomerTypeID { get; set; }

	    }
    }
}