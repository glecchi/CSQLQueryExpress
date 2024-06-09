
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;
using CSQLQueryExpress.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Sales Totals by Amount", Schema = "dbo")]
	    public class View_Sales_Totals_by_Amount : ISQLQueryEntity
	    {
		    [Column("SaleAmount")]
		    public decimal? SaleAmount { get; set; }

		    [Column("OrderID")]
		    public int OrderID { get; set; }

		    [Required]
		    [Column("CompanyName")]
		    public string CompanyName { get; set; }

		    [Column("ShippedDate")]
		    public DateTime? ShippedDate { get; set; }

	    }
    }
}