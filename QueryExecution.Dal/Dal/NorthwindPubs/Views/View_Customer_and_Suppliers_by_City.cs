
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Customer and Suppliers by City", Schema = "dbo")]
	    public class View_Customer_and_Suppliers_by_City : ISQLQueryEntity
	    {
		    [Column("City")]
		    public string City { get; set; }

		    [Required]
		    [Column("CompanyName")]
		    public string CompanyName { get; set; }

		    [Column("ContactName")]
		    public string ContactName { get; set; }

		    [Required]
		    [Column("Relationship")]
		    public string Relationship { get; set; }

	    }
    }
}