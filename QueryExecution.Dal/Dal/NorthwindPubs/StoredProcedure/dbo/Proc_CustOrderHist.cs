
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [StoredProcedure("CustOrderHist", Schema = "dbo")]
	    public class Proc_CustOrderHist : ISQLStoredProcedure<Proc_CustOrderHist_Result>
	    {
            [Parameter("CustomerID")]
		    public string CustomerID { get; set; }

	    }
    }
}