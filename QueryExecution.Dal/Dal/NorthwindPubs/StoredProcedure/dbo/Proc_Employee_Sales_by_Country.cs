using System;
using CSQLQueryExpress;
using CSQLQueryExpress.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [StoredProcedure("Employee Sales by Country", Schema = "dbo")]
	    public class Proc_Employee_Sales_by_Country : ISQLStoredProcedure<Proc_Employee_Sales_by_Country_Result>
	    {
            [Parameter("Beginning_Date")]
		    public DateTime Beginning_Date { get; set; }

            [Parameter("Ending_Date")]
		    public DateTime Ending_Date { get; set; }

	    }
    }
}