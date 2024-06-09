
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Shippers", Schema = "dbo")]
	    public class Shippers : ISQLQueryEntity
	    {
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		    [Column("ShipperID")]
		    public int ShipperID { get; set; }

		    [Required]
		    [Column("CompanyName")]
		    public string CompanyName { get; set; }

		    [Column("Phone")]
		    public string Phone { get; set; }

	    }
    }
}