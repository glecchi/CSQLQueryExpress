
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Quarterly Orders", Schema = "dbo")]
	    public class View_Quarterly_Orders : ISQLQueryEntity
	    {
		    [Column("CustomerID")]
		    public string CustomerID { get; set; }

		    [Column("CompanyName")]
		    public string CompanyName { get; set; }

		    [Column("City")]
		    public string City { get; set; }

		    [Column("Country")]
		    public string Country { get; set; }

	    }
    }
}