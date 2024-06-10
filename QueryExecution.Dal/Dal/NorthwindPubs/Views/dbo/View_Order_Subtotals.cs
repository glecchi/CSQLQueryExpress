
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;
using CSQLQueryExpress.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Order Subtotals", Schema = "dbo")]
	    public class View_Order_Subtotals : ISQLQueryEntity
	    {
		    [Column("OrderID")]
		    public int OrderID { get; set; }

		    [Column("Subtotal")]
		    public decimal? Subtotal { get; set; }

	    }
    }
}