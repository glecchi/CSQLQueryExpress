
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Categories", Schema = "dbo")]
	    public class Categories : ISQLQueryEntity
	    {
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		    [Column("CategoryID")]
		    public int CategoryID { get; set; }

		    [Required]
		    [Column("CategoryName")]
		    public string CategoryName { get; set; }

		    [Column("Description")]
		    public string Description { get; set; }

		    [Column("Picture")]
		    public byte[] Picture { get; set; }

	    }
    }
}