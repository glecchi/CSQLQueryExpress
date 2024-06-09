
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Category Sales for 1997", Schema = "dbo")]
	    public class View_Category_Sales_for_1997 : ISQLQueryEntity
	    {
		    [Required]
		    [Column("CategoryName")]
		    public string CategoryName { get; set; }

		    [Column("CategorySales")]
		    public decimal? CategorySales { get; set; }

	    }
    }
}