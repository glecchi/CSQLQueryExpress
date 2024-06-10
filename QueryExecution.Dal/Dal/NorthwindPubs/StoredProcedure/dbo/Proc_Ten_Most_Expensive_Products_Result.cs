
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;
using CSQLQueryExpress.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public class Proc_Ten_Most_Expensive_Products_Result
	{
		public string TenMostExpensiveProducts { get; set; }

		public decimal? UnitPrice { get; set; }

	}
}