
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Order Details", Schema = "dbo")]
	    public class Order_Details : ISQLQueryEntity
	    {
		    [Column("OrderID")]
		    public int OrderID { get; set; }

		    [Column("ProductID")]
		    public int ProductID { get; set; }

		    [Column("UnitPrice")]
		    public decimal UnitPrice { get; set; }

		    [Column("Quantity")]
		    public short Quantity { get; set; }

		    [Column("Discount")]
		    public float Discount { get; set; }

	    }
    }
}