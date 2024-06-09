
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [StoredProcedure("CustOrdersOrders", Schema = "dbo")]
	    public class Proc_CustOrdersOrders : ISQLStoredProcedure<Proc_CustOrdersOrders_Result>
	    {
            [Parameter("CustomerID")]
		    public string CustomerID { get; set; }

	    }
    }
}