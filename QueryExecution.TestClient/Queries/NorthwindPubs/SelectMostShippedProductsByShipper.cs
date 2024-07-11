using QueryExecution.Dapper.CommandFactory;
using QueryExecution.Dapper.CommandFactory.Commands;
using CSQLQueryExpress;
using CSQLQueryExpress.Fragments;
using QueryExecution.Dal.NorthwindPubs;
using CSQLQueryExpress.Schema;

namespace QueryExecution.TestClient.Queries.NorthwindPubs
{
    [SQLQueryCommand(5)]
    internal class SelectMostShippedProductsByShipper : SQLSelectQueryCommand<ShippedProductsByShipper>
    {
        public SelectMostShippedProductsByShipper(ISQLQueryCommandFactory commandFactory) : base(commandFactory)
        {
        }

        protected override SQLQuerySelect<ShippedProductsByShipper> GetQuerySelect()
        {
            var cte = new SQLQuery()
                .From<dbo.Shippers>()
                .InnerJoin<dbo.Orders>((sh, ord) => sh.ShipperID == ord.ShipVia)
                .InnerJoin<dbo.Order_Details>((sh, ord, ordDet) => ord.OrderID == ordDet.OrderID)
                .InnerJoin<ProductsCrossDatabase>((sh, ord, ordDet, prod) => ordDet.ProductID == prod.ProductID)
                .GroupBy(
                    (sh, ord, ordDet, prod) => sh.CompanyName,
                    (sh, ord, ordDet, prod) => prod.ProductName)
                .Select<ShippedProductsByShipper>(
                    (sh, ord, ordDet, prod, res) => sh.CompanyName,
                    (sh, ord, ordDet, prod, res) => prod.ProductName.IsNull("UNKNOWN").As(res.ProductName),
                    (sh, ord, ordDet, prod, res) => Count.All().As(res.ProductCount),
                    (sh, ord, ordDet, prod, res) => Row.Number().Over(n => n.PartitionBy(sh.CompanyName).OrderBy(Count.All().Desc())).As(res.RowNumber))
                .ToCteTable();

            var query = new SQLQuery()
                .From(cte)
                .Where(c => c.RowNumber <= 10)
                .OrderBy(c => c.RowNumber.Asc(), c => c.CompanyName.Asc())
                .Select(c => c.All());

            return query;
        }
    }

    [Database("NorthwindPubs")]
    class ProductsCrossDatabase : dbo.Products
    {

    }

    class ShippedProductsByShipper : ISQLQueryEntity
    {
        public string CompanyName { get; set; }

        public string ProductName { get; set; }

        public int ProductCount { get; set; }

        public int RowNumber { get; set; }

        public decimal? UnitPrice { get; set; }
    }
}
