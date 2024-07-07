using System;

namespace QueryExecution.Dal.NorthwindPubs
{
    public class Proc_CustOrdersOrders_Result
	{
		public int OrderID { get; set; }

		public DateTime? OrderDate { get; set; }

		public DateTime? RequiredDate { get; set; }

		public DateTime? ShippedDate { get; set; }

	}
}