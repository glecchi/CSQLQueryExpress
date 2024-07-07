using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Current Product List", Schema = "dbo")]
	    public class View_Current_Product_List : ISQLQueryEntity
	    {
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		    [Column("ProductID")]
		    public int ProductID { get; set; }

		    [Required]
		    [Column("ProductName")]
		    public string ProductName { get; set; }

	    }
    }
}