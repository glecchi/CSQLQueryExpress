
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public class Proc_Employee_Sales_by_Country_Result
	{
		public string Country { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public DateTime? ShippedDate { get; set; }

		public int OrderID { get; set; }

		public decimal? SaleAmount { get; set; }

	}
}