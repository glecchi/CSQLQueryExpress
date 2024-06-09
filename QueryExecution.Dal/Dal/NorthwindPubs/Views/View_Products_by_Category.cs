
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Products by Category", Schema = "dbo")]
	    public class View_Products_by_Category : ISQLQueryEntity
	    {
		    [Required]
		    [Column("CategoryName")]
		    public string CategoryName { get; set; }

		    [Required]
		    [Column("ProductName")]
		    public string ProductName { get; set; }

		    [Column("QuantityPerUnit")]
		    public string QuantityPerUnit { get; set; }

		    [Column("UnitsInStock")]
		    public short? UnitsInStock { get; set; }

		    [Column("Discontinued")]
		    public bool Discontinued { get; set; }

	    }
    }
}