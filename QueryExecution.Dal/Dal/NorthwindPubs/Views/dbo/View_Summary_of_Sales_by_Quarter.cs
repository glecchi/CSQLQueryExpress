
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;
using CSQLQueryExpress.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Summary of Sales by Quarter", Schema = "dbo")]
	    public class View_Summary_of_Sales_by_Quarter : ISQLQueryEntity
	    {
		    [Column("ShippedDate")]
		    public DateTime? ShippedDate { get; set; }

		    [Column("OrderID")]
		    public int OrderID { get; set; }

		    [Column("Subtotal")]
		    public decimal? Subtotal { get; set; }

	    }
    }
}