
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [StoredProcedure("CustOrdersDetail", Schema = "dbo")]
	    public class Proc_CustOrdersDetail : ISQLStoredProcedure<Proc_CustOrdersDetail_Result>
	    {
            [Parameter("OrderID")]
		    public int OrderID { get; set; }

	    }
    }
}