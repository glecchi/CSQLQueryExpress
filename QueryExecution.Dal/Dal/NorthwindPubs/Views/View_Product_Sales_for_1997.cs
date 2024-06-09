
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Product Sales for 1997", Schema = "dbo")]
	    public class View_Product_Sales_for_1997 : ISQLQueryEntity
	    {
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