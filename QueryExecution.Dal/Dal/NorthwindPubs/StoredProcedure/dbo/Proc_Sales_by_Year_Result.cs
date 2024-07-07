using System;

namespace QueryExecution.Dal.NorthwindPubs
{
    public class Proc_Sales_by_Year_Result
	{
		public DateTime? ShippedDate { get; set; }

		public int OrderID { get; set; }

		public decimal? Subtotal { get; set; }

		public string Year { get; set; }

	}
}