using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Sales by Category", Schema = "dbo")]
	    public class View_Sales_by_Category : ISQLQueryEntity
	    {
		    [Column("CategoryID")]
		    public int CategoryID { get; set; }

		    [Required]
		    [Column("CategoryName")]
		    public string CategoryName { get; set; }

		    [Required]
		    [Column("ProductName")]
		    public string ProductName { get; set; }

		    [Column("ProductSales")]
		    public decimal? ProductSales { get; set; }

	    }
    }
}