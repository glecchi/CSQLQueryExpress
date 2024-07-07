using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Order Details Extended", Schema = "dbo")]
	    public class View_Order_Details_Extended : ISQLQueryEntity
	    {
		    [Column("OrderID")]
		    public int OrderID { get; set; }

		    [Column("ProductID")]
		    public int ProductID { get; set; }

		    [Required]
		    [Column("ProductName")]
		    public string ProductName { get; set; }

		    [Column("UnitPrice")]
		    public decimal UnitPrice { get; set; }

		    [Column("Quantity")]
		    public short Quantity { get; set; }

		    [Column("Discount")]
		    public float Discount { get; set; }

		    [Column("ExtendedPrice")]
		    public decimal? ExtendedPrice { get; set; }

	    }
    }
}