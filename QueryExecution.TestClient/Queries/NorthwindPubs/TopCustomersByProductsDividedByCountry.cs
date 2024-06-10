using QueryExecution.Dal.NorthwindPubs;
using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Extensions;
using CSQLQueryExpress.Fragments;
using CSQLQueryExpress.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(5)]
    internal class TopCustomersByProductsDividedByCountry : SQLSelectQueryCommand<CustomerByOrdersAndCountry>
    {
        public TopCustomersByProductsDividedByCountry(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
        {
        }

        protected override SQLQuerySelect<CustomerByOrdersAndCountry> GetQuerySelect()
        {
            var cte = new SQLQuery()
                .From<dbo.Customers>()
                .InnerJoin<dbo.Orders>((cus, ord) => cus.CustomerID == ord.CustomerID)
                .InnerJoin<dbo.Order_Details>((cus, ord, ordDet) => ord.OrderID == ordDet.OrderID)
                .InnerJoin<dbo.Products>((cus, ord, ordDet, prod) => prod.ProductID == ordDet.ProductID)
                .GroupBy(
                    (cus, ord, ordDet, prod) => cus.Country,
                    (cus, ord, ordDet, prod) => cus.ContactName,
                    (cus, ord, ordDet, prod) => prod.ProductName)
                .Having((cus, ord, ordDet, prod) => Count.All() > 3)
                .Select<CustomerByOrdersAndCountry>(
                    (cus, ord, ordDet, prod, res) => cus.Country,
                    (cus, ord, ordDet, prod, res) => cus.ContactName,
                    (cus, ord, ordDet, prod, res) => prod.ProductName,
                    (cus, ord, ordDet, prod, res) => Count.All().As(res.Orders),
                    (cus, ord, ordDet, prod, res) => Row.Number().Over(o => o.OrderBy(() => Count.All().Desc())).As(res.Row))
                .ToCteTable();

            var query = new SQLQuery()
                .From(cte)
                .OrderBy(c => c.Country, c => c.Row, c => c.ProductName)
                .Select(c => c.All());

            return query;
        }
    }

    class CustomerByOrdersAndCountry : ISQLQueryEntity
    {
        public string Country { get; set; }

        public string ProductName { get; set; }

        public string ContactName { get; set; }

        public int Orders { get; set; }

        public int Row { get; set; }        
    }
}
