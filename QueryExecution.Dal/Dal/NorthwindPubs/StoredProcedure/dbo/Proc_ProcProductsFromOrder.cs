using System;
using CSQLQueryExpress;
using CSQLQueryExpress.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [StoredProcedure("ProcProductsFromOrder", Schema = "dbo")]
	    public class Proc_ProcProductsFromOrder : ISQLStoredProcedure<Proc_ProcProductsFromOrder_Result>
	    {
            [Parameter("OrderID")]
		    public int OrderID { get; set; }

	    }
    }
}