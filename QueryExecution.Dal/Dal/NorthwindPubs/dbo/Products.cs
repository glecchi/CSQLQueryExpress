using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Products", Schema = "dbo")]
	    public class Products : ISQLQueryEntity
	    {
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		    [Column("ProductID")]
		    public int ProductID { get; set; }

		    [Required]
		    [Column("ProductName")]
		    public string ProductName { get; set; }

		    [Column("SupplierID")]
		    public int? SupplierID { get; set; }

		    [Column("CategoryID")]
		    public int? CategoryID { get; set; }

		    [Column("QuantityPerUnit")]
		    public string QuantityPerUnit { get; set; }

		    [Column("UnitPrice")]
		    public decimal? UnitPrice { get; set; }

		    [Column("UnitsInStock")]
		    public short? UnitsInStock { get; set; }

		    [Column("UnitsOnOrder")]
		    public short? UnitsOnOrder { get; set; }

		    [Column("ReorderLevel")]
		    public short? ReorderLevel { get; set; }

		    [Column("Discontinued")]
		    public bool Discontinued { get; set; }

	    }
    }
}