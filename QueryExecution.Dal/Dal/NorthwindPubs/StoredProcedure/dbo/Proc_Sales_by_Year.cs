
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [StoredProcedure("Sales by Year", Schema = "dbo")]
	    public class Proc_Sales_by_Year : ISQLStoredProcedure<Proc_Sales_by_Year_Result>
	    {
            [Parameter("Beginning_Date")]
		    public DateTime Beginning_Date { get; set; }

            [Parameter("Ending_Date")]
		    public DateTime Ending_Date { get; set; }

	    }
    }
}