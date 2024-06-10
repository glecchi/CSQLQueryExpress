
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;
using CSQLQueryExpress.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public class Proc_CustOrderHist_Result
	{
		public string ProductName { get; set; }

		public int? Total { get; set; }

	}
}