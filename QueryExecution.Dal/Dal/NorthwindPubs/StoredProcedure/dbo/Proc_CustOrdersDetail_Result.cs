using System;

namespace QueryExecution.Dal.NorthwindPubs
{
    public class Proc_CustOrdersDetail_Result
	{
		public string ProductName { get; set; }

		public decimal UnitPrice { get; set; }

		public short Quantity { get; set; }

		public int? Discount { get; set; }

		public decimal? ExtendedPrice { get; set; }

	}
}