using System;
using CSQLQueryExpress;
using CSQLQueryExpress.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [StoredProcedure("SalesByCategory", Schema = "dbo")]
	    public class Proc_SalesByCategory : ISQLStoredProcedure<Proc_SalesByCategory_Result>
	    {
            [Parameter("CategoryName")]
		    public string CategoryName { get; set; }

            [Parameter("OrdYear")]
		    public string OrdYear { get; set; }

	    }
    }
}