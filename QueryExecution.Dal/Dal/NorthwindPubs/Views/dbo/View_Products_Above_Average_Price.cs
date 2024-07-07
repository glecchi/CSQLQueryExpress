using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Products Above Average Price", Schema = "dbo")]
	    public class View_Products_Above_Average_Price : ISQLQueryEntity
	    {
		    [Required]
		    [Column("ProductName")]
		    public string ProductName { get; set; }

		    [Column("UnitPrice")]
		    public decimal? UnitPrice { get; set; }

	    }
    }
}